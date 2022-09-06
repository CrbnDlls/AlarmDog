namespace AlarmDog
{
    partial class OpenAcc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenAcc));
            this.dataGridViewAccounts = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MFO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACCOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S230 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KEKD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KTK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KVK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.HasOpened = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonOpenAcc = new System.Windows.Forms.Button();
            this.buttonFill = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccounts)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewAccounts
            // 
            this.dataGridViewAccounts.AllowUserToAddRows = false;
            this.dataGridViewAccounts.AllowUserToDeleteRows = false;
            this.dataGridViewAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.MFO,
            this.NAME,
            this.ACCOUNT,
            this.S230,
            this.KEKD,
            this.KTK,
            this.KVK,
            this.IDG,
            this.IDS,
            this.SPS,
            this.OpenAccount,
            this.HasOpened});
            this.dataGridViewAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewAccounts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewAccounts.Location = new System.Drawing.Point(0, 70);
            this.dataGridViewAccounts.Name = "dataGridViewAccounts";
            this.dataGridViewAccounts.Size = new System.Drawing.Size(909, 421);
            this.dataGridViewAccounts.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.ID.HeaderText = "№";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ID.ToolTipText = "Номер по порядку";
            this.ID.Width = 5;
            // 
            // MFO
            // 
            this.MFO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.MFO.HeaderText = "МФО";
            this.MFO.Name = "MFO";
            this.MFO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MFO.ToolTipText = "МФО";
            this.MFO.Width = 5;
            // 
            // NAME
            // 
            this.NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.NAME.HeaderText = "Наименование";
            this.NAME.Name = "NAME";
            this.NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NAME.ToolTipText = "Наименование счета";
            this.NAME.Width = 5;
            // 
            // ACCOUNT
            // 
            this.ACCOUNT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ACCOUNT.HeaderText = "Счет";
            this.ACCOUNT.Name = "ACCOUNT";
            this.ACCOUNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ACCOUNT.ToolTipText = "Номер счета";
            this.ACCOUNT.Width = 36;
            // 
            // S230
            // 
            this.S230.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.S230.HeaderText = "S230";
            this.S230.Name = "S230";
            this.S230.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.S230.ToolTipText = "Символ отчетности";
            this.S230.Width = 38;
            // 
            // KEKD
            // 
            this.KEKD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.KEKD.HeaderText = "KEKD";
            this.KEKD.Name = "KEKD";
            this.KEKD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KEKD.ToolTipText = "Код экономического вида деятельности";
            this.KEKD.Width = 42;
            // 
            // KTK
            // 
            this.KTK.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.KTK.HeaderText = "KTK";
            this.KTK.Name = "KTK";
            this.KTK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KTK.ToolTipText = "Код территории";
            this.KTK.Width = 34;
            // 
            // KVK
            // 
            this.KVK.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.KVK.HeaderText = "KVK";
            this.KVK.Name = "KVK";
            this.KVK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KVK.ToolTipText = "Код ведомственной классификации";
            this.KVK.Width = 34;
            // 
            // IDG
            // 
            this.IDG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.IDG.HeaderText = "IDG";
            this.IDG.Name = "IDG";
            this.IDG.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IDG.ToolTipText = "Группа аккумуляции";
            this.IDG.Width = 32;
            // 
            // IDS
            // 
            this.IDS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.IDS.HeaderText = "IDS";
            this.IDS.Name = "IDS";
            this.IDS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IDS.ToolTipText = "Схема аккумуляции";
            this.IDS.Width = 31;
            // 
            // SPS
            // 
            this.SPS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SPS.HeaderText = "SPS";
            this.SPS.Name = "SPS";
            this.SPS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SPS.Width = 34;
            // 
            // OpenAccount
            // 
            this.OpenAccount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OpenAccount.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.OpenAccount.HeaderText = "Открыть счет ?";
            this.OpenAccount.Items.AddRange(new object[] {
            "Да",
            "Нет"});
            this.OpenAccount.Name = "OpenAccount";
            this.OpenAccount.ToolTipText = "Открыть счет ?";
            // 
            // HasOpened
            // 
            this.HasOpened.HeaderText = "Счет открыт";
            this.HasOpened.Image = global::AlarmDog.Properties.Resources.NotDone;
            this.HasOpened.Name = "HasOpened";
            this.HasOpened.ReadOnly = true;
            this.HasOpened.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.HasOpened.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.HasOpened.ToolTipText = "Открылся ли счет ?";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonOpenAcc);
            this.panel1.Controls.Add(this.buttonFill);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(909, 70);
            this.panel1.TabIndex = 1;
            // 
            // buttonOpenAcc
            // 
            this.buttonOpenAcc.Enabled = false;
            this.buttonOpenAcc.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOpenAcc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonOpenAcc.Location = new System.Drawing.Point(148, 12);
            this.buttonOpenAcc.Name = "buttonOpenAcc";
            this.buttonOpenAcc.Size = new System.Drawing.Size(110, 52);
            this.buttonOpenAcc.TabIndex = 1;
            this.buttonOpenAcc.Text = "Открыть счета";
            this.buttonOpenAcc.UseVisualStyleBackColor = true;
            this.buttonOpenAcc.Click += new System.EventHandler(this.buttonOpenAcc_Click);
            // 
            // buttonFill
            // 
            this.buttonFill.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFill.ForeColor = System.Drawing.Color.ForestGreen;
            this.buttonFill.Location = new System.Drawing.Point(12, 12);
            this.buttonFill.Name = "buttonFill";
            this.buttonFill.Size = new System.Drawing.Size(110, 52);
            this.buttonFill.TabIndex = 0;
            this.buttonFill.Text = "Наполнить таблицу";
            this.buttonFill.UseVisualStyleBackColor = true;
            this.buttonFill.Click += new System.EventHandler(this.buttonFill_Click);
            // 
            // OpenAcc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 491);
            this.Controls.Add(this.dataGridViewAccounts);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpenAcc";
            this.Text = "Открытие счетов";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OpenAcc_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccounts)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAccounts;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonFill;
        private System.Windows.Forms.Button buttonOpenAcc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MFO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACCOUNT;
        private System.Windows.Forms.DataGridViewTextBoxColumn S230;
        private System.Windows.Forms.DataGridViewTextBoxColumn KEKD;
        private System.Windows.Forms.DataGridViewTextBoxColumn KTK;
        private System.Windows.Forms.DataGridViewTextBoxColumn KVK;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDG;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPS;
        private System.Windows.Forms.DataGridViewComboBoxColumn OpenAccount;
        private System.Windows.Forms.DataGridViewImageColumn HasOpened;
    }
}