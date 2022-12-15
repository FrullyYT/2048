namespace _2048Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "2048";
            Console.CursorVisible= false;
            Console.WindowWidth = 29;
            Console.WindowHeight = 19;
            Console.BackgroundColor = ConsoleColor.Black;

            welcomeScreen();

            gioco();
        }

        static void welcomeScreen()
        {
            Console.WriteLine(@"  _____ _____ __ __ _____ ");
            Console.WriteLine(@" |     |  _  |  Y  |  _  |");
            Console.WriteLine(@" |__|  |. |  |  |  |. |  |");
            Console.WriteLine(@"  / __/|. |  |___  |. _  |");
            Console.WriteLine(@" |: 1 \|: 1  |  |: |: 1  |");
            Console.WriteLine(@" |:..  |:..  |  |:.|:..  |");
            Console.WriteLine(@" `-----`-----'  `--`-----'");
            Thread.Sleep(1000);
            Console.Clear();
        }

        static void gioco()
        {
            int punteggio = 0;
            int[,] tabellone = tabellaIniziale();
            bool tastoGiusto = true;
            bool ok;

            while(true)
            {
                scriveTabella(tabellone, punteggio);

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
                    break;
                }
            } // while (!mossePossibili(tabellone) || !vinto(punteggio));

            scriveTabella(tabellone, punteggio);

            if (!mossePossibili(tabellone))
            {
                Console.Write("Hai perso");
            }
            else
            {
                Console.Write("Hai vinto!!!");
            }

            char siNo;

            Console.Write(". rigiocare? (s/n) ");
            do
            {
                siNo = Console.ReadKey(true).KeyChar;
            } while (siNo != 's' && siNo != 'n');

            switch (siNo)
            {
                case 's':
                    gioco();
                    break;
                case 'n':
                    Console.Clear();
                    Console.WriteLine("chiusura in corso...");
                    break;
                default:
                    break;
            }
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
            /*tabellone[0, 0] = 2;
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
            tabellone[3, 3] = 2;*/
            //mette i primi 2 numeri iniziali, 2 o 4 con 1/10 di possibilità
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
            }

            return tabellone;
        }

        static void stampaNumeroColorato(string casella, int valore)
        {
            switch (valore)
            {
                case 2:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 16:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case 32:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case 64:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 128:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case 256:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
            }
            Console.Write(casella);
            Console.ForegroundColor = ConsoleColor.White;
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
            //altezza 16 larghezza 19 (altezza + fine/rigiocare ? )
            Console.WriteLine($"punteggio = {punteggio}");
            Console.WriteLine($"╔══════╦══════╦══════╦══════╗");
            Console.WriteLine($"║      ║      ║      ║      ║");

            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[0, 0], tabellone[0, 0]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[0, 1], tabellone[0, 1]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[0, 2], tabellone[0, 2]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[0, 3], tabellone[0, 3]);
            Console.WriteLine("║");

            Console.WriteLine($"║      ║      ║      ║      ║");
            Console.WriteLine($"╠══════╬══════╬══════╬══════╣");
            Console.WriteLine($"║      ║      ║      ║      ║");

            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[1, 0], tabellone[1, 0]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[1, 1], tabellone[1, 1]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[1, 2], tabellone[1, 2]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[1, 3], tabellone[1, 3]);
            Console.WriteLine("║");

            Console.WriteLine($"║      ║      ║      ║      ║");
            Console.WriteLine($"╠══════╬══════╬══════╬══════╣");
            Console.WriteLine($"║      ║      ║      ║      ║");

            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[2, 0], tabellone[2, 0]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[2, 1], tabellone[2, 1]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[2, 2], tabellone[2, 2]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[2, 3], tabellone[2, 3]);
            Console.WriteLine("║");

            Console.WriteLine($"║      ║      ║      ║      ║");
            Console.WriteLine($"╠══════╬══════╬══════╬══════╣");
            Console.WriteLine($"║      ║      ║      ║      ║");

            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[3, 0], tabellone[3, 0]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[3, 1], tabellone[3, 1]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[3, 2], tabellone[3, 2]);
            Console.Write("║");
            stampaNumeroColorato(tabellaStringhe[3, 3], tabellone[3, 3]);
            Console.WriteLine("║");

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
                    if (tabellone[i, e] == 0)
                    {
                        mossePossibili = true;
                    }
                }
            }

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