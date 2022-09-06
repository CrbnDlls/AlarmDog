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
    public partial class Archive : Form
    {
        string LogFileName;

        public Archive(string LogName)
        {
            InitializeComponent();
            ToolTip tip_1 = new ToolTip();
            tip_1.SetToolTip(buttonAdd, "Добавить задание определенного типа");
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
            ToolTip tip_7 = new ToolTip();
            tip_7.SetToolTip(trackBarTaskType, "Передвинте ползунок для выбора необходимого типа задания");
            LoadData();
            if (checkedListBoxTasks.Items.Count != 0)
            {
                checkedListBoxTasks.SetSelected(0, true);
            }
            LogFileName = LogName;
            
        }

        private void LoadData()
        {
            if (!File.Exists("Archive.xml"))
            {
                using (XmlWriter writer = XmlWriter.Create("Archive.xml"))
                {
                    // Write XML data.
                    writer.WriteStartElement("Archive");
                    writer.WriteEndElement();

                    writer.Flush();
                }
            }
            XmlDocument archive = new XmlDocument();
            archive.Load("Archive.xml");
            XmlNodeList nodelst = archive.SelectNodes("/Archive/Task");
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
                    XmlDocument archive = new XmlDocument();
                    archive.Load("Archive.xml");
                    XmlNode node = archive.SelectSingleNode("/Archive");
                    node.RemoveChild(archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]"));
                    archive.Save("Archive.xml");
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
                        labelSelectedTaskName.Text = null;
                        labelSelectedTaskType.Text = null;
                        propertyGridTaskDescription.SelectedObject = null;
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
            XmlDocument archive = new XmlDocument();
            archive.Load("Archive.xml");
            XmlNode node = archive.DocumentElement;
            XmlElement task = archive.CreateElement("Task");
            task.SetAttribute("Name", "Название задания");
            task.SetAttribute("Enabled", "Нет");
            XmlElement element = archive.CreateElement("Type");
            element.InnerText = labelTaskType.Text;
            task.AppendChild(element);
            //CopyFile MoveFile
            if (element.InnerText == "Копировать файл" | element.InnerText == "Переместить файл")
            {
                element = archive.CreateElement("FromPath");
                task.AppendChild(element);
                element = archive.CreateElement("Filter");
                task.AppendChild(element);
                element = archive.CreateElement("ToPath");
                task.AppendChild(element);
                element = archive.CreateElement("ReWrite");
                task.AppendChild(element);
            }
            //DeleteFile
            if (element.InnerText == "Удалить файл")
            {
                element = archive.CreateElement("FromPath");
                task.AppendChild(element);
                element = archive.CreateElement("Filter");
                task.AppendChild(element);
                element = archive.CreateElement("Recursive");
                element.InnerText = "Верхняя папка";
                task.AppendChild(element);
            }
            //CopyDirectory MoveDirectory
            if (element.InnerText == "Копировать каталог" | element.InnerText == "Переместить каталог")
            {
                element = archive.CreateElement("FromPath");
                task.AppendChild(element);
                element = archive.CreateElement("Filter");
                task.AppendChild(element);
                element = archive.CreateElement("ToPath");
                task.AppendChild(element);
                element = archive.CreateElement("ReWrite");
                task.AppendChild(element);
            }
            //DeleteDirectory
            if (element.InnerText == "Удалить каталог")
            {
                element = archive.CreateElement("FromPath");
                task.AppendChild(element);
            }

            node.AppendChild(task);
            archive.Save("Archive.xml");
        }

        private void trackBarTaskType_Scroll(object sender, EventArgs e)
        {
            if (trackBarTaskType.Value == 0)
                labelTaskType.Text = "Копировать файл";
            if (trackBarTaskType.Value == 1)
                labelTaskType.Text = "Переместить файл";
            if (trackBarTaskType.Value == 2)
                labelTaskType.Text = "Удалить файл";
            if (trackBarTaskType.Value == 3)
                labelTaskType.Text = "Копировать каталог";
            if (trackBarTaskType.Value == 4)
                labelTaskType.Text = "Переместить каталог";
            if (trackBarTaskType.Value == 5)
                labelTaskType.Text = "Удалить каталог";

        }


        private void checkedListBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxTasks.SelectedIndex != -1)
            {
                XmlDocument archive = new XmlDocument();
                archive.Load("Archive.xml");

                XmlNode node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Type");
                labelSelectedTaskType.Text = node.InnerText;
                
                //CopyFile MoveFile
                if (node.InnerText == "Копировать файл" | node.InnerText == "Переместить файл")
                {
                    CopyFile task = new CopyFile();
                    task.Тип = node.InnerText;
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                    task.Название = node.Attributes["Name"].Value;
                    labelSelectedTaskName.Text = NextLine(node.Attributes["Name"].Value, 36);
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/FromPath");
                    task.Источник = node.InnerText;
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Filter");
                    task.Маска = node.InnerText;
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/ToPath");
                    task.Приемник = node.InnerText;
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/ReWrite");
                    if (node.InnerText == "Да")
                    {
                        task.Перезапись = YesNo.Да;
                    }
                    propertyGridTaskDescription.SelectedObject = task;
                }
                //DeleteFile
                if (node.InnerText == "Удалить файл")
                {
                    DeleteFile task = new DeleteFile();
                    task.Тип = node.InnerText;
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                    task.Название = node.Attributes["Name"].Value;
                    labelSelectedTaskName.Text = NextLine(node.Attributes["Name"].Value, 36);
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/FromPath");
                    task.Источник = node.InnerText;
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Filter");
                    task.Маска = node.InnerText;
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Recursive");
                    if (node.InnerText == "ВложенныеПапки")
                    {
                        task.Вложенность = Recurse.ВложенныеПапки;
                    }
                    propertyGridTaskDescription.SelectedObject = task;
                }
                //CopyDirectory MoveDirectory
                if (node.InnerText == "Копировать каталог" | node.InnerText == "Переместить каталог")
                {
                    CopyDirectory task = new CopyDirectory();
                    task.Тип = node.InnerText;
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                    task.Название = node.Attributes["Name"].Value;
                    labelSelectedTaskName.Text = NextLine(node.Attributes["Name"].Value, 36);
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/FromPath");
                    task.Источник = node.InnerText;
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/ToPath");
                    task.Приемник = node.InnerText;
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/ReWrite");
                    if (node.InnerText == "Да")
                    {
                        task.Перезапись = YesNo.Да;
                    }
                    propertyGridTaskDescription.SelectedObject = task;
                }
                //DeleteDirectory
                if (node.InnerText == "Удалить каталог")
                {
                    DeleteDirectory task = new DeleteDirectory();
                    task.Тип = node.InnerText;
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                    task.Название = node.Attributes["Name"].Value;
                    labelSelectedTaskName.Text = NextLine(node.Attributes["Name"].Value,36);
                    node = archive.SelectSingleNode("Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/FromPath");
                    task.Источник = node.InnerText;
                    propertyGridTaskDescription.SelectedObject = task;
                }
                
            }
        }   
        
        private void propertyGridTaskDescription_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            XmlDocument archive = new XmlDocument();
            XmlNode node;
            archive.Load("Archive.xml");

            if (propertyGridTaskDescription.SelectedGridItem.Label == "Название")
            {
                node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                node.Attributes["Name"].Value = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
                labelSelectedTaskName.Text = NextLine(propertyGridTaskDescription.SelectedGridItem.Value.ToString(),36);
                archive.Save("Archive.xml");
                checkedListBoxTasks.Items[checkedListBoxTasks.SelectedIndex] = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }

            if (propertyGridTaskDescription.SelectedGridItem.Label == "Приемник")
            {
                node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/ToPath");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }

            if (propertyGridTaskDescription.SelectedGridItem.Label == "Источник")
            {
                node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/FromPath");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }

            if (propertyGridTaskDescription.SelectedGridItem.Label == "Маска")
            {
                node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Filter");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }

            if (propertyGridTaskDescription.SelectedGridItem.Label == "Вложенность")
            {
                node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/Recursive");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }

            if (propertyGridTaskDescription.SelectedGridItem.Label == "Перезапись")
            {
                node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]/ReWrite");
                node.InnerText = propertyGridTaskDescription.SelectedGridItem.Value.ToString();
            }
            archive.Save("Archive.xml");
        }

        private void checkedListBoxTasks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxTasks.SelectedIndex != -1)
            {
                XmlDocument archive = new XmlDocument();
                archive.Load("Archive.xml");
                XmlNode node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                if (checkedListBoxTasks.CheckedIndices.Contains(checkedListBoxTasks.SelectedIndex))
                {
                    node.Attributes["Enabled"].InnerText = "Нет";
                }
                else
                {
                    node.Attributes["Enabled"].InnerText = "Да";
                }
                archive.Save("Archive.xml");
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
                    log.WriteToLogFile(LogFileName, "Запущен процесс архивирования");
                    log.WriteToLogFile(LogFileName, "Количество заданий: " + i.Count());
                    foreach (int l in i)
                    {
                        log.WriteToLogFile(LogFileName, "Название задания: " + checkedListBoxTasks.Items[(l - 1)].ToString());
                    }
                    ArchiveProcess arch = new ArchiveProcess(i);
                    arch.ShowDialog();
                    log.WriteToLogFile(LogFileName, "Сформирован файл журнала архива: " + arch.ЖурналАрхива);
                    foreach (string line in arch.СписокОшибок)
                    {
                        log.WriteToLogFile(LogFileName, line);
                    }
                    ErrorForm msg = new ErrorForm(arch.СписокОшибок, "Результаты архивирования");
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
                XmlDocument archive = new XmlDocument();
                archive.Load("Archive.xml");
                XmlNode node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                InnerXml_1 = node.InnerXml;
                Name_1 = node.Attributes["Name"].Value;
                Enabled_1 = node.Attributes["Enabled"].Value;
                node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex) + "]");
                InnerXml_2 = node.InnerXml;
                Name_2 = node.Attributes["Name"].Value;
                Enabled_2 = node.Attributes["Enabled"].Value;
                node.InnerXml = InnerXml_1;
                node.Attributes["Name"].Value = Name_1;
                node.Attributes["Enabled"].Value = Enabled_1;
                node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                node.InnerXml = InnerXml_2;
                node.Attributes["Name"].Value = Name_2;
                node.Attributes["Enabled"].Value = Enabled_2;
                archive.Save("Archive.xml");
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
                XmlDocument archive = new XmlDocument();
                archive.Load("Archive.xml");
                XmlNode node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                InnerXml_1 = node.InnerXml;
                Name_1 = node.Attributes["Name"].Value;
                Enabled_1 = node.Attributes["Enabled"].Value;
                node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 2) + "]");
                InnerXml_2 = node.InnerXml;
                Name_2 = node.Attributes["Name"].Value;
                Enabled_2 = node.Attributes["Enabled"].Value;
                node.InnerXml = InnerXml_1;
                node.Attributes["Name"].Value = Name_1;
                node.Attributes["Enabled"].Value = Enabled_1;
                node = archive.SelectSingleNode("/Archive/Task[" + (checkedListBoxTasks.SelectedIndex + 1) + "]");
                node.InnerXml = InnerXml_2;
                node.Attributes["Name"].Value = Name_2;
                node.Attributes["Enabled"].Value = Enabled_2;
                archive.Save("Archive.xml");
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
                    int[] l = new int[1];
                    l[0] = checkedListBoxTasks.SelectedIndex + 1;
                    LogFile log = new LogFile();
                    log.WriteToLogFile(LogFileName, "Запущен процесс архивирования");
                    log.WriteToLogFile(LogFileName, "Количество заданий: 1");
                    log.WriteToLogFile(LogFileName, "Название задания: " + checkedListBoxTasks.SelectedItem.ToString());
                    ArchiveProcess arch = new ArchiveProcess(l);
                    arch.ShowDialog();
                    log.WriteToLogFile(LogFileName, "Сформирован файл журнала архива: " + arch.ЖурналАрхива);
                    foreach (string line in arch.СписокОшибок)
                    {
                        log.WriteToLogFile(LogFileName, line);
                    }
                    ErrorForm msg = new ErrorForm(arch.СписокОшибок, "Результаты архивирования");
                    msg.ShowDialog();
                }
            }
        }
        private string NextLine(string Line, int Lenth)
        {
            if (Line.Length > Lenth)
            {
                if (Line.Substring(0, Lenth).LastIndexOf(" ") != -1)
                {
                    Line = Line.Substring(0, Line.Substring(0, Lenth).LastIndexOf(" ")) + "\r" + Line.Substring(Line.Substring(0, Lenth).LastIndexOf(" ")+ 1);
                }
            }
            return Line;
        }
    }
    public class CopyFile
    {
        private string Type;
        private string Name;
        private string FromPath;
        private string Filter;
        private string ToPath;
        private YesNo ReWrite;

        [Browsable(true),
        Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)),
        Description("Каталог, из которого происходит копирование/перемещение файлов. dd - день, mm - месяц, yy или yyyy - год."),
        Category("Параметры задания")]
        public string Источник
        {
            get { return FromPath; }
            set { FromPath = value; }
        }

        [Description("Имя файла или маска по которой будут отбиратся файлы для копирования/перемещения. dd - день, mm - месяц, yy или yyyy - год."),
        Category("Параметры задания")]
        public string Маска
        {
            get { return Filter; }
            set { Filter = value; }
        }

        [Browsable(true),
        Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)),
        Description("Каталог, в который происходит копирование файлов.  dd - день, mm - месяц, yy или yyyy - год."),
        Category("Параметры задания")]
        public string Приемник
        {
            get { return ToPath; }
            set { ToPath = value; }
        }

        [Description("Перезаписывать файлы с одинаковым именем."),
        Category("Параметры задания")]
        public YesNo Перезапись
        {
            get { return ReWrite; }
            set { ReWrite = value; }
        }

        [Description("Название задания."),
        Category("Параметры задания")]
        public string Название
        {
            get { return Name; }
            set { Name = value; }
        }


        [ReadOnly(true),
        Description("Тип задания."),
        Category("Параметры задания")]
        public string Тип
        {
            get { return Type; }
            set { Type = value; }
        }
    }

    public class DeleteFile
    {
        private string Type;
        private string Name;
        private string FromPath;
        private string Filter;
        private Recurse Recursive;

        [Browsable(true),
        Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)),
        Description("Верхний каталог, из которого происходит удаление файлов. dd - день, mm - месяц, yy или yyyy - год."),
        Category("Параметры задания")]
        public string Источник
        {
            get { return FromPath; }
            set { FromPath = value; }
        }

        [Description("Имя файла или маска по которой будут отбиратся файлы для удаления. dd - день, mm - месяц, yy или yyyy - год."),
        Category("Параметры задания")]
        public string Маска
        {
            get { return Filter; }
            set { Filter = value; }
        }

        [Description("Удалять из верхней папки или из верхней папки и ее подпапок."),
        Category("Параметры задания")]
        public Recurse Вложенность
        {
            get { return Recursive; }
            set { Recursive = value; }
        }

        [Description("Название задания."),
        Category("Параметры задания")]
        public string Название
        {
            get { return Name; }
            set { Name = value; }
        }

        [ReadOnly(true),
        Description("Тип задания."),
        Category("Параметры задания")]
        public string Тип
        {
            get { return Type; }
            set { Type = value; }
        }
    }
    public enum Recurse 
    {
        ВерхняяПапка, ВложенныеПапки
    }

    public class CopyDirectory
    {
        private string Type;
        private string Name;
        private string FromPath;
        private string ToPath;
        private YesNo ReWrite;

        [Browsable(true),
        Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)),
        Description("Каталог, который копируем/перемещаем. dd - день, mm - месяц, yy или yyyy - год."),
        Category("Параметры задания")]
        public string Источник
        {
            get { return FromPath; }
            set { FromPath = value; }
        }

        [Browsable(true),
        Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)),
        Description("Каталог, в который копируем/перемещаем другой каталог. dd - день, mm - месяц, yy или yyyy - год."),
        Category("Параметры задания")]
        public string Приемник
        {
            get { return ToPath; }
            set { ToPath = value; }
        }

        [Description("Название задания."),
        Category("Параметры задания")]
        public string Название
        {
            get { return Name; }
            set { Name = value; }
        }

        [Description("Перезаписывать файлы с одинаковым именем."),
        Category("Параметры задания")]
        public YesNo Перезапись
        {
            get { return ReWrite; }
            set { ReWrite = value; }
        }

        [ReadOnly(true),
        Description("Тип задания."),
        Category("Параметры задания")]
        public string Тип
        {
            get { return Type; }
            set { Type = value; }
        }
    }

    public class DeleteDirectory
    {
        private string Type;
        private string Name;
        private string FromPath;

        [Browsable(true),
        Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)),
        Description("Каталог, который удаляем. dd - день, mm - месяц, yy или yyyy - год."),
        Category("Параметры задания")]
        public string Источник
        {
            get { return FromPath; }
            set { FromPath = value; }
        }

        [Description("Название задания."),
        Category("Параметры задания")]
        public string Название
        {
            get { return Name; }
            set { Name = value; }
        }

        [ReadOnly(true),
        Description("Тип задания."),
        Category("Параметры задания")]
        public string Тип
        {
            get { return Type; }
            set { Type = value; }
        }
    }
}
