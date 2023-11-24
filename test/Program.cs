using DocumentFormat.OpenXml.Wordprocessing;
using ikem23_wapi.Models;
using ikem23_wapi.Services;

namespace test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filepath = "test.xlsx";
            ImportTemplate it = new ImportTemplate();
            it.ColumnMapping.Add(new ColumnDefinition {ExcelColumn = "A", PropertyName = "IdBiopsie" });
            var a = new ExcelReaderService().ReadPatientRecords(filepath, it);
            Console.WriteLine("");
        }

        public static ImportTemplate loadTestTemplate()
        {
            ImportTemplate it = new ImportTemplate();
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumn = "A", PropertyName = "Chromosome" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumn = "B", PropertyName = "Region" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumn = "C", PropertyName = "Type" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumn = "F", PropertyName = "Length" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumn = "G", PropertyName = "Count" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumn = "H", PropertyName = "Coverage" });
            return it;
        }
    }
}
