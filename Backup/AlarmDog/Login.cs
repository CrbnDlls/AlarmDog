using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Security;
using System.Runtime.InteropServices;

namespace AlarmDog
{
    public partial class Login : Form
    {
        private OracleConnection conn;
        private string User;
        private SecureString Pass;
        private string DataBase;

        public string Пользователь
        { get { return User; } }

        public SecureString Пароль
        { get { return Pass; } }

        public string БазаДанных
        { get { return DataBase; } }

        public OracleConnection СоединениеСБазой
        { get { return conn; } }
        
        public Login(string Database, string UserID, SecureString Password)
        {
            InitializeComponent();
            ShowInTaskbar = false;
            textBoxUserId.Text = UserID;
            if (Database != null)
            {
                textBoxDataBase.Text = Database;
            }
            Pass = new SecureString();
            if (!AutoConnection(Password))
            {
                ShowDialog();
            }
            
        }
        private bool AutoConnection(SecureString Password)
        {
            if (Password != null && Password.Length != 0)
            {
                Pass = Password;
                Connect();
                if (conn.State == ConnectionState.Open)
                {
                    User = textBoxUserId.Text;
                    DataBase = textBoxDataBase.Text;
                    /*Buttons butt = new Buttons(conn, Pass, textBoxUserId.Text, textBoxDataBase.Text);
                    butt.Show();*/
                    Dispose(true);
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private void Connect()
        {
            conn = new OracleConnection();
            OracleConnectionStringBuilder connString = new OracleConnectionStringBuilder();
            connString.DataSource = textBoxDataBase.Text;
            connString.UserID = textBoxUserId.Text;
            IntPtr hWndPass = Marshal.SecureStringToCoTaskMemAnsi(Pass);
            connString.Password = Marshal.PtrToStringAnsi(hWndPass, Pass.Length);
            Marshal.ZeroFreeCoTaskMemAnsi(hWndPass);
            conn.ConnectionString = connString.ConnectionString;
            connString.Password = "";
            try
            {
                conn.Open();
            }
            catch (OracleException e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            foreach (char sign in textBoxPassword.Text.ToCharArray())
            {
                Pass.AppendChar(sign);
            }
            textBoxPassword.Text = "xxxxxxxx";
            
            Connect();
            if (conn.State == ConnectionState.Open)
            {
                User = textBoxUserId.Text;
                DataBase = textBoxDataBase.Text;
                /*Buttons but = new Buttons(conn, Pass, User, textBoxDataBase.Text);
                but.Show();*/
                Dispose(true);
            }
            else
            {
                Pass.Clear();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }
    }
}
