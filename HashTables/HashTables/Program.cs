using System;

namespace HashTables
{
    class Program
    {
        const int ChainsMethodIndex = 0;
        const int StraightAdresMethodIndex = 1;
        const int ExitIndex = 2;

        static IHashTable<string, string> TableToWork;

        public static void Main(string[] args)
        {
            HashTableWithChains<string, string> hashTableChains = null;
            HashTableWithStraightAddress<string, string> hashTableStraightAddress = null;

            Menu chooseTaskMenu = new Menu("Выберите задание:",
                "Метод разрешения коллизий с помощью цепочек", "Метод открытой адресации для разрешения коллизий", "Выйти");
            int indexOfAnswer = chooseTaskMenu.GetIndexOfAnswer();
            if (indexOfAnswer == ExitIndex)
            {
                System.Environment.Exit(0);
            }
            else if (indexOfAnswer == ChainsMethodIndex)
            {
                HashFuncType type = ChooseHashFunc();
                int size = PositiveNumberFromUser();
                hashTableChains = new HashTableWithChains<string, string>(type, size);
                if (IsDataNeedGenerate())
                {
                    DataWorker.AddDataOnHashTable(DataWorker.GenerateStringKeys(size), hashTableChains);
                }
                TableToWork = hashTableChains;
            }
            else if (indexOfAnswer == StraightAdresMethodIndex)
            {
                HashFuncType type = ChooseHashFunc();
                int size = PositiveNumberFromUser();
                hashTableStraightAddress = new HashTableWithStraightAddress<string, string>(type, size);
                if (IsDataNeedGenerate())
                {
                    DataWorker.AddDataOnHashTable(DataWorker.GenerateStringKeys(size), hashTableStraightAddress);
                }
                TableToWork = hashTableStraightAddress;
            }

            UserWorkWithTable();
        }


        const int MD5Index = 0;
        const int RsIndex = 1;
        const int Sha256 = 2;
        const int DivIndex = 3;
        const int ExitHashMenuIndex = 4;
        public static HashFuncType ChooseHashFunc()
        {
            Menu chooseHashFuncTypeMenu = new Menu("Выберите хэш-функцию:",
                "MD5", "Rs", "Sha256", "Div", "Выйти");
            int indexOfAnsver = chooseHashFuncTypeMenu.GetIndexOfAnswer();
            switch (indexOfAnsver)
            {
                case MD5Index: return HashFuncType.MD5;
                case RsIndex: return HashFuncType.Rs;
                case Sha256: return HashFuncType.Sha256;
                case DivIndex: return HashFuncType.Div;
                case ExitHashMenuIndex:
                    Environment.Exit(0);
                    break;

            }

            throw new IndexOutOfRangeException("Нет других хэш-функций");
        }


        public static int PositiveNumberFromUser()
        {
            Console.WriteLine("Введите размер таблицы");
            Console.CursorVisible = true;
            int result;
            string answer = Console.ReadLine();
            bool allCorrect = false;
            do
            {
                allCorrect = int.TryParse(answer, out result);
                if (allCorrect) allCorrect = allCorrect && result > 0;

                if (allCorrect) break;
                else
                {
                    Console.WriteLine("Ошибка введённых данных, попробуйте ещё раз");
                    answer = Console.ReadLine();
                }
            } while (true);
            ConsoleHelper.ClearConsole();
            return result;
        }

        public static bool IsDataNeedGenerate()
        {
            Console.WriteLine("Сгенерировать данные?\nНажмите Y чтобы сгенерировать случайные данные\nНажмите N чтобы оставить таблицу пустой");
            bool isAnswerCorrect = false;
            bool result = false;
            while (!isAnswerCorrect)
            {
                ConsoleKeyInfo key = Console.ReadKey(false);

                if (key.KeyChar.ToString().ToUpper()[0] == 'N')
                {
                    isAnswerCorrect = true;
                    result = false;
                }
                else if (key.KeyChar.ToString().ToUpper()[0] == 'Y')
                {
                    isAnswerCorrect = true;
                    result = true;
                }
                else
                {
                    isAnswerCorrect = false;
                    Console.WriteLine("Вы нажали что-то другое, попробуйте ещё раз");
                }
            }

            ConsoleHelper.ClearConsole();
            return result;
        }

        const int AddIndex = 0;
        const int SearchIndex = 1;
        const int DeleteIndex = 2;

        const int UpdateIndex = 3;
        const int ExitFromWorkTableIndex = 4;
        public static void UserWorkWithTable()
        {
            Menu workWithTableMenu = new Menu("Выберите, что вы хотите сделать:",
                "Добавить элемент в таблицу", "Найти элемент", "Удалить элемент", "Изменить значение элемента", "Выйти");
            do
            {
                int res = workWithTableMenu.GetIndexOfAnswer();
                string key;
                string value;
                try
                {

                    switch (res)
                    {
                        case AddIndex:
                            key = GetKeyOrValue(true);
                            value = GetKeyOrValue(false);
                            TableToWork.Add(key, value);
                            break;
                        case SearchIndex:
                            key = GetKeyOrValue(true);
                            Console.WriteLine($"Результат: {TableToWork[key]}");
                            break;
                        case DeleteIndex:
                            key = GetKeyOrValue(true);
                            TableToWork.Remove(key);
                            break;
                        case UpdateIndex:
                            key = GetKeyOrValue(true);
                            value = GetKeyOrValue(false);
                            TableToWork.SetValue(key, value);
                            break;

                        case ExitFromWorkTableIndex:
                            Environment.Exit(0);
                            break;

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (true);
        }


        public static string GetKeyOrValue(bool IsUserInputKey)
        {
            string str=null;
            if(IsUserInputKey)
            {
                Console.WriteLine("Введите ключ");
                while (str == null||str==String.Empty)
                {
                    Console.WriteLine("Помните, что ключ не может быть null");
                    str = Console.ReadLine();
                    
                }
            }
            else
            {

                Console.WriteLine("Введите значение");
                str = Console.ReadLine();
            }

            return str;
        }
    }
}