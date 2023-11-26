using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ikem23_wapi.DTOs;
using ikem23_wapi.Models;
using System;
using System.Reflection;

namespace ikem23_wapi.Services
{

    public class ExcelReaderService
    {

        public List<(MolecularSequence, Observation)> ReadMolecularSequence(string fileName, FhirImportTemplate template, PatientRecordCreateDto patientRecorDto)
        {

            using Stream stream = new StreamReader(fileName).BaseStream;
            return ReadMolecularSequence(stream, template, patientRecorDto);
        }

        public List<Specimen> ReadSpecimenSequence(string fileName, FhirImportTemplate template, PatientRecordCreateDto patientRecorDto)
        {

            using Stream stream = new StreamReader(fileName).BaseStream;
            return ReadSpecimenSequence(stream, template, patientRecorDto);
        }

        /// <summary>
        /// Function reads data from excel file stream provided custom import template for each file. These are selected or created within the UI.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="template"></param>
        /// <param name="patientRecorDto"></param>
        /// <returns></returns>
        public List<(MolecularSequence, Observation)> ReadMolecularSequence(Stream stream, FhirImportTemplate template, PatientRecordCreateDto patientRecorDto)
        {
            ObjReference patient = new ObjReference();
            patient.Reference = "Patient/" + patientRecorDto.PacientId.ToString();
            var retObject = new List<(MolecularSequence, Observation)>();

            var workbook = new XLWorkbook(stream);
            var ws1 = workbook.Worksheet(1);


            if (ws1.RowsUsed().Count() <= 0) return null;
            var rows = ws1.RangeUsed().RowsUsed().Skip(1); // Skip header row
            foreach (var row in rows)
            {
                var rowNumber = row.RowNumber();
                var molecularSequence = new MolecularSequence { };
                var observation = new Observation();
                retObject.Add((molecularSequence, observation));


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


                //Data are loaded based on user selected import template of excel columns
                //This matching is based on FHIR standard name and custom templates
                foreach (var col in ws1.ColumnsUsed())
                {
                    int colNum = col.ColumnNumber();
                    var colDef = template.ColumnMapping.Find(x => x.ExcelColumnLetter == col.ColumnLetter());
                    if (colDef == null) continue;

                    var cellVal = row.Cell(colNum).Value;

                    if (colDef.Id == MoleculeSequenceName.ObservedSeq)
                    {
                        molecularSequence.ObservedSeq = cellVal.ToString();
                    }
                    if (colDef.Id == MoleculeSequenceName.ReadCoverage)
                    {
                        molecularSequence.ReadCoverage = int.Parse(cellVal.ToString());
                    }
                    if (colDef.Id == MoleculeSequenceName.Start)
                    {
                        variant.Start = int.Parse(cellVal.ToString());
                    }
                    if (colDef.Id == MoleculeSequenceName.End)
                    {
                        variant.End = int.Parse(cellVal.ToString());
                    }
                    if (colDef.Id == MoleculeSequenceName.ObservedAllele)
                    {
                        variant.ObservedAllele = cellVal.ToString();
                    }
                    if (colDef.Id == MoleculeSequenceName.ReferenceAllele)
                    {
                        variant.ReferenceAllele = cellVal.ToString();
                    }
                    if (colDef.Id == MoleculeSequenceName.Type)
                    {
                        //qualitiy.Type = cellVal.ToString();
                    }
                    if (colDef.Id == MoleculeSequenceName.Score)
                    {
                        qualitiy.Score = new Score { Value = int.Parse(cellVal.ToString()) };
                    }
                    if (colDef.Id == MoleculeSequenceName.chromosome)
                    {
                        molecularSequence.ReferenceSeq = new ReferenceSeq { Chromosome = new Code { Text = cellVal.ToString() } };
                    }
                    if (colDef.Id == MoleculeSequenceName.orientation)
                    {
                        //molecularSequence.ReferenceSeq = new ReferenceSeq { Orientation = cellVal.ToString()  };
                        //TODO zjistit converzi
                    }
                    if (colDef.Id == ObservationName.observationgeneticsGeneGene)
                    {
                        if (!string.IsNullOrEmpty(cellVal.ToString()))
                        {
                            Dictionary<string, string> dict = new Dictionary<string, string>
                        {
                            { "url", ObservationName.observationgeneticsGeneGene },
                            { "valueString", cellVal.ToString() }
                        };
                            observation.Extension.Add(dict);
                        }
                    }
                    if (colDef.Id == ObservationName.observationgeneticsVariantName)
                    {
                        if (!string.IsNullOrEmpty(cellVal.ToString()))
                        {
                            Dictionary<string, string> dict = new Dictionary<string, string>
                        {
                            { "url", ObservationName.observationgeneticsVariantName },
                            { "valueString", cellVal.ToString() }
                        };
                            observation.Extension.Add(dict);
                        }
                    }

                    if (colDef.Id == ObservationName.observationgeneticsAminoAcidChangeName)
                    {
                        if (!string.IsNullOrEmpty(cellVal.ToString()))
                        {
                            Dictionary<string, string> dict = new Dictionary<string, string>
                        {
                            { "url", ObservationName.observationgeneticsAminoAcidChangeName },
                            { "valueString", cellVal.ToString() }
                        };
                            observation.Extension.Add(dict);
                        }

                    }
                    if (colDef.Id == ObservationName.observationgeneticsDNARegionNameDNARegionName)
                    {
                        if (!string.IsNullOrEmpty(cellVal.ToString()))
                        {
                            Dictionary<string, string> dict = new Dictionary<string, string>
                            {
                                { "url", ObservationName.observationgeneticsDNARegionNameDNARegionName },
                                { "valueString", cellVal.ToString() }
                            };

                            observation.Extension.Add(dict);
                        }
                    }
                    if (colDef.Id == ObservationName.observationgeneticsAminoAcidChangeType)
                    {
                        if (!string.IsNullOrEmpty(cellVal.ToString()))
                        {
                            Dictionary<string, string> dict = new Dictionary<string, string>
                        {
                            { "url", ObservationName.observationgeneticsAminoAcidChangeType },
                            { "valueString", cellVal.ToString() }
                        };
                            observation.Extension.Add(dict);
                        }
                    }
                }
            }

            return retObject;
        }

