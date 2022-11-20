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
    public partial class ProcedPage : ContentPage
    {
        public int starttime = 0, donetime = 0,done = 0, proc=0;
        private bool isStated = false,isStoped=false, isDone=true;
        public Bluetooth bluetooth = new Bluetooth();
        public DateTime startProcTime=DateTime.MaxValue;


        public bool alive = true;
        public ProcedPage(Bluetooth bl)
        {
            InitializeComponent();
           
            Device.StartTimer(TimeSpan.FromSeconds(1), StateTick);
            proc = Preferences.Get("ProcedNubmer", 0);

            bluetooth = bl;
            bluetooth.IsProcStart = true;

            MessagingCenter.Subscribe<string>(this, "Start", (sender) =>
            {
                startProcTime = DateTime.Now;

                Device.StartTimer(TimeSpan.FromSeconds(1), StartTimer);
                StatusLable.Text = "Процедура выполняется.";

                

                MessagingCenter.Subscribe<string>(this, "Remove", (ssender) =>
                {
                    StatusLable.Text = "Процедура прервана. Наденьте устройство.";
                    isStoped = true;
                });

                MessagingCenter.Subscribe<string>(this, "Continue", (ssender) =>
                {
                    StatusLable.Text = "Процедура выполняется.";
                    isStoped = false;
                });
            });

            MessagingCenter.Subscribe<string>(this, "Finesh", (sender) =>
            {
                Vibration.Vibrate();
                if ((DateTime.Now.Subtract(startProcTime).TotalSeconds > (starttime * 0.6)) && isDone)
                {
                    if (proc == 1)
                    {
                        Preferences.Set("DateNextProc", DateTime.Now.AddMonths(1));
                    }
                    if (proc == 2)
                    {
                        Preferences.Set("DateNextProc", DateTime.Now.AddDays(10));
                    }
                    if (proc == 3)
                    {
                        Preferences.Set("DateNextProc", DateTime.Now.AddDays(21));
                    }
                    if (proc == 4)
                    {
                        Preferences.Set("DateNextProc", DateTime.Now.AddDays(7));
                    }

                    done = done + 1;
                    Preferences.Set("DoneProc", done);
                    isDone = false;
                }
                isStated = false;
                MessagingCenter.Unsubscribe<string>(this, "Start");
                MessagingCenter.Unsubscribe<string>(this, "Finesh");
                MessagingCenter.Unsubscribe<string>(this, "Remove");
                MessagingCenter.Unsubscribe<string>(this, "Continue");
                if (bluetooth.IsProcStart)
                {
                    Navigation.PopAsync();
                }
                alive = false;
            });

            
            switch(proc)
            {
                case 1:
                    DiscriptionProc.Text = "Вам рекомендована программа воздействия импульсными токами на параорбитальную область для профилактики зрительного утомления, включающая различные режимы в диапазоне частот 60-80 Гц,а также модулированного сигнала 77АМ.";
                    starttime = 462;
                    break;
                case 2:
                    DiscriptionProc.Text = "Вам рекомендована интенсивная программа воздействия импульсными токами на параорбитальную область для профилактики зрительного утомления, включающая различные режимы в диапазоне частот 60-80 Гц, а также модулированного сигнала 77АМ.";
                    starttime = 522;
                    break;
                case 3:
                    DiscriptionProc.Text = "Вам рекомендована программа воздействия импульсными токами на параорбитальную область для профилактики работы аккомодационного аппарата и улучшения гидродинамических показателей глаза, включающая различные режимы в диапазоне частот 60-80 Гц, а также модулированного сигнала 77.10.";
                    starttime = 502;
                    break;
                case 4:
                    DiscriptionProc.Text = "Вам рекомендована программа воздействия импульсными токами на параорбитальную область для профилактики работы аккомодационного аппарата, улучшения гидродинамических показателей глаза и стиммуляции зрительного нерва, включающая различные режимы в диапазоне частот 2-10 Гц, 60-80 Гц, а также модулированного сигнала 77.10.";
                    starttime = 462;
                    break;
                default:
                    DiscriptionProc.Text = "Вам рекомендована программа воздействия импульсными токами на параорбитальную область для профилактики зрительного утомления, включающая различные режимы в диапазоне частот 60-80 Гц,а также модулированного сигнала 77АМ.";
                    starttime = 582;
                    break;
            }
            LableCountProc.Text = Preferences.Get("CountProc", 1).ToString();
            done = Preferences.Get("DoneProc", 0);
            LableDoneProc.Text = done.ToString();
            OstTime.Text = starttime / 60 + ":" + (starttime % 60).ToString("00");
        }

        private bool StateTick()
        {
            if (bluetooth.GetState() != DeviceState.Connected)
            {
                ConLost.IsVisible = true;
            }
            else
            {
                ConLost.IsVisible = false;
            }
            return alive;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            if(!isStated)
            {
                bluetooth.WriteBLE(proc.ToString());
                isStated = true;
                StartStopButton.Source = "stopbtn.png";
            }
            else
            {
                bluetooth.WriteBLE("0");
            }
        }

        private bool StartTimer()
        {
            if(!isStated)
            {
                return false;
            }
            if(isStoped)
            {
                return isStated;
            }
            int timevar=0;
            donetime++;
            timevar = starttime - donetime;
            if(timevar >= 0)
            {
                DoneTime.Text = donetime / 60 + ":" + (donetime % 60).ToString("00");
                OstTime.Text = timevar / 60 + ":" + (timevar % 60).ToString("00");
            }
            else
            {
                Navigation.PopAsync();
                return false;
            }

            if((donetime>(starttime*0.6))&&isDone)
            {
                if (proc == 1)
                {
                    Preferences.Set("DateNextProc", DateTime.Now.AddMonths(1));
                }
                if (proc == 2)
                {
                    Preferences.Set("DateNextProc", DateTime.Now.AddDays(10));
                }
                if (proc == 3)
                {
                    Preferences.Set("DateNextProc", DateTime.Now.AddDays(21));
                }
                if (proc == 4)
                {
                    Preferences.Set("DateNextProc", DateTime.Now.AddDays(7));
                }

                done = done + 1;
                Preferences.Set("DoneProc", done);
                isDone = false;
            }

            if (bluetooth.GetState() != DeviceState.Connected)
            {
                ConLost.IsVisible = true;
            }
            else
            {
                ConLost.IsVisible = false;
            }
            return isStated;
        }
    }
}