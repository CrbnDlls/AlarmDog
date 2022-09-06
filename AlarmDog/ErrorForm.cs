using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlarmDog
{
    /// <summary>
    /// Форма в которой отображается случившаяся ошибка
    /// </summary>
    class ErrorForm :Form
    {
        private Button buttonClose;
        private ListBox listBoxErrorMessage;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="ErrorMessage">Сообщение, которое необходимо показать</param>
        public ErrorForm(string[] ErrorMessage, string Header)
        {
            InitializeComponent();
            listBoxErrorMessage.Items.AddRange(ErrorMessage);
            Text = Header;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.listBoxErrorMessage = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonClose.Location = new System.Drawing.Point(0, 104);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(509, 20);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // listBoxErrorMessage
            // 
            this.listBoxErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxErrorMessage.ForeColor = System.Drawing.Color.Black;
            this.listBoxErrorMessage.FormattingEnabled = true;
            this.listBoxErrorMessage.ItemHeight = 20;
            this.listBoxErrorMessage.Location = new System.Drawing.Point(0, 0);
            this.listBoxErrorMessage.Name = "listBoxErrorMessage";
            this.listBoxErrorMessage.Size = new System.Drawing.Size(509, 104);
            this.listBoxErrorMessage.TabIndex = 1;
            // 
            // ErrorForm
            // 
            this.AcceptButton = this.buttonClose;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(509, 124);
            this.Controls.Add(this.listBoxErrorMessage);
            this.Controls.Add(this.buttonClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorForm";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }
    }
}
