using UIKit;

namespace MVVMLightDemo.iOS
{
	public partial class SecondViewController : UIViewController
	{
		public SecondViewController () : base ("SecondViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.Title = "Second Page";
		}
	}
}