using System;
using System.IO;

namespace SF_8_Results_Task_3
{
    class Program
    {
        static int chDirDel = 0;
        static int chFileDel = 0;
        static long chFileSizeTotal = 0;
        static long chFileSizeDel = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("\tПРОГРАММА ОЧИСТКИ ПАПОК В УКАЗАННОЙ ДИРЕКТОРИИ \n\t  И ПОДСЧЁТА ИХ РАЗМЕРА ДО И ПОСЛЕ ПРОЦЕДУРЫ\n");
            Console.Write("Введите путь до папки: ");
            string pachFolder = Console.ReadLine();
            //string pachFolder = "D://SF8FFD";

            if (Directory.Exists(pachFolder))
            {
                DeleteFolderAndFiles(pachFolder);

                Console.WriteLine($"Исходный размер папки: {chFileSizeTotal} байт.\n");
                Console.WriteLine($"Освобождено: {chFileSizeDel} байт.");
                Console.WriteLine($"Текущий размер папки: {chFileSizeTotal - chFileSizeDel} байт.\n");
                Console.WriteLine($"Удалено: {chDirDel} папок и {chFileDel} файлов");
                Console.WriteLine("Программа завершена.");
            }
            else
            {
                Console.WriteLine("Папка не существует. Или неверно задан путь.");
            }
        }

        private static void DeleteFolderAndFiles(string folder)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(folder);
                DirectoryInfo[] diDir = dirInfo.GetDirectories();
                FileInfo[] diFiles = dirInfo.GetFiles();

                foreach (FileInfo f in diFiles)
                {
                    chFileSizeTotal += f.Length;
                    if ((DateTime.Now - f.LastAccessTime) > TimeSpan.FromMinutes(30))
                    {
                        f.Delete();
                        chFileDel++;
                        chFileSizeDel += f.Length;
                    }
                }
                
                foreach (DirectoryInfo df in diDir)
                {
                    DeleteFolderAndFiles(df.FullName);
                }
                if (dirInfo.GetDirectories().Length == 0 && dirInfo.GetFiles().Length == 0)
                {
                    dirInfo.Delete();
                    chDirDel++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
        }
    }
}
