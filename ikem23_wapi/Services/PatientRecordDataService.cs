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

        public async Task<List<MolecularSequence>> Get()
        {
            BundleDto<MolecularSequence> bundle = await _httpClient.GetFromJsonAsync<BundleDto<MolecularSequence>>(Globals.FHIRServerUri + "/MolecularSequence");
            return bundle.Entry.Select(e => e.Resource).ToList();
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

                foreach (var (ms,obs) in sequence)
                {
                    string msGuid = Guid.NewGuid().ToString();
                    string msFullUrl = "urn:uuid:" + msGuid;
                    molecularSequences.Add(ms);
                    
                    bundle.Entry.Add(new TransactionEntryDto()
                    {
                        Resource = ms,
                        FullUrl = msFullUrl,
                        Request = new BundleRequestDto()
                        {
                            Method = "POST",
                            Url = "MolecularSequence",
                            //IfNoneExist = "identifier=urn:oid:2.16.528.1.1007.3.1|93827369"
                        }
                    });

                    string obsGuid = Guid.NewGuid().ToString();
                    obs.DerivedFrom.Add(new ObjReference() { Reference = msFullUrl });
                    observations.Add(obs);

                    bundle.Entry.Add(new TransactionEntryDto()
                    {
                        Resource = obs,
                        FullUrl = "urn:uuid:" + obsGuid,
                        Request = new BundleRequestDto()
                        {
                            Method = "POST",
                            Url = "Observation",
                            //IfNoneExist = "identifier=urn:oid:2.16.528.1.1007.3.1|93827369"
                        }
                    });
                }
            }

            var response = await _httpClient.PostAsJsonAsync(Globals.FHIRServerUri, bundle);
            string json = JsonSerializer.Serialize(bundle, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            string err = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

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
