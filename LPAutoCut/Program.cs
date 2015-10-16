using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPAutoCut {
    static class Program {
        static System.Timers.Timer _timer;
        static List<EpisodeTimeCode> Timecodes = new List<EpisodeTimeCode>();
        static DateTime Start, Stop;
        static EpisodeTimeCode cur_Timecode;
        static Form1 form;
        static bool isEpisode = false;
        static bool isStarted = false;

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

        public static void startTimer() {
            Start = DateTime.Now;
            _timer = new System.Timers.Timer(500);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true;
            updateTotalTime();
            isStarted = true;
            form.onStart();
        }

        public static void stopTimer() {
            isStarted = false;
            form.onStop();
            Stop = DateTime.Now;
            _timer.Enabled = false;
            if (isEpisode)
                stopEpisode();
            updateTotalTime();
            updateEpisodeTime();   
        }

        private static void _timer_Elapsed(object sender, ElapsedEventArgs e) {
            updateTotalTime();
            updateEpisodeTime();
        }

        public static void startEpisode() {
            if (!isStarted) return;
            cur_Timecode = new EpisodeTimeCode();
            cur_Timecode.Start = DateTime.Now;
            isEpisode = true;
        }

        public static void stopEpisode() {
            if (!isStarted) return;
            cur_Timecode.End = DateTime.Now;
            Timecodes.Add(cur_Timecode);
            form.addEpisodeTime(cur_Timecode.Start.Subtract(Start), cur_Timecode.End.Subtract(Start));
            isEpisode = false;
        }

        private static void updateTotalTime() {
            form.setTotalTime(DateTime.Now.Subtract(Start));
        }

        private static void updateEpisodeTime() {
            if(isEpisode)
                form.setEpTime(DateTime.Now.Subtract(cur_Timecode.Start));
            else
                form.resetEpTime();
        }

        private class EpisodeTimeCode {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string[] getValueAsStringArray() {
                return new string[] { Start.ToString("hh\\:mm\\:ss"), End.ToString("hh\\:mm\\:ss") };
            }
        }
    }
}
