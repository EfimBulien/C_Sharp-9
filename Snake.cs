using SnakeGame;
using System.Threading;
class Snake
{
    static bool isRunning = false;
    static int directionX = (int)Enums.DX;
    static int directionY = (int)Enums.DY;
    static Position food = new Position();
    static Random randomGen = new Random();
    static Queue<Position> snakeElements = new Queue<Position>();

    static void Main()
    {
        Console.WindowWidth = (int)Enums.Width;
        Console.WindowHeight = (int)Enums.Height;

        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;

        StartGame();

        while (isRunning != true)
        {
            if (Console.KeyAvailable)
            {
                Input(Console.ReadKey(true).Key);
            }

            MoveSnake();
            CheckCollision();
            CheckFood();
            DrawSnake();

            Thread.Sleep(100);
        }

        Console.Clear();
        Console.WriteLine("Вы проиграли :( ");
        Console.WriteLine($"Ваш счёт: {snakeElements.Count - 1}");
    }

    static void StartGame()
    {
        Position snakeHead = new Position((int)Enums.Width / 2, (int)Enums.Height / 2);
        snakeElements.Enqueue(snakeHead);

        GenerateFood();
    }

    static void DrawSnake()
    {
        Console.Clear();

        foreach (Position position in snakeElements)
        {
            try
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write("■");
            }
            catch (Exception)
            {
                
                return;
            }

        }

        Console.SetCursorPosition(food.X, food.Y);
        Console.Write("O");
    }

    static void MoveSnake()
    {
        Position currentHead = snakeElements.Last();
        Position newHead = new Position(currentHead.X + directionX, currentHead.Y + directionY);

        snakeElements.Enqueue(newHead);

        if (newHead.X == food.X && newHead.Y == food.Y)
        {
            GenerateFood();
        }
        else
        {
            snakeElements.Dequeue();
        }
    }

    static private void CheckCollision()
    {
        try
        {
            Position head = snakeElements.Last();

            if (head.X < 0 || head.X >= (int)Enums.Width || head.Y < 0 || head.Y >= (int)Enums.Height)
            {
                isRunning = true;
            }

            if (snakeElements.Count > 1 && snakeElements.Take(snakeElements.Count - 1).Any(p => p.X == head.X && p.Y == head.Y))
            {
                isRunning = true;
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    static void CheckFood()
    {
        Position head = snakeElements.Last();

        if (head.X == food.X && head.Y == food.Y)
        {
            GenerateFood();
        }
    }

    static void GenerateFood()
    {
        int x = randomGen.Next(0, (int)Enums.Width);
        int y = randomGen.Next(0, (int)Enums.Height);

        food = new Position(x, y);

        while (snakeElements.Any(p => p.X == x && p.Y == y))
        {
            x = randomGen.Next(0, (int)Enums.Width);
            y = randomGen.Next(0, (int)Enums.Height);
            food = new Position(x, y);
        }
    }

    static void Input(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (directionY == 0)
                {
                    directionX = 0;
                    directionY = -1;
                }
                break;

            case ConsoleKey.DownArrow:
                if (directionY == 0)
                {
                    directionX = 0;
                    directionY = 1;
                }
                break;

            case ConsoleKey.LeftArrow:
                if (directionX == 0)
                {
                    directionX = -1;
                    directionY = 0;
                }
                break;

            case ConsoleKey.RightArrow:
                if (directionX == 0)
                {
                    directionX = 1;
                    directionY = 0;
                }
                break;

            case ConsoleKey.Escape:
                isRunning = true;
                break;
        }
    }
}
