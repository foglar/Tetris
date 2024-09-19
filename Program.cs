using System;

// TODO: Create methods to clear and set blocks in the matrix, to replace method SetBlock and Clear in Matrix class

namespace Tetris
{
    public class Tetris
    {

        private static int keyDelay = 300; // Delay between keypress handling (in ms)
        private static DateTime lastKeyPressTime = DateTime.Now;
        public static void Main(string[] args)
        {
            Matrix matrix = new Matrix();
            Block blocks = new Block(matrix);
            blocks.DrawBlock();
            while (true)
            {
                //matrix.Clear(); 
                matrix.Print();

                for (int i = 0; i < 5; i++)
                {
                    HandleInput(blocks);
                }

                if (!blocks.MoveDown())
                {
                    blocks = new Block(matrix);
                    blocks.DrawBlock();
                }
                System.Threading.Thread.Sleep(400);
            }
        }
        private static void HandleInput(Block blocks)
        {
            if (Console.KeyAvailable)
            {
                // Read key input without blocking
                ConsoleKeyInfo key = Console.ReadKey(true);

                // Debounce keypress handling to prevent spamming
                if ((DateTime.Now - lastKeyPressTime).TotalMilliseconds > keyDelay)
                {
                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        blocks.MoveLeft();
                    }
                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        blocks.MoveRight();
                    }

                    // Update the time of the last key press
                    lastKeyPressTime = DateTime.Now;
                }
            }
        }
    }

    //int[,] box_shape = new int[,] {
    //    {0, 0},
    //    {0, 1},
    //    {1, 0},
    //    {1, 1},
    //};
//
    //public class Shape {
    //    // Object containing the shape of the block
    //    // The shape is a 2D array of characters
//
    //    public Shape(char[,] shape)
    //    {
    //        this.shape = shape;
    //    }
    //}

    public class Block
    {
        private int x;
        private int y;
        private Matrix matrix;

        public Block(Matrix matrix)
        {
            this.x = 0 % Matrix.HEIGHT;
            this.y = 6 % Matrix.WIDTH; 
            this.matrix = matrix;
        }

        public void MoveLeft()
        {
            if (y > 0)
            {
                y--;
                y--;
                matrix.SetBlock(x, y, '█');
                matrix.SetBlock(x, y+1, '█');
                matrix.SetBlock(x, y+2, ' ');
                matrix.SetBlock(x, y+3, ' ');
                
            }
        }

        public void MoveRight()
        {
            if (y < Matrix.WIDTH - 2)
            {
                y++;
                y++;
                matrix.SetBlock(x, y,'█');
                matrix.SetBlock(x, y+1, '█');
                matrix.SetBlock(x, y-1, ' ');
                matrix.SetBlock(x, y-2, ' ');
            }
        }

        public bool MoveDown()
        {
            if (matrix.GetBlock(x+1, y) == '█')
            {
                return false;
            }
            else if (x < Matrix.HEIGHT - 1)
            {
                x++;
                matrix.SetBlock(x, y, '█');
                matrix.SetBlock(x, y + 1, '█');
                matrix.SetBlock(x - 1, y, ' ');
                matrix.SetBlock(x - 1, y + 1, ' ');
                return true;
            }
            else
            {
                x = 0;
                return false;
            }
        }

        public void DrawBlock()
        {
            if (x < Matrix.HEIGHT && y < Matrix.WIDTH)
            {
                matrix.SetBlock(x, y, '█');
                matrix.SetBlock(x, y + 1, '█');
            }
        }
    }

    public class Matrix
    {
        public const int HEIGHT = 22; 
        public const int WIDTH = 20;  

        private char[,] matrix;

        public Matrix()
        {
            matrix = new char[HEIGHT, WIDTH];

            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    matrix[i, j] = ' ';
                }
            }
        }

        public void SetBlock(int x, int y, char c)
        {
            if (x >= 0 && x < HEIGHT && y >= 0 && y < WIDTH)
            {
                matrix[x, y] = c;
            }
        }

        public char GetBlock(int x, int y)
        {
            if (x >= 0 && x < HEIGHT && y >= 0 && y < WIDTH)
            {
                return matrix[x, y];
            }
            return 'E';
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

        public void Print()
        {
            Console.Clear();

            for (int i = 0; i < HEIGHT; i++)
            {
                Console.Write("│ ");
                for (int j = 0; j < WIDTH; j++)
                {
                    if (matrix[i, j] == ' ')
                    {
                        //Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    Console.Write(matrix[i, j]);
                    Console.ResetColor();
                }
                Console.WriteLine(" │");
            }
            Console.WriteLine(" ──────────────────────");
        }
    }
}
