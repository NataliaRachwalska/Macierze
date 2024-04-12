using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macierze
{
    internal class Macierz
    {
        public int wielkosc;
        public volatile double[,] dane;
        public Macierz(int wielkosc)
        {
          this.wielkosc = wielkosc;
          this.dane = new double[wielkosc, wielkosc];
        }
      
        public void Print()
        {
            for (int i = 0; i < wielkosc; i++)
            {
                for (int j = 0; j < wielkosc; j++)
                {
                    Console.Write(dane[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public void Losowanie(int ziarno)
        {
            Random rand = new Random(ziarno);
            for (int i = 0; i < wielkosc; i++)
            {
                for (int j = 0; j < wielkosc; j++)
                {
                    dane[i, j] = rand.Next(20); ; 
                }
            }
        }
        public double Oblicz_pole(Macierz A, Macierz B, int i, int j)
        {
            int wielkosc = A.wielkosc;
            double pole = 0;

            for (int k = 0; k < wielkosc; k++)
            {
                pole += A.dane[i, k] * B.dane[k, j];
            }
            return pole;
        }
        public Macierz Mnożenie_macierzy_1 (Macierz A, Macierz B, int liczba_watkow)
        {
            int wielkosc = A.wielkosc;
            Macierz wynikowa = new Macierz(wielkosc);

            Thread[] threads = new Thread[liczba_watkow];
            int kroki = wielkosc / liczba_watkow; 
            int reszta = wielkosc % liczba_watkow;
            int wiersz_poczatkowy = 0;
            int wiersz_koncowy = 0;

            for (int i = 0; i < liczba_watkow; i++)
            {
                wiersz_poczatkowy = i * kroki;
                wiersz_koncowy = (i + 1) * kroki - 1;
                if (i == liczba_watkow - 1)
                {
                    wiersz_koncowy += reszta;
                }
                int poczatek_mnozenia = wiersz_poczatkowy;
                int koniec_mnozenia = wiersz_koncowy;

                threads[i] = new Thread(() =>
                {
                    for (int j = poczatek_mnozenia; j <= koniec_mnozenia; j++)
                    {
                        for (int k = 0; k < wielkosc; k++)
                        {
                         wynikowa.dane[j, k] =Oblicz_pole(A, B, j, k);
                        }
                        
                    }
                });

                threads[i].Start();
            }

            foreach (Thread x in threads) x.Join();

            return wynikowa;
        }
        public Macierz Mnożenie_macierzy_2(Macierz A, Macierz B, int liczba_watkow)
        {
            int wielkosc = A.wielkosc;
            Macierz wynikowa = new Macierz(wielkosc);   
          
            int kroki = wielkosc / liczba_watkow;
            int reszta = wielkosc % liczba_watkow;
            int wiersz_poczatkowy = 0;
            int wiersz_koncowy = 0;

            Parallel.For(0, liczba_watkow, i =>
            {
                wiersz_poczatkowy = i * kroki;
                wiersz_koncowy = (i + 1) * kroki - 1;

                if (i == liczba_watkow - 1)
                {
                    wiersz_koncowy += reszta;
                }
                int poczatek_mnozenia = wiersz_poczatkowy;
                int koniec_mnozenia = wiersz_koncowy;
                
                    for (int j = poczatek_mnozenia; j <= koniec_mnozenia; j++)
                    {
                        for (int k = 0; k < wielkosc; k++)
                        {
                            wynikowa.dane[j, k] = Oblicz_pole(A, B, j, k);
                        }

                    }

            });

                
            return wynikowa;
        }
   

    }
}
