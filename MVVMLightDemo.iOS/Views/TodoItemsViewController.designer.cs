// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MVVMLightDemo.iOS
{
	[Register ("TodoItemsViewController")]
	partial class TodoItemsViewController
	{
		[Outlet]
		UIKit.UIButton buttonAddItem { get; set; }

		[Outlet]
		UIKit.UIButton buttonNavigateToSecondPage { get; set; }

		[Outlet]
		UIKit.UITableView tableViewTodoItems { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonAddItem != null) {
				buttonAddItem.Dispose ();
				buttonAddItem = null;
			}

			if (buttonNavigateToSecondPage != null) {
				buttonNavigateToSecondPage.Dispose ();
				buttonNavigateToSecondPage = null;
			}

			if (tableViewTodoItems != null) {
				tableViewTodoItems.Dispose ();
				tableViewTodoItems = null;
			}
		}
	}
}
