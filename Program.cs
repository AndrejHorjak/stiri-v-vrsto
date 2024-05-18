using System;
using System.Data;

namespace StiriVVrsto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*polje je 6x7*/
            bool igralec = false;
            char[,] igralno_polje = new char[6, 7];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    igralno_polje[i, j] = ' ';
                }
            }
            int[] prosta_mesta = new int[7] { 5, 5, 5, 5, 5, 5, 5 };
            char[] znaki = { 'X', 'O' };
            for (int k = 0; k < 42; k++)
            {
                IzpisiPolje(igralno_polje);
                Console.Write((igralec ? 2 : 1) + ". igralec na potezi\nvstavite stevilo stolpca:");
                int stolpec;
                while (true)
                {
                    try
                    {
                        stolpec = int.Parse(Console.ReadLine()) - 1;
                        if (stolpec < 0 || stolpec >= 7)
                        {
                            throw (new FormatException("Index out of range."));
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Napaka pri vnosu.\nVpisite stevilo med 1 in 7!");
                        continue;
                    }

                    if (prosta_mesta[stolpec] >= 0 && prosta_mesta[stolpec] < 7) { break; }
                    else
                    {
                        Console.WriteLine("stolpec je poln vpisi se enkrat");
                    }
                }
                igralno_polje[prosta_mesta[stolpec], stolpec] = znaki[igralec ? 1 : 0];
                prosta_mesta[stolpec] -= 1;
                if (Zmaga(igralno_polje))
                {
                    IzpisiPolje(igralno_polje);
                    Console.WriteLine((igralec ? 2 : 1) + ". igralec je zmagal");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                igralec = !igralec;
            }

            IzpisiPolje(igralno_polje);
            Console.WriteLine("igra je izenacena");
            Console.ReadKey();
        }

        private static void IzpisiPolje(char[,] igralno_polje)
        {
            for (int j = 0; j < 6; j++)
            {
                Console.Write('|');
                for (int i = 0; i < 7; i++)
                {
                    Console.Write(igralno_polje[j, i]);
                    Console.Write('|');
                }
                Console.WriteLine();
            }
            Console.WriteLine(" 1 2 3 4 5 6 7 ");
        }

        static bool VPolju(int x, int y)
        {
            if (x >= 6 || x < 0) return false;
            if (y >= 7 || y < 0) return false;
            return true;
        }

        static bool Zmaga(char[,] polje)
        {
            int[,] smeri = { { 0, 1 }, { 1, 0 }, { 1, 1 }, { 1, -1 } };

            for(int smer = 0; smer < 4; smer++)
            {
                for(int i = 0; i < 6; i++)
                {
                    for(int j = 0; j < 7; j++)
                    {
                        if (polje[i, j] == ' ') { continue; }
                        int stevka = 0, k = 0;
                        do
                        {
                            stevka++;
                            k++;
                            if (!VPolju(i + smeri[smer, 0] * k, j + smeri[smer, 1] * k)) { break; }
                        } while (polje[i, j] == polje[i + smeri[smer, 0] * k, j + smeri[smer, 1] * k] && k<4);
                        if (stevka == 4) { return true; }
                    }
                }
            }
            return false;
        }
    }
}
