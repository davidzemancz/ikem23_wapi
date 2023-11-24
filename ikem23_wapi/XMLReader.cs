using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ikem23_wapi
{

    public class XMLReader
    {
        public static ImportObj ReadEXcelFile(string fileName, ImportTemplate template)
        {
            var importObj = new ImportObj();
            var workbook = new XLWorkbook(fileName);
            Dictionary<int, string> colIdName = new Dictionary<int, string>();
            var ws1 = workbook.Worksheet(1);

            foreach (var hc in ws1.ColumnsUsed())
            {
                int colNum = hc.ColumnNumber();
                colIdName.Add(colNum, ws1.Row(1).Cell(colNum).Value.ToString());
            }

            var rows = ws1.RangeUsed().RowsUsed().Skip(1); // Skip header row
            foreach (var row in rows)
            {
                var rowNumber = row.RowNumber();
                foreach (var col in ws1.ColumnsUsed())
                {
                    int colNum = col.ColumnNumber();
                    string value  = row.Cell(colNum).Value.ToString();
                    ColumnDefinition cd = new ColumnDefinition { Id = colNum, ExcelColumn = colIdName[colNum] };
                    importObj.data.Add((rowNumber, cd), value);
                }
            }

            return importObj;
        }
    }
}
