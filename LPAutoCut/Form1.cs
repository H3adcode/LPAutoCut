using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPAutoCut {
    public partial class Form1 : Form {

        private static string TIMEFORMAT = "hh\\:mm\\:ss";
        private static string EPPAUSED = "paused";

        public Form1() {
            InitializeComponent();
        }

        public void setEpTime(TimeSpan eptime) {
            setEpTimeSafe(eptime.ToString(TIMEFORMAT));
        }

        public void resetEpTime() {
            setEpTimeSafe(EPPAUSED);
        }

        private void setEpTimeSafe(string text) {
            if (tb_episodetime.InvokeRequired)
                tb_episodetime.Invoke((MethodInvoker)delegate() {
                    tb_episodetime.Text = text;
                });
            else
                tb_episodetime.Text = text;
        }

        public void addEpisodeTime(TimeSpan start, TimeSpan stop) {
            lv_eptimes.Items.Add(new ListViewItem(new string[] { start.ToString(TIMEFORMAT), stop.ToString(TIMEFORMAT), start.Subtract(stop).ToString(TIMEFORMAT) }));
        }

        public void setTotalTime(TimeSpan totaltime) {
            if (tb_totaltime.InvokeRequired)
                tb_totaltime.Invoke((MethodInvoker)delegate() {
                    tb_totaltime.Text = totaltime.ToString(TIMEFORMAT);
                });
            else
                tb_totaltime.Text = totaltime.ToString(TIMEFORMAT);
        }

        public void onStart() {
            bt_start.Enabled = false;
            bt_epstart.Enabled = true;
            bt_epend.Enabled = true;
        }

        public void onStop() {
            bt_start.Enabled = true;
            bt_epstart.Enabled = false;
            bt_epend.Enabled = false;
        }

        private void bt_start_Click(object sender, EventArgs e) {
            Program.startTimer();
        }

        private void bt_stop_Click(object sender, EventArgs e) {
            Program.stopTimer();
        }

        private void bt_setstart_Click(object sender, EventArgs e) {
            Program.startEpisode();
        }

        private void bt_setend_Click(object sender, EventArgs e) {
            Program.stopEpisode();
        }
    }
}
