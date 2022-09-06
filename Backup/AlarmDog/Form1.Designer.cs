namespace AlarmDog
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialogMusik = new System.Windows.Forms.OpenFileDialog();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.panelAccept = new System.Windows.Forms.Panel();
            this.labelAccept2 = new System.Windows.Forms.Label();
            this.buttonAccCancel = new System.Windows.Forms.Button();
            this.buttonAccOk = new System.Windows.Forms.Button();
            this.labelAccept1 = new System.Windows.Forms.Label();
            this.checkBoxMusik = new System.Windows.Forms.CheckBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.listBoxFoundFiles = new System.Windows.Forms.ListBox();
            this.propertyGridTasks = new System.Windows.Forms.PropertyGrid();
            this.comboBoxTasks = new System.Windows.Forms.ComboBox();
            this.panelStart_Musik = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAutoMail = new System.Windows.Forms.CheckBox();
            this.buttonMail = new System.Windows.Forms.Button();
            this.checkBoxVypiska = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoArchive = new System.Windows.Forms.CheckBox();
            this.buttonArchive = new System.Windows.Forms.Button();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelDataBase = new System.Windows.Forms.Label();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonCurrency = new System.Windows.Forms.Button();
            this.checkBoxUFiles = new System.Windows.Forms.CheckBox();
            this.panelProperties = new System.Windows.Forms.Panel();
            this.panelEd_Del_Task = new System.Windows.Forms.Panel();
            this.splitterIn_Out = new System.Windows.Forms.Splitter();
            this.timerUFiles = new System.Windows.Forms.Timer(this.components);
            this.timerMail = new System.Windows.Forms.Timer(this.components);
            this.panelAccept.SuspendLayout();
            this.panelStart_Musik.SuspendLayout();
            this.panelProperties.SuspendLayout();
            this.panelEd_Del_Task.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialogMusik
            // 
            this.openFileDialogMusik.AddExtension = false;
            this.openFileDialogMusik.AutoUpgradeEnabled = false;
            this.openFileDialogMusik.CheckFileExists = false;
            this.openFileDialogMusik.Filter = "Файлы мультимедиа|*.mp3;*.wav;*.wma";
            this.openFileDialogMusik.InitialDirectory = "c:\\";
            this.openFileDialogMusik.RestoreDirectory = true;
            this.openFileDialogMusik.Title = "Выберите звуковой файл";
            this.openFileDialogMusik.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogMusik_FileOk);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonAdd.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.buttonAdd.Location = new System.Drawing.Point(0, 0);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(35, 28);
            this.buttonAdd.TabIndex = 11;
            this.buttonAdd.Text = "+";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonDel.ForeColor = System.Drawing.Color.Red;
            this.buttonDel.Location = new System.Drawing.Point(35, 0);
            this.buttonDel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(35, 28);
            this.buttonDel.TabIndex = 12;
            this.buttonDel.Text = "-";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // panelAccept
            // 
            this.panelAccept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAccept.Controls.Add(this.labelAccept2);
            this.panelAccept.Controls.Add(this.buttonAccCancel);
            this.panelAccept.Controls.Add(this.buttonAccOk);
            this.panelAccept.Controls.Add(this.labelAccept1);
            this.panelAccept.Enabled = false;
            this.panelAccept.Location = new System.Drawing.Point(368, 49);
            this.panelAccept.Margin = new System.Windows.Forms.Padding(4);
            this.panelAccept.Name = "panelAccept";
            this.panelAccept.Size = new System.Drawing.Size(347, 204);
            this.panelAccept.TabIndex = 3;
            this.panelAccept.Visible = false;
            // 
            // labelAccept2
            // 
            this.labelAccept2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAccept2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAccept2.Location = new System.Drawing.Point(1, 70);
            this.labelAccept2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAccept2.Name = "labelAccept2";
            this.labelAccept2.Size = new System.Drawing.Size(340, 25);
            this.labelAccept2.TabIndex = 15;
            this.labelAccept2.Text = "label1";
            this.labelAccept2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonAccCancel
            // 
            this.buttonAccCancel.Location = new System.Drawing.Point(208, 134);
            this.buttonAccCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAccCancel.Name = "buttonAccCancel";
            this.buttonAccCancel.Size = new System.Drawing.Size(117, 34);
            this.buttonAccCancel.TabIndex = 14;
            this.buttonAccCancel.Text = "Нет";
            this.buttonAccCancel.UseVisualStyleBackColor = true;
            this.buttonAccCancel.Click += new System.EventHandler(this.buttonAccCancel_Click);
            // 
            // buttonAccOk
            // 
            this.buttonAccOk.Location = new System.Drawing.Point(17, 134);
            this.buttonAccOk.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAccOk.Name = "buttonAccOk";
            this.buttonAccOk.Size = new System.Drawing.Size(117, 34);
            this.buttonAccOk.TabIndex = 13;
            this.buttonAccOk.Text = "Да";
            this.buttonAccOk.UseVisualStyleBackColor = true;
            this.buttonAccOk.Click += new System.EventHandler(this.buttonAccOk_Click);
            // 
            // labelAccept1
            // 
            this.labelAccept1.AutoSize = true;
            this.labelAccept1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAccept1.Location = new System.Drawing.Point(64, 22);
            this.labelAccept1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAccept1.Name = "labelAccept1";
            this.labelAccept1.Size = new System.Drawing.Size(191, 25);
            this.labelAccept1.TabIndex = 0;
            this.labelAccept1.Text = "Вы хотите удалить";
            // 
            // checkBoxMusik
            // 
            this.checkBoxMusik.AutoSize = true;
            this.checkBoxMusik.Location = new System.Drawing.Point(201, 4);
            this.checkBoxMusik.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxMusik.Name = "checkBoxMusik";
            this.checkBoxMusik.Size = new System.Drawing.Size(271, 21);
            this.checkBoxMusik.TabIndex = 17;
            this.checkBoxMusik.Text = "Использовать звуковое оповещение";
            this.checkBoxMusik.UseVisualStyleBackColor = true;
            this.checkBoxMusik.Click += new System.EventHandler(this.checkBoxMusik_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonStart.Location = new System.Drawing.Point(4, 7);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(189, 58);
            this.buttonStart.TabIndex = 20;
            this.buttonStart.Text = "Старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipTitle = "AlarmDog";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "AlarmDog";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // listBoxFoundFiles
            // 
            this.listBoxFoundFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFoundFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxFoundFiles.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listBoxFoundFiles.FormattingEnabled = true;
            this.listBoxFoundFiles.ItemHeight = 31;
            this.listBoxFoundFiles.Location = new System.Drawing.Point(267, 0);
            this.listBoxFoundFiles.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxFoundFiles.Name = "listBoxFoundFiles";
            this.listBoxFoundFiles.Size = new System.Drawing.Size(797, 221);
            this.listBoxFoundFiles.TabIndex = 22;
            // 
            // propertyGridTasks
            // 
            this.propertyGridTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridTasks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.propertyGridTasks.Location = new System.Drawing.Point(0, 28);
            this.propertyGridTasks.Margin = new System.Windows.Forms.Padding(4);
            this.propertyGridTasks.Name = "propertyGridTasks";
            this.propertyGridTasks.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGridTasks.Size = new System.Drawing.Size(267, 200);
            this.propertyGridTasks.TabIndex = 23;
            this.propertyGridTasks.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridTasks_PropertyValueChanged);
            // 
            // comboBoxTasks
            // 
            this.comboBoxTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxTasks.FormattingEnabled = true;
            this.comboBoxTasks.Location = new System.Drawing.Point(70, 0);
            this.comboBoxTasks.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxTasks.Name = "comboBoxTasks";
            this.comboBoxTasks.Size = new System.Drawing.Size(197, 24);
            this.comboBoxTasks.TabIndex = 24;
            this.comboBoxTasks.SelectionChangeCommitted += new System.EventHandler(this.comboBoxTasks_SelectionChangeCommitted);
            this.comboBoxTasks.TextUpdate += new System.EventHandler(this.comboBoxTasks_TextUpdate);
            // 
            // panelStart_Musik
            // 
            this.panelStart_Musik.Controls.Add(this.label1);
            this.panelStart_Musik.Controls.Add(this.checkBoxAutoMail);
            this.panelStart_Musik.Controls.Add(this.buttonMail);
            this.panelStart_Musik.Controls.Add(this.checkBoxVypiska);
            this.panelStart_Musik.Controls.Add(this.checkBoxAutoArchive);
            this.panelStart_Musik.Controls.Add(this.buttonArchive);
            this.panelStart_Musik.Controls.Add(this.labelUserName);
            this.panelStart_Musik.Controls.Add(this.labelDataBase);
            this.panelStart_Musik.Controls.Add(this.buttonDisconnect);
            this.panelStart_Musik.Controls.Add(this.buttonCurrency);
            this.panelStart_Musik.Controls.Add(this.checkBoxUFiles);
            this.panelStart_Musik.Controls.Add(this.buttonStart);
            this.panelStart_Musik.Controls.Add(this.checkBoxMusik);
            this.panelStart_Musik.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStart_Musik.Location = new System.Drawing.Point(0, 228);
            this.panelStart_Musik.Margin = new System.Windows.Forms.Padding(4);
            this.panelStart_Musik.Name = "panelStart_Musik";
            this.panelStart_Musik.Size = new System.Drawing.Size(1064, 101);
            this.panelStart_Musik.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 17);
            this.label1.TabIndex = 33;
            this.label1.Text = "F1 - Вызов справки";
            // 
            // checkBoxAutoMail
            // 
            this.checkBoxAutoMail.AutoSize = true;
            this.checkBoxAutoMail.Enabled = false;
            this.checkBoxAutoMail.Location = new System.Drawing.Point(201, 41);
            this.checkBoxAutoMail.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxAutoMail.Name = "checkBoxAutoMail";
            this.checkBoxAutoMail.Size = new System.Drawing.Size(349, 21);
            this.checkBoxAutoMail.TabIndex = 32;
            this.checkBoxAutoMail.Text = "Включить автоматическую отправку сообщений";
            this.checkBoxAutoMail.UseVisualStyleBackColor = true;
            this.checkBoxAutoMail.Visible = false;
            this.checkBoxAutoMail.Click += new System.EventHandler(this.checkBoxAutoMail_Click);
            // 
            // buttonMail
            // 
            this.buttonMail.Enabled = false;
            this.buttonMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonMail.ForeColor = System.Drawing.Color.Goldenrod;
            this.buttonMail.Location = new System.Drawing.Point(661, 44);
            this.buttonMail.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMail.Name = "buttonMail";
            this.buttonMail.Size = new System.Drawing.Size(189, 30);
            this.buttonMail.TabIndex = 31;
            this.buttonMail.Text = "Почта";
            this.buttonMail.UseVisualStyleBackColor = true;
            this.buttonMail.Visible = false;
            this.buttonMail.Click += new System.EventHandler(this.buttonMail_Click);
            // 
            // checkBoxVypiska
            // 
            this.checkBoxVypiska.AutoSize = true;
            this.checkBoxVypiska.Enabled = false;
            this.checkBoxVypiska.Location = new System.Drawing.Point(201, 78);
            this.checkBoxVypiska.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxVypiska.Name = "checkBoxVypiska";
            this.checkBoxVypiska.Size = new System.Drawing.Size(465, 21);
            this.checkBoxVypiska.TabIndex = 30;
            this.checkBoxVypiska.Text = "Включить автоматическое создание выписки на \"Казна-Видатки\"";
            this.checkBoxVypiska.UseVisualStyleBackColor = true;
            this.checkBoxVypiska.Visible = false;
            this.checkBoxVypiska.CheckedChanged += new System.EventHandler(this.checkBoxVypiska_CheckedChanged);
            // 
            // checkBoxAutoArchive
            // 
            this.checkBoxAutoArchive.AutoSize = true;
            this.checkBoxAutoArchive.Enabled = false;
            this.checkBoxAutoArchive.Location = new System.Drawing.Point(201, 59);
            this.checkBoxAutoArchive.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxAutoArchive.Name = "checkBoxAutoArchive";
            this.checkBoxAutoArchive.Size = new System.Drawing.Size(310, 21);
            this.checkBoxAutoArchive.TabIndex = 29;
            this.checkBoxAutoArchive.Text = "Включить автоматическое архивирование";
            this.checkBoxAutoArchive.UseVisualStyleBackColor = true;
            this.checkBoxAutoArchive.Visible = false;
            this.checkBoxAutoArchive.Click += new System.EventHandler(this.checkBoxAutoArchive_Click);
            // 
            // buttonArchive
            // 
            this.buttonArchive.Enabled = false;
            this.buttonArchive.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonArchive.ForeColor = System.Drawing.Color.DarkBlue;
            this.buttonArchive.Location = new System.Drawing.Point(661, 7);
            this.buttonArchive.Margin = new System.Windows.Forms.Padding(4);
            this.buttonArchive.Name = "buttonArchive";
            this.buttonArchive.Size = new System.Drawing.Size(189, 30);
            this.buttonArchive.TabIndex = 26;
            this.buttonArchive.Text = "Архив";
            this.buttonArchive.UseVisualStyleBackColor = true;
            this.buttonArchive.Visible = false;
            this.buttonArchive.Click += new System.EventHandler(this.buttonArchive_Click);
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelUserName.ForeColor = System.Drawing.Color.Red;
            this.labelUserName.Location = new System.Drawing.Point(920, 75);
            this.labelUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(0, 24);
            this.labelUserName.TabIndex = 25;
            // 
            // labelDataBase
            // 
            this.labelDataBase.AutoSize = true;
            this.labelDataBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDataBase.ForeColor = System.Drawing.Color.Red;
            this.labelDataBase.Location = new System.Drawing.Point(795, 75);
            this.labelDataBase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDataBase.Name = "labelDataBase";
            this.labelDataBase.Size = new System.Drawing.Size(0, 24);
            this.labelDataBase.TabIndex = 24;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDisconnect.ForeColor = System.Drawing.Color.Sienna;
            this.buttonDisconnect.Location = new System.Drawing.Point(859, 44);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(189, 30);
            this.buttonDisconnect.TabIndex = 23;
            this.buttonDisconnect.Text = "Отключить";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Visible = false;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonCurrency
            // 
            this.buttonCurrency.Enabled = false;
            this.buttonCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCurrency.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonCurrency.Location = new System.Drawing.Point(859, 7);
            this.buttonCurrency.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCurrency.Name = "buttonCurrency";
            this.buttonCurrency.Size = new System.Drawing.Size(189, 30);
            this.buttonCurrency.TabIndex = 22;
            this.buttonCurrency.Text = "$$$";
            this.buttonCurrency.UseVisualStyleBackColor = true;
            this.buttonCurrency.Visible = false;
            this.buttonCurrency.Click += new System.EventHandler(this.buttonCurrency_Click);
            // 
            // checkBoxUFiles
            // 
            this.checkBoxUFiles.AutoSize = true;
            this.checkBoxUFiles.Enabled = false;
            this.checkBoxUFiles.Location = new System.Drawing.Point(201, 22);
            this.checkBoxUFiles.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxUFiles.Name = "checkBoxUFiles";
            this.checkBoxUFiles.Size = new System.Drawing.Size(399, 21);
            this.checkBoxUFiles.TabIndex = 21;
            this.checkBoxUFiles.Text = "Выполнять проверку поступления файлов в хранилище";
            this.checkBoxUFiles.UseVisualStyleBackColor = true;
            this.checkBoxUFiles.Visible = false;
            this.checkBoxUFiles.Click += new System.EventHandler(this.checkBoxUFiles_Click);
            // 
            // panelProperties
            // 
            this.panelProperties.Controls.Add(this.propertyGridTasks);
            this.panelProperties.Controls.Add(this.panelEd_Del_Task);
            this.panelProperties.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelProperties.Location = new System.Drawing.Point(0, 0);
            this.panelProperties.Margin = new System.Windows.Forms.Padding(4);
            this.panelProperties.Name = "panelProperties";
            this.panelProperties.Size = new System.Drawing.Size(267, 228);
            this.panelProperties.TabIndex = 26;
            // 
            // panelEd_Del_Task
            // 
            this.panelEd_Del_Task.Controls.Add(this.comboBoxTasks);
            this.panelEd_Del_Task.Controls.Add(this.buttonDel);
            this.panelEd_Del_Task.Controls.Add(this.buttonAdd);
            this.panelEd_Del_Task.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEd_Del_Task.Location = new System.Drawing.Point(0, 0);
            this.panelEd_Del_Task.Margin = new System.Windows.Forms.Padding(4);
            this.panelEd_Del_Task.Name = "panelEd_Del_Task";
            this.panelEd_Del_Task.Size = new System.Drawing.Size(267, 28);
            this.panelEd_Del_Task.TabIndex = 0;
            // 
            // splitterIn_Out
            // 
            this.splitterIn_Out.Location = new System.Drawing.Point(267, 0);
            this.splitterIn_Out.Margin = new System.Windows.Forms.Padding(4);
            this.splitterIn_Out.Name = "splitterIn_Out";
            this.splitterIn_Out.Size = new System.Drawing.Size(4, 228);
            this.splitterIn_Out.TabIndex = 27;
            this.splitterIn_Out.TabStop = false;
            // 
            // timerUFiles
            // 
            this.timerUFiles.Interval = 900000;
            this.timerUFiles.Tick += new System.EventHandler(this.timerUFiles_Tick);
            // 
            // timerMail
            // 
            this.timerMail.Interval = 60000;
            this.timerMail.Tick += new System.EventHandler(this.timerMail_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 329);
            this.Controls.Add(this.panelAccept);
            this.Controls.Add(this.splitterIn_Out);
            this.Controls.Add(this.listBoxFoundFiles);
            this.Controls.Add(this.panelProperties);
            this.Controls.Add(this.panelStart_Musik);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "AlarmDog";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panelAccept.ResumeLayout(false);
            this.panelAccept.PerformLayout();
            this.panelStart_Musik.ResumeLayout(false);
            this.panelStart_Musik.PerformLayout();
            this.panelProperties.ResumeLayout(false);
            this.panelEd_Del_Task.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogMusik;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Panel panelAccept;
        private System.Windows.Forms.Button buttonAccOk;
        private System.Windows.Forms.Label labelAccept1;
        private System.Windows.Forms.Button buttonAccCancel;
        private System.Windows.Forms.Label labelAccept2;
        private System.Windows.Forms.CheckBox checkBoxMusik;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ListBox listBoxFoundFiles;
        private System.Windows.Forms.PropertyGrid propertyGridTasks;
        private System.Windows.Forms.ComboBox comboBoxTasks;
        private System.Windows.Forms.Panel panelStart_Musik;
        private System.Windows.Forms.Panel panelProperties;
        private System.Windows.Forms.Panel panelEd_Del_Task;
        private System.Windows.Forms.Splitter splitterIn_Out;
        private System.Windows.Forms.Timer timerUFiles;
        private System.Windows.Forms.CheckBox checkBoxUFiles;
        private System.Windows.Forms.Button buttonCurrency;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label labelDataBase;
        private System.Windows.Forms.Button buttonArchive;
        private System.Windows.Forms.CheckBox checkBoxAutoArchive;
        private System.Windows.Forms.CheckBox checkBoxVypiska;
        private System.Windows.Forms.Button buttonMail;
        private System.Windows.Forms.Timer timerMail;
        private System.Windows.Forms.CheckBox checkBoxAutoMail;
        private System.Windows.Forms.Label label1;

    }
}

