using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eyefit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        Bluetooth bluetooth = new Bluetooth();
        public bool isWork = false,isStarted=false;
        public int time = 0;
        public DateTime starttime, stoptime;
        public TestPage(Bluetooth bl)
        {
            InitializeComponent();
            bluetooth = bl;
            MessagingCenter.Subscribe<string>(this, "hi", (sender) =>
            {
                DataLable.Text += sender + Environment.NewLine;
                isWork = false;
                time = 0;
            });
            MessagingCenter.Subscribe<string>(this, "Start", (sender) =>
            {
                StatusLable.Text = "Старт";
                if(!isStarted)
                {
                    Device.StartTimer(TimeSpan.FromSeconds(1), StartTimer);
                    isStarted = true;
                    starttime = DateTime.Now;
                }
                isWork = true;
            });
            MessagingCenter.Subscribe<string>(this, "Finesh", (sender) =>
            {
                StatusLable.Text = "Закончено";
                isWork = false;
                time = 0;
            });
            //MessagingCenter.Subscribe<string>(this, "Program", (sender) =>
            //{
            //    StatusLable.Text = "Наденьте устройство";
            //    isWork = false;
            //    time = 0;
            //});
            MessagingCenter.Subscribe<string>(this, "Remove", (sender) =>
            {
                if(isWork)
                {
                    StatusLable.Text = "Девайс снят";
                    isWork = false;
                    stoptime = DateTime.Now;
                }
            });
            MessagingCenter.Subscribe<string>(this, "Continue", (sender) =>
            {
                starttime = DateTime.Now.AddSeconds(-((stoptime - starttime).TotalSeconds));
                StatusLable.Text = "Работаем дальше";
                isWork = true;
                
            });
        }

        private bool StartTimer()
        {
            if (isWork)
            {
                time++;
                Time.Text = (DateTime.Now-starttime).Minutes  + ":" + (DateTime.Now-starttime).Seconds.ToString("00");
            }
            return isStarted;
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            bluetooth.WriteBLE("2");
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            bluetooth.WriteBLE("3");
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            bluetooth.WriteBLE("4");
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            bluetooth.WriteBLE("0");
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            bluetooth.Disconnect();
            await Navigation.PopAsync();
        }

        private void ContentPage_Disappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<string>(this, "hi");
            MessagingCenter.Unsubscribe<string>(this, "Start");
            MessagingCenter.Unsubscribe<string>(this, "Finesh");
            MessagingCenter.Unsubscribe<string>(this, "Remove");
            MessagingCenter.Unsubscribe<string>(this, "Continue");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            bluetooth.WriteBLE("1");
            starttime = DateTime.Now;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var bytes = bluetooth.GetValue();
            if (bytes != null)
            {
                DataLable.Text = bytes.ToString();
            }
        }
    }
}