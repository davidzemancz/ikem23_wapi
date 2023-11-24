using System.Reflection.Metadata.Ecma335;

namespace ikem23_wapi
{
    public class PatientRecord
    {
        public string IdBiopsie { get; set; }
        public string Projekt { get; set; }
        public string Diagnoza { get; set; }
        public string OnkologickyKod { get; set; }
        public int KodPojistovna { get; set; }
        public DateTime PrijemLMP { get; set; }
        public DateTime UzavreniLMP { get; set; }
        public TimeSpan DobaOdezvy => UzavreniLMP - PrijemLMP;
        public int PatientId {  get; set; }
        public string IGVKontrola { get; set; }
        public string MedeaZapis { get; set; }
        public string Sekvenator {  get; set; }
        public string PanelGenu { get; set; }
        public double PomerNadorovychBunek { get; set; }
        public double DNAKoncPo1PCR { get; set; }
        public double DNAPrumernePokryti { get; set; }
        public int DNATMB { get; set; }
        public string DNAMSI { get; set; }
        public string HRD { get; set; }
        public string GenomBuildPuvodni { get; set; }
        public string Chromosome { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }
        public string Reference {  get; set; }
        public string Allele { get; set; }
        public string Length { get; set; }
        public string Count { get; set; }
        public string Coverage { get; set; }
        public string Frequency { get; set; }
        public string ForwardReverseBalance { get; set; } 
        public string AverageQuality { get; set; }
        public string GeneName { get; set; }
        public string CodingRegionChange { get; set; }
        public string AminoAcidChange { get; set; }
        public string ExonNumber { get; set; }
        public string TypeOfMutation { get; set; }

    }
}
