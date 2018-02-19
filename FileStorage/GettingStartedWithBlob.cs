//----------------------------------------------------------------------------------
// Microsoft Azure Storage Team
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//----------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

// Create share, upload file, download file, list files and folders, copy file, abort copy file, write range, list ranges.
namespace FileStorage
{
    public class GettingStartedWithBlob
    {
        private static string StorageEmulatorLocation =
            @"C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\AzureStorageEmulator.exe";

        public CloudStorageAccount ParseTheConnectionString()
        {
            // Parse the connection string and return a reference to the storage account.
            return CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
        }

        public CloudBlobClient CreateBlobServiceClient()
        {
            CloudStorageAccount storageAccount = ParseTheConnectionString();

            return storageAccount.CreateCloudBlobClient();
        }

        public async void CreateContainer()
        {
            // Retrieve storage account from connection string.
            string connString =
                "DefaultEndpointsProtocol=https;AccountName=kostas;AccountKey=buZHusA5uV8JK4+2s9GDTzwpDHpPPHZZ1R+dbrmE0qGxZUz5CCAMutZR9SCm3EXl1VMc6LTX262B5d4R/l0vtg==";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //StartEmulator(blobClient);

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            Console.WriteLine(container.Exists());

            // Create the container if it doesn't already exist.
            await container.CreateIfNotExistsAsync();

            //By default, the new container is private, meaning that you must specify your storage access key to download blobs from this container.
            //    If you want to make the files within the container available to everyone, you can set the container to be public
            container.SetPermissions(
                new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob});
        }

        public async void UploadBlobToContainer()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            await container.CreateIfNotExistsAsync();

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = File.OpenRead(@"C:\e-PortfolioFiles\IntegrationTestFile.txt"))
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }
        }

        public async void ListBlobsInContainer()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("photos");

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);

                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }
        }

        public async void DownloadBlob()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("photo1.jpg");

            // Save blob contents to a file.
            using (var fileStream = File.OpenWrite(@"path\myfile"))
            {
                await blockBlob.DownloadToStreamAsync(fileStream);
            }
        }

        public async void DownloadToStream()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Retrieve reference to a blob named "myblob.txt"
            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("myblob.txt");

            string text;
            using (var memoryStream = new MemoryStream())
            {
                await blockBlob2.DownloadToStreamAsync(memoryStream);
                text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public async void DeleteBlob()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Retrieve reference to a blob named "myblob.txt".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob.txt");

            // Delete the blob.
            await blockBlob.DeleteAsync();
        }

        public void ClearBlobItemsFromEmulator()
        {
            ExecuteCommandOnEmulator("clear blob");
        }

        public void StartEmulator()
        {
            ExecuteCommandOnEmulator("start");
        }

        public void StopEmulator()
        {
            ExecuteCommandOnEmulator("stop");
        }

        public void ExecuteCommandOnEmulator(string arguments)
        {
            ProcessStartInfo start = new ProcessStartInfo
            {
                Arguments = arguments,
                FileName = StorageEmulatorLocation
            };
            Process proc = new Process
            {
                StartInfo = start
            };

            proc.Start();
            proc.WaitForExit();
        }

        private CloudBlobClient GetAzureClient() => new CloudBlobClient(
            new Uri("http://127.0.0.1:10000/devstoreaccount1"),
            new StorageCredentials("devstoreaccount1", "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw=="));

        public CloudBlockBlob GetBlockBlob()
        {
            var azureCLient = GetAzureClient();
            var container = azureCLient.GetContainerReference("container");
            container.CreateIfNotExists();

            container.SetPermissions(
                new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = File.OpenRead(@"C:\e-PortfolioFiles\IntegrationTestFile.txt"))
            {
                blockBlob.UploadFromStreamAsync(fileStream);
            }

            return container.GetBlockBlobReference("blobname");

        }
    }
}
