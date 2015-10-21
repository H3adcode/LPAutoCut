using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;

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
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit); 

            settingsDefault = new Dictionary<SettingName, string>();
            settingsDefault.Add(SettingName.Alert, "TRUE");
            settingsDefault.Add(SettingName.AlertTime, "00:00:05");

            loadSettings();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            Application.Run(form);
        }

        static void OnProcessExit(object sender, EventArgs e) {
            saveSettings();
        }

        public static void StartTimer() {
            markers.Clear();
            start = DateTime.Now;
            updateTimer = new System.Timers.Timer(500);
            updateTimer.Elapsed += new ElapsedEventHandler(OnUpdateTimerElapsed);
            updateTimer.Enabled = true;

            isStarted = true;

            form.OnStart();
        }

        public static void StopTimer() {
            form.OnStop();
            stop = DateTime.Now;
            updateTimer.Enabled = false;
            if (isEpisode)
                StopEpisode();
            isStarted = false;

            SaveTimecodes();
        }

        public static void StartEpisode() {
            if (!isStarted) return;
            SetMarker(MarkerType.EpStart);
            currentEpisodeStart = DateTime.Now;
            isEpisode = true;

            form.OnEpisodeStart();
        }

        public static void StopEpisode() {
            if (!isStarted) return;
            SetMarker(MarkerType.EpEnd);
            currentEpisodeEnd = DateTime.Now;
            form.AddEpisodeTime(currentEpisodeStart.Subtract(start), currentEpisodeEnd.Subtract(start));
            isEpisode = false;
            form.OnEpisodeStop();
        }

        public static void SetMarker(MarkerType type) {
            Marker marker = new Marker();
            marker.timestamp = DateTime.Now.Subtract(start);
            marker.type = type;
            markers.Add(marker);
            form.AddMarkerInfo(marker.timestamp, type.ToString());
        }

        static void OnUpdateTimerElapsed(object sender, ElapsedEventArgs e) {
            UpdateTotalTime();
            UpdateEpisodeTime();
            if (isEpisode && IsAlertActive() && TimeSpan.Compare(DateTime.Now.Subtract(currentEpisodeStart), GetAlertTime()) > 0)
                form.OnAlertTimeElapsed();
        }

        static void UpdateTotalTime() {
            form.SetTotalTime(DateTime.Now.Subtract(start));
        }

        static void UpdateEpisodeTime() {
            if(isEpisode)
                form.SetEpTime(DateTime.Now.Subtract(currentEpisodeStart));
            else
                form.ResetEpTime();
        }

        static void SaveTimecodes() {
            System.IO.File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Marker.txt", markers.Select(i => i.ToString()).ToArray());
        }

        static void loadSettings() {
            try {
                string[] settingsRaw = System.IO.File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Settings.txt");
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
        }

        public static void SetAlert(bool alert) {
            settings[SettingName.Alert] = alert.ToString();
        }

        public static void SetAlertTime(DateTime alertTime) {
            settings[SettingName.AlertTime] = alertTime.ToString("hh:mm:ss");
        }

        static void saveSettings() {
            System.IO.File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Settings.txt", settings.Select(i => i.Key.ToString() + "=" + i.Value).ToArray());
        }

        public static bool IsAlertActive() {
            return settings[SettingName.Alert].CompareTo("TRUE") == 0;
        }

        public static TimeSpan GetAlertTime() {
            string[] splits = settings[SettingName.AlertTime].Split(':');
            int hours = 0, minutes = 0, seconds = 0;
            if (!(Int32.TryParse(splits[0], out hours) && Int32.TryParse(splits[1], out minutes) && Int32.TryParse(splits[2], out seconds)))
                Console.Error.WriteLine("Could not read Alert Time from settings");
            return new TimeSpan(hours, minutes, seconds);
        }

        static void CallMarkerExportScript(params string[] args) {
            //System.Diagnostics.Process.Start("");

            Process scriptProc = new Process();
            scriptProc.StartInfo.FileName = @"script.vbs";
            scriptProc.StartInfo.WorkingDirectory = @"c:\"; //<---very important 
            scriptProc.StartInfo.Arguments = string.Join(" ", args);
            scriptProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //prevent console window from popping up
            scriptProc.Start();
            scriptProc.WaitForExit(); // <-- Optional if you want program running until your script exit
            scriptProc.Close();
        }
        
        class Marker {
            public TimeSpan timestamp { get; set; }
            public MarkerType type { get; set; }
            public override string ToString() {
                return timestamp.ToString(timecodeExportFormat) + ";" + type;
            }
        }

        public static void ExportMarker() {
            foreach (Marker marker in markers) {
                CallMarkerExportScript(marker.timestamp.ToString(timecodeExportFormat), marker.type.ToString());
            }
        }
    }
}
