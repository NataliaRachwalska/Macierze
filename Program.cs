namespace Macierze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int ziarno_1, ziarno_2, wielkosc;

            Console.WriteLine("Podaj ziarno dla pierwszej macierzy:");
            while (!int.TryParse(Console.ReadLine(), out ziarno_1))
            {
                Console.WriteLine("Niepoprawne dane. Podaj liczbę całkowitą:");
            }

            Console.WriteLine("Podaj ziarno dla drugiej macierzy:");
            while (!int.TryParse(Console.ReadLine(), out ziarno_2))
            {
                Console.WriteLine("Niepoprawne dane. Podaj liczbę całkowitą:");
            }

            Console.WriteLine("Podaj wielkość macierzy:");
            while (!int.TryParse(Console.ReadLine(), out wielkosc))
            {
                Console.WriteLine("Niepoprawne dane. Podaj liczbę całkowitą:");
            }
            Macierz macierz_1 = new Macierz(wielkosc);
            macierz_1.Losowanie(ziarno_1);
            Macierz macierz_2 = new Macierz(wielkosc);
            macierz_2.Losowanie(ziarno_2);

            //Console.WriteLine("Wygenerowana macierz pierwsza:");
            //macierz_1.Print();
            //Console.WriteLine("Wygenerowana macierz druga:");
            //macierz_2.Print();
            
               DateTime startTime = DateTime.Now;
               Macierz macierz_wynikowa = macierz_1.Mnożenie_macierzy_2(macierz_1, macierz_2, 3);
               DateTime stopTime = DateTime.Now;
               TimeSpan roznica = stopTime - startTime;
               Console.WriteLine("Czas pracy:" + roznica.TotalMilliseconds);
            
            //Console.WriteLine("Wygenerowana macierz końcowa:");
            //macierz_wynikowa.Print();

        }
    }
}
