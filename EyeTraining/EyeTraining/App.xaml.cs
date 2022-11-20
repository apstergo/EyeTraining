using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eyefit
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "Eyesfit.db";
        public App()
        {
            InitializeComponent();
            bool show = Preferences.Get("ShowWarning", false);
            if(!show)
            {
                MainPage = new NavigationPage(new WarningPage())
                {
                    BarBackgroundColor = Color.FromHex("#FFFFFF"),
                    BarTextColor = Color.FromHex("#4F4F4E")
                };
            }
            else
            {
                MainPage = new NavigationPage(new ScanPage())
                {
                    BarBackgroundColor = Color.FromHex("#FFFFFF"),
                    BarTextColor = Color.FromHex("#4F4F4E")
                };
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
