using System.Diagnostics;

namespace lab9_2;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Bölénycsora mérete: ");
        var n = int.Parse(Console.ReadLine());
        Console.Write("Játékterület mérete: ");
        var m = int.Parse(Console.ReadLine());

        Game game = new(m, n);

        game.Run();
    }

    class Field
    {
        public Field(int size)
        {
            Size = size;
        }

        public int Size;

        public int TargetX => Size - 1;
        public int TargetY => Size - 1;

        public bool AllowedPosition(int x, int y) => x >= 0 && x < Size && y >= 0 && y < Size;

        public void Show()
        {
            Console.Write("||");

            for (var i = 0; i < Size; i++)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(i.ToString().PadLeft(2));
                Console.ResetColor();
            }

            Console.ResetColor();
            Console.WriteLine("||");

            for (var i = 0; i < Size; i++)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(i.ToString().PadLeft(2));
                Console.ResetColor();
                Console.Write(new string(' ', Size * 2));

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(i.ToString().PadLeft(2));
                Console.ResetColor();
            }

            Console.Write("||");

            for (var i = 0; i < Size; i++)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(i.ToString().PadLeft(2));
                Console.ResetColor();
            }

            Console.ResetColor();
            Console.WriteLine("||");
        }
    }

    class Buffalo
    {
        int _x = 0;
        int _y = 0;

        public bool IsActive { get; private set; } = true;

        public int X => _x;
        public int Y => _y;

        public void Move(Field field)
        {
            if (!IsActive) return;

            Random random = new();

            while (true)
            {
                var direction = random.Next(0, 3);

                var x = _x;
                var y = _y;

                switch (direction)
                {
                    case 0:
                        x++;
                        break;
                    case 1:
                        y++;
                        break;
                    case 2:
                        x++;
                        y++;
                        break;
                }

                if (!field.AllowedPosition(x, y)) continue;

                _x = x;
                _y = y;
                break;
            }
        }

        public void Deactivate() => IsActive = false;

        public void Show()
        {
            Console.SetCursorPosition(_x * 2 + 3, _y + 1);
            Console.ForegroundColor = IsActive ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write("B");
            Console.ResetColor();
        }
    }

    class Game
    {
        Field _field;
        List<Buffalo> _buffalos = new();

        public bool IsOver => IsPlayerWon || IsBuffaloWon;

        public bool IsPlayerWon => _buffalos.All(buffalo => !buffalo.IsActive);

        public bool IsBuffaloWon =>
            _buffalos.Any(buffalo => buffalo.X == _field.TargetX && buffalo.Y == _field.TargetY);


        public Game(int fieldSize, int buffaloCount)
        {
            _field = new(fieldSize);

            for (var i = 0; i < buffaloCount; i++)
            {
                _buffalos.Add(new());
            }
        }

        private void VisualizeElements()
        {
            Console.Clear();
            _field.Show();

            foreach (var buffalo in _buffalos.Where(x => !x.IsActive))
            {
                buffalo.Show();
            }

            foreach (var buffalo in _buffalos.Where(x => x.IsActive))
            {
                buffalo.Show();
            }
        }

        private void Shoot(int x, int y)
        {
            foreach (var buffalo in _buffalos.Where(buffalo => buffalo.X == x && buffalo.Y == y))
            {
                buffalo.Deactivate();
            }
        }

        public void Run()
        {
            while (!IsOver)
            {
                VisualizeElements();

                foreach (var buffalo in _buffalos)
                {
                    buffalo.Move(_field);
                }

                Console.SetCursorPosition(0, _field.Size + 2);
                Console.WriteLine($"Maradt: {_buffalos.Count(x => x.IsActive)} bölény");
                Console.Write("Lövés: ");
                var input = Console.ReadLine().Split(' ');
                var x = int.Parse(input[0]);
                var y = int.Parse(input[1]);

                Shoot(x, y);
            }

            VisualizeElements();
            Console.SetCursorPosition(0, _field.Size + 2);

            if (IsPlayerWon)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Nyertél!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Vesztettél!");
            }

            Console.ResetColor();
        }
    }
}