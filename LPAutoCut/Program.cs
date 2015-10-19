using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;

namespace LPAutoCut {
    static class Program {
        static System.Timers.Timer timer;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            Application.Run(form);
        }

        public static void StartTimer() {
            start = DateTime.Now;
            timer = new System.Timers.Timer(500);
            timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
            timer.Enabled = true;
            UpdateTotalTime();
            isStarted = true;
            form.onStart();
        }

        public static void StopTimer() {
            isStarted = false;
            form.onStop();
            stop = DateTime.Now;
            timer.Enabled = false;
            if (isEpisode)
                StopEpisode();
            UpdateTotalTime();
            UpdateEpisodeTime();

            SaveTimecodes();
        }

        public static void StartEpisode() {
            if (!isStarted) return;
            SetMarker(MarkerType.EpStart);
            currentEpisodeStart = DateTime.Now;
            isEpisode = true;
        }

        public static void StopEpisode() {
            if (!isStarted) return;
            SetMarker(MarkerType.EpEnd);
            currentEpisodeEnd = DateTime.Now;
            form.addEpisodeTime(currentEpisodeStart.Subtract(start), currentEpisodeEnd.Subtract(start));
            isEpisode = false;
        }

        public static void SetMarker(MarkerType type) {
            Marker marker = new Marker();
            marker.timestamp = DateTime.Now.Subtract(start);
            marker.type = type;
            markers.Add(marker);
            form.addMarkerInfo(marker.timestamp, type.ToString());
        }

        static void OnTimerElapsed(object sender, ElapsedEventArgs e) {
            UpdateTotalTime();
            UpdateEpisodeTime();
        }

        static void UpdateTotalTime() {
            form.setTotalTime(DateTime.Now.Subtract(start));
        }

        static void UpdateEpisodeTime() {
            if(isEpisode)
                form.setEpTime(DateTime.Now.Subtract(currentEpisodeStart));
            else
                form.resetEpTime();
        }

        static void SaveTimecodes() {
            System.IO.File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Marker.txt", markers.Select(i => i.ToString()).ToArray());
        }
        
        class Marker {
            public TimeSpan timestamp { get; set; }
            public MarkerType type { get; set; }
            public string[] getMarkerAsStringArray(string format) {
                return new string[] { timestamp.ToString(format), type.ToString() };
            }
            public string ToString() {
                return timestamp.ToString(timecodeExportFormat) + ";" + type;
            }
        }
    }
}
