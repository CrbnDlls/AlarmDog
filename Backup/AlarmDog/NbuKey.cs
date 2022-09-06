using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AlarmDog
{
    /// <summary>
    /// Класс для выяснения необходимости обновления ключей
    /// </summary>
    class NbuKey
    {
        private YesNo NeedsUpdate;

        /// <summary>
        /// Содержит значение "Да", если необходимо обновить ключи; иначе "Нет".
        /// </summary>
        public YesNo НеобходимоОбновить
        {
            get { return NeedsUpdate; }
        }

        /// <summary>
        /// Получает список файлов ключей, которые необходимо обновить.
        /// </summary>
        /// <param name="Path">Каталог в котором находятся файлы ключей</param>
        /// <param name="Filters">Маски имен файлов ключей; например *.txt|bn??.doc</param>
        public string[] GetKeyList(string Path, string Filters)
        {
            NeedsUpdate = YesNo.Нет;
            if (Int32.Parse(DateTime.Now.Hour.ToString()) >= 7 & Int32.Parse(DateTime.Now.Hour.ToString()) <= 10)
            {
                DirectorySearcher searcher = new DirectorySearcher();
                searcher.SearchDirectory(Path, Filters);
                if (searcher.Ошибка == null)
                {
                    if (searcher.СписокФайлов != null)
                    {
                        NeedsUpdate = YesNo.Да;
                        return searcher.СписокФайлов;
                    }
                    else 
                    {
                        NeedsUpdate = YesNo.Нет;
                        return null;
                    }
                }
                else
                {
                    NeedsUpdate = YesNo.Нет;
                    return null;
                }
            }
            else
            {
                NeedsUpdate = YesNo.Нет;
                return null;
            }
        }
    }
}
