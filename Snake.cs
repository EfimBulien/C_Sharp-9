using SnakeGame;
using System.Threading;
internal class Program
{
    private static void Main(string[] args)
    {
        bool isRunning = true;
        int directionX = (int)Enums.DX;
        int directionY = (int)Enums.DY;
        Position food = new Position();
        Random randomGen = new Random();
        Queue<Position> snakeElements = new Queue<Position>();

        Console.WindowWidth = (int)Enums.Width;
        Console.WindowHeight = (int)Enums.Height;

        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;

        StartGame();

        while (isRunning != false)
        {
            if (Console.KeyAvailable)
            {
                Input(Console.ReadKey(true).Key);
            }

            MoveSnake();
            CollisionSnake();
            EatFood();
            DrawSnake();

            Thread.Sleep(100);
        }

        Console.Clear();
        Console.WriteLine("Игра окончена :( ");
        Console.WriteLine($"Ваш счёт: {snakeElements.Count - 1}");

        void StartGame()
        {
            Position snakeHead = new Position((int)Enums.Width / 2, (int)Enums.Height / 2);
            snakeElements.Enqueue(snakeHead);

            SpawnFood();
        }

        void DrawSnake()
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
            Console.Write("■");
        }

        void MoveSnake()
        {
            Position currentHead = snakeElements.Last();
            Position newHead = new Position(currentHead.X + directionX, currentHead.Y + directionY);

            snakeElements.Enqueue(newHead);

            if (newHead.X == food.X && newHead.Y == food.Y)
            {
                SpawnFood();
            }
            else
            {
                snakeElements.Dequeue();
            }
        }

        void CollisionSnake()
        {
            try
            {
                Position head = snakeElements.Last();

                if (head.X < 0 || head.X >= (int)Enums.Width || head.Y < 0 || head.Y >= (int)Enums.Height)
                {
                    isRunning = false;
                }

                if (snakeElements.Count > 1 && snakeElements.Take(snakeElements.Count - 1).Any(p => p.X == head.X && p.Y == head.Y))
                {
                    isRunning = false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        void EatFood()
        {
            Position head = snakeElements.Last();

            if (head.X == food.X && head.Y == food.Y)
            {
                SpawnFood();
            }
        }

        void SpawnFood()
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

        void Input(ConsoleKey key)
        {
            switch (key)
            {
                case (ConsoleKey)Enums.Up:
                    if (directionY == 0)
                    {
                        directionX = 0;
                        directionY = -1;
                    }
                    break;

                case (ConsoleKey)Enums.Down:
                    if (directionY == 0)
                    {
                        directionX = 0;
                        directionY = 1;
                    }
                    break;

                case (ConsoleKey)Enums.Left:
                    if (directionX == 0)
                    {
                        directionX = -1;
                        directionY = 0;
                    }
                    break;

                case (ConsoleKey)Enums.Right:
                    if (directionX == 0)
                    {
                        directionX = 1;
                        directionY = 0;
                    }
                    break;

                case (ConsoleKey)Enums.Escape:
                    isRunning = false;
                    break;
            }
        }
    }
}
