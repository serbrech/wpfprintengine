using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Xaml;
using SUT.PrintEngine.Utils;
using XamlWriter = System.Windows.Markup.XamlWriter;

namespace DEMOApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        
        #region print Visual / FrameworkElement
        private void PrintVisualClick(object sender, RoutedEventArgs e)
        {
            var visualSize = new Size(visual.ActualWidth, visual.ActualHeight);
            var printControl = PrintControlFactory.Create(visualSize, visual);
            printControl.ShowPrintPreview();
        }

        #endregion

        #region PrintDataTable
        private void PrintDataTableClick(object sender, RoutedEventArgs e)
        {
            var dataTable = CreateSampleDataTable();
            var columnWidths = new List<double>() {30, 40, 300, 300, 150};
            var ht = new HeaderTemplate();
            var headerTemplate = XamlWriter.Save(ht);
            var printControl = PrintControlFactory.Create(dataTable, columnWidths, headerTemplate);
            printControl.ShowPrintPreview();
        }

        private DataTable CreateSampleDataTable()
        {
            var dataTable = new DataTable();
            AddColumn(dataTable, "ID", typeof(int));
            AddColumn(dataTable, "Name", typeof(string));
            AddColumn(dataTable, "Birth Date", typeof(DateTime));
            AddColumn(dataTable, "Profession", typeof(string));
            AddColumn(dataTable, "Address", typeof(string));

            for (int i = 1; i < 300; i++)
            {
                AddRow(dataTable, i);
            }
            return dataTable;
        }

        private void AddRow(DataTable dataTable, int i)
        {
            var name = string.Format("saraf {0}", i);
            var dob = string.Format("{0}", DateTime.Now.AddDays(-(i * 18)));
            var profession = string.Format("Engineer {0}", i);
            var address = string.Format("8{0} Jack Clow Road, London", i);
            var dataRow = dataTable.NewRow();
            dataRow[0] = i;
            dataRow[1] = name;
            dataRow[2] = dob;
            dataRow[3] = profession;
            dataRow[4] = address;
            dataTable.Rows.Add(dataRow);
        }

        private void AddColumn(DataTable dataTable, string columnName, Type type)
        {
            var dataColumn = new DataColumn(columnName, type);
            dataTable.Columns.Add(dataColumn);
        }
        #endregion
    }
}
