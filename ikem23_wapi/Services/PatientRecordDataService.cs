using ikem23_wapi.DTOs;
using ikem23_wapi.Models;

namespace ikem23_wapi.Services
{
    public class PatientRecordDataService
    {

        public async Task<List<PatientRecord>> Get()
        {
            return null;
        }

        public async Task Post(PatientRecordCreateDto patientRecorDto)
        {
            DiagnosticReport diagnosticReport = new DiagnosticReport();
            List<Observation> observations = new List<Observation>();
            List<MolecularSequence> molecularSequences = new List<MolecularSequence>();
            
            foreach (var file in patientRecorDto.Files)
            {
                var sequence = ReadMolecularSequence(file, template);
                molecularSequences.Concat(sequence);
            }
        }

        public async Task Delete(int id)
        {
            
        }
    }
}
