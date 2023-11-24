using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ikem23_wapi
{
    public class XMLReader
    {
        public static void ReadEXcelFile(string fileName)
        {
            var workbook = new XLWorkbook(fileName);
            var ws1 = workbook.Worksheet(1);

            var rows = ws1.RangeUsed().RowsUsed().Skip(1); // Skip header row
            foreach (var row in rows)
            {
                var rowNumber = row.RowNumber();
                foreach (var col in ws1.ColumnsUsed())
                {
                    int colNum = col.ColumnNumber();
                    object value  = row.Cell(colNum).Value;
                }
            }
        }
    }
}
