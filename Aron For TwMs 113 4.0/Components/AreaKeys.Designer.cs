
namespace Aron_For_TwMs_113_4.Components
{
    partial class AreaKeys
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.CoMP = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.CkMP = new System.Windows.Forms.CheckBox();
            this.CoHP = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CkHP = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.num4 = new System.Windows.Forms.NumericUpDown();
            this.num3 = new System.Windows.Forms.NumericUpDown();
            this.num2 = new System.Windows.Forms.NumericUpDown();
            this.num1 = new System.Windows.Forms.NumericUpDown();
            this.num0 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.CoKey4 = new System.Windows.Forms.ComboBox();
            this.CkKey4 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CoKey3 = new System.Windows.Forms.ComboBox();
            this.CkKey3 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CoKey2 = new System.Windows.Forms.ComboBox();
            this.CkKey2 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CoKey1 = new System.Windows.Forms.ComboBox();
            this.CkKey1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CoKey0 = new System.Windows.Forms.ComboBox();
            this.CkKey0 = new System.Windows.Forms.CheckBox();
            this.numHP = new System.Windows.Forms.NumericUpDown();
            this.numMP = new System.Windows.Forms.NumericUpDown();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMP)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.numMP);
            this.groupBox4.Controls.Add(this.numHP);
            this.groupBox4.Controls.Add(this.CoMP);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.CkMP);
            this.groupBox4.Controls.Add(this.CoHP);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.CkHP);
            this.groupBox4.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(22, 288);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(407, 125);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "自動恢復";
            // 
            // CoMP
            // 
            this.CoMP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoMP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CoMP.FormattingEnabled = true;
            this.CoMP.Location = new System.Drawing.Point(253, 74);
            this.CoMP.Name = "CoMP";
            this.CoMP.Size = new System.Drawing.Size(121, 24);
            this.CoMP.TabIndex = 22;
            this.CoMP.SelectedIndexChanged += new System.EventHandler(this.combo_AutoRecoverKeys_Changed);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(223, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 15);
            this.label7.TabIndex = 23;
            this.label7.Text = "按";
            // 
            // CkMP
            // 
            this.CkMP.AutoSize = true;
            this.CkMP.Location = new System.Drawing.Point(27, 79);
            this.CkMP.Name = "CkMP";
            this.CkMP.Size = new System.Drawing.Size(81, 19);
            this.CkMP.TabIndex = 25;
            this.CkMP.Text = "MP 少於";
            this.CkMP.UseVisualStyleBackColor = true;
            this.CkMP.CheckedChanged += new System.EventHandler(this.ckAutoKeyboard_Changed);
            // 
            // CoHP
            // 
            this.CoHP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoHP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CoHP.FormattingEnabled = true;
            this.CoHP.Location = new System.Drawing.Point(253, 27);
            this.CoHP.Name = "CoHP";
            this.CoHP.Size = new System.Drawing.Size(121, 24);
            this.CoHP.TabIndex = 21;
            this.CoHP.SelectedIndexChanged += new System.EventHandler(this.combo_AutoRecoverKeys_Changed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(223, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 15);
            this.label6.TabIndex = 21;
            this.label6.Text = "按";
            // 
            // CkHP
            // 
            this.CkHP.AutoSize = true;
            this.CkHP.Location = new System.Drawing.Point(27, 32);
            this.CkHP.Name = "CkHP";
            this.CkHP.Size = new System.Drawing.Size(78, 19);
            this.CkHP.TabIndex = 21;
            this.CkHP.Text = "HP 少於";
            this.CkHP.UseVisualStyleBackColor = true;
            this.CkHP.CheckedChanged += new System.EventHandler(this.ckAutoKeyboard_Changed);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.num4);
            this.groupBox2.Controls.Add(this.num3);
            this.groupBox2.Controls.Add(this.num2);
            this.groupBox2.Controls.Add(this.num1);
            this.groupBox2.Controls.Add(this.num0);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.CoKey4);
            this.groupBox2.Controls.Add(this.CkKey4);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.CoKey3);
            this.groupBox2.Controls.Add(this.CkKey3);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.CoKey2);
            this.groupBox2.Controls.Add(this.CkKey2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.CoKey1);
            this.groupBox2.Controls.Add(this.CkKey1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.CoKey0);
            this.groupBox2.Controls.Add(this.CkKey0);
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(22, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(407, 279);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "自動按鍵";
            // 
            // num4
            // 
            this.num4.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.num4.Location = new System.Drawing.Point(240, 238);
            this.num4.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.num4.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num4.Name = "num4";
            this.num4.Size = new System.Drawing.Size(87, 23);
            this.num4.TabIndex = 25;
            this.num4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.num4.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num4.ValueChanged += new System.EventHandler(this.num_AutoKeyboard_ValueChanged);
            // 
            // num3
            // 
            this.num3.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.num3.Location = new System.Drawing.Point(240, 185);
            this.num3.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.num3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num3.Name = "num3";
            this.num3.Size = new System.Drawing.Size(87, 23);
            this.num3.TabIndex = 24;
            this.num3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.num3.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num3.ValueChanged += new System.EventHandler(this.num_AutoKeyboard_ValueChanged);
            // 
            // num2
            // 
            this.num2.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.num2.Location = new System.Drawing.Point(240, 132);
            this.num2.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.num2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(87, 23);
            this.num2.TabIndex = 23;
            this.num2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.num2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num2.ValueChanged += new System.EventHandler(this.num_AutoKeyboard_ValueChanged);
            // 
            // num1
            // 
            this.num1.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.num1.Location = new System.Drawing.Point(240, 79);
            this.num1.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.num1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(87, 23);
            this.num1.TabIndex = 22;
            this.num1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.num1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num1.ValueChanged += new System.EventHandler(this.num_AutoKeyboard_ValueChanged);
            // 
            // num0
            // 
            this.num0.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.num0.Location = new System.Drawing.Point(240, 28);
            this.num0.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.num0.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num0.Name = "num0";
            this.num0.Size = new System.Drawing.Size(87, 23);
            this.num0.TabIndex = 21;
            this.num0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.num0.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num0.ValueChanged += new System.EventHandler(this.num_AutoKeyboard_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(341, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 20;
            this.label5.Text = "毫秒";
            // 
            // CoKey4
            // 
            this.CoKey4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoKey4.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CoKey4.FormattingEnabled = true;
            this.CoKey4.Location = new System.Drawing.Point(100, 238);
            this.CoKey4.Name = "CoKey4";
            this.CoKey4.Size = new System.Drawing.Size(121, 24);
            this.CoKey4.TabIndex = 18;
            this.CoKey4.SelectedIndexChanged += new System.EventHandler(this.combo_AutoKeys_Changed);
            // 
            // CkKey4
            // 
            this.CkKey4.AutoSize = true;
            this.CkKey4.Location = new System.Drawing.Point(27, 243);
            this.CkKey4.Name = "CkKey4";
            this.CkKey4.Size = new System.Drawing.Size(63, 19);
            this.CkKey4.TabIndex = 17;
            this.CkKey4.Text = "按鍵4";
            this.CkKey4.UseVisualStyleBackColor = true;
            this.CkKey4.CheckedChanged += new System.EventHandler(this.ckAutoKeyboard_Changed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(341, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "毫秒";
            // 
            // CoKey3
            // 
            this.CoKey3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoKey3.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CoKey3.FormattingEnabled = true;
            this.CoKey3.Location = new System.Drawing.Point(100, 184);
            this.CoKey3.Name = "CoKey3";
            this.CoKey3.Size = new System.Drawing.Size(121, 24);
            this.CoKey3.TabIndex = 14;
            this.CoKey3.SelectedIndexChanged += new System.EventHandler(this.combo_AutoKeys_Changed);
            // 
            // CkKey3
            // 
            this.CkKey3.AutoSize = true;
            this.CkKey3.Location = new System.Drawing.Point(27, 190);
            this.CkKey3.Name = "CkKey3";
            this.CkKey3.Size = new System.Drawing.Size(63, 19);
            this.CkKey3.TabIndex = 13;
            this.CkKey3.Text = "按鍵3";
            this.CkKey3.UseVisualStyleBackColor = true;
            this.CkKey3.CheckedChanged += new System.EventHandler(this.ckAutoKeyboard_Changed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(341, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "毫秒";
            // 
            // CoKey2
            // 
            this.CoKey2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoKey2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CoKey2.FormattingEnabled = true;
            this.CoKey2.Location = new System.Drawing.Point(100, 131);
            this.CoKey2.Name = "CoKey2";
            this.CoKey2.Size = new System.Drawing.Size(121, 24);
            this.CoKey2.TabIndex = 10;
            this.CoKey2.SelectedIndexChanged += new System.EventHandler(this.combo_AutoKeys_Changed);
            // 
            // CkKey2
            // 
            this.CkKey2.AutoSize = true;
            this.CkKey2.Location = new System.Drawing.Point(27, 137);
            this.CkKey2.Name = "CkKey2";
            this.CkKey2.Size = new System.Drawing.Size(63, 19);
            this.CkKey2.TabIndex = 9;
            this.CkKey2.Text = "按鍵2";
            this.CkKey2.UseVisualStyleBackColor = true;
            this.CkKey2.CheckedChanged += new System.EventHandler(this.ckAutoKeyboard_Changed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "毫秒";
            // 
            // CoKey1
            // 
            this.CoKey1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoKey1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CoKey1.FormattingEnabled = true;
            this.CoKey1.Location = new System.Drawing.Point(100, 79);
            this.CoKey1.Name = "CoKey1";
            this.CoKey1.Size = new System.Drawing.Size(121, 24);
            this.CoKey1.TabIndex = 6;
            this.CoKey1.SelectedIndexChanged += new System.EventHandler(this.combo_AutoKeys_Changed);
            // 
            // CkKey1
            // 
            this.CkKey1.AutoSize = true;
            this.CkKey1.Location = new System.Drawing.Point(27, 84);
            this.CkKey1.Name = "CkKey1";
            this.CkKey1.Size = new System.Drawing.Size(63, 19);
            this.CkKey1.TabIndex = 5;
            this.CkKey1.Text = "按鍵1";
            this.CkKey1.UseVisualStyleBackColor = true;
            this.CkKey1.CheckedChanged += new System.EventHandler(this.ckAutoKeyboard_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(341, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "毫秒";
            // 
            // CoKey0
            // 
            this.CoKey0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoKey0.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CoKey0.FormattingEnabled = true;
            this.CoKey0.Location = new System.Drawing.Point(100, 27);
            this.CoKey0.Name = "CoKey0";
            this.CoKey0.Size = new System.Drawing.Size(121, 24);
            this.CoKey0.TabIndex = 2;
            this.CoKey0.SelectedIndexChanged += new System.EventHandler(this.combo_AutoKeys_Changed);
            // 
            // CkKey0
            // 
            this.CkKey0.AutoSize = true;
            this.CkKey0.Location = new System.Drawing.Point(27, 31);
            this.CkKey0.Name = "CkKey0";
            this.CkKey0.Size = new System.Drawing.Size(63, 19);
            this.CkKey0.TabIndex = 1;
            this.CkKey0.Text = "按鍵0";
            this.CkKey0.UseVisualStyleBackColor = true;
            this.CkKey0.CheckedChanged += new System.EventHandler(this.ckAutoKeyboard_Changed);
            // 
            // numHP
            // 
            this.numHP.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numHP.Location = new System.Drawing.Point(121, 33);
            this.numHP.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numHP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHP.Name = "numHP";
            this.numHP.Size = new System.Drawing.Size(87, 23);
            this.numHP.TabIndex = 26;
            this.numHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numHP.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numHP.ValueChanged += new System.EventHandler(this.num_HPMP_ValueChanged);
            // 
            // numMP
            // 
            this.numMP.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numMP.Location = new System.Drawing.Point(121, 74);
            this.numMP.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numMP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMP.Name = "numMP";
            this.numMP.Size = new System.Drawing.Size(87, 23);
            this.numMP.TabIndex = 27;
            this.numMP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numMP.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMP.ValueChanged += new System.EventHandler(this.num_HPMP_ValueChanged);
            // 
            // AreaKeys
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AreaKeys";
            this.Size = new System.Drawing.Size(458, 457);
            this.Load += new System.EventHandler(this.AreaKeys_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox CoMP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox CkMP;
        private System.Windows.Forms.ComboBox CoHP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox CkHP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CoKey4;
        private System.Windows.Forms.CheckBox CkKey4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CoKey3;
        private System.Windows.Forms.CheckBox CkKey3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CoKey2;
        private System.Windows.Forms.CheckBox CkKey2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CoKey1;
        private System.Windows.Forms.CheckBox CkKey1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CoKey0;
        private System.Windows.Forms.CheckBox CkKey0;
        private System.Windows.Forms.NumericUpDown num4;
        private System.Windows.Forms.NumericUpDown num3;
        private System.Windows.Forms.NumericUpDown num2;
        private System.Windows.Forms.NumericUpDown num1;
        private System.Windows.Forms.NumericUpDown num0;
        private System.Windows.Forms.NumericUpDown numMP;
        private System.Windows.Forms.NumericUpDown numHP;
    }
}
