using System;

namespace Tetris
{
    public class Tetris
    {
        private static int keyDelay = 50; // Delay between keypress handling (in ms)
        private static DateTime lastKeyPressTime = DateTime.Now;

        public static void Main(string[] args)
        {
            Matrix matrix = new Matrix();
            Shape shape = new Shape(matrix);
            int previousScore = 0;

            while (true)
            {
                matrix.Print();
                shape.Draw();

                HandleInput(shape);

                if (!shape.MoveDown())
                {
                    shape = new Shape(matrix);
                    matrix.CheckForCompleteLines();
                    if (matrix.IsGameOver() == true)
                    {
                        Console.WriteLine("Game Over!");
                        break;
                    }
                }

                int score = matrix.GetScore();
                if (score >= previousScore + 800)
                {
                    Speed.SpeedUp();
                    previousScore = matrix.GetScore();
                }

                System.Threading.Thread.Sleep(Speed.ShowSpeed());
            }
        }

        // TODO: Improve input handling
        private static void HandleInput(Shape shape)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.LeftArrow)
                        shape.MoveLeft();
                    else if (key.Key == ConsoleKey.RightArrow)
                        shape.MoveRight();
                    else if (key.Key == ConsoleKey.UpArrow)
                        shape.Rotate();
                    else if (key.Key == ConsoleKey.DownArrow)
                        shape.MoveDown();
                }
            }
        }
    }

    public class Speed()
    {
        private static int speed = 400;

        public static int ShowSpeed()
        {
            return speed;
        }

        public static void SpeedUp()
        {
            speed -= 50;
        }
    }


    public class Shape
    {
        private int[,] current_shape;
        private int randomRotation;
        private int randomShape;

        private static List<List<int[,]>> shapes = ShapesData.shapes;
        private static List<int[,]> shape;

        private int x, y;
        private Matrix matrix;
        public Shape(Matrix matrix)
        {
            Random random = new Random();
            randomShape = random.Next(0, shapes.Count);
            shape = shapes[randomShape];
            randomRotation = random.Next(0, shape.Count);
            current_shape = shape[randomRotation];
            this.matrix = matrix;
            matrix.IncreaseScore(10);
            // TODO: Implement random color
            this.x = 0; // Starting position
            this.y = 8; // Center of the grid
        }

        public void Draw()
        {
            for (int i = 0; i < current_shape.GetLength(0); i++)
            {
                matrix.SetBlock(x + current_shape[i, 0], y + current_shape[i, 1], '█');
            }
        }

        public bool MoveDown()
        {
            if (CanMove(x + 1, y))
            {
                Clear();
                x++;
                Draw();
                return true;
            }
            return false;
        }

        public void MoveLeft()
        {
            if (CanMove(x, y - 1))
            {
                Clear();
                y--;
                y--;
                Draw();
            }
        }

        public void MoveRight()
        {
            if (CanMove(x, y + 1))
            {
                Clear();
                y++;
                y++;
                Draw();
            }
        }

        public void Rotate()
        {
            Clear();
            randomRotation = (randomRotation + 1) % shape.Count;
            // !IMPORTANT: Check if rotation is possible
            current_shape = shape[randomRotation];
            Draw();
        }

        private bool IsPartOfShape(int matrixX, int matrixY)
        {
            for (int i = 0; i < current_shape.GetLength(0); i++)
            {
                if (x + current_shape[i, 0] == matrixX && y + current_shape[i, 1] == matrixY)
                {
                    return true;
                }
            }
            return false;
        }


        private bool CanMove(int newX, int newY)
        {
            for (int i = 0; i < current_shape.GetLength(0); i++)
            {
                int newBlockX = newX + current_shape[i, 0];
                int newBlockY = newY + current_shape[i, 1];

                if (newBlockX >= Matrix.HEIGHT || newBlockX < 0 || newBlockY < 0 || newBlockY >= Matrix.WIDTH)
                {
                    return false;
                }
                if (matrix.GetBlock(newBlockX, newBlockY) != ' ' && !IsPartOfShape(newBlockX, newBlockY))
                {
                    return false;
                }
            }
            return true;
        }

        private void Clear()
        {
            for (int i = 0; i < current_shape.GetLength(0); i++)
            {
                matrix.ClearBlock(x + current_shape[i, 0], y + current_shape[i, 1]);
            }
        }
    }

    public class Matrix
    {
        public const int HEIGHT = 22;
        public const int WIDTH = 20;
        private char[,] matrix;
        private int score = 0;

        public Matrix()
        {
            matrix = new char[HEIGHT, WIDTH];
            Clear();
        }

        public void SetBlock(int x, int y, char c)
        {
            if (IsInBounds(x, y))
            {
                matrix[x, y] = c;
            }
        }

        public void ClearBlock(int x, int y)
        {
            if (IsInBounds(x, y))
            {
                matrix[x, y] = ' ';
            }
        }

        public char GetBlock(int x, int y)
        {
            return IsInBounds(x, y) ? matrix[x, y] : 'E';
        }

        public void Print()
        {
            Console.Clear();
            for (int i = 0; i < HEIGHT; i++)
            {
                Console.Write("│ ");
                for (int j = 0; j < WIDTH; j++)
                {
                    Console.ForegroundColor = matrix[i, j] == ' ' ? ConsoleColor.DarkBlue : ConsoleColor.DarkBlue;
                    Console.Write(matrix[i, j]);
                }
                Console.ResetColor();
                Console.WriteLine(" │");
            }
            Console.WriteLine(" ──────────────────────");

            Console.WriteLine("Score: " + score);
        }

        public int GetScore()
        {
            return score;
        }
        public void IncreaseScore(int score)
        {
            this.score += score;
        }

        public void CheckForCompleteLines()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                bool isComplete = true;
                for (int j = 0; j < WIDTH; j++)
                {
                    if (matrix[i, j] == ' ')
                    {
                        isComplete = false;
                        break;
                    }
                }
                if (isComplete)
                {
                    // Remove line
                    IncreaseScore(100);
                    for (int j = 0; j < WIDTH; j++)
                    {
                        matrix[i, j] = ' ';
                    }
                    // Move all lines above one down
                    for (int k = i; k > 0; k--)
                    {
                        for (int j = 0; j < WIDTH; j++)
                        {
                            matrix[k, j] = matrix[k - 1, j];
                        }
                    }
                }
            }
        }

        public bool IsGameOver()
        {
            for (int i = 0; i < WIDTH; i++)
            {
                if (matrix[0, i] != ' ')
                {
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    matrix[i, j] = ' ';
                }
            }
        }

        private bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < HEIGHT && y >= 0 && y < WIDTH;
        }
    }
}
