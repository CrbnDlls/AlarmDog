using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;

namespace AlarmDog
{
    public partial class ArchiveProcess : Form
    {
        int[] g;
        string ArchiveLogName;
        string[] ErrorList;
        delegate void SetTextCallback(string text);
        delegate void SetStepCallback(int step);
        delegate void PerformStepProcCallback();
        delegate void PerformStepAllCallback();
        delegate void ProgressBarProcessValueCallback(int position);
        delegate void DisposeCallback(bool disposing);
        delegate void SetLableTextCallback(string from, string to);

        public string[] СписокОшибок
        {
            get { return ErrorList; }
        }
        
        public string ЖурналАрхива
        {
            get { return ArchiveLogName; }
        }

        public ArchiveProcess(int[] iProcess)
        {
            InitializeComponent();
            progressBarProcess.Refresh();
            progressBarAllProcesses.Refresh();
            labelTotalProgress.Refresh();
            progressBarAllProcesses.Step = 100 / iProcess.Count();
            g = iProcess;
            Thread arch = new Thread(new ThreadStart(Execute));
            arch.Start();
        }

        public void Execute()
        {
            string Filter, FromPath, ToPath;
            
            string[] Dir;
            int indx;
            ErrorList = new string[g.Count()];
            int TaskNumber = 0;
            XmlDocument archive = new XmlDocument();
            archive.Load("Archive.xml");
            XmlNode node;
            DirectorySearcher searcher = new DirectorySearcher();
            LogFile log = new LogFile();
            log.ПутьиИмяФайла = log.GetLogFileName("yyyymmdd_arch.log");
            ArchiveLogName = log.ПутьиИмяФайла;
            log.WriteToLogFile("Запущен процесс архивации");
            foreach (int i in g)
            {
                YesNo ReWrite = YesNo.Нет;

                SearchOption SerOp = SearchOption.TopDirectoryOnly;
                searcher.КритерийПоиска = SearchOption.TopDirectoryOnly;
                ProgressBarProcessValue(0);

                node = archive.SelectSingleNode("/Archive/Task[" + i + "]/Type");
                if (node.InnerText == "Копировать файл")
                {
                    log.WriteToLogFile("Тип задания: " + node.InnerText);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]");
                    SetText(node.Attributes["Name"].Value.ToString());
                    log.WriteToLogFile("Название задания: " + node.Attributes["Name"].Value.ToString());
                    ErrorList[TaskNumber] = node.Attributes["Name"].Value.ToString() + ": ";
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/FromPath");
                    FromPath = Format(node.InnerText);
                    log.WriteToLogFile("Из: " + FromPath);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/Filter");
                    Filter = Format(node.InnerText);
                    log.WriteToLogFile("Маска: " + Filter);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/ToPath");
                    ToPath = Format(node.InnerText);
                    log.WriteToLogFile("В: " + ToPath);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/ReWrite");
                    if (node.InnerText == "Да")
                    {
                        ReWrite = YesNo.Да;
                    }
                    log.WriteToLogFile("Перезапись: " + ReWrite.ToString());
                    
                    if (!Directory.Exists(ToPath))
                    {
                        log.WriteToLogFile("Директории не существует: " + ToPath);  
                        try
                        {
                            Directory.CreateDirectory(ToPath);
                            log.WriteToLogFile("Создана директория: " + ToPath);
                        }
                        catch (Exception e)
                        {
                            log.WriteToLogFile("Не могу создать директорию: " + ToPath);
                            log.WriteToLogFile(e.Message);
                            log.WriteToLogFile("Задание не выполнено");
                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание не выполнено";
                        }
                    }

                    if (Directory.Exists(ToPath))
                    {
                        searcher.SearchDirectory(FromPath, Filter);
                        if (searcher.Ошибка == null & searcher.СписокФайлов != null)
                        {

                            int quantity;
                            if (searcher.СписокФайлов.Count() >= 100)
                            {
                                quantity = searcher.СписокФайлов.Count() / 100;
                                SetStep(1);
                            }
                            else
                            {
                                quantity = 1;
                                SetStep(100 / searcher.СписокФайлов.Count());
                            }


                            int Cnt = 0;
                            int Cnt2 = 1;
                            int ErrorCnt = 0;
                            foreach (string line in searcher.СписокФайлов)
                            {
                                SetLableText(line, ToPath + line.Substring(line.LastIndexOf("\\")));
                                if (ReWrite == YesNo.Да)
                                {
                                    if (File.Exists(ToPath + line.Substring(line.LastIndexOf("\\"))))
                                    {
                                        try
                                        {
                                            if (File.GetAttributes(ToPath + line.Substring(line.LastIndexOf("\\"))).ToString().Contains("ReadOnly"))
                                            {
                                                File.SetAttributes(ToPath + line.Substring(line.LastIndexOf("\\")), FileAttributes.Normal);
                                                log.WriteToLogFile("Изменен атрибут файла: " + ToPath + line.Substring(line.LastIndexOf("\\")));
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу изменить атрибут файла: " + ToPath + line.Substring(line.LastIndexOf("\\")));
                                            log.WriteToLogFile(e.Message);
                                        }
                                    }
                                    try
                                    {
                                        File.Copy(line, ToPath + line.Substring(line.LastIndexOf("\\")), true);
                                        log.WriteToLogFile("Файл: " + line);
                                        log.WriteToLogFile("Скопирован: " + ToPath + line.Substring(line.LastIndexOf("\\")));
                                    }
                                    catch (Exception e)
                                    {
                                        log.WriteToLogFile("Не могу скопировать файл: " + line);
                                        log.WriteToLogFile(e.Message);
                                        ErrorCnt = ErrorCnt + 1;
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        File.Copy(line, ToPath + line.Substring(line.LastIndexOf("\\")), false);
                                        log.WriteToLogFile("Файл: " + line);
                                        log.WriteToLogFile("Скопирован: " + ToPath + line.Substring(line.LastIndexOf("\\")));
                                    }
                                    catch (Exception e)
                                    {
                                        log.WriteToLogFile("Не могу скопировать файл: " + line);
                                        log.WriteToLogFile(e.Message);
                                        ErrorCnt = ErrorCnt + 1;
                                    }
                                }
                                Cnt = Cnt + 1;
                                if (Cnt == quantity * Cnt2)
                                {
                                    PerformStepProc();
                                    Cnt2 = Cnt2 + 1;
                                }
                                if (Cnt == searcher.СписокФайлов.Count())
                                {
                                    if (ErrorCnt != 0)
                                    {
                                        ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание выполнено c " + ErrorCnt + " ошибкой(ми)";
                                        log.WriteToLogFile("Задание выполнено c " + ErrorCnt + " ошибкой(ми)");
                                    }
                                    else
                                    {
                                        log.WriteToLogFile("Задание выполнено");
                                        ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание выполнено";
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (searcher.Ошибка == null)
                            {
                                log.WriteToLogFile("Нет файлов для копирования");
                                log.WriteToLogFile("Задание выполнено");
                                ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Нет файлов для копирования";
                            }
                            else
                            {
                                foreach (string line in searcher.Ошибка)
                                {
                                    log.WriteToLogFile(line);
                                }
                                log.WriteToLogFile("Задание не выполнено");
                                ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание не выполнено";
                            }
                        }
                    }
                }

                if (node.InnerText == "Переместить файл")
                {
                    log.WriteToLogFile("Тип задания: " + node.InnerText);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]");
                    SetText(node.Attributes["Name"].Value.ToString());
                    log.WriteToLogFile("Название задания: " + node.Attributes["Name"].Value.ToString());
                    ErrorList[TaskNumber] = node.Attributes["Name"].Value.ToString() + ": ";
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/FromPath");
                    FromPath = Format(node.InnerText);
                    log.WriteToLogFile("Из: " + FromPath);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/Filter");
                    Filter = Format(node.InnerText);
                    log.WriteToLogFile("Маска: " + Filter);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/ToPath");
                    ToPath = Format(node.InnerText);
                    log.WriteToLogFile("В: " + ToPath);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/ReWrite");
                    if (node.InnerText == "Да")
                    {
                        ReWrite = YesNo.Да;
                    }
                    log.WriteToLogFile("Перезапись: " + ReWrite.ToString());

                    if (!Directory.Exists(ToPath))
                    {
                        log.WriteToLogFile("Директории не существует: " + ToPath);
                        try
                        {
                            Directory.CreateDirectory(ToPath);
                            log.WriteToLogFile("Создана директория: " + ToPath);
                        }
                        catch (Exception e)
                        {
                            log.WriteToLogFile("Не могу создать директорию: " + ToPath);
                            log.WriteToLogFile(e.Message);
                            log.WriteToLogFile("Задание не выполнено");
                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание не выполнено";
                        }
                    }

                    if (Directory.Exists(ToPath))
                    {
                        searcher.SearchDirectory(FromPath, Filter);
                        if (searcher.Ошибка == null & searcher.СписокФайлов != null)
                        {
                            int quantity;
                            if (searcher.СписокФайлов.Count() >= 100)
                            {
                                quantity = searcher.СписокФайлов.Count() / 100;
                                SetStep(1);
                            }
                            else
                            {
                                quantity = 1;
                                SetStep(100 / searcher.СписокФайлов.Count());
                            }


                            int Cnt = 0;
                            int Cnt2 = 1;
                            int ErrorCnt = 0;
                            foreach (string line in searcher.СписокФайлов)
                            {
                                int Switch = 0;

                                SetLableText(line, ToPath + line.Substring(line.LastIndexOf("\\")));
                                if (ReWrite == YesNo.Да)
                                {
                                    if (File.Exists(ToPath + line.Substring(line.LastIndexOf("\\"))))
                                    {
                                        try
                                        {
                                            if (File.GetAttributes(ToPath + line.Substring(line.LastIndexOf("\\"))).ToString().Contains("ReadOnly"))
                                            {
                                                File.SetAttributes(ToPath + line.Substring(line.LastIndexOf("\\")), FileAttributes.Normal);
                                                log.WriteToLogFile("Изменен атрибут файла: " + ToPath + line.Substring(line.LastIndexOf("\\")));
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу изменить атрибут файла: " + ToPath + line.Substring(line.LastIndexOf("\\")));
                                            log.WriteToLogFile(e.Message);
                                        }
                                    }

                                    try
                                    {
                                        File.Copy(line, ToPath + line.Substring(line.LastIndexOf("\\")), true);
                                        Switch = 1;
                                        log.WriteToLogFile("Файл: " + line);
                                        log.WriteToLogFile("Скопирован: " + ToPath + line.Substring(line.LastIndexOf("\\")));
                                    }
                                    catch (Exception e)
                                    {
                                        log.WriteToLogFile("Не могу скопировать файл: " + line);
                                        log.WriteToLogFile(e.Message);
                                        ErrorCnt = ErrorCnt + 1;
                                    }

                                    if (File.Exists(ToPath + line.Substring(line.LastIndexOf("\\"))) & Switch == 1)
                                    {
                                        try
                                        {
                                            if (File.GetAttributes(line).ToString().Contains("ReadOnly"))
                                            {
                                                File.SetAttributes(line, FileAttributes.Normal);
                                                log.WriteToLogFile("Изменен атрибут файла: " + line);
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу изменить атрибут файла: " + line);
                                            log.WriteToLogFile(e.Message);
                                        }

                                        try
                                        {
                                            File.Delete(line);
                                            log.WriteToLogFile("Удален: " + line);
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу удалить файл: " + line);
                                            log.WriteToLogFile(e.Message);
                                            ErrorCnt = ErrorCnt + 1;
                                        }
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        File.Copy(line, ToPath + line.Substring(line.LastIndexOf("\\")), false);
                                        Switch = 1;
                                        log.WriteToLogFile("Файл: " + line);
                                        log.WriteToLogFile("Скопирован: " + ToPath + line.Substring(line.LastIndexOf("\\")));
                                    }
                                    catch (Exception e)
                                    {
                                        log.WriteToLogFile("Не могу скопировать файл: " + line);
                                        log.WriteToLogFile(e.Message);
                                        ErrorCnt = ErrorCnt + 1;
                                    }

                                    if (File.Exists(ToPath + line.Substring(line.LastIndexOf("\\"))) & Switch == 1)
                                    {
                                        try
                                        {
                                            if (File.GetAttributes(line).ToString().Contains("ReadOnly"))
                                            {
                                                File.SetAttributes(line, FileAttributes.Normal);
                                                log.WriteToLogFile("Изменен атрибут файла: " + line);
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу изменить атрибут файла: " + line);
                                            log.WriteToLogFile(e.Message);
                                        }

                                        try
                                        {
                                            File.Delete(line);
                                            log.WriteToLogFile("Удален: " + line);
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу удалить файл: " + line);
                                            log.WriteToLogFile(e.Message);
                                            ErrorCnt = ErrorCnt + 1;
                                        }
                                    }
                                }
                                Cnt = Cnt + 1;
                                if (Cnt == quantity * Cnt2)
                                {
                                    PerformStepProc();
                                    Cnt2 = Cnt2 + 1;
                                }
                                if (Cnt == searcher.СписокФайлов.Count())
                                {
                                    if (ErrorCnt != 0)
                                    {
                                        ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание выполнено c " + ErrorCnt + " ошибкой(ми)";
                                        log.WriteToLogFile("Задание выполнено c " + ErrorCnt + " ошибкой(ми)");
                                    }
                                    else
                                    {
                                        log.WriteToLogFile("Задание выполнено");
                                        ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание выполнено";
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (searcher.Ошибка == null)
                            {
                                log.WriteToLogFile("Нет файлов для перемещения");
                                log.WriteToLogFile("Задание выполнено");
                                ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Нет файлов для перемещения";
                            }
                            else
                            {
                                foreach (string line in searcher.Ошибка)
                                {
                                    log.WriteToLogFile(line);
                                }
                                log.WriteToLogFile("Задание не выполнено");
                                ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание не выполнено";
                            }

                        }
                    }
                }

                if (node.InnerText == "Удалить файл")
                {
                    log.WriteToLogFile("Тип задания: " + node.InnerText);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]");
                    SetText(node.Attributes["Name"].Value.ToString());
                    log.WriteToLogFile("Название задания: " + node.Attributes["Name"].Value.ToString());
                    ErrorList[TaskNumber] = node.Attributes["Name"].Value.ToString() + ": ";
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/FromPath");
                    FromPath = Format(node.InnerText);
                    log.WriteToLogFile("Из: " + FromPath);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/Filter");
                    Filter = Format(node.InnerText);
                    log.WriteToLogFile("Маска: " + Filter);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/Recursive");
                    log.WriteToLogFile("Вложенность: " + node.InnerText);
                    if (node.InnerText == "ВложенныеПапки")
                    {
                        SerOp = SearchOption.AllDirectories;
                    }

                    
                    searcher.SearchDirectory(FromPath, Filter, SerOp);

                    if (searcher.Ошибка == null & searcher.СписокФайлов != null)
                    {
                        int quantity;
                        if (searcher.СписокФайлов.Count() >= 100)
                        {
                            quantity = searcher.СписокФайлов.Count() / 100;
                            SetStep(1);
                        }
                        else
                        {
                            quantity = 1;
                            SetStep(100 / searcher.СписокФайлов.Count());
                        }


                        int Cnt = 0;
                        int Cnt2 = 1;
                        int ErrorCnt = 0;
                        foreach (string line in searcher.СписокФайлов)
                        {
                            SetLableText(line, "");
                            try
                            {
                                if (File.GetAttributes(line).ToString().Contains("ReadOnly"))
                                {
                                    File.SetAttributes(line, FileAttributes.Normal);
                                    log.WriteToLogFile("Изменен атрибут файла: " + line);
                                }
                            }
                            catch (Exception e)
                            {
                                log.WriteToLogFile("Не могу изменить атрибут файла: " + line);
                                log.WriteToLogFile(e.Message);
                            }
                            try
                            {                                
                                File.Delete(line);
                                log.WriteToLogFile("Удален: " + line);
                            }
                            catch (Exception e)
                            {
                                log.WriteToLogFile("Не могу удалить файл: " + line);
                                log.WriteToLogFile(e.Message);
                                ErrorCnt = ErrorCnt + 1;
                            }
                            Cnt = Cnt + 1;
                            if (Cnt == quantity * Cnt2)
                            {
                                Cnt2 = Cnt2 + 1;
                                PerformStepProc();
                            }
                        }

                        if (ErrorCnt != 0)
                        {
                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание выполнено c " + ErrorCnt + " ошибкой(ми)";
                            log.WriteToLogFile("Задание выполнено c " + ErrorCnt + " ошибкой(ми)");
                        }
                        else
                        {
                            log.WriteToLogFile("Задание выполнено");
                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание выполнено";
                        }

                    }
                    else
                    {
                        if (searcher.Ошибка == null)
                        {
                            log.WriteToLogFile("Нет файлов для удаления");
                            log.WriteToLogFile("Задание выполнено");
                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Нет файлов для удаления";
                        }
                        else
                        {
                            foreach (string line in searcher.Ошибка)
                            {
                                log.WriteToLogFile(line);
                            }
                            log.WriteToLogFile("Задание не выполнено");
                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание не выполнено";
                        }

                    }
                }

                if (node.InnerText == "Копировать каталог")
                {
                    log.WriteToLogFile("Тип задания: " + node.InnerText);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]");
                    SetText(node.Attributes["Name"].Value.ToString());
                    log.WriteToLogFile("Название задания: " + node.Attributes["Name"].Value.ToString());
                    ErrorList[TaskNumber] = node.Attributes["Name"].Value.ToString() + ": ";
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/FromPath");
                    FromPath = Format(node.InnerText);
                    log.WriteToLogFile("Копируемый каталог: " + FromPath);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/ToPath");
                    ToPath = Format(node.InnerText);
                    log.WriteToLogFile("В: " + ToPath);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/ReWrite");
                    if (node.InnerText == "Да")
                    {
                        ReWrite = YesNo.Да;
                    }
                    log.WriteToLogFile("Перезапись: " + ReWrite.ToString());

                    indx = FromPath.LastIndexOf("\\");

                    if (Directory.Exists(FromPath))
                    {
                        Dir = Directory.GetDirectories(FromPath, "*", SearchOption.AllDirectories);
                        try
                        {
                            Directory.CreateDirectory(ToPath + FromPath.Substring(indx));
                            log.WriteToLogFile("Создана директория: " + ToPath + FromPath.Substring(indx));
                        }
                        catch (Exception e)
                        {
                            log.WriteToLogFile("Не могу создать директорию: " + ToPath + FromPath.Substring(indx));
                            log.WriteToLogFile(e.Message);
                            log.WriteToLogFile("Задание не выполнено");
                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание не выполнено";

                        }


                        if (Directory.Exists(ToPath + FromPath.Substring(indx)))
                        {
                            int ErrorCnt = 0;
                            foreach (string line in Dir)
                            {
                                try
                                {
                                    Directory.CreateDirectory(ToPath + line.Substring(indx));
                                    log.WriteToLogFile("Создана директория: " + ToPath + line.Substring(indx));
                                }
                                catch (Exception e)
                                {
                                    log.WriteToLogFile("Не могу создать директорию: " + ToPath + line.Substring(indx));
                                    log.WriteToLogFile(e.Message);
                                    ErrorCnt = ErrorCnt + 1;
                                }
                            }

                            searcher.SearchDirectory(FromPath, "*.*", SearchOption.AllDirectories);
                            if (searcher.СписокФайлов != null)
                            {
                                int quantity;
                                if (searcher.СписокФайлов.Count() >= 100)
                                {
                                    quantity = searcher.СписокФайлов.Count() / 100;
                                    SetStep(1);
                                }
                                else
                                {
                                    quantity = 1;
                                    SetStep(100 / searcher.СписокФайлов.Count());
                                }


                                int Cnt = 0;
                                int Cnt2 = 1;
                                foreach (string line in searcher.СписокФайлов)
                                {
                                    SetLableText(line, ToPath + line.Substring(line.LastIndexOf("\\")));
                                    if (ReWrite == YesNo.Да)
                                    {
                                        if (File.Exists(ToPath + line.Substring(indx)))
                                        {
                                            try
                                            {
                                                if (File.GetAttributes(ToPath + line.Substring(indx)).ToString().Contains("ReadOnly"))
                                                {
                                                    File.SetAttributes(ToPath + line.Substring(indx), FileAttributes.Normal);
                                                    log.WriteToLogFile("Изменен атрибут файла: " + ToPath + line.Substring(indx));
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                log.WriteToLogFile("Не могу изменить атрибут файла: " + ToPath + line.Substring(indx));
                                                log.WriteToLogFile(e.Message);
                                            }
                                        }

                                        try
                                        {
                                            File.Copy(line, ToPath + line.Substring(indx), true);
                                            log.WriteToLogFile("Файл: " + line);
                                            log.WriteToLogFile("Скопирован: " + ToPath + line.Substring(indx));
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу скопировать файл: " + line);
                                            log.WriteToLogFile(e.Message);
                                            ErrorCnt = ErrorCnt + 1;
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            File.Copy(line, ToPath + line.Substring(indx), false);
                                            log.WriteToLogFile("Файл: " + line);
                                            log.WriteToLogFile("Скопирован: " + ToPath + line.Substring(indx));
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу скопировать файл: " + line);
                                            log.WriteToLogFile(e.Message);
                                            ErrorCnt = ErrorCnt + 1;
                                        }
                                    }
                                    
                                    Cnt = Cnt + 1;
                                    if (Cnt == quantity * Cnt2)
                                    {
                                        PerformStepProc();
                                        Cnt2 = Cnt2 + 1;
                                    }
                                    if (Cnt == searcher.СписокФайлов.Count())
                                    {
                                        if (ErrorCnt != 0)
                                        {
                                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание выполнено c " + ErrorCnt + " ошибкой(ми)";
                                            log.WriteToLogFile("Задание выполнено c " + ErrorCnt + " ошибкой(ми)");
                                        }
                                        else
                                        {
                                            log.WriteToLogFile("Задание выполнено");
                                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание выполнено";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                log.WriteToLogFile("Файлов для копирования нет");
                                log.WriteToLogFile("Задание выполнено");
                                ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Файлов для копирования нет";
                            }
                        }
                    }
                    else
                    {
                        log.WriteToLogFile("Директории для копирования нет: " + FromPath);
                        log.WriteToLogFile("Задание выполнено");
                        ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Директории для копирования нет: " + FromPath;
                    }
                }

                if (node.InnerText == "Переместить каталог")
                {
                    log.WriteToLogFile("Тип задания: " + node.InnerText);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]");
                    SetText(node.Attributes["Name"].Value.ToString());
                    log.WriteToLogFile("Название задания: " + node.Attributes["Name"].Value.ToString());
                    ErrorList[TaskNumber] = node.Attributes["Name"].Value.ToString() + ": ";
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/FromPath");
                    FromPath = Format(node.InnerText);
                    log.WriteToLogFile("Перемещаемый каталог: " + FromPath);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/ToPath");
                    ToPath = Format(node.InnerText);
                    log.WriteToLogFile("В: " + ToPath);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/ReWrite");
                    if (node.InnerText == "Да")
                    {
                        ReWrite = YesNo.Да;
                    }
                    log.WriteToLogFile("Перезапись: " + ReWrite.ToString());

                    indx = FromPath.LastIndexOf("\\");

                    if (Directory.Exists(FromPath))
                    {
                        Dir = Directory.GetDirectories(FromPath, "*", SearchOption.AllDirectories);
                        try
                        {
                            Directory.CreateDirectory(ToPath + FromPath.Substring(indx));
                            log.WriteToLogFile("Создана директория: " + ToPath + FromPath.Substring(indx));
                        }
                        catch (Exception e)
                        {
                            log.WriteToLogFile("Не могу создать директорию: " + ToPath + FromPath.Substring(indx));
                            log.WriteToLogFile(e.Message);
                            log.WriteToLogFile("Задание не выполнено");
                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание не выполнено";
                        }


                        if (Directory.Exists(ToPath + FromPath.Substring(indx)))
                        {
                            int ErrorCnt = 0;
                            foreach (string line in Dir)
                            {
                                try
                                {
                                    Directory.CreateDirectory(ToPath + line.Substring(indx));
                                    log.WriteToLogFile("Создана директория: " + ToPath + line.Substring(indx));
                                }
                                catch (Exception e)
                                {
                                    log.WriteToLogFile("Не могу создать директорию: " + ToPath + line.Substring(indx));
                                    log.WriteToLogFile(e.Message);
                                    ErrorCnt = ErrorCnt + 1;
                                }
                            }

                            searcher.SearchDirectory(FromPath, "*.*", SearchOption.AllDirectories);
                            if (searcher.СписокФайлов != null)
                            {
                                int quantity;
                                if (searcher.СписокФайлов.Count() >= 100)
                                {
                                    quantity = searcher.СписокФайлов.Count() / 100;
                                    SetStep(1);
                                }
                                else
                                {
                                    quantity = 1;
                                    SetStep(100 / searcher.СписокФайлов.Count());    
                                }


                                int Cnt = 0;
                                int Cnt2 = 1;
                                                          
                                foreach (string line in searcher.СписокФайлов)
                                {
                                    int Switch = 0;
                                    SetLableText(line, ToPath + line.Substring(line.LastIndexOf("\\")));
                                    if (ReWrite == YesNo.Да)
                                    {
                                        if (File.Exists(ToPath + line.Substring(indx)))
                                        {
                                            try
                                            {
                                                if (File.GetAttributes(ToPath + line.Substring(indx)).ToString().Contains("ReadOnly"))
                                                {
                                                    File.SetAttributes(ToPath + line.Substring(indx), FileAttributes.Normal);
                                                    log.WriteToLogFile("Изменен атрибут файла: " + ToPath + line.Substring(indx));
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                log.WriteToLogFile("Не могу изменить атрибут файла: " + ToPath + line.Substring(indx));
                                                log.WriteToLogFile(e.Message);
                                            }
                                        }

                                        try
                                        {
                                            File.Copy(line, ToPath + line.Substring(indx), true);
                                            Switch = 1;
                                            log.WriteToLogFile("Файл: " + line);
                                            log.WriteToLogFile("Скопирован: " + ToPath + line.Substring(indx));
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу скопировать файл: " + line);
                                            log.WriteToLogFile(e.Message);
                                            ErrorCnt = ErrorCnt + 1;
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            File.Copy(line, ToPath + line.Substring(indx), false);
                                            Switch = 1;
                                            log.WriteToLogFile("Файл: " + line);
                                            log.WriteToLogFile("Скопирован: " + ToPath + line.Substring(indx));
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу скопировать файл: " + line);
                                            log.WriteToLogFile(e.Message);
                                            ErrorCnt = ErrorCnt + 1;
                                        }
                                    }
                                    
                                    
                                    if (File.Exists(ToPath + line.Substring(indx)) & Switch == 1)
                                    {
                                        try
                                        {
                                            if (File.GetAttributes(line).ToString().Contains("ReadOnly"))
                                            {
                                                File.SetAttributes(line, FileAttributes.Normal);
                                                log.WriteToLogFile("Изменен атрибут файла: " + line);
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу изменить атрибут файла: " + line);
                                            log.WriteToLogFile(e.Message);
                                        }
                                        try
                                        {
                                            File.Delete(line);
                                            log.WriteToLogFile("Удален: " + line);
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу удалить файл: " + line);
                                            log.WriteToLogFile(e.Message);
                                            ErrorCnt = ErrorCnt + 1;
                                        }
                                    }
                                    Cnt = Cnt + 1;
                                    if (Cnt == quantity * Cnt2)
                                    {
                                        PerformStepProc();
                                        Cnt2 = Cnt2 + 1;
                                    }
                                }
                            }
                            else
                            {
                                log.WriteToLogFile("Файлов для перемещения нет");
                            }

                            searcher.SearchDirectory(FromPath, "*.*", SearchOption.AllDirectories);
                            if (searcher.СписокФайлов == null)
                            {
                                try
                                {
                                    Directory.Delete(FromPath, true);
                                    log.WriteToLogFile("Директория удалена: " + FromPath);
                                    
                                }
                                catch (Exception e)
                                {
                                    log.WriteToLogFile("Не могу удалить директорию: " + FromPath);
                                    log.WriteToLogFile(e.Message);
                                    ErrorCnt = ErrorCnt + 1;
                                }
                            }
                            else
                            {
                                foreach (string line in Dir)
                                {
                                    searcher.SearchDirectory(line, "*.*", SearchOption.AllDirectories);
                                    if (searcher.СписокФайлов == null)
                                    {
                                        try
                                        {
                                            Directory.Delete(line, true);
                                            log.WriteToLogFile("Директория удалена: " + line);
                                        }
                                        catch (Exception e)
                                        {
                                            log.WriteToLogFile("Не могу удалить директорию: " + line);
                                            log.WriteToLogFile(e.Message);
                                            ErrorCnt = ErrorCnt + 1;
                                        }
                                    }
                                }

                            }
                            if (ErrorCnt != 0)
                            {
                                ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание выполнено c " + ErrorCnt + " ошибкой(ми)";
                                log.WriteToLogFile("Задание выполнено c " + ErrorCnt + " ошибкой(ми)");
                            }
                            else
                            {
                                log.WriteToLogFile("Задание выполнено");
                                ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание выполнено";
                            }
                        }
                    }
                    else
                    {
                        log.WriteToLogFile("Директории для перемещения нет: " + FromPath);
                        log.WriteToLogFile("Задание выполнено");
                        ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Директории для перемещения нет: " + FromPath;
                    }
                }

                if (node.InnerText == "Удалить каталог")
                {
                    log.WriteToLogFile("Тип задания: " + node.InnerText);
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]");
                    SetText(node.Attributes["Name"].Value.ToString());
                    log.WriteToLogFile("Название задания: " + node.Attributes["Name"].Value.ToString());
                    ErrorList[TaskNumber] = node.Attributes["Name"].Value.ToString() + ": ";
                    node = archive.SelectSingleNode("/Archive/Task[" + i + "]/FromPath");
                    FromPath = Format(node.InnerText);
                    log.WriteToLogFile("Удаляемый каталог: " + FromPath);
                    SetStep(100);
                    if (Directory.Exists(FromPath))
                    {
                        SetLableText(FromPath, "");
                        try
                        {
                            Directory.Delete(FromPath, true);
                            log.WriteToLogFile("Директория удалена: " + FromPath);
                            log.WriteToLogFile("Задание выполнено");
                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание выполнено";

                        }
                        catch (Exception e)
                        {
                            log.WriteToLogFile("Не могу удалить директорию: " + FromPath);
                            log.WriteToLogFile(e.Message);
                            log.WriteToLogFile("Задание не выполнено");
                            ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Задание не выполнено";
                        }
                        PerformStepProc();
                    }
                    else
                    {
                        log.WriteToLogFile("Директории для удаления нет: " + FromPath);
                        log.WriteToLogFile("Задание выполнено");
                        ErrorList[TaskNumber] = ErrorList[TaskNumber] + "Директории для удаления нет: " + FromPath;
                    }
                }
                TaskNumber = TaskNumber + 1;
                PerformStepAll();
            }
            Thread.Sleep(1000);
            Dispose(true);
            
        
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

        private void SetText(string text)
        {

            if (labelProcessName.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                Invoke(d, new object[] { text });
            }
            else
            {
                labelProcessName.Text = text;
            }
        }
        
        private void SetLableText(string from, string to)
        {

            if (labelProcessName.InvokeRequired)
            {
                SetLableTextCallback d = new SetLableTextCallback(SetLableText);
                Invoke(d, new object[] { from, to });
            }
            else
            {
                labelFromPath.Text = "ИЗ: " + from;
                labelToPath.Text = "В:  " + to;
            }
        }

        private void SetStep(int step)
        {

            if (labelProcessName.InvokeRequired)
            {
                SetStepCallback d = new SetStepCallback(SetStep);
                Invoke(d, new object[] { step });
            }
            else
            {
                progressBarProcess.Step = step;
                
            }
        }
        
        private void PerformStepProc()
        {

            if (labelProcessName.InvokeRequired)
            {
                PerformStepProcCallback d = new PerformStepProcCallback(PerformStepProc);
                Invoke(d, new object[] { });
            }
            else
            {
                progressBarProcess.PerformStep();
                labelProcPerc.Text = progressBarProcess.Value.ToString() + " %";
            }
        }

        private void PerformStepAll()
        {

            if (labelProcessName.InvokeRequired)
            {
                PerformStepAllCallback d = new PerformStepAllCallback(PerformStepAll);
                Invoke(d, new object[] { });
            }
            else
            {
                progressBarAllProcesses.PerformStep();
                labelTotalPerc.Text = progressBarAllProcesses.Value.ToString() + " %";
                
            }
        }

        private void ProgressBarProcessValue(int position)
        {

            if (labelProcessName.InvokeRequired)
            {
                ProgressBarProcessValueCallback d = new ProgressBarProcessValueCallback(ProgressBarProcessValue);
                Invoke(d, new object[] { position });
            }
            else
            {
                progressBarProcess.Value = position;
            }
        }
    }
}
