using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;

using MVVMLightDemo.Common;

using GalaSoft.MvvmLight.Helpers;

namespace MVVMLightDemo.Droid
{
	[Activity (Label = "MVVMLightDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class TodoItemsActivity : Activity
	{
		TodoItemsViewModel _todoItemsViewModel;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Title = "Todo items";

			SetContentView (Resource.Layout.TodoItemsActivity);

			_todoItemsViewModel = new TodoItemsViewModel ();

			//Create bindings to ViewModel
			ListView listViewTodoItems = FindViewById<ListView> (Resource.Id.listViewTodoItems);
			listViewTodoItems.Adapter = _todoItemsViewModel.TodoItems.GetAdapter (GetTodoItems);
			listViewTodoItems.ItemClick += OnItemSelected;

			Button buttonAddItem = FindViewById<Button> (Resource.Id.buttonAddItem);
			buttonAddItem.SetCommand ("Click", _todoItemsViewModel.AddNewTodoCommand);
		}

		View GetTodoItems(int position, TodoItem todoItem, View convertView)
		{
			if (convertView == null)
			{
				convertView = LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
			}

			var textView1 = convertView.FindViewById<TextView>(Android.Resource.Id.Text1);
			textView1.Text = todoItem.Name;

			var textView2 = convertView.FindViewById<TextView>(Android.Resource.Id.Text2);
			textView2.Text = todoItem.Description;

			return convertView;
		}

		void OnItemSelected(object sender, AdapterView.ItemClickEventArgs e)
		{
			var selectedTodo = _todoItemsViewModel.TodoItems[e.Position];

			_todoItemsViewModel.SelectTodoItemCommand.Execute(selectedTodo);
		}
	}
}