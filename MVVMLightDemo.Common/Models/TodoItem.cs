using GalaSoft.MvvmLight;

namespace MVVMLightDemo.Common
{
	public class TodoItem : ObservableObject
	{
		public string Name { get; set; }

		public string Description { get; set; }
	}
}