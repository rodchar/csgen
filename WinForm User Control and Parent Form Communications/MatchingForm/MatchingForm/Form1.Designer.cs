namespace MatchingForm
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
            this.ProductsPanel = new System.Windows.Forms.Panel();
            this.ReceiptItemNamesPanel = new System.Windows.Forms.Panel();
            this.btnLinkNames = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProductsPanel
            // 
            this.ProductsPanel.AutoSize = true;
            this.ProductsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProductsPanel.Location = new System.Drawing.Point(0, 0);
            this.ProductsPanel.Name = "ProductsPanel";
            this.ProductsPanel.Size = new System.Drawing.Size(200, 100);
            this.ProductsPanel.TabIndex = 0;
            // 
            // ReceiptItemNamesPanel
            // 
            this.ReceiptItemNamesPanel.AutoSize = true;
            this.ReceiptItemNamesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReceiptItemNamesPanel.Location = new System.Drawing.Point(234, 0);
            this.ReceiptItemNamesPanel.Name = "ReceiptItemNamesPanel";
            this.ReceiptItemNamesPanel.Size = new System.Drawing.Size(200, 100);
            this.ReceiptItemNamesPanel.TabIndex = 1;
            // 
            // btnLinkNames
            // 
            this.btnLinkNames.Location = new System.Drawing.Point(179, 160);
            this.btnLinkNames.Name = "btnLinkNames";
            this.btnLinkNames.Size = new System.Drawing.Size(75, 23);
            this.btnLinkNames.TabIndex = 0;
            this.btnLinkNames.Text = "Link Names";
            this.btnLinkNames.UseVisualStyleBackColor = true;
            this.btnLinkNames.Click += new System.EventHandler(this.btnLinkNames_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 349);
            this.Controls.Add(this.btnLinkNames);
            this.Controls.Add(this.ReceiptItemNamesPanel);
            this.Controls.Add(this.ProductsPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ProductsPanel;
        private System.Windows.Forms.Panel ReceiptItemNamesPanel;
        private System.Windows.Forms.Button btnLinkNames;
    }
}

