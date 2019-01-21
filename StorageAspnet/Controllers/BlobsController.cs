using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Web;
using System.Web.Mvc;

namespace StorageAspnet.Controllers
{
    public class BlobsController : Controller
    {
        // GET: Blobs
        public ActionResult Index()
        {
            return View();
        }
        private CloudBlobContainer GetCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("<wwk>_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("test-blob-container");
            return container;
        }
        public string UploadBlob()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference("myBlob");
            using (var fileStream = System.IO.File.OpenRead(@"c:\src\sample.txt"))
            {
                blob.UploadFromStream(fileStream);
            }
            return "success!";
        }
    }
}