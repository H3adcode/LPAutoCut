using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace LPAutoCut {
    static class Program {
        static System.Timers.Timer updateTimer;
        static List<Marker> markers = new List<Marker>();
        static DateTime start, stop;
        static DateTime currentEpisodeStart;
        static DateTime currentEpisodeEnd;
        static Form1 form;
        static bool isEpisode = false;
        static bool isStarted = false;
        static string timecodeExportFormat = "hh\\:mm\\:ss";

        static Dictionary<SettingName, string> settings;
        static Dictionary<SettingName, string> settingsDefault;

        public enum SettingName { Alert, AlertTime }
        
        public enum MarkerType { EpStart, EpEnd, Edit, Cut, Mark };

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main() {
            settingsDefault = new Dictionary<SettingName, string>();
            settingsDefault.Add(SettingName.Alert, "TRUE");
            settingsDefault.Add(SettingName.AlertTime, "00:20:00");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();

            loadSettings();

            Application.Run(form);
        }
        
        internal static void StartTimer() {
            markers.Clear();
            start = DateTime.Now;
            updateTimer = new System.Timers.Timer(1000);
            updateTimer.Elapsed += new ElapsedEventHandler(OnUpdateTimerElapsed);
            updateTimer.Enabled = true;

            isStarted = true;

            form.OnStart();
        }

        internal static void StopTimer() {
            form.OnStop();
            stop = DateTime.Now;
            updateTimer.Enabled = false;
            if (isEpisode)
                StopEpisode();
            isStarted = false;
        }

        internal static void StartEpisode() {
            if (!isStarted) return;
            SetMarker(MarkerType.EpStart);
            currentEpisodeStart = DateTime.Now;
            isEpisode = true;

            form.OnEpisodeStart();
        }

        internal static void StopEpisode() {
            if (!isStarted) return;
            SetMarker(MarkerType.EpEnd);
            currentEpisodeEnd = DateTime.Now;
            form.AddEpisodeTime(currentEpisodeStart.Subtract(start), currentEpisodeEnd.Subtract(start));
            isEpisode = false;
            form.OnEpisodeStop();
        }

        internal static void SetMarker(MarkerType type) {
            Marker marker = new Marker();
            marker.timestamp = DateTime.Now.Subtract(start);
            marker.type = type;
            markers.Add(marker);
            form.AddMarkerInfo(marker.timestamp, type.ToString());
        }

        static void OnUpdateTimerElapsed(object sender, ElapsedEventArgs e) {
            DateTime updateTime = DateTime.Now;
            UpdateTotalTime(updateTime);
            UpdateEpisodeTime(updateTime);
            if (isEpisode && IsAlertActive() && TimeSpan.Compare(updateTime.Subtract(currentEpisodeStart), GetAlertTimeSpan()) > 0)
                form.OnTimeAlertOn();
            else
                form.OnTimeAlertOff();
        }

        static void UpdateTotalTime(DateTime updateTime) {
            form.SetTotalTime(updateTime.Subtract(start));
        }

        static void UpdateEpisodeTime(DateTime updateTime) {
            if(isEpisode)
                form.SetEpTime(updateTime.Subtract(currentEpisodeStart));
            else
                form.ResetEpTime();
        }

        internal static void loadSettings() {
            try {
                string[] settingsRaw = System.IO.File.ReadAllLines(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Settings.txt");
                settings = new Dictionary<SettingName, string>();
                for (int i = 0; i < settingsRaw.Length; i++) {
                    string[] settingParts = settingsRaw[i].Split('=');
                    SettingName currentSetting = (SettingName)Enum.Parse(typeof(SettingName), settingParts[0]);
                    //if(!settings.ContainsKey(currentSetting)
                    //    settings.Add(currentSetting, "");    
                    settings[currentSetting] = settingParts[1].ToUpper();
                }
            } catch (Exception e) {
                Console.Error.WriteLine("Could not load settings. Reset to default Settings.");
                settings = new Dictionary<SettingName, string>(settingsDefault);
            }
            form.SetAlertChecked(IsAlertActive());
            form.SetAlertTime(GetAlertDateTime());
        }

        internal static void SetAlert(bool alert) {
            settings[SettingName.Alert] = alert.ToString().ToUpper();
        }

        internal static void SetAlertTime(DateTime alertTime) {
            settings[SettingName.AlertTime] = alertTime.ToString("HH\\:mm\\:ss");
        }

        internal static void saveSettings() {
            System.IO.File.WriteAllLines(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Settings.txt", settings.Select(i => i.Key.ToString() + "=" + i.Value).ToArray());
        }

        internal static bool IsAlertActive() {
            return settings[SettingName.Alert].CompareTo("TRUE") == 0;
        }

        internal static TimeSpan GetAlertTimeSpan() {
            string[] splits = settings[SettingName.AlertTime].Split(':');
            int hours = 0, minutes = 0, seconds = 0;
            if (!(Int32.TryParse(splits[0], out hours) && Int32.TryParse(splits[1], out minutes) && Int32.TryParse(splits[2], out seconds)))
                Console.Error.WriteLine("Could not read Alert Time from settings");
            return new TimeSpan(hours, minutes, seconds);
        }

        private static DateTime GetAlertDateTime() {
            string[] splits = settings[SettingName.AlertTime].Split(':');
            int hours = 0, minutes = 0, seconds = 0;
            if (!(Int32.TryParse(splits[0], out hours) && Int32.TryParse(splits[1], out minutes) && Int32.TryParse(splits[2], out seconds)))
                Console.Error.WriteLine("Could not read Alert Time from settings");
            DateTime alertTime = DateTime.Now;
            alertTime = alertTime.Subtract(new TimeSpan(alertTime.Hour, alertTime.Minute, alertTime.Second));
            return alertTime.Add(new TimeSpan(hours, minutes, seconds));
        }

        static void CallMarkerExportScript(params string[] args) {
            //System.Diagnostics.Process.Start("");

            Process scriptProc = new Process();
            scriptProc.StartInfo.FileName = @"script.vbs";
            scriptProc.StartInfo.WorkingDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName; //<---very important 
            scriptProc.StartInfo.Arguments = string.Join(" ", args);
            scriptProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //prevent console window from popping up
            scriptProc.Start();
            scriptProc.WaitForExit(); // <-- Optional if you want program running until your script exit
            scriptProc.Close();
        }

        internal static void ExportMarker() {
            foreach (Marker marker in markers) {
                CallMarkerExportScript(marker.timestamp.ToString(timecodeExportFormat), marker.type.ToString());
            }      
        }

        internal static void SaveMarkers() {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt Files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                System.IO.File.WriteAllLines(saveFileDialog.FileName, markers.Select(i => i.ToString()).ToArray());
            }
        }

        internal static void LoadMarkers() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt Files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string[] markersRaw = System.IO.File.ReadAllLines(openFileDialog.FileName);
                markers.Clear();
                form.resetForm();
                TimeSpan tempEpisodeStart = new TimeSpan();
                TimeSpan tempEpisodeEnd;
                for (int i = 0; i < markersRaw.Length; i++) {
                    string[] markerDataRaw = markersRaw[i].Split(' ');
                    string[] markerTimeRaw = markerDataRaw[0].Split(':');
                    Marker marker = new Marker();
                    int hours = 0, minutes = 0, seconds = 0;
                    MarkerType type;                    
                    if(!(Int32.TryParse(markerTimeRaw[0], out hours) && Int32.TryParse(markerTimeRaw[1], out minutes) && Int32.TryParse(markerTimeRaw[2], out seconds) && Enum.TryParse(markerDataRaw[1], out type))) {
                        Console.Error.WriteLine("Faild to load marker from file");
                        return;
                    }
                    marker.timestamp = new TimeSpan(hours, minutes, seconds);
                    marker.type = type;
                    markers.Add(marker);

                    form.AddMarkerInfo(marker.timestamp, marker.type.ToString());
                    if (type.Equals(MarkerType.EpStart))
                        tempEpisodeStart = marker.timestamp;
                    else if (type.Equals(MarkerType.EpEnd)) {
                        tempEpisodeEnd = marker.timestamp;
                        if(tempEpisodeStart != null) form.AddEpisodeTime(tempEpisodeStart, tempEpisodeEnd);
                    }
                }
            }
        }
        
        class Marker {
            public TimeSpan timestamp { get; set; }
            public MarkerType type { get; set; }
            public override string ToString() {
                return timestamp.ToString(timecodeExportFormat) + " " + type;
            }
        }
    }
}
