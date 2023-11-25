using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ikem23_wapi.DTOs;
using ikem23_wapi.Models;
using System.Reflection;

namespace ikem23_wapi.Services
{

    public class ExcelReaderService
    {
       
        public List<MolecularSequence> ReadMolecularSequence(string fileName, ImportTemplate template, PatientRecordCreateDto patientRecorDto)
        {

            using Stream stream = new StreamReader(fileName).BaseStream;
            return ReadMolecularSequence(stream, template, patientRecorDto);
        }

       
        public List<MolecularSequence> ReadMolecularSequence(Stream stream, ImportTemplate template, PatientRecordCreateDto patientRecorDto)
        {
            ObjReference patient = new ObjReference();
            patient.Reference = "Patient/" + patientRecorDto.PacientId.ToString();
            var importObj = new List<MolecularSequence>();
            var workbook = new XLWorkbook(stream);
            var ws1 = workbook.Worksheet(1);


            if (ws1.RowsUsed().Count() <= 0) return null;
            var rows = ws1.RangeUsed().RowsUsed().Skip(1); // Skip header row
            foreach (var row in rows)
            {
                var rowNumber = row.RowNumber();
                var molecularSequence = new MolecularSequence { };

                //Variant info
                List<Variant> variants = new List<Variant>();
                Variant variant = new Variant();
                variants.Add(variant);
                
                //Quality info
                List<Quality> qualities = new List<Quality>();
                Quality qualitiy = new Quality();
                qualities.Add(qualitiy);

                molecularSequence.Patient = patient;
                molecularSequence.Variant = variants;
                molecularSequence.Quality = qualities;

                foreach (var col in ws1.ColumnsUsed())
                {
                    int colNum = col.ColumnNumber();
                    var colDef = template.ColumnMapping.Find(x => x.ExcelColumnLetter == col.ColumnLetter());
                    if (colDef == null) continue;

                    var cellVal = row.Cell(colNum).Value;

                    if (colDef.Id == "ObservedSeq")
                    {
                        molecularSequence.ObservedSeq = cellVal.ToString();
                    }
                    if (colDef.Id == "ReadCoverage")
                    {
                        molecularSequence.ReadCoverage = int.Parse(cellVal.ToString());
                    }
                    if (colDef.Id == "Start")
                    {
                        variant.Start = int.Parse(cellVal.ToString());
                    }
                    if (colDef.Id == "End")
                    {
                        variant.End = int.Parse(cellVal.ToString());
                    }
                    if (colDef.Id == "ObservedAllele")
                    {
                        variant.ObservedAllele = cellVal.ToString();
                    }
                    if (colDef.Id == "ReferenceAllele")
                    {
                        variant.ReferenceAllele = cellVal.ToString();
                    }
                    if (colDef.Id == "Type")
                    {
                        qualitiy.Type = cellVal.ToString();
                    }
                    if (colDef.Id == "Score")
                    {
                        qualitiy.Score = new Score { Value = int.Parse(cellVal.ToString()) };
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
