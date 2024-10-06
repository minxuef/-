namespace CI
{
    partial class Dialogue
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
            this.pw = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pw
            // 
            this.pw.AutoSize = true;
            this.pw.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pw.Location = new System.Drawing.Point(83, 90);
            this.pw.Name = "pw";
            this.pw.Size = new System.Drawing.Size(97, 15);
            this.pw.TabIndex = 0;
            this.pw.Text = "请输入口令：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(201, 87);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(146, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Dialogue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 274);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pw);
            this.Name = "Dialogue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "口令输入对话框";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pw;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}