namespace Trivadis.AzureBootcamp.CrossCutting.CloudStorage
{
    public class CloudStorageFileUpload
    {
        public string ContentType { get; set; }
        public long SizeInBytes { get; set; }
        public string RealFileName { get; set; }
        public string FilePath { get; set; }
    }
}
