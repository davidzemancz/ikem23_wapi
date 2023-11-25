using DocumentFormat.OpenXml.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace ikem23_wapi.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public List<PatientName> Name { get; set; }
    }

    public class PatientName
    {
        public string Family { get; set; }
        public List<string> Given { get; set; }
    }

    public class DiagnosticReport
    {
        public string ResourceType { get; set; } = nameof(DiagnosticReport);
        public int Id { get; set; }
        public List<ObjReference> Observation { get; set; }
        public ObjReference Subject { get; set; }
        
        /// <summary>
        /// IdBiopise
        /// </summary>
        public Code Code { get; set; }
        public DateTime EffectiveDateTime { get; set; }  
    }

    public class ObjReference
    {
        public string Reference { get; set; }
    }

    public class Observation
    {
        public string ResourceType { get; set; } = nameof(Observation);
        public string Status { get; set; }
        public Code Code { get; set; }
        public List<Component> Component { get; set; }
        public ObjReference Value { get; set; } //zde je ulozena molecular sequence
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
        public string ResourceType { get; set; } = nameof(MolecularSequence);
        public string Type { get; set; }
        public int CoordinateSystem { get; set; }
        public ObjReference Patient { get; set; }
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
