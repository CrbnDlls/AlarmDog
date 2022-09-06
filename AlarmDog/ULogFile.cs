using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace AlarmDog
{
    /// <summary>
    /// Класс реализует чтение файла журнала почты НБУ
    /// </summary>
    class ULogFile
    {
        private string uFileName;
        private string[] uFiles;
        private string ErrorMessage;
        private int Cnt;
        
        /// <summary>
        /// Количество найденных файлов
        /// </summary>
        public int Count
        { get { return Cnt; } }
        
        /// <summary>
        /// Путь и Имя файла журнала почты НБУ
        /// </summary>
        public string ФайлЖурналаПочтыНБУ
        { get { return uFileName; } }

        /// <summary>
        /// Список найденных файлов
        /// </summary>
        public string[] СписокФайлов
        { get { return uFiles; } }

        /// <summary>
        /// Сообщение ошибки при выполнении GetFileList
        /// </summary>
        public string Ошибка
        { get { return ErrorMessage; } }

        /// <summary>
        /// Производит поиск файлов $B, $K, $T, $V, $U, $F и наполняет ими СписокФайлов
        /// </summary>
        public void GetFileList()
        {
            GetUFileName();

            if (File.Exists(uFileName))
            {
                string line;
                string[] FILES = new string[500];
                Cnt = 0;
                StreamReader fileReader = new StreamReader(uFileName);
                
                while ((line = fileReader.ReadLine()) != null)
                {

                    if ((line.Substring(118, 2) == "$B") | (line.Substring(118, 2) == "$K") | (line.Substring(118, 2) == "$T") | (line.Substring(118, 2) == "$V") | (line.Substring(118, 2) == "$F") | (line.Substring(118, 2) == "$U"))
                    {
                        FILES[Cnt] = line.Substring(118, 12);
                        Cnt++;
                    }

                }
                fileReader.Close();
                
                uFiles = new string[Cnt];
                for (int i = 0; i < Cnt; i++)
                {
                    uFiles[i] = FILES[i];
                }
                                
                ErrorMessage = null;
            }
            else
            {
                Cnt = 0;
                uFiles = null;
                ErrorMessage = "Файл журнала почты НБУ не доступен или не существует:";
                return;
            }
        }

        /// <summary>
        /// Формирует Путь и Имя файла журнала почты НБУ
        /// </summary>
        public void GetUFileName()
        {
            XmlDocument settings = new XmlDocument();
            settings.Load("AlarmDog.xml");
            XmlNode node = settings.SelectSingleNode("/Settings/ULogFile/Path");
            uFileName = node.InnerText;
            node = settings.SelectSingleNode("/Settings/ULogFile/LogFileNameFormat");
            if (uFileName.EndsWith("\\"))
            {
                uFileName = uFileName + node.InnerText;
            }
            else
            {
                uFileName = uFileName + "\\" + node.InnerText;
            }

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
            
            uFileName = uFileName.Replace("dd", Date);
            uFileName = uFileName.Replace("mm", Month);
            uFileName = uFileName.Replace("yyyy", DateTime.Now.Year.ToString());
            uFileName = uFileName.Replace("yy", DateTime.Now.Year.ToString().Substring(2));
            
        }
    }
}