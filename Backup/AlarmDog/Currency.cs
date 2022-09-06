using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Xml;
using System.Security;

namespace AlarmDog
{
    /// <summary>
    /// Форма в которой реализовано получение значений кор. счета, валютного счета и доходов общего фонда по областям
    /// </summary>
    public class Currency : Form
    {
        private Button buttonClose;
        private DataGridView dataGridViewGUDKU;
        private Panel panel1;
        private Button buttonConnect;
        private Panel panel2;
        private Label labelCorAcc;
        private Label labelCurrency;
        private Label label1;
        private Label label2;
        private DataGridViewTextBoxColumn NAME;
        private DataGridViewTextBoxColumn ACCOUNT;
        private DataGridViewTextBoxColumn VALUE;
        private OracleConnection conn;
        private Button buttonSendMail;
        public string[] UP;
        private string LogName;
        public SecureString Password;
        public string UserName;

        private string GetCurrency()
        {
            string vCurrency;
            vCurrency = null;
            OracleCommand cmd = new OracleCommand("SELECT SUM(OSTQ) FROM ACCOUNTS WHERE NLS LIKE '4611%'", conn);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read() == true)
            {
                vCurrency = reader.GetValue(0).ToString();
            }

            if (vCurrency.Length > 2)
            {
                vCurrency = vCurrency.Insert(vCurrency.Length - 2, ".");
                for (int i = vCurrency.Length - 6; i >= 1; i = i - 3)
                { vCurrency = vCurrency.Insert(i, " "); }
            }
            else
            {
                if (vCurrency.Length == 1)
                {
                    vCurrency = "0.0" + vCurrency;
                }
                else
                {
                    vCurrency = "0." + vCurrency;
                }
            }
            
            return vCurrency;
        }
        /// <summary>
        /// Конструктор формы
        /// </summary>
        public Currency(OracleConnection Connection, string LogFileName)
        {
            InitializeComponent();
            LogName = LogFileName;
            conn = Connection;
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            XmlNodeList nodelst = settings.SelectNodes("/Settings/GUDKU");

            for (int i = 1; i <= nodelst.Count; i++)
            {
                XmlNode node = settings.SelectSingleNode("/Settings/GUDKU[" + i + "]/Name");
                string[] row = new string[2];
                row[0] = node.InnerText; 
                node = settings.SelectSingleNode("/Settings/GUDKU[" + i + "]/Account"); 
                row[1] = node.InnerText;
                this.dataGridViewGUDKU.Rows.Add(row);
            }
            buttonConnect.Select();
        }

