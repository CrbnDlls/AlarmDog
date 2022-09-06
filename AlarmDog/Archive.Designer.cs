namespace AlarmDog
{
    partial class Archive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archive));
            this.propertyGridTaskDescription = new System.Windows.Forms.PropertyGrid();
            this.checkedListBoxTasks = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxExecute = new System.Windows.Forms.GroupBox();
            this.labelSelectedTaskName = new System.Windows.Forms.Label();
            this.labelSelectedTaskType = new System.Windows.Forms.Label();
            this.buttonExecuteAll = new System.Windows.Forms.Button();
            this.buttonExecuteSelected = new System.Windows.Forms.Button();
            this.groupBoxTaskType = new System.Windows.Forms.GroupBox();
            this.labelTaskType = new System.Windows.Forms.Label();
            this.trackBarTaskType = new System.Windows.Forms.TrackBar();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBoxExecute.SuspendLayout();
            this.groupBoxTaskType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTaskType)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGridTaskDescription
            // 
            this.propertyGridTaskDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridTaskDescription.Location = new System.Drawing.Point(467, 101);
            this.propertyGridTaskDescription.Margin = new System.Windows.Forms.Padding(4);
            this.propertyGridTaskDescription.Name = "propertyGridTaskDescription";
            this.propertyGridTaskDescription.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGridTaskDescription.Size = new System.Drawing.Size(500, 477);
            this.propertyGridTaskDescription.TabIndex = 0;
            this.propertyGridTaskDescription.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridTaskDescription_PropertyValueChanged);
            // 
            // checkedListBoxTasks
            // 
            this.checkedListBoxTasks.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkedListBoxTasks.FormattingEnabled = true;
            this.checkedListBoxTasks.Location = new System.Drawing.Point(0, 101);
            this.checkedListBoxTasks.Margin = new System.Windows.Forms.Padding(4);
            this.checkedListBoxTasks.Name = "checkedListBoxTasks";
            this.checkedListBoxTasks.Size = new System.Drawing.Size(407, 463);
            this.checkedListBoxTasks.TabIndex = 1;
            this.checkedListBoxTasks.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxTasks_SelectedIndexChanged);
            this.checkedListBoxTasks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxTasks_ItemCheck);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBoxExecute);
            this.panel1.Controls.Add(this.groupBoxTaskType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 101);
            this.panel1.TabIndex = 2;
            // 
            // groupBoxExecute
            // 
            this.groupBoxExecute.Controls.Add(this.labelSelectedTaskName);
            this.groupBoxExecute.Controls.Add(this.labelSelectedTaskType);
            this.groupBoxExecute.Controls.Add(this.buttonExecuteAll);
            this.groupBoxExecute.Controls.Add(this.buttonExecuteSelected);
            this.groupBoxExecute.Location = new System.Drawing.Point(4, 4);
            this.groupBoxExecute.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxExecute.Name = "groupBoxExecute";
            this.groupBoxExecute.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxExecute.Size = new System.Drawing.Size(535, 94);
            this.groupBoxExecute.TabIndex = 8;
            this.groupBoxExecute.TabStop = false;
            this.groupBoxExecute.Text = "Тип выбраного задания";
            // 
            // labelSelectedTaskName
            // 
            this.labelSelectedTaskName.AutoSize = true;
            this.labelSelectedTaskName.Location = new System.Drawing.Point(13, 53);
            this.labelSelectedTaskName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSelectedTaskName.Name = "labelSelectedTaskName";
            this.labelSelectedTaskName.Size = new System.Drawing.Size(0, 17);
            this.labelSelectedTaskName.TabIndex = 8;
            // 
            // labelSelectedTaskType
            // 
            this.labelSelectedTaskType.AutoSize = true;
            this.labelSelectedTaskType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSelectedTaskType.Location = new System.Drawing.Point(12, 20);
            this.labelSelectedTaskType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSelectedTaskType.Name = "labelSelectedTaskType";
            this.labelSelectedTaskType.Size = new System.Drawing.Size(0, 29);
            this.labelSelectedTaskType.TabIndex = 6;
            // 
            // buttonExecuteAll
            // 
            this.buttonExecuteAll.Location = new System.Drawing.Point(312, 48);
            this.buttonExecuteAll.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExecuteAll.Name = "buttonExecuteAll";
            this.buttonExecuteAll.Size = new System.Drawing.Size(215, 38);
            this.buttonExecuteAll.TabIndex = 7;
            this.buttonExecuteAll.Text = "Выполнить все отмеченные";
            this.buttonExecuteAll.UseVisualStyleBackColor = true;
            this.buttonExecuteAll.Click += new System.EventHandler(this.buttonExecuteAll_Click);
            // 
            // buttonExecuteSelected
            // 
            this.buttonExecuteSelected.Location = new System.Drawing.Point(312, 12);
            this.buttonExecuteSelected.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExecuteSelected.Name = "buttonExecuteSelected";
            this.buttonExecuteSelected.Size = new System.Drawing.Size(215, 28);
            this.buttonExecuteSelected.TabIndex = 3;
            this.buttonExecuteSelected.Text = "Выполнить одно задание";
            this.buttonExecuteSelected.UseVisualStyleBackColor = true;
            this.buttonExecuteSelected.Click += new System.EventHandler(this.buttonExecuteSelected_Click);
            // 
            // groupBoxTaskType
            // 
            this.groupBoxTaskType.Controls.Add(this.labelTaskType);
            this.groupBoxTaskType.Controls.Add(this.trackBarTaskType);
            this.groupBoxTaskType.Controls.Add(this.buttonAdd);
            this.groupBoxTaskType.Controls.Add(this.buttonDelete);
            this.groupBoxTaskType.Location = new System.Drawing.Point(547, 4);
            this.groupBoxTaskType.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxTaskType.Name = "groupBoxTaskType";
            this.groupBoxTaskType.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxTaskType.Size = new System.Drawing.Size(416, 94);
            this.groupBoxTaskType.TabIndex = 6;
            this.groupBoxTaskType.TabStop = false;
            this.groupBoxTaskType.Text = "Тип добовляемого задания";
            // 
            // labelTaskType
            // 
            this.labelTaskType.AutoSize = true;
            this.labelTaskType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTaskType.Location = new System.Drawing.Point(8, 20);
            this.labelTaskType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTaskType.Name = "labelTaskType";
            this.labelTaskType.Size = new System.Drawing.Size(219, 29);
            this.labelTaskType.TabIndex = 5;
            this.labelTaskType.Text = "Копировать файл";
            // 
            // trackBarTaskType
            // 
            this.trackBarTaskType.Location = new System.Drawing.Point(69, 53);
            this.trackBarTaskType.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarTaskType.Maximum = 5;
            this.trackBarTaskType.Name = "trackBarTaskType";
            this.trackBarTaskType.Size = new System.Drawing.Size(139, 56);
            this.trackBarTaskType.TabIndex = 4;
            this.trackBarTaskType.Scroll += new System.EventHandler(this.trackBarTaskType_Scroll);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(308, 11);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(100, 38);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(308, 63);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(100, 28);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(407, 101);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(13, 477);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonDown);
            this.panel2.Controls.Add(this.buttonUp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(420, 101);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(47, 477);
            this.panel2.TabIndex = 4;
            // 
            // buttonDown
            // 
            this.buttonDown.Image = global::AlarmDog.Properties.Resources.Down;
            this.buttonDown.Location = new System.Drawing.Point(4, 149);
            this.buttonDown.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(25, 124);
            this.buttonDown.TabIndex = 6;
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Image = global::AlarmDog.Properties.Resources.Up;
            this.buttonUp.Location = new System.Drawing.Point(4, 17);
            this.buttonUp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(25, 124);
            this.buttonUp.TabIndex = 5;
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // Archive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 578);
            this.Controls.Add(this.propertyGridTaskDescription);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.checkedListBoxTasks);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Archive";
            this.Text = "Настройки архивации";
            this.panel1.ResumeLayout(false);
            this.groupBoxExecute.ResumeLayout(false);
            this.groupBoxExecute.PerformLayout();
            this.groupBoxTaskType.ResumeLayout(false);
            this.groupBoxTaskType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTaskType)).EndInit();
            this.panel2.ResumeLayout(false);
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
        private System.Windows.Forms.TrackBar trackBarTaskType;
        private System.Windows.Forms.Label labelTaskType;
        private System.Windows.Forms.GroupBox groupBoxTaskType;
        private System.Windows.Forms.Button buttonExecuteAll;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.GroupBox groupBoxExecute;
        private System.Windows.Forms.Label labelSelectedTaskType;
        private System.Windows.Forms.Label labelSelectedTaskName;
        

    }
}