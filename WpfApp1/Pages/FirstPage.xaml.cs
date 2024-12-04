using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WpfApp1.Pages
{
    public partial class FirstPage : Page
    {
        private DataTable matrix1Data;
        private DataTable matrix2Data;
        private DataTable matrixOutputData;
        private Random random = new Random();

        public FirstPage()
        {
            OperandTextBox = new TextBox();
            Matrix2Grid = new DataGrid();
            InitializeComponent();
            matrix1Data = new DataTable();
            matrix2Data = new DataTable();
            matrixOutputData = new DataTable();
            Matrix1Grid.ItemsSource = matrix1Data.DefaultView;
            Matrix2Grid.ItemsSource = matrix2Data.DefaultView;
            MatrixOutputGrid.ItemsSource = matrixOutputData.DefaultView;
        }

        private void DimensionsChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(RowsTextBox.Text, out int rows) || !int.TryParse(ColumnsTextBox.Text, out int columns))
                return;

            Matrix1Grid.ItemsSource = null;
            Matrix2Grid.ItemsSource = null;
            MatrixOutputGrid.ItemsSource = null;

            UpdateMatrixGrids(rows, columns);

            Matrix1Grid.ItemsSource = matrix1Data.DefaultView;
            Matrix2Grid.ItemsSource = matrix2Data.DefaultView;
            MatrixOutputGrid.ItemsSource = matrixOutputData.DefaultView;
        }

        private void UpdateMatrixGrids(int rows, int columns)
        {
            UpdateMatrixGrid(matrix1Data, rows, columns);
            UpdateMatrixGrid(matrix2Data, rows, columns);
            UpdateMatrixGrid(matrixOutputData, rows, columns);
        }

        private void UpdateMatrixGrid(DataTable dt,int rows, int columns)
        {
            dt.Clear();
            dt.Columns.Clear();

            for (int i = 0; i < columns; i++)
            {
                dt.Columns.Add($"Col{i}", typeof(double));
            }

            for (int i = 0; i < rows; i++)
            {
                dt.Rows.Add(dt.NewRow());
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
        private void SolveButtonClick(object sender, RoutedEventArgs e)
        {
            MyMath.Matrix result;
            var matr1 = GetMatrixFromDataTable(matrix1Data);
            if (OperationComboBox.SelectedIndex == 0)
            {
                var matr2 = GetMatrixFromDataTable(matrix2Data);
                result = matr1 + matr2;
            }
            else
            {
                result = matr1 * Convert.ToDouble(OperandTextBox.Text);
            }
            FillDataTableFromMatrix(result, matrixOutputData);
        }

        private void OperationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OperationComboBox.SelectedIndex == 0)
            {
                Matrix2Grid.Visibility = Visibility.Visible;
                OperandTextBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                Matrix2Grid.Visibility = Visibility.Collapsed;
                OperandTextBox.Visibility = Visibility.Visible;
            }
        }

        public MyMath.Matrix GetMatrixFromDataTable(DataTable dt)
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
            return new MyMath.Matrix(matrix);
        }

        private void FillDataTableFromMatrix(MyMath.Matrix matrix, DataTable dt)
        {
            dt.Clear();
            dt.Columns.Clear();

            for (int i = 0; i < matrix.Columns; i++)
            {
                dt.Columns.Add($"Col{i}", typeof(double));
            }

            for (int i = 0; i < matrix.Rows; i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < matrix.Columns; j++)
                {
                    row[j] = matrix[i, j];
                }
                dt.Rows.Add(row);
            }
        }
    }
}
