namespace AudioVisualizerWinFramework
{
    partial class RenderForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            settingsForm.Dispose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RenderForm
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(676, 455);
            this.Name = "RenderForm";
            this.ClientSizeChanged += new System.EventHandler(this.RenderForm_ClientSizeChanged);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RenderForm_MouseDoubleClick);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

