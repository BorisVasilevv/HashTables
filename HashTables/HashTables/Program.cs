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

            HashFuncType type = ChooseHashFunc();
            Console.WriteLine("Введите размер таблицы");
            int size = PositiveNumberFromUser();

            if (indexOfAnswer == ChainsMethodIndex)
            {
                
                hashTableChains = new HashTableWithChains<string, string>(type, size);
                if (IsDataNeedGenerate())
                {
                    Console.WriteLine("Генерация данных");
                    DataWorker.AddDataOnHashTable(DataWorker.GenerateStringKeys(size), hashTableChains);
                }
                TableToWork = hashTableChains;
            }
            else if (indexOfAnswer == StraightAdresMethodIndex)
            {
                
                hashTableStraightAddress = new HashTableWithStraightAddress<string, string>(type, size);
                if (IsDataNeedGenerate())
                {
                    Console.WriteLine("Генерация данных");
                    DataWorker.AddDataOnHashTable(DataWorker.GenerateStringKeys(size), hashTableStraightAddress);
                }
                TableToWork =hashTableStraightAddress;
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
                ConsoleKeyInfo key = Console.ReadKey(true);

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
        const int ShowKeysToUserIndex = 4;
        const int ExitFromWorkTableIndex = 5;
        public static void UserWorkWithTable()
        {
            Menu workWithTableMenu = new Menu("Выберите, что вы хотите сделать:",
                "Добавить элемент в таблицу", "Найти элемент", "Удалить элемент", "Изменить значение элемента","Вывести первые n ключей", "Выйти");
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
                            Console.WriteLine("Элемент добавлен успешно");
                            break;
                        case SearchIndex:
                            key = GetKeyOrValue(true);
                            Console.WriteLine($"Элемент с ключём {key} найден\nЗначение элемента: {TableToWork[key]}");
                            break;
                        case DeleteIndex:
                            key = GetKeyOrValue(true);
                            Console.WriteLine($"Элемент с ключём {key} удалён\nЗначение элемента было: {TableToWork[key]}");
                            TableToWork.Remove(key);                  
                            break;
                        case UpdateIndex:
                            key = GetKeyOrValue(true);
                            value = GetKeyOrValue(false);

                            string oldValue = TableToWork[key];
                            TableToWork[key] = value;
                            Console.WriteLine($"Значение по ключу {key} изменено\nСтарое значение: {oldValue}\nНовое значение: {value}");

                            break;
                        case ShowKeysToUserIndex:
                            int countKeys=TableToWork.Count;
                            if( countKeys == 0 )
                            {
                                Console.WriteLine("В таблице нет элементов");
                                break;
                            }
                            Console.WriteLine($"Введите число от 1 до {countKeys}");
                            bool isAnswerCorrect = false;
                            int number = 0;
                            while (!isAnswerCorrect)
                            {
                                number= PositiveNumberFromUser();
                                isAnswerCorrect = number <= countKeys;
                                if(!isAnswerCorrect)
                                {
                                    Console.WriteLine($"Такого количества ключей нет, введите число поменьше, число от 1 до {countKeys}");
                                }
                            }
                            ConsoleHelper.ClearConsole();
                            string[] keys = TableToWork.GetKeys(number);
                            foreach (string k in keys)
                            {
                                Console.WriteLine(k);
                            }
                            

                            break;
                        case ExitFromWorkTableIndex:
                            Environment.Exit(0);
                            break;

                    }
             
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                    Console.ReadKey(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                    Console.ReadKey(false);
                }
            } while (true);
        }


        public static string GetKeyOrValue(bool IsUserInputKey)
        {
            string str=null;
            Console.CursorVisible = true;
            if (IsUserInputKey)
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
            ConsoleHelper.ClearConsole();
            return str;
        }
    }
}