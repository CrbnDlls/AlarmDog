using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.XPath;
using System.Reflection;
using System.Drawing.Design;
using WMPLib;
using System.Security;
using System.Data.OracleClient;





namespace AlarmDog
{
    public partial class Form1 : Form
    {
        delegate void SetTextCallback(string text);
        delegate void DelTextCallback(string text);
        delegate void SetListBoxArchiveTextCallback(string text);
        delegate void ShowEndResultCallback(string[] text);
        delegate void StartArchiveCallback(int[] iProcesses);
        delegate void zTimeCallback();
        private Container watchers;
        private int iComBox;
        private string LogFileName;
        private System.Timers.Timer zTimer;
        private System.Timers.Timer vTimer;
        private string CurrArchivePath;
        private string ArchivePath;
        private string KeyPath;
        private string[] uFiles;
        private string[] LostFiles;
        private string zEnabled;        
        private WindowsMediaPlayer MusikPlayer;
        private SecureString Password;
        private string UserId;
        private string DataBase;
        private OracleConnection conn;
        private string AutoArchive;
        private string AutoArchiveEnabled;
        private int CntDwn;
        private string DoVypiska;
        private int LastDay;
        private string[] MailListBoxMessages;
        private string Tech;

        public Form1()
        {
            InitializeComponent();
            
            string PathMusik;
            int StrLenth;
            
            ToolTip TipAdd = new ToolTip();
            TipAdd.SetToolTip(buttonAdd, "Добавить задание");
            ToolTip TipDel = new ToolTip();
            TipDel.SetToolTip(buttonDel, "Удалить задание");
            ToolTip TipStart = new ToolTip();
            TipDel.SetToolTip(buttonStart, "Запустить работу программы");
            ToolTip TipArchive = new ToolTip();
            TipDel.SetToolTip(buttonArchive, "Настройки архивации");
            ToolTip TipButtons = new ToolTip();
            TipDel.SetToolTip(buttonCurrency, "Подключиться к базе");
            ToolTip TipDisconnect = new ToolTip();
            TipDel.SetToolTip(buttonDisconnect, "Отключиться от базы");

            XmlDocument settings = new XmlDocument();
            XmlNode node;
            
            if (!File.Exists("AlarmDog.xml"))
            {

                using (XmlWriter writer = XmlWriter.Create("AlarmDog.xml"))
                {
                    // Write XML data.
                    writer.WriteStartElement("Settings");
                    writer.WriteEndElement();

                    writer.Flush();
                }

                
                settings.Load("AlarmDog.xml");
                #region Наполнение файла настроек XML
                node = settings.DocumentElement;
                XmlElement task = settings.CreateElement("Task");
                task.SetAttribute("name", "Задание 1");
                XmlElement path = settings.CreateElement("Path");
                path.InnerText = " ";
                task.AppendChild(path);
                XmlElement mask = settings.CreateElement("Filter");
                mask.InnerText = " ";
                task.AppendChild(mask);
                XmlElement enabled = settings.CreateElement("Enabled");
                enabled.InnerText = "Да";
                task.AppendChild(enabled);
                node.AppendChild(task);
                //Tehnolog or Monitor
                task = settings.CreateElement("Tehnolog");
                task.SetAttribute("name", "Признак АРМа бухгалтера СЭП");
                task.InnerText = "Нет";
                node.AppendChild(task);
                //uLogFile
                task = settings.CreateElement("ULogFile");
                task.SetAttribute("name", "Путь и формат имени файла журнала почты НБУ");
                path = settings.CreateElement("Path");
                path.InnerText = "u:\\NBUMAIL\\NEWSTAT";
                task.AppendChild(path);
                mask = settings.CreateElement("LogFileNameFormat");
                mask.SetAttribute("example", "uddmmyy.log: dd - день, mm - месяц, yy или yyyy - год");
                mask.InnerText = "Dddmmyy.STA";
                task.AppendChild(mask);
                node.AppendChild(task);
                //NBU_KEY
                task = settings.CreateElement("NbuKey");
                task.SetAttribute("name", "Путь и фильтры имен файлов ключей НБУ");
                path = settings.CreateElement("Path");
                path.InnerText = "f:\\Bars\\NBU_KEY";
                task.AppendChild(path);
                mask = settings.CreateElement("Filters");
                mask.SetAttribute("example", "*.txt|12??.doc");
                mask.InnerText = "!B_11QK?.???|P_1QKO??.???|!3_11QK?.???|!B_0VSE?.???";
                task.AppendChild(mask);
                enabled = settings.CreateElement("Enabled");
                enabled.InnerText = "Да";
                task.AppendChild(enabled);
                node.AppendChild(task);
                //LastWorkingDay
                path = settings.CreateElement("LastWorkingDay");
                path.SetAttribute("name", "Путь и имя файла, в котором хранятся данные о предидущем рабочем дне");
                path.InnerText = "f:\\Soft\\AlarmDog\\LastWorkingDay.xml";
                node.AppendChild(path);
                //ArchivePath
                task = settings.CreateElement("ArchivePath");
                task.SetAttribute("name", "Каталоги архивов файлов от НБУ");
                path = settings.CreateElement("Path");
                path.SetAttribute("name", "Текущий каталог архива операционного дня");
                path.InnerText = "f:\\Bars.tss\\BACKUP\\Nbu\\yymmdd\\OK!";
                task.AppendChild(path);
                path = settings.CreateElement("Path");
                path.SetAttribute("name", "Каталог операционного дня в архиве");
                path.InnerText = "r:\\Nbu\\yymmdd\\OK!";
                task.AppendChild(path);
                node.AppendChild(task);
                //$Z
                task = settings.CreateElement("ZFile");
                task.SetAttribute("name", "Напоминание об отправке $Z");
                enabled = settings.CreateElement("Enabled");
                enabled.InnerText = "Да";
                task.AppendChild(enabled);
                path = settings.CreateElement("Path");
                path.SetAttribute("name", "Путь к файлу в архиве");
                path.InnerText = "f:\\Bars.tss\\BACKUP\\Nbu\\yymmdd\\OUT";
                task.AppendChild(path);
                path = settings.CreateElement("NameFormat");
                path.SetAttribute("name", "Формат имени файла");
                path.InnerText = "$Z11QK??.000";
                task.AppendChild(path);
                node.AppendChild(task);
                //SendMail
                task = settings.CreateElement("SendMail");
                task.SetAttribute("name", "Отправка кор. счета СМС");
                path = settings.CreateElement("From");
                path.InnerText = "rrp5@treasury.gov.ua";
                task.AppendChild(path);
                path = settings.CreateElement("SMTP");
                path.InnerText = "mail.treasury.gov.ua";
                task.AppendChild(path);
                mask = settings.CreateElement("To");
                mask.SetAttribute("name", "Даневич");
                mask.InnerText = "380674668145@2sms.kyivstar.net";
                task.AppendChild(mask);
                mask = settings.CreateElement("To");
                mask.SetAttribute("name", "Федорук Василий Олексеевич");
                mask.InnerText = "380673635080@2sms.kyivstar.net";
                task.AppendChild(mask);
                mask = settings.CreateElement("To");
                mask.SetAttribute("name", "Юрек Іван");
                mask.InnerText = "380979204670@2sms.kyivstar.net";
                task.AppendChild(mask);
                mask = settings.CreateElement("To");
                mask.SetAttribute("name", "Слюз Т.Я.");
                mask.InnerText = "380672193535@2sms.kyivstar.net";
                task.AppendChild(mask);
                enabled = settings.CreateElement("Enabled");
                enabled.InnerText = "Да";
                task.AppendChild(enabled);
                node.AppendChild(task);
                //LogFile
                task = settings.CreateElement("LogFile");
                task.SetAttribute("name", "Параметры файла журнала программы");
                path = settings.CreateElement("Path");
                task.AppendChild(path);
                mask = settings.CreateElement("LogFileNameFormat");
                mask.InnerText = "Alarmddmmyy.log";
                task.AppendChild(mask);
                node.AppendChild(task);
                //DataBase
                task = settings.CreateElement("Database");
                path = settings.CreateElement("Name");
                path.InnerText = "DNTR9";
                task.AppendChild(path);
                node.AppendChild(task);
                //Buttons
                task = settings.CreateElement("Buttons");
                path = settings.CreateElement("Tehnol");
                path.InnerText = "f:\\SOFT\\Texnolog\\Technol.exe";
                task.AppendChild(path);
                path = settings.CreateElement("TehnolLog");
                path.InnerText = "f:\\Soft\\Texnolog\\Technol.log";
                task.AppendChild(path);
                path = settings.CreateElement("BGen");
                path.InnerText = "f:\\SOFT\\@Bgenerator\\BGEN.EXE";
                task.AppendChild(path);
                node.AppendChild(task);
                //AutoArchive
                task = settings.CreateElement("AutoArchive");
                task.SetAttribute("name", "Настройки автоматического архивирования");
                path = settings.CreateElement("ZeePath");
                path.SetAttribute("name", "Расположение архива ZEE");
                path.InnerText = "f:\\Bars\\Kazna\\ARKB\\Zee";
                task.AppendChild(path);
                path = settings.CreateElement("ZeeNameFormat");
                path.SetAttribute("name", "Формат имени файла Zee");
                path.InnerText = "K1ZHddmm.zee";
                task.AppendChild(path);
                path = settings.CreateElement("ATBULogPath");
                path.SetAttribute("name", "Расположение файла журнала ATBU");
                path.InnerText = "f:\\Bars\\Kazna\\Log_path";
                task.AppendChild(path);
                path = settings.CreateElement("ATBULogNameFormat");
                path.SetAttribute("name", "Формат имени файла журнала ATBU");
                path.InnerText = "yyyymmdd.log";
                task.AppendChild(path);
                path = settings.CreateElement("ArmSepPath");
                path.SetAttribute("name", "Расположение архива АРМ СЭП");
                path.InnerText = "f:\\Bars.tss\\BACKUP\\ARMNBU";
                task.AppendChild(path);
                path = settings.CreateElement("ArmSepNameFormat");
                path.SetAttribute("name", "Формат имени файла архива АРМ СЭП");
                path.InnerText = "s1QK????.zip";
                task.AppendChild(path);
                path = settings.CreateElement("ArmNbuPath");
                path.SetAttribute("name", "Расположение архива АРМ НБУ");
                path.InnerText = "f:\\Bars.tss\\BACKUP\\ArmInf";
                task.AppendChild(path);
                path = settings.CreateElement("ArmNbuNameFormat");
                path.SetAttribute("name", "Формат имени файла архива АРМ НБУ");
                path.InnerText = "i1QK????.zip";
                task.AppendChild(path);
                node.AppendChild(task);
                #region Наименования ГУДКУ и счета Основного фонда гос. бюджета
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у м.Києві";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992820019";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText ="ГУДКУ у Київській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992821018";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ в АР Крим";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992824026";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Вінницькій обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992802015";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText ="ГУДКУ у Волинській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992803014";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Дніпропетровській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992805012";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText ="ГУДКУ у Донецькій обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992834016";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Житомирській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992811039";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Закарпатській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992812016";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Запорізькій обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992813015";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ в Івано-Франківській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992836014";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Кіровоградській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText = "31121992823016";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Луганській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992804013";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ Львівській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992825014";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Миколаївській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992826013";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ в Одеській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992828011";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Полтавській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992831019";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Рівненській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992833017";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Сумській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992837013";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Тернопільській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992838012";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Харківській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992851011";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Херсонській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992852010";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Хмельницькій обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992815013";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText ="ГУДКУ у Черкаській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992854018";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Чернівецькій обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992856135";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у Чернігівській обл.";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992853592";
                task.AppendChild(mask);
                node.AppendChild(task);
                task = settings.CreateElement("GUDKU");
                path = settings.CreateElement("Name");
                path.InnerText = "ГУДКУ у м.Севастополь";
                task.AppendChild(path);
                mask = settings.CreateElement("Account");
                mask.InnerText ="31121992824509";
                task.AppendChild(mask);
                node.AppendChild(task);
                #endregion
                #endregion
                settings.Save("AlarmDog.xml");
            }
            
                
            settings.Load("AlarmDog.xml");
            XmlNodeList nodelst = settings.SelectNodes("/Settings/Task");
            foreach (XmlNode node1 in nodelst)
            {
                comboBoxTasks.Items.Add(node1.Attributes["name"].Value);
                
            }
            
            EventArgs e = new EventArgs();
            if (comboBoxTasks.Items.Count == 0)
            {
                comboBoxTasks.SelectedIndex = -1;
                comboBoxTasks.Text = null;
                propertyGridTasks.Enabled = false;
                buttonStart.Enabled = false;
                buttonDel.Enabled = false;
            }
            else
            {
                comboBoxTasks.SelectedIndex = 0;
                comboBoxTasks_SelectionChangeCommitted(comboBoxTasks, e);
            }
            
            
            MusikPlayer = new WindowsMediaPlayer();
            PathMusik = Path.GetFullPath("AlarmDog.exe");
            StrLenth = PathMusik.Length;
            PathMusik = PathMusik.Remove(StrLenth - 13);
            openFileDialogMusik.InitialDirectory = PathMusik;

            

            
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

            LogFileName = "Alarm" + Date + Month + DateTime.Now.Year.ToString().Substring(2) + ".log";
            //ArchivePath
            node = settings.SelectSingleNode("/Settings/ArchivePath/Path[1]");
            CurrArchivePath = node.InnerText;
            CurrArchivePath = CurrArchivePath.Replace("dd", Date);
            CurrArchivePath = CurrArchivePath.Replace("mm", Month);
            CurrArchivePath = CurrArchivePath.Replace("yyyy", DateTime.Now.Year.ToString());
            CurrArchivePath = CurrArchivePath.Replace("yy", DateTime.Now.Year.ToString().Substring(2));
            node = settings.SelectSingleNode("/Settings/ArchivePath/Path[2]");
            ArchivePath = node.InnerText;
            ArchivePath = ArchivePath.Replace("dd", Date);
            ArchivePath = ArchivePath.Replace("mm", Month);
            ArchivePath = ArchivePath.Replace("yyyy", DateTime.Now.Year.ToString());
            ArchivePath = ArchivePath.Replace("yy", DateTime.Now.Year.ToString().Substring(2));
            //NbuKey
            node = settings.SelectSingleNode("/Settings/NbuKey/Enabled");
            
            if (node.InnerText == "Да")
            {
                NbuKey NKey = new NbuKey();
                node = settings.SelectSingleNode("/Settings/NbuKey/Path");
                KeyPath = node.InnerText;
                node = settings.SelectSingleNode("/Settings/NbuKey/Filters");
                string Filters = node.InnerText;
                string[] KeyList = NKey.GetKeyList(KeyPath,Filters);
                if (NKey.НеобходимоОбновить == YesNo.Да)
                {
                    listBoxFoundFiles.Items.Add("Необходимо обновить ключи.");
                    listBoxFoundFiles.Items.AddRange(KeyList);
                }
            }
            //$Z
            node = settings.SelectSingleNode("/Settings/ZFile/Enabled");
            zEnabled = node.InnerText;
            DirectorySearcher searcher = new DirectorySearcher();
            node = settings.SelectSingleNode("/Settings/ZFile/Path");
            searcher.Путь = Format(node.InnerText);
            node = settings.SelectSingleNode("/Settings/ZFile/NameFormat");
            searcher.Маска = Format(node.InnerText);
            searcher.SearchDirectory();
            if (searcher.Ошибка == null && searcher.СписокФайлов != null)
            {
                zEnabled = "Нет";
            }
            //Database
            node = settings.SelectSingleNode("/Settings/Database/Name");
            DataBase = node.InnerText;
            //Tehnolog
            node = settings.SelectSingleNode("/Settings/Tehnolog");
            if (node.InnerText == "Да")
            {
                Tech = "Да";
                checkBoxUFiles.Visible = true;
                checkBoxAutoArchive.Visible = true;
                buttonCurrency.Visible = true;
                buttonArchive.Visible = true;
                buttonDisconnect.Visible = true;
                checkBoxVypiska.Visible = true;
                buttonMail.Visible = true;
                checkBoxAutoMail.Visible = true;

                buttonMail.Enabled = true;
                checkBoxUFiles.Enabled = true;
                checkBoxAutoArchive.Enabled = true;
                buttonCurrency.Enabled = true;
                buttonArchive.Enabled = true;
                checkBoxAutoMail.Enabled = true;
                //LastWorkingDay
                node = settings.SelectSingleNode("/Settings/LastWorkingDay");
                string LastWorkingDay = node.InnerText;
                XmlDocument lastday = new XmlDocument();
                if (!File.Exists(LastWorkingDay))
                {
                    try
                    {
                        using (XmlWriter writer = XmlWriter.Create(LastWorkingDay))
                        {
                            // Write XML data.
                            writer.WriteStartElement("Days");
                            writer.WriteEndElement();

                            writer.Flush();
                        }
                        lastday.Load(LastWorkingDay);
                        node = lastday.DocumentElement;
                        XmlElement element = lastday.CreateElement("LastWorkingDay");
                        element.InnerText = DateTime.Now.DayOfYear.ToString();
                        node.AppendChild(element);
                        element = lastday.CreateElement("ToDay");
                        element.InnerText = DateTime.Now.DayOfYear.ToString();
                        node.AppendChild(element);
                        lastday.Save(LastWorkingDay);
                    }
                    catch (Exception ex)
                    {
                        checkBoxAutoMail.Enabled = false;
                        checkBoxAutoMail.Visible = false;
                        MessageBox.Show("Не могу создать файл\rАвтоматическая отправка сообщений не возможна\r" + ex.Message, "Ошибка");
                    }

                }
                else
                {
                    try
                    {
                        lastday.Load(LastWorkingDay);
                        node = lastday.SelectSingleNode("/Days/ToDay");
                        if (node.InnerText != DateTime.Now.DayOfYear.ToString())
                        {
                            string Day = node.InnerText;
                            node = lastday.SelectSingleNode("/Days/LastWorkingDay");
                            node.InnerText = Day;
                            node = lastday.SelectSingleNode("/Days/ToDay");
                            node.InnerText = DateTime.Now.DayOfYear.ToString();
                        }
                        lastday.Save(LastWorkingDay);
                    }
                    catch (Exception ex)
                    {
                        checkBoxAutoMail.Enabled = false;
                        checkBoxAutoMail.Visible = false;
                        MessageBox.Show("Не могу открыть файл\rАвтоматическая отправка сообщений не возможна\r" + ex.Message, "Ошибка");
                    }
                }
                node = lastday.SelectSingleNode("/Days/LastWorkingDay");
                if (node != null)
                {
                    LastDay = Int32.Parse(node.InnerText);
                }
                else
                {
                    checkBoxAutoMail.Enabled = false;
                    checkBoxAutoMail.Visible = false;
                }
            }
            //LogFile
            LogFile log = new LogFile();
            node = settings.SelectSingleNode("/Settings/LogFile/Path");
            LogFileName = node.InnerText;
            node = settings.SelectSingleNode("/Settings/LogFile/LogFileNameFormat");
            if (LogFileName.EndsWith("\\") | LogFileName == "")
            {
                LogFileName = LogFileName + node.InnerText;
            }
            else
            {
                LogFileName = LogFileName + "\\" + node.InnerText;
            }
            AutoArchiveEnabled = "No";
            
            LogFileName = log.GetLogFileName(LogFileName);
            log.WriteToLogFile(LogFileName, "Запуск программы");
            
        }
        
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int Cnt;

