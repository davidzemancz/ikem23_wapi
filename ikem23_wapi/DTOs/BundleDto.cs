namespace ikem23_wapi.DTOs
{
    public class BundleDto<T>
    {
        public List<EntryDto<T>> Entry { get; set; }
        public int Total { get; set; }
    }

    public class EntryDto<T>
    {
        public T Resource { get; set; }
    }

   
}
