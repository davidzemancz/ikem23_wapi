namespace ikem23_wapi.DTOs
{
    public class PatientRecordCreateDto
    {
        public string IdBiopsie { get; set; }
        public string Projekt { get; set; }
        public string Diagnoza { get; set; }
        public string OnkologickyKod { get; set; }
        public int KodPojistovna { get; set; }
        public DateTime PrijemLMP { get; set; }
        public DateTime UzavreniLMP { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
