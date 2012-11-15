namespace DetailViewExercise2
{
    partial class Main
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
            this.ucDetailView1 = new DetailViewExercise2.ucDetailView();
            this.SuspendLayout();
            // 
            // ucDetailView1
            // 
            this.ucDetailView1.DataSource = null;
            this.ucDetailView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucDetailView1.Location = new System.Drawing.Point(0, 0);
            this.ucDetailView1.MetaList = null;
            this.ucDetailView1.Name = "ucDetailView1";
            this.ucDetailView1.Size = new System.Drawing.Size(671, 406);
            this.ucDetailView1.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 468);
            this.Controls.Add(this.ucDetailView1);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private ucDetailView ucDetailView1;


    }
}