        #region Код, автоматически созданный конструктором форм Windows
        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Currency));
            this.buttonClose = new System.Windows.Forms.Button();
            this.dataGridViewGUDKU = new System.Windows.Forms.DataGridView();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACCOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonSendMail = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCorAcc = new System.Windows.Forms.Label();
            this.labelCurrency = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGUDKU)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonClose.Location = new System.Drawing.Point(0, 31);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(424, 30);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // dataGridViewGUDKU
            // 
            this.dataGridViewGUDKU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGUDKU.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NAME,
            this.ACCOUNT,
            this.VALUE});
            this.dataGridViewGUDKU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewGUDKU.Location = new System.Drawing.Point(0, 127);
            this.dataGridViewGUDKU.Name = "dataGridViewGUDKU";
            this.dataGridViewGUDKU.RowTemplate.Height = 16;
            this.dataGridViewGUDKU.Size = new System.Drawing.Size(426, 513);
            this.dataGridViewGUDKU.TabIndex = 2;
            // 
            // NAME
            // 
            this.NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NAME.HeaderText = "Название ГУДКУ";
            this.NAME.Name = "NAME";
            this.NAME.ReadOnly = true;
            this.NAME.Width = 113;
            // 
            // ACCOUNT
            // 
            this.ACCOUNT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ACCOUNT.HeaderText = "№ Счета";
            this.ACCOUNT.Name = "ACCOUNT";
            this.ACCOUNT.ReadOnly = true;
            this.ACCOUNT.Width = 70;
            // 
            // VALUE
            // 
            this.VALUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.VALUE.DefaultCellStyle = dataGridViewCellStyle1;
            this.VALUE.HeaderText = "Получены ли доходы ?";
            this.VALUE.Name = "VALUE";
            this.VALUE.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonConnect);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 63);
            this.panel1.TabIndex = 3;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConnect.ForeColor = System.Drawing.Color.Green;
            this.buttonConnect.Location = new System.Drawing.Point(0, 0);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(424, 31);
            this.buttonConnect.TabIndex = 7;
            this.buttonConnect.Text = "Обновить";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonSendMail
            // 
            this.buttonSendMail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonSendMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSendMail.ForeColor = System.Drawing.Color.Blue;
            this.buttonSendMail.Location = new System.Drawing.Point(0, 613);
            this.buttonSendMail.Name = "buttonSendMail";
            this.buttonSendMail.Size = new System.Drawing.Size(426, 27);
            this.buttonSendMail.TabIndex = 8;
            this.buttonSendMail.Text = "Послать SMS";
            this.buttonSendMail.UseVisualStyleBackColor = true;
            this.buttonSendMail.Click += new System.EventHandler(this.buttonSendMail_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.labelCorAcc);
            this.panel2.Controls.Add(this.labelCurrency);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(426, 64);
            this.panel2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Кор. счет:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Валюта:";
            // 
            // labelCorAcc
            // 
            this.labelCorAcc.AutoSize = true;
            this.labelCorAcc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCorAcc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelCorAcc.Location = new System.Drawing.Point(157, 35);
            this.labelCorAcc.Name = "labelCorAcc";
            this.labelCorAcc.Size = new System.Drawing.Size(45, 24);
            this.labelCorAcc.TabIndex = 1;
            this.labelCorAcc.Text = "0.00";
            // 
            // labelCurrency
            // 
            this.labelCurrency.AutoSize = true;
            this.labelCurrency.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCurrency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelCurrency.Location = new System.Drawing.Point(157, 2);
            this.labelCurrency.Name = "labelCurrency";
            this.labelCurrency.Size = new System.Drawing.Size(45, 24);
            this.labelCurrency.TabIndex = 0;
            this.labelCurrency.Text = "0.00";
            // 
            // Currency
            // 
            this.AcceptButton = this.buttonConnect;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(426, 640);
            this.Controls.Add(this.buttonSendMail);
            this.Controls.Add(this.dataGridViewGUDKU);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Currency";
            this.ShowInTaskbar = false;
            this.Text = "Валюта";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGUDKU)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion  

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                labelCurrency.Text = GetCurrency();
                labelCorAcc.Text = GetCorAccValue();
                string[] IncomeValues = GetIncomeValues();
                                
                for (int i = 0; i <= dataGridViewGUDKU.Rows.Count - 2; i++)
                {
                    dataGridViewGUDKU.Rows[i].Cells["VALUE"].Value = IncomeValues[i];
                    if (IncomeValues[i] == "0.00")
                    {
                        dataGridViewGUDKU.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridViewGUDKU.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }
             

        private string GetCorAccValue()
        {
            string CorAccValue;
            CorAccValue = null;
            
            OracleCommand cmd = new OracleCommand("SELECT OSTC FROM ACCOUNTS WHERE NLS = '111191'", conn);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read() == true)
            {
                CorAccValue = reader.GetValue(0).ToString();
            }

            if (CorAccValue.Length > 2)
            {
                CorAccValue = CorAccValue.Substring(1);
                CorAccValue = CorAccValue.Insert(CorAccValue.Length - 2, ".");
                for (int i = CorAccValue.Length - 6; i >= 1; i = i - 3)
                { CorAccValue = CorAccValue.Insert(i, " "); }
            }
            else
            {
                if (CorAccValue.Length == 1)
                {
                    CorAccValue = "0.0" + CorAccValue;
                }
                else
                {
                    CorAccValue = "0." + CorAccValue;
                }
            }
            

            return CorAccValue;
        }

        private string[] GetIncomeValues()
        {
            string[] IncomeValues;
            IncomeValues = new string[0];

            string IncomeValue;
            

            for (int i = 0; i <= dataGridViewGUDKU.Rows.Count - 2; i++)
            {
                IncomeValue = "0.00";
                OracleCommand cmd = new OracleCommand("SELECT KOS FROM ACCOUNTS WHERE NLS = '" + dataGridViewGUDKU.Rows[i].Cells["ACCOUNT"].Value.ToString() + "' AND to_char(DAPP,'ddmmyyyy') = to_char(SYSDATE,'ddmmyyyy')", conn);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read() == true)
                {
                    
                        IncomeValue = reader.GetValue(0).ToString();
                        if (IncomeValue.Length > 2)
                        {
                            IncomeValue = IncomeValue.Insert(IncomeValue.Length - 2, ".");
                            for (int r = IncomeValue.Length - 6; r >= 1; r = r - 3)
                            { IncomeValue = IncomeValue.Insert(r, " "); }
                        }
                        else
                        {
                            if (IncomeValue.Length == 1)
                            {
                                IncomeValue = "0.0" + IncomeValue;
                            }
                            else
                            {
                                IncomeValue = "0." + IncomeValue;
                            }
                        }
                    
                }
                IncomeValues = IncomeValues.Concat(IncomeValue.Split(("|").ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).ToArray();
                
            }
            
            return IncomeValues;
        }

        private void buttonSendMail_Click(object sender, EventArgs e)
        {
            
            SendMail send = new SendMail(conn);
            LogFile log = new LogFile();
            if (send.Ошибка[0] != "Ok")
            {
                MessageBox.Show(send.Ошибка[0], "Ошибка");
                log.WriteToLogFile(LogName, send.Ошибка[0]);
            }
            else 
            {
                if (send.Ошибка.Count() > 1)
                {
                    string[] eMessage = new string[send.Ошибка.Count() + 2];
                    eMessage[0] = "Выполнена ручная отправка сообщений по данным адресам:";
                    log.WriteToLogFile(LogName, "Выполнена ручная отправка сообщений по данным адресам:");
                    eMessage[1] = send.Получатели;
                    log.WriteToLogFile(LogName, send.Получатели);
                    eMessage[2] = "Cледующие сообщения не отправлены";
                    log.WriteToLogFile(LogName, "Cледующие сообщения не отправлены");
                    for (int i = 3; i < send.Ошибка.Count() + 2; i++)
                    {
                        eMessage[i] = send.Ошибка[i - 2];
                        log.WriteToLogFile(LogName, send.Ошибка[i - 2]);
                    }
                    ErrorForm error = new ErrorForm(eMessage, "Выполнена отправка");
                    error.ShowDialog();
                }
                else
                {
                    string[] eMessage = new string[2];
                    eMessage[0] = "Выполнена ручная отправка сообщений по данным адресам:";
                    log.WriteToLogFile(LogName, "Выполнена ручная отправка сообщений по данным адресам:");
                    eMessage[1] = send.Получатели;
                    log.WriteToLogFile(LogName, send.Получатели);
                    ErrorForm error = new ErrorForm(eMessage, "Выполнена отправка");
                    error.ShowDialog();
                    
                    
                }
            }
        }



        
    }
}
