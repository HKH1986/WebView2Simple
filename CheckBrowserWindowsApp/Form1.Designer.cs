namespace CheckBrowserWindowsApp
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
            this.webViewMain = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webViewMain)).BeginInit();
            this.SuspendLayout();
            // 
            // webViewMain
            // 
            this.webViewMain.AllowExternalDrop = true;
            this.webViewMain.CreationProperties = null;
            this.webViewMain.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewMain.Location = new System.Drawing.Point(0, 0);
            this.webViewMain.Name = "webViewMain";
            this.webViewMain.Size = new System.Drawing.Size(954, 613);
            this.webViewMain.TabIndex = 0;
            this.webViewMain.ZoomFactor = 1D;
            this.webViewMain.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webViewMain_CoreWebView2InitializationCompleted);
            this.webViewMain.WebMessageReceived += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs>(this.webViewMain_WebMessageReceived);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 521);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 80);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 613);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.webViewMain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webViewMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webViewMain;
        private System.Windows.Forms.Button button1;
    }
}

