using UIKit;
using Foundation;

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
			Window = new UIWindow (UIScreen.MainScreen.Bounds);
			Window.RootViewController = new UINavigationController(new TodoItemsViewController ());      
			Window.MakeKeyAndVisible ();

			return true;
		}
	}
}