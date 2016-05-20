using System;

namespace Trivadis.AzureBootcamp.CrossCutting.CloudStorage
{
    public class CloudStorageFileUploadResult
    {
        public String Uri { get; set; }
        public string ContentType { get; set; }
        public string Filename { get; set; }
    }
}
