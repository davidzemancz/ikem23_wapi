using DocumentFormat.OpenXml.Wordprocessing;
using ikem23_wapi;

namespace test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filepath = "test.xlsx";
            ImportTemplate it = new ImportTemplate();
            it.ColumnMapping.Add(new ColumnDefinition {ExcelColumn = "A", PropertyName = "IdBiosepse" });
            var a = ExcelReader.ReadExcelFile(filepath, it);
            Console.WriteLine("");
        }
    }
}
