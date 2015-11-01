﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace LPAutoCut {
    public partial class MainForm : Form {

        #region test

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;

        private LowLevelKeyboardProc _proc = hookProc;

        private static IntPtr hhook = IntPtr.Zero;

        public void SetHook() {
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, LoadLibrary("User32"), 0);
        }

        public static void UnHook() {
            UnhookWindowsHookEx(hhook);
        }

        public static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam) {
            if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN) {
                switch (Marshal.ReadInt32(lParam)) {
                    case (int)Keys.F9: Program.StartTimer(); break;
                    case (int)Keys.F10: Program.StopTimer(); break;
                    case (int)Keys.F7: Program.StartEpisode(); break;
                    case (int)Keys.F8: Program.StopEpisode(); break;
                    case (int)Keys.F2: Program.SetMarker(Program.MarkerType.Edit); break;
                    case (int)Keys.F3: Program.SetMarker(Program.MarkerType.Cut); break;
                    case (int)Keys.F4: Program.SetMarker(Program.MarkerType.Mark); break;
                }
                return (IntPtr)0;
            } else
                return CallNextHookEx(hhook, code, (int)wParam, lParam);
        }

        #endregion

        // system hotkey references
        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        string EPPAUSED = "paused";
        
        List<Button> buttonGroupEnabledOnStart = new List<Button>(); // enabled if timer started, else disabled
        List<Button> buttonGroupDisabledOnStart = new List<Button>(); // disabled if timer started, else enabled
        List<Button> buttonGroupEnabledInEpisode = new List<Button>(); // enabled if episode is started, else disabled
        List<Button> buttonGroupDisabledInEpisode = new List<Button>(); // disabled if episode is started, else enabled
        bool flashEffectActive = false;

        // system hotkey key codes
        int WM_HOTKEY = 0x0312;
        enum KeyModifier {
            NOMOD = 0x0000,
            ALT = 0x0001,
            CTRL = 0x0002,
            SHIFT = 0x0004,
            WIN = 0x0008
        }
        
        System.Timers.Timer flashEffectTimer; // timer for alert flash effect

        internal MainForm() {
            InitializeComponent();
                        
            dtp_alert.Format = DateTimePickerFormat.Time;
            dtp_alert.ShowUpDown = true;

            buttonGroupEnabledOnStart.Add(bt_epstart);
            buttonGroupEnabledOnStart.Add(bt_edit);
            buttonGroupEnabledOnStart.Add(bt_cut);
            buttonGroupEnabledOnStart.Add(bt_mark);
            buttonGroupEnabledOnStart.Add(bt_stop);

            buttonGroupDisabledOnStart.Add(bt_start);
            //buttonGroupDisabledOnStart.Add(bt_save);
            //buttonGroupDisabledOnStart.Add(bt_load);
            //buttonGroupDisabledOnStart.Add(bt_export);

            buttonGroupEnabledInEpisode.Add(bt_epend);

            buttonGroupDisabledInEpisode.Add(bt_epstart);
            
            flashEffectTimer = new System.Timers.Timer(500);
            flashEffectTimer.Elapsed += new ElapsedEventHandler(OnFlashEffectTimerElapsed);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit); 

            SetHook();

            //RegisterHotKey(this.Handle, 0, (int)KeyModifier.NOMOD, (int)Keys.F9); // Start Timer
            //RegisterHotKey(this.Handle, 1, (int)KeyModifier.NOMOD, (int)Keys.F10); // Stop Timer
            //RegisterHotKey(this.Handle, 2, (int)KeyModifier.NOMOD, (int)Keys.F7); // Start Episode
            //RegisterHotKey(this.Handle, 3, (int)KeyModifier.NOMOD, (int)Keys.F8); // Stop Episode
            //RegisterHotKey(this.Handle, 4, (int)KeyModifier.NOMOD, (int)Keys.F2); // Cut Marker
            //RegisterHotKey(this.Handle, 5, (int)KeyModifier.NOMOD, (int)Keys.F3); // Edit Marker
            //RegisterHotKey(this.Handle, 6, (int)KeyModifier.NOMOD, (int)Keys.F4); // Mark Marker
        }

        static void OnProcessExit(object sender, EventArgs e) {
            UnHook();
        }
        
        //protected override void WndProc(ref Message m) {
        //    base.WndProc(ref m);

        //    if (m.Msg == WM_HOTKEY) {
        //        //Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
        //        //KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
        //        int id = m.WParam.ToInt32();                                          // The id of the hotkey that was pressed.

        //        switch (id) {
        //            case 0: Program.StartTimer(); break;
        //            case 1: Program.StopTimer(); break;
        //            case 2: Program.StartEpisode(); break;
        //            case 3: Program.StopEpisode(); break;
        //            case 4: Program.SetMarker(Program.MarkerType.Edit); break;
        //            case 5: Program.SetMarker(Program.MarkerType.Cut); break;
        //            case 6: Program.SetMarker(Program.MarkerType.Mark); break;
        //        }
        //    }
        //}

        internal void SetEpTime(TimeSpan eptime) {
            SetEpTimeSafe(eptime.ToString(Properties.Settings.Default.TimeCodeDisplayFormat));
        }

        internal void ResetEpTime() {
            SetEpTimeSafe(EPPAUSED);
        }

        void SetEpTimeSafe(string text) {
            if (tb_episodetime.InvokeRequired)
                tb_episodetime.Invoke((MethodInvoker)delegate() {
                    tb_episodetime.Text = text;
                });
            else
                tb_episodetime.Text = text;
        }

        internal void AddEpisodeTime(TimeSpan start, TimeSpan stop) {
            lv_eptimes.Items.Add(new ListViewItem(new string[] { start.ToString(Properties.Settings.Default.TimeCodeDisplayFormat), stop.ToString(Properties.Settings.Default.TimeCodeDisplayFormat), start.Subtract(stop).ToString(Properties.Settings.Default.TimeCodeDisplayFormat) }));
        }

        internal void AddMarkerInfo(TimeSpan timestamp, string info) {
            lv_marker.Items.Add(new ListViewItem(new String[] { timestamp.ToString(Properties.Settings.Default.TimeCodeDisplayFormat), info }));
        }

        internal void SetTotalTime(TimeSpan totaltime) {
            if (tb_totaltime.InvokeRequired)
                tb_totaltime.Invoke((MethodInvoker)delegate() {
                    tb_totaltime.Text = totaltime.ToString(Properties.Settings.Default.TimeCodeDisplayFormat);
                });
            else
                tb_totaltime.Text = totaltime.ToString(Properties.Settings.Default.TimeCodeDisplayFormat);
        }

        internal void SetAlertChecked(bool alertChecked) {
            cb_alert.Checked = alertChecked;
            dtp_alert.Enabled = alertChecked;
        }

        internal void SetAlertTime(TimeSpan alertTime) {
            dtp_alert.Value = DateTime.Today.Add(alertTime);
        }

        internal void OnStart() {
            lv_eptimes.Items.Clear();
            lv_marker.Items.Clear();
            foreach (Button button in buttonGroupEnabledOnStart)
                button.Enabled = true;
            foreach (Button button in buttonGroupDisabledOnStart)
                button.Enabled = false;
        }

        internal void OnStop() {
            foreach (Button button in buttonGroupEnabledOnStart)
                button.Enabled = false;
            foreach (Button button in buttonGroupDisabledOnStart)
                button.Enabled = true;
        }

        internal void OnEpisodeStart() {
            foreach (Button button in buttonGroupEnabledInEpisode)
                button.Enabled = true;
            foreach (Button button in buttonGroupDisabledInEpisode)
                button.Enabled = false;
        }

        internal void OnEpisodeStop() {
            flashEffectTimer.Enabled = false;
            if (tb_episodetime.InvokeRequired) {
                tb_episodetime.Invoke((MethodInvoker)delegate() {
                    tb_episodetime.BackColor = Color.White;
                });
            } else
                tb_episodetime.BackColor = Color.White;


            foreach (Button button in buttonGroupEnabledInEpisode)
                button.Enabled = false;
            foreach (Button button in buttonGroupDisabledInEpisode)
                button.Enabled = true;
        }

        internal void OnTimeAlertOn() {
            flashEffectTimer.Enabled = true;
        }

        internal void OnTimeAlertOff() {
            flashEffectTimer.Enabled = false;
            if (tb_episodetime.InvokeRequired) {
                tb_episodetime.Invoke((MethodInvoker)delegate() {
                    tb_episodetime.BackColor = Color.White;
                });
            } else
                tb_episodetime.BackColor = Color.White;
        }

        void OnFlashEffectTimerElapsed(object sender, ElapsedEventArgs e) {
            if (flashEffectActive) {
                if (tb_episodetime.InvokeRequired) {
                    tb_episodetime.Invoke((MethodInvoker)delegate() {
                        tb_episodetime.BackColor = Color.White;
                    });
                } else
                    tb_episodetime.BackColor = Color.White;
            } else {
                if (tb_episodetime.InvokeRequired) {
                    tb_episodetime.Invoke((MethodInvoker)delegate() {
                        tb_episodetime.BackColor = Color.Yellow;
                    });
                } else
                    tb_episodetime.BackColor = Color.Yellow;
            }
            flashEffectActive = !flashEffectActive;
        }

        internal void resetForm() {
            tb_totaltime.Clear();
            tb_episodetime.Clear();
            lv_eptimes.Items.Clear();
            lv_marker.Items.Clear();
        }

        private void bt_start_Click(object sender, EventArgs e) {
            Program.StartTimer();
        }

        private void bt_stop_Click(object sender, EventArgs e) {
            Program.StopTimer();
        }

        private void bt_setstart_Click(object sender, EventArgs e) {
            Program.StartEpisode();
        }

        private void bt_setend_Click(object sender, EventArgs e) {
            Program.StopEpisode();
        }

        private void btn_edit_Click(object sender, EventArgs e) {
            Program.SetMarker(Program.MarkerType.Edit);
        }

        private void btn_cut_Click(object sender, EventArgs e) {
            Program.SetMarker(Program.MarkerType.Cut);
        }

        private void btn_mark_Click(object sender, EventArgs e) {
            Program.SetMarker(Program.MarkerType.Mark);
        }

        private void cb_alert_CheckedChanged(object sender, EventArgs e) {
            Program.SetAlert(cb_alert.Checked);
            dtp_alert.Enabled = cb_alert.Checked;
        }

        private void dtp_alert_ValueChanged(object sender, EventArgs e) {
            Program.SetAlertTime(dtp_alert.Value.Subtract(DateTime.Today));
        }

        private void bt_save_Click(object sender, EventArgs e) {
            Program.SaveMarkers();
        }

        private void bt_load_Click(object sender, EventArgs e) {
            Program.LoadMarkers();
        }

        private void bt_setDefault_Click(object sender, EventArgs e) {
            Program.saveSettings();
        }

        private void bt_restoreDefault_Click(object sender, EventArgs e) {
            Program.resetSettings();
        }

        private void bt_export_Click(object sender, EventArgs e) {
            Program.ExportMarker();
        }
    }
}
