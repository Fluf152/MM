using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using MyMath;

namespace WpfApp1.Pages
{
    public partial class SecondPage : Page
    {
        private DataTable matrixData;
        private Random random = new Random();
        private const int MatrixSize = 3;

        public SecondPage()
        {
            InitializeComponent();
            InitializeMatrix();
        }

        private void InitializeMatrix()
        {
            matrixData = new DataTable();
            for (int i = 0; i < MatrixSize; i++)
            {
                matrixData.Columns.Add($"Col{i}", typeof(double));
            }
            for (int i = 0; i < MatrixSize; i++)
            {
                matrixData.Rows.Add(matrixData.NewRow());
            }
            MainMatrixGrid.ItemsSource = matrixData.DefaultView;
        }

        private void GenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRow row in matrixData.Rows)
            {
                for (int i = 0; i < MatrixSize; i++)
                {
                    row[i] = random.Next(-10, 11);
                }
            }
        }

        private void CalculateDeterminant_Click(object sender, RoutedEventArgs e)
        {
            Matrix matrix = GetCurrentMatrix();
            double det = matrix.Determinant();
            DeterminantText.Text = det.ToString("F2");
        }

        private void ShowMinors_Click(object sender, RoutedEventArgs e)
        {
            MinorsPanel.Items.Clear();
            Matrix matrix = GetCurrentMatrix();

            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    DataGrid minorGrid = CreateMinorGrid(matrix, j, i);
                    MinorsPanel.Items.Add(minorGrid);
                }
            }
        }

        private void CalculateCofactors_Click(object sender, RoutedEventArgs e)
        {
            MinorsPanel.Items.Clear();
            Matrix matrix = GetCurrentMatrix();

            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    var a = CalculateMinor(matrix, i, j);
                    var b = Math.Pow(-1, i + j + 2);
                    double cofactor = Math.Pow(-1, i + j+2) * CalculateMinor(matrix, i, j);
                    TextBlock cofactorBlock = new TextBlock
                    {
                        Text = $"C{i + 1}{j + 1} = {cofactor:F2}",
                        Margin = new Thickness(5)
                    };
                    MinorsPanel.Items.Add(cofactorBlock);
                }
            }
        }

        private Matrix GetCurrentMatrix()
        {
            double[,] data = new double[MatrixSize, MatrixSize];
            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    if (matrixData.Rows[i][j] == DBNull.Value) continue;
                    data[i, j] = Convert.ToDouble(matrixData.Rows[i][j]);
                }
            }
            return new Matrix(data);
        }

        private DataGrid CreateMinorGrid(Matrix matrix, int row, int col)
        {
            DataTable minorData = new DataTable();
            for (int i = 0; i < MatrixSize - 1; i++)
            {
                minorData.Columns.Add($"Col{i}", typeof(double));
            }

            int r = 0;
            for (int i = 0; i < MatrixSize; i++)
            {
                if (i == row) continue;
                DataRow newRow = minorData.NewRow();
                int c = 0;
                for (int j = 0; j < MatrixSize; j++)
                {
                    if (j == col) continue;
                    newRow[c++] = matrix[i, j];
                }
                minorData.Rows.Add(newRow);
                r++;
            }

            DataGrid grid = new DataGrid
            {
                ItemsSource = minorData.DefaultView,
                AutoGenerateColumns = true,
                CanUserAddRows = false,
                Margin = new Thickness(5),
                HeadersVisibility = DataGridHeadersVisibility.Row
            };

            return grid;
        }

        private double CalculateMinor(Matrix matrix, int row, int col)
        {
            Matrix minorMatrix = new Matrix(MatrixSize - 1, MatrixSize - 1);
            int r = 0;
            for (int i = 0; i < MatrixSize; i++)
            {
                if (i == row) continue;
                int c = 0;
                for (int j = 0; j < MatrixSize; j++)
                {
                    if (j == col) continue;
                    minorMatrix[r, c] = matrix[i, j];
                    c++;
                }
                r++;
            }
            return minorMatrix.Determinant();
        }
    }
}
