using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using Microsoft.Practices.ServiceLocation;

namespace MVVMLightDemo.Common
{
	public class TodoItemsViewModel : ViewModelBase
	{
		public RelayCommand AddNewTodoCommand { 
			get {
				return new RelayCommand (() => {
					TodoItems.Add (new TodoItem { Name = "Button clicked item", Description = "Demo item" });
				});
			}
		}

		public RelayCommand NavigateToSecondPageCommand {
			get {
				return new RelayCommand (() => {
					var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
					navigationService.NavigateTo(PageConstants.SecondPage);
				});
			}
		}

		public RelayCommand<TodoItem> SelectTodoItemCommand {
			get {
				return new RelayCommand<TodoItem> (async(todoItem) => {
					var dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
					await dialogService.ShowMessageBox (todoItem.Name + " clicked", "Item clicked");
				});
			}
		}

		public RelayCommand RefreshTodoItemsCommand { 
			get {
				return new RelayCommand (() => {
					TodoItems.Add (new TodoItem { Name = "Pull to refresh item", Description = "Demo item"});

					//Send message that refresh is finished
					Messenger.Default.Send (new PullToRefreshMessage { IsFinished = true });
				});
			}
		}

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
		}
	}
}