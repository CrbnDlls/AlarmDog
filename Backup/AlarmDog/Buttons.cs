using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Data.OracleClient;
using System.Security;
using System.Xml;

namespace AlarmDog
{
    public partial class Buttons : Form
    {
        private string User, Base, LogName, Tehnol, BGener;
        private SecureString Pass;
        private OracleConnection conn;
        int AutoS;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, string lParam);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr ParentHwnd, IntPtr ChildHwnd, String lpClassName, String lpWindowName);
        
        [DllImport("user32.dll")]
        private static extern void EnableWindow(IntPtr Handle, bool OnOff);

        public Buttons(OracleConnection Connection, SecureString Password, string UserID, string Database, string LogFileName, int AutoStart)
        {
            InitializeComponent();
            User = UserID;
            Pass = Password;
            Base = Database;
            AutoS = AutoStart;
            LogName = LogFileName;
            labelBaseName3.Text = User;
            labelBaseName2.Text = Base;
            conn = Connection;
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            XmlNode node = settings.SelectSingleNode("/Settings/Buttons/Tehnol");
            if (node != null)
            {
                Tehnol = node.InnerText;
                buttonTechnol.Enabled = true;
                if (AutoS == 1)
                {
                    object sender = new object();
                    EventArgs e = new EventArgs();
                    buttonTechnol_Click(sender,e);
                }
            }
            
            node = settings.SelectSingleNode("Settings/Buttons/BGen");
            if (node != null)
            {
                BGener = node.InnerText;
                buttonBGenerator.Enabled = true;
            }
        }

        private void buttonTechnol_Click(object sender, EventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = Tehnol;
            proc.Start();
            IntPtr Technol;
            Technol = IntPtr.Zero;
            while (Technol == IntPtr.Zero)
            {
                Technol = FindWindow("TLoginForm", "Логин");
                Thread.Sleep(100);
            }
            IntPtr EditBox = IntPtr.Zero;
            while (EditBox == IntPtr.Zero)
            {
                EditBox = FindWindowEx(Technol, IntPtr.Zero, "TEdit", "Bars");
                Thread.Sleep(100);
            }
            
            SendMessage(EditBox, 0x000C, 0, User);

            EditBox = IntPtr.Zero;
            while (EditBox == IntPtr.Zero)
            {
                EditBox = FindWindowEx(Technol, IntPtr.Zero, "TEdit", "");
                Thread.Sleep(100);
            }
            IntPtr Password = Marshal.SecureStringToCoTaskMemAnsi(Pass);
            SendMessage(EditBox, 0x000C, 0, Marshal.PtrToStringAnsi(Password,Pass.Length));
            Marshal.ZeroFreeCoTaskMemAnsi(Password);

            EditBox = IntPtr.Zero;
            while (EditBox == IntPtr.Zero)
            {
                EditBox = FindWindowEx(Technol, IntPtr.Zero, "TEdit", "TREASURY");
                Thread.Sleep(100);
            }
            
            SendMessage(EditBox, 0x000C, 0, Base);


            IntPtr Butt = IntPtr.Zero;
            while (Butt == IntPtr.Zero)
            {
                Butt = FindWindowEx(Technol, IntPtr.Zero, "TBitBtn", "OK");
                Thread.Sleep(100);
            }
            SendMessage(Butt, 0x0201, 0, 0);
            SendMessage(Butt, 0x0202, 0, 0);

            if (AutoS == 1)
            {
                IntPtr MainForm = IntPtr.Zero;
                while (MainForm == IntPtr.Zero)
                {
                    MainForm = FindWindow("TMainForm", "Исполнение технологических процедур");
                    Thread.Sleep(100);
                }

                Butt = IntPtr.Zero;
                while (Butt == IntPtr.Zero)
                {
                    Butt = FindWindowEx(MainForm, IntPtr.Zero, "TBitBtn", "Поехали");
                    Thread.Sleep(100);
                }
                SendMessage(Butt, 0x0201, 0, 0);
                SendMessage(Butt, 0x0202, 0, 0);

                Dispose(true);
            }
       }

        private void buttonOpenAcc_Click(object sender, EventArgs e)
        {
            OpenAcc acc = new OpenAcc(conn, LogName);
            acc.ShowDialog();
        }

        private void Buttons_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Close();
        }

        private void buttonBGenerator_Click(object sender, EventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = BGener;
            proc.Start();
            IntPtr BGen;
            BGen = IntPtr.Zero;
            while (BGen == IntPtr.Zero)
            {
                BGen = FindWindow("TMainForm", "@B generator");
                Thread.Sleep(100);
            }
            IntPtr EditBox1 = IntPtr.Zero;
            while (EditBox1 == IntPtr.Zero)
            {
                EditBox1 = FindWindowEx(BGen, IntPtr.Zero, "TEdit", "");
                Thread.Sleep(100);
            }
            IntPtr Password = Marshal.SecureStringToCoTaskMemAnsi(Pass);
            SendMessage(EditBox1, 0x000C, 0, Marshal.PtrToStringAnsi(Password, Pass.Length));
            Marshal.ZeroFreeCoTaskMemAnsi(Password);
            

            IntPtr EditBox2 = IntPtr.Zero;
            while (EditBox2 == IntPtr.Zero)
            {
                EditBox2 = FindWindowEx(BGen, EditBox1, "TEdit", "");
                Thread.Sleep(100);
            }
            SendMessage(EditBox2, 0x000C, 0, User);

            IntPtr ComboBox = IntPtr.Zero;
            while (ComboBox == IntPtr.Zero)
            {
                ComboBox = FindWindowEx(BGen, IntPtr.Zero, "TComboBox", "");
                Thread.Sleep(100);
            }

            IntPtr Edit = IntPtr.Zero;
            while (Edit == IntPtr.Zero)
            {
                Edit = FindWindowEx(ComboBox, IntPtr.Zero, "Edit", "");
                Thread.Sleep(100);
            }
            SendMessage(Edit, 0x000C, 0, Base);
                       
        }

        private void buttonCurrency_Click(object sender, EventArgs e)
        {
            Currency curr = new Currency(conn, LogName);
            curr.ShowDialog();
            
        }
    }
}
