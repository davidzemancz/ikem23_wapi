﻿using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
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
            BundleDto<MolecularSequence> bundle = await _httpClient.GetFromJsonAsync<BundleDto<MolecularSequence>>(Globals.FHIRServerUri + "/MolecularSequence");
            List<MolecularSequence> mss = bundle.Entry.Select(e => e.Resource).ToList();

            BundleDto<Observation> bundle2 = await _httpClient.GetFromJsonAsync<BundleDto<Observation>>(Globals.FHIRServerUri + "/Observation");
            List<Observation> obss = bundle2.Entry.Select(e => e.Resource).ToList();

            BundleDto<Specimen> bundle3 = await _httpClient.GetFromJsonAsync<BundleDto<Specimen>>(Globals.FHIRServerUri + "/Specimen");
            List<Specimen> spess = bundle3.Entry.Select(e => e.Resource).ToList();

            //BundleDto<DiagnosticReport> bundle3 = await _httpClient.GetFromJsonAsync<BundleDto<DiagnosticReport>>(Globals.FHIRServerUri + "/DiagnosticReport");
            //List<DiagnosticReport> reps = bundle3.Entry.Select(e => e.Resource).ToList();

            List<PatientRecord> records = new();
            int id = 0;
            foreach (Observation observation in obss)
            {
                string msId = observation.DerivedFrom[0].Reference.Split('/')[1];
                string smId = observation.Specimen.Reference.Split("/")[1];

                MolecularSequence obsMss = mss.Find(ms => ms.Id == msId);

                Specimen obSpec = spess.Find(ms => ms.Id == smId);

                PatientRecord record = new();

                record.Id = ++id;
                record.IdBiopsie = obSpec.Extension[0]["valueString"];
                record.Projekt = msId + "-" + observation.Code.Text;
                record.Chromosome = obsMss.ReferenceSeq.Chromosome.Text;
                record.Reference = obsMss.Variant[0].ReferenceAllele;
                record.Allele = obsMss.Variant[0].ObservedAllele;
                record.Length = (obsMss.Variant[0].End - obsMss.Variant[0].Start).ToString();
                record.Count = "0";
                record.Coverage = obsMss.ReadCoverage.ToString();
                record.ForwardReverseBalance = obsMss.ReferenceSeq.Orientation;
                record.AverageQuality = obsMss.Quality[0].Score.Value.ToString();
                record.GeneName = observation.Extension.Find(elem => elem["url"] == "observation-geneticsGene.Gene").Get("valueString");
                record.CodingRegionChange = observation.Extension.Find(elem => elem["url"] == "observation-geneticsVariant.Namee").Get("valueString");
                record.AminoAcidChange = observation.Extension.Find(elem => elem["url"] == "observation-geneticsAminoAcidChange.Name").Get("valueString");
                record.ExonNumber = observation.Extension.Find(elem => elem["url"] == "observation-geneticsDNARegionName.DNARegionName").Get("valueString");
                record.TypeOfMutation = observation.Extension.Find(elem => elem["url"] == "observation-geneticsAminoAcidChange.Type").Get("valueString");

                records.Add(record);
            }


            return records;
        }


        public async Task Post(Specimen specimen)
        {
            var response = await _httpClient.PostAsJsonAsync(Globals.FHIRServerUri + "/Specimen", specimen);
            string err = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }

        public async Task Post(PatientRecordCreateDto patientRecorDto)
        {
            TransactionBundleDto bundle = new();
            bundle.Type = "transaction";

            DiagnosticReport diagnosticReport = new DiagnosticReport();
            List<Observation> observations = new List<Observation>();
            List<MolecularSequence> molecularSequences = new List<MolecularSequence>();


            foreach (var file in patientRecorDto.Files)
            {
                var sequence = _excelReaderService.ReadMolecularSequence(file.File, file.Template, patientRecorDto);

                Specimen specimen = new Specimen();
                
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("url", patientRecorDto.IdBiopsie);
                dict.Add("valueString", patientRecorDto.IdBiopsie);
                specimen.Extension.Add(dict);

                Dictionary<string, string> dict2 = new Dictionary<string, string>();
                dict2.Add("url", patientRecorDto.Diagnoza);
                dict2.Add("valueString", patientRecorDto.Diagnoza);

                specimen.Extension.Add(dict2);

                Dictionary<string, string> dict3 = new Dictionary<string, string>();
                dict3.Add("url", patientRecorDto.OnkologickyKod);
                dict3.Add("valueString", patientRecorDto.OnkologickyKod);

                specimen.Extension.Add(dict3);

                string sGuid = Guid.NewGuid().ToString();
                string sFullUrl = "urn:uuid:" + sGuid;

                bundle.Entry.Add(new TransactionEntryDto()
                {
                    Resource = specimen,
                    FullUrl = sFullUrl,
                    Request = new BundleRequestDto()
                    {
                        Method = "POST",
                        Url = "Specimen",
                    }
                });

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
                        }
                    });

                    string obsGuid = Guid.NewGuid().ToString();
                    obs.DerivedFrom.Add(new ObjReference() { Reference = msFullUrl });
                    obs.Specimen = new ObjReference() { Reference = sFullUrl };
                    observations.Add(obs);

                    bundle.Entry.Add(new TransactionEntryDto()
                    {
                        Resource = obs,
                        FullUrl = "urn:uuid:" + obsGuid,
                        Request = new BundleRequestDto()
                        {
                            Method = "POST",
                            Url = "Observation",
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
