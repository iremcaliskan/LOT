using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views 
{
    public class MainPageCS : TabbedPage
    {
        public MainPageCS()
        {
            //var navigationPage = new NavigationPage(new WatchPageCS());
            //navigationPage.IconImageSource = "schedule.png";
            //navigationPage.Title = "Schedule";

            Children.Add(new TodoListPageCS());
            Children.Add(new WatchPageCS());
            Children.Add(new ReadPageCS());
        }
    }
}
