namespace AudioVisualizerWinFramework
{
    partial class SettingsForm
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.inputModeComboBox = new System.Windows.Forms.ComboBox();
            this.renderModeComboBox = new System.Windows.Forms.ComboBox();
            this.xScaleNumberBox = new System.Windows.Forms.NumericUpDown();
            this.yScaleNumberBox = new System.Windows.Forms.NumericUpDown();
            this.samplePowNumberBox = new System.Windows.Forms.NumericUpDown();
            this.smoothingNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.samplePowLabel = new System.Windows.Forms.Label();
            this.smoothingLabel = new System.Windows.Forms.Label();
            this.colorNamesListBox = new System.Windows.Forms.ListBox();
            this.colorsListBox = new System.Windows.Forms.ListBox();
            this.filePanel = new System.Windows.Forms.Panel();
            this.audioPlaybackPanel = new System.Windows.Forms.Panel();
            this.exportButton = new System.Windows.Forms.Button();
            this.songProgressBar = new System.Windows.Forms.ProgressBar();
            this.playButton = new System.Windows.Forms.Button();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.loadFileButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.xScaleNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yScaleNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.samplePowNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smoothingNumberBox)).BeginInit();
            this.filePanel.SuspendLayout();
            this.audioPlaybackPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(11, 10);
            this.Start.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(76, 32);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Stop
            // 
            this.Stop.Enabled = false;
            this.Stop.Location = new System.Drawing.Point(92, 10);
            this.Stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(85, 32);
            this.Stop.TabIndex = 1;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // inputModeComboBox
            // 
            this.inputModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputModeComboBox.FormattingEnabled = true;
            this.inputModeComboBox.Items.AddRange(new object[] {
            "Speaker Out",
            "Microphone In",
            "From File"});
            this.inputModeComboBox.Location = new System.Drawing.Point(182, 10);
            this.inputModeComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inputModeComboBox.Name = "inputModeComboBox";
            this.inputModeComboBox.Size = new System.Drawing.Size(164, 28);
            this.inputModeComboBox.TabIndex = 4;
            this.inputModeComboBox.SelectedIndexChanged += new System.EventHandler(this.InputModeComboBox_SelectedIndexChanged);
            // 
            // renderModeComboBox
            // 
            this.renderModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.renderModeComboBox.FormattingEnabled = true;
            this.renderModeComboBox.Location = new System.Drawing.Point(354, 10);
            this.renderModeComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.renderModeComboBox.Name = "renderModeComboBox";
            this.renderModeComboBox.Size = new System.Drawing.Size(164, 28);
            this.renderModeComboBox.TabIndex = 5;
            this.renderModeComboBox.SelectedIndexChanged += new System.EventHandler(this.RenderModeComboBox_SelectedIndexChanged);
            // 
            // xScaleNumberBox
            // 
            this.xScaleNumberBox.DecimalPlaces = 2;
            this.xScaleNumberBox.Location = new System.Drawing.Point(11, 66);
            this.xScaleNumberBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xScaleNumberBox.Name = "xScaleNumberBox";
            this.xScaleNumberBox.Size = new System.Drawing.Size(162, 26);
            this.xScaleNumberBox.TabIndex = 6;
            // 
            // yScaleNumberBox
            // 
            this.yScaleNumberBox.DecimalPlaces = 2;
            this.yScaleNumberBox.Location = new System.Drawing.Point(11, 116);
            this.yScaleNumberBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.yScaleNumberBox.Name = "yScaleNumberBox";
            this.yScaleNumberBox.Size = new System.Drawing.Size(162, 26);
            this.yScaleNumberBox.TabIndex = 6;
            // 
            // samplePowNumberBox
            // 
            this.samplePowNumberBox.Location = new System.Drawing.Point(182, 66);
            this.samplePowNumberBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.samplePowNumberBox.Name = "samplePowNumberBox";
            this.samplePowNumberBox.Size = new System.Drawing.Size(162, 26);
            this.samplePowNumberBox.TabIndex = 7;
            // 
            // smoothingNumberBox
            // 
            this.smoothingNumberBox.Location = new System.Drawing.Point(184, 116);
            this.smoothingNumberBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.smoothingNumberBox.Name = "smoothingNumberBox";
            this.smoothingNumberBox.Size = new System.Drawing.Size(162, 26);
            this.smoothingNumberBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "X Scale";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Y Scale";
            // 
            // samplePowLabel
            // 
            this.samplePowLabel.AutoSize = true;
            this.samplePowLabel.Location = new System.Drawing.Point(182, 44);
            this.samplePowLabel.Name = "samplePowLabel";
            this.samplePowLabel.Size = new System.Drawing.Size(170, 20);
            this.samplePowLabel.TabIndex = 9;
            this.samplePowLabel.Text = "Sample Size Exponent";
            // 
            // smoothingLabel
            // 
            this.smoothingLabel.AutoSize = true;
            this.smoothingLabel.Location = new System.Drawing.Point(182, 94);
            this.smoothingLabel.Name = "smoothingLabel";
            this.smoothingLabel.Size = new System.Drawing.Size(86, 20);
            this.smoothingLabel.TabIndex = 10;
            this.smoothingLabel.Text = "Smoothing";
            // 
            // colorNamesListBox
            // 
            this.colorNamesListBox.FormattingEnabled = true;
            this.colorNamesListBox.ItemHeight = 20;
            this.colorNamesListBox.Location = new System.Drawing.Point(354, 46);
            this.colorNamesListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colorNamesListBox.Name = "colorNamesListBox";
            this.colorNamesListBox.Size = new System.Drawing.Size(162, 144);
            this.colorNamesListBox.TabIndex = 11;
            this.colorNamesListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ColorNamesListBox_MouseDoubleClick);
            // 
            // colorsListBox
            // 
            this.colorsListBox.BackColor = System.Drawing.SystemColors.Menu;
            this.colorsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.colorsListBox.FormattingEnabled = true;
            this.colorsListBox.ItemHeight = 14;
            this.colorsListBox.Location = new System.Drawing.Point(521, 46);
            this.colorsListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colorsListBox.Name = "colorsListBox";
            this.colorsListBox.Size = new System.Drawing.Size(27, 154);
            this.colorsListBox.TabIndex = 12;
            this.colorsListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ColorsListBox_DrawItem);
            this.colorsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ColorNamesListBox_MouseDoubleClick);
            // 
            // filePanel
            // 
            this.filePanel.Controls.Add(this.audioPlaybackPanel);
            this.filePanel.Controls.Add(this.fileNameLabel);
            this.filePanel.Controls.Add(this.loadFileButton);
            this.filePanel.Location = new System.Drawing.Point(11, 146);
            this.filePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filePanel.Name = "filePanel";
            this.filePanel.Size = new System.Drawing.Size(338, 157);
            this.filePanel.TabIndex = 13;
            // 
            // audioPlaybackPanel
            // 
            this.audioPlaybackPanel.Controls.Add(this.exportButton);
            this.audioPlaybackPanel.Controls.Add(this.songProgressBar);
            this.audioPlaybackPanel.Controls.Add(this.playButton);
            this.audioPlaybackPanel.Location = new System.Drawing.Point(3, 46);
            this.audioPlaybackPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.audioPlaybackPanel.Name = "audioPlaybackPanel";
            this.audioPlaybackPanel.Size = new System.Drawing.Size(337, 88);
            this.audioPlaybackPanel.TabIndex = 2;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(231, 46);
            this.exportButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(101, 40);
            this.exportButton.TabIndex = 5;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // songProgressBar
            // 
            this.songProgressBar.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.songProgressBar.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.songProgressBar.Location = new System.Drawing.Point(3, 14);
            this.songProgressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.songProgressBar.Name = "songProgressBar";
            this.songProgressBar.Size = new System.Drawing.Size(329, 27);
            this.songProgressBar.TabIndex = 4;
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(0, 45);
            this.playButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(40, 40);
            this.playButton.TabIndex = 3;
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileNameLabel.Location = new System.Drawing.Point(97, 4);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(125, 29);
            this.fileNameLabel.TabIndex = 1;
            this.fileNameLabel.Text = "File Name";
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(0, 0);
            this.loadFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(101, 42);
            this.loadFileButton.TabIndex = 0;
            this.loadFileButton.Text = "Open File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.LoadFileButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(559, 314);
            this.Controls.Add(this.filePanel);
            this.Controls.Add(this.colorsListBox);
            this.Controls.Add(this.colorNamesListBox);
            this.Controls.Add(this.smoothingLabel);
            this.Controls.Add(this.samplePowLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.smoothingNumberBox);
            this.Controls.Add(this.samplePowNumberBox);
            this.Controls.Add(this.yScaleNumberBox);
            this.Controls.Add(this.xScaleNumberBox);
            this.Controls.Add(this.renderModeComboBox);
            this.Controls.Add(this.inputModeComboBox);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SettingsForm";
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.xScaleNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yScaleNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.samplePowNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smoothingNumberBox)).EndInit();
            this.filePanel.ResumeLayout(false);
            this.filePanel.PerformLayout();
            this.audioPlaybackPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.ComboBox inputModeComboBox;
        private System.Windows.Forms.ComboBox renderModeComboBox;
        private System.Windows.Forms.NumericUpDown xScaleNumberBox;
        private System.Windows.Forms.NumericUpDown yScaleNumberBox;
        private System.Windows.Forms.NumericUpDown samplePowNumberBox;
        private System.Windows.Forms.NumericUpDown smoothingNumberBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label samplePowLabel;
        private System.Windows.Forms.Label smoothingLabel;
        private System.Windows.Forms.ListBox colorNamesListBox;
        private System.Windows.Forms.ListBox colorsListBox;
        private System.Windows.Forms.Panel filePanel;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.Panel audioPlaybackPanel;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.ProgressBar songProgressBar;
        private System.Windows.Forms.Button exportButton;
    }
}