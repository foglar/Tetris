using System;

namespace Tetris
{
    public class Tetris
    {
        private static int keyDelay = 300; // Delay between keypress handling (in ms)
        private static DateTime lastKeyPressTime = DateTime.Now;

        public static void Main(string[] args)
        {
            Matrix matrix = new Matrix();
            Shape shape = new Shape(matrix);
            matrix.IncreaseScore(10);
            while (true)
            {
                matrix.Print();
                shape.Draw();

                HandleInput(shape);

                if (!shape.MoveDown())
                {
                    matrix.IncreaseScore(10);
                    shape = new Shape(matrix); // Create a new shape if current one can't move down
                    matrix.CheckForCompleteLines();
                    // Check for game over
                                }

                System.Threading.Thread.Sleep(400);
            }
        }

        private static void HandleInput(Shape shape)
        {
            if (Console.KeyAvailable && (DateTime.Now - lastKeyPressTime).TotalMilliseconds > keyDelay)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.LeftArrow)
                    shape.MoveLeft();
                else if (key.Key == ConsoleKey.RightArrow)
                    shape.MoveRight();

                lastKeyPressTime = DateTime.Now;
            }
        }
    }

    public class Shape
    {
        private int[,] shape;
        private int x, y;
        private Matrix matrix;
        private static readonly int[][,] shapes = new int[][,] {

            new int[,]{
                {0, 0}, {0, 1}, {0, 2}, {0, 3}, {1, 0}, {1, 1}, {1, 2}, {1, 3},
            },
            new int[,] {
                {0, 0}, {0, 1}, {0, 2}, {0, 3}, {0, -1}, {0, -2}, {0, 4}, {0, 5},
            },
            new int[,] {
                {1,-2}, {1,-1}, {1,0}, {1,1}, {1, 2}, {1, 3}, {0, 0}, {0, 1},
            },
            new int[,] {
                {1, -2}, {1, -1}, {1, 0}, {1, 1}, {1, 2}, {1, 3}, {0, 2}, {0, 3},
            }
        };

        public Shape(Matrix matrix)
        {
            Random random = new Random();
            this.shape = shapes[random.Next(0, shapes.GetLength(0))];
            this.matrix = matrix;
            // TODO: Implement random color
            this.x = 0; // Starting position
            this.y = 8; // Center of the grid
        }

        public void Draw()
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                matrix.SetBlock(x + shape[i, 0], y + shape[i, 1], '█');
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

        // TODO: Implement Rotate method
        // public void Rotate()

        private bool IsPartOfShape(int matrixX, int matrixY)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                if (x + shape[i, 0] == matrixX && y + shape[i, 1] == matrixY)
                {
                    return true;
                }
            }
            return false;
        }


        private bool CanMove(int newX, int newY)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                int newBlockX = newX + shape[i, 0];
                int newBlockY = newY + shape[i, 1];

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
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                matrix.ClearBlock(x + shape[i, 0], y + shape[i, 1]);
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
        
        public void IncreaseScore(int score)
        {
            this.score += score;
        }

        public void CheckForCompleteLines() {
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
