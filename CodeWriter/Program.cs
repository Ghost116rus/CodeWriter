namespace CodeWriter
{
    internal class Program
    {
        private static string _filePath = "data.txt";
        private static string _filePattern = "*.cs";

        private static void ShowMenu()
        {
            Console.WriteLine("1 - загрузить в исходный файл все текстовые файлы из каталога");
            Console.WriteLine("0 - завершить работу");
            Console.WriteLine("11 - показать меню");
        }
        
        private static void ReadFilesInDirectory(StreamWriter writer,  string path)
        {
            foreach (var dir in Directory.GetDirectories(path))
            {
                ReadFilesInDirectory(writer, dir);
            }

            foreach (var file in Directory.GetFiles(path, _filePattern))
            {
                var fileName = file.Split('\\').Last();
                writer.WriteLine(fileName); // Записываем строку в файл
                writer.WriteLine(); // Пустую строку
                writer.WriteLine(File.ReadAllText(file)); // Пустую строку
                writer.WriteLine(); // Пустую строку
                writer.WriteLine(); // Пустую строку
            }
        }

        private static void ReadFiles()
        {
            try
            {
                Console.WriteLine("Введите путь к заданной директории:");
                var dirPath = Console.ReadLine();

                if (!Directory.Exists(dirPath)) 
                {
                    Console.WriteLine("Данная директория не найдена!");
                    return;
                }

                // Проверяем, существует ли файл
                if (!File.Exists(_filePath))
                {
                    // Если файл не существует, создаем его
                    File.Create(_filePath).Dispose();
                }
                // Записываем данные в файл
                using (StreamWriter writer = new StreamWriter(_filePath, true)) // true для добавления в конец файла
                {
                    foreach (var dir in Directory.GetDirectories(dirPath))
                    {
                        ReadFilesInDirectory(writer, dir);
                    }
                }
            } 
            catch 
            {
                Console.WriteLine("Что-то пошло не так");
            }
        }

        private static int GetUserChoice() 
        {
            int userChoice = -1;
            while (userChoice == -1)
            {
                try 
                {
                    Console.Write("Выберите действие: ");
                    userChoice = int.Parse(Console.ReadLine());
                }
                catch
                {

                }
            }
            return userChoice;
        }

        static void Main(string[] args)
        {
            ShowMenu();
            var userChoice = GetUserChoice();

            while (userChoice != 0)
            {
                switch (userChoice)
                {
                    case 1:
                        ReadFiles();
                        break;
                    case 11:
                        ShowMenu();
                        break;
                    default:
                        break;
                }

                userChoice = GetUserChoice();
            }
        }
    }
}
