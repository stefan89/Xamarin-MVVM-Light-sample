using UIKit;
using Foundation;

using GalaSoft.MvvmLight.Helpers;

using MVVMLightDemo.Common;

namespace MVVMLightDemo.iOS
{
	public partial class TodoItemsViewController : UIViewController
	{
		TodoItemsViewModel _todoItemsViewModel;
		ObservableTableViewController<TodoItem> _observableTableViewController;

		public TodoItemsViewController () : base ("TodoItemsViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Todo items";

			_todoItemsViewModel = new TodoItemsViewModel ();

			//Create bindings to ViewModel
			_observableTableViewController = _todoItemsViewModel.TodoItems.GetController (CreateTodoItemCell, BindTodoItemCell);
			_observableTableViewController.TableView = tableViewTodoItems;
			_observableTableViewController.SelectionChanged += OnItemSelected;
			
			buttonAddItem.SetCommand ("TouchUpInside", _todoItemsViewModel.AddNewTodoCommand);
			buttonNavigateToSecondPage.SetCommand ("TouchUpInside", _todoItemsViewModel.NavigateToSecondPageCommand);
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

		void OnItemSelected(object sender, System.EventArgs e){
			
			_todoItemsViewModel.SelectTodoItemCommand.Execute (_observableTableViewController.SelectedItem);
		}
	}
}