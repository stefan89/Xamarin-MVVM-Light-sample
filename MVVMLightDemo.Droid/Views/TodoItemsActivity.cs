using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;

using Microsoft.Practices.ServiceLocation;

using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;

using MVVMLightDemo.Common;

namespace MVVMLightDemo.Droid
{
	[Activity (Label = "MVVMLightDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class TodoItemsActivity : ActivityBase
	{
		TodoItemsViewModel _todoItemsViewModel;
		SwipeRefreshLayout swipeRefreshLayout;
		static bool _initialized;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Title = "Todo items";

			SetContentView (Resource.Layout.TodoItemsActivity);

			if (!_initialized)
			{
				_initialized = true;
				ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

				var navigationService = new NavigationService();
				navigationService.Configure(PageConstants.SecondPage, typeof(SecondActivity));

				SimpleIoc.Default.Register<INavigationService>(() => navigationService);
			}

			_todoItemsViewModel = new TodoItemsViewModel ();

			//Create bindings to ViewModel
			ListView listViewTodoItems = FindViewById<ListView> (Resource.Id.listViewTodoItems);
			listViewTodoItems.Adapter = _todoItemsViewModel.TodoItems.GetAdapter (GetTodoItems);
			listViewTodoItems.ItemClick += OnItemSelected;

			Button buttonAddItem = FindViewById<Button> (Resource.Id.buttonAddItem);
			buttonAddItem.SetCommand ("Click", _todoItemsViewModel.AddNewTodoCommand);

			Button buttonNavigateToSecondPage = FindViewById<Button> (Resource.Id.buttonNavigateToSecondPage);
			buttonNavigateToSecondPage.SetCommand ("Click", _todoItemsViewModel.NavigateToSecondPageCommand);

			swipeRefreshLayout = FindViewById<SwipeRefreshLayout> (Resource.Id.refresher);
			swipeRefreshLayout.SetCommand ("Refresh", _todoItemsViewModel.RefreshTodoItemsCommand);

			//Obtain message when refresh is finished
			GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<PullToRefreshMessage> (this, (pullToRefreshMessage) => {
				if (pullToRefreshMessage.IsFinished) {
					swipeRefreshLayout.Refreshing = false;
				}
			});
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