using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;

namespace AlarmDog
{
    public partial class SendVypiska : Form
    {
        string LogFileName;

        public SendVypiska(string LogName)
        {
            InitializeComponent();
            ToolTip tip_1 = new ToolTip();
            tip_1.SetToolTip(buttonAdd, "Добавить задание");
            ToolTip tip_2 = new ToolTip();
            tip_2.SetToolTip(buttonDelete, "Удалить задание");
            ToolTip tip_3 = new ToolTip();
            tip_3.SetToolTip(buttonDown, "Переместить задание ниже по порядку");
            ToolTip tip_4 = new ToolTip();
            tip_4.SetToolTip(buttonUp, "Переместить задание выше по порядку");
            ToolTip tip_5 = new ToolTip();
            tip_5.SetToolTip(buttonExecuteAll, "Выполнить все отмеченные задания");
            ToolTip tip_6 = new ToolTip();
            tip_6.SetToolTip(buttonExecuteSelected, "Выполнить выбранное задание");
            LoadData();
            if (checkedListBoxTasks.Items.Count != 0)
            {
                checkedListBoxTasks.SetSelected(0, true);
            }
            LogFileName = LogName;
        }

        private void LoadData()
        {
            if (!File.Exists("SendVypiska.xml"))
            {
                using (XmlWriter writer = XmlWriter.Create("SendVypiska.xml"))
                {
                    // Write XML data.
                    writer.WriteStartElement("SendVypiska");
                    writer.WriteEndElement();

                    writer.Flush();
                }
            }
            XmlDocument sendVypiska = new XmlDocument();
            sendVypiska.Load("SendVypiska.xml");
            XmlNodeList nodelst = sendVypiska.SelectNodes("/SendVypiska/Task");
            foreach (XmlNode node in nodelst)
            {
                if (node.Attributes["Enabled"].Value == "Да")
                {
                    checkedListBoxTasks.Items.Add(node.Attributes["Name"].Value, true);
                }
                else
                {
                    checkedListBoxTasks.Items.Add(node.Attributes["Name"].Value, false);
                }
            }
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            XmlNode node1 = settings.SelectSingleNode("/Settings/SendMail/From");
            textBoxSender.Text = node1.InnerText;
            node1 = settings.SelectSingleNode("/Settings/SendMail/SMTP");
            textBoxSmtp.Text = node1.InnerText;
 
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (checkedListBoxTasks.SelectedItem != null)
            {
                if (MessageBox.Show("Вы хотите удалить\r" + checkedListBoxTasks.SelectedItem.ToString() + " ?", "Подтверждение удаления задания", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int i = checkedListBoxTasks.SelectedIndex;
                    int[] iChecked = new int[checkedListBoxTasks.CheckedIndices.Count];
                    for (int l = 0; l < checkedListBoxTasks.CheckedIndices.Count; l++)
                    {
                        iChecked[l] = checkedListBoxTasks.CheckedIndices[l];
                    }
                    XmlDocument sendVypiska = new XmlDocument();
                    sendVypiska.Load("SendVypiska.xml");
                    XmlNode node = sendVypiska.SelectSingleNode("/SendVypiska");
                    node.RemoveChild(sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]"));
                    sendVypiska.Save("SendVypiska.xml");
                    checkedListBoxTasks.Items.Remove(checkedListBoxTasks.SelectedItem.ToString());

                    
                    for (int l = 0; l < checkedListBoxTasks.Items.Count; l++)
                    {
                        checkedListBoxTasks.SetItemChecked(l, false);
                    }

                    foreach (int indx in iChecked)
                    {
                        if (indx < i)
                        {
                            checkedListBoxTasks.SetItemChecked(indx, true);
                        }

                        if (indx > i)
                        {
                            checkedListBoxTasks.SetItemChecked(indx - 1, true);
                        }
                    }
                    
                    
                    if (checkedListBoxTasks.Items.Count == 0)
                    {
                        textBoxLetter.Text = null;
                        propertyGridTaskDescription.SelectedObject = null;
                        checkedListBoxTasks.SelectedIndex = i - 1;
                    }
                    else
                    {
                        if (i != 0)
                        {
                            checkedListBoxTasks.SelectedIndex = i - 1;
                        }
                        else
                        {
                            checkedListBoxTasks.SelectedIndex = i;
                        }
                        checkedListBoxTasks_SelectedIndexChanged(sender, e); 
                    }
                }
            }
            
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            checkedListBoxTasks.Items.Add("Название задания", false);
            XmlDocument sendVypiska = new XmlDocument();
            sendVypiska.Load("SendVypiska.xml");
            XmlNode node = sendVypiska.DocumentElement;
            XmlElement task = sendVypiska.CreateElement("Task");
            task.SetAttribute("Name", "Название задания");
            task.SetAttribute("Enabled", "Нет");
            XmlElement element = sendVypiska.CreateElement("ToEmail");
            task.AppendChild(element);
            element = sendVypiska.CreateElement("Dir");
            task.AppendChild(element);
            element = sendVypiska.CreateElement("Filter");
            task.AppendChild(element);
            element = sendVypiska.CreateElement("Subject");
            task.AppendChild(element);
            element = sendVypiska.CreateElement("Attach");
            element.InnerText = "Нет";
            task.AppendChild(element);
            element = sendVypiska.CreateElement("Schedule");
            XmlElement chldelement = sendVypiska.CreateElement("Day");
            chldelement.InnerText = "Понедельник";
            element.AppendChild(chldelement);
            task.AppendChild(element);
            element = sendVypiska.CreateElement("Time");
            element.InnerText = "08:50";
            task.AppendChild(element);
            element = sendVypiska.CreateElement("Text");
            task.AppendChild(element);
            element = sendVypiska.CreateElement("AttachSend");
            element.InnerText = "Да";
            task.AppendChild(element);
            node.AppendChild(task);
            sendVypiska.Save("SendVypiska.xml");
        }

        private void checkedListBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxAttachedFiles.Items.Clear();
            if (checkedListBoxTasks.SelectedIndex != -1)
            {
                XmlDocument sendVypiska = new XmlDocument();
                sendVypiska.Load("SendVypiska.xml");
                XmlNodeList nodelst = sendVypiska.SelectNodes("SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Schedule/Day");
                MailTask task = new MailTask(nodelst.Count);
                for (int i = 0; i < nodelst.Count; i++)
                {
                    if (nodelst[i].InnerText == "Понедельник")
                        task.Расписание[i] = Дней.Понедельник;
                    if (nodelst[i].InnerText == "Вторник")
                        task.Расписание[i] = Дней.Вторник;
                    if (nodelst[i].InnerText == "Среда")
                        task.Расписание[i] = Дней.Среда;
                    if (nodelst[i].InnerText == "Четверг")
                        task.Расписание[i] = Дней.Четверг;
                    if (nodelst[i].InnerText == "Пятница")
                        task.Расписание[i] = Дней.Пятница;
                }
                XmlNode node = sendVypiska.SelectSingleNode("SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                task.Название = node.Attributes["Name"].Value;
                node = sendVypiska.SelectSingleNode("SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Dir");
                task.Директория = node.InnerText;
                node = sendVypiska.SelectSingleNode("SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Filter");
                task.Маска = node.InnerText;
                node = sendVypiska.SelectSingleNode("SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/ToEmail");
                task.Получатель = node.InnerText;
                node = sendVypiska.SelectSingleNode("SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Subject");
                task.Тема = node.InnerText;
                node = sendVypiska.SelectSingleNode("SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Attach");
                if (node.InnerText == "Да")
                {
                    task.Присоединять = YesNo.Да;
                    DirectorySearcher searcher = new DirectorySearcher();
                    searcher.SearchDirectory(Format(task.Директория), Format(task.Маска));
                    if (searcher.Ошибка == null && searcher.СписокФайлов != null)
                    {
                        listBoxAttachedFiles.Items.AddRange(searcher.СписокФайлов);
                    }
                }
                if (node.InnerText == "Нет")
                    task.Присоединять = YesNo.Нет;
                node = sendVypiska.SelectSingleNode("SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Time");
                task.Время = TimeSpan.Parse(node.InnerText);
                node = sendVypiska.SelectSingleNode("SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/AttachSend");
                if (node.InnerText == "Да")
                    task.ОтправлятьСФайлами = YesNo.Да;
                if (node.InnerText == "Нет")
                    task.ОтправлятьСФайлами = YesNo.Нет;
                propertyGridTaskDescription.SelectedObject = task;
                node = sendVypiska.SelectSingleNode("SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Text");
                textBoxLetter.Text = node.InnerText;
                


            }
        }   
        
        private void propertyGridTaskDescription_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            XmlDocument sendVypiska = new XmlDocument();
            XmlNode node;
            sendVypiska.Load("SendVypiska.xml");
            

            if (propertyGridTaskDescription.SelectedGridItem.Label == "Название")
            {
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                node.Attributes["Name"].Value = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
                sendVypiska.Save("SendVypiska.xml");
                checkedListBoxTasks.Items[checkedListBoxTasks.SelectedIndex] = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }

            if (propertyGridTaskDescription.SelectedGridItem.Label == "E-mail получателя")
            {
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/ToEmail");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }

            if (propertyGridTaskDescription.SelectedGridItem.Label == "Директория")
            {
                
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Dir");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }

            if (propertyGridTaskDescription.SelectedGridItem.Label == "Имя файла")
            {
                
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Filter");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }

            if (propertyGridTaskDescription.SelectedGridItem.Label == "Присоединять файлы ?")
            {
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Attach");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
                
            }
            if (propertyGridTaskDescription.SelectedGridItem.Label == "Отправлять только со вложениями")
            {
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/AttachSend");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }
            if (propertyGridTaskDescription.SelectedGridItem.Label == "Тема")
            {
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Subject");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }
            if (propertyGridTaskDescription.SelectedGridItem.Label == "Время отправки")
            {
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Time");
                if (propertyGridTaskDescription.SelectedGridItem.Value.ToString().IndexOf(".") == -1)
                {
                    node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
                }
                else
                {
                    string Number = propertyGridTaskDescription.SelectedGridItem.Value.ToString().Substring(0, propertyGridTaskDescription.SelectedGridItem.Value.ToString().IndexOf("."));
                    
                    if (Number.Length >= 4)
                    {
                        string Number1 = Number.Substring(0, 2);
                        string Number2 = Number.Substring(2, 2);
                        if (Int32.Parse(Number1) > 23)
                        {
                            Number1 = Number.Substring(0, 1);
                            Number2 = Number.Substring(1, 2);
                        }
                        if (Int32.Parse(Number2) > 59)
                        {
                            Number2 = "59:59";
                            
                        }
                        else
                        {
                            Number2 = Number2 + ":00";
                        }
                        node.InnerText = Number1 + ":" + Number2;
                    }
                    if (Number.Length == 3)
                    {
                        string Number1 = "0" + Number.Substring(0, 1);
                        string Number2 = Number.Substring(1, 2);
                        if (Int32.Parse(Number2) > 59)
                        {
                            Number2 = "59:59";
                        }
                        else
                        {
                            Number2 = Number2 + ":00";
                        }
                        node.InnerText = Number1 + ":" + Number2;
                    }
                    if (Number.Length == 2)
                    {
                        string Number1 = Number.Substring(0, 2);
                        if (Int32.Parse(Number1) > 23)
                            Number1 = "23";
                        node.InnerText = Number1 + ":00:00";
                    }
                    if (Number.Length == 1)
                    {
                        node.InnerText = "0" + Number.Substring(0, 1) + ":00:00";
                    }
                }

            }
            if (propertyGridTaskDescription.SelectedGridItem.Label == "Расписание")
            {
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Schedule");
                node.RemoveAll();
                for (int i = 0; i < propertyGridTaskDescription.SelectedGridItem.GridItems.Count; i++)
                {
                    XmlElement element = sendVypiska.CreateElement("Day");
                    element.InnerText = propertyGridTaskDescription.SelectedGridItem.GridItems[i].Value.ToString();
                    node.AppendChild(element);
                }
            }
            if (propertyGridTaskDescription.SelectedGridItem.Parent.Label == "Расписание")
            {
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Schedule/Day[" + (Int32.Parse(propertyGridTaskDescription.SelectedGridItem.Label.Substring(1, 1)) + 1).ToString() + "]");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }
            sendVypiska.Save("SendVypiska.xml");
            EventArgs ev = new EventArgs();
            checkedListBoxTasks_SelectedIndexChanged(s, ev);
            
        }

        private void checkedListBoxTasks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxTasks.SelectedIndex != -1)
            {
                XmlDocument sendVypiska = new XmlDocument();
                sendVypiska.Load("SendVypiska.xml");
                XmlNode node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                if (checkedListBoxTasks.CheckedIndices.Contains(checkedListBoxTasks.SelectedIndex))
                {
                    node.Attributes["Enabled"].InnerText = "Нет";
                }
                else
                {
                    node.Attributes["Enabled"].InnerText = "Да";
                }
                sendVypiska.Save("SendVypiska.xml");
                

            }
        }

        private void buttonExecuteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Будет выполнено заданий: " + checkedListBoxTasks.CheckedIndices.Count, "Подтверждение выполнения задания", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (checkedListBoxTasks.CheckedIndices.Count != 0)
                {
                    int[] i = new int[checkedListBoxTasks.CheckedIndices.Count];
                    for (int l = 0; l < checkedListBoxTasks.CheckedIndices.Count; l++)
                    {
                        i[l] = checkedListBoxTasks.CheckedIndices[l] + 1;
                    }
                    LogFile log = new LogFile();
                    log.WriteToLogFile(LogFileName, "Запущен процесс отправки сообщений");
                    log.WriteToLogFile(LogFileName, "Количество заданий: " + i.Count());
                    foreach (int l in i)
                    {
                        log.WriteToLogFile(LogFileName, "Название задания: " + checkedListBoxTasks.Items[(l - 1)].ToString());
                    }
                    SendVypiskaProcess SndVypProc = new SendVypiskaProcess(i);
                    log.WriteToLogFile(LogFileName, "Сформирован файл журнала почты: " + SndVypProc.ЖурналАрхива);
                    ErrorForm msg = new ErrorForm(SndVypProc.СообщенияОВыполнении, "Результаты отправки почты");
                    msg.ShowDialog();
                }
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (checkedListBoxTasks.SelectedIndex != -1 & checkedListBoxTasks.SelectedIndex != 0)
            {
                string InnerXml_1, Name_1, Enabled_1, InnerXml_2, Name_2, Enabled_2;
                int i = checkedListBoxTasks.SelectedIndex - 1;
                XmlDocument sendVypiska = new XmlDocument();
                sendVypiska.Load("SendVypiska.xml");
                XmlNode node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                InnerXml_1 = node.InnerXml;
                Name_1 = node.Attributes["Name"].Value;
                Enabled_1 = node.Attributes["Enabled"].Value;
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex) + "]");
                InnerXml_2 = node.InnerXml;
                Name_2 = node.Attributes["Name"].Value;
                Enabled_2 = node.Attributes["Enabled"].Value;
                node.InnerXml = InnerXml_1;
                node.Attributes["Name"].Value = Name_1;
                node.Attributes["Enabled"].Value = Enabled_1;
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                node.InnerXml = InnerXml_2;
                node.Attributes["Name"].Value = Name_2;
                node.Attributes["Enabled"].Value = Enabled_2;
                sendVypiska.Save("SendVypiska.xml");
                checkedListBoxTasks.Items.Clear();
                LoadData();
                checkedListBoxTasks.SetSelected(i, true);
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (checkedListBoxTasks.SelectedIndex != -1 & checkedListBoxTasks.SelectedIndex != checkedListBoxTasks.Items.Count - 1)
            {
                string InnerXml_1, Name_1, Enabled_1, InnerXml_2, Name_2, Enabled_2;
                int i = checkedListBoxTasks.SelectedIndex + 1;
                XmlDocument sendVypiska = new XmlDocument();
                sendVypiska.Load("SendVypiska.xml");
                XmlNode node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                InnerXml_1 = node.InnerXml;
                Name_1 = node.Attributes["Name"].Value;
                Enabled_1 = node.Attributes["Enabled"].Value;
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 2) + "]");
                InnerXml_2 = node.InnerXml;
                Name_2 = node.Attributes["Name"].Value;
                Enabled_2 = node.Attributes["Enabled"].Value;
                node.InnerXml = InnerXml_1;
                node.Attributes["Name"].Value = Name_1;
                node.Attributes["Enabled"].Value = Enabled_1;
                node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                node.InnerXml = InnerXml_2;
                node.Attributes["Name"].Value = Name_2;
                node.Attributes["Enabled"].Value = Enabled_2;
                sendVypiska.Save("SendVypiska.xml");
                checkedListBoxTasks.Items.Clear();
                LoadData();
                checkedListBoxTasks.SetSelected(i, true);
                
            }
        }

