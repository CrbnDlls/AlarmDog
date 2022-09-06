using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Timers;

namespace AlarmDog
{
    /// <summary>
    /// Класс реализует подсчет интервала для запуска таймера по отправке $Z
    /// </summary>
    class ZFile
    {
        private int Interval;

        /// <summary>
        /// Интервал на который необходимо запустить таймер
        /// </summary>
        public int Интервал
        { get { return Interval; } }

        /// <summary>
        /// При необходимости включения таймера подсчитывает интервал и вносит его в Интервал; иначе Интервал = 0
        /// </summary>
        /// <param name="ArchivePath">Каталог в котором хранятся файлы $K текущего операционного дня</param>
        public void Check(string ArchivePath)
        {
            if (Int32.Parse(DateTime.Now.Hour.ToString()) >= 18)
            {
                string KFileName;
                char[] Flag = new char[1];

                DirectorySearcher searcher = new DirectorySearcher();
                searcher.SearchDirectory(ArchivePath, "$K11QK??.???");
                if (searcher.Ошибка != null)
                {
                    return;
                }
                KFileName = searcher.СписокФайлов.Last();
                StreamReader reader = new StreamReader(KFileName);
                reader.BaseStream.Position = 320;
                reader.Read(Flag, 0, 1);
                reader.Close();
                if (Flag[0].ToString() == "Y")
                {


                    if (Int32.Parse(DateTime.Now.Hour.ToString()) >= 18 & Int32.Parse(DateTime.Now.Hour.ToString()) <= 19)
                    {
                        Interval = (60 - Int32.Parse(DateTime.Now.Minute.ToString())) * 60 * 1000;
                    }

                    if (Int32.Parse(DateTime.Now.Hour.ToString()) >= 19 & Int32.Parse(DateTime.Now.Minute.ToString()) <= 30)
                    {
                        Interval = (50 - Int32.Parse(DateTime.Now.Minute.ToString())) * 60 * 1000;
                    }

                    if (Int32.Parse(DateTime.Now.Hour.ToString()) >= 19 & Int32.Parse(DateTime.Now.Minute.ToString()) >= 30)
                    {
                        Interval = (80 - Int32.Parse(DateTime.Now.Minute.ToString())) * 60 * 1000;
                    }
                }
            }
            else 
            {
                Interval = 0;
            }

                
        }
    }
}

