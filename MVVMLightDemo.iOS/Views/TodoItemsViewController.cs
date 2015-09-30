using System;

using UIKit;
using Foundation;

using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Messaging;

using MVVMLightDemo.Common;

namespace MVVMLightDemo.iOS
{
	public partial class TodoItemsViewController : UIViewController
	{
		TodoItemsViewModel _todoItemsViewModel;
		ObservableTableViewController<TodoItem> _observableTableViewController;
		UIRefreshControl _refreshControl;

		public TodoItemsViewController () : base ("TodoItemsViewController", null)
		{
			_todoItemsViewModel = new TodoItemsViewModel ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Todo items";

			_refreshControl = new UIRefreshControl ();
			_refreshControl.AttributedTitle = new NSAttributedString ("Refreshing", new UIStringAttributes ());
			tableViewTodoItems.AddSubview (_refreshControl);

			//Create bindings to ViewModel
			_observableTableViewController = _todoItemsViewModel.TodoItems.GetController (CreateTodoItemCell, BindTodoItemCell);
			_observableTableViewController.TableView = tableViewTodoItems;
			_observableTableViewController.SelectionChanged += OnItemSelected;

			buttonAddItem.SetCommand ("TouchUpInside", _todoItemsViewModel.AddNewTodoCommand);
			buttonNavigateToSecondPage.SetCommand ("TouchUpInside", _todoItemsViewModel.NavigateToSecondPageCommand);

			_refreshControl.SetCommand ("ValueChanged", _todoItemsViewModel.RefreshTodoItemsCommand);

			//OR
			//_refreshControl.ValueChanged += (object sender, EventArgs e) => 
			//{
			//	_todoItemsViewModel.ValueChangedCommand.Execute(null);
			//};

			//Obtain message when refresh is finished
			Messenger.Default.Register<PullToRefreshMessage> (this, (pullToRefreshMessage) => {
				if (pullToRefreshMessage.IsFinished) {
					_refreshControl.EndRefreshing ();
				}
			});
		}

		void StopRefreshing()
		{
			_refreshControl.EndRefreshing ();
		}

		UITableViewCell CreateTodoItemCell(NSString reusableCellId)
		{
			UITableViewCell cell = tableViewTodoItems.DequeueReusableCell (reusableCellId);

			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, reusableCellId);
			}
			return cell;
		}

		void BindTodoItemCell(UITableViewCell cell, TodoItem todoItem, NSIndexPath path)
		{
			cell.TextLabel.Text = todoItem.Name;
			cell.DetailTextLabel.Text = todoItem.Description;
		}

		void OnItemSelected(object sender, EventArgs e){
			
			_todoItemsViewModel.SelectTodoItemCommand.Execute (_observableTableViewController.SelectedItem);
		}
	}
}
//http://stackoverflow.com/questions/28151572/binding-a-property-to-a-viewmodel-with-mvvmlight-and-xamarin-ios