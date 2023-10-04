using System.Text;
using System.Text.RegularExpressions;

namespace lab4;

class Program
{
    static void Main(string[] args)
    {
        Feladat1();
        Feladat2();
        Feladat3();
        Feladat3_NoRegex();
        Feladat4();
        Feladat5();
        Feladat6();
        Feladat7();
        Feladat8();
        Feladat9();
        Feladat10();
        Feladat11();
    }

    static void Feladat1()
    {
        Console.Write("Kérek egy szöveget: ");
        string text = Console.ReadLine();

        int letterCount = 0;
        int numberCount = 0;
        int maganhangzoCount = 0;

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                letterCount++;
                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                {
                    maganhangzoCount++;
                }
            }
            else if (char.IsDigit(c))
            {
                numberCount++;
            }
        }

        Console.WriteLine(
            $"A szövegben {letterCount} betű, {numberCount} számjegy és {maganhangzoCount} magánhangzó van.");
    }

    static void Feladat2()
    {
        Console.Write("Kérek egy szöveget: ");
        string text = Console.ReadLine();

        bool isPalindrome = true;
        for (int i = 0; i < text.Length / 2; i++)
        {
            if (text[i] == text[text.Length - i - 1]) continue;
            isPalindrome = false;
            break;
        }

        Console.WriteLine($"A szöveg {(isPalindrome ? "palindróma" : "nem palindróma")}");
    }

    static void Feladat3()
    {
        Console.Write("Kérek egy rendszámot: ");
        string text = Console.ReadLine().ToUpper();

        var match = Regex.Match(text, @"([A-Z]).*([A-Z]).*([A-Z]).*([A-Z]).*(\d).*(\d).*(\d)");

        if (match.Success)
        {
            Console.WriteLine(
                $"A rendszám {match.Groups[1].Value}{match.Groups[2].Value} {match.Groups[3].Value}{match.Groups[4].Value}-{match.Groups[5].Value}{match.Groups[6].Value}{match.Groups[7].Value}");
        }
        else
        {
            Console.WriteLine("Nem rendszám!");
        }
    }

    static void Feladat3_NoRegex()
    {
        Console.Write("Kérek egy rendszámot: ");
        string text = Console.ReadLine().ToUpper();

        string plate = "";

        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsLetter(text[i]))
            {
                plate += text[i];
                switch (plate.Length)
                {
                    case 2:
                        plate += " ";
                        break;
                    case 5:
                        plate += "-";
                        break;
                }
            }
            else if (char.IsDigit(text[i]))
            {
                plate += text[i];
            }
        }

        Console.WriteLine(plate);
    }

    static void Feladat4()
    {
        Console.Write("Mennyi rendszámot szeretne generálni? ");
        int count = int.Parse(Console.ReadLine());

        Random random = new();

        for (int i = 0; i < count; i++)
        {
            string plate = "";
            for (int j = 0; j < 2; j++)
            {
                plate += (char)random.Next('A', 'Z' + 1);
            }

            plate += " ";

            for (int j = 0; j < 2; j++)
            {
                plate += (char)random.Next('A', 'Z' + 1);
            }

            plate += "-";

            for (int j = 0; j < 3; j++)
            {
                plate += random.Next(0, 10);
            }

            Console.WriteLine(plate);
        }
    }

    static void Feladat5()
    {
        Console.Write("Email cím: ");
        string email = Console.ReadLine();

        /*
         *  a) pontosan egy @ karaktert tartalmaz
         *  b) tartalmaz legalább egy betű karaktert a @ előtt
         *  c) tartalmaz legalább egy . karaktert a @ után
         *  d) a @ és az utolsó . karakter között kell legyen legalább egy betű vagy szám karakter
         *  e) ha tartalmaz . karaktert a @ előtt is, akkor a . előtt és után is betű vagy szám karakter kell álljon
         *  f) az utolsó . karakter után legalább két betűt kell tartalmazzon
         */

        if (email.Length < 6)
        {
            Console.WriteLine("Az email cím túl rövid!");
            return;
        }

        // f) az utolsó . karakter után legalább két betűt kell tartalmazzon
        if (!char.IsLetter(email[^2]) || !char.IsLetter(email[^1]))
        {
            Console.WriteLine("Az utolsó két karakter nem betű!");
            return;
        }

        bool hasOtherError = false;
        bool hasAt = false;
        bool hasLetterBeforeAt = false;
        bool hasDotAfterAt = false;

        int atIndex = -1; // email.IndexOf('@');
        int lastDotIndex = -1; // email.LastIndexOf('.');

        for (int i = 0; i < email.Length; i++)
        {
            if (email[i] == '@')
            {
                // a) pontosan egy @ karaktert tartalmaz
                if (hasAt)
                {
                    Console.WriteLine("Több @ karakter!");
                    hasOtherError = true;
                    break;
                }

                hasAt = true;
                atIndex = i;

                if (i == 0)
                {
                    Console.WriteLine("Nincs karakter a @ előtt!");
                    hasOtherError = true;
                    break;
                }

                // b) tartalmaz legalább egy betű karaktert a @ előtt
                for (int j = 0; j < i; j++)
                {
                    if (!char.IsLetter(email[j])) continue;
                    hasLetterBeforeAt = true;
                    break;
                }
            }
            else if (email[i] == '.')
            {
                if (hasAt)
                {
                    // c) tartalmaz legalább egy . karaktert a @ után
                    hasDotAfterAt = true;
                }
                else
                {
                    // e) ha tartalmaz . karaktert a @ előtt is, akkor a . előtt és után is betű vagy szám karakter kell álljon
                    if (i == 0 || !char.IsLetterOrDigit(email[i - 1]) || !char.IsLetterOrDigit(email[i + 1]))
                    {
                        Console.WriteLine("Nincs betű vagy szám karakter a . előtt vagy után a kukac előtt!");
                        hasOtherError = true;
                        break;
                    }
                }

                lastDotIndex = i;
            }
        }

        if (hasOtherError) return;

        bool hasLetterOrDigitBetweenAtAndLastDot = false;

        // d) a @ és az utolsó . karakter között kell legyen legalább egy betű vagy szám karakter
        for (int i = atIndex + 1; i < lastDotIndex; i++)
        {
            if (!char.IsLetterOrDigit(email[i])) continue;
            hasLetterOrDigitBetweenAtAndLastDot = true;
            break;
        }

        if (!hasAt)
        {
            Console.WriteLine("Nincs kukac karakter!");
        }
        else if (!hasLetterBeforeAt)
        {
            Console.WriteLine("Nincs betű a kukac előtt!");
        }
        else if (!hasDotAfterAt)
        {
            Console.WriteLine("Nincs . karakter a kukac után!");
        }
        else if (!hasLetterOrDigitBetweenAtAndLastDot)
        {
            Console.WriteLine("Nincs betű vagy szám karakter a kukac és az utolsó . között!");
        }
        else
        {
            Console.WriteLine("Helyes email cím!");
        }
    }

    static void Feladat6()
    {
        const string neptunKod = "E1XALI";
        Random random = new();

        // 1,572,120,576 lehetőség van, de lehet, hogy duplikált kódokat generálunk
        string karakterek = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        ulong i = 0;

        while (true)
        {
            i++;

            string kod = "";

            kod += (char)random.Next('A', 'Z' + 1);

            for (int j = 0; j < neptunKod.Length - 1; j++)
            {
                kod += karakterek[random.Next(0, karakterek.Length)];
            }

            Console.Write(new string('\b', 27) + $"{kod} {i:D20}");

            if (kod == neptunKod) break;
        }

        Console.WriteLine();
    }

    static void Feladat7()
    {
        Console.Write("Kérek egy szöveget: ");
        string text = Console.ReadLine();

        string spongeCase = "";

        Random random = new();

        for (int i = 0; i < text.Length; i++)
        {
            if (random.Next(0, 2) == 0)
            {
                spongeCase += char.ToUpper(text[i]);
            }
            else
            {
                spongeCase += char.ToLower(text[i]);
            }
        }

        Console.WriteLine(spongeCase);
    }

    static void Feladat8()
    {
        const string text = "Vincent;Vega;Vince\nMarsellus;Wallace;Big Man\nWinston;Wolf;The Wolf";
        string[,] matrix = new string[3, 3];

        string[] rows = text.Split("\n");

        for (int i = 0; i < rows.Length; i++)
        {
            string[] columns = rows[i].Split(";");
            for (int j = 0; j < columns.Length; j++)
            {
                matrix[i, j] = columns[j];
            }
        }

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.WriteLine($"{matrix[i, 0]} {matrix[i, 1]} {matrix[i, 2]}");
        }
    }

    static void Feladat9()
    {
        Console.Write("Kérek egy szöveget: ");
        string text = Console.ReadLine();

        int depth = 0;
        Stack<char> stack = new();

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '(' || text[i] == '[')
            {
                depth++;
                stack.Push(text[i]);
            }
            else if (text[i] == ')' || text[i] == ']')
            {
                if (depth == 0)
                {
                    Console.WriteLine("Hibás zárójel!");
                    return;
                }

                if (stack.Peek() == '(' && text[i] != ')' || stack.Peek() == '[' && text[i] != ']')
                {
                    Console.WriteLine("Hibás zárójel típus!");
                    return;
                }

                depth--;
                stack.Pop();
            }
        }

        Console.WriteLine(depth == 0 ? "Helyes zárójel!" : "Hibás zárójel!");
    }

    static void Feladat10()
    {
        const int n = 20;
        const int m = 50;
        char[,] matrix = new char[n, m];
        int cursorTop = 0;
        int cursorLeft = 0;

        void GoLeft()
        {
            if (cursorLeft > 0)
            {
                cursorLeft--;
            }
            else if (cursorTop > 0)
            {
                cursorTop--;
                cursorLeft = m - 1;
            }
        }

        void GoRight()
        {
            if (cursorLeft < m - 1)
            {
                cursorLeft++;
            }
            else if (cursorTop < n - 1)
            {
                cursorTop++;
                cursorLeft = 0;
            }
        }

        void GoDown()
        {
            if (cursorTop < n - 1)
            {
                cursorTop++;
            }
            else
            {
                cursorTop = 0;
            }
        }

        WriteMatrix(matrix, cursorTop, cursorLeft);
        while (true)
        {
            ConsoleKeyInfo input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    if (cursorTop > 0)
                    {
                        cursorTop--;
                    }
                    else
                    {
                        cursorTop = n - 1;
                    }

                    break;
                case ConsoleKey.Enter:
                    cursorLeft = 0;
                    GoDown();
                    break;
                case ConsoleKey.DownArrow:
                    GoDown();
                    break;
                case ConsoleKey.LeftArrow:
                    GoLeft();
                    break;
                case ConsoleKey.RightArrow:
                    GoRight();
                    break;
                case ConsoleKey.Backspace:
                    GoLeft();
                    matrix[cursorTop, cursorLeft] = ' ';
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    matrix[cursorTop, cursorLeft] = input.KeyChar;
                    GoRight();
                    break;
            }

            WriteMatrix(matrix, cursorTop, cursorLeft);
        }
    }

    static void WriteMatrix(char[,] matrix, int cursorTop, int cursorLeft)
    {
        Console.Clear();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            string row = "";
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                row += matrix[i, j];
            }

            Console.WriteLine(row);
        }

        Console.SetCursorPosition(cursorLeft, cursorTop);
    }

    static void Feladat11()
    {
        Console.Write("Kérek egy szöveget: ");
        string text = Console.ReadLine().ToUpper();

        const string encodeCharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        byte[] bytes = Encoding.Unicode.GetBytes(text);

        string base64 = "";

        for (int i = 0; i < bytes.Length; i += 3)
        {
            // 3 bytes
            byte b1 = bytes[i];
            byte b2 = i + 1 < bytes.Length ? bytes[i + 1] : (byte)0;
            byte b3 = i + 2 < bytes.Length ? bytes[i + 2] : (byte)0;

            // 6 bits
            int c1 = b1 >> 2; // [111111]00 00000000 00000000
            int c2 = (b1 & 0b11) << 4 | b2 >> 4; // 000000[11 1111]0000 00000000
            int c3 = (b2 & 0b1111) << 2 | b3 >> 6; // 00000000 0000[1111 11]000000
            int c4 = b3 & 0b111111; // 00000000 00000000 00[111111]

            // 4 bytes
            base64 += encodeCharSet[c1];
            base64 += encodeCharSet[c2];
            base64 += i + 1 < bytes.Length ? encodeCharSet[c3] : '=';
            base64 += i + 2 < bytes.Length ? encodeCharSet[c4] : '=';
        }

        Console.WriteLine(base64);
    }
}