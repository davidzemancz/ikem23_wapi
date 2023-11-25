using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json;




namespace ikem23_wapi.Models
{
    public class PatientRecord
    {
        public int Id { get; set; }
        public int KodPojistovna { get; set; }
        public int PatientId { get; set; }  // rodny cislo 
    }

    public class DiagnosticReport
    {
        public int PatientId { get; set; }
        public string IdBiopsie { get; set; }
        public string Diagnoza { get; set; }
        public string OnkologickyKod { get; set; }
        public DateTime PrijemLMP { get; set; }  // effectiveDateTime
        public DateTime UzavreniLMP { get; set; }
        public double PomerNadorovychBunek { get; set; }
        public Observation observation { get; set; }
    }

    public class Observation
    {
        public string ResourceType { get; set; }
        public string Status { get; set; }
        public Code Code { get; set; }
        public string ValueString { get; set; }
        public List<Component> Component { get; set; }
    }

    public class Code
    {
        public string Text { get; set; }
    }

    public class Component
    {
        public Code Code { get; set; }
        public string ValueString { get; set; }
    }


    public class MolecularSequence
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string patient { get; set; } //tohle ma byt reference
        public string performer { get; set; }
        public int coordinateSystem { get; set; }

        //      "variant": [{"start": 2, "end": 3, "observedAllele": "C", "referenceAllele": "G"}],
        //      "observedSeq": "AGT",
        //      "quality": [{"type": "snp","score": {"value": 30}]

        public int readCoverage { get; set; }
    }
}
