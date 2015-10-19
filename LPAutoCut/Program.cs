using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;

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
        
        public enum MarkerType { EpStart, EpEnd, Edit, Cut, Mark };

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main() {
            DateTime defaultTime = DateTime.Now;
            AlertTime = new TimeSpan(0, 0, 5);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            Application.Run(form);
        }

        public static bool AlertActive { get; set; }
        public static TimeSpan AlertTime { get; set; }

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
            if (isEpisode && AlertActive && TimeSpan.Compare(DateTime.Now.Subtract(currentEpisodeStart), AlertTime) > 0)
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
        
        class Marker {
            public TimeSpan timestamp { get; set; }
            public MarkerType type { get; set; }
            public override string ToString() {
                return timestamp.ToString(timecodeExportFormat) + ";" + type;
            }
        }
    }
}