            XmlDocument settings = new XmlDocument();
            XmlNode node;

            Cnt = comboBoxTasks.Items.Count + 1;

            if (!File.Exists("AlarmDog.xml"))
            {

                using (XmlWriter writer = XmlWriter.Create("AlarmDog.xml"))
                {
                    // Write XML data.
                    writer.WriteStartElement("Settings");
                    writer.WriteEndElement();

                    writer.Flush();
                }

                settings.Load("AlarmDog.xml");
                node = settings.SelectSingleNode("/Settings");
                node.InnerXml = "<Task name=\"Задание 1\"><Path> </Path><Filter> </Filter><Enabled>Да</Enabled></Task>";
                settings.Save("AlarmDog.xml");
                comboBoxTasks.Items.Add("Задание 1");
                comboBoxTasks.SelectedIndex = 0;
                comboBoxTasks_SelectionChangeCommitted(comboBoxTasks, e);

            }
            else
            {
                settings.Load("AlarmDog.xml");
                node = settings.DocumentElement;
                XmlElement task = settings.CreateElement("Task");
                task.SetAttribute("name", "Задание " + Cnt);
                XmlElement path = settings.CreateElement("Path");
                path.InnerText = " ";
                task.AppendChild(path);
                XmlElement mask = settings.CreateElement("Filter");
                mask.InnerText = " ";
                task.AppendChild(mask);
                XmlElement enabled = settings.CreateElement("Enabled");
                enabled.InnerText = "Да";
                task.AppendChild(enabled);
                node.AppendChild(task);
                settings.Save("AlarmDog.xml");
                comboBoxTasks.Items.Add("Задание " + Cnt);
                comboBoxTasks.SelectedIndex = Cnt - 1;
                comboBoxTasks_SelectionChangeCommitted(comboBoxTasks, e);

            }
            
