using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Pages
{
    public partial class FirstPage : Page
    {
        private DataTable matrix1Data;
        private DataTable matrix2Data;
        private Random random = new Random();

        public FirstPage()
        {
            InitializeComponent();
            matrix1Data = new DataTable();
            matrix2Data = new DataTable();
            Matrix1Grid.ItemsSource = matrix1Data.DefaultView;
            Matrix2Grid.ItemsSource = matrix2Data.DefaultView;
        }

        private void DimensionsChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(RowsTextBox.Text, out int rows) || !int.TryParse(ColumnsTextBox.Text, out int columns))
                return;

            UpdateMatrixGrids(rows, columns);
        }

        private void UpdateMatrixGrids(int rows, int columns)
        {
            matrix1Data.Clear();
            matrix2Data.Clear();
            matrix1Data.Columns.Clear();
            matrix2Data.Columns.Clear();

            for (int i = 0; i < columns; i++)
            {
                matrix1Data.Columns.Add($"Col{i}", typeof(double));
                matrix2Data.Columns.Add($"Col{i}", typeof(double));
            }

            for (int i = 0; i < rows; i++)
            {
                matrix1Data.Rows.Add(matrix1Data.NewRow());
                matrix2Data.Rows.Add(matrix2Data.NewRow());
            }
        }

        private void RandomFill_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRow row in matrix1Data.Rows)
            {
                for (int i = 0; i < matrix1Data.Columns.Count; i++)
                {
                    row[i] = random.Next(-10, 11);
                }
            }

            foreach (DataRow row in matrix2Data.Rows)
            {
                for (int i = 0; i < matrix2Data.Columns.Count; i++)
                {
                    row[i] = random.Next(-10, 11);
                }
            }
        }

        private void OperationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle operation selection here
        }

        public double[,] GetMatrixFromDataTable(DataTable dt)
        {
            int rows = dt.Rows.Count;
            int cols = dt.Columns.Count;
            double[,] matrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (dt.Rows[i][j] != DBNull.Value)
                    {
                        matrix[i, j] = Convert.ToDouble(dt.Rows[i][j]);
                    }
                }
            }

            return matrix;
        }
    }
}
