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
                    Console.WriteLine("stolpec je poln vpisi se enkrat");
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

        static bool Zmaga(char[,] polje)
        {
            for (int j = 0; j < 6; j++)
            {
                int stevka = 1;
                char znak = polje[j, 0];
                for (int i = 1; i < 7; i++)
                {
                    if (znak == polje[j, i] && znak != ' ') { stevka++; }
                    else
                    {
                        znak = polje[j, i];
                        stevka = 1;
                    }
                    if (stevka == 4) { return true; }

                }

            }

            for (int j = 0; j < 7; j++)
            {
                int stevka = 1;
                char znak = polje[0, j];
                for (int i = 1; i < 6; i++)
                {
                    if (znak == polje[i, j] && znak != ' ') { stevka++; }
                    else
                    {
                        znak = polje[i, j];
                        stevka = 1;
                    }
                    if (stevka == 4) { return true; }
                }
            }

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (polje[j, i] == ' ') { continue; }
                    int stevka = 1;
                    for (int k = 1; k < 4; k++)
                    {
                        if (polje[j, i] == polje[j + k, i + k]) { stevka++; }
                    }
                    if (stevka == 4) { return true; }
                }
            }

            for (int j = 0; j < 3; j++)
            {
                for (int i = 3; i < 7; i++)
                {
                    if (polje[j, i] == ' ') { continue; }
                    int stevka = 1;
                    for (int k = 1; k < 4; k++)
                    {
                        if (polje[j, i] == polje[j + k, i - k]) { stevka++; }
                    }
                    if (stevka == 4) { return true; }
                }
            }

            return false;
        }
    }
}
