namespace _2048Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int punteggio = 0;
            int[,] tabellone = tabellaIniziale();
            bool tastoGiusto = true;

            while(true)
            {
                scriveTabella(tabellone, punteggio);//bdhsfivbsjhu

                do
                {
                    tastoGiusto = true;
                    //legge un tasto
                    ConsoleKeyInfo tasto = Console.ReadKey();

                    switch (tasto.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            tabellone = sinistra(tabellone, ref punteggio);
                            break;
                        case ConsoleKey.UpArrow:
                            tabellone = su(tabellone, ref punteggio);
                            break;
                        case ConsoleKey.RightArrow:
                            tabellone = destra(tabellone, ref punteggio);
                            break;
                        case ConsoleKey.DownArrow:
                            tabellone = giu(tabellone, ref punteggio);
                            break;

                        default:
                            tastoGiusto = false;
                            break;
                    }
                } while (!tastoGiusto);
                if (!mossePossibili(tabellone))
                {
                    scriveTabella(tabellone, punteggio);
                    break;
                }
            }// while (!mossePossibili(tabellone) || !vinto(punteggio));

            Console.WriteLine("fine");
            Console.ReadLine();
        }

        static int[,] tabellaIniziale()
        {
            Random rnd = new Random();
            int contatore = 0;

            int[,] tabellone = new int[4, 4];

            //riempie il tabellone di 0
            for (int i = 0; i < 3; i++)
            {
                for (int e = 0; e < 3; e++)
                {
                    tabellone[i, e] = 0;
                }
            }
            tabellone[0, 0] = 2;
            tabellone[0, 1] = 2;
            tabellone[0, 2] = 16;
            tabellone[0, 3] = 2;

            tabellone[1, 1] = 8;
            tabellone[1, 2] = 64;
            tabellone[1, 3] = 4;

            tabellone[2, 0] = 2;
            tabellone[2, 1] = 8;
            tabellone[2, 2] = 16;
            tabellone[2, 3] = 8;

            tabellone[3, 1] = 16;
            tabellone[3, 2] = 4;
            tabellone[3, 3] = 2;
            /*mette i primi 2 numeri iniziali, 2 o 4 con 1/10 di possibilità
            while (contatore != 2)
            {
                int dim1 = rnd.Next(0, 4);
                int dim2 = rnd.Next(0, 4);
                if (tabellone[dim1, dim2] == 0)
                {
                    if (rnd.Next(1, 11) == 1)
                    {
                        tabellone[dim1, dim2] = 4;
                    }
                    else
                    {
                        tabellone[dim1, dim2] = 2;
                    }
                    contatore++;
                }
            }*/

            return tabellone;
        }

        static void scriveTabella(int[,] tabellone, int punteggio)
        {
            string[,] tabellaStringhe = new string[4, 4];


            for (int i = 0; i < 4; i++)
            {
                for (int e = 0; e < 4; e++)
                {
                    if (tabellone[i, e] == 0)
                    {
                        tabellaStringhe[i, e] = $"      ";
                    }
                    else if (tabellone[i, e].ToString().Length == 1)
                    {
                        tabellaStringhe[i, e] = $"   {tabellone[i, e]}  ";
                    }
                    else if (tabellone[i, e].ToString().Length == 2)
                    {
                        tabellaStringhe[i, e] = $"  {tabellone[i, e]}  ";
                    }
                    else if (tabellone[i, e].ToString().Length == 3)
                    {
                        tabellaStringhe[i, e] = $"  {tabellone[i, e]} ";
                    }
                    else if (tabellone[i, e].ToString().Length == 4)
                    {
                        tabellaStringhe[i, e] = $" {tabellone[i, e]} ";
                    }
                    else if (tabellone[i, e].ToString().Length == 5)
                    {
                        tabellaStringhe[i, e] = $" {tabellone[i, e]}";
                    }
                    else if (tabellone[i, e].ToString().Length == 6)
                    {
                        tabellaStringhe[i, e] = $"{tabellone[i, e]}";
                    }
                    else
                    {
                        tabellaStringhe[i, e] = "      ";
                    }
                }
            }
            Console.Clear();
            
            Console.WriteLine($"punteggio = {punteggio}");
            Console.WriteLine($"╔══════╦══════╦══════╦══════╗");
            Console.WriteLine($"║      ║      ║      ║      ║");
            Console.WriteLine($"║{tabellaStringhe[0, 0]}║{tabellaStringhe[0, 1]}║{tabellaStringhe[0, 2]}║{tabellaStringhe[0, 3]}║");
            Console.WriteLine($"║      ║      ║      ║      ║");
            Console.WriteLine($"╠══════╬══════╬══════╬══════╣");
            Console.WriteLine($"║      ║      ║      ║      ║");
            Console.WriteLine($"║{tabellaStringhe[1, 0]}║{tabellaStringhe[1, 1]}║{tabellaStringhe[1, 2]}║{tabellaStringhe[1, 3]}║");
            Console.WriteLine($"║      ║      ║      ║      ║");
            Console.WriteLine($"╠══════╬══════╬══════╬══════╣");
            Console.WriteLine($"║      ║      ║      ║      ║");
            Console.WriteLine($"║{tabellaStringhe[2, 0]}║{tabellaStringhe[2, 1]}║{tabellaStringhe[2, 2]}║{tabellaStringhe[2, 3]}║");
            Console.WriteLine($"║      ║      ║      ║      ║");
            Console.WriteLine($"╠══════╬══════╬══════╬══════╣");
            Console.WriteLine($"║      ║      ║      ║      ║");
            Console.WriteLine($"║{tabellaStringhe[3, 0]}║{tabellaStringhe[3, 1]}║{tabellaStringhe[3, 2]}║{tabellaStringhe[3, 3]}║");
            Console.WriteLine($"║      ║      ║      ║      ║");
            Console.WriteLine($"╚══════╩══════╩══════╩══════╝");
        }

        static bool mossePossibili(int[,] tabellone)
        {
            bool mossePossibili = false;

            for (int i = 0; i < 4; i++)
            {
                for (int e = 0; e < 4; e++)
                {
                    if (e != 3)
                    {
                        if (tabellone[i, e] == tabellone[i, e + 1])
                        {
                            mossePossibili = true;
                        }
                    }
                    
                }
            }
            for (int e = 0; e < 4; e++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i != 3)
                    {
                        if (tabellone[i, e] == tabellone[i + 1, e])
                        {
                            mossePossibili = true;
                        }
                    }
                }
            }
                    return mossePossibili;
        }

        static bool vinto(int punteggio)
        {
            bool vinto = false;
            if (punteggio >= 262142)
            {
                vinto = true;
            }
            return vinto;
        }

        static int[,] sinistra(int[,] tabella, ref int punteggio)
        {
            int[,] tabellone = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int e = 0; e < 4; e++)
                {
                    tabellone[i, e] = tabella[i, e];
                }
            }

            //sposta
            for (int a = 0; a < 4; a++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int e = 0; e < 4; e++)
                    {
                        if (e != 0)
                        {
                            if (tabellone[i, e - 1] == 0)
                            {
                                tabellone[i, e - 1] = tabellone[i, e];
                                tabellone[i, e] = 0;
                            }
                        }
                    }
                }
            }
            //comprimi
            for (int i = 0; i < 4; i++)
            {
                for (int e = 0; e < 4; e++)
                {
                    if (e != 3)
                    {
                        if (tabellone[i, e] == tabellone[i, e + 1])
                        {
                            tabellone[i, e] = tabellone[i, e] + tabellone[i, e + 1];
                            tabellone[i, e + 1] = 0;

                            punteggio += tabellone[i, e];
                        }
                    }
                }
            }
            //sposta
            for (int a = 0; a < 4; a++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int e = 0; e < 4; e++)
                    {
                        if (e != 0)
                        {
                            if (tabellone[i, e - 1] == 0)
                            {
                                tabellone[i, e - 1] = tabellone[i, e];
                                tabellone[i, e] = 0;
                            }
                        }
                    }
                }
            }

            if (!matriciUguali(tabellone, tabella))
            {
                tabellone = aggiungiNumero(tabellone);
            }

            return tabellone;
        }

        static int[,] su(int[,] tabella, ref int punteggio)
        {

            int[,] tabellone = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int e = 0; e < 4; e++)
                {
                    tabellone[i, e] = tabella[i, e];
                }
            }
            
            //sposta
            for (int a = 0; a < 4; a++)
            {
                for (int e = 0; e < 4; e++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i != 0)
                        {
                            if (tabellone[i - 1, e] == 0)
                            {
                                tabellone[i - 1, e] = tabellone[i, e];
                                tabellone[i, e] = 0;
                            }
                        }
                    }
                }
            }
            //comprimi
            for (int e = 0; e < 4; e++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i != 3)
                    {
                        if (tabellone[i, e] == tabellone[i + 1, e])
                        {
                            tabellone[i, e] = tabellone[i, e] + tabellone[i + 1, e];
                            tabellone[i + 1, e] = 0;

                            punteggio += tabellone[i, e];
                        }
                    }
                }
            }
            //sposta
            for (int a = 0; a < 4; a++)
            {
                for (int e = 0; e < 4; e++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i != 0)
                        {
                            if (tabellone[i - 1, e] == 0)
                            {
                                tabellone[i - 1, e] = tabellone[i, e];
                                tabellone[i, e] = 0;
                            }
                        }
                    }
                }
            }

            if (!matriciUguali(tabellone, tabella))
            {
                tabellone = aggiungiNumero(tabellone);
            }

            return tabellone;
        }

        static int[,] destra(int[,] tabella, ref int punteggio)
        {
            int[,] tabellone = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int e = 0; e < 4; e++)
                {
                    tabellone[i, e] = tabella[i, e];
                }
            }

            //sposta
            for (int a = 0; a < 4; a++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int e = 3; e >= 0; e--)
                    {
                        if (e != 3)
                        {
                            if (tabellone[i, e + 1] == 0)
                            {
                                tabellone[i, e + 1] = tabellone[i, e];
                                tabellone[i, e] = 0;
                            }
                        }
                    }
                }
            }
            //comprimi
            for (int i = 0; i < 4; i++)
            {
                for (int e = 0; e < 4; e++)
                {
                    if (e != 0)
                    {
                        if (tabellone[i, e] == tabellone[i, e - 1])
                        {
                            tabellone[i, e] = tabellone[i, e] + tabellone[i, e - 1];
                            tabellone[i, e - 1] = 0;

                            punteggio += tabellone[i, e];
                        }
                    }
                }
            }
            //sposta
            for (int a = 0; a < 4; a++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int e = 3; e >= 0; e--)
                    {
                        if (e != 3)
                        {
                            if (tabellone[i, e + 1] == 0)
                            {
                                tabellone[i, e + 1] = tabellone[i, e];
                                tabellone[i, e] = 0;
                            }
                        }
                    }
                }
            }

            if (!matriciUguali(tabellone, tabella))
            {
                tabellone = aggiungiNumero(tabellone);
            }

            return tabellone;
        }

        static int[,] giu(int[,] tabella, ref int punteggio)
        {
            int[,] tabellone = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int e = 0; e < 4; e++)
                {
                    tabellone[i, e] = tabella[i, e];
                }
            }

            //sposta
            for (int a = 0; a < 4; a++)
            {
                for (int e = 0; e < 4; e++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i != 3)
                        {
                            if (tabellone[i + 1, e] == 0)
                            {
                                tabellone[i + 1, e] = tabellone[i, e];
                                tabellone[i, e] = 0;
                            }
                        }
                    }
                }
            }
            //comprimi
            for (int e = 0; e < 4; e++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i != 0)
                    {
                        if (tabellone[i, e] == tabellone[i - 1, e])
                        {
                            tabellone[i, e] = tabellone[i, e] + tabellone[i - 1, e];
                            tabellone[i - 1, e] = 0;

                            punteggio += tabellone[i, e];
                        }
                    }
                }
            }
            //sposta
            for (int a = 0; a < 4; a++)
            {
                for (int e = 0; e < 4; e++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i != 3)
                        {
                            if (tabellone[i + 1, e] == 0)
                            {
                                tabellone[i + 1, e] = tabellone[i, e];
                                tabellone[i, e] = 0;
                            }
                        }
                    }
                }
            }

            if (!matriciUguali(tabellone, tabella))
            {
                tabellone = aggiungiNumero(tabellone);
            }

            return tabellone;
        }

        static int[,] aggiungiNumero(int[,] tabellone)
        {
            Random rnd = new Random();
            int contatore = 0;

            while (contatore != 1)
            {
                int dim1 = rnd.Next(0, 4);
                int dim2 = rnd.Next(0, 4);
                if (tabellone[dim1, dim2] == 0)
                {
                    if (rnd.Next(1, 11) == 1)
                    {
                        tabellone[dim1, dim2] = 4;
                    }
                    else
                    {
                        tabellone[dim1, dim2] = 2;
                    }
                    contatore++;
                }
            }
            return tabellone;
        }

        static bool matriciUguali(int[,] tabellone, int[,] tabella)
        {
            bool uguali = true;
            for (int i = 0; i < 4; i++)
            {
                for (int e = 0; e < 4; e++)
                {
                    if (tabellone[i, e] != tabella[i, e])
                    {
                        uguali = false;
                    }
                }
            }
            return uguali;
        }
    }
}