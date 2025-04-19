namespace WordFilteringTryIt
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.descFun = new System.Windows.Forms.Label();
            this.serviceURL = new System.Windows.Forms.Label();
            this.methodOP = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(60, 110);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(464, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(60, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // descFun
            // 
            this.descFun.AutoSize = true;
            this.descFun.Location = new System.Drawing.Point(57, 246);
            this.descFun.Name = "descFun";
            this.descFun.Size = new System.Drawing.Size(48, 13);
            this.descFun.TabIndex = 4;
            this.descFun.Text = "descFun";
            // 
            // serviceURL
            // 
            this.serviceURL.AutoSize = true;
            this.serviceURL.Location = new System.Drawing.Point(57, 274);
            this.serviceURL.Name = "serviceURL";
            this.serviceURL.Size = new System.Drawing.Size(63, 13);
            this.serviceURL.TabIndex = 5;
            this.serviceURL.Text = "serviceURL";
            this.serviceURL.Click += new System.EventHandler(this.serviceURL_Click);
            // 
            // methodOP
            // 
            this.methodOP.AutoSize = true;
            this.methodOP.Location = new System.Drawing.Point(57, 303);
            this.methodOP.Name = "methodOP";
            this.methodOP.Size = new System.Drawing.Size(57, 13);
            this.methodOP.TabIndex = 6;
            this.methodOP.Text = "methodOP";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.methodOP);
            this.Controls.Add(this.serviceURL);
            this.Controls.Add(this.descFun);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label descFun;
        private System.Windows.Forms.Label serviceURL;
        private System.Windows.Forms.Label methodOP;
    }
}

