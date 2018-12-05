using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SfListViewSample
{
    class Behavior : Behavior <SfListView>
    {
        ViewModel viewModel;
        Random random = new Random();
        Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
        public SfListView listView { get; private set; }
        protected override void OnAttachedTo(SfListView bindable)
        {
            base.OnAttachedTo(bindable);
            listView = bindable as SfListView;
            viewModel = new ViewModel();
            listView.BindingContext = viewModel;
            listView.DataSource.DisplayItems.CollectionChanged += DisplayItems_CollectionChanged;
        }

        private async void DisplayItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                await Task.Delay(100);
                var item = e.NewItems[0];
                var itemIndex = listView.DataSource.DisplayItems.IndexOf(item);
                (listView.LayoutManager as LinearLayout).ScrollToRowIndex(itemIndex, true);
            }
        }

        protected override void OnDetachingFrom(SfListView bindable)
        {
            base.OnDetachingFrom(bindable);
            listView.DataSource.DisplayItems.CollectionChanged -= DisplayItems_CollectionChanged;
            listView = null;
        }
    }
}
