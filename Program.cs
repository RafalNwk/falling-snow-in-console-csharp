using System;
using System.Threading;

namespace ConsoleApp1
{
    public class Plansza
    {
        static readonly Random rnd = new Random();
        public static int szerokosc = 75;
        public static int wysokosc = 45;
        public static int ilesniezek = 1; //można zmienić ilość generowanych śnieżek na samej górze co odświeżenie
        public static int odswiezanie = 50; //odświeżanie w ms

        char[,] plansza = new char[wysokosc, szerokosc];

        public Plansza() //tworzenie planszy
        {
            for (int i = 0; i < wysokosc; i++) //tworzenie podłogi
            {
                for (int j = 1; j < szerokosc - 1; j++)
                {
                    if (i == wysokosc - 1)
                    {
                        plansza[i, j] = '-';
                    }
                    else plansza[i, j] = ' ';
                }
            }
            for (int i = wysokosc - 2; i >= wysokosc - 7; i--) //tworzenie pnia drzewa
            {
                for (int j = 35; j <= 38; j++)
                {
                    plansza[i, j] = '1';
                }
            }
            int x = 20;
            int condlugosc = 34;
            for (int i = wysokosc - 8; i >= wysokosc - 25; i--) //tworzenie reszty drzewa
            {
                for (int dlugosc = 0; dlugosc <= condlugosc; dlugosc++)
                {
                    plansza[i, x + dlugosc] = '4';
                }
                x++;
                condlugosc -= 2;
            }
        }
        public void GenerujPlansze() //obliczenia planszy
        {
            for (int i = wysokosc - 3; i >= 0; i--)
            {
                for (int j = 1; j < szerokosc - 1; j++)
                {
                    if (plansza[i, j] == '*')
                    { 
                        //spadanie śniegu w dół
                        if (plansza[i + 1, j] == ' ')
                        {
                            plansza[i, j] = ' ';

                            //////////generowanie losowych ruchów prawo lewo
                            int x;
                            if (j == 0) x = rnd.Next(3);
                            else if (j == 1) x = rnd.Next(4) - 1;
                            else if (j == szerokosc - 1) x = rnd.Next(3) - 2;
                            else if (j == szerokosc - 2) x = rnd.Next(4) - 3;
                            else x = rnd.Next(5) - 2;
                            //////////
                            if (plansza[i + 1, j + x] != ' ') plansza[i + 1, j] = '*';
                            else plansza[i + 1, j + x] = '*';
                        }

                        //naturalne zsypywanie się śniegu
                        if (plansza[i + 1, j] == '*' && plansza[i + 2, j] == '*' && (plansza[i + 2, j + 1] == ' ' || plansza[i + 1, j - 1] == ' '))
                        {
                            if (plansza[i + 1, j + 1] == ' ' || plansza[i + 1, j - 1] == ' ')
                            {
                                int x = rnd.Next(2);
                                if (x == 0 && plansza[i + 1, j - 1] == ' ')
                                {
                                    plansza[i, j] = ' ';
                                    plansza[i + 1, j - 1] = '*';
                                }
                                else if (x == 1 && plansza[i + 1, j + 1] == ' ')
                                {
                                    plansza[i, j] = ' ';
                                    plansza[i + 1, j + 1] = '*';
                                }
                            }
                        }
                    }
                }
            }
            for (int j = 1; j < szerokosc - 1; j++) //czyszczenie pierwszej linii
            {
                plansza[0, j] = ' ';
            }
            for (int i = 0; i < ilesniezek; i++) //generowanie sniegu w pierwszej linii
            {
                plansza[0, 1 + rnd.Next(szerokosc - 2)] = '*';
            }
        }
        public void WyswietlPlansze() //wyswietlanie planszy
        {
            for (int i = 0; i < wysokosc; i++)
            {
                for (int j = 0; j < szerokosc; j++)
                {
                    Console.Write(plansza[i, j]);
                }
                Console.WriteLine();
            }
        }
        public void WyswietlajWygenerowanePlansze() //wyswietlanie animacji
        {
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                GenerujPlansze();
                WyswietlPlansze();
                Thread.Sleep(odswiezanie);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Plansza.szerokosc + 1, Plansza.wysokosc + 1);
            Console.CursorVisible = false;
            Console.Title = "Pada śnieg, pada śnieg";
            Plansza plansza = new Plansza();
            plansza.WyswietlajWygenerowanePlansze();
        }
    }
}