
namespace Aron_For_TwMs_113_4.Components
{
    partial class AreaLock
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radSelect = new System.Windows.Forms.RadioButton();
            this.radAutoLock = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.t_Lock = new System.Windows.Forms.Timer(this.components);
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ckNotify = new System.Windows.Forms.CheckBox();
            this.ckAutoSave = new System.Windows.Forms.CheckBox();
            this.CkStart = new System.Windows.Forms.CheckBox();
            this.Bubypass = new System.Windows.Forms.Button();
            this.CkBypass = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnF5 = new System.Windows.Forms.Button();
            this.btnUserSetting = new System.Windows.Forms.Button();
            this.lbExeFile = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.radSelect);
            this.groupBox1.Controls.Add(this.radAutoLock);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "鎖定方式";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(175, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Filter:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("新細明體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox1.Location = new System.Drawing.Point(230, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 25);
            this.textBox1.TabIndex = 8;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(358, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 65);
            this.button1.TabIndex = 7;
            this.button1.Text = "鎖定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radSelect
            // 
            this.radSelect.AutoSize = true;
            this.radSelect.Checked = true;
            this.radSelect.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.radSelect.Location = new System.Drawing.Point(16, 61);
            this.radSelect.Margin = new System.Windows.Forms.Padding(4);
            this.radSelect.Name = "radSelect";
            this.radSelect.Size = new System.Drawing.Size(75, 16);
            this.radSelect.TabIndex = 6;
            this.radSelect.TabStop = true;
            this.radSelect.Text = "手動選擇";
            this.radSelect.UseVisualStyleBackColor = true;
            this.radSelect.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radAutoLock
            // 
            this.radAutoLock.AutoSize = true;
            this.radAutoLock.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radAutoLock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.radAutoLock.Location = new System.Drawing.Point(16, 27);
            this.radAutoLock.Margin = new System.Windows.Forms.Padding(4);
            this.radAutoLock.Name = "radAutoLock";
            this.radAutoLock.Size = new System.Drawing.Size(75, 16);
            this.radAutoLock.TabIndex = 5;
            this.radAutoLock.Text = "自動鎖定";
            this.radAutoLock.UseVisualStyleBackColor = true;
            this.radAutoLock.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(112, 58);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(239, 22);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            // 
            // t_Lock
            // 
            this.t_Lock.Enabled = true;
            this.t_Lock.Interval = 2000;
            this.t_Lock.Tick += new System.EventHandler(this.t_Lock_Tick);
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.Transparent;
            this.groupBox7.Controls.Add(this.ckNotify);
            this.groupBox7.Controls.Add(this.ckAutoSave);
            this.groupBox7.Controls.Add(this.CkStart);
            this.groupBox7.Controls.Add(this.Bubypass);
            this.groupBox7.Controls.Add(this.CkBypass);
            this.groupBox7.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox7.ForeColor = System.Drawing.Color.White;
            this.groupBox7.Location = new System.Drawing.Point(8, 105);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(439, 95);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Options";
            // 
            // ckNotify
            // 
            this.ckNotify.AutoSize = true;
            this.ckNotify.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ckNotify.Location = new System.Drawing.Point(104, 57);
            this.ckNotify.Margin = new System.Windows.Forms.Padding(4);
            this.ckNotify.Name = "ckNotify";
            this.ckNotify.Size = new System.Drawing.Size(79, 21);
            this.ckNotify.TabIndex = 15;
            this.ckNotify.Text = "通知開關";
            this.ckNotify.UseVisualStyleBackColor = true;
            this.ckNotify.CheckedChanged += new System.EventHandler(this.ckNotify_CheckedChanged);
            // 
            // ckAutoSave
            // 
            this.ckAutoSave.AutoSize = true;
            this.ckAutoSave.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ckAutoSave.Location = new System.Drawing.Point(160, 25);
            this.ckAutoSave.Margin = new System.Windows.Forms.Padding(4);
            this.ckAutoSave.Name = "ckAutoSave";
            this.ckAutoSave.Size = new System.Drawing.Size(131, 21);
            this.ckAutoSave.TabIndex = 14;
            this.ckAutoSave.Text = "自動儲存本頁設定";
            this.ckAutoSave.UseVisualStyleBackColor = true;
            this.ckAutoSave.CheckedChanged += new System.EventHandler(this.S_CheckedChanged);
            // 
            // CkStart
            // 
            this.CkStart.AutoSize = true;
            this.CkStart.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CkStart.Location = new System.Drawing.Point(18, 57);
            this.CkStart.Margin = new System.Windows.Forms.Padding(4);
            this.CkStart.Name = "CkStart";
            this.CkStart.Size = new System.Drawing.Size(78, 21);
            this.CkStart.TabIndex = 13;
            this.CkStart.Text = "自動Play";
            this.CkStart.UseVisualStyleBackColor = true;
            this.CkStart.CheckedChanged += new System.EventHandler(this.CkStart_CheckedChanged);
            // 
            // Bubypass
            // 
            this.Bubypass.ForeColor = System.Drawing.Color.Black;
            this.Bubypass.Location = new System.Drawing.Point(348, 28);
            this.Bubypass.Margin = new System.Windows.Forms.Padding(4);
            this.Bubypass.Name = "Bubypass";
            this.Bubypass.Size = new System.Drawing.Size(83, 50);
            this.Bubypass.TabIndex = 12;
            this.Bubypass.Text = "手動注入ByPass";
            this.Bubypass.UseVisualStyleBackColor = true;
            this.Bubypass.Click += new System.EventHandler(this.Bubypass_Click);
            // 
            // CkBypass
            // 
            this.CkBypass.AutoSize = true;
            this.CkBypass.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CkBypass.Location = new System.Drawing.Point(18, 25);
            this.CkBypass.Margin = new System.Windows.Forms.Padding(4);
            this.CkBypass.Name = "CkBypass";
            this.CkBypass.Size = new System.Drawing.Size(134, 21);
            this.CkBypass.TabIndex = 11;
            this.CkBypass.Text = "鎖定即注入ByPass";
            this.CkBypass.UseVisualStyleBackColor = true;
            this.CkBypass.CheckedChanged += new System.EventHandler(this.CkBypass_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.btnF5);
            this.groupBox2.Controls.Add(this.btnUserSetting);
            this.groupBox2.Controls.Add(this.lbExeFile);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(8, 213);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(439, 123);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "啟動器";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBox2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(9, 31);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(174, 22);
            this.comboBox2.TabIndex = 10;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // btnF5
            // 
            this.btnF5.ForeColor = System.Drawing.Color.Black;
            this.btnF5.Location = new System.Drawing.Point(203, 19);
            this.btnF5.Margin = new System.Windows.Forms.Padding(4);
            this.btnF5.Name = "btnF5";
            this.btnF5.Size = new System.Drawing.Size(63, 41);
            this.btnF5.TabIndex = 19;
            this.btnF5.Text = "刷新";
            this.btnF5.UseVisualStyleBackColor = true;
            this.btnF5.Click += new System.EventHandler(this.btnF5_Click);
            // 
            // btnUserSetting
            // 
            this.btnUserSetting.ForeColor = System.Drawing.Color.Black;
            this.btnUserSetting.Location = new System.Drawing.Point(274, 19);
            this.btnUserSetting.Margin = new System.Windows.Forms.Padding(4);
            this.btnUserSetting.Name = "btnUserSetting";
            this.btnUserSetting.Size = new System.Drawing.Size(68, 41);
            this.btnUserSetting.TabIndex = 18;
            this.btnUserSetting.Text = "自訂義";
            this.btnUserSetting.UseVisualStyleBackColor = true;
            this.btnUserSetting.Click += new System.EventHandler(this.btnUserSetting_Click);
            // 
            // lbExeFile
            // 
            this.lbExeFile.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbExeFile.Location = new System.Drawing.Point(7, 64);
            this.lbExeFile.Name = "lbExeFile";
            this.lbExeFile.Size = new System.Drawing.Size(426, 50);
            this.lbExeFile.TabIndex = 17;
            this.lbExeFile.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lbExeFile.TextChanged += new System.EventHandler(this.lbExeFile_TextChanged);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(350, 19);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 41);
            this.button2.TabIndex = 16;
            this.button2.Text = "選擇程式";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "MapleStory.exe";
            this.openFileDialog1.Filter = "*.exe|*.exe";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // AreaLock
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AreaLock";
            this.Size = new System.Drawing.Size(458, 590);
            this.Load += new System.EventHandler(this.AreaLock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radSelect;
        private System.Windows.Forms.RadioButton radAutoLock;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer t_Lock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button Bubypass;
        private System.Windows.Forms.CheckBox CkBypass;
        private System.Windows.Forms.CheckBox CkStart;
        private System.Windows.Forms.CheckBox ckAutoSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckNotify;
        private System.Windows.Forms.Label lbExeFile;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnUserSetting;
        private System.Windows.Forms.Button btnF5;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}
