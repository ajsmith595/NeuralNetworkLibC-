using System;
using System.Collections.Generic;

namespace NeuralNetworkLibrary
{
    public class Matrix
    {
        public int rows;
        public int columns;
        private List<List<double>> data = new List<List<double>>();

        public List<double> this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index] = value;
            }
        }

        public Matrix GetDuplicate()
        {
            Matrix m = new Matrix(rows, columns);
            m.data = new List<List<double>>();
            for (var i = 0; i < data.Count; i++)
            {
                List<double> current = new List<double>();
                for (var j = 0; j < data[i].Count; j++)
                {
                    current.Add(data[i][j]);
                }
                m.data.Add(current);
            }
            return m;
        }
        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            for (var i = 0; i < rows; i++)
            {
                data.Add(new List<double>());
                for (var j = 0; j < columns; j++)
                {
                    data[i].Add(0);
                }
            }
        }
        public Matrix(double[] vals)
        {
            this.rows = vals.Length;
            this.columns = 1;
            for (var i = 0; i < vals.Length; i++)
            {
                List<double> val = new List<double>() { vals[i] };
                this.data.Add(val);
            }
        }
        public void RandomiseValues()
        {
            Random r = new Random();
            for (var i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this[i][j] = (double)r.NextDouble() * 2f - 1f;
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
            data = m.data;
        }
        public void ApplyFunction(Func<double, double> function)
        {
            for (var i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var val = this[i][j];
                    this[i][j] = function(val);
                }
            }
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
        public Matrix FunctionApplied(Func<double, double> function)
        {
            Matrix m = new Matrix(rows, columns);
            for (var i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var val = this[i][j];
                    m[i][j] = function(val);
                }
            }
            return m;
        }
        public List<double> ToList()
        {
            List<double> vals = new List<double>();
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    vals.Add(this[i][j]);
                }
            }
            return vals;
        }
        public override string ToString()
        {
            string s = "Matrix(" + rows + " X " + columns + ") [\n\t";
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




        public static Matrix operator *(Matrix m, double scalar)
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
                        double sum = 0;
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
        public static Matrix operator %(Matrix m1, Matrix m2) // Hadamard Product!
        {
            if (m1.rows == m2.rows && m1.columns == m2.columns)
            {
                Matrix m = new Matrix(m1.rows, m2.columns);
                for (var i = 0; i < m.rows; i++)
                {
                    for (var j = 0; j < m.columns; j++)
                    {
                        m[i][j] = m1[i][j] * m2[i][j];
                    }
                }

                return m;
            }
            throw new DimensionException("The matricies must have the same dimensions to perform a hadamard product");
        }
        public static Matrix operator +(Matrix m, double scalar)
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
            Matrix m1_ = m1.GetDuplicate();
            if (m1_.rows == m2.rows && m1_.columns == m2.columns)
            {
                for (var i = 0; i < m1_.rows; i++)
                {
                    for (int j = 0; j < m1_.columns; j++)
                    {
                        m1_[i][j] += m2[i][j];
                    }
                }
                return m1_;
            }
            throw new DimensionException("The matricies must have the same dimensions to add");
        }
        public static Matrix operator -(Matrix m, double scalar)
        {
            for (var i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.columns; j++)
                {
                    m[i][j] -= scalar;
                }
            }

            return m;
        }
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            Matrix m1_ = m1.GetDuplicate();
            if (m1_.rows == m2.rows && m1_.columns == m2.columns)
            {
                for (var i = 0; i < m1_.rows; i++)
                {
                    for (int j = 0; j < m1_.columns; j++)
                    {
                        m1_[i][j] -= m2[i][j];
                    }
                }
                return m1_;
            }
            throw new DimensionException("The matricies must have the same dimensions to subtract");
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


}
