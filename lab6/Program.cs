// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator

namespace lab6;

class Program
{
    static void Main(string[] args)
    {
        Feladat1();
        Feladat2();
        Feladat3();
        Feladat4();
        Feladat5();
    }

    static void Feladat1()
    {
        Book book1 = new Book("Harry Potter and the Philosopher's Stone", "J. K. Rowling", 1997, 223);
        Console.WriteLine(book1.AllData());
    }

    class Book
    {
        public Book(string title, string author, int year, int pageCount)
        {
            Title = title;
            Author = author;
            Year = year;
            PageCount = pageCount;
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int PageCount { get; set; }

        public string AllData()
        {
            return $"{Author}: {Title}, {Year} ({PageCount} pages)";
        }
    }

    static void Feladat2()
    {
        Rectangle rectangle1 = new Rectangle(5, 3, ConsoleColor.Red);
        Rectangle rectangle2 = new Rectangle(2, 4, ConsoleColor.Blue);
        Rectangle rectangle3 = new Rectangle(0, 0, ConsoleColor.Green);
        Rectangle rectangle4 = new Rectangle(1, 1, ConsoleColor.Yellow);

        if (rectangle1.IsValid())
        {
            rectangle1.Draw(0, 0);
        }

        if (rectangle2.IsValid())
        {
            rectangle2.Draw(5, 0);
        }

        if (rectangle3.IsValid())
        {
            rectangle3.Draw(0, 3);
        }

        if (rectangle4.IsValid())
        {
            rectangle4.Draw(5, 4);
        }
    }

    class Rectangle
    {
        public Rectangle(int a, int b, ConsoleColor color)
        {
            A = a;
            B = b;
            Color = color;
        }

        public int A { get; set; }
        public int B { get; set; }
        public ConsoleColor Color { get; set; }

        private int Area()
        {
            return A * B;
        }

        public bool IsValid()
        {
            return Area() > 0;
        }

        public void Draw(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = Color;
            for (int i = 0; i < B; i++)
            {
                Console.SetCursorPosition(x, y + i);
                for (int j = 0; j < A; j++)
                {
                    Console.Write(" ");
                }
            }

            Console.ResetColor();
        }
    }

    static void Feladat3()
    {
        Runner runner1 = new Runner("Józsi", 1, 2);
        Runner runner2 = new Runner("Feri", 2, 3);

        int maxDistance = Console.WindowWidth - 1;

        while (true)
        {
            runner1.RefershDistance(1);
            runner2.RefershDistance(1);

            if (runner1.GetDistance() >= maxDistance || runner2.GetDistance() >= maxDistance)
            {
                break;
            }

            runner1.Show();
            runner2.Show();
            Thread.Sleep(500);
        }
    }

    class Runner
    {
        public Runner(string name, int number, int speed)
        {
            Name = name;
            Number = number;
            Speed = speed;
        }

        public string Name { get; set; }
        public int Number { get; set; }
        public int Speed { get; set; }
        public int Distance { get; set; } = 0;

        public void RefershDistance(int seconds)
        {
            Distance += Speed * seconds;
        }

        public void Show()
        {
            Console.SetCursorPosition(0, Number);
            Console.Write(new string(' ', Distance) + Name[..1]);
        }

        public int GetDistance()
        {
            return Distance;
        }
    }

    static void Feladat4()
    {
        Crypto crypto = new Crypto(3);
        const string message = "Hello World!";

        string encodedMessage = crypto.Encode(message);
        Console.WriteLine(encodedMessage);

        string decodedMessage = crypto.Decode(encodedMessage);
        Console.WriteLine(decodedMessage);
    }

    class Crypto
    {
        private int Key;

        public Crypto(int key)
        {
            Key = key;
        }

        private string TransformMessage(string message, int key)
        {
            string transformedMessage = "";

            foreach (char c in message)
            {
                transformedMessage += (char)(c + key % 26);
            }

            return transformedMessage;
        }

        public string Encode(string message)
        {
            return TransformMessage(message, Key);
        }

        public string Decode(string message)
        {
            return TransformMessage(message, -Key);
        }
    }

    static void Feladat5()
    {
        StreamReader sr = new("NHANES_1999-2018.csv");
        
        List<Entry> entries = new();
        
        sr.ReadLine(); // skip header
        
        while (!sr.EndOfStream)
        {
            entries.Add(new Entry(sr.ReadLine()));
        }
        
        float sumFemaleBmi = 0;
        int countFemale = 0;
        float sumMaleBmi = 0;
        int countMale = 0;

        int countHighBloodSugar = 0; // > 5.6

        int maxBmiIndex = 0;

        int sumAgeOverweight = 0; // BMI >= 30
        int countOverweight = 0;

        for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i].Riagendr == 1)
            {
                sumMaleBmi += entries[i].Bmxbmi;
                countMale++;
            }
            else
            {
                sumFemaleBmi += entries[i].Bmxbmi;
                countFemale++;
            }

            if (entries[i].Lbdglusi > 5.6)
            {
                countHighBloodSugar++;
            }

            if (entries[i].Bmxbmi > entries[maxBmiIndex].Bmxbmi)
            {
                maxBmiIndex = i;
            }

            if (entries[i].Bmxbmi >= 30)
            {
                sumAgeOverweight += entries[i].Ridageyr;
                countOverweight++;
            }
        }

        Console.WriteLine($"Nők átlag BMI: {sumFemaleBmi / countFemale}\nFérfiak átlag BMI: {sumMaleBmi / countMale}");
        Console.WriteLine(
            $"5.6-nál magasabb vércukorszintűek százalékos aránya: {(float)countHighBloodSugar / entries.Count * 100}%");
        Console.WriteLine($"Max BMI-vel({entries[maxBmiIndex].Bmxbmi}) vércukorszint: {entries[maxBmiIndex].Lbdglusi}");
        Console.WriteLine($"Túlsúlyosak átlag életkora: {(float)sumAgeOverweight / countOverweight}");
    }

    class Entry
    {
        public Entry(string line)
        {
            string[] parts = line.Split(',');
            Seqn = int.Parse(parts[0]);
            Survey = parts[1];
            Riagendr = (byte)float.Parse(parts[2]);
            Ridageyr = (byte)float.Parse(parts[3]);
            Bmxbmi = float.Parse(parts[4]);
            Lbdglusi = float.Parse(parts[5]);
        }
        
        public Entry(int seqn, string survey, byte riagendr, byte ridageyr, float bmxbmi, float lbdglusi)
        {
            Seqn = seqn;
            Survey = survey;
            Riagendr = riagendr;
            Ridageyr = ridageyr;
            Bmxbmi = bmxbmi;
            Lbdglusi = lbdglusi;
        }

        public int Seqn { get; set; } // ID
        public string Survey { get; set; } // time of the survey
        public byte Riagendr { get; set; } // gender (1=male,2=female)
        public byte Ridageyr { get; set; } // age
        public float Bmxbmi { get; set; } // BMI
        public float Lbdglusi { get; set; } // blood sugar level
    }
}