using DocumentFormat.OpenXml.Wordprocessing;
using ikem23_wapi;
using ikem23_wapi.Models;
using ikem23_wapi.Services;
using System.Net.Http;
using System.Text.Json;

namespace test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filepath = "test.xlsx";
            ImportTemplate it = loadTestTemplate();
            var http = new HttpClient();
            http.DefaultRequestHeaders.Add("x-api-key", Globals.FHIRServerApiKey);
            var service = new ImportTemplateDataService(http);

            string String1 = JsonSerializer.Serialize(it);
            var x = service.MapImportTemplateToConceptMap(it);
            var y = service.MapConceptMapToImportTemplate(x);
            string String2 = JsonSerializer.Serialize(y);
            bool rovnajiSe = String1 == String2;
            Console.WriteLine("");
        }

        public static ImportTemplate loadTestTemplate()
        {
            ImportTemplate it = new ImportTemplate();
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "A", Id = "Chromosome" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "B", Id = "Region" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "C", Id = "Type" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "F", Id = "Length" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "G", Id = "Count" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "H", Id = "Coverage" });
            return it;
        }
    }
}
