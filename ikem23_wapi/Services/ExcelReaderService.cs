using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ikem23_wapi.Models;
using System.Reflection;

namespace ikem23_wapi.Services
{

    public class ExcelReaderService
    {
        public List<Patient> ReadPatientRecords(string fileName, ImportTemplate template)
        {

            using Stream stream = new StreamReader(fileName).BaseStream;
            return ReadPatientRecords(stream, template);
        }

        public List<MolecularSequence> ReadMolecularSequence(string fileName, ImportTemplate template)
        {

            using Stream stream = new StreamReader(fileName).BaseStream;
            return ReadMolecularSequence(stream, template);
        }

        public List<Patient> ReadPatientRecords(Stream stream, ImportTemplate template)
        {
            var importObj = new List<Patient>();
            var workbook = new XLWorkbook(stream);
            var ws1 = workbook.Worksheet(2);


            if (ws1.RowsUsed().Count() <= 0) return null;
            var rows = ws1.RangeUsed().RowsUsed().Skip(1); // Skip header row
            foreach (var row in rows)
            {
                var rowNumber = row.RowNumber();
                var patientRecord = new Patient { };
                foreach (var col in ws1.ColumnsUsed())
                {
                    int colNum = col.ColumnNumber();
                    var colDef = template.ColumnMapping.Find(x => x.ExcelColumnLetter == col.ColumnLetter());
                    if (colDef == null) continue;

                    var cellVal = row.Cell(colNum).Value;

                    PropertyInfo propInfo = patientRecord.GetType().GetProperty(colDef.PropertyName);
                    if (propInfo == null) throw new Exception("Property not found");
                    if (propInfo.PropertyType == typeof(string))
                    {
                        propInfo.SetValue(patientRecord, cellVal.ToString());
                    }
                    else if (propInfo.PropertyType == typeof(double))
                    {
                        propInfo.SetValue(patientRecord, cellVal.GetNumber());
                    }

                }
                importObj.Add(patientRecord);
            }

            return importObj;
        }

        public List<Patient> ReadPatientRecords(Stream stream, ImportTemplate template)
        {
            var importObj = new List<Patient>();
            var workbook = new XLWorkbook(stream);
            var ws1 = workbook.Worksheet(2);


            if (ws1.RowsUsed().Count() <= 0) return null;
            var rows = ws1.RangeUsed().RowsUsed().Skip(1); // Skip header row
            foreach (var row in rows)
            {
                var rowNumber = row.RowNumber();
                var patientRecord = new Patient { };
                foreach (var col in ws1.ColumnsUsed())
                {
                    int colNum = col.ColumnNumber();
                    var colDef = template.ColumnMapping.Find(x => x.ExcelColumn == col.ColumnLetter());
                    if (colDef == null) continue;

                    var cellVal = row.Cell(colNum).Value;

                    PropertyInfo propInfo = patientRecord.GetType().GetProperty(colDef.PropertyName);
                    if (propInfo == null) throw new Exception("Property not found");
                    if (propInfo.PropertyType == typeof(string))
                    {
                        propInfo.SetValue(patientRecord, cellVal.ToString());
                    }
                    else if (propInfo.PropertyType == typeof(double))
                    {
                        propInfo.SetValue(patientRecord, cellVal.GetNumber());
                    }

                }
                importObj.Add(patientRecord);
            }

            return importObj;
        }

        public List<MolecularSequence> ReadMolecularSequence(Stream stream, ImportTemplate template)
        {
            var importObj = new List<MolecularSequence>();
            var workbook = new XLWorkbook(stream);
            var ws1 = workbook.Worksheet(2);


            if (ws1.RowsUsed().Count() <= 0) return null;
            var rows = ws1.RangeUsed().RowsUsed().Skip(1); // Skip header row
            foreach (var row in rows)
            {
                var rowNumber = row.RowNumber();
                var molecularSequence = new MolecularSequence { };
                foreach (var col in ws1.ColumnsUsed())
                {
                    int colNum = col.ColumnNumber();
                    var colDef = template.ColumnMapping.Find(x => x.ExcelColumn == col.ColumnLetter());
                    if (colDef == null) continue;

                    var cellVal = row.Cell(colNum).Value;

                    PropertyInfo propInfo = molecularSequence.GetType().GetProperty(colDef.PropertyName);
                    if (propInfo == null) throw new Exception("Property not found");
                    if (propInfo.PropertyType == typeof(string))
                    {
                        propInfo.SetValue(molecularSequence, cellVal.ToString());
                    }
                    else if (propInfo.PropertyType == typeof(double))
                    {
                        propInfo.SetValue(molecularSequence, cellVal.GetNumber());
                    }

                }
                importObj.Add(molecularSequence);
            }

            return importObj;
        }

        public void renameColumns(ImportTemplate template, Dictionary<int, string> colIdName)
        {

        }
    }
}
