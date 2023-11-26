using DocumentFormat.OpenXml.Wordprocessing;
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
        /// <summary>
        /// Testovani program
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            string filepath = "../../../../test2.xlsx";
            FhirImportTemplate it = loadTestTemplate();
            var http = new HttpClient();
            http.DefaultRequestHeaders.Add("x-api-key", Globals.FHIRServerApiKey);
            var importService = new ImportTemplateDataService(http);
            await importService.Post(it);
            var excelService = new ExcelReaderService();
            var patientRecordService = new PatientRecordDataService(excelService, http);

            using Stream stream = new StreamReader(filepath).BaseStream;
             
            await patientRecordService.Post(new PatientRecordCreateDto()
            {
                PacientId = 1,
                Diagnoza = "00059",
                OnkologickyKod = "8190/0",
                IdBiopsie = "5355/23",
                Files = new List<PatientRecordFileDto>() { new PatientRecordFileDto() { Template = it, File = stream } }

            });
        }

        public static FhirImportTemplate loadTestTemplate()
        {
            FhirImportTemplate it = new FhirImportTemplate();
            it.Name = "Default VFN Template";
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "A", Id = MoleculeSequenceName.chromosome });
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "D", Id = MoleculeSequenceName.ReferenceAllele});
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "E", Id = MoleculeSequenceName.ObservedAllele});
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "U", Id = ObservationName.observationgeneticsGeneGene});
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "V", Id = ObservationName.observationgeneticsVariantName});
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "W", Id = ObservationName.observationgeneticsAminoAcidChangeName});
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "Z", Id = ObservationName.observationgeneticsDNARegionNameDNARegionName});
            it.ColumnMapping.Add(new ColumnDefinition { ExcelColumnLetter = "AP", Id =ObservationName.observationgeneticsAminoAcidChangeType});
            return it;
        }
    }
}
