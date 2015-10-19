namespace LPAutoCut {
    partial class Form1 {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_totaltime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_epend = new System.Windows.Forms.Button();
            this.bt_epstart = new System.Windows.Forms.Button();
            this.tb_episodetime = new System.Windows.Forms.TextBox();
            this.bt_stop = new System.Windows.Forms.Button();
            this.bt_start = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lv_eptimes = new System.Windows.Forms.ListView();
            this.ch_start = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_end = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_edit = new System.Windows.Forms.Button();
            this.bt_cut = new System.Windows.Forms.Button();
            this.lv_marker = new System.Windows.Forms.ListView();
            this.ch_timestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bt_mark = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtp_alert = new System.Windows.Forms.DateTimePicker();
            this.cb_alert = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(334, 501);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(326, 475);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Timer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tb_totaltime);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.bt_epend);
            this.groupBox3.Controls.Add(this.bt_epstart);
            this.groupBox3.Controls.Add(this.tb_episodetime);
            this.groupBox3.Controls.Add(this.bt_stop);
            this.groupBox3.Controls.Add(this.bt_start);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(8, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(307, 137);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Timer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total Time";
            // 
            // tb_totaltime
            // 
            this.tb_totaltime.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_totaltime.Location = new System.Drawing.Point(6, 32);
            this.tb_totaltime.Name = "tb_totaltime";
            this.tb_totaltime.ReadOnly = true;
            this.tb_totaltime.Size = new System.Drawing.Size(129, 38);
            this.tb_totaltime.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Episode Time";
            // 
            // bt_epend
            // 
            this.bt_epend.Enabled = false;
            this.bt_epend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_epend.Location = new System.Drawing.Point(222, 89);
            this.bt_epend.Name = "bt_epend";
            this.bt_epend.Size = new System.Drawing.Size(75, 38);
            this.bt_epend.TabIndex = 5;
            this.bt_epend.Text = "EP End";
            this.bt_epend.UseVisualStyleBackColor = true;
            this.bt_epend.Click += new System.EventHandler(this.bt_setend_Click);
            // 
            // bt_epstart
            // 
            this.bt_epstart.Enabled = false;
            this.bt_epstart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_epstart.Location = new System.Drawing.Point(141, 89);
            this.bt_epstart.Name = "bt_epstart";
            this.bt_epstart.Size = new System.Drawing.Size(75, 38);
            this.bt_epstart.TabIndex = 9;
            this.bt_epstart.Text = "EP Start";
            this.bt_epstart.UseVisualStyleBackColor = true;
            this.bt_epstart.Click += new System.EventHandler(this.bt_setstart_Click);
            // 
            // tb_episodetime
            // 
            this.tb_episodetime.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_episodetime.Location = new System.Drawing.Point(6, 89);
            this.tb_episodetime.Name = "tb_episodetime";
            this.tb_episodetime.ReadOnly = true;
            this.tb_episodetime.Size = new System.Drawing.Size(129, 38);
            this.tb_episodetime.TabIndex = 3;
            // 
            // bt_stop
            // 
            this.bt_stop.Enabled = false;
            this.bt_stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_stop.Location = new System.Drawing.Point(222, 32);
            this.bt_stop.Name = "bt_stop";
            this.bt_stop.Size = new System.Drawing.Size(75, 38);
            this.bt_stop.TabIndex = 6;
            this.bt_stop.Text = "Stop";
            this.bt_stop.UseVisualStyleBackColor = true;
            this.bt_stop.Click += new System.EventHandler(this.bt_stop_Click);
            // 
            // bt_start
            // 
            this.bt_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_start.Location = new System.Drawing.Point(141, 32);
            this.bt_start.Name = "bt_start";
            this.bt_start.Size = new System.Drawing.Size(75, 38);
            this.bt_start.TabIndex = 4;
            this.bt_start.Text = "Start";
            this.bt_start.UseVisualStyleBackColor = true;
            this.bt_start.Click += new System.EventHandler(this.bt_start_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lv_eptimes);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 334);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 135);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Episode Times";
            // 
            // lv_eptimes
            // 
            this.lv_eptimes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_start,
            this.ch_end,
            this.ch_duration});
            this.lv_eptimes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_eptimes.Enabled = false;
            this.lv_eptimes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_eptimes.FullRowSelect = true;
            this.lv_eptimes.Location = new System.Drawing.Point(3, 16);
            this.lv_eptimes.Name = "lv_eptimes";
            this.lv_eptimes.Size = new System.Drawing.Size(301, 116);
            this.lv_eptimes.TabIndex = 10;
            this.lv_eptimes.UseCompatibleStateImageBehavior = false;
            this.lv_eptimes.View = System.Windows.Forms.View.Details;
            // 
            // ch_start
            // 
            this.ch_start.Text = "Start";
            this.ch_start.Width = 80;
            // 
            // ch_end
            // 
            this.ch_end.Text = "End";
            this.ch_end.Width = 80;
            // 
            // ch_duration
            // 
            this.ch_duration.Text = "Duration";
            this.ch_duration.Width = 83;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_edit);
            this.groupBox1.Controls.Add(this.bt_cut);
            this.groupBox1.Controls.Add(this.lv_marker);
            this.groupBox1.Controls.Add(this.bt_mark);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 179);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Marker";
            // 
            // bt_edit
            // 
            this.bt_edit.Enabled = false;
            this.bt_edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_edit.Location = new System.Drawing.Point(6, 19);
            this.bt_edit.Name = "bt_edit";
            this.bt_edit.Size = new System.Drawing.Size(75, 38);
            this.bt_edit.TabIndex = 11;
            this.bt_edit.Text = "Edit";
            this.bt_edit.UseVisualStyleBackColor = true;
            this.bt_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // bt_cut
            // 
            this.bt_cut.Enabled = false;
            this.bt_cut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_cut.Location = new System.Drawing.Point(87, 19);
            this.bt_cut.Name = "bt_cut";
            this.bt_cut.Size = new System.Drawing.Size(75, 38);
            this.bt_cut.TabIndex = 13;
            this.bt_cut.Text = "Cut";
            this.bt_cut.UseVisualStyleBackColor = true;
            this.bt_cut.Click += new System.EventHandler(this.btn_cut_Click);
            // 
            // lv_marker
            // 
            this.lv_marker.BackColor = System.Drawing.SystemColors.Window;
            this.lv_marker.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_timestamp,
            this.ch_description});
            this.lv_marker.Enabled = false;
            this.lv_marker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_marker.Location = new System.Drawing.Point(6, 63);
            this.lv_marker.Name = "lv_marker";
            this.lv_marker.Size = new System.Drawing.Size(291, 108);
            this.lv_marker.TabIndex = 12;
            this.lv_marker.UseCompatibleStateImageBehavior = false;
            this.lv_marker.View = System.Windows.Forms.View.Details;
            // 
            // ch_timestamp
            // 
            this.ch_timestamp.Text = "Timestamp";
            this.ch_timestamp.Width = 80;
            // 
            // ch_description
            // 
            this.ch_description.Text = "Description";
            this.ch_description.Width = 166;
            // 
            // bt_mark
            // 
            this.bt_mark.Enabled = false;
            this.bt_mark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_mark.Location = new System.Drawing.Point(168, 19);
            this.bt_mark.Name = "bt_mark";
            this.bt_mark.Size = new System.Drawing.Size(75, 38);
            this.bt_mark.TabIndex = 14;
            this.bt_mark.Text = "Mark";
            this.bt_mark.UseVisualStyleBackColor = true;
            this.bt_mark.Click += new System.EventHandler(this.btn_mark_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(326, 475);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtp_alert);
            this.groupBox4.Controls.Add(this.cb_alert);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(8, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(310, 77);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Alert";
            // 
            // dtp_alert
            // 
            this.dtp_alert.AllowDrop = true;
            this.dtp_alert.Enabled = false;
            this.dtp_alert.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_alert.Location = new System.Drawing.Point(7, 43);
            this.dtp_alert.Name = "dtp_alert";
            this.dtp_alert.Size = new System.Drawing.Size(118, 23);
            this.dtp_alert.TabIndex = 1;
            this.dtp_alert.ValueChanged += new System.EventHandler(this.dtp_alert_ValueChanged);
            // 
            // cb_alert
            // 
            this.cb_alert.AutoSize = true;
            this.cb_alert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_alert.Location = new System.Drawing.Point(7, 20);
            this.cb_alert.Name = "cb_alert";
            this.cb_alert.Size = new System.Drawing.Size(101, 17);
            this.cb_alert.TabIndex = 0;
            this.cb_alert.Text = "Alert at time limit";
            this.cb_alert.UseVisualStyleBackColor = true;
            this.cb_alert.CheckedChanged += new System.EventHandler(this.cb_alert_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 501);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lv_eptimes;
        private System.Windows.Forms.Button bt_epstart;
        private System.Windows.Forms.Button bt_stop;
        private System.Windows.Forms.Button bt_epend;
        private System.Windows.Forms.Button bt_start;
        private System.Windows.Forms.TextBox tb_episodetime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_totaltime;
        private System.Windows.Forms.ColumnHeader ch_start;
        private System.Windows.Forms.ColumnHeader ch_end;
        private System.Windows.Forms.ColumnHeader ch_duration;
        private System.Windows.Forms.ListView lv_marker;
        private System.Windows.Forms.Button bt_edit;
        private System.Windows.Forms.ColumnHeader ch_timestamp;
        private System.Windows.Forms.ColumnHeader ch_description;
        private System.Windows.Forms.Button bt_mark;
        private System.Windows.Forms.Button bt_cut;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtp_alert;
        private System.Windows.Forms.CheckBox cb_alert;
    }
}

