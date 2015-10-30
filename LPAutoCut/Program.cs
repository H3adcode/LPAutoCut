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
        static System.Timers.Timer clockUpdateTimer;
        static List<Marker> markers = new List<Marker>();
        static DateTime start, currentEpisodeStart, currentEpisodeEnd;
        static MainForm mainForm;
        static bool isEpisode = false;
        static bool isStarted = false;
        static string tmpJSXFile = Path.GetTempPath() + "\\" + Properties.Settings.Default.JSXTempFileName;
        static string tmpMKRFile = Path.GetTempPath() + "\\" + Properties.Settings.Default.MKRTempFileName;
        static string executionPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        
        public enum MarkerType { EpStart, EpEnd, Edit, Cut, Mark };

        [STAThread]
        static void Main() {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit); 

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm();

            loadSettings();

            Application.Run(mainForm);
        }

        // clear temp files on close
        static void OnProcessExit (object sender, EventArgs e) {
            if (File.Exists(tmpJSXFile))
                File.Delete(tmpJSXFile);
            if (File.Exists(tmpMKRFile))
                File.Delete(tmpMKRFile);
        }


        internal static void StartStopTimer() {
            if (isStarted)
                StopTimer();
            else
                StartTimer();
        }

        internal static void StartTimer() {
            // clear form
            markers.Clear();
            // init timer
            start = DateTime.Now;
            clockUpdateTimer = new System.Timers.Timer(100);
            clockUpdateTimer.Elapsed += new ElapsedEventHandler(OnUpdateTimerElapsed);
            clockUpdateTimer.Enabled = true;
            isStarted = true;
            // update form
            mainForm.OnStart();
        }

        internal static void StopTimer() {
            // update form
            mainForm.OnStop();
            // stop timer
            clockUpdateTimer.Enabled = false;
            if (isEpisode) // if episode running stop episode
                StopEpisode();
            isStarted = false;
        }

        internal static void StartStopEpisode() {
            if (!isStarted) return; // if not started break
            if (isEpisode)
                StopEpisode();
            else
                StartEpisode();
        }

        internal static void StartEpisode() {
            if (!isStarted) return; // if not started break
            currentEpisodeStart = DateTime.Now;
            SetMarker(MarkerType.EpStart);
            isEpisode = true;
            // update form
            mainForm.OnEpisodeStart();
        }

        internal static void StopEpisode() {
            if (!isStarted) return; // if not started break
            currentEpisodeEnd = DateTime.Now;
            SetMarker(MarkerType.EpEnd);
            isEpisode = false;
            // update form
            mainForm.AddEpisodeTime(currentEpisodeStart.Subtract(start), currentEpisodeEnd.Subtract(start));
            mainForm.OnEpisodeStop();
        }

        internal static void SetMarker(MarkerType type) {
            Marker marker = new Marker();
            marker.timestamp = DateTime.Now.Subtract(start);
            marker.type = type;
            markers.Add(marker);
            mainForm.AddMarkerInfo(marker.timestamp, type.ToString());
        }

        // updates time displays and alerts
        static void OnUpdateTimerElapsed(object sender, ElapsedEventArgs e) {
            DateTime updateTime = DateTime.Now;
            UpdateTotalTime(updateTime); // total time
            UpdateEpisodeTime(updateTime); // episode time
            // if alert is set and alert time elapased update form
            if (isEpisode && Properties.Settings.Default.AlertChecked 
                && TimeSpan.Compare(updateTime.Subtract(currentEpisodeStart), Properties.Settings.Default.AlertTime) > 0)
                mainForm.OnTimeAlertOn();
            else
                mainForm.OnTimeAlertOff();
        }

        static void UpdateTotalTime(DateTime updateTime) {
            mainForm.SetTotalTime(updateTime.Subtract(start));
        }

        static void UpdateEpisodeTime(DateTime updateTime) {
            if(isEpisode)
                mainForm.SetEpTime(updateTime.Subtract(currentEpisodeStart));
            else
                mainForm.ResetEpTime();
        }

        // resets settings to application defaults
        internal static void resetSettings() {
            Properties.Settings.Default.Reset();
            loadSettings();
        }

        internal static void loadSettings() {
            mainForm.SetAlertChecked(Properties.Settings.Default.AlertChecked);
            mainForm.SetAlertTime(Properties.Settings.Default.AlertTime);
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

        // exports marker to active sequence in Adobe Premiere Pro CC
        internal static void ExportMarker() {
            if (!File.Exists(tmpJSXFile)) { // if jsx script not in temp folder create it
                using (Stream resFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Properties.Settings.Default.JSXResFileName)) {
                    using (Stream tmpFileStream = File.Create(tmpJSXFile)) {
                        resFileStream.CopyTo(tmpFileStream);
                    }
                }
            }
            // copy current markers to temp folder
            System.IO.File.WriteAllLines(tmpMKRFile, markers.Select(i => i.ToStringSeconds()).ToArray());
            // call jsx script which will read marker info on itself
            Process scriptProc = new Process();
            scriptProc.StartInfo.FileName = Path.GetFileName(tmpJSXFile);
            scriptProc.StartInfo.WorkingDirectory = Path.GetDirectoryName(tmpJSXFile); 
            //scriptProc.StartInfo.Arguments = string.Join(" ", args);
            scriptProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            scriptProc.Start();
            scriptProc.WaitForExit();
            scriptProc.Close();     
        }

        // saves marker to user specified location
        internal static void SaveMarkers() {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Marker Files (*.mkr)|*.mkr|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                System.IO.File.WriteAllLines(saveFileDialog.FileName, markers.Select(i => i.ToString()).ToArray());
            }
        }

        // loads marker from user specified location
        internal static void LoadMarkers() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Marker Files (*.mkr)|*.mkr|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string[] markersRaw = System.IO.File.ReadAllLines(openFileDialog.FileName);
                markers.Clear(); // clear marker list
                mainForm.resetForm(); // clear form
                TimeSpan tempEpisodeStart = new TimeSpan();
                TimeSpan tempEpisodeEnd;
                for (int i = 0; i < markersRaw.Length; i++) { // create a marker form each raw file line
                    string[] markerDataRaw = markersRaw[i].Split(' '); // split time and info
                    string[] markerTimeRaw = markerDataRaw[0].Split(new char[]{':','.'}); // split time codes
                    int hours = 0, minutes = 0, seconds = 0, milliseconds = 0;
                    MarkerType type;                    
                    if(!(Int32.TryParse(markerTimeRaw[0], out hours) // try to convert time codes
                        && Int32.TryParse(markerTimeRaw[1], out minutes) 
                        && Int32.TryParse(markerTimeRaw[2], out seconds)
                        && Int32.TryParse(markerTimeRaw[3], out milliseconds)
                        && Enum.TryParse(markerDataRaw[1], out type))) { // try to convert marker type
                        Console.Error.WriteLine("Faild to load marker from file");
                        return; // break if conversion failed
                    }
                    Marker marker = new Marker();
                    marker.timestamp = new TimeSpan(0, hours, minutes, seconds, milliseconds * (int)Math.Pow(10.0, (3.0 - markerTimeRaw[3].Length))); // Math.Pow determines the accuracy of given milliseconds
                    marker.type = type;
                    markers.Add(marker);
                    // update markers in form
                    mainForm.AddMarkerInfo(marker.timestamp, marker.type.ToString());
                    // update episode times in form
                    if (type.Equals(MarkerType.EpStart))
                        tempEpisodeStart = marker.timestamp;
                    else if (type.Equals(MarkerType.EpEnd)) {
                        tempEpisodeEnd = marker.timestamp;
                        if(tempEpisodeStart != null) mainForm.AddEpisodeTime(tempEpisodeStart, tempEpisodeEnd);
                    }
                }
            }
        }
        
        class Marker {
            public TimeSpan timestamp { get; set; }
            public MarkerType type { get; set; }
            public override string ToString() {
                return timestamp.ToString(Properties.Settings.Default.TimeCodeExportFormat) + " " + type;
            }
            public string ToStringSeconds()  {
                return timestamp.TotalSeconds.ToString() + " " + type;
            }
        }
    }
}
