namespace lab7_2;

class Program
{
    static void Main(string[] args)
    {
        const int width = 11;
        
        Mole mole = new();

        while (true)
        {
            Console.Clear();
            mole.Hide(0, width);

            Console.SetCursorPosition(0, 1);
            Console.WriteLine("A vakond elbújt, vajon hol lehet? (balról jobbra 0-tól {0}-ig)", width - 1);
            int x = int.Parse(Console.ReadLine());

            mole.TurnUp();

            if (x == mole.X)
            {
                Console.WriteLine("Eltaláltad!");
                break;
            }
            else
            {
                Console.WriteLine("Nem találtad el! {0}-nál volt.", mole.X);
                Thread.Sleep(1000);
            }
        }
    }

    class Mole
    {
        private int _x;

        public int X => _x;

        public void TurnUp()
        {
            int cursorTop = Console.CursorTop;
            int cursorLeft = Console.CursorLeft;

            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', _x) + "M");

            Console.SetCursorPosition(cursorLeft, cursorTop);
        }

        public void Hide(int min, int max)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth));

            Random random = new();

            _x = random.Next(min, max);
        }
    }
}