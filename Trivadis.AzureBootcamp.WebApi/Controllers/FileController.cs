using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Trivadis.AzureBootcamp.CrossCutting.CloudStorage;

namespace Trivadis.AzureBootcamp.WebApi.Controllers
{
    [RoutePrefix("api/file")]
    public class FileController : ApiControllerBase
    {
        private AzureStorageAccount _storageAccount;

        public FileController()
        {
            _storageAccount = new AzureStorageAccount();
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IHttpActionResult> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            string workingFolder = Path.GetTempPath();
            var provider = new MultipartFormDataStreamProvider(workingFolder);

            await Request.Content.ReadAsMultipartAsync(provider);

            MultipartFileData uploadedfile = provider.FileData.FirstOrDefault();
            if (uploadedfile != null)
            {
                FileInfo tempfile = new FileInfo(uploadedfile.LocalFileName);

                CloudStorageFileUpload cloud = new CloudStorageFileUpload();
                cloud.ContentType = uploadedfile.Headers.ContentType.MediaType;
                cloud.FilePath = tempfile.FullName;
                cloud.RealFileName = uploadedfile.Headers.ContentDisposition.FileName.Trim('"');
                cloud.SizeInBytes = tempfile.Length;

                var result = await _storageAccount.UploadAsync(cloud);

                if (tempfile.Exists)
                {
                    tempfile.Delete();
                }

                return Ok(new
                {
                    ContentType = result.ContentType,
                    Filename = result.Filename,
                    Fileuri = result.Uri
                });
            }

            return BadRequest("no file found in multipart form stream!");
        }
    }
}
