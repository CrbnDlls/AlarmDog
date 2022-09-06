using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Xml;
using System.Net.Mail;
using System.Security;

namespace AlarmDog
{
    class SendMail
    {
        private OracleConnection conn;
        private string From;
        private string To;
        private string[] Recievers;
        private string[] Error;
        private string SMTP;

        public string Получатели
        { get { return To; } }
        public string[] Ошибка
        { get { return Error; } }
        
        public SendMail(OracleConnection Connection)
        {
            Error = Load();
            if (Error[0] == "Ok")
            {
                conn = Connection;

                if (conn.State == ConnectionState.Open)
                {
                    string CorrAccValue = GetCorAccValue();
                    SmtpClient MailClient = new SmtpClient(SMTP);
                    MailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    char[] separator = new char[1];
                    foreach (string Reciever in Recievers)
                    {
                        try
                        {
                            MailClient.Send(From, Reciever, "ACCOUNT", CorrAccValue);
                        }
                        catch (Exception e)
                        {
                            string[] temp = new string[2];
                            temp[0] = "Отправитель: " + From + " Получатель: " + Reciever;
                            temp[1] = e.Message;
                            Error = Error.Concat(temp).ToArray();
                        }
                    }
                }
                else
                {
                    Error[0] = "Отсутствует соединение с базой данных";
                }
            } 
        }

        private string[] Load()
        {
            string[] LoadError = new string[1];
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            XmlNode node = settings.SelectSingleNode("/Settings/SendMail/Enabled");
            if (node.InnerText != "Да")
            {
                LoadError[0] = "Отправка отключена в файле настроек";
                return LoadError;
            }
            node = settings.SelectSingleNode("/Settings/SendMail/From");
            From = node.InnerText;
            node = settings.SelectSingleNode("/Settings/SendMail/SMTP");
            SMTP = node.InnerText;
            XmlNodeList nodelst = settings.SelectNodes("/Settings/SendMail/To");
            To = nodelst[0].InnerText; 
            for (int i = 1; i < nodelst.Count; i++)
            {
                To = To + "," + nodelst[i].InnerText;
            }
            Recievers = new string[nodelst.Count];
            for (int i = 0; i < nodelst.Count; i++)
            {
                Recievers[i] = nodelst[i].InnerText;
            }
            LoadError[0] = "Ok";
            return LoadError;
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
    }
}
