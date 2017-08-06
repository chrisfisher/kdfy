using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Friendly.Context;
using System.Net.Http.Headers;

namespace Friendly.Controllers
{
    [RoutePrefix("api")]
    public class ImageController : ApiController
    {
        private readonly FriendlyContext _friendlyContext; 

        public ImageController(FriendlyContext friendlyContext)
        {
            _friendlyContext = friendlyContext;
        }

        [Route("images/add")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                var parts = await Request.Content.ReadAsMultipartAsync();

                foreach (var part in parts.Contents)
                {
                    if (part.Headers.ContentType != null)
                    {
                        var imageStream = await part.ReadAsStreamAsync();
                        var imageId = Guid.NewGuid().ToString();                        
                        var fileType = GetFileTypeFromHeader(part.Headers.ContentType);
                        var fileName = String.Format("{0}.{1}", imageId, fileType);
                        SaveImage(imageStream, fileName);
                        var resp = new ImageApiModel { ImageId = imageId, FileType = fileType };
                        return Request.CreateResponse(HttpStatusCode.OK, resp);
                    }
                }

                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Route("images/delete/{id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteImage(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Request.CreateResponse(HttpStatusCode.BadRequest, "Must provide valid id.");

            var image = _friendlyContext.ImageLinks.Find(id);

            if (image == null) return Request.CreateResponse(HttpStatusCode.BadRequest, "Cannot find image with provided id."); ;

            _friendlyContext.ImageLinks.Remove(image);

            try
            {
                _friendlyContext.SaveChanges();
                var storageConnection = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ToString();
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnection);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("location-images");
                if (!container.Exists()) return Request.CreateResponse(HttpStatusCode.BadRequest, "Container 'location-images' does not exist."); ;

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(id);

                await blockBlob.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Deleted successfully"); ;
        }

        private void SaveImage(Stream imageStream, string id)
        {
            var storageConnection = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ToString();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnection);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("location-images");
            container.CreateIfNotExists();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(id);

            blockBlob.UploadFromStream(imageStream);
        }

        private static string GetFileTypeFromHeader(MediaTypeHeaderValue headerValue)
        {
            switch (headerValue.MediaType)
            {
                case "image/jpg":
                    return "jpg";
                case "image/png":
                    return "png";
                default:
                    return "png";
            }
        }
    }

    public class ImageApiModel
    {
        public string ImageId { get; set; }
        public string FileType { get; set; }
    }
}
