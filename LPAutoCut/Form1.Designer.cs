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
            this.btn_mark = new System.Windows.Forms.Button();
            this.btn_cut = new System.Windows.Forms.Button();
            this.lv_marker = new System.Windows.Forms.ListView();
            this.ch_timestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_edit = new System.Windows.Forms.Button();
            this.lv_eptimes = new System.Windows.Forms.ListView();
            this.ch_start = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_end = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bt_epstart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_stop = new System.Windows.Forms.Button();
            this.bt_epend = new System.Windows.Forms.Button();
            this.bt_start = new System.Windows.Forms.Button();
            this.tb_episodetime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_totaltime = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(296, 362);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_mark);
            this.tabPage1.Controls.Add(this.btn_cut);
            this.tabPage1.Controls.Add(this.lv_marker);
            this.tabPage1.Controls.Add(this.btn_edit);
            this.tabPage1.Controls.Add(this.lv_eptimes);
            this.tabPage1.Controls.Add(this.bt_epstart);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.bt_stop);
            this.tabPage1.Controls.Add(this.bt_epend);
            this.tabPage1.Controls.Add(this.bt_start);
            this.tabPage1.Controls.Add(this.tb_episodetime);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tb_totaltime);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(288, 336);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Timer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_mark
            // 
            this.btn_mark.Location = new System.Drawing.Point(187, 84);
            this.btn_mark.Name = "btn_mark";
            this.btn_mark.Size = new System.Drawing.Size(75, 23);
            this.btn_mark.TabIndex = 14;
            this.btn_mark.Text = "Mark";
            this.btn_mark.UseVisualStyleBackColor = true;
            this.btn_mark.Click += new System.EventHandler(this.btn_mark_Click);
            // 
            // btn_cut
            // 
            this.btn_cut.Location = new System.Drawing.Point(106, 85);
            this.btn_cut.Name = "btn_cut";
            this.btn_cut.Size = new System.Drawing.Size(75, 23);
            this.btn_cut.TabIndex = 13;
            this.btn_cut.Text = "Cut";
            this.btn_cut.UseVisualStyleBackColor = true;
            this.btn_cut.Click += new System.EventHandler(this.btn_cut_Click);
            // 
            // lv_marker
            // 
            this.lv_marker.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_timestamp,
            this.ch_description});
            this.lv_marker.Location = new System.Drawing.Point(11, 114);
            this.lv_marker.Name = "lv_marker";
            this.lv_marker.Size = new System.Drawing.Size(263, 86);
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
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(25, 85);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(75, 23);
            this.btn_edit.TabIndex = 11;
            this.btn_edit.Text = "Edit";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // lv_eptimes
            // 
            this.lv_eptimes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_start,
            this.ch_end,
            this.ch_duration});
            this.lv_eptimes.Location = new System.Drawing.Point(11, 219);
            this.lv_eptimes.Name = "lv_eptimes";
            this.lv_eptimes.Size = new System.Drawing.Size(263, 105);
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
            this.ch_duration.Width = 80;
            // 
            // bt_epstart
            // 
            this.bt_epstart.Enabled = false;
            this.bt_epstart.Location = new System.Drawing.Point(118, 55);
            this.bt_epstart.Name = "bt_epstart";
            this.bt_epstart.Size = new System.Drawing.Size(75, 23);
            this.bt_epstart.TabIndex = 9;
            this.bt_epstart.Text = "EP Start";
            this.bt_epstart.UseVisualStyleBackColor = true;
            this.bt_epstart.Click += new System.EventHandler(this.bt_setstart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Episode Times";
            // 
            // bt_stop
            // 
            this.bt_stop.Location = new System.Drawing.Point(199, 19);
            this.bt_stop.Name = "bt_stop";
            this.bt_stop.Size = new System.Drawing.Size(75, 23);
            this.bt_stop.TabIndex = 6;
            this.bt_stop.Text = "Stop";
            this.bt_stop.UseVisualStyleBackColor = true;
            this.bt_stop.Click += new System.EventHandler(this.bt_stop_Click);
            // 
            // bt_epend
            // 
            this.bt_epend.Enabled = false;
            this.bt_epend.Location = new System.Drawing.Point(199, 55);
            this.bt_epend.Name = "bt_epend";
            this.bt_epend.Size = new System.Drawing.Size(75, 23);
            this.bt_epend.TabIndex = 5;
            this.bt_epend.Text = "EP End";
            this.bt_epend.UseVisualStyleBackColor = true;
            this.bt_epend.Click += new System.EventHandler(this.bt_setend_Click);
            // 
            // bt_start
            // 
            this.bt_start.Location = new System.Drawing.Point(118, 19);
            this.bt_start.Name = "bt_start";
            this.bt_start.Size = new System.Drawing.Size(75, 23);
            this.bt_start.TabIndex = 4;
            this.bt_start.Text = "Start";
            this.bt_start.UseVisualStyleBackColor = true;
            this.bt_start.Click += new System.EventHandler(this.bt_start_Click);
            // 
            // tb_episodetime
            // 
            this.tb_episodetime.Location = new System.Drawing.Point(11, 59);
            this.tb_episodetime.Name = "tb_episodetime";
            this.tb_episodetime.ReadOnly = true;
            this.tb_episodetime.Size = new System.Drawing.Size(100, 20);
            this.tb_episodetime.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Episode Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total Time";
            // 
            // tb_totaltime
            // 
            this.tb_totaltime.Location = new System.Drawing.Point(11, 19);
            this.tb_totaltime.Name = "tb_totaltime";
            this.tb_totaltime.ReadOnly = true;
            this.tb_totaltime.Size = new System.Drawing.Size(100, 20);
            this.tb_totaltime.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(288, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 362);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lv_eptimes;
        private System.Windows.Forms.Button bt_epstart;
        private System.Windows.Forms.Label label3;
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
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.ColumnHeader ch_timestamp;
        private System.Windows.Forms.ColumnHeader ch_description;
        private System.Windows.Forms.Button btn_mark;
        private System.Windows.Forms.Button btn_cut;
    }
}

