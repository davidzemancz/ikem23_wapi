﻿using DocumentFormat.OpenXml.Wordprocessing;
using ikem23_wapi;
using ikem23_wapi.DTOs;
using ikem23_wapi.Models;
using ikem23_wapi.Services;
using System.Net.Http;
using System.Text.Json;

namespace test
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string filepath = "../../../../test2.xlsx";
            ImportTemplate it = loadTestTemplate();
            var http = new HttpClient();
            http.DefaultRequestHeaders.Add("x-api-key", Globals.FHIRServerApiKey);
            var importService = new ImportTemplateDataService(http);
            var excelService = new ExcelReaderService();
            var patientRecordService = new PatientRecordDataService(excelService, http);

            using Stream stream = new StreamReader(filepath).BaseStream;
             
            await patientRecordService.Post(new PatientRecordCreateDto()
            {
                PacientId = 1,
                Files = new List<PatientRecordFileDto>() { new PatientRecordFileDto() { Template = it, File = stream } }

            });
        }

        public static ImportTemplate loadTestTemplate()
        {
            ImportTemplate it = new ImportTemplate();
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "A", Id = "chromosome" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "B", Id = "Region" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "D", Id = "ReferenceAllele" });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "E", Id = "ObservedAllele" });
            return it;
        }
    }
}
