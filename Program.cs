using System;
using System.Data;

namespace StiriVVrsto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[] znaki = { 'X', 'O' };
            while (true)
            {
                Console.WriteLine("=============");
                Console.WriteLine("Štiri v vrsto");
                Console.WriteLine("=============");
                Console.WriteLine("1: Začetek igre");
                Console.WriteLine("2: Spremeni igralna simbola");
                Console.WriteLine("3: Zapri igro");


                string vnos = Console.ReadLine();
                switch (vnos)
                {
                    case "1":
                        Igra(znaki);
                        break;
                    case "2":
                        ZamenjajSimbol(znaki);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Napaka pri vnosu");
                        break;
                }
            }
        }

        private static void Igra(char[] znaki)
        {
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
            
            for (int k = 0; k < 42; k++)
            {
                IzpisiPolje(igralno_polje);
                Console.Write((igralec ? 2 : 1) + ". igralec na potezi\nvstavite stevilo stolpca: ");
                int stolpec = 0;
                UporabniskiVnos(ref stolpec, prosta_mesta);
                igralno_polje[prosta_mesta[stolpec], stolpec] = znaki[igralec ? 1 : 0];
                prosta_mesta[stolpec] -= 1;
                if (Zmaga(igralno_polje))
                {
                    IzpisiPolje(igralno_polje);
                    Console.WriteLine((igralec ? 2 : 1) + ". igralec je zmagal");
                    Console.ReadKey();
                    return;
                }
                igralec = !igralec;
            }

            IzpisiPolje(igralno_polje);
            Console.WriteLine("igra je izenacena");
            Console.ReadKey();
        }

        private static void UporabniskiVnos(ref int stolpec, int[] prosta_mesta)
        {
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
        static void ZamenjajSimbol(char[] simbola)
        {
            Console.WriteLine("Vnesi novi znak za 1. igralca");
            string vnos = "";
            while (true)
            {
                vnos = Console.ReadLine();
                if (vnos.Length == 1) break;
                Console.WriteLine("Napačen vnos.\nVpiši natanko en znak!");
            }
            simbola[0] = vnos[0];
            Console.WriteLine("Vnesi novi znak za 2. igralca");
            vnos = "";
            while (true)
            {
                vnos = Console.ReadLine();
                if (vnos.Length != 1) 
                {
                    Console.WriteLine("Napačen vnos.\nVpiši natanko en znak!");
                    continue;
                }
                if (simbola[0] == vnos[0]) 
                {
                    Console.WriteLine("Vpiši drugačen simbol!");
                    continue;
                }
                break;
            }
            simbola[1] = vnos[0];
        }
    }
}
