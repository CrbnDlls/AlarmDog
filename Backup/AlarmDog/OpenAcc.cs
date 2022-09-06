using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data.OracleClient;
using System.IO;

namespace AlarmDog
{
    public partial class OpenAcc : Form
    {
        private OracleConnection conn;
        private LogFile log;
        private string LogName;

        public OpenAcc(OracleConnection Connection, string LogFileName)
        {
            InitializeComponent();
            conn = Connection;
            LoadData();
            ShowInTaskbar = false;
            SetRole("CUST001, SPECPARAM");
            log = new LogFile();
            LogName = LogFileName;
        }

        private void LoadData() 
        {
            dataGridViewAccounts.Rows.Add();
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            XmlNodeList nodelst = settings.SelectNodes("/Settings/GUDKU/Account");
            dataGridViewAccounts.Rows[0].Cells["ID"].Value = 0;
            dataGridViewAccounts.Rows[0].Cells["MFO"].Value = 820172;
            dataGridViewAccounts.Rows[0].Cells["Name"].Value = "ГУДКУ";
            for (int i = 1; i <= nodelst.Count; i++)
            {
                XmlNode node = settings.SelectSingleNode("/Settings/GUDKU[" + i + "]/Account");
                dataGridViewAccounts.Rows.Add();
                dataGridViewAccounts.Rows[i].Cells["ID"].Value = i;
                dataGridViewAccounts.Rows[i].Cells["MFO"].Value = node.InnerText.Substring(8, 6);
                node = settings.SelectSingleNode("/Settings/GUDKU[" + i + "]/Name");
                dataGridViewAccounts.Rows[i].Cells["Name"].Value = node.InnerText;
            }
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            if (dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value == null || dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value.ToString().Length != 8)
            {
                MessageBox.Show("Длинна счета должна быть равна 8","Ошибка");
                return;
            }

            if (dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value.ToString().Substring(0,4) != "3122")
            {
                MessageBox.Show("Балансовый должен быть 3122", "Ошибка");
                return;
            }

            string Account, IDS;
            CheckBit bit = new CheckBit();
            IDS = dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value.ToString().Substring(0, 4) + dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value.ToString().Substring(5);
            OracleCommand cmd5 = new OracleCommand("SELECT * FROM PEREKR_S WHERE IDS = " + IDS, conn);
            OracleDataReader reader = cmd5.ExecuteReader();
            if (!reader.HasRows)
            {
                MessageBox.Show("Такой схемы аккумуляции не существует " + IDS, "Ошибка");
                IDS = null;
            }

            
            for (int i = 1; i < dataGridViewAccounts.Rows.Count; i++)
            {
                Account = dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value.ToString() + dataGridViewAccounts.Rows[i].Cells["MFO"].Value.ToString();
                dataGridViewAccounts.Rows[i].Cells["ACCOUNT"].Value = bit.CalculateCheckBit(Account);
                dataGridViewAccounts.Rows[i].Cells["S230"].Value = dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value.ToString().Substring(5, 3);
                dataGridViewAccounts.Rows[i].Cells["KEKD"].Value = dataGridViewAccounts.Rows[0].Cells["KEKD"].Value;
                dataGridViewAccounts.Rows[i].Cells["KTK"].Value = dataGridViewAccounts.Rows[0].Cells["KTK"].Value;
                dataGridViewAccounts.Rows[i].Cells["KVK"].Value = dataGridViewAccounts.Rows[0].Cells["KVK"].Value;
                dataGridViewAccounts.Rows[i].Cells["IDG"].Value = 3;
                dataGridViewAccounts.Rows[i].Cells["IDS"].Value = IDS;
                dataGridViewAccounts.Rows[i].Cells["SPS"].Value = 1;
                if (dataGridViewAccounts.Rows[0].Cells["KEKD"].Value != null)
                {
                    dataGridViewAccounts.Rows[i].Cells["Name"].Value = dataGridViewAccounts.Rows[i].Cells["Name"].Value.ToString() + " " + dataGridViewAccounts.Rows[0].Cells["KEKD"].Value.ToString();
                }
                dataGridViewAccounts.Rows[i].Cells["OpenAccount"].Value = "Да";


            }
            if (dataGridViewAccounts.Rows[0].Cells["KEKD"].Value != null)
            {
                dataGridViewAccounts.Rows[0].Cells["Name"].Value = dataGridViewAccounts.Rows[0].Cells["Name"].Value.ToString() + " " + dataGridViewAccounts.Rows[0].Cells["KEKD"].Value.ToString();
            }
            Account = dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value.ToString();
            dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value = bit.CalculateCheckBit(Account);
            dataGridViewAccounts.Rows[0].Cells["OpenAccount"].Value = "Да";
            buttonOpenAcc.Enabled = true;
        }

