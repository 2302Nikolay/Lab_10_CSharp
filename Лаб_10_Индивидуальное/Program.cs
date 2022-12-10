using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Lab_10_und
{
    public class Matrix
    {
        private float[,] matrix;
        private int m, n;

        // Конструктор 
        public Matrix(int m, int n)
        {
            this.m = m;
            this.n = n;
            matrix = new float[m, n];
        }



        // Генератор матриц заданного размера 
        public void GeneratMatr(int M, int N)
        {
            m = M; n = N;

            Random r = new Random(DateTime.Now.Millisecond);

            matrix = new float[M, N];

            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                    matrix[i, j] = (float)r.Next(100);
        }

        // Сохранение сгенерированной матрицы из файла
        public void SaveMatr(string pFileName)
        {
            if (matrix.Length > 0)
            {
                if (File.Exists(pFileName))
                    File.Delete(pFileName);

                FileInfo f = new FileInfo(pFileName);
                TextWriter tw = f.CreateText();

                tw.WriteLine(m.ToString());
                tw.WriteLine(n.ToString());

                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tw.WriteLine(i.ToString() + " " + j.ToString() + " " + matrix[i, j].ToString());
                tw.Close();
            }
        }

        // Загрузка сохранённой матрицы из файла
        public Boolean LoadMatr(string pFileName)
        {
            if (File.Exists(pFileName))
            {
                try
                {
                    TextReader tr = File.OpenText(pFileName);
                    m = Convert.ToInt32(tr.ReadLine());
                    n = Convert.ToInt32(tr.ReadLine());

                    matrix = new float[m, n];
                    string line;
                    string[] substring;

                    for (int i = 0; i < m; i++)
                        for (int j = 0; j < n; j++)
                        {
                            line = tr.ReadLine();
                            substring = line.Split(new char[] { ' ' }, 3);
                            matrix[i, j] = Convert.ToSingle(substring[2]);
                        }
                    tr.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        // Вывод маттрицы на консоль
        public void PrintMatr()
        {
            Console.WriteLine("\n");
            if (matrix.Length > 0)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(matrix[i, j].ToString() + " ");
                    }
                    Console.WriteLine();
                }
            }
        }

        // Метод сложения элементов двух матриц в чётных столбцах и нечётных строках
        public static float SumElement(Matrix pMatr_1, Matrix pMatr_2)
        {
            float sum = 0;

            for (int i = 1; i < pMatr_1.m; i=i+2)
            {
                for (int j = 0; j < pMatr_1.n; j=j+2)
                {
                    sum += pMatr_1.matrix[i, j] + pMatr_2.matrix[i, j];
                }
            }

            return sum;
        }

        // Перегрузка оператора, не нужна в этой работе, сделал по приколу 😎
        public static Matrix operator +(Matrix pMatr_1, Matrix pMatr_2)
        {
            Matrix tempMatr = new Matrix(pMatr_1.m, pMatr_1.n);
            for (int i = 0; i < pMatr_1.m; i++)
            {
                for (int j = 0; j < pMatr_1.n; j++)
                {
                    tempMatr.matrix[i, j] = pMatr_1.matrix[i, j] + pMatr_2.matrix[i, j];
                }
            }
            return tempMatr;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int m, n;
            // Ввод размера матрицы
            Console.Write("Введите m >> ");
            m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите n >> ");
            n = Convert.ToInt32(Console.ReadLine());

            Matrix first = new Matrix(m, n);
            Matrix second = new Matrix(m, n);
            // Matrix third = new Matrix(m, n); // Для проверки работы оператора

            first.GeneratMatr(m, n);
            first.SaveMatr("First_matrix.txt");

            second.GeneratMatr(m, n);
            second.SaveMatr("Second_matrix.txt");

            if (first.LoadMatr("First_matrix.txt") & second.LoadMatr("Second_matrix.txt"))
            {
                first.PrintMatr();
                second.PrintMatr();
                // third = first + second; // Для проверки работы оператора
                Console.WriteLine("\n" + Matrix.SumElement(first, second)); 
                // third.SaveMatr("Rezult_matrix.txt"); // Для проверки работы оператора
                // third.PrintMatr(); // Для проверки работы оператора
            }

            Console.ReadKey();
        }
    }
}