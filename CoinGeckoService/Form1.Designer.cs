namespace CoinGeckoService
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
            this.txtCoinName = new System.Windows.Forms.TextBox();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.pressToFetch = new System.Windows.Forms.Button();
            this.cryptoResult = new System.Windows.Forms.Label();
            this.serviceDesc = new System.Windows.Forms.Label();
            this.serviceURL = new System.Windows.Forms.Label();
            this.methodOpReturn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCoinName
            // 
            this.txtCoinName.Location = new System.Drawing.Point(89, 80);
            this.txtCoinName.Name = "txtCoinName";
            this.txtCoinName.Size = new System.Drawing.Size(100, 20);
            this.txtCoinName.TabIndex = 0;
            this.txtCoinName.Text = "Coin Name";
            // 
            // txtCurrency
            // 
            this.txtCurrency.Location = new System.Drawing.Point(212, 80);
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.Size = new System.Drawing.Size(100, 20);
            this.txtCurrency.TabIndex = 1;
            this.txtCurrency.Text = "Currency";
            // 
            // pressToFetch
            // 
            this.pressToFetch.Location = new System.Drawing.Point(89, 126);
            this.pressToFetch.Name = "pressToFetch";
            this.pressToFetch.Size = new System.Drawing.Size(75, 23);
            this.pressToFetch.TabIndex = 2;
            this.pressToFetch.Text = "Fetch Info";
            this.pressToFetch.UseVisualStyleBackColor = true;
            this.pressToFetch.Click += new System.EventHandler(this.pressToFetch_Click);
            // 
            // cryptoResult
            // 
            this.cryptoResult.AutoSize = true;
            this.cryptoResult.Location = new System.Drawing.Point(33, 176);
            this.cryptoResult.Name = "cryptoResult";
            this.cryptoResult.Size = new System.Drawing.Size(131, 13);
            this.cryptoResult.TabIndex = 3;
            this.cryptoResult.Text = "Result Printed in JSON file";
            // 
            // serviceDesc
            // 
            this.serviceDesc.AutoSize = true;
            this.serviceDesc.Location = new System.Drawing.Point(225, 136);
            this.serviceDesc.Name = "serviceDesc";
            this.serviceDesc.Size = new System.Drawing.Size(87, 13);
            this.serviceDesc.TabIndex = 4;
            this.serviceDesc.Text = "serviceOperation";
            // 
            // serviceURL
            // 
            this.serviceURL.AutoSize = true;
            this.serviceURL.Location = new System.Drawing.Point(225, 176);
            this.serviceURL.Name = "serviceURL";
            this.serviceURL.Size = new System.Drawing.Size(63, 13);
            this.serviceURL.TabIndex = 5;
            this.serviceURL.Text = "serviceURL";
            // 
            // methodOpReturn
            // 
            this.methodOpReturn.AutoSize = true;
            this.methodOpReturn.Location = new System.Drawing.Point(225, 216);
            this.methodOpReturn.Name = "methodOpReturn";
            this.methodOpReturn.Size = new System.Drawing.Size(88, 13);
            this.methodOpReturn.TabIndex = 6;
            this.methodOpReturn.Text = "methodOpReturn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 861);
            this.Controls.Add(this.methodOpReturn);
            this.Controls.Add(this.serviceURL);
            this.Controls.Add(this.serviceDesc);
            this.Controls.Add(this.cryptoResult);
            this.Controls.Add(this.pressToFetch);
            this.Controls.Add(this.txtCurrency);
            this.Controls.Add(this.txtCoinName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCoinName;
        private System.Windows.Forms.TextBox txtCurrency;
        private System.Windows.Forms.Button pressToFetch;
        private System.Windows.Forms.Label cryptoResult;
        private System.Windows.Forms.Label serviceDesc;
        private System.Windows.Forms.Label serviceURL;
        private System.Windows.Forms.Label methodOpReturn;
    }
}