        private void buttonExecuteSelected_Click(object sender, EventArgs e)
        {
            if (checkedListBoxTasks.SelectedIndex != -1)
            {
                if (MessageBox.Show("Будет выполнено задание:\r" + checkedListBoxTasks.SelectedItem.ToString(), "Подтверждение выполнения задания", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LogFile log = new LogFile();
                    log.WriteToLogFile(LogFileName, "Запущен процесс отправки сообщений");
                    log.WriteToLogFile(LogFileName, "Количество заданий: 1");
                    log.WriteToLogFile(LogFileName, "Название задания: " + checkedListBoxTasks.SelectedItem.ToString());
                    int[] l = new int[1];
                    l[0] = checkedListBoxTasks.SelectedIndex + 1;
                    SendVypiskaProcess SndVypProc = new SendVypiskaProcess(l);
                    log.WriteToLogFile(LogFileName, "Сформирован файл журнала почты: " + SndVypProc.ЖурналАрхива);
                    ErrorForm msg = new ErrorForm(SndVypProc.СообщенияОВыполнении, "Результаты отправки почты");
                    msg.ShowDialog();
                }
            }
        }

        private void textBoxSender_TextChanged(object sender, EventArgs e)
        {
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            XmlNode node = settings.SelectSingleNode("/Settings/SendMail/From");
            node.InnerText = textBoxSender.Text;
            settings.Save("AlarmDog.xml");
            
        }

        private void textBoxSmtp_TextChanged(object sender, EventArgs e)
        {
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            XmlNode node = settings.SelectSingleNode("/Settings/SendMail/SMTP");
            node.InnerText = textBoxSmtp.Text;
            settings.Save("AlarmDog.xml");
        }

        private void textBoxLetter_TextChanged(object sender, EventArgs e)
        {
            if (checkedListBoxTasks.Items.Count != 0)
            {
                XmlDocument sendVypiska = new XmlDocument();
                sendVypiska.Load("SendVypiska.xml");
                XmlNode node = sendVypiska.SelectSingleNode("SendVypiska/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Text");
                node.InnerText = textBoxLetter.Text;
                sendVypiska.Save("SendVypiska.xml");
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
    public class MailTask
    {
        private string Name;
        private string ToEmail;
        private string Subject;
        private string Dir;
        private string Filter;
        private YesNo Attach;
        private Дней[] Schedule;
        private TimeSpan Time;
        private YesNo WhenToSend;

        public MailTask(int DayCount)
        {
            Schedule = new Дней[DayCount];
        }
        [Description("E-MAIL получателя, несколько адресов получателей нужно разделять запятой (,)"),
        DisplayName("E-mail получателя"),
        Category("Параметры задания")]
        public string Получатель
        {
            get { return ToEmail; }
            set { ToEmail = value; }
        }

        [Description("Имя файла или маска по которой будут отбиратся файлы для присоединения. dd - день, mm - месяц, yy или yyyy - год. Можно указывать несколько имен файлов или Масок имен файлов через прямую вертикальную черту | "),
        DisplayName("Имя файла"),
        Category("Параметры задания")]
        public string Маска
        {
            get { return Filter; }
            set { Filter = value; }
        }

        [Description("Тема сообщения"),
        Category("Параметры задания")]
        public string Тема
        {
            get { return Subject; }
            set { Subject = value; }
        }

        [Description("Название задания."),
        Category("Параметры задания")]
        public string Название
        {
            get { return Name; }
            set { Name = value; }
        }


        [Browsable(true),
        Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)),
        Description("Каталог, из которого присоединяются файлы. dd - день, mm - месяц, yy или yyyy - год."),
        Category("Параметры задания")]
        public string Директория
        {
            get { return Dir; }
            set { Dir = value; }
        }

        [Description("Присоединять файлы к письму."),
        DisplayName("Присоединять файлы ?"),
        Category("Параметры задания")]
        public YesNo Присоединять
        {
            get { return Attach; }
            set { Attach = value; }
        }

        [Description("В какие дни отправлять.\rПонедельник - отправлять в понедельник или в первый рабочий день недели.\rОстальные дни недели обозначают сами себя."),
        Category("Параметры задания")]
        public Дней[] Расписание
        {
            get { return Schedule; }
            set { Schedule = value; }
        }

        [Browsable(true),
        DisplayName("Время отправки"),
        Description("Время автоматической отправки сообщения с точностью до 1 минуты"),
        Category("Параметры задания")]
        public TimeSpan Время
        {
            get { return Time; }
            set { Time = value; }
        }

        [Browsable(true),
        DisplayName("Отправлять только со вложениями"),
        Description("Отправлять только те сообщения, для которых в указаных папках есть файлы для присоединения."),
        Category("Параметры задания")]
        public YesNo ОтправлятьСФайлами
        {
            get { return WhenToSend; }
            set { WhenToSend = value; }
        }
                
    }
    
    public enum Дней
    { Понедельник, Вторник, Среда, Четверг, Пятница }
}
