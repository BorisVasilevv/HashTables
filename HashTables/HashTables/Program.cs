using System;

namespace HashTables
{
    class Program
    {
        const int ChainsMethodIndex = 0;
        const int StraightAdresMethodIndex = 1;
        const int ExitIndex = 2;


        

        public static void Main(string[] args)
        {
            HashTableWithChains<string, string> hashTableCh = null;
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
                HashFuncType type=ChooseHashFunc();
                int size = PositiveNumberFromUser();

            }
            else if (indexOfAnswer == StraightAdresMethodIndex)
            {
                HashFuncType type = ChooseHashFunc();
                int size = PositiveNumberFromUser();
            }

            Console.ReadKey();


            

            Menu workWithTableMenu = new Menu("Выберите, что вы хотите сделать:",
                "Добавить элемент в таблицу", "Найти элемент", "Удалить элемент", "Изменить значение элемента" ,"Выйти");
        }


        const int MD5Index = 0;
        const int RsIndex = 1;
        const int Sha256 = 2;
        const int DivIndex = 3;
        const int ExitHashMenuIndex = 4;
        public static HashFuncType ChooseHashFunc()
        {
            Menu chooseHashFuncTypeMenu = new Menu("Выберите хэш-функцию:",
                "MD5", "Rs", "Sha256","Div","Выйти");
            int indexOfAnsver = chooseHashFuncTypeMenu.GetIndexOfAnswer();
            bool exit = false;
            switch (indexOfAnsver)
            {
                case MD5Index: return HashFuncType.MD5;
                case RsIndex: return HashFuncType.Rs;
                case Sha256: return HashFuncType.Sha256;
                case DivIndex: return HashFuncType.Div;
                case ExitHashMenuIndex:
                    exit = true;
                    break;
                
            }

            if (exit) Environment.Exit(0);
            throw new IndexOutOfRangeException("Нет других хэш-функций");
        }


        public static int PositiveNumberFromUser()
        {
            Console.WriteLine("Введите размер таблицы");

            int result;
            string answer=Console.ReadLine();
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

        public bool IsDataNeedGenerate()
        {
            Console.WriteLine("Сгенерировать данные?\nНажмите Y чтобы сгенерировать случайные данные\nНажмите N чтобы оставить таблицу пустой");
            bool isAnswerCorrect=false;
            bool result=false;
            while(!isAnswerCorrect)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.KeyChar == 'N')
                {
                    isAnswerCorrect=true;
                    result = false;
                }
                else if (key.KeyChar == 'Y')
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
    }
}