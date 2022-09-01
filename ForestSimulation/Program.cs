using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace ForestSimulation
{
    class Program
    { 
        static void Main(string[] args)
        {
            Program p = new Program();
            Console.CursorVisible = false;
            Console.WindowHeight =25;
            Console.WindowWidth =80;
            Console.SetBufferSize(80, 25);
            p.Start();
        }
        public void Start()
        {
            Thread t = new Thread(new ThreadStart(Simulation));
            speed = 50;
            Console.WriteLine("Napisz prędkość z jaką ma być wykonywana symulacja w milisekundach (Rekomendowane: 65)");
            try
            {
                speed = int.Parse(Console.ReadLine());
            }
            catch
            {
                speed = 65;
            }
            Console.Clear();
            Console.WriteLine("Czy ma być widoczna \"spalona ziemia\"? (y/n)");
                string ans = Console.ReadLine();
                char letter = ans.First();
                letter = char.ToLower(letter);

                if (letter == 'y' || letter == 't')
                    ScorchedEarthVis = true;
                else if (letter == 'n')
                    ScorchedEarthVis = false;
                else
                    ScorchedEarthVis = true;
            Console.Clear();
            Console.WriteLine("Czy puste pola mają być widoczne? (y/n)");
            string ans2 = Console.ReadLine();
            char letter2 = ans2.First();
            letter2 = char.ToLower(letter2);
            if (letter2 == 'y' || letter2 == 't')
                BlankVis = true;
            else if (letter2 == 'n')
                BlankVis = false;
            else
                BlankVis = true;
            Console.Clear();
            Console.WriteLine("Czy renderować tylko bloki? (y/n)");
            string ans3 = Console.ReadLine();
            char letter3 = ans3.First();
            letter3 = char.ToLower(letter3);
            if (letter3 == 'y' || letter3 == 't')
            {
                OnlyBlocks = true;
                ScorchedEarthVis = false;
                BlankVis = false;
                speed += 30;
            }
                
            else if (letter3 == 'n')
                OnlyBlocks = false;
            else
                OnlyBlocks = false;
            Console.Clear();
            Console.WriteLine("Podaj ilość jezior (Rekomendowana: 3 - 4)\nCzasami symulacja generuje mniej jezior!");
            try
            {
                LakesIterations = int.Parse(Console.ReadLine());
            }
            catch
            {
                LakesIterations = 3;
            }
            Console.Clear();
            t.Start();
           
        }
        public int speed;
        public bool ScorchedEarthVis = false;
        public bool BlankVis = false;
        public int LakesIterations = 3;
        public bool OnlyBlocks = false;
        public void Simulation()
        {

            Random rand = new Random();
            string[,] Map = new string[23, 78];

           
            string[,] newMap1 = new string[23, 78];
            for (int a = 0; a < 23; a++)
            {
                for (int b = 0; b < 78; b++)
                {
                    newMap1[a, b] = "-";
                }

            }
            #region CreatingLakes
            for (int l = 0; l < LakesIterations; l++)
            {
                int x = rand.Next(4, 19);
                int y = rand.Next(6, 75);
                Map[x,y] = "O";
                Map[x + 4, y] = "O";
            }


            for (int r = 0; r <4; r++)
            {
                for (int a = 2; a < 21; a++)
                {
                    for (int b = 2; b < 76; b++)
                    {
                        string[] bordering1 = new string[] { Map[a - 1, b], Map[a, b + 1], Map[a + 1, b], Map[a, b - 1], Map[a - 1, b - 1], Map[a + 1, b + 1], Map[a + 1, b - 1], Map[a - 1, b + 1] };
                        string[] bordering2water = new string[] { Map[a - 1, b + 1], Map[a - 1, b], Map[a, b + 1], Map[a + 1, b], Map[a, b - 2], Map[a, b - 1], Map[a - 1, b - 1], Map[a, b + 2], Map[a + 1, b - 2], Map[a + 1, b + 2], Map[a + 2, b - 2], Map[a + 2, b - 1], Map[a + 2, b], Map[a + 1, b + 1], Map[a + 1, b - 1], Map[a - 2, b - 2], Map[a - 2, b - 1], Map[a - 2, b], Map[a - 2, b + 1], Map[a - 2, b + 2], Map[a - 1, b - 2], Map[a + 2, b + 2], Map[a - 1, b + 2], Map[a + 2, b + 1] };
                        foreach (string s in bordering2water)
                        {
                            if (s == "O" && rand.Next(1, 7) == 5)
                            {
                                newMap1[a, b] = "O";
                                int somenumber = rand.Next(1, 4);
                                if (somenumber == 1)
                                    newMap1[a, b - 1] = "O";
                                else if (somenumber == 2)
                                    newMap1[a, b + 1] = "O";
                                else
                                {
                                    newMap1[a, b - 1] = "O";
                                    newMap1[a, b + 1] = "O";
                                }

                                break;
                            }

                        }

                    }

                }
                for (int t = 0; t < 23; t++)
                {
                    for (int y = 0; y < 77; y++)
                    {
                        switch (newMap1[t, y])
                        {
                            case "-":
                                Map[t, y] = "-";
                                break;
                            case "O":
                                Map[t, y] = "O";
                                break;
                        }
                    }
                }
            }
                #endregion



            for (int a = 0; a < 22; a++)
            {
                Console.SetCursorPosition(2, a + 1);
                for (int b = 0; b < 78; b++)
                {
                    
                    if ((a == 0 || a == 1 || a == 20 || a == 21) && (b != 0 && b != 1 && b != 76 && b != 77))
                    {

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Map[a, b] = "-";
                        Console.Write("=");
                    }
                    else if (b == 0 || b == 1 || b == 76 || b == 77)
                    {

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Map[a, b] = "-";
                        Console.Write("|");
                    }
                   
                    else if(Map[a,b] == "O")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write("O");
                        Map[a, b] = "O";
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        if (BlankVis == false)
                            Console.ForegroundColor = ConsoleColor.Black;
                        else
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        Map[a, b] = "-";
                        Console.Write("-");
                    }
                }
                Console.WriteLine("");

            }
            do
            {
                string[,] newMap = new string[23, 77];
                for (int a = 0; a < 23; a++)
                {
                    for (int b = 0; b < 77; b++)
                    {
                        newMap[a, b] = "-";
                    }

                }
                for (int a = 2; a < 20; a++)
                {
                    for (int b = 2; b < 76; b++)
                    {
                        switch (Map[a, b])
                        {
                            case "O":
                                newMap[a, b] = "O";
                                break;
                            case "+":
                                if (rand.Next(1, 34) == 1)
                                    newMap[a, b] = "+";
                                else
                                    newMap[a, b] = "~";
                                break;
                            case "-":
                                newMap[a, b] = "-";
                                bool change = false;
                                string[] bordering1 = new string[] { Map[a - 1, b], Map[a, b + 1], Map[a + 1, b], Map[a, b - 1], Map[a - 1, b - 1], Map[a + 1, b + 1], Map[a + 1, b - 1], Map[a - 1, b + 1] };
                                string[] bordering4water = new string[] { Map[a - 1, b], Map[a, b + 1], Map[a + 1, b], Map[a, b - 1], Map[a - 1, b - 1], Map[a + 1, b + 1], Map[a + 1, b - 1], Map[a - 1, b + 1], Map[a - 2, b - 2], Map[a - 2, b - 1], Map[a - 2, b], Map[a - 2, b + 1], Map[a - 2, b + 2], Map[a - 1, b - 2], Map[a - 1, b + 2], Map[a, b - 2], Map[a, b + 2], Map[a + 1, b - 2], Map[a + 1, b + 2], Map[a + 2, b - 2], Map[a + 2, b - 1], Map[a + 2, b], Map[a + 2, b + 1], Map[a + 2, b + 2] };
                                foreach (string bor in bordering1)
                                {
                                    if (bor == "+" || bor == "~")
                                    {
                                        change = false;
                                        break;
                                    }
                                    else if (bor == "T")
                                        change = true;

                                }
                                int pluschances = 0;
                                foreach(string bwa in bordering4water)
                                {
                                    if (bwa == "O")
                                        pluschances += 40;
                                }
                                if (pluschances >= 699)
                                    pluschances = 699;
                                if (change && rand.Next(1,(701 - pluschances)) == 1)
                                    newMap[a, b] = "t";
                                bool change2 = true;
                                foreach (string bor in bordering1)
                                {
                                    if (bor == "+" || bor == "~")
                                    {
                                        change = false;
                                        break;
                                    }                         
                                }
                                if (change2 == true && rand.Next(1, 10001) == 1)
                                    newMap[a, b] = "T";
                                break;
                            case "T":
                                newMap[a, b] = "T";
                                bool water = false;
                                string[] bordering2 = new string[] { Map[a - 1, b], Map[a, b + 1], Map[a + 1, b], Map[a, b - 1], Map[a - 1, b - 1], Map[a + 1, b + 1], Map[a + 1, b - 1], Map[a - 1, b + 1] };
                                string[] bordering2water = new string[] { Map[a - 1, b], Map[a, b + 1], Map[a + 1, b], Map[a, b - 1], Map[a - 1, b - 1], Map[a + 1, b + 1], Map[a + 1, b - 1], Map[a - 1, b + 1], Map[a - 2, b -2], Map[a - 2, b -1], Map[a - 2, b],Map[a - 2, b + 1], Map[a - 2, b + 2], Map[a - 1, b -2] , Map[a - 1, b + 2], Map[a, b -2], Map[a, b + 2], Map[a + 1, b - 2], Map[a + 1, b + 2], Map[a + 2, b - 2], Map[a + 2, b - 1], Map[a + 2, b], Map[a + 2, b + 1], Map[a + 2, b + 2] };
                                    
                                    foreach (string bor in bordering2water)
                                    {
                                        if (bor == "O")
                                            water = true;

                                    }                              
                                    foreach (string bor in bordering2)
                                {
                                    if (water)
                                    {
                                        if (bor == "+" && rand.Next(1, 34) == 1)
                                        {
                                            newMap[a, b] = "+";

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (bor == "+" && rand.Next(1, 34) != 1)
                                        {
                                            newMap[a, b] = "+";

                                            break;
                                        }
                                    }
                                    
                                }
                                if (newMap[a, b] == "+")
                                    break;
                                if(newMap[a,b] != "+" && newMap[a,b] != "D")
                                {
                                    foreach (string bor in bordering2)
                                    {
                                        if (bor == "D" && rand.Next(1, 11) == 1)
                                        {
                                            newMap[a, b] = "D";

                                            break;
                                        }


                                    }
                                }

                                if (rand.Next(1, 50001) == 65)
                                    newMap[a, b] = "D";
                                if (rand.Next(1,10001) == 65 && newMap[a,b] != "D")
                                    newMap[a, b] = "+";
                                break;
                            case "~":
                                newMap[a, b] = "~";
                                if(rand.Next(1,21) == 1)
                                    newMap[a, b] = "-";
                                break;
                            case "D":
                                newMap[a, b] = "D";
                                string[] bordering4 = new string[] { Map[a - 1, b], Map[a, b + 1], Map[a + 1, b], Map[a, b - 1], Map[a - 1, b - 1], Map[a + 1, b + 1], Map[a + 1, b - 1], Map[a - 1, b + 1] };
                                foreach (string bor in bordering4)
                                {
                                    if (bor == "+" && rand.Next(1, 33) != 1)
                                    {
                                        newMap[a, b] = "+";

                                        break;
                                    }


                                }
                                if (newMap[a, b] == "+")
                                    break;
                                if (rand.Next(1, 10001) == 65)
                                    newMap[a, b] = "+";
                                if (rand.Next(1, 51) == 1 && newMap[a,b] != "+")
                                    newMap[a, b] = "-";
                                if (rand.Next(1, 101) == 1 && newMap[a, b] != "+" && newMap[a, b] != "-")
                                    newMap[a, b] = "T";
                                break;
                            case "t":
                                newMap[a, b] = "t";
                                string[] bordering3 = new string[] { Map[a - 1, b], Map[a, b + 1], Map[a + 1, b], Map[a, b - 1], Map[a - 1, b - 1], Map[a + 1, b + 1], Map[a + 1, b - 1], Map[a - 1, b + 1] };
                                string[] bordering3water = new string[] { Map[a - 1, b], Map[a, b + 1], Map[a + 1, b], Map[a, b - 1], Map[a - 1, b - 1], Map[a + 1, b + 1], Map[a + 1, b - 1], Map[a - 1, b + 1], Map[a - 2, b - 2], Map[a - 2, b - 1], Map[a - 2, b], Map[a - 2, b + 1], Map[a - 2, b + 2], Map[a - 1, b - 2], Map[a - 1, b + 2], Map[a, b - 2], Map[a, b + 2], Map[a + 1, b - 2], Map[a + 1, b + 2], Map[a + 2, b - 2], Map[a + 2, b - 1], Map[a + 2, b], Map[a + 2, b + 1], Map[a + 2, b + 2] };
                                bool water2 = true;
                                foreach (string bor in bordering3water)
                                {
                                    if (bor == "O")
                                        water2 = true;

                                }
                                foreach (string bor in bordering3)
                                {
                                    if (water2)
                                    {

                                        if (bor == "+" && rand.Next(1, 34) == 1)
                                        {
                                            newMap[a, b] = "+";
                                            break;
                                        }
                                    }

                                    else
                                    {
                                        if (bor == "+" && rand.Next(1, 34) != 1)
                                        {
                                            newMap[a, b] = "+";

                                            break;
                                        }
                                    }

                                }
                                if (newMap[a, b] != "+" && rand.Next(1, 21) == 1)
                                    newMap[a, b] = "T";
                                break;
                        }

                    }
                }
                for (int a = 0; a < 23; a++)
                {
                    for (int b = 0; b < 77; b++)
                    {
                        if (Map[a, b] != newMap[a, b])
                        {

                            switch (newMap[a, b])
                            {
                                case "-":
                                    if(BlankVis == false)
                                        Console.ForegroundColor = ConsoleColor.Black;
                                    else
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.SetCursorPosition(b + 3, a + 1);
                                    Console.Write("\b-");
                                    Map[a, b] = "-";
                                    break;
                                case "T":
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    if (OnlyBlocks)
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                    else
                                        Console.BackgroundColor = ConsoleColor.Black;
                                    Console.SetCursorPosition(b + 3, a + 1);
                                    Console.Write("\bT");
                                    Map[a, b] = "T";
                                    break;
                                case "+":
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition(b + 3, a + 1);
                                    Console.Write("\b+");
                                    Map[a, b] = "+";
                                    break;
                                case "t":
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    if (OnlyBlocks)
                                        Console.BackgroundColor = ConsoleColor.Green;
                                    else
                                        Console.BackgroundColor = ConsoleColor.Black;
                                    Console.SetCursorPosition(b + 3, a + 1);
                                    Console.Write("\bT");
                                    Map[a, b] = "t";
                                    break;
                                case "~":
                                    if (ScorchedEarthVis == false)
                                        Console.ForegroundColor = ConsoleColor.Black;
                                    else
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.SetCursorPosition(b + 3, a + 1);
                                    Console.Write("\b~");
                                    Map[a, b] = "~";
                                    break;
                                case "D":
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    if (OnlyBlocks)
                                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    else
                                        Console.BackgroundColor = ConsoleColor.Black;
                                    Console.SetCursorPosition(b + 3, a + 1);
                                    Console.Write("\bT");
                                    Map[a, b] = "D";
                                    break;
                                case "O":
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                                    Console.SetCursorPosition(b + 3, a + 1);
                                    Console.Write("\bO");
                                    Map[a, b] = "O";
                                    break;
                                default:
                                    Map[a, b] = "-";
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.SetCursorPosition(b + 3, a + 1);
                                    Console.Write("\b-");
                                    break;
                            }
                        }
                        
                    }
                }
                Console.CursorVisible = false;
                Thread.Sleep(speed);


            }
            while (true);
        }
    }
}
