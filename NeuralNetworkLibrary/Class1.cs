using System;
using System.Collections.Generic;

namespace NeuralNetworkLibrary
{

    public class Matrix
    {
        public int rows;
        public int columns;
        private List<List<float>> matrix = new List<List<float>>();

        public List<float> this[int index]
        {
            get
            {
                return matrix[index];
            }
            set
            {
                matrix[index] = value;
            }
        }

        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            for (var i = 0; i < rows; i++)
            {
                matrix.Add(new List<float>());
                for (var j = 0; j < columns; j++)
                {
                    matrix[i].Add(0);
                }
            }
        }
        public void RandomiseValues()
        {
            Random r = new Random();
            for (var i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this[i][j] = (float)Math.Floor(r.NextDouble() * 10);
                }
            }
        }
        public void Transpose()
        {
            Matrix m = new Matrix(columns, rows);
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    m[j][i] = this[i][j];
                }
            }
            rows = m.rows;
            columns = m.columns;
            matrix = m.matrix;
        }
        public Matrix Transposed()
        {
            Matrix m = new Matrix(columns, rows);
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    m[j][i] = this[i][j];
                }
            }
            return m;
        }

        public override string ToString()
        {
            string s = "Matrix [\n\t";
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    s += this[i][j].ToString() + "\t";
                }
                s = s.Substring(0, s.Length - 1);
                s += "\n\t";
            }
            s = s.Substring(0, s.Length - 1) + "]";
            return s;
        }


        public static Matrix operator *(Matrix m, float scalar)
        {
            for (var i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.columns; j++)
                {
                    m[i][j] *= scalar;
                }
            }

            return m;
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.columns == m2.rows)
            {
                Matrix m = new Matrix(m1.rows, m2.columns);
                for (var i = 0; i < m.rows; i++)
                {
                    for (var j = 0; j < m.columns; j++)
                    {
                        var sum = 0f;
                        for (var k = 0; k < m1.columns; k++)
                        {
                            sum += m1[i][k] * m2[k][j];
                        }
                        m[i][j] = sum;
                    }
                }

                return m;
            }
            throw new DimensionException("The columns of the first matrix must match the rows of the second to multiply them.");
        }
        public static Matrix operator +(Matrix m, float scalar)
        {
            for (var i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.columns; j++)
                {
                    m[i][j] += scalar;
                }
            }

            return m;
        }
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.rows == m2.rows && m1.columns == m2.columns)
            {
                for (var i = 0; i < m1.rows; i++)
                {
                    for (int j = 0; j < m1.columns; j++)
                    {
                        m1[i][j] += m2[i][j];
                    }
                }

                return m1;
            }
            throw new DimensionException("The matricies must have the same dimensions to add");
        }



    }

    public class DimensionException : Exception
    {
        public new string Message;
        public DimensionException(string m)
        {
            Message = m;
        }
    }
}