        private void buttonOpenAcc_Click(object sender, EventArgs e)
        {
            if (!CheckAcc0())
            { dataGridViewAccounts.Rows[0].Cells["OpenAccount"].Value = "Нет"; }

            for (int i = 1; i < dataGridViewAccounts.Rows.Count; i++)
            {
                if (!CheckAcc(i))
                { dataGridViewAccounts.Rows[i].Cells["OpenAccount"].Value = "Нет"; }
            }           

            for (int i = 0; i < dataGridViewAccounts.Rows.Count; i++)
            {
                OpenAccounts(i);
            }
        }
                
        private bool CheckAcc0()
        {
            if (dataGridViewAccounts.Rows[0].Cells["OpenAccount"].Value.ToString() == "Да")
            {
                if (dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value == null || dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value.ToString().Length != 8)
                {
                    MessageBox.Show("Длинна счета должна быть равна 8 (0)", "Ошибка");
                    return false;
                }

                if (dataGridViewAccounts.Rows[0].Cells["ACCOUNT"].Value.ToString().Substring(0, 4) != "3122")
                {
                    MessageBox.Show("Балансовый должен быть 3122 (0)", "Ошибка");
                    return false;
                }

                if (dataGridViewAccounts.Rows[0].Cells["Name"].Value == null || dataGridViewAccounts.Rows[0].Cells["Name"].Value.ToString().Length == 0)
                {
                    MessageBox.Show("Не заполнено название счета (0)", "Ошибка");
                    return false;
                }
                if (dataGridViewAccounts.Rows[0].Cells["IDS"].Value != null)
                {
                    OracleCommand cmd3 = new OracleCommand("SELECT * FROM PEREKR_S WHERE IDS = " + dataGridViewAccounts.Rows[0].Cells["IDS"].Value.ToString(), conn);
                    OracleDataReader reader = cmd3.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Такой схемы аккумуляции не существует " + dataGridViewAccounts.Rows[0].Cells["IDS"].Value, "Ошибка");
                        return false;
                    }
                }
                if (dataGridViewAccounts.Rows[0].Cells["IDG"].Value != null)
                {
                    OracleCommand cmd3 = new OracleCommand("SELECT * FROM PEREKR_G WHERE IDG = " + dataGridViewAccounts.Rows[0].Cells["IDG"].Value.ToString(), conn);
                    OracleDataReader reader = cmd3.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Такой группы аккумуляции не существует " + dataGridViewAccounts.Rows[0].Cells["IDG"].Value, "Ошибка");
                        return false;
                    }
                }
                return true;
            }
            return true;
        }
        private bool CheckAcc(int i)
        {
            
                if (dataGridViewAccounts.Rows[i].Cells["OpenAccount"].Value.ToString() == "Да")
                {
                    if (dataGridViewAccounts.Rows[i].Cells["IDS"].Value != null)
                    {
                        OracleCommand cmd2 = new OracleCommand("SELECT * FROM PEREKR_S WHERE IDS = " + dataGridViewAccounts.Rows[i].Cells["IDS"].Value.ToString(), conn);
                        OracleDataReader reader = cmd2.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Такой схемы аккумуляции не существует " + dataGridViewAccounts.Rows[i].Cells["IDS"].Value, "Ошибка");
                            return false;
                        }
                    }
                    if (dataGridViewAccounts.Rows[i].Cells["IDG"].Value != null)
                    {
                        OracleCommand cmd3 = new OracleCommand("SELECT * FROM PEREKR_G WHERE IDG = " + dataGridViewAccounts.Rows[i].Cells["IDG"].Value.ToString(), conn);
                        OracleDataReader reader = cmd3.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Такой группы аккумуляции не существует " + dataGridViewAccounts.Rows[i].Cells["IDG"].Value, "Ошибка");
                            return false;
                        }
                    }
                    if (dataGridViewAccounts.Rows[i].Cells["ACCOUNT"].Value == null || dataGridViewAccounts.Rows[i].Cells["ACCOUNT"].Value.ToString().Length != 14)
                    {
                        MessageBox.Show("Длинна счета должна быть равна 14 (" + i + ")", "Ошибка");
                        return false;
                    }

                    if (dataGridViewAccounts.Rows[i].Cells["ACCOUNT"].Value.ToString().Substring(0, 4) != "3122")
                    {
                        MessageBox.Show("Балансовый должен быть 3122 (" + i + ")", "Ошибка");
                        return false;
                    }

                    if (dataGridViewAccounts.Rows[i].Cells["Name"].Value == null || dataGridViewAccounts.Rows[i].Cells["Name"].Value.ToString().Length == 0)
                    {
                        MessageBox.Show("Не заполнено название счета (" + i + ")", "Ошибка");
                        return false;
                    }
                    return true;
                }
                return true;
            
        }
        private void OpenAccounts(int i)
        {
            if (dataGridViewAccounts.Rows[i].Cells["OpenAccount"].Value.ToString() == "Да")
            {
                string S230, KEKD, KTK, KVK, IDG, IDS, SPS, ACC;
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter par;

                cmd.CommandText = "BARS.op_reg_lock";
                par = cmd.CreateParameter();
                par.ParameterName = "mod_";
                par.OracleType = OracleType.Int16;
                par.Size = 0;
                par.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(par);
                par = cmd.CreateParameter();
                par.ParameterName = "p1_";
                par.OracleType = OracleType.Int32;
                par.Size = 0;
                par.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(par);
                par = cmd.CreateParameter();
                par.ParameterName = "p2_";
                par.OracleType = OracleType.Int16;
                par.Size = 0;
                par.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(par);
                par = cmd.CreateParameter();
                par.ParameterName = "p3_";
                par.OracleType = OracleType.Int16;
                par.Size = 0;
                par.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(par);
                par = cmd.CreateParameter();
                par.ParameterName = "p4_";
                par.OracleType = OracleType.Int16;
                par.Size = 0;
                par.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(par);
                par = cmd.CreateParameter();
                par.ParameterName = "rnk_";
                par.OracleType = OracleType.Int16;
                par.Size = 0;
                par.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(par);
                par = cmd.CreateParameter();
                par.ParameterName = "p_nls_";
                par.OracleType = OracleType.VarChar;
                par.Size = 0;
                par.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(par);
                par = cmd.CreateParameter();
                par.ParameterName = "kv_";
                par.OracleType = OracleType.Int16;
                par.Size = 0;
                par.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(par);
                par = cmd.CreateParameter();
                par.ParameterName = "nms_";
                par.OracleType = OracleType.VarChar;
                par.Size = 0;
                par.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(par);
                par = cmd.CreateParameter();
                par.ParameterName = "tip_";
                par.OracleType = OracleType.Char;
                par.Size = 0;
                par.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(par);
                par = cmd.CreateParameter();
                par.ParameterName = "isp_";
                par.OracleType = OracleType.Int16;
                par.Size = 0;
                par.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(par);
                par = cmd.CreateParameter();
                par.ParameterName = "accR_";
                par.OracleType = OracleType.Int32;
                par.Size = 0;
                par.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par);

                cmd.Parameters["mod_"].Value = 99;
                cmd.Parameters["p1_"].Value = 0;
                cmd.Parameters["p2_"].Value = 0;
                cmd.Parameters["p3_"].Value = 1;
                cmd.Parameters["p4_"].Value = 0;
                cmd.Parameters["rnk_"].Value = 1;
                cmd.Parameters["p_nls_"].Value = dataGridViewAccounts.Rows[i].Cells["ACCOUNT"].Value.ToString();
                cmd.Parameters["kv_"].Value = 980;
                cmd.Parameters["nms_"].Value = dataGridViewAccounts.Rows[i].Cells["Name"].Value.ToString();
                if (i == 0)
                {
                    cmd.Parameters["tip_"].Value = "KDB";
                }
                else
                {
                    cmd.Parameters["tip_"].Value = "DKZ";
                }
                cmd.Parameters["isp_"].Value = 503;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException oe)
                {
                    MessageBox.Show(oe.Message, "Ошибка");
                    log.WriteToLogFile(LogName, "Отказ в открытии счета " + dataGridViewAccounts.Rows[i].Cells["ACCOUNT"].Value.ToString());
                    log.WriteToLogFile(LogName, oe.Message);
                    return;
                }

                log.WriteToLogFile(LogName, "Счет " + dataGridViewAccounts.Rows[i].Cells["ACCOUNT"].Value.ToString() + " открыт");

                ACC = cmd.Parameters["accR_"].Value.ToString();

                if (dataGridViewAccounts.Rows[i].Cells["S230"].Value == null)
                {
                    S230 = "NULL";
                }
                else
                {
                    S230 = "'" + dataGridViewAccounts.Rows[i].Cells["S230"].Value.ToString() + "'";
                }
                if (dataGridViewAccounts.Rows[i].Cells["KEKD"].Value == null)
                {
                    KEKD = "NULL";
                }
                else
                {
                    KEKD = dataGridViewAccounts.Rows[i].Cells["KEKD"].Value.ToString();
                }
                if (dataGridViewAccounts.Rows[i].Cells["KTK"].Value == null)
                {
                    KTK = "NULL";
                }
                else
                {
                    KTK = dataGridViewAccounts.Rows[i].Cells["KTK"].Value.ToString();
                }
                if (dataGridViewAccounts.Rows[i].Cells["KVK"].Value == null)
                {
                    KVK = "NULL";
                }
                else
                {
                    KVK = dataGridViewAccounts.Rows[i].Cells["KVK"].Value.ToString();
                }
                if (dataGridViewAccounts.Rows[i].Cells["IDG"].Value == null)
                {
                    IDG = "NULL";
                }
                else
                {
                    IDG = dataGridViewAccounts.Rows[i].Cells["IDG"].Value.ToString();
                }
                if (dataGridViewAccounts.Rows[i].Cells["IDS"].Value == null)
                {
                    IDS = "NULL";
                }
                else
                {
                    IDS = dataGridViewAccounts.Rows[i].Cells["IDS"].Value.ToString();
                }
                if (dataGridViewAccounts.Rows[i].Cells["SPS"].Value == null)
                {
                    SPS = "NULL";
                }
                else
                {
                    SPS = dataGridViewAccounts.Rows[i].Cells["SPS"].Value.ToString();
                }
                OracleCommand cmd1 = new OracleCommand("UPDATE BARS.SPECPARAM SET S230 = " + S230 +
                                                                                ", KEKD = " + KEKD +
                                                                                ", KTK = " + KTK +
                                                                                ", KVK = " + KVK +
                                                                                ", IDG = " + IDG +
                                                                                ", IDS = " + IDS +
                                                                                ", SPS = " + SPS + " WHERE ACC = " + ACC, conn);
                try
                {
                    cmd1.ExecuteNonQuery();
                }
                catch (OracleException oe)
                {
                    MessageBox.Show(oe.Message, "Ошибка");
                    log.WriteToLogFile(LogName, "Отказ в внесении спецпараметров счета " + dataGridViewAccounts.Rows[i].Cells["ACCOUNT"].Value.ToString());
                    log.WriteToLogFile(LogName, oe.Message);
                    return;
                }

                
                dataGridViewAccounts.Rows[i].Cells["HasOpened"].Value = global::AlarmDog.Properties.Resources.Done;
                log.WriteToLogFile(LogName,"Внесены спецпараметры счета " + dataGridViewAccounts.Rows[i].Cells["ACCOUNT"].Value.ToString());     
                

            }
            else 
            {
                dataGridViewAccounts.Rows[i].Cells["HasOpened"].Value = global::AlarmDog.Properties.Resources.NotDone;
            }
        }

        private void SetRole(string NewRoles)
        {
            string ROLES = "";
            OracleCommand cmd = new OracleCommand("SELECT granted_role from user_role_privs where username = user and default_role = 'YES'", conn);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ROLES = ROLES + ", " + reader.GetValue(0).ToString();
            }
            if (NewRoles == null)
            {
                cmd.CommandText = "SET ROLE " + ROLES.Substring(2);
            }
            else
            {
                cmd.CommandText = "SET ROLE " + ROLES.Substring(2) + ", " + NewRoles;
            }
            cmd.ExecuteNonQuery();
        }

        private void OpenAcc_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetRole(null);
        }
    }
}
