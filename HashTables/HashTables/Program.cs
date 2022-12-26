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
            Menu chooseTaskMenu = new Menu("Выберите задание:",
                "Метод разрешения коллизий с помощью цепочек", "Метод открытой адресации для разрешения коллизий", "Выйти");
            int indexOfAnswer = chooseTaskMenu.GetIndexOfAnswer();
            if (indexOfAnswer == ExitIndex) System.Environment.Exit(0);
            else if (indexOfAnswer == ChainsMethodIndex) Console.WriteLine("Ch");
            else if (indexOfAnswer == StraightAdresMethodIndex) Console.WriteLine("St");

            Console.ReadKey();




            Menu workWithTableMenu = new Menu("Выберите, что вы хотите сделать:",
                "Добавить элемент в таблицу", "Найти элемент", "Удалить элемент", "Показать таблицу" ,"Выйти");
        }
    }
}