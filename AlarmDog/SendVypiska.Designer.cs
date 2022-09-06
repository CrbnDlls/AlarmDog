namespace AlarmDog
{
    partial class SendVypiska
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendVypiska));
            this.propertyGridTaskDescription = new System.Windows.Forms.PropertyGrid();
            this.checkedListBoxTasks = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonExecuteAll = new System.Windows.Forms.Button();
            this.groupBoxTaskType = new System.Windows.Forms.GroupBox();
            this.labelSmtp = new System.Windows.Forms.Label();
            this.textBoxSmtp = new System.Windows.Forms.TextBox();
            this.labelSender = new System.Windows.Forms.Label();
            this.textBoxSender = new System.Windows.Forms.TextBox();
            this.buttonExecuteSelected = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxLetter = new System.Windows.Forms.TextBox();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.groupBoxAttachedFiles = new System.Windows.Forms.GroupBox();
            this.listBoxAttachedFiles = new System.Windows.Forms.ListBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel1.SuspendLayout();
            this.groupBoxTaskType.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxAttachedFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGridTaskDescription
            // 
            this.propertyGridTaskDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.propertyGridTaskDescription.Location = new System.Drawing.Point(0, 0);
            this.propertyGridTaskDescription.Name = "propertyGridTaskDescription";
            this.propertyGridTaskDescription.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGridTaskDescription.Size = new System.Drawing.Size(426, 222);
            this.propertyGridTaskDescription.TabIndex = 0;
            this.propertyGridTaskDescription.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridTaskDescription_PropertyValueChanged);
            // 
            // checkedListBoxTasks
            // 
            this.checkedListBoxTasks.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkedListBoxTasks.ForeColor = System.Drawing.Color.Black;
            this.checkedListBoxTasks.FormattingEnabled = true;
            this.checkedListBoxTasks.Location = new System.Drawing.Point(0, 82);
            this.checkedListBoxTasks.Name = "checkedListBoxTasks";
            this.checkedListBoxTasks.Size = new System.Drawing.Size(260, 379);
            this.checkedListBoxTasks.TabIndex = 1;
            this.checkedListBoxTasks.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxTasks_SelectedIndexChanged);
            this.checkedListBoxTasks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxTasks_ItemCheck);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonExecuteAll);
            this.panel1.Controls.Add(this.groupBoxTaskType);
            this.panel1.Controls.Add(this.buttonExecuteSelected);
            this.panel1.Controls.Add(this.buttonDelete);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(731, 82);
            this.panel1.TabIndex = 2;
            // 
            // buttonExecuteAll
            // 
            this.buttonExecuteAll.Location = new System.Drawing.Point(558, 45);
            this.buttonExecuteAll.Name = "buttonExecuteAll";
            this.buttonExecuteAll.Size = new System.Drawing.Size(161, 31);
            this.buttonExecuteAll.TabIndex = 7;
            this.buttonExecuteAll.Text = "Выполнить все отмеченные";
            this.buttonExecuteAll.UseVisualStyleBackColor = true;
            this.buttonExecuteAll.Click += new System.EventHandler(this.buttonExecuteAll_Click);
            // 
            // groupBoxTaskType
            // 
            this.groupBoxTaskType.Controls.Add(this.labelSmtp);
            this.groupBoxTaskType.Controls.Add(this.textBoxSmtp);
            this.groupBoxTaskType.Controls.Add(this.labelSender);
            this.groupBoxTaskType.Controls.Add(this.textBoxSender);
            this.groupBoxTaskType.Location = new System.Drawing.Point(93, 2);
            this.groupBoxTaskType.Name = "groupBoxTaskType";
            this.groupBoxTaskType.Size = new System.Drawing.Size(459, 76);
            this.groupBoxTaskType.TabIndex = 6;
            this.groupBoxTaskType.TabStop = false;
            this.groupBoxTaskType.Text = "Общие настройки";
            // 
            // labelSmtp
            // 
            this.labelSmtp.AutoSize = true;
            this.labelSmtp.Location = new System.Drawing.Point(6, 44);
            this.labelSmtp.Name = "labelSmtp";
            this.labelSmtp.Size = new System.Drawing.Size(175, 13);
            this.labelSmtp.TabIndex = 3;
            this.labelSmtp.Text = "Сервер исходящей почты (SMTP)";
            // 
            // textBoxSmtp
            // 
            this.textBoxSmtp.Location = new System.Drawing.Point(187, 41);
            this.textBoxSmtp.Name = "textBoxSmtp";
            this.textBoxSmtp.Size = new System.Drawing.Size(169, 20);
            this.textBoxSmtp.TabIndex = 2;
            this.textBoxSmtp.TextChanged += new System.EventHandler(this.textBoxSmtp_TextChanged);
            // 
            // labelSender
            // 
            this.labelSender.AutoSize = true;
            this.labelSender.Location = new System.Drawing.Point(74, 16);
            this.labelSender.Name = "labelSender";
            this.labelSender.Size = new System.Drawing.Size(103, 13);
            this.labelSender.TabIndex = 1;
            this.labelSender.Text = "E-Mail отправителя";
            // 
            // textBoxSender
            // 
            this.textBoxSender.Location = new System.Drawing.Point(187, 11);
            this.textBoxSender.Name = "textBoxSender";
            this.textBoxSender.Size = new System.Drawing.Size(169, 20);
            this.textBoxSender.TabIndex = 0;
            this.textBoxSender.TextChanged += new System.EventHandler(this.textBoxSender_TextChanged);
            // 
            // buttonExecuteSelected
            // 
            this.buttonExecuteSelected.Location = new System.Drawing.Point(558, 6);
            this.buttonExecuteSelected.Name = "buttonExecuteSelected";
            this.buttonExecuteSelected.Size = new System.Drawing.Size(161, 23);
            this.buttonExecuteSelected.TabIndex = 3;
            this.buttonExecuteSelected.Text = "Выполнить одно задание";
            this.buttonExecuteSelected.UseVisualStyleBackColor = true;
            this.buttonExecuteSelected.Click += new System.EventHandler(this.buttonExecuteSelected_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(12, 53);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 6);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 31);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(260, 82);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 388);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonDown);
            this.panel2.Controls.Add(this.buttonUp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(270, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(35, 388);
            this.panel2.TabIndex = 4;
            // 
            // buttonDown
            // 
            this.buttonDown.Image = global::AlarmDog.Properties.Resources.Down;
            this.buttonDown.Location = new System.Drawing.Point(3, 121);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(19, 101);
            this.buttonDown.TabIndex = 6;
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Image = global::AlarmDog.Properties.Resources.Up;
            this.buttonUp.Location = new System.Drawing.Point(3, 14);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(19, 101);
            this.buttonUp.TabIndex = 5;
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.splitter3);
            this.panel3.Controls.Add(this.groupBoxAttachedFiles);
            this.panel3.Controls.Add(this.splitter2);
            this.panel3.Controls.Add(this.propertyGridTaskDescription);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(305, 82);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(426, 388);
            this.panel3.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxLetter);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 312);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 76);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Текст сообщения";
            // 
            // textBoxLetter
            // 
            this.textBoxLetter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLetter.Location = new System.Drawing.Point(3, 16);
            this.textBoxLetter.Multiline = true;
            this.textBoxLetter.Name = "textBoxLetter";
            this.textBoxLetter.Size = new System.Drawing.Size(420, 57);
            this.textBoxLetter.TabIndex = 1;
            this.textBoxLetter.TextChanged += new System.EventHandler(this.textBoxLetter_TextChanged);
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Location = new System.Drawing.Point(0, 309);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(426, 3);
            this.splitter3.TabIndex = 4;
            this.splitter3.TabStop = false;
            // 
            // groupBoxAttachedFiles
            // 
            this.groupBoxAttachedFiles.Controls.Add(this.listBoxAttachedFiles);
            this.groupBoxAttachedFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxAttachedFiles.Location = new System.Drawing.Point(0, 225);
            this.groupBoxAttachedFiles.Name = "groupBoxAttachedFiles";
            this.groupBoxAttachedFiles.Size = new System.Drawing.Size(426, 84);
            this.groupBoxAttachedFiles.TabIndex = 3;
            this.groupBoxAttachedFiles.TabStop = false;
            this.groupBoxAttachedFiles.Text = "Присоединяемые файлы";
            // 
            // listBoxAttachedFiles
            // 
            this.listBoxAttachedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAttachedFiles.FormattingEnabled = true;
            this.listBoxAttachedFiles.Location = new System.Drawing.Point(3, 16);
            this.listBoxAttachedFiles.Name = "listBoxAttachedFiles";
            this.listBoxAttachedFiles.Size = new System.Drawing.Size(420, 56);
            this.listBoxAttachedFiles.TabIndex = 2;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 222);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(426, 3);
            this.splitter2.TabIndex = 2;
            this.splitter2.TabStop = false;
            // 
            // SendVypiska
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 470);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.checkedListBoxTasks);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SendVypiska";
            this.Text = "Настройки отправки выписок почтой";
            this.panel1.ResumeLayout(false);
            this.groupBoxTaskType.ResumeLayout(false);
            this.groupBoxTaskType.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxAttachedFiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGridTaskDescription;
        private System.Windows.Forms.CheckedListBox checkedListBoxTasks;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonExecuteSelected;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox groupBoxTaskType;
        private System.Windows.Forms.Button buttonExecuteAll;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Label labelSender;
        private System.Windows.Forms.TextBox textBoxSender;
        private System.Windows.Forms.Label labelSmtp;
        private System.Windows.Forms.TextBox textBoxSmtp;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TextBox textBoxLetter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.GroupBox groupBoxAttachedFiles;
        private System.Windows.Forms.ListBox listBoxAttachedFiles;
        

    }
}