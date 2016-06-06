using System;
using System.IO;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Azure.WebJobs.Shared
{
    public class DiskSizeCalculator
    {
        private readonly string _storageAccountConnectionString;

        public DiskSizeCalculator(string storageAccountConnectionString)
        {
            _storageAccountConnectionString = storageAccountConnectionString;
        }

        public void Calculate(string containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageAccountConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container
            });

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0} - {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
            builder.AppendLine();
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    builder.AppendFormat("{0} TotalSize={1} GB; TotalFreeSpace={2} GB",
                        drive.Name,
                        ToGb(drive.TotalSize),
                        ToGb(drive.TotalFreeSpace));
                    builder.AppendLine();
                }
            }

            String filename = String.Format("disksize_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
            CloudBlockBlob blob = container.GetBlockBlobReference(filename);
            blob.UploadText(builder.ToString());
        }

        static long ToGb(long size)
        {
            var kb = size / 1024;
            var mb = kb / 1024;
            return mb / 1024;
        }
    }
}
