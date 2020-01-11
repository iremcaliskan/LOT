using Todo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Todo
{
    public partial class App : Application
    {
        static TodoItemDatabase database;
        static WatchItemDatabase database1;
        static ReadItemDatabase database2;


        public App()
        {
            InitializeComponent();

            var nav = new NavigationPage(new MainPage());
            nav.BarTextColor = Color.White;

            MainPage = nav;
        }

        public static TodoItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TodoItemDatabase();
                }
                return database;
            }
        }
        public static WatchItemDatabase Database1
        {
            get
            {
                if (database1 == null)
                {
                    database1 = new WatchItemDatabase();
                }
                return database1;
            }
        }
        public static ReadItemDatabase Database2
        {
            get
            {
                if (database2 == null)
                {
                    database2 = new ReadItemDatabase();
                }
                return database2;
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}

