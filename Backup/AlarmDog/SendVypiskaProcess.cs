using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net.Mail;

namespace AlarmDog
{
    class SendVypiskaProcess
    {
        private string[] Messages;
        private string MailLogName;

        public string ЖурналАрхива
        {
            get { return MailLogName; }
        }

        public string[] СообщенияОВыполнении
        {
            get { return Messages; }
        }
        public SendVypiskaProcess(int[] iProcesses)
        {
            string SMTP, FromEmail;
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            LogFile log = new LogFile();
            log.ПутьиИмяФайла = log.GetLogFileName("yyyymmdd_mail.log");
            MailLogName = log.ПутьиИмяФайла;
            log.WriteToLogFile("Запущен процесс отправки сообщений");
            XmlNode node = settings.SelectSingleNode("Settings/SendMail/SMTP");
            SMTP = node.InnerText;
            log.WriteToLogFile("Сервер исходящей почты: " + SMTP);
            node = settings.SelectSingleNode("Settings/SendMail/From");
            FromEmail = node.InnerText;
            log.WriteToLogFile("Отправитель: " + FromEmail);
            XmlDocument SendVypiska = new XmlDocument();
            SendVypiska.Load("SendVypiska.xml");
            SmtpClient MailClient = new SmtpClient(SMTP);
            MailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            Messages = new string[iProcesses.Count()];
            int TaskNumber = 0;
            
            foreach (int Process in iProcesses)
            {
                string ToEmail, Subject, Letter;
                node = SendVypiska.SelectSingleNode("/SendVypiska/Task[" + Process + "]");
                log.WriteToLogFile("Название задания: " + node.Attributes["Name"].Value);
                Messages[TaskNumber] = node.Attributes["Name"].Value + ": ";
                node = SendVypiska.SelectSingleNode("/SendVypiska/Task[" + Process + "]/ToEmail");
                ToEmail = node.InnerText;
                log.WriteToLogFile("Получатель: " + node.InnerText);
                node = SendVypiska.SelectSingleNode("/SendVypiska/Task[" + Process + "]/Subject");
                Subject = node.InnerText;
                log.WriteToLogFile("Тема: " + node.InnerText);
                node = SendVypiska.SelectSingleNode("/SendVypiska/Task[" + Process + "]/Text");
                Letter = node.InnerText;
                log.WriteToLogFile("Текст сообщения: " + node.InnerText);
                MailMessage MlMessage = new MailMessage();
                try
                {
                    MailAddress address = new MailAddress(FromEmail);
                    MlMessage.From = address;
                }
                catch (Exception e)
                {
                    log.WriteToLogFile(FromEmail);
                    log.WriteToLogFile(e.Message);
                } 
                try
                {
                    MlMessage.To.Add(ToEmail);
                }
                catch (Exception e)
                {
                    log.WriteToLogFile(ToEmail);
                    log.WriteToLogFile(e.Message);
                } 
                MlMessage.Subject = Subject;
                MlMessage.Body = Letter;
                MlMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess;
                node = SendVypiska.SelectSingleNode("/SendVypiska/Task[" + Process + "]/Attach");
                log.WriteToLogFile("Присоединять файлы: " + node.InnerText);
                if (node.InnerText == "Да")
                {
                    string Dir, Filter;
                    node = SendVypiska.SelectSingleNode("/SendVypiska/Task[" + Process + "]/Dir");
                    Dir = Format(node.InnerText);
                    log.WriteToLogFile("Каталог: " + node.InnerText);
                    node = SendVypiska.SelectSingleNode("/SendVypiska/Task[" + Process + "]/Filter");
                    Filter = Format(node.InnerText);
                    log.WriteToLogFile("Имя файла: " + node.InnerText);
                    DirectorySearcher searcher = new DirectorySearcher();
                    searcher.SearchDirectory(Dir, Filter);
                    if (searcher.Ошибка == null && searcher.СписокФайлов != null)
                    {
                        foreach (string FileName in searcher.СписокФайлов)
                        {
                            log.WriteToLogFile("Присоединен файл: " + FileName);
                            Attachment Attach = new Attachment(FileName);
                            MlMessage.Attachments.Add(Attach);
                        }
                    }
                    else
                    {
                        if (searcher.Ошибка == null)
                        {
                            log.WriteToLogFile("Файлов для присоединения нет.");
                        }
                        else
                        {
                            foreach (string line in searcher.Ошибка)
                            {
                                log.WriteToLogFile(line);
                            } 
                        }
 
                    }
                }
                node = SendVypiska.SelectSingleNode("/SendVypiska/Task[" + Process + "]/AttachSend");
                log.WriteToLogFile("Отправлять только со вложениями: " + node.InnerText);
                if ((node.InnerText == "Да" & MlMessage.Attachments.Count > 0) | node.InnerText == "Нет")
                {
                    try
                    {
                        MailClient.Send(MlMessage);
                        log.WriteToLogFile("Отправка выполнена.");
                        Messages[TaskNumber] = Messages[TaskNumber] + "Отправка выполнена.";
                    }
                    catch (Exception e)
                    {
                        log.WriteToLogFile("Не могу выполнить отправку.");
                        log.WriteToLogFile(e.Message);
                        log.WriteToLogFile("Отправка не выполнена.");
                        Messages[TaskNumber] = Messages[TaskNumber] + "Отправка не выполнена.";
                    }
                }
                else
                {
                    log.WriteToLogFile("Отправка не выполнена, нет вложений.");
                    Messages[TaskNumber] = Messages[TaskNumber] + "Отправка не выполнена, нет вложений.";
                }
                TaskNumber = TaskNumber + 1;
                
            }
            
        }
        private string Format(string Path)
        {

            string Date, Month;

            if (DateTime.Now.Day.ToString().Length == 1)
            {
                Date = "0" + DateTime.Now.Day.ToString();
            }
            else
            {
                Date = DateTime.Now.Day.ToString();
            }
            if (DateTime.Now.Month.ToString().Length == 1)
            {
                Month = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                Month = DateTime.Now.Month.ToString();
            }

            Path = Path.Replace("dd", Date);
            Path = Path.Replace("mm", Month);
            Path = Path.Replace("yyyy", DateTime.Now.Year.ToString());
            Path = Path.Replace("yy", DateTime.Now.Year.ToString().Substring(2));

            return Path;
        }
    }
}
