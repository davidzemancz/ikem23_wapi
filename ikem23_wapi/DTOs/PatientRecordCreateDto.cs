using ikem23_wapi.Models;

namespace ikem23_wapi.DTOs
{
    public class PatientRecordCreateDto
    {
        public int PacientId { get; set; }
        public string IdBiopsie { get; set; }
        public string Projekt { get; set; }
        public string Diagnoza { get; set; }
        public double PomerNadorovychBunek { get; set; }
        public string OnkologickyKod { get; set; }
        public int KodPojistovna { get; set; }
        public DateTime PrijemLMP { get; set; }
        public DateTime UzavreniLMP { get; set; }
        public List<PatientRecordFileDto> Files { get; set; }
    }

    public class PatientRecordFileDto
    {
        public Stream File { get; set; }
        public ImportTemplate Template { get; set; }
    }
}
