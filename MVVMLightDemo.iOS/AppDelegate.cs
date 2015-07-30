using System;
using System.IO;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using UIKit;
using Foundation;

using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

using Microsoft.Practices.ServiceLocation;

using MVVMLightDemo.Common;

namespace MVVMLightDemo.iOS
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			var navigationController = new UINavigationController(new TodoItemsViewController ());      

			Window = new UIWindow (UIScreen.MainScreen.Bounds);
			Window.RootViewController = navigationController;

			// Initialize and register the Navigation Service
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			var navigationService = new NavigationService();
			navigationService.Initialize(navigationController);
			navigationService.Configure(PageConstants.SecondPage, typeof(SecondViewController));

			SimpleIoc.Default.Register<INavigationService>(() => navigationService);

			Window.MakeKeyAndVisible ();
			return true;
		}

		async Task UploadFileAsync(Uri uri, string filename)
		{
			using (FileStream stream = File.OpenRead(filename))
			{
				var client = new HttpClient();
				var response = await client.PostAsync(uri, new StreamContent(stream)); //1 parmeter is het adres van de server, 2e parameter is de http content
				response.EnsureSuccessStatusCode();
			}
		}

		async Task UploadPhotoAsync (Uri uri, byte[] photoBytes)
		{
			try {
				var httpClient = new HttpClient ();

				var content = new MultipartFormDataContent ();

				var fileContent = new ByteArrayContent (photoBytes);

				fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse ("multipart/form-data");
				fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue ("attachment") {
					FileName = "image.jpg",
					Name = "file"
				};

				content.Add (fileContent);

				await httpClient.PostAsync (uri, content);

			} catch (Exception ex) {
				Debug.WriteLine (ex.Message);
			}
		}
	}
}