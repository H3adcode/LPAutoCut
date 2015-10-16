﻿using System;
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

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static int WM_HOTKEY = 0x0312;

        enum KeyModifier {
            NOMOD = 0x0000,
            ALT = 0x0001,
            CTRL = 0x0002,
            SHIFT = 0x0004,
            WIN = 0x0008
        }

        public Form1() {
            InitializeComponent();

            RegisterHotKey(this.Handle, 0, (int)KeyModifier.NOMOD, (int)Keys.F9);
            RegisterHotKey(this.Handle, 1, (int)KeyModifier.NOMOD, (int)Keys.F10);
            RegisterHotKey(this.Handle, 2, (int)KeyModifier.NOMOD, (int)Keys.F11);
            RegisterHotKey(this.Handle, 3, (int)KeyModifier.NOMOD, (int)Keys.F12);
        }

        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY) {
                //Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                //KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                          // The id of the hotkey that was pressed.

                if (id == 0)
                    Program.startTimer();
                else if (id == 1)
                    Program.stopTimer();
                else if (id == 2)
                    Program.startEpisode();
                else if (id == 3)
                    Program.stopEpisode();
            }
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
