using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ikem23_wapi
{

    public class XMLReader
    {
        public static List<PatientRecord> ReadEXcelFile(string fileName, ImportTemplate template)
        {
            var importObj = new List<PatientRecord>();
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
                var PatientRecord = new PatientRecord { };
                foreach (var col in ws1.ColumnsUsed())
                {
                    int colNum = col.ColumnNumber();
                    string value  = row.Cell(colNum).Value.ToString();
                    var colDef = template.ColumnMapping.Find(x => x.ExcelColumn == col.ColumnLetter());
                    if (colDef == null) continue;

                }
            }

            return importObj;
        }

        public void renameColumns(ImportTemplate template, Dictionary<int, string> colIdName)
        {

        }
    }
}
