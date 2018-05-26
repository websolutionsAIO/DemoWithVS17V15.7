using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using App3.ViewModels;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using System.Threading;
using System.IO;
using Google.Apis.Upload;

namespace App3
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Status.Text = txtName.Text + " - " + txtAge.Text;

            var abc = FirstVM.DoSomeDataAccess();

            Status.Text = abc;// txtName.Text + " - " + txtAge.Text;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            string abc = FirstVM.AddRecord(int.Parse(txtAge.Text));

            Status.Text = abc;// txtName.Text + " - " + txtAge.Text;
        }

        //private void push()
        //{
        //    GoogleWebAuthorizationBroker.Folder = "Drive.Sample";
        //    UserCredential credential;
        //    using (var stream = new System.IO.FileStream("client_secrets.json",
        //        System.IO.FileMode.Open, System.IO.FileAccess.Read))
        //    {
        //        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
        //            GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None);
        //    }

        //    // Create the service.
        //    var service = new DriveService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = "Drive API Sample",
        //    });

        //    await UploadFileAsync(service);
        //}

        // CHANGE THIS if you upload a file type other than a jpg
        private const string ContentType = @"application/x-sqlite3";

        // <summary>The Drive API scopes.</summary>
        private static readonly string[] Scopes = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };

        private string dbPath = Path.Combine(
                     Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                     "ormdemo.db3");

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Run();
        }

        private async Task Run()
        {
            GoogleWebAuthorizationBroker.Folder = "Drive.Sample";
            UserCredential credential;

            string filename = "client_secrets.json";
            filename = "client_secret_790001279756-k1fee1nrtqr60hvd6eihhra81eg4tqla.apps.googleusercontent.com.json";

            using (var stream = new System.IO.FileStream(filename,
                System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None);
            }

            // Create the service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "App3"// "Drive API Sample",
            });

            await UploadFileAsync(service);

            //// uploaded succeeded
            //Console.WriteLine("\"{0}\" was uploaded successfully", uploadedFile.Title);
            //await DownloadFile(service, uploadedFile.DownloadUrl);
            //await DeleteFile(service, uploadedFile);
        }

        private Task<IUploadProgress> UploadFileAsync(DriveService service)
        {
            var title = dbPath;// "ormdemo.db3";
            if (title.LastIndexOf('\\') != -1)
            {
                title = title.Substring(title.LastIndexOf('\\') + 1);
            }

            var uploadStream = new System.IO.FileStream(dbPath, System.IO.FileMode.Open,
                System.IO.FileAccess.Read);

            var insert = service.Files.Insert(new Google.Apis.Drive.v2.Data.File { Title = title }, uploadStream, ContentType);

            insert.ChunkSize = FilesResource.InsertMediaUpload.MinimumChunkSize * 2;
            //insert.ProgressChanged += Upload_ProgressChanged;
            //insert.ResponseReceived += Upload_ResponseReceived;

            var task = insert.UploadAsync();

            task.ContinueWith(t =>
            {
                // NotOnRanToCompletion - this code will be called if the upload fails
                Console.WriteLine("Upload Failed. " + t.Exception);
            }, TaskContinuationOptions.NotOnRanToCompletion);
            task.ContinueWith(t =>
            {
                //Logger.Debug("Closing the stream");
                uploadStream.Dispose();
                //Logger.Debug("The stream was closed");
            });

            return task;
        }

    }
}
