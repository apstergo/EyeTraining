using Plugin.BLE.Abstractions;
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
    public partial class ProfilePage : ContentPage
    {
        public List<UserProfile> profiles = new List<UserProfile>();
        public DateTime lostconectdt;
        public Bluetooth bluetooth = new Bluetooth();
        public bool check_alive=true, islost=false, lostcontact=true;

        public ProfilePage(Bluetooth bl)
        {
            InitializeComponent();
            bluetooth = bl;
            if(Preferences.Get("NameProfile", null)!=null)
            {
                double progress= 0;
                profiles = new List<UserProfile>();
                var user = new UserProfile();
                user.UserName = Preferences.Get("NameProfile", "Default");
                DateTime dt = Preferences.Get("DateNextProc", DateTime.Now);
                user.NextDate = dt.Date.ToString("M");
                user.NextTime = "в "+dt.ToString("t");
                if (Preferences.Get("CountProc", 0) != 0)
                {
                    progress = Preferences.Get("DoneProc", 0);
                    progress = progress / Preferences.Get("CountProc", 1);
                    user.Progress = progress;
                }
                else
                {
                    user.Progress = 0;
                }
                if(progress<0.2)
                {
                    user.ProgressColor = Color.Red;
                } 
                else if(progress < 0.5)
                {
                    user.ProgressColor = Color.Yellow;
                }
                else if (progress < 0.7)
                {
                    user.ProgressColor = Color.Green;
                }

                user.CountNumberProc = Preferences.Get("CountProc", 2) - Preferences.Get("DoneProc", 1);
                user.CurrentNumberProc = 0;
                profiles.Add(user);
            }
            List.ItemsSource = profiles;
            Device.StartTimer(TimeSpan.FromSeconds(1), StateTick);
        }

        private bool StateTick()
        {
            if (bluetooth.IsDisconnect)
            {
                return false;
            }

            if (!check_alive)
            {
                return false;
            }
            if (bluetooth.GetState() != DeviceState.Connected)
            {
                if (!islost)
                {
                    Device.StartTimer(TimeSpan.FromSeconds(5), SignalOutTime);
                    lostconectdt = DateTime.Now;
                    islost = true;
                }
                if(ConLost.IsVisible == false)
                {
                    ConLost.IsVisible = true;
                }
            }
            else
            {
                islost = false;
                if (ConLost.IsVisible == true)
                {
                    ConLost.IsVisible = false;
                }
            }
            return check_alive;
        }

        private bool SignalOutTime()
        {
            if(bluetooth.IsDisconnect)
            {
                return false;
            }

            if (bluetooth.GetState() == DeviceState.Connected)
            {
                lostcontact = false;
                islost = false;
                bluetooth.StartUpdate();
                return false;
            }
            else
            {
                bluetooth.LostConnect();
            }

            Vibration.Vibrate();

            if (Convert.ToInt32((DateTime.Now - lostconectdt).TotalSeconds) > 30)
            {
                Application.Current.MainPage = new NavigationPage(new ScanPage());
                check_alive = false;
                return false;
            }

            return lostcontact;
        }

        private void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(Preferences.Get("DoneProc", 0) < Preferences.Get("CountProc", 1))
            {
                Navigation.PushAsync(new ProcedPage(bluetooth));
            }
            else
            {
                if(Preferences.Get("FinalRes", false))
                {
                    Navigation.PushAsync(new Resultsresults());
                }
                else
                {
                    Navigation.PushAsync(new ResultTreatment());
                }
                
            }
            List.SelectedItem = null;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuestionPage(bluetooth));
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (Preferences.Get("NameProfile", null) != null)
            {
                double progress = 0;
                profiles = new List<UserProfile>();
                var user = new UserProfile();
                user.UserName = Preferences.Get("NameProfile", "Default");
                DateTime dt = Preferences.Get("DateNextProc", DateTime.Now);
                user.NextDate = dt.Date.ToString("M");
                user.NextTime = "в " + dt.ToString("t");
                if (Preferences.Get("CountProc", 0)!=0)
                {
                    progress = Preferences.Get("DoneProc", 0);
                    progress = progress / Preferences.Get("CountProc", 1);
                    //DisplayAlert("", progress.ToString(), "ok");
                    user.Progress = progress;
                }
                else
                {
                    user.Progress = 0;
                }
                if (progress < 0.25)
                {
                    user.ProgressColor = Color.Red;
                }
                else if (progress < 0.5)
                {
                    user.ProgressColor = Color.Yellow;
                }
                else if (progress > 0.7)
                {
                    user.ProgressColor = Color.Green;
                }
                user.CountNumberProc = Preferences.Get("CountProc", 2) - Preferences.Get("DoneProc", 1);
                profiles.Add(user);
            }
            List.ItemsSource = profiles;

            if(bluetooth.IsProcStart)
            {
                bluetooth.IsProcStart = false;
                bluetooth.WriteBLE("0");
            }
        }
    }
}