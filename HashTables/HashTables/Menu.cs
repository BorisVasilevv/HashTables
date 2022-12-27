using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class Menu
    {
        string _question;
        List<string> _answers;

        public Menu(string question, List<string> answers)
        {
            _answers = answers;
            
            _question = question;
        }

        public Menu(string question, params string[] items)
        {
            _answers = new List<string>();
            foreach (string item in items)
            {
                _answers.Add(item);
                
            }
            _question=question;
        }

        int NumberOfSelectedElem = 0;

        public int GetIndexOfAnswer()
        {
            int answer;
            DrawMenu();
            answer = NumberOfSelectedElem;
            NumberOfSelectedElem = 0;
            ConsoleHelper.ClearConsole();
            return answer;
        }

        public void DrawMenu()
        {
            bool isUserChoose = false;
            
            Console.WriteLine(_question);
            Console.CursorVisible = false;
            while(!isUserChoose)
            {
                ConsoleHelper.ClearConsole();
                Console.WriteLine(_question);
                for (int i=0;i<_answers.Count;i++)
                {
                    if(i==NumberOfSelectedElem) Console.BackgroundColor=ConsoleColor.DarkCyan;
                    Console.WriteLine(_answers[i]);
                    Console.BackgroundColor=ConsoleColor.Black;                   
                }


                ConsoleKeyInfo key= Console.ReadKey(false);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        ChangeSelection(false);
                        break;
                    case ConsoleKey.DownArrow:
                        ChangeSelection(true);
                        break;
                    case ConsoleKey.Enter:
                        isUserChoose = true;
                        ConsoleHelper.ClearConsole();
                        break;

                }
            }
        }


        void ChangeSelection(bool selectedDecrease)
        {
            if(selectedDecrease)
            {
                NumberOfSelectedElem++;
                if (NumberOfSelectedElem >= _answers.Count) NumberOfSelectedElem = 0;
            }
            else
            {
                NumberOfSelectedElem--;
                if(NumberOfSelectedElem<0)NumberOfSelectedElem=_answers.Count-1;
            }
        }


        
    }
}
