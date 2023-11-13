namespace lab9;

class Program
{
    static void Main(string[] args)
    {
        var players = RandomPlayers(20);

        var team = new Team();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Elérhető játékosok:");
            PrintPlayers(players);

            Console.WriteLine("Csapat:");
            PrintPlayers(team.Players, 6);

            if (team.IsFull)
            {
                Console.WriteLine("A csapat megtelt!");
                break;
            }

            Console.Write("Hozzáadni kívánt játékos sorszáma: (kilépés: q) ");
            var input = Console.ReadLine();

            if (input == "q")
                break;

            if (!int.TryParse(input, out var playerIndex) || playerIndex < 0 || playerIndex >= players.Length)
                Console.WriteLine("Hibás sorszám!");
            else
            {
                var player = players[playerIndex];
                if (team.IsIncluded(player))
                    Console.WriteLine("A játékos már a csapatban van!");
                else if (!team.IsAvailable(player))
                    Console.WriteLine("A játékos nem játszhat ebben a posztban!");
                else
                {
                    team.Include(player);
                    Console.WriteLine("A játékos hozzáadva a csapathoz!");
                }
            }

            Console.WriteLine("Nyomj meg egy gombot a folytatáshoz...");
            Console.ReadKey();
        }
    }

    static void PrintPlayers(Player[] players, int? maxColumns = null)
    {
        const int numberColumnWidth = 3;
        const int nameColumnWidth = 15;
        const int positionColumnWidth = 10;

        int columnCount =
            Console.WindowWidth / (numberColumnWidth + nameColumnWidth + positionColumnWidth + 1);

        if (maxColumns.HasValue)
            columnCount = Math.Min(columnCount, maxColumns.Value);

        for (int i = 0; i < columnCount; i++)
        {
            Console.Write(
                $"{"#",numberColumnWidth}{"Név",nameColumnWidth}{"Pozíció",positionColumnWidth}");

            if (i != columnCount - 1)
                Console.Write("|");
        }

        Console.WriteLine();

        for (int i = 0; i < columnCount; i++)
        {
            Console.Write(
                $"{new string('-', numberColumnWidth + nameColumnWidth + positionColumnWidth)}");

            if (i != columnCount - 1)
                Console.Write("|");
        }

        Console.WriteLine();

        for (var i = 0; i < players.Length; i++)
        {
            var player = players[i];


            Console.Write(
                $"{i,numberColumnWidth}{player.Name,nameColumnWidth}{player.Position,positionColumnWidth}");

            if ((i + 1) % columnCount == 0)
            {
                Console.WriteLine();
            }
            else
            {
                Console.Write("|");
            }
        }

        Console.WriteLine();
    }

    static Player[] RandomPlayers(int num)
    {
        var players = new Player[num];
        Random rnd = new();
        for (var i = 0; i < num; i++)
        {
            players[i] = new(
                $"{RandomLastNames[rnd.Next(RandomLastNames.Length)]} {RandomFirstNames[rnd.Next(RandomFirstNames.Length)]}",
                (Position)rnd.Next(4));
        }

        return players;
    }


    enum Position
    {
        kapus,
        vedo,
        center,
        tamado
    }

    class Player
    {
        public Player(string name, Position position)
        {
            this.Name = name;
            this.Position = position;
        }

        public string Name;
        public Position Position;

        public override string ToString()
        {
            return $"{Name} - {Position}";
        }
    }

    class Team
    {
        public Team()
        {
            Players = Array.Empty<Player>();
        }

        public Player[] Players;
        public int NumberOfPlayers => Players.Length;

        public bool IsFull => NumberOfPlayers == 6;

        public bool IsIncluded(Player player) => Players.Contains(player);

        public bool IsAvailable(Player player)
        {
            switch (player.Position)
            {
                default:
                case Position.kapus:
                case Position.vedo:
                    // max 1
                    return Players.All(p => p.Position != player.Position);
                case Position.center:
                case Position.tamado:
                    // max 2
                    return Players.Count(p => p.Position == player.Position) < 2;
            }
        }

        public void Include(Player player)
        {
            Players = Players.Append(player).ToArray();
        }
    }

    private static readonly string[] RandomLastNames =
    {
        "Kovács",
        "Nagy",
        "Tóth",
        "Szabó",
        "Horváth",
        "Varga",
        "Kiss",
        "Molnár",
        "Németh",
        "Farkas",
        "Balogh",
        "Papp",
        "Takács",
        "Juhász",
        "Mészáros",
        "Oláh",
        "Simon",
        "Fekete",
        "Major",
        "Szilágyi",
        "Fehér",
        "Balázs",
        "Gál",
        "Kis",
        "Szűcs",
        "Lakatos",
        "Katona",
        "Szalai",
        "Vörös",
        "Molnárné",
        "Rácz",
        "Székely",
        "Gulyás",
        "Sipos",
        "Kocsis",
        "Fodor",
        "Pintér",
        "Szabóné",
        "Bíró",
        "Magyar",
        "Boros",
        "Nemes",
        "Jakab",
        "Halász",
        "Vincze",
        "Király",
        "Kerekes",
        "Balog",
        "Antal",
        "Illés",
        "Török",
        "Deák",
        "Hegedűs"
    };

    private static readonly string[] RandomFirstNames =
    {
        "Pista",
        "Józsi",
        "Béla",
        "Géza",
        "Laci",
        "Feri",
        "Jani",
        "Gábor",
        "Zoli",
        "Pali",
        "Sanyi",
        "Barni",
        "Dani",
        "Tomi",
    };
}