namespace lab7;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("N: ");
        int n = int.Parse(Console.ReadLine());

        ExamResult[] examResults = new ExamResult[n];

        for (int i = 0; i < n; i++)
        {
            examResults[i] = new ExamResult();
        }

        Console.Write("Sikeres dolgozatok:");

        float avgScore = 0;
        int highestScoreIndex = 0;

        for (var i = 0; i < examResults.Length; i++)
        {
            var examResult = examResults[i];

            if (examResult.Passed)
            {
                Console.Write($" {examResult.NeptunCode}");
            }

            avgScore += examResult.Score;

            if (examResult.Score > examResults[highestScoreIndex].Score)
            {
                highestScoreIndex = i;
            }
        }

        avgScore /= n;

        Console.WriteLine();

        Console.WriteLine($"Átlag pontszám: {avgScore}");

        Console.WriteLine(
            $"Max pontszám: {examResults[highestScoreIndex].NeptunCode} ({examResults[highestScoreIndex].Score})");
    }

    public enum EGrade
    {
        Elégtelen,
        Elegyseges,
        Kozepes,
        Jo,
        Jeles
    }

    class ExamResult
    {
        private string _neptunCode;
        private byte _score;

        public string NeptunCode
        {
            get => _neptunCode;
            set
            {
                if (value.Length != 6)
                {
                    throw new ArgumentException("Neptun code must be 6 characters long");
                }

                _neptunCode = value;
            }
        }

        public byte Score
        {
            get => _score;
            set
            {
                if (value > 100)
                {
                    throw new ArgumentException("Score must be between 0 and 100");
                }

                _score = value;
            }
        }

        public bool Passed => Score >= 50;

        public EGrade Grade(byte[] scoreLimits)
        {
            if (Score < scoreLimits[1])
            {
                return EGrade.Elégtelen;
            }

            if (Score < scoreLimits[2])
            {
                return EGrade.Elegyseges;
            }

            if (Score < scoreLimits[3])
            {
                return EGrade.Kozepes;
            }

            if (Score < scoreLimits[4])
            {
                return EGrade.Jo;
            }

            return EGrade.Jeles;
        }

        public ExamResult(string neptunCode, byte score)
        {
            NeptunCode = neptunCode;
            Score = score;
        }

        public ExamResult()
        {
            Random random = new();

            const string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string neptunCode = "";

            neptunCode += (char)random.Next('A', 'Z' + 1);

            for (int j = 0; j < 5; j++)
            {
                neptunCode += charSet[random.Next(0, charSet.Length)];
            }

            NeptunCode = neptunCode;

            Score = (byte)random.Next(0, 101);
        }
    }
}