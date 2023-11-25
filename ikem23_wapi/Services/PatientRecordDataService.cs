using ikem23_wapi.DTOs;
using ikem23_wapi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ikem23_wapi.Services
{
    public class PatientRecordDataService
    {
        private readonly ExcelReaderService _excelReaderService;
        private readonly HttpClient _httpClient;


        public PatientRecordDataService(ExcelReaderService excelReaderService, HttpClient httpClient)
        {
            _excelReaderService = excelReaderService;
            _httpClient = httpClient;
        }

        public async Task<List<PatientRecord>> Get()
        {
            return null;
        }

        public async Task Post(PatientRecordCreateDto patientRecorDto)
        {
            TransactionBundleDto bundle = new();
            bundle.Type = "transaction";
            bundle.Id = "my-new-bundle";


            DiagnosticReport diagnosticReport = new DiagnosticReport();
            List<Observation> observations = new List<Observation>();
            List<MolecularSequence> molecularSequences = new List<MolecularSequence>();


            foreach (var file in patientRecorDto.Files)
            {
                var sequence = _excelReaderService.ReadMolecularSequence(file.File, file.Template, patientRecorDto);

                foreach (var item in sequence)
                {
                    bundle.Entry.Add(new TransactionEntryDto()
                    {
                        Resource = new TransactionEntryDto()
                        {
                            Resource = item,
                            FillUrl = "urn:uuid:928c0716-1a7e-427e-95f5-cb56300e1737",
                            Request = new BundleRequestDto()
                            {
                                Method = "POST",
                                Url = "MolecularSequence",
                                IfNoneExist = "identifier=urn:oid:2.16.528.1.1007.3.1|93827369"
                            }
                        }
                    });

                    var response = await _httpClient.PostAsJsonAsync(Globals.FHIRServerUri, item);
                    string json = JsonSerializer.Serialize(item, new JsonSerializerOptions() { });

                    string err = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();
                    string json2 = JsonSerializer.Serialize(bundle, new JsonSerializerOptions() { });
                }
                


                molecularSequences.Concat(sequence);
            }

          
            //TODO: zde ulozit molecularSequences

            //foreach (var molecularSequence in molecularSequences)
            //{
            //    Observation o = new Observation();
            //    o.Code = null;
            //    o.Component = null;
            //    observations.Add(o);
            //    o.Value = new ObjReference { Reference = "MolecularSequence/" + molecularSequence.Id.ToString() };
            //}
        }

        public async Task Delete(int id)
        {

        }
    }
}