            if (comboBoxTasks.Items.Count == 1)
            {
                buttonStart.Enabled = true;
                propertyGridTasks.Enabled = true;
                buttonDel.Enabled = true;
            }

        }
       
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            checkBoxMusik.Enabled = true;
            buttonStart.Enabled = true;
        }
        
        private void buttonDel_Click(object sender, EventArgs e)
        {
            buttonDel.Enabled = false;
            buttonAdd.Enabled = false;
            checkBoxMusik.Enabled = false;
            buttonStart.Enabled = false;
            comboBoxTasks.Enabled = false;
            propertyGridTasks.Enabled = false;
            checkBoxUFiles.Enabled = false;
            checkBoxAutoArchive.Enabled = false;
            buttonCurrency.Enabled = false;
            buttonArchive.Enabled = false;
            buttonMail.Enabled = false;
            checkBoxAutoMail.Enabled = false;
            labelAccept2.Text = comboBoxTasks.Text + " ?";
            panelAccept.Enabled = true;
            panelAccept.Visible = true;
        }

        private void buttonAccCancel_Click(object sender, EventArgs e)
        {
            panelAccept.Enabled = false;
            panelAccept.Visible = false;
            buttonDel.Enabled = true;
            buttonAdd.Enabled = true;
            checkBoxMusik.Enabled = true;
            buttonStart.Enabled = true;
            comboBoxTasks.Enabled = true;
            propertyGridTasks.Enabled = true;
            checkBoxUFiles.Enabled = true;
            checkBoxAutoArchive.Enabled = true;
            buttonCurrency.Enabled = true;
            buttonArchive.Enabled = true;
            buttonMail.Enabled = true;
            checkBoxAutoMail.Enabled = true;
        }

        private void buttonAccOk_Click(object sender, EventArgs e)
        {
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            XmlNode node = settings.SelectSingleNode("/Settings");
            node.RemoveChild(settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]"));
            
            if (comboBoxTasks.SelectedIndex == -1)
            {
                comboBoxTasks.Items.Clear();
            }
            comboBoxTasks.Items.Remove(comboBoxTasks.SelectedItem);
            
            settings.Save("AlarmDog.xml");
            if (comboBoxTasks.Items.Count == 0)
            {
                comboBoxTasks.SelectedIndex = -1;
                comboBoxTasks.Text = null;
                propertyGridTasks.Enabled = false;
                buttonStart.Enabled = false;
            }
            else
            {
                buttonStart.Enabled = true;
                propertyGridTasks.Enabled = true;
                comboBoxTasks.SelectedIndex = 0;
                comboBoxTasks_SelectionChangeCommitted(comboBoxTasks, e);
            }
            
            panelAccept.Enabled = false;
            panelAccept.Visible = false;
            if (comboBoxTasks.Items.Count != 0)
            {
                buttonDel.Enabled = true;
            }
            buttonAdd.Enabled = true;
            checkBoxMusik.Enabled = true;
            checkBoxUFiles.Enabled = true;
            checkBoxAutoArchive.Enabled = true;
            buttonCurrency.Enabled = true;
            buttonArchive.Enabled = true;
            comboBoxTasks.Enabled = true;
            buttonMail.Enabled = true;
            checkBoxAutoMail.Enabled = true;
            
        }
               

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int NodesCnt = comboBoxTasks.Items.Count;
            XmlNode node;
            LogFile log = new LogFile();
            log.ПутьиИмяФайла = LogFileName;

            switch (buttonStart.Text)
            { 
                case "Старт":
                    listBoxFoundFiles.Items.Clear();
                    log.WriteToLogFile("Процесс наблюдения: Запущен");
                    watchers = new Container();
                    buttonStart.ForeColor = System.Drawing.Color.DarkRed;
                    buttonStart.Text = "Стоп";
                    checkBoxMusik.Enabled = false;
                    propertyGridTasks.Enabled = false;
                    buttonAdd.Enabled = false;
                    buttonDel.Enabled = false;

                    XmlDocument settings = new XmlDocument();
                    settings.Load("AlarmDog.xml");
                    
                    for (int i = 1; i <= NodesCnt; i++) 
                    {
                        node = settings.SelectSingleNode("/Settings/Task[" + i + "]/Enabled");
                        if (node.InnerText == "Да")
                        {
                            node = settings.SelectSingleNode("/Settings/Task[" + i + "]/Path");
                            if (!Directory.Exists(node.InnerText))
                            {
                                listBoxFoundFiles.ForeColor = Color.Red;
                                listBoxFoundFiles.Items.Add("Данного пути не существует:");
                                listBoxFoundFiles.Items.Add(comboBoxTasks.Items[i - 1].ToString() + ": " + node.InnerText);
                            }
                        }
                        
                    }

                    if (listBoxFoundFiles.Items.Count == 0)
                    {
                        for (int i = 1; i <= NodesCnt; i++)
                        {
                            node = settings.SelectSingleNode("/Settings/Task[" + i + "]/Enabled");
                            if (node.InnerText == "Да")
                            {
                                string[] fileList;
                                System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher();
                                node = settings.SelectSingleNode("/Settings/Task[" + i + "]/Path");
                                watcher.Path = node.InnerText;
                                node = settings.SelectSingleNode("/Settings/Task[" + i + "]/Filter");
                                watcher.Filter = node.InnerText;
                                watcher.Created += new FileSystemEventHandler(watcher_Created);
                                watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
                                watcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName;
                                watcher.IncludeSubdirectories = false;
                                watcher.EnableRaisingEvents = true;
                                watchers.Add(watcher, "watcher" + i);

                                fileList = Directory.GetFiles(watcher.Path, watcher.Filter, SearchOption.TopDirectoryOnly);
                                listBoxFoundFiles.Items.AddRange(fileList);
                                foreach (string str in fileList)
                                {
                                    log.WriteToLogFile("Поступил файл: " + str);
                                }
                            }
                        }

                        if (checkBoxUFiles.Checked == true)
                        {
                            ULogFile uLog = new ULogFile();
                            uLog.GetFileList();
                            if (uLog.Ошибка == null)
                            {
                                uFiles = uLog.СписокФайлов;
                                timerUFiles.Enabled = true;
                                log.WriteToLogFile("Запущена проверка поступления файлов в архив.");
                            }
                            else
                            {
                                listBoxFoundFiles.ForeColor = Color.Red;
                                listBoxFoundFiles.Items.Add("Проверка поступления файлов в архив не выполняется.");
                                listBoxFoundFiles.Items.Add(uLog.Ошибка);
                                listBoxFoundFiles.Items.Add(uLog.ФайлЖурналаПочтыНБУ);
                                log.WriteToLogFile("Запущена проверка поступления файлов в архив.");
                                log.WriteToLogFile("Проверка поступления файлов в архив не выполняется.");
                                log.WriteToLogFile(uLog.Ошибка);
                                log.WriteToLogFile(uLog.ФайлЖурналаПочтыНБУ); 
                            }
                        }

                        if (checkBoxAutoArchive.Checked == true)
                        {
                            checkBoxAutoArchive_Click(sender, e);
                        }
                        if (checkBoxAutoMail.Checked == true)
                        {
                            timerMail.Enabled = true;
                            log.WriteToLogFile(LogFileName, "Включена автоматическая отправка сообщений");
                        }
                        if (listBoxFoundFiles.Items.Count == 0)
                        {
                            WindowState = FormWindowState.Minimized;
                            ShowInTaskbar = false;
                        }
                        else
                        {
                            if (checkBoxMusik.Checked == true)
                            {
                                MusikPlayer.controls.play();
                            }
                        }
                        GC.Collect();
                    }
                    else
                    {
                        log.WriteToLogFile("Процесс наблюдения: Запущен с ошибками");
                        listBoxFoundFiles.Items.Add("Программа не работает.");
                        listBoxFoundFiles.Items.Add("Нажмите кнопку \"Стоп\" и измените настройки.");
                    }
                    break;

                case "Стоп":
                    listBoxFoundFiles.Items.Clear();
                    listBoxFoundFiles.ForeColor = Color.Black;
                    watchers.Dispose();
                    MusikPlayer.controls.pause();
                    notifyIcon.Visible = false;
                    ShowInTaskbar = true;
                    buttonStart.ForeColor = System.Drawing.Color.DarkGreen;
                    buttonStart.Text = "Старт";
                    checkBoxMusik.Enabled = true;
                    propertyGridTasks.Enabled = true;
                    buttonAdd.Enabled = true;
                    buttonDel.Enabled = true;
                    timerUFiles.Enabled = false;
                    if (checkBoxUFiles.Checked == true)
                    {
                        log.WriteToLogFile("Остановлена проверка поступления файлов в архив");
                    }
                    if (checkBoxAutoMail.Checked == true)
                    {
                        timerMail.Enabled = false;
                        log.WriteToLogFile(LogFileName, "Автоматическая отправка сообщений выключена");
                    }

                    if (checkBoxAutoArchive.Checked == true)
                    {
                        checkBoxAutoArchive_Click(sender, e);
                    }
                    log.WriteToLogFile("Процесс наблюдения: Остановлен");
                    break;
            }
            
            
        }

        void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            /*throw new NotImplementedException();*/
            DelText(e.FullPath);
        }

        public void DelText(string text) 
        {
            if (listBoxFoundFiles.InvokeRequired)
            {
                DelTextCallback d = new DelTextCallback(DelText);
                Invoke(d, new object[] { text });
            }
            else
            {
                listBoxFoundFiles.Items.Remove(text.ToString());
                if (text.ToString().Substring(text.ToString().Length - 12, 6) == "$Z11QK")
                {
                    listBoxFoundFiles.ForeColor = Color.Black;
                    listBoxFoundFiles.Items.Remove("Отправить СМС - кор. счет");
                    listBoxFoundFiles.Items.Remove("Автоматическая отправка SMS не выполнена. смотри лог");
                    listBoxFoundFiles.Items.Remove("Выполнена автоматическая отправка SMS.");
                    listBoxFoundFiles.Items.Remove("Автоматическая отправка SMS не выполнена.");
                    listBoxFoundFiles.Items.Remove("Автоматическая отправка SMS выполнена с ошибками. смотри лог");
                    listBoxFoundFiles.Items.Remove("Не введены Имя пользователя и Пароль.");
                }
                LogFile log = new LogFile();
                log.WriteToLogFile(LogFileName, "Обработан файл: " + text.ToString());
                if (checkBoxAutoArchive.Checked == true & Password != null & UserId != null)
                {
                    if (text.ToString().Substring(text.ToString().Length - 12, 6) == "$V11QK" & text.ToString().Substring(text.ToString().Length - 3, 1) == "0")
                    {
                        AutoArchive = "Yes";
                        DoAutoArchive();
                    }
                }
                if (listBoxFoundFiles.Items.Count == 0) 
                {
                    if (checkBoxMusik.Checked == true)
                    {
                        MusikPlayer.controls.pause();
                    }
                    WindowState = FormWindowState.Minimized;
                    ShowInTaskbar = false;
                }
            }
        }

        private void DoAutoArchive()
        {
            CntDwn = 10;
            LogFile log = new LogFile();
            if (vTimer == null)
            {
                vTimer = new System.Timers.Timer(180000);
                vTimer.Elapsed += new System.Timers.ElapsedEventHandler(vTimer_Elapsed);
                vTimer.Enabled = true;
            }
            else
            {
                vTimer.Enabled = true;
            }

        }

        void vTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //throw new NotImplementedException();
            vTimer.Enabled = false;
            if (AutoArchive == "SecondCheckComlited")
            {
                CntDwn = CntDwn - 1;
                if (CntDwn == 0)
                {
                    SetListBoxArchiveText("АвтоАрхив: Запущен процесс архивирования");
                    AutoArchive = "Start";
                    XmlDocument settings = new XmlDocument();
                    settings.Load("Archive.xml");
                    XmlNodeList nodelst = settings.SelectNodes("/Archive/Task");
                    int l = 0;

                    for (int i = 0; i < nodelst.Count; i++)
                    {
                        if (nodelst[i].Attributes["Enabled"].Value == "Да")
                        {
                            l = l + 1;
                        }
                    }
                    int[] iProcesses = new int[l];
                    SetListBoxArchiveText("АвтоАрхив: Количество заданий: " + l);
                    l = 0;
                    for (int i = 0; i < nodelst.Count; i++)
                    {
                        if (nodelst[i].Attributes["Enabled"].Value == "Да")
                        {
                            SetListBoxArchiveText("АвтоАрхив: Название задания: " + nodelst[i].Attributes["Name"].Value);
                            iProcesses[l] = i + 1;
                            l = l + 1;
                        }
                    }
                    StartArchive(iProcesses);
                    
                }
                else
                {
                    SetListBoxArchiveText("АвтоАрхив: " + CntDwn);
                }
            }
                        
            if (AutoArchive == "FirstCheckComlited")
            {
                vTimer.Interval = 30000;
                if (MeetConditions())
                {
                    AutoArchive = "SecondCheckComlited";
                    vTimer.Interval = 1000;
                    SetListBoxArchiveText("АвтоАрхив: Закончена вторая проверка");
                }
            }

            if (AutoArchive == "Yes")
            {
                vTimer.Interval = 30000;
                if (MeetConditions())
                {
                    AutoArchive = "FirstCheckComlited";
                    vTimer.Interval = 120000;
                    SetListBoxArchiveText("АвтоАрхив: Закончена первая проверка");
                    
                }
            }
            vTimer.Enabled = true;
        }

        private void StartArchive(int[] iProcesses)
        {
            if (listBoxFoundFiles.InvokeRequired)
            {
                StartArchiveCallback d = new StartArchiveCallback(StartArchive);
                Invoke(d, new object[] { iProcesses });
            }
            else
            {

                if (checkBoxAutoArchive.Checked == true)
                {
                    ArchiveProcess arch = new ArchiveProcess(iProcesses);
                    arch.ShowDialog(this);
                    SetListBoxArchiveText("АвтоАрхив: Сформирован файл журнала архива: " + arch.ЖурналАрхива);
                    foreach (string line in arch.СписокОшибок)
                    {
                        SetListBoxArchiveText(line);
                    }
                    ShowEndResult(arch.СписокОшибок);
                }
                
            }
        }

        private void ShowEndResult(string[] text)
        {
            if (listBoxFoundFiles.InvokeRequired)
            {
                ShowEndResultCallback d = new ShowEndResultCallback(ShowEndResult);
                Invoke(d, new object[] { text });
            }
            else
            {
                ErrorForm msg = new ErrorForm(text, "Результаты архивирования");
                msg.ShowDialog();
                AutoArchive = "Stop";
                SetListBoxArchiveText("АвтоАрхив: Архивирование завершено");
            }
        }
        private void TehnolStart()
        {
            Buttons but = new Buttons(conn, Password, UserId, DataBase, LogFileName, 1);           
        }

        private void SetListBoxArchiveText(string text)
        {
            if (listBoxFoundFiles.InvokeRequired)
            {
                SetListBoxArchiveTextCallback d = new SetListBoxArchiveTextCallback(SetListBoxArchiveText);
                Invoke(d, new object[] { text });
            }
            else
            {
                LogFile log = new LogFile();
                log.WriteToLogFile(LogFileName, text);
                if (AutoArchive != "Start")
                {
                    listBoxFoundFiles.Items.Add(text);
                }
                if (text == "АвтоАрхив: Закончена первая проверка" & checkBoxVypiska.Checked == true)
                {
                    Thread Tehnol = new Thread(new ThreadStart(TehnolStart));
                    Tehnol.Start();
                    log.WriteToLogFile("Запущено автоматическое создание выписки  на АС \"Казна-Видатки\"");
                    listBoxFoundFiles.Items.Add("Запущено автоматическое создание выписки  на АС \"Казна-Видатки\"");
                    
                }

                listBoxFoundFiles.Items.Remove("АвтоАрхив: 9");
                listBoxFoundFiles.Items.Remove("АвтоАрхив: 8");
                listBoxFoundFiles.Items.Remove("АвтоАрхив: 7");
                listBoxFoundFiles.Items.Remove("АвтоАрхив: 6");
                listBoxFoundFiles.Items.Remove("АвтоАрхив: 5");
                listBoxFoundFiles.Items.Remove("АвтоАрхив: 4");
                listBoxFoundFiles.Items.Remove("АвтоАрхив: 3");
                listBoxFoundFiles.Items.Remove("АвтоАрхив: 2");
                
                
                if (AutoArchive == "Start")
                {
                    buttonArchive.Enabled = false;
                }
                if (AutoArchive == "Stop")
                {
                    listBoxFoundFiles.Items.Remove("АвтоАрхив: Закончена первая проверка");
                    listBoxFoundFiles.Items.Remove("АвтоАрхив: Закончена вторая проверка");
                    listBoxFoundFiles.Items.Remove("АвтоАрхив: 1");
                    listBoxFoundFiles.Items.Remove("АвтоАрхив: Запущен процесс архивирования");
                    listBoxFoundFiles.Items.Remove("АвтоАрхив: Архивирование завершено");
                    listBoxFoundFiles.Items.Remove("Запущено автоматическое создание выписки на АС \"Казна-Видатки\"");
                    buttonArchive.Enabled = true;
                }
                ShowInTaskbar = true;
                if (WindowState != FormWindowState.Normal)
                {
                    WindowState = FormWindowState.Normal;
                }

            }
        }

        void watcher_Created(object sender, FileSystemEventArgs e)
        {
            /*throw new NotImplementedException();*/
            SetText(e.FullPath);
        }

        private void SetText(string text)
		{
			
			if (listBoxFoundFiles.InvokeRequired)
			{	
				SetTextCallback d = new SetTextCallback(SetText);
				Invoke(d, new object[] { text });
			}
			else
			{
                LogFile log = new LogFile();
                listBoxFoundFiles.Items.Add(text.ToString());
                if (Tech == "Да")
                {
                    if (text.ToString().Substring(text.ToString().Length - 12, 6) == "$Z11QK")
                    {
                        listBoxFoundFiles.Items.Remove("Время отправлять $Z");
                        listBoxFoundFiles.Items.Add("Отправить СМС - кор. счет");
                        if (Password != null & UserId != null)
                        {
                            try
                            {
                                conn.Open();
                            }
                            catch
                            {

                            }
                            SendMail send = new SendMail(conn);
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                            if (send.Ошибка[0] != "Ok")
                            {
                                listBoxFoundFiles.ForeColor = Color.Red;
                                listBoxFoundFiles.Items.Add("Автоматическая отправка SMS не выполнена. смотри лог");
                                log.WriteToLogFile(LogFileName, "Автоматическая отправка SMS не выполнена.");
                                log.WriteToLogFile(LogFileName, send.Ошибка[0]);
                            }
                            else
                            {
                                if (send.Ошибка.Count() > 1)
                                {
                                    listBoxFoundFiles.Items.Add("Автоматическая отправка SMS выполнена с ошибками. смотри лог");
                                    log.WriteToLogFile(LogFileName, "Автоматическая отправка SMS выполнена с ошибками.");
                                    for (int i = 1; i < send.Ошибка.Count(); i++)
                                    {
                                        log.WriteToLogFile(LogFileName, send.Ошибка[i]);
                                    }
                                }
                                else
                                {
                                    listBoxFoundFiles.Items.Add("Выполнена автоматическая отправка SMS.");
                                    log.WriteToLogFile(LogFileName, "Выполнена автоматическая отправка SMS.");
                                    log.WriteToLogFile(LogFileName, send.Получатели);
                                }
                            }
                        }
                        else
                        {
                            listBoxFoundFiles.Items.Add("Автоматическая отправка SMS не выполнена.");
                            listBoxFoundFiles.Items.Add("Не введены Имя пользователя и Пароль.");
                            log.WriteToLogFile(LogFileName, "Автоматическая отправка SMS не выполнена.");
                            log.WriteToLogFile(LogFileName, "Не введены Имя пользователя и Пароль.");
                        }
                    }
                }
                log.WriteToLogFile(LogFileName,"Поступил файл: " + text.ToString());
                if (checkBoxMusik.Checked == true) 
                {
                    MusikPlayer.controls.play();
                }
                ShowInTaskbar = true;
                if (WindowState != FormWindowState.Normal)
                {
                    WindowState = FormWindowState.Normal;
                }
                
			}
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            ShowInTaskbar = true;
        }

        private void openFileDialogMusik_FileOk(object sender, CancelEventArgs e)
        {
            MusikPlayer.settings.autoStart = false;
            MusikPlayer.URL = openFileDialogMusik.FileName;
            checkBoxMusik.Checked = true;
            
        }

        private void comboBoxTasks_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XmlNode node;
            Task task = new Task();
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            
            if (comboBoxTasks.SelectedIndex == -1)
            {
                node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]");
                task.Название = node.Attributes["name"].Value;
                node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]/Path");
                task.Путь = node.InnerText;
                node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]/Filter");
                task.Маска = node.InnerText;
                node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]/Enabled");
                if (node.InnerText == "Да")
                {
                    task.Включено = YesNo.Да;
                }
                else
                {
                    task.Включено = YesNo.Нет;
                }
                propertyGridTasks.SelectedObject = task;

            }
            else
            {
                node = settings.SelectSingleNode("/Settings/Task[" + (comboBoxTasks.SelectedIndex + 1) + "]");
                task.Название = node.Attributes["name"].Value;
                node = settings.SelectSingleNode("/Settings/Task[" + (comboBoxTasks.SelectedIndex + 1) + "]/Path");
                task.Путь = node.InnerText;
                node = settings.SelectSingleNode("/Settings/Task[" + (comboBoxTasks.SelectedIndex + 1) + "]/Filter");
                task.Маска = node.InnerText;
                node = settings.SelectSingleNode("/Settings/Task[" + (comboBoxTasks.SelectedIndex + 1) + "]/Enabled");
                if (node.InnerText == "Да")
                {
                    task.Включено = YesNo.Да;
                }
                else
                {
                    task.Включено = YesNo.Нет;
                }
                propertyGridTasks.SelectedObject = task;
                iComBox = comboBoxTasks.SelectedIndex;
            }
        }

        private void propertyGridTasks_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
                        
            XmlDocument settings = new XmlDocument();
            XmlNode node;
            settings.Load("AlarmDog.xml");
            
            if (propertyGridTasks.SelectedGridItem.Label == "Путь")
            {
                node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]/Path");
                node.InnerText = propertyGridTasks.SelectedGridItem.Value.ToString();
                node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]/Enabled");
                if (node.InnerText == "Да")
                {
                    if (!Directory.Exists(propertyGridTasks.SelectedGridItem.Value.ToString()))
                    {
                        listBoxFoundFiles.Items.Add("Данного пути не существует:");
                        listBoxFoundFiles.Items.Add(propertyGridTasks.SelectedGridItem.Value.ToString());
                    }
                    else
                    {
                        listBoxFoundFiles.Items.Clear();
                    }
                }
            }

            if (propertyGridTasks.SelectedGridItem.Label == "Маска")
            {
                node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]/Filter");
                node.InnerText = propertyGridTasks.SelectedGridItem.Value.ToString();
            }

            if (propertyGridTasks.SelectedGridItem.Label == "Название")
            {
                node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]");
                node.Attributes["name"].Value = propertyGridTasks.SelectedGridItem.Value.ToString();
                comboBoxTasks.Items[iComBox] = propertyGridTasks.SelectedGridItem.Value.ToString();
            }

            if (propertyGridTasks.SelectedGridItem.Label == "Включено")
            {
                node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]/Enabled");
                node.InnerText = propertyGridTasks.SelectedGridItem.Value.ToString();
                if (propertyGridTasks.SelectedGridItem.Value.ToString() != "Да")
                {
                    listBoxFoundFiles.Items.Clear();
                }
                else
                {
                    node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]/Path");
                    if (!Directory.Exists(node.InnerText))
                    {
                        listBoxFoundFiles.Items.Add("Данного пути не существует:");
                        listBoxFoundFiles.Items.Add(node.InnerText);
                    }
                }
            }
            settings.Save("AlarmDog.xml");


        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                notifyIcon.Visible = true;
            }
        }

        


        private void comboBoxTasks_TextUpdate(object sender, EventArgs e)
        {
            if (comboBoxTasks.Items.Count == 0)
            {
                buttonAdd_Click(sender, e);
            }
            Task task = new Task();
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            XmlNode node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]");
            node.Attributes["name"].Value = comboBoxTasks.Text;
            settings.Save("AlarmDog.xml");
            comboBoxTasks.Items[iComBox] = comboBoxTasks.Text;
            node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]");
            task.Название = node.Attributes["name"].Value;
            node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]/Path");
            task.Путь = node.InnerText;
            node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]/Filter");
            task.Маска = node.InnerText;
            node = settings.SelectSingleNode("/Settings/Task[" + (iComBox + 1) + "]/Enabled");
            if (node.InnerText == "Да")
            {
                task.Включено = YesNo.Да;
            }
            else
            {
                task.Включено = YesNo.Нет;
            }
            propertyGridTasks.SelectedObject = task;
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogFile log = new LogFile();
            log.WriteToLogFile(LogFileName,"Завершение работы");
        }

        private void timerUFiles_Tick(object sender, EventArgs e)
        {
            string[] okFiles;
            int l = 0;
            
            LogFile log = new LogFile();
            log.ПутьиИмяФайла = LogFileName;

            ULogFile uLog = new ULogFile();
            uLog.GetFileList();
            timerUFiles.Interval = 900000;

            if (uLog.Ошибка == null)
            {
                listBoxFoundFiles.Items.Remove("Проверка поступления файлов в архив не выполняется.");
                listBoxFoundFiles.Items.Remove("Файл журнала почты НБУ не доступен или не существует:");
                listBoxFoundFiles.Items.Remove(uLog.ФайлЖурналаПочтыНБУ);

                DirectorySearcher searcher = new DirectorySearcher();

                if (Int32.Parse(DateTime.Now.Hour.ToString()) >= 20)
                {
                    searcher.SearchDirectory(CurrArchivePath, "$K11QK??.G??|$F11QK??.G??|$V11QK??.???");
                    if (searcher.СписокФайлов == null || searcher.СписокФайлов.Count() != 4)
                    {
                        if (uFiles.Count() != uLog.Count)
                        {
                            listBoxFoundFiles.Items.Remove("Зависла Почта НБУ");
                        }
                        else
                        {
                            listBoxFoundFiles.ForeColor = Color.Red;
                            listBoxFoundFiles.Items.Remove("Зависла Почта НБУ");
                            listBoxFoundFiles.Items.Add("Зависла Почта НБУ");
                            log.WriteToLogFile("Зависла Почта НБУ");
                            timerUFiles.Interval = 60000;
                        }
                    }
                    else
                    {
                        listBoxFoundFiles.Items.Remove("Зависла Почта НБУ");
                    }
                    
                }
                else
                {
                    if (uFiles.Count() != uLog.Count)
                    {
                        listBoxFoundFiles.Items.Remove("Зависла Почта НБУ");
                    }
                    else
                    {
                        listBoxFoundFiles.ForeColor = Color.Red;
                        listBoxFoundFiles.Items.Remove("Зависла Почта НБУ");
                        listBoxFoundFiles.Items.Add("Зависла Почта НБУ");
                        log.WriteToLogFile("Зависла Почта НБУ");
                        timerUFiles.Interval = 60000;
                    }
                }
                
                searcher.SearchDirectory(CurrArchivePath, "*.*");
                if (searcher.Ошибка != null)
                {
                    log.WriteToLogFile("Данный архив не доступен или не существует:");
                    log.WriteToLogFile(CurrArchivePath);
                    searcher.SearchDirectory(ArchivePath, "*.*");
                }

                if (searcher.Ошибка != null)
                {
                    listBoxFoundFiles.Items.Remove("Проверка поступления файлов в архив не выполняется.");
                    listBoxFoundFiles.Items.Remove("Данные архивы не доступны или не существуют:");
                    listBoxFoundFiles.Items.Remove(CurrArchivePath);
                    listBoxFoundFiles.Items.Remove(ArchivePath);
                    listBoxFoundFiles.ForeColor = Color.Red;
                    listBoxFoundFiles.Items.Add("Проверка поступления файлов в архив не выполняется.");
                    listBoxFoundFiles.Items.Add("Данные архивы не доступны или не существуют:");
                    listBoxFoundFiles.Items.Add(ArchivePath);
                    listBoxFoundFiles.Items.Add(CurrArchivePath);
                    log.WriteToLogFile("Данный архив не доступен или не существует:");
                    log.WriteToLogFile(ArchivePath);
                    log.WriteToLogFile("Проверка поступления файлов в архив не выполняется.");
                    timerUFiles.Interval = 60000;
                }
                else
                {
                    if (uFiles.Count() != uLog.Count)
                    {
                        listBoxFoundFiles.ForeColor = Color.Black;
                    }
                    listBoxFoundFiles.Items.Remove("Проверка поступления файлов в архив не выполняется.");
                    listBoxFoundFiles.Items.Remove("Данные архивы не доступны или не существуют:");
                    listBoxFoundFiles.Items.Remove(ArchivePath);
                    listBoxFoundFiles.Items.Remove(CurrArchivePath);
                    
                    okFiles = searcher.СписокФайлов;
                    searcher.SearchDirectory(KeyPath, "*.*");
                    if (searcher.Ошибка == null)
                    {
                        okFiles = okFiles.Union(searcher.СписокФайлов).ToArray();
                    }
                    
                    if (LostFiles != null)
                    {
                        foreach (string line in LostFiles)
                        {

                            for (int i = 0; i <= okFiles.Count() - 1; i++)
                            {

                                if (line == okFiles[i].Substring(okFiles[i].Length - 12))
                                {
                                    listBoxFoundFiles.Items.Remove("В архиве не найден файл: " + line);
                                    log.WriteToLogFile("В архив поступил файл: " + line);
                                    LostFiles[l] = null;
                                }
                            }
                            l++;
                        }
                    }
                    l = 0;
                    foreach (string line in uFiles)
                    {
                        for (int i = 0; i <= okFiles.Count() - 1; i++)
                        {

                            if (line == okFiles[i].Substring(okFiles[i].Length - 12))
                            {
                                uFiles[l] = null;
                            }
                        }
                        if (uFiles[l] != null)
                        {
                            listBoxFoundFiles.Items.Remove("В архиве не найден файл: " + line);
                            listBoxFoundFiles.Items.Add("В архиве не найден файл: " + line);
                            log.WriteToLogFile("В архиве не найден файл: " + line);
                            timerUFiles.Interval = 60000;
                        }

                        l++;
                    }
                    LostFiles = uFiles;
                }

                uLog.GetFileList();

                uFiles = uLog.СписокФайлов;
            }
            else
            {
                listBoxFoundFiles.Items.Remove("Проверка поступления файлов в архив не выполняется.");
                listBoxFoundFiles.Items.Remove(uLog.Ошибка);
                listBoxFoundFiles.Items.Remove(uLog.ФайлЖурналаПочтыНБУ);
                listBoxFoundFiles.ForeColor = Color.Red;
                listBoxFoundFiles.Items.Add("Проверка поступления файлов в архив не выполняется.");
                listBoxFoundFiles.Items.Add(uLog.Ошибка);
                listBoxFoundFiles.Items.Add(uLog.ФайлЖурналаПочтыНБУ);
                log.WriteToLogFile("Проверка поступления файлов в архив не выполняется.");
                log.WriteToLogFile(uLog.Ошибка);
                log.WriteToLogFile(uLog.ФайлЖурналаПочтыНБУ);
                timerUFiles.Interval = 60000;
            }

            // Z File
            if (zEnabled == "Да")
            {
                if (Int32.Parse(DateTime.Now.Hour.ToString()) >= 18)
                {
                    zTimer = new System.Timers.Timer();
                    zTimer.Interval = 300000;
                    zTimer.Elapsed += new System.Timers.ElapsedEventHandler(zTimer_Elapsed);
                    zTimer.Enabled = true;
                    zEnabled = "Работает";
                }
                
            }

            if (listBoxFoundFiles.Items.Count != 0)
            {
                if (checkBoxMusik.Checked == true)
                {
                    MusikPlayer.controls.play();
                }
                ShowInTaskbar = true;
                if (WindowState != FormWindowState.Normal)
                {
                    WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                if (checkBoxMusik.Checked == true)
                {
                    MusikPlayer.controls.pause();
                }
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
            }
            GC.Collect();
        }

        void zTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //throw new NotImplementedException();
            if (zEnabled == "Работает")
            {
                ZFile z = new ZFile();
                z.Check(CurrArchivePath);
                if (z.Интервал != 0)
                {
                    zTimer.Interval = z.Интервал;
                    zEnabled = "Нет";
                }
            }
            else
            {
                zTime();
                zTimer.Enabled = false;
                zTimer.Dispose();
            }
        }

        public void zTime()
        {

            if (listBoxFoundFiles.InvokeRequired)
            {
                zTimeCallback d = new zTimeCallback(zTime);
                Invoke(d);
            }
            else
            {
                listBoxFoundFiles.Items.Add("Время отправлять $Z");
                if (checkBoxMusik.Checked == true)
                {
                    MusikPlayer.controls.play();
                }
                ShowInTaskbar = true;
                if (WindowState != FormWindowState.Normal)
                {
                    WindowState = FormWindowState.Normal;
                }

            
                
            }

            
        }

        private void checkBoxUFiles_Click(object sender, EventArgs e)
        {
            LogFile log = new LogFile();
            log.ПутьиИмяФайла = LogFileName;
            ULogFile uLog = new ULogFile();
            uLog.GetUFileName();                    
            if (checkBoxUFiles.Checked == true)
            {
                if (buttonStart.Text == "Стоп")
                {
                    uLog.GetFileList();
                    if (uLog.Ошибка == null)
                    {
                        uFiles = uLog.СписокФайлов;
                        timerUFiles.Enabled = true;
                        log.WriteToLogFile("Запущена проверка поступления файлов в архив.");
                    }
                    else
                    {
                        listBoxFoundFiles.ForeColor = Color.Red;
                        listBoxFoundFiles.Items.Add("Проверка поступления файлов в архив не выполняется.");
                        listBoxFoundFiles.Items.Add(uLog.Ошибка);
                        listBoxFoundFiles.Items.Add(uLog.ФайлЖурналаПочтыНБУ);
                        log.WriteToLogFile("Запущена проверка поступления файлов в архив.");
                        log.WriteToLogFile("Проверка поступления файлов в архив не выполняется.");
                        log.WriteToLogFile(uLog.Ошибка);
                        log.WriteToLogFile(uLog.ФайлЖурналаПочтыНБУ);
                    }
                    if (listBoxFoundFiles.Items.Count != 0)
                    {
                        if (checkBoxMusik.Checked == true)
                        {
                            MusikPlayer.controls.play();
                        }
                    }
                    else 
                    {
                        if (checkBoxMusik.Checked == true)
                        {
                            MusikPlayer.controls.pause();
                        }
                    }
                }
            }
            else
            {
                listBoxFoundFiles.ForeColor = Color.Black;
                if (uFiles != null)
                {
                    for (int i = 0; i <= uFiles.Count() - 1; i++)
                    {
                        listBoxFoundFiles.Items.Remove("В архиве не найден файл: " + uFiles[i]);
                    }
                }
                listBoxFoundFiles.Items.Remove("Зависла Почта НБУ");
                listBoxFoundFiles.Items.Remove("Проверка поступления файлов в архив не выполняется.");
                listBoxFoundFiles.Items.Remove("Файл журнала почты НБУ не доступен или не существует:");
                listBoxFoundFiles.Items.Remove(uLog.ФайлЖурналаПочтыНБУ);
                if (buttonStart.Text == "Стоп")
                {
                    log.WriteToLogFile("Остановлена проверка поступления файлов в архив");
                    if (listBoxFoundFiles.Items.Count != 0)
                    {
                        if (checkBoxMusik.Checked == true)
                        {
                            MusikPlayer.controls.play();
                        }
                    }
                    else
                    {
                        if (checkBoxMusik.Checked == true)
                        {
                            MusikPlayer.controls.pause();
                        }
                    }
                }
                timerUFiles.Enabled = false;
            }

        }

        private void checkBoxMusik_Click(object sender, EventArgs e)
        {
            if (checkBoxMusik.Checked == true)
            {
                checkBoxMusik.Checked = false;
                openFileDialogMusik.ShowDialog();
            }
            else
            {
                MusikPlayer.close();
                
                
            }
        }

        private void buttonCurrency_Click(object sender, EventArgs e)
        {
            listBoxFoundFiles.Items.Remove("Автоматическое архивирование не запущено.");
            listBoxFoundFiles.Items.Remove("Введите Имя пользователя и Пароль.");
            Login login = new Login(DataBase, UserId, Password);
            UserId = login.Пользователь;
            Password = login.Пароль;
            if (Password.Length == 0)
            {
                Password = null;
            }
            DataBase = login.БазаДанных;
            conn = login.СоединениеСБазой;
            if (conn != null && conn.State == ConnectionState.Open)
            {
                Buttons but = new Buttons(conn, Password, UserId, DataBase, LogFileName, 0);
                but.Show();
                buttonDisconnect.Enabled = true;
                labelDataBase.Text = DataBase;
                labelUserName.Text = UserId;
                
            }
            if (checkBoxAutoArchive.Checked == true)
            {
                checkBoxAutoArchive_Click(sender, e);
            }
                        
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            Password = null;
            UserId = null;
            labelDataBase.Text = null;
            labelUserName.Text = null;
            buttonDisconnect.Enabled = false;
            if (checkBoxAutoArchive.Checked == true)
            {
                checkBoxAutoArchive_Click(sender, e);
            }
        }

        private void buttonArchive_Click(object sender, EventArgs e)
        {
            Archive arch = new Archive(LogFileName);
            arch.Show();
        }

        private void checkBoxAutoArchive_Click(object sender, EventArgs e)
        {
            LogFile log = new LogFile();
            
            if (checkBoxAutoArchive.Checked == true)
            {
                
                if (buttonStart.Text == "Стоп")
                {
                    if (Password != null & UserId != null)
                    {
                        checkBoxVypiska.Enabled = true;
                        log.WriteToLogFile(LogFileName, "Включено автоматическое архивирование");
                        AutoArchiveEnabled = "Yes";
                        if (AutoArchive == "Yes")
                        {
                            DoAutoArchive();
                        }
                        else
                        {
                            DirectorySearcher searcher = new DirectorySearcher();
                            searcher.SearchDirectory(CurrArchivePath, "$V11QK??.0??");
                            if (searcher.Ошибка == null && searcher.СписокФайлов != null)
                            {
                                AutoArchive = "Yes";
                                DoAutoArchive();
                            }
                        }
                    }
                    else
                    {
                        listBoxFoundFiles.Items.Add("Автоматическое архивирование не запущено.");
                        listBoxFoundFiles.Items.Add("Введите Имя пользователя и Пароль.");
                        if (vTimer != null)
                        {
                            vTimer.Enabled = false;
                        }
                        if (AutoArchiveEnabled == "Yes")
                        {
                            log.WriteToLogFile(LogFileName, "Автоматическое архивирование выключено");
                            AutoArchiveEnabled = "No";
                        }
                        checkBoxVypiska.Enabled = false;
                        checkBoxVypiska.Checked = false;
                    }
                }
                else
                {
                    if (vTimer != null)
                    {
                        vTimer.Enabled = false;
                    }
                    if (AutoArchiveEnabled == "Yes")
                    {
                        log.WriteToLogFile(LogFileName, "Автоматическое архивирование выключено");
                        AutoArchiveEnabled = "No";
                    }
                    checkBoxVypiska.Enabled = false;
                    checkBoxVypiska.Checked = false;
                }
            }
            else
            {
                if (AutoArchiveEnabled == "Yes")
                {
                    log.WriteToLogFile(LogFileName, "Автоматическое архивирование выключено");
                    AutoArchiveEnabled = "No";
                }
                checkBoxVypiska.Enabled = false;
                checkBoxVypiska.Checked = false;
                listBoxFoundFiles.Items.Remove("Автоматическое архивирование не запущено.");
                listBoxFoundFiles.Items.Remove("Введите Имя пользователя и Пароль.");
                if (vTimer != null)
                {
                    vTimer.Enabled = false;
                }
            }
        }

        private bool MeetConditions()
        {
            DirectorySearcher searcher = new DirectorySearcher();
            searcher.SearchDirectory(CurrArchivePath, "$V11QK??.0??");
            if (searcher.Ошибка == null && searcher.СписокФайлов != null)
            {
                StreamReader reader = new StreamReader(searcher.СписокФайлов[0]);
                reader.BaseStream.Position = 369;
                char[] Flag = new char[1];
                reader.Read(Flag, 0, 1);
                reader.Close();
                if (Flag[0].ToString() == "U")
                {
                    searcher.SearchDirectory(CurrArchivePath, "$K11QK??.G??|$F11QK??.G??|$U11QK??.???|$V11QK??.???");
                    if (searcher.Ошибка != null)
                    {
                        return false;
                    }
                    if (searcher.СписокФайлов == null || searcher.СписокФайлов.Count() != 5)
                    {
                        return false;
                    }
                }
                else
                {
                    searcher.SearchDirectory(CurrArchivePath, "$K11QK??.G??|$F11QK??.G??|$V11QK??.???");
                    if (searcher.Ошибка != null)
                    {
                        return false;
                    }
                    if (searcher.СписокФайлов == null || searcher.СписокФайлов.Count() != 4)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch
                {
                    return false;
                }
                OracleCommand cmd = new OracleCommand("SELECT VAL FROM PARAMS WHERE PAR = 'RRPDAY'", conn);
                OracleDataReader oraReader = cmd.ExecuteReader();
                while (oraReader.Read())
                {
                    if (oraReader.GetValue(0).ToString() != "0")
                    {
                        conn.Close();
                        return false;
                    }

                }
                conn.Close();
            }
            else
            {
                OracleCommand cmd = new OracleCommand("SELECT VAL FROM PARAMS WHERE PAR = 'RRPDAY'", conn);
                OracleDataReader oraReader = cmd.ExecuteReader();
                while (oraReader.Read())
                {
                    if (oraReader.GetValue(0).ToString() != "0")
                    {
                        return false;
                    }
                }
            }
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            XmlNode node;
            char[] buffer;
            string EndFrase;

            if (DoVypiska == "No" | (DoVypiska == "Yes" & AutoArchive == "FirstCheckComlited"))
            {
                node = settings.SelectSingleNode("/Settings/AutoArchive/ArmSepPath");
                string Dir = node.InnerText;
                node = settings.SelectSingleNode("/Settings/AutoArchive/ArmSepNameFormat");
                string Filter = Format(node.InnerText);
                searcher.SearchDirectory(Dir, Filter);
                if (searcher.Ошибка != null)
                {
                    return false;
                }
                if (searcher.СписокФайлов == null || searcher.СписокФайлов.Count() != 1)
                {
                    return false;
                }
                node = settings.SelectSingleNode("/Settings/AutoArchive/ArmNbuPath");
                Dir = node.InnerText;
                node = settings.SelectSingleNode("/Settings/AutoArchive/ArmNbuNameFormat");
                Filter = Format(node.InnerText);
                searcher.SearchDirectory(Dir, Filter);
                if (searcher.Ошибка != null)
                {
                    return false;
                }
                if (searcher.СписокФайлов == null || searcher.СписокФайлов.Count() != 1)
                {
                    return false;
                }
                node = settings.SelectSingleNode("/Settings/AutoArchive/ZeePath");
                Dir = node.InnerText;
                node = settings.SelectSingleNode("/Settings/AutoArchive/ZeeNameFormat");
                Filter = Format(node.InnerText);
                searcher.SearchDirectory(Dir, Filter);
                if (searcher.Ошибка != null)
                {
                    return false;
                }
                if (searcher.СписокФайлов == null || searcher.СписокФайлов.Count() != 1)
                {
                    return false;
                }

                node = settings.SelectSingleNode("/Settings/AutoArchive/ATBULogPath");
                Dir = node.InnerText;
                node = settings.SelectSingleNode("/Settings/AutoArchive/ATBULogNameFormat");
                Filter = Format(node.InnerText);
                if (Dir.EndsWith("\\"))
                {
                    Dir = Dir + Filter;
                }
                else
                {
                    Dir = Dir + "\\" + Filter;
                }

                StreamReader reader2 = new StreamReader(Dir, Encoding.GetEncoding(866));
                reader2.BaseStream.Position = reader2.BaseStream.Length - 44;
                buffer = new char[42];
                reader2.Read(buffer, 0, 42);
                EndFrase = new string(buffer);
                reader2.Close();
                if (EndFrase != "! Монитор КЛИЕНТ-БАНК - завершение работы.")
                {
                    return false;
                }
            }
            node = settings.SelectSingleNode("/Settings/Buttons/TehnolLog");
            if (DoVypiska == "Yes" & AutoArchive == "FirstCheckComlited")
            {
                StreamReader reader3 = new StreamReader(node.InnerText, Encoding.Default);
                buffer = new char[27];
                char[] del_buffer = new char[10];
                reader3.BaseStream.Position = reader3.BaseStream.Length - 38;
                reader3.Read(buffer, 0, 11);
                reader3.Read(del_buffer, 0, 9);
                reader3.Read(buffer, 11, 16);
                EndFrase = new string(buffer);
                reader3.Close();
                if (EndFrase != Format("dd.mm.yyyy Задачи выполнены"))
                {
                    return false;
                } 
            }


            return true; 
        }

        public string Format(string Dir)
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

            Dir = Dir.Replace("dd", Date);
            Dir = Dir.Replace("mm", Month);
            Dir = Dir.Replace("yyyy", DateTime.Now.Year.ToString());
            Dir = Dir.Replace("yy", DateTime.Now.Year.ToString().Substring(2));

            return Dir;
        }

        private void checkBoxVypiska_CheckedChanged(object sender, EventArgs e)
        {
            LogFile log = new LogFile();

            if (checkBoxVypiska.Checked == true)
            {
                DoVypiska = "Yes";
                log.WriteToLogFile(LogFileName, "Включено автоматическое формирование выписки на АС \"Казна-Видатки\"");
            }
            else
            {
                DoVypiska = "No";
                log.WriteToLogFile(LogFileName, "Автоматическое формирование выписки на АС \"Казна-Видатки\" выключено");
            }
        }

        private void buttonMail_Click(object sender, EventArgs e)
        {
            SendVypiska vypiska = new SendVypiska(LogFileName);
            vypiska.Show();
        }

        private void timerMail_Tick(object sender, EventArgs e)
        {
            if (MailListBoxMessages != null)
            {
                for (int i = 0; i < MailListBoxMessages.Count(); i++)
                {
                    listBoxFoundFiles.Items.Remove(MailListBoxMessages[i]);
                }
            }
            LogFile log = new LogFile();
            XmlDocument SendVypiska = new XmlDocument();
            SendVypiska.Load("SendVypiska.xml");
            XmlNodeList nodelst = SendVypiska.SelectNodes("/SendVypiska/Task");
            XmlNode node;
            int[] iProcesses = null;
            for (int i = 1; i <= nodelst.Count; i++)
            {
                node = SendVypiska.SelectSingleNode("/SendVypiska/Task[" + i +"]");
                if (node.Attributes["Enabled"].Value == "Да")
                {
                    if (MomentToSend(i) == "Yes")
                    {
                        if (iProcesses == null)
                        {
                            iProcesses = new int[1];
                            iProcesses[0] = i;
                        }
                        else
                        {
                            int[] l = new int[1];
                            l[0] = i;
                            iProcesses = iProcesses.Union(l).ToArray();
                        }
                    }
                }
            }
            
            if (iProcesses != null)
            {
                log.WriteToLogFile(LogFileName, "Выполняется автоматическая отправка сообщений");
                log.WriteToLogFile(LogFileName, "Количество заданий: " + iProcesses.Count());
                for (int i = 1; i <= nodelst.Count; i++)
                {
                    node = SendVypiska.SelectSingleNode("/SendVypiska/Task[" + i + "]");
                    log.WriteToLogFile(LogFileName, "Название заданий: " + node.Attributes["Name"].Value);
                    
                }
                SendVypiskaProcess SndVypProc = new SendVypiskaProcess(iProcesses);
                MailListBoxMessages = SndVypProc.СообщенияОВыполнении;
                foreach (string line in MailListBoxMessages)
                {
                    log.WriteToLogFile(LogFileName, line); 
                }
                for (int i = 0; i < MailListBoxMessages.Count(); i++)
                {
                    if (MailListBoxMessages[i].Length >= 36)
                    {
                        if (MailListBoxMessages[i].Substring(MailListBoxMessages[i].Length - 36) != "Отправка не выполнена, нет вложений.")
                        {
                            listBoxFoundFiles.Items.Add(MailListBoxMessages[i]);
                        }
                    }
                    else
                    {
                        listBoxFoundFiles.Items.Add(MailListBoxMessages[i]);
                    }
                }
            }

            if (listBoxFoundFiles.Items.Count != 0)
            {
                if (checkBoxMusik.Checked == true)
                {
                    MusikPlayer.controls.play();
                }
                ShowInTaskbar = true;
                if (WindowState != FormWindowState.Normal)
                {
                    WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                if (checkBoxMusik.Checked == true)
                {
                    MusikPlayer.controls.pause();
                }
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
            }
        }

        private string MomentToSend(int NodeNumber)
        {
            XmlDocument sendVypiska = new XmlDocument();
            sendVypiska.Load("SendVypiska.xml");
            XmlNode node = sendVypiska.SelectSingleNode("/SendVypiska/Task[" + NodeNumber + "]/Time");
            int Hour, Minute;
            if (node.InnerText.Substring(0, 1) == "0")
            {
                Hour = Int32.Parse(node.InnerText.Substring(1, 1));
            }
            else
            {
                Hour = Int32.Parse(node.InnerText.Substring(0, 2));
            }
            if (node.InnerText.Substring(3, 1) == "0")
            {
                Minute = Int32.Parse(node.InnerText.Substring(4, 1));
            }
            else
            {
                Minute = Int32.Parse(node.InnerText.Substring(3, 2));
            }
            if (DateTime.Now.Hour != Hour | DateTime.Now.Minute != Minute)
            {
                return "No";
            }
            int WeekEnd;
            if (DateTime.Now.DayOfYear > LastDay)
            {
                WeekEnd = DateTime.Now.DayOfYear - LastDay;
            }
            else
            {
                WeekEnd = DateTime.Now.DayOfYear;
            }

            XmlNodeList nodelst = sendVypiska.SelectNodes("/SendVypiska/Task[" + NodeNumber + "]/Schedule/Day");
            foreach (XmlNode node1 in nodelst)
            {
                if (WeekEnd >= 3 & node1.InnerText == Дней.Понедельник.ToString())
                {
                    return "Yes";
                }
                
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday & node1.InnerText == Дней.Понедельник.ToString())
                {
                    return "Yes";
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday & node1.InnerText == Дней.Вторник.ToString())
                {
                    return "Yes";
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday & node1.InnerText == Дней.Среда.ToString())
                {
                    return "Yes";
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday & node1.InnerText == Дней.Четверг.ToString())
                {
                    return "Yes";
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday & node1.InnerText == Дней.Пятница.ToString())
                {
                    return "Yes";
                }
            }
            return "No";

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void checkBoxAutoMail_Click(object sender, EventArgs e)
        {
            LogFile log = new LogFile();

            if (checkBoxAutoMail.Checked == true)
            {
                if (buttonStart.Text == "Стоп")
                {
                    timerMail.Enabled = true;
                    log.WriteToLogFile(LogFileName, "Включена автоматическая отправка сообщений");
                }
            }
            else
            {
                if (buttonStart.Text == "Стоп")
                {
                    timerMail.Enabled = false;
                    log.WriteToLogFile(LogFileName, "Автоматическая отправка сообщений выключена");
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyData == Keys.F1)
                {
                    DirectorySearcher searcher = new DirectorySearcher();
                    searcher.SearchDirectory(Directory.GetCurrentDirectory(), "AlarmDogHlp.chm", SearchOption.TopDirectoryOnly);
                    if (searcher.СписокФайлов != null)
                    {    
                            Help.ShowHelp(this, "AlarmDogHlp.chm", HelpNavigator.TableOfContents);
                    }
                    else
                    {
                        MessageBox.Show("Отсутствует файл справки  " + Directory.GetCurrentDirectory() + "AlarmDogHlp.chm", "Ошибка");    
                    }
                }
            }
            
                
            
        }
        
    }

    /// <summary>
    /// Класс задания для AlarmDog
    /// </summary>
    public class Task
    {
        private string path;
        private string mask;
        private string name;
        private YesNo onOff;

        /// <summary>
        /// Директория, в которой проводится наблюдение за появлением файлов.
        /// </summary>
        [Browsable(true),
        Editor(typeof(System.Windows.Forms.Design.FolderNameEditor),typeof(System.Drawing.Design.UITypeEditor)),       
        Description("Директория, в которой проводится наблюдение за появлением файлов."),
        Category("Критерии наблюдения")]
        public string  Путь
        { 
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// Маска для фильтрации файлов по имени
        /// </summary>
        [Description("Маска для фильтрации файлов по имени."),
        Category("Критерии наблюдения")]
        public string Маска
        {
            get { return mask; }
            set { mask = value; }
        }

        /// <summary>
        /// Название задания.
        /// </summary>
        [Description("Название задания."),
        Category("Название задания")]
        public string Название
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Указывает, включено ли задание.
        /// </summary>
        [Description("Указывает, включено ли задание."), 
        Category("Параметры работы")]
        public YesNo Включено
        {
            get { return onOff; }
            set { onOff = value; }
        }

                   
    }

    /// <summary>
    /// Содержит два значения "Нет" и "Да"
    /// </summary>
    public enum YesNo
    {
        Нет, Да 
    }


