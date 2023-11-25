namespace ikem23_wapi.Models
{
    public static class MoleculeSequenceName
    {
        public const string ResourceType = "MoleculeSequence";
        public const string Id = "id";
        public const string Meta = "meta";
        public const string Identifier = "identifier";
        public const string Type = "type";
        public const string CoordinateSystem = "coordinateSystem";
        public const string Patient = "patient";
        public const string Specimen = "specimen";
        public const string Device = "device";
        public const string Performer = "performer";
        public const string Quantity = "quantity";
        public const string ReferenceSeq = "referenceSeq";
        public const string Variant = "variant";
        public const string ObservedSeq = "ObservedSeq";
        public const string Start = "Start";
        public const string End = "End";
        public const string ReadCoverage = "ReadCoverage";
        public const string ObservedAllele = "ObservedAllele";
        public const string ReferenceAllele = "ReferenceAllele";
        public const string Score = "Score";
        public const string chromosome = "chromosome";
        public const string orientation = "orientation";

        // Add more attributes and sub-attributes as needed
    }

    public static class ObservationName
    {
        public const string ResourceType = "Observation";
        public const string Id = "id";
        public const string Meta = "meta";
        public const string Text = "text";
        public const string Identifier = "identifier";
        public const string Status = "status";
        public const string Category = "category";
        public const string Code = "code";
        public const string Subject = "subject";
        public const string Encounter = "encounter";
        public const string EffectiveDateTime = "effectiveDateTime";
        public const string ValueQuantity = "valueQuantity";
        public const string ValueCodeableConcept = "valueCodeableConcept";
        public const string Interpretation = "interpretation";
        public const string Performer = "performer";
        public const string ReferenceRange = "referenceRange";
        public const string observationgeneticsGeneGene = "observation-geneticsGene.Gene";
        public const string observationgeneticsVariantName = "observation-geneticsVariant.Name";
        public const string observationgeneticsAminoAcidChangeName = "observation-geneticsAminoAcidChange.Name";
        public const string observationgeneticsDNARegionNameDNARegionName = "observation-geneticsDNARegionName.DNARegionName";
        public const string observationgeneticsAminoAcidChangeType = "observation-geneticsAminoAcidChange.Type";

        // Add more attributes and sub-attributes as needed
    }
}
