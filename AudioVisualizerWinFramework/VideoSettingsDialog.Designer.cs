namespace AudioVisualizerWinFramework
{
    partial class VideoSettingsDialog
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.videoWidthNumberBox = new System.Windows.Forms.NumericUpDown();
            this.videoHeightNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fpsNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bitDepthNumberBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.videoWidthNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoHeightNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitDepthNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(223, 244);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(101, 40);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // confirmButton
            // 
            this.confirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.confirmButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.confirmButton.Location = new System.Drawing.Point(12, 244);
            this.confirmButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(101, 40);
            this.confirmButton.TabIndex = 2;
            this.confirmButton.Text = "OK";
            this.confirmButton.UseVisualStyleBackColor = true;
            // 
            // videoWidthNumberBox
            // 
            this.videoWidthNumberBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.videoWidthNumberBox.Location = new System.Drawing.Point(12, 32);
            this.videoWidthNumberBox.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.videoWidthNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.videoWidthNumberBox.Name = "videoWidthNumberBox";
            this.videoWidthNumberBox.Size = new System.Drawing.Size(120, 26);
            this.videoWidthNumberBox.TabIndex = 4;
            this.videoWidthNumberBox.Value = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.videoWidthNumberBox.ValueChanged += new System.EventHandler(this.VideoWidthNumberBox_ValueChanged);
            // 
            // videoHeightNumberBox
            // 
            this.videoHeightNumberBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.videoHeightNumberBox.Location = new System.Drawing.Point(12, 84);
            this.videoHeightNumberBox.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.videoHeightNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.videoHeightNumberBox.Name = "videoHeightNumberBox";
            this.videoHeightNumberBox.Size = new System.Drawing.Size(120, 26);
            this.videoHeightNumberBox.TabIndex = 5;
            this.videoHeightNumberBox.Value = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.videoHeightNumberBox.ValueChanged += new System.EventHandler(this.videoHeightNumberBox_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Height";
            // 
            // fpsNumberBox
            // 
            this.fpsNumberBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.fpsNumberBox.Location = new System.Drawing.Point(151, 32);
            this.fpsNumberBox.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.fpsNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fpsNumberBox.Name = "fpsNumberBox";
            this.fpsNumberBox.Size = new System.Drawing.Size(120, 26);
            this.fpsNumberBox.TabIndex = 8;
            this.fpsNumberBox.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.fpsNumberBox.ValueChanged += new System.EventHandler(this.fpsNumberBox_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Frames Per Second";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(147, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Bit Depth";
            // 
            // bitDepthNumberBox
            // 
            this.bitDepthNumberBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.bitDepthNumberBox.Location = new System.Drawing.Point(151, 84);
            this.bitDepthNumberBox.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.bitDepthNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.bitDepthNumberBox.Name = "bitDepthNumberBox";
            this.bitDepthNumberBox.Size = new System.Drawing.Size(120, 26);
            this.bitDepthNumberBox.TabIndex = 10;
            this.bitDepthNumberBox.Value = new decimal(new int[] {
            300000000,
            0,
            0,
            0});
            this.bitDepthNumberBox.ValueChanged += new System.EventHandler(this.bitDepthNumberBox_ValueChanged);
            // 
            // VideoSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 295);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bitDepthNumberBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fpsNumberBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.videoHeightNumberBox);
            this.Controls.Add(this.videoWidthNumberBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Name = "VideoSettingsDialog";
            this.Text = "Video Settings";
            ((System.ComponentModel.ISupportInitialize)(this.videoWidthNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoHeightNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitDepthNumberBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.NumericUpDown videoWidthNumberBox;
        private System.Windows.Forms.NumericUpDown videoHeightNumberBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown fpsNumberBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown bitDepthNumberBox;
    }
}