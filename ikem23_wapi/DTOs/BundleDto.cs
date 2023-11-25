namespace ikem23_wapi.DTOs
{
    public class BundleDto<T>
    {
        public string ResourceType { get; set; } = "Bundle";
        public List<EntryDto<T>> Entry { get; set; } = new();
        public int Total { get; set; }
    }

    public class TransactionBundleDto
    {
        public string ResourceType { get; set; } = "Bundle";
        public List<TransactionEntryDto> Entry { get; set; } = new();
        public string Id { get; set; }
        public string Type { get; set; }
    }

    public class EntryDto<T>
    {
        public T Resource { get; set; }
    }

    public class TransactionEntryDto
    {
        public BundleRequestDto Request { get; set; }
        public string FullUrl { get; set; }
        public object Resource { get; set; }
    }

    public class BundleRequestDto
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public string IfNoneExist { get; set; }
    }

   
}
