using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using Android.OS;
using Android.App;
using Android.Content;

namespace MVVMLightDemo.Droid
{
	[Activity (Label = "Second Page")]			
	public class SecondActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
		}
	}
}