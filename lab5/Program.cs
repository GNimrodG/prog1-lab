using System.IO;

namespace lab5;

class Program
{
    static void Main(string[] args)
    {
        // Feladat1();
        // Feladat2();
        // Feladat3();
        Feladat4();
    }

    static void Feladat1()
    {
        StreamReader sr = new StreamReader("colorem_ipsum.txt");
        string sor;
        while ((sor = sr.ReadLine()) != null)
        {
            string[] parts = sor.Split('#');
            Console.ForegroundColor = Enum.TryParse(parts[0], out ConsoleColor color) ? color : ConsoleColor.White;
            Console.WriteLine(parts[1]);
        }
    }

    static void Feladat2()
    {
        StreamWriter sw = new StreamWriter("lotto.txt");
        DateTime date = DateTime.Now;
        while (true)
        {
            int[] numbers = new int[5];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = GetUniqueRandomNumber(1, 91, numbers);
            }

            Console.WriteLine($"[{date:yyyy. MM. dd.}] {string.Join(" ", numbers)}");
            sw.WriteLine($"[{date:yyyy. MM. dd.}] {string.Join(" ", numbers)}");

            Console.Write("Another week? (y/N): ");

            if (Console.ReadLine()?.ToLower() != "y")
                break;

            date = date.AddDays(7);
        }

        return;

        int GetUniqueRandomNumber(int min, int max, int[] numbers)
        {
            Random rnd = new();
            int num = rnd.Next(min, max);
            while (numbers.Contains(num))
            {
                num = rnd.Next(min, max);
            }

            return num;
        }
    }

    static void Feladat3()
    {
        StreamReader sr = new StreamReader("ant_instructions.txt");

        string[] baseParts = sr.ReadLine().Split(' ');
        double x = double.Parse(baseParts[0]);
        double y = double.Parse(baseParts[1]);
        int angle = int.Parse(baseParts[2]);

        Console.WriteLine($"[{x}, {y}] {angle} fok");

        while (!sr.EndOfStream)
        {
            string sor = sr.ReadLine();
            Console.WriteLine(sor);
            string[] parts = sor.Split(' ');
            switch (parts[0])
            {
                case "go":
                    int steps = int.Parse(parts[1]);
                    double angleRad = angle * Math.PI / 180;
                    x += steps * Math.Cos(angleRad);
                    y += steps * Math.Sin(angleRad);
                    break;
                case "right":
                    angle += int.Parse(parts[1]);
                    angle %= 360;
                    break;
                case "left":
                    angle -= int.Parse(parts[1]);
                    angle %= 360;
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }

            Console.WriteLine($"[{x}, {y}] {angle} fok");
        }
    }
    static void Feladat4()
    {
        StreamReader sr = new("NHANES_1999-2018.csv");

        List<int> seqn = new(); // ID
        List<string> survey = new(); // time of the survey
        List<byte> riagendr = new(); // gender (1=male,2=female)
        List<byte> ridageyr = new(); // age
        List<float> bmxbmi = new(); // BMI
        List<float> lbdglusi = new(); // blood sugar level

        sr.ReadLine(); // skip header

        while (!sr.EndOfStream)
        {
            string[] parts = sr.ReadLine().Split(',');
            seqn.Add(int.Parse(parts[0]));
            survey.Add(parts[1]);
            riagendr.Add((byte)float.Parse(parts[2]));
            ridageyr.Add((byte)float.Parse(parts[3]));
            bmxbmi.Add(float.Parse(parts[4]));
            lbdglusi.Add(float.Parse(parts[5]));
        }

        float sumFemaleBmi = 0;
        int countFemale = 0;
        float sumMaleBmi = 0;
        int countMale = 0;

        int countHighBloodSugar = 0; // > 5.6

        int maxBmiIndex = 0;

        int sumAgeOverweight = 0; // BMI >= 30
        int countOverweight = 0;

        for (int i = 0; i < seqn.Count; i++)
        {
            if (riagendr[i] == 1)
            {
                sumMaleBmi += bmxbmi[i];
                countMale++;
            }
            else
            {
                sumFemaleBmi += bmxbmi[i];
                countFemale++;
            }

            if (lbdglusi[i] > 5.6)
            {
                countHighBloodSugar++;
            }

            if (bmxbmi[i] > bmxbmi[maxBmiIndex])
            {
                maxBmiIndex = i;
            }

            if (bmxbmi[i] >= 30)
            {
                sumAgeOverweight += ridageyr[i];
                countOverweight++;
            }
        }

        Console.WriteLine($"Nők átlag BMI: {sumFemaleBmi / countFemale}\nFérfiak átlag BMI: {sumMaleBmi / countMale}");
        Console.WriteLine(
            $"5.6-nál magasabb vércukorszintűek százalékos aránya: {(float)countHighBloodSugar / seqn.Count * 100}%");
        Console.WriteLine($"Max BMI-vel({bmxbmi[maxBmiIndex]}) vércukorszint: {lbdglusi[maxBmiIndex]}");
        Console.WriteLine($"Túlsúlyosak átlag életkora: {(float)sumAgeOverweight / countOverweight}");
    }
}