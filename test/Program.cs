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
            it.ColumnMapping.Add(new ColumnDefinition {ExcelColumnLetter = "A", PropertyName = "IdBiopsie" });
            var a = new ExcelReaderService().ReadMolecularSequences(filepath, it);
            Console.WriteLine("");
        }

        public static ImportTemplate loadTestTemplate()
        {
            ImportTemplate it = new ImportTemplate();
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "A", PropertyName = "Chromosome" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "B", PropertyName = "Region" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "C", PropertyName = "Type" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "F", PropertyName = "Length" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "G", PropertyName = "Count" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "H", PropertyName = "Coverage" });
            return it;
        }
    }
}
