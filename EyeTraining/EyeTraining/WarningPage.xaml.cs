using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eyefit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WarningPage : ContentPage
    {
        public WarningPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            await Navigation.PushAsync(new ScanPage());
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Preferences.Set("ShowWarning", e.Value);
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //Bluetooth bl = new Bluetooth();
            //Navigation.PushAsync(new QuestionPage(bl));
        }
    }
}