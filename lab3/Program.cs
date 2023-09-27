namespace lab3;

class Program
{
    static void Main(string[] args)
    {
        string[] kartyapakli = Feladat1();
        Console.WriteLine($"A keverés előtt: {string.Join(", ", kartyapakli)}");
        Feladat2(kartyapakli);
        Console.WriteLine($"A keverés után: {string.Join(", ", kartyapakli)}");
        Feladat3();
        Feladat4();
        Feladat5();
        Feladat6();
        Feladat7();
        Feladat8();
        Feladat9();
        Feladat10();
    }

    static string[] Feladat1()
    {
        string[] szinek = { "Kőr", "Káró", "Treff", "Pikk" };
        string[] lapok = new string[52];
        for (int i = 0; i < szinek.Length; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                int index = i * 13 + j;
                if (j < 9)
                    lapok[index] = szinek[i] + " " + (j + 2);
                else
                    lapok[index] = j switch
                    {
                        9 => szinek[i] + " Jumbó",
                        10 => szinek[i] + " Dáma",
                        11 => szinek[i] + " Király",
                        12 => szinek[i] + " Ász",
                        _ => lapok[index]
                    };
            }
        }

        return lapok;
    }

    static void Feladat2(string[] kartyapakli)
    {
        for (int i = 0; i < kartyapakli.Length; i++)
        {
            int j = new Random().Next(i, kartyapakli.Length);
            // (kartypakli[j], kartypakli[i]) = (kartypakli[i], kartypakli[j]);
            string temp = kartyapakli[j];
            kartyapakli[j] = kartyapakli[i];
            kartyapakli[i] = temp;
        }
    }

    static void Feladat3()
    {
        Console.Write("Adjon meg 3 szót szóközzel elválasztva: ");
        string[] szavak = Console.ReadLine().Split(' ');

        Console.Write("Adjon meg még 1 szót: ");
        string szo = Console.ReadLine();

        bool bennevan = false;
        int index = 0;

        for (int i = 0; i < szavak.Length; i++)
        {
            if (szavak[i] != szo) continue;
            bennevan = true;
            index = i;
            break;
        }

        if (bennevan)
            Console.WriteLine($"A \"{szo}\" szó benne van a szavak között, a {index + 1}. helyen.");
        else
            Console.WriteLine($"A \"{szo}\" szó nincs benne a szavak között.");
    }

    static void Feladat4()
    {
        List<string> szavak = new List<string>();

        Console.WriteLine("Adjon meg szavakat, majd STOP beírásával befejezheti!");

        while (true)
        {
            string input = Console.ReadLine();
            if (input == "STOP") break;
            szavak.Add(input);
        }

        Console.Write("Adjon meg még 1 szót: ");
        string szo = Console.ReadLine();

        bool bennevan = false;
        int index = 0;

        for (int i = 0; i < szavak.Count; i++)
        {
            if (szavak[i] != szo) continue;
            bennevan = true;
            index = i;
            break;
        }

        if (bennevan)
            Console.WriteLine($"A \"{szo}\" szó benne van a szavak között, a {index + 1}. helyen.");
        else
            Console.WriteLine($"A \"{szo}\" szó nincs benne a szavak között.");
    }

    static void Feladat5()
    {
        List<string> nevek = new List<string>();
        List<int> eletkorok = new List<int>();
        List<bool> tapasztalatok = new List<bool>();

        Console.WriteLine("Adjon meg személyeket, majd üres név beírásával befejezheti!");
        while (true)
        {
            Console.Write("Név: ");
            string nev = Console.ReadLine();
            if (nev == "") break;
            Console.Write("Életkor: ");
            int eletkor = int.Parse(Console.ReadLine());
            Console.Write("Tapasztalat(y/n): ");
            bool tapasztalat = Console.ReadLine().ToLower() == "y";

            nevek.Add(nev);
            eletkorok.Add(eletkor);
            tapasztalatok.Add(tapasztalat);
        }

        int atlagEletkor = 0;

        foreach (int eletkor in eletkorok)
        {
            atlagEletkor += eletkor;
        }

        atlagEletkor /= eletkorok.Count;

        Console.WriteLine($"A személyek átlag életkora: {atlagEletkor}");

        int tapasztalatNelkuliekSzama = 0;
        int atlagEletkorTapasztalatNelkul = 0;
        int legidosebbIndex = 0;

        for (int i = 0; i < eletkorok.Count; i++)
        {
            if (tapasztalatok[i])
            {
                if (eletkorok[i] > eletkorok[legidosebbIndex])
                    legidosebbIndex = i;
                continue;
            }

            atlagEletkorTapasztalatNelkul += eletkorok[i];
            tapasztalatNelkuliekSzama++;
        }

        atlagEletkorTapasztalatNelkul /= tapasztalatNelkuliekSzama;

        Console.WriteLine($"Átlag életkor tapasztalat nélkül: {atlagEletkorTapasztalatNelkul}");

        Console.WriteLine(
            $"A legidősebb tapasztalt személy: {nevek[legidosebbIndex]} ({eletkorok[legidosebbIndex]} éves)");
    }

    static void Feladat6()
    {
        Console.Write("N: ");
        int n = int.Parse(Console.ReadLine());
        Console.Write("M: ");
        int m = int.Parse(Console.ReadLine());

        int[,] matrix = new int[n, m];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = new Random().Next(-9, 9);
                Console.Write($"{(matrix[i, j] > -1 ? "+" : "")}{matrix[i, j]} ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();

        int[,] transzponalt = new int[m, n];
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                transzponalt[i, j] = matrix[j, i];
                Console.Write($"{(transzponalt[i, j] > -1 ? "+" : "")}{transzponalt[i, j]} ");
            }

            Console.WriteLine();
        }
    }

    static void Feladat7()
    {
        const int horgaszokSzama = 5;
        const int halfajtakSzama = 10;
        int[,] matrix = new int[horgaszokSzama, halfajtakSzama];

        Console.Write(new string(' ', 12));
        for (int i = 0; i < halfajtakSzama; i++)
        {
            Console.Write($"|{i + 1:00}");
        }

        Console.WriteLine("|Össz.");

        for (int i = 0; i < horgaszokSzama; i++)
        {
            for (int j = 0; j < halfajtakSzama; j++)
            {
                matrix[i, j] = new Random().Next(0, 5);
            }
        }

        int[] sumByHorgaszok = new int[horgaszokSzama];
        for (int i = 0; i < horgaszokSzama; i++)
        {
            for (int j = 0; j < halfajtakSzama; j++)
            {
                sumByHorgaszok[i] += matrix[i, j];
            }
        }

        int winnerHorgaszCount = 0;
        for (int i = 0; i < horgaszokSzama; i++)
        {
            if (sumByHorgaszok[i] > winnerHorgaszCount)
                winnerHorgaszCount = sumByHorgaszok[i];
        }

        int[] sumByFajtak = new int[halfajtakSzama];
        for (int i = 0; i < horgaszokSzama; i++)
        {
            for (int j = 0; j < halfajtakSzama; j++)
            {
                sumByFajtak[j] += matrix[i, j];
            }
        }

        for (int i = 0; i < horgaszokSzama; i++)
        {
            if (sumByHorgaszok[i] == winnerHorgaszCount)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }

            if (sumByHorgaszok[i] == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.Write($"{i + 1:00}. horgász ");
            for (int j = 0; j < halfajtakSzama; j++)
            {
                Console.Write($"|{matrix[i, j].ToString(),2}");
            }

            Console.WriteLine($"|{sumByHorgaszok[i].ToString(),2}");
            Console.ResetColor();
        }


        Console.Write(new string('-', 12));
        for (int i = 0; i < halfajtakSzama; i++)
        {
            Console.Write("|--");
        }

        Console.WriteLine("|--");

        Console.Write(" Összesítve ");
        for (int i = 0; i < halfajtakSzama; i++)
        {
            Console.Write($"|{sumByFajtak[i].ToString(),2}");
        }

        Console.WriteLine("|");
    }

    static void Feladat8()
    {
        List<int> szamok = new List<int>();

        Console.Write("Adjon meg egy számot: ");
        int input = int.Parse(Console.ReadLine());
        szamok.Add(input);
        while (true)
        {
            int szam = szamok[szamok.Count - 1];
            if (szam == 1) break;
            Console.Write($"{szam} -> ");
            if (szam % 2 == 0)
            {
                szamok.Add(szam / 2);
            }
            else
            {
                szamok.Add(3 * szam + 1);
            }
        }

        Console.WriteLine("1");
    }

    static void Feladat9()
    {
        int[] x = { 1, 2, 3, 4, 5, 6, 7, 8 };
        for (int i = 0;
             i < x.Length / 2;
             i++) // csak a feleig megyunk, mert különben visszacserélné a már megcserélt elemeket
        {
            int tmp = x[i];
            x[i] = x[x.Length - i - 1];
            x[x.Length - i - 1] = tmp; // -1, mert az utolsó elem indexe 7, nem 8
        }

        Console.WriteLine(string.Join(", ", x));
    }

    static void Feladat10()
    {
        const int length = 10;

        int[] array = new int[length];
        List<int> list = new();

        for (int i = 0; i < length; i++)
        {
            array[i] = new Random().Next(1, 10);
            list.Add(new Random().Next(1, 10));
        }

        int[] everySecondArray = new int[length / 2];
        List<int> everySecondList = new();

        for (int i = 0; i < length; i += 2)
        {
            everySecondArray[i / 2] = array[i];
            everySecondList.Add(list[i]);
        }

        for (int i = 0; i < array.Length / 2; i++)
        {
            (array[i], array[array.Length - i - 1]) = (array[array.Length - i - 1], array[i]);
            (list[i], list[list.Count - i - 1]) = (list[list.Count - i - 1], list[i]);
        }

        Console.WriteLine(string.Join(", ", array));
        Console.WriteLine(string.Join(", ", list));

        Console.WriteLine(string.Join(", ", everySecondArray));
        Console.WriteLine(string.Join(", ", everySecondList));

        // Rendezzük a lehető legkisebb négyzetes mátrixba a gyűjtemény elemeit (az esetlegesen üresen maradó értékek helyére nulla kerüljön).
    }
}