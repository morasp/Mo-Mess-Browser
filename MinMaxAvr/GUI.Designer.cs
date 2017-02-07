namespace Mo_Messs_Browser.Plugins.MinMaxAvr
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cb_aktiviert = new System.Windows.Forms.CheckBox();
            this.lbl_min = new System.Windows.Forms.Label();
            this.lbl_avr = new System.Windows.Forms.Label();
            this.lbl_max = new System.Windows.Forms.Label();
            this.lbl_avr_v = new System.Windows.Forms.Label();
            this.lbl_max_v = new System.Windows.Forms.Label();
            this.lbl_min_v = new System.Windows.Forms.Label();
            this.btn_reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_aktiviert
            // 
            this.cb_aktiviert.AutoSize = true;
            this.cb_aktiviert.Location = new System.Drawing.Point(20, 18);
            this.cb_aktiviert.Margin = new System.Windows.Forms.Padding(5);
            this.cb_aktiviert.Name = "cb_aktiviert";
            this.cb_aktiviert.Size = new System.Drawing.Size(93, 26);
            this.cb_aktiviert.TabIndex = 0;
            this.cb_aktiviert.Text = "Aktiviert";
            this.cb_aktiviert.UseVisualStyleBackColor = true;
            this.cb_aktiviert.CheckedChanged += new System.EventHandler(this.cb_aktiviert_CheckedChanged);
            this.cb_aktiviert.Click += new System.EventHandler(this.cb_aktiviert_CheckedChanged);
            // 
            // lbl_min
            // 
            this.lbl_min.AutoSize = true;
            this.lbl_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_min.Location = new System.Drawing.Point(33, 77);
            this.lbl_min.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_min.Name = "lbl_min";
            this.lbl_min.Size = new System.Drawing.Size(80, 22);
            this.lbl_min.TabIndex = 1;
            this.lbl_min.Text = "Minimum";
            // 
            // lbl_avr
            // 
            this.lbl_avr.AutoSize = true;
            this.lbl_avr.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_avr.Location = new System.Drawing.Point(33, 249);
            this.lbl_avr.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_avr.Name = "lbl_avr";
            this.lbl_avr.Size = new System.Drawing.Size(110, 22);
            this.lbl_avr.TabIndex = 2;
            this.lbl_avr.Text = "Durchschnitt";
            // 
            // lbl_max
            // 
            this.lbl_max.AutoSize = true;
            this.lbl_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_max.Location = new System.Drawing.Point(33, 163);
            this.lbl_max.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_max.Name = "lbl_max";
            this.lbl_max.Size = new System.Drawing.Size(85, 22);
            this.lbl_max.TabIndex = 3;
            this.lbl_max.Text = "Maximum";
            // 
            // lbl_avr_v
            // 
            this.lbl_avr_v.AutoSize = true;
            this.lbl_avr_v.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_avr_v.Location = new System.Drawing.Point(254, 245);
            this.lbl_avr_v.Name = "lbl_avr_v";
            this.lbl_avr_v.Size = new System.Drawing.Size(54, 25);
            this.lbl_avr_v.TabIndex = 4;
            this.lbl_avr_v.Text = "0.00";
            // 
            // lbl_max_v
            // 
            this.lbl_max_v.AutoSize = true;
            this.lbl_max_v.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_max_v.Location = new System.Drawing.Point(254, 159);
            this.lbl_max_v.Name = "lbl_max_v";
            this.lbl_max_v.Size = new System.Drawing.Size(54, 25);
            this.lbl_max_v.TabIndex = 5;
            this.lbl_max_v.Text = "0.00";
            // 
            // lbl_min_v
            // 
            this.lbl_min_v.AutoSize = true;
            this.lbl_min_v.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_min_v.Location = new System.Drawing.Point(254, 73);
            this.lbl_min_v.Name = "lbl_min_v";
            this.lbl_min_v.Size = new System.Drawing.Size(54, 25);
            this.lbl_min_v.TabIndex = 6;
            this.lbl_min_v.Text = "0.00";
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(511, 12);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(80, 39);
            this.btn_reset.TabIndex = 7;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 358);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.lbl_min_v);
            this.Controls.Add(this.lbl_max_v);
            this.Controls.Add(this.lbl_avr_v);
            this.Controls.Add(this.lbl_max);
            this.Controls.Add(this.lbl_avr);
            this.Controls.Add(this.lbl_min);
            this.Controls.Add(this.cb_aktiviert);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GUI";
            this.Text = "Min-Max-Avr";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cb_aktiviert;
        private System.Windows.Forms.Label lbl_min;
        private System.Windows.Forms.Label lbl_avr;
        private System.Windows.Forms.Label lbl_max;
        private System.Windows.Forms.Label lbl_avr_v;
        private System.Windows.Forms.Label lbl_max_v;
        private System.Windows.Forms.Label lbl_min_v;
        private System.Windows.Forms.Button btn_reset;
    }
}