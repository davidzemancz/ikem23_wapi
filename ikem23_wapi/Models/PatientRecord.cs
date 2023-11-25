﻿using System.Reflection.Metadata.Ecma335;
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
        public string ResourceType { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public int CoordinateSystem { get; set; }
        public PatientRecord Patient { get; set; }
        public List<Variant> Variant { get; set; }
        public string ObservedSeq { get; set; }
        public List<Quality> Quality { get; set; }
        public int ReadCoverage { get; set; }
    }

    public class Variant
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string ObservedAllele { get; set; }
        public string ReferenceAllele { get; set; }
    }

    public class Quality
    {
        public string Type { get; set; }
        public Score Score { get; set; }
    }

    public class Score
    {
        public int Value { get; set; }
    }
}