        public List<Specimen> ReadSpecimenSequence(Stream stream, FhirImportTemplate template, PatientRecordCreateDto patientRecorDto)
        {
            ObjReference patient = new ObjReference();
            patient.Reference = "Patient/" + patientRecorDto.PacientId.ToString();
            var retObject = new List<Specimen>();

            var workbook = new XLWorkbook(stream);
            var ws1 = workbook.Worksheet(1);


            if (ws1.RowsUsed().Count() <= 0) return null;
            var rows = ws1.RangeUsed().RowsUsed().Skip(1); // Skip header row
            foreach (var row in rows)
            {
                var rowNumber = row.RowNumber();
                var specimen = new Specimen();
                retObject.Add(specimen);


                //Data are loaded based on user selected import template of excel columns
                foreach (var col in ws1.ColumnsUsed())
                {
                    int colNum = col.ColumnNumber();
                    var colDef = template.ColumnMapping.Find(x => x.ExcelColumnLetter == col.ColumnLetter());
                    if (colDef == null) continue;

                    var cellVal = row.Cell(colNum).Value;

                    if (colDef.Id == "Název bloku")
                    {
                        //specimen.identifier.NazevBloku = cellVal.ToString();
                    }

                    if (colDef.Id == "ID biopsie")
                    {
                        //specimen.identifier.Value = cellVal.ToString();
                    }
                    if (colDef.Id == "příjem LMP")
                    {
                        if (cellVal.ToString() != "")
                        {
                            specimen.Collection.collectedDateTime = cellVal.ToString();
                        }
                    }
                    if (colDef.Id == "uzavření LMP")
                    {
                        if (cellVal.ToString() != "")
                        {
                            specimen.Processing.Add(new Processing { TimeDateTime = cellVal.ToString() });
                        }
                    }
                }
            }
            return retObject;
        }

        public void renameColumns(FhirImportTemplate template, Dictionary<int, string> colIdName)
        {

        }
    }
}
