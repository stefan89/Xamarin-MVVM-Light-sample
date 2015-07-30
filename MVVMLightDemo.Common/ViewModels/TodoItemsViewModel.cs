using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;

using Microsoft.Practices.ServiceLocation;

namespace MVVMLightDemo.Common
{
	public class TodoItemsViewModel : ViewModelBase
	{
		public RelayCommand AddNewTodoCommand { get; set; }

		public RelayCommand NavigateToSecondPageCommand { get; set; }

		public RelayCommand<TodoItem> SelectTodoItemCommand { get; set; }

		public ObservableCollection<TodoItem> TodoItems { get; set; }

		public TodoItemsViewModel ()
		{
			TodoItems = new ObservableCollection<TodoItem> { 
				new TodoItem { Name = "Afwas", 				Description = "Witte was doen" },
				new TodoItem { Name = "Stofzuigen", 		Description = "Slaapkamers stofzuigen" },
				new TodoItem { Name = "Olie pijlen", 		Description = "Olie pijlen van auto" },
				new TodoItem { Name = "Rekeningen betalen", Description = "Openstaande rekeningen betalen" },
				new TodoItem { Name = "Cadeau kopen", 		Description = "Verjaardagscadeau kopen voor Pietje" }
			};

			AddNewTodoCommand = new RelayCommand (() => {
				TodoItems.Add (new TodoItem { Name = "Button clicked item", Description = "Demo item"});
			});

			NavigateToSecondPageCommand = new RelayCommand (() => {
				var navavigationService = ServiceLocator.Current.GetInstance<INavigationService>();
				navavigationService.NavigateTo(PageConstants.SecondPage);
			});

			SelectTodoItemCommand = new RelayCommand<TodoItem> ((todoItem) => {
				Console.WriteLine (todoItem.Name + " clicked");
			});
		}
	}
}