using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace LPAutoCut {
    static class Program {
        static System.Timers.Timer updateTimer;
        static List<Marker> markers = new List<Marker>();
        static DateTime start, stop;
        static DateTime currentEpisodeStart;
        static DateTime currentEpisodeEnd;
        static MainForm form;
        static bool isEpisode = false;
        static bool isStarted = false;
        static string timecodeExportFormat = "hh\\:mm\\:ss";
        static string tmpJSXFile = Path.GetTempPath() + "\\LPAutoCut.temp.jsx";
        static string tmpMKRFile = Path.GetTempPath() + "\\LPAutoCut.temp.txt";

        static string executionPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        
        public enum MarkerType { EpStart, EpEnd, Edit, Cut, Mark };

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main() {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit); 

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new MainForm();

            loadSettings();

            Application.Run(form);
        }

        static void OnProcessExit (object sender, EventArgs e) {
            if (File.Exists(tmpJSXFile))
                File.Delete(tmpJSXFile);
            if (File.Exists(tmpMKRFile))
                File.Delete(tmpMKRFile);
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
            if (isEpisode && Properties.Settings.Default.AlertChecked && TimeSpan.Compare(updateTime.Subtract(currentEpisodeStart), Properties.Settings.Default.AlertTime) > 0)
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

        internal static void resetSettings() {
            Properties.Settings.Default.Reset();
            loadSettings();
        }

        internal static void loadSettings() {
            form.SetAlertChecked(Properties.Settings.Default.AlertChecked);
            form.SetAlertTime(Properties.Settings.Default.AlertTime);
        }

        internal static void SetAlert(bool alert) {
            Properties.Settings.Default.AlertChecked = alert;
        }

        internal static void SetAlertTime(TimeSpan alertTime) {
            Properties.Settings.Default.AlertTime = alertTime;
        }

        internal static void saveSettings() {
            Properties.Settings.Default.Save();
        }

        internal static void ExportMarker() {
            if (!File.Exists(tmpJSXFile)) {
                using (Stream resFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LPAutoCut.SetMarker.jsx")) {
                    using (Stream tmpFileStream = File.Create(tmpJSXFile)) {
                        resFileStream.CopyTo(tmpFileStream);
                    }
                }
            }
            System.IO.File.WriteAllLines(tmpMKRFile, markers.Select(i => i.ToStringSeconds()).ToArray());
            Process scriptProc = new Process();
            scriptProc.StartInfo.FileName = Path.GetFileName(tmpJSXFile);
            scriptProc.StartInfo.WorkingDirectory = Path.GetDirectoryName(tmpJSXFile); //<---very important 
            //scriptProc.StartInfo.Arguments = string.Join(" ", args);
            scriptProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //prevent console window from popping up
            scriptProc.Start();
            scriptProc.WaitForExit(); // <-- Optional if you want program running until your script exit
            scriptProc.Close();     
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
            public string ToStringSeconds()  {
                return timestamp.TotalSeconds.ToString() + " " + type;
            }
        }
    }
}
