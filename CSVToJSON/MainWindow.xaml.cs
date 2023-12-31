﻿using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace CSVToJSON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv"
            };
            if(openFileDialog.ShowDialog() == true)
                ReadCsv(openFileDialog.FileName);
        }

        private void ReadCsv(string fileName)
        {
            try
            {
                string csvInput = File.ReadAllText(fileName);
                CsvTextBox.Text += csvInput;
                CsvData data = CsvReader.ParseCsvWithHeader(csvInput);
                JsonTextBox.Text = data.ToJson();
            }
            catch (Exception e)
            {
                CsvTextBox.Text = $"Error: {e}";
            }
        }

    }
}