namespace AlarmDog
{
    partial class ArchiveProcess
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
            if (base.InvokeRequired)
            {
                DisposeCallback d = new DisposeCallback(Dispose);
                Invoke(d, new object[] { disposing });
            }
            else
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }

                base.Dispose(disposing);
            }
            
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBarProcess = new System.Windows.Forms.ProgressBar();
            this.progressBarAllProcesses = new System.Windows.Forms.ProgressBar();
            this.labelProcessName = new System.Windows.Forms.Label();
            this.labelTotalProgress = new System.Windows.Forms.Label();
            this.labelProcPerc = new System.Windows.Forms.Label();
            this.labelTotalPerc = new System.Windows.Forms.Label();
            this.labelFromPath = new System.Windows.Forms.Label();
            this.labelToPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarProcess
            // 
            this.progressBarProcess.Location = new System.Drawing.Point(22, 62);
            this.progressBarProcess.Name = "progressBarProcess";
            this.progressBarProcess.Size = new System.Drawing.Size(464, 23);
            this.progressBarProcess.TabIndex = 0;
            // 
            // progressBarAllProcesses
            // 
            this.progressBarAllProcesses.Location = new System.Drawing.Point(22, 111);
            this.progressBarAllProcesses.Name = "progressBarAllProcesses";
            this.progressBarAllProcesses.Size = new System.Drawing.Size(464, 23);
            this.progressBarAllProcesses.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarAllProcesses.TabIndex = 1;
            // 
            // labelProcessName
            // 
            this.labelProcessName.AutoSize = true;
            this.labelProcessName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProcessName.Location = new System.Drawing.Point(22, 5);
            this.labelProcessName.Name = "labelProcessName";
            this.labelProcessName.Size = new System.Drawing.Size(50, 24);
            this.labelProcessName.TabIndex = 2;
            this.labelProcessName.Text = "label1";
            this.labelProcessName.UseCompatibleTextRendering = true;
            // 
            // labelTotalProgress
            // 
            this.labelTotalProgress.AutoSize = true;
            this.labelTotalProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTotalProgress.Location = new System.Drawing.Point(177, 88);
            this.labelTotalProgress.Name = "labelTotalProgress";
            this.labelTotalProgress.Size = new System.Drawing.Size(163, 20);
            this.labelTotalProgress.TabIndex = 3;
            this.labelTotalProgress.Text = "Прогресс архивации";
            // 
            // labelProcPerc
            // 
            this.labelProcPerc.AutoSize = true;
            this.labelProcPerc.Location = new System.Drawing.Point(496, 68);
            this.labelProcPerc.Name = "labelProcPerc";
            this.labelProcPerc.Size = new System.Drawing.Size(24, 13);
            this.labelProcPerc.TabIndex = 4;
            this.labelProcPerc.Text = "0 %";
            // 
            // labelTotalPerc
            // 
            this.labelTotalPerc.AutoSize = true;
            this.labelTotalPerc.Location = new System.Drawing.Point(496, 117);
            this.labelTotalPerc.Name = "labelTotalPerc";
            this.labelTotalPerc.Size = new System.Drawing.Size(24, 13);
            this.labelTotalPerc.TabIndex = 5;
            this.labelTotalPerc.Text = "0 %";
            // 
            // labelFromPath
            // 
            this.labelFromPath.AutoSize = true;
            this.labelFromPath.Location = new System.Drawing.Point(19, 29);
            this.labelFromPath.Name = "labelFromPath";
            this.labelFromPath.Size = new System.Drawing.Size(25, 13);
            this.labelFromPath.TabIndex = 6;
            this.labelFromPath.Text = "ИЗ:";
            // 
            // labelToPath
            // 
            this.labelToPath.AutoSize = true;
            this.labelToPath.Location = new System.Drawing.Point(19, 46);
            this.labelToPath.Name = "labelToPath";
            this.labelToPath.Size = new System.Drawing.Size(17, 13);
            this.labelToPath.TabIndex = 7;
            this.labelToPath.Text = "В:";
            // 
            // ArchiveProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 141);
            this.ControlBox = false;
            this.Controls.Add(this.labelToPath);
            this.Controls.Add(this.labelFromPath);
            this.Controls.Add(this.labelTotalPerc);
            this.Controls.Add(this.labelProcPerc);
            this.Controls.Add(this.labelTotalProgress);
            this.Controls.Add(this.labelProcessName);
            this.Controls.Add(this.progressBarAllProcesses);
            this.Controls.Add(this.progressBarProcess);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArchiveProcess";
            this.ShowInTaskbar = false;
            this.Text = "Идет процесс архивирования...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarProcess;
        private System.Windows.Forms.ProgressBar progressBarAllProcesses;
        private System.Windows.Forms.Label labelProcessName;
        private System.Windows.Forms.Label labelTotalProgress;
        private System.Windows.Forms.Label labelProcPerc;
        private System.Windows.Forms.Label labelTotalPerc;
        private System.Windows.Forms.Label labelFromPath;
        private System.Windows.Forms.Label labelToPath;
    }
}