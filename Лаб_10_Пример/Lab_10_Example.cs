using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лабораторная_10
{
    public class Matrix
    {
        private float[,] matrix;
        int m, n;

        // Конструктор
        public Matrix() { }

        // Генерация матрицы заданного размера
        public void GenerateMatrix(int M, int N)
        {
            m = M; n = N;

            Random r = new Random(DateTime.Now.Millisecond);

            matrix = new float[M, N];

            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                    matrix[i, j] = (float)r.Next(1000) / 973f;
        }

        // Сохранение сгенерированной матрицы в файл
        public void SaveMatrix(string pFileName)
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
                        tw.WriteLine(i.ToString() + " " + j.ToString() + " " + matrix[i, j].ToString("E10"));
                tw.Close();
            }
        }

        // Загрузка сохранённой матрицы из файла
        public Boolean LoadMatrix(string pFileName)
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

        public void PrintMatrix()
        {
            if (matrix.Length > 0)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                        Console.Write(matrix[i, j].ToString("E3") + "   ");

                    Console.WriteLine();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Matrix m = new Matrix();

            m.GenerateMatrix(10, 5);
            m.SaveMatrix("Matrix.txt");

            if (m.LoadMatrix("Matrix.txt"))
            {
                m.PrintMatrix();
            }

            Console.ReadKey();
        }
    }
}
