using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.CrossCutting.CloudStorage
{
    public class AzureStorageAccount
    {
        private readonly ILogger _log = LogManager.GetLogger(typeof(AzureStorageAccount));

        private const string ContainerName = "chatimages";

        // Lab ---------------------------------------------------------------------------------------------------------------
        public async Task<CloudStorageFileUploadResult> UploadAsync(CloudStorageFileUpload file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            return await Task.FromResult(new CloudStorageFileUploadResult());
        }
        // Lab ---------------------------------------------------------------------------------------------------------------

        private string CreateSharedAccessBlobPolicy(CloudBlockBlob cloudblob)
        {
            SharedAccessBlobPolicy saspolicy = new SharedAccessBlobPolicy();
            saspolicy.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-1);
            saspolicy.SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15); // 15min Zugriff auf den Blob sollte reichen
            saspolicy.Permissions = SharedAccessBlobPermissions.Read;
            string sasToken = cloudblob.GetSharedAccessSignature(saspolicy);

            return cloudblob.Uri + sasToken;
        }

        private CloudBlockBlob CreateCloudBlockBlob(CloudStorageFileUpload file)
        {
            if (string.IsNullOrWhiteSpace(Settings.AzureStorageAccountConnectionString))
            {
                throw new ArgumentException("Storage account connection string not set in web.config! (key=AzureStorageAccount)");
            }

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(Settings.AzureStorageAccountConnectionString);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var cloudBlobContainer = cloudBlobClient.GetContainerReference(ContainerName);
            cloudBlobContainer.CreateIfNotExists(BlobContainerPublicAccessType.Off);

            string fileId = string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(file.RealFileName));

            CloudBlockBlob cloudblob = cloudBlobContainer.GetBlockBlobReference(fileId);

            cloudblob.Properties.ContentType = file.ContentType;
            cloudblob.Metadata["filename"] = file.RealFileName;
            cloudblob.Metadata["length"] = file.SizeInBytes.ToString();

            _log.Info("Create CloudBlockBlob...Filename={0};FileId={1};Container={2}", file.RealFileName, fileId, cloudBlobContainer.Uri);

            return cloudblob;
        }
    }
}
