using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using MyMath;

namespace WpfApp1.Pages
{
    public partial class ThirdPage : Page
    {
        private const int MatrixSize = 5;
        private DataTable originalData;
        private Random random = new Random();
        private Matrix currentMatrix;

        public ThirdPage()
        {
            InitializeComponent();
            InitializeMatrixData();
        }

        private void InitializeMatrixData()
        {
            originalData = new DataTable();
            for (int i = 0; i < MatrixSize; i++)
            {
                originalData.Columns.Add($"Col{i}", typeof(double));
            }
            for (int i = 0; i < MatrixSize; i++)
            {
                originalData.Rows.Add(originalData.NewRow());
            }
            OriginalMatrixGrid.ItemsSource = originalData.DefaultView;
        }

        private void GenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRow row in originalData.Rows)
            {
                for (int i = 0; i < MatrixSize; i++)
                {
                    row[i] = random.Next(-50, 51);
                }
            }
            currentMatrix = GetMatrixFromDataTable(originalData);
            ClearTransformedMatrices();
        }

        private void ToTriangular_Click(object sender, RoutedEventArgs e)
        {
            currentMatrix = GetMatrixFromDataTable(originalData);
            Matrix triangular = currentMatrix.ToTriangular();
            DisplayMatrix(triangular, TriangularMatrixGrid);
        }

        private void ToDiagonal_Click(object sender, RoutedEventArgs e)
        {
            currentMatrix = GetMatrixFromDataTable(originalData);
            Matrix diagonal = currentMatrix.ToDiagonalGauss();
            DisplayMatrix(diagonal, DiagonalMatrixGrid);
        }

        private Matrix GetMatrixFromDataTable(DataTable dt)
        {
            double[,] data = new double[MatrixSize, MatrixSize];
            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    if (dt.Rows[i][j] == DBNull.Value) continue;
                    data[i, j] = Convert.ToDouble(dt.Rows[i][j]);
                }
            }
            return new Matrix(data);
        }

        private void DisplayMatrix(Matrix matrix, DataGrid grid)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < MatrixSize; i++)
            {
                dt.Columns.Add($"Col{i}", typeof(double));
            }

            for (int i = 0; i < MatrixSize; i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < MatrixSize; j++)
                {
                    row[j] = Math.Round(matrix[i, j], 4);
                }
                dt.Rows.Add(row);
            }

            grid.ItemsSource = dt.DefaultView;
        }

        private void ClearTransformedMatrices()
        {
            TriangularMatrixGrid.ItemsSource = null;
            DiagonalMatrixGrid.ItemsSource = null;
        }
    }
}
