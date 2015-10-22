using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace LPAutoCut {
    public partial class Form1 : Form {

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        static string TIMEFORMAT = "hh\\:mm\\:ss";
        static string EPPAUSED = "paused";

        static List<Button> buttonGroupEnabledOnStart = new List<Button>();
        static List<Button> buttonGroupDisabledOnStart = new List<Button>();
        static bool flashEffectActive = false;

        static int WM_HOTKEY = 0x0312;
        enum KeyModifier {
            NOMOD = 0x0000,
            ALT = 0x0001,
            CTRL = 0x0002,
            SHIFT = 0x0004,
            WIN = 0x0008
        }
        
        System.Timers.Timer flashEffectTimer;

        public Form1() {
            InitializeComponent();
            
            cb_alert.Checked = Program.IsAlertActive();
            
            dtp_alert.Format = DateTimePickerFormat.Time;
            dtp_alert.ShowUpDown = true;
            DateTime defaultTime = DateTime.Now;
            dtp_alert.Value = defaultTime.Subtract(new TimeSpan(defaultTime.Hour - Program.GetAlertTime().Days, defaultTime.Minute - Program.GetAlertTime().Minutes, defaultTime.Second - Program.GetAlertTime().Seconds));

            buttonGroupEnabledOnStart.Add(bt_epstart);
            buttonGroupEnabledOnStart.Add(bt_epend);
            buttonGroupEnabledOnStart.Add(bt_edit);
            buttonGroupEnabledOnStart.Add(bt_cut);
            buttonGroupEnabledOnStart.Add(bt_mark);
            buttonGroupEnabledOnStart.Add(bt_stop);

            buttonGroupDisabledOnStart.Add(bt_start);
            
            flashEffectTimer = new System.Timers.Timer(250);
            flashEffectTimer.Elapsed += new ElapsedEventHandler(OnFlashEffectTimerElapsed);

            RegisterHotKey(this.Handle, 0, (int)KeyModifier.NOMOD, (int)Keys.F9);
            RegisterHotKey(this.Handle, 1, (int)KeyModifier.NOMOD, (int)Keys.F10);
            RegisterHotKey(this.Handle, 2, (int)KeyModifier.NOMOD, (int)Keys.F7);
            RegisterHotKey(this.Handle, 3, (int)KeyModifier.NOMOD, (int)Keys.F8);
        }

        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY) {
                //Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                //KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                          // The id of the hotkey that was pressed.

                if (id == 0)
                    Program.StartTimer();
                else if (id == 1)
                    Program.StopTimer();
                else if (id == 2)
                    Program.StartEpisode();
                else if (id == 3)
                    Program.StopEpisode();
            }
        }

        public void SetEpTime(TimeSpan eptime) {
            SetEpTimeSafe(eptime.ToString(TIMEFORMAT));
        }

        public void ResetEpTime() {
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

        public void AddEpisodeTime(TimeSpan start, TimeSpan stop) {
            lv_eptimes.Items.Add(new ListViewItem(new string[] { start.ToString(TIMEFORMAT), stop.ToString(TIMEFORMAT), start.Subtract(stop).ToString(TIMEFORMAT) }));
        }

        public void AddMarkerInfo(TimeSpan timestamp, string info) {
            lv_marker.Items.Add(new ListViewItem(new String[] { timestamp.ToString(TIMEFORMAT), info }));
        }

        public void SetTotalTime(TimeSpan totaltime) {
            if (tb_totaltime.InvokeRequired)
                tb_totaltime.Invoke((MethodInvoker)delegate() {
                    tb_totaltime.Text = totaltime.ToString(TIMEFORMAT);
                });
            else
                tb_totaltime.Text = totaltime.ToString(TIMEFORMAT);
        }

        public void OnStart() {
            lv_eptimes.Items.Clear();
            lv_marker.Items.Clear();
            foreach (Button button in buttonGroupEnabledOnStart)
                button.Enabled = true;
            foreach (Button button in buttonGroupDisabledOnStart)
                button.Enabled = false;
        }

        public void OnStop() {
            foreach (Button button in buttonGroupEnabledOnStart)
                button.Enabled = false;
            foreach (Button button in buttonGroupDisabledOnStart)
                button.Enabled = true;
        }

        public void OnEpisodeStart() {
            // Stub
        }

        public void OnEpisodeStop() {
            flashEffectTimer.Enabled = false;
            tb_episodetime.BackColor = Color.WhiteSmoke;
        }

        public void OnAlertTimeElapsed() {
            flashEffectTimer.Enabled = true;
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
            Program.SetAlertTime(dtp_alert.Value);
        }

        private void button1_Click(object sender, EventArgs e) {
            Program.ExportMarker();
        }

        private void bt_save_Click(object sender, EventArgs e) {
            Program.SaveMarkers();
        }

        private void bt_load_Click(object sender, EventArgs e) {
            Program.LoadMarkers();
        }

        public void resetForm() {
            tb_episodetime.Clear();
            tb_episodetime.Clear();
            lv_eptimes.Items.Clear();
            lv_marker.Items.Clear();
        }
    }
}
