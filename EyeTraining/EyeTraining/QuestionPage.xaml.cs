using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.BLE.Abstractions;

namespace Eyefit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage
    {
        public string Answer2, Answer3, Answer4, Answer5,ProfName;
        public bool alive = true;
        public Bluetooth bluetooth = new Bluetooth();
        public QuestionPage(Bluetooth bl)
        {
            InitializeComponent();
            bluetooth = bl;
            //Device.StartTimer(TimeSpan.FromSeconds(1), StateTick);
        }

        private bool StateTick()
        {
            if(bluetooth.GetState()!= DeviceState.Connected)
            {
                ConLost.IsVisible = true;
            }
            else
            {
                ConLost.IsVisible = false;
            }
            return alive;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(QuestionPrev.IsVisible)
            {
                QuestionPrev.IsVisible = false;
                Question1.IsVisible = true;
                indicatorView.Position = 1;
                QuestionButton.IsEnabled = false;
                QuestionButton.Text = "Далее";
            }
            else if(Question1.IsVisible)
            {
                if(NameProf.Text != "")
                {
                    Question1.IsVisible = false;
                    Question2.IsVisible = true;
                    indicatorView.Position = 2;
                    QuestionButton.IsEnabled = false;
                    ProfName = NameProf.Text;
                }
                else
                {
                    DisplayAlert("", "Введите имя профиля", "ok");
                }
            } 
            else if (Question2.IsVisible)
            {
                Question2.IsVisible = false;
                Question3.IsVisible = true;
                indicatorView.Position = 3;
                QuestionButton.IsEnabled = false;
            }
            else if (Question3.IsVisible)
            {
                Question3.IsVisible = false;
                Question4.IsVisible = true;
                indicatorView.Position = 4;
                QuestionButton.IsEnabled = false;
            }
            else if (Question4.IsVisible)
            {
                Question4.IsVisible = false;
                Question5.IsVisible = true;
                indicatorView.Position = 5;
                QuestionButton.IsEnabled = false;
            }
            else if (Question5.IsVisible)
            {
                Question5.IsVisible = false;
                Neobez.IsVisible = true;
                indicatorView.Position = 6;
                QuestionButton.Text = "Завершить";
            }
            else if (Neobez.IsVisible)
            {
                try
                {
                    string Answer = Answer2 + "-" + Answer3 + "-" + Answer4 + "-" + Answer5;
                    string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath("Eyesfit.db");
                    var database = new SQLiteConnection(databasePath);
                    var list = database.Query<Answers>("select * from Answer where Answer_ID=?", Answer);

                    Preferences.Set("NameProfile", ProfName);
                    Preferences.Set("ProcedNubmer", list.First().Proc);
                    Preferences.Set("DateNextProc", DateTime.Now);
                    if (list.First().Proc == 1)
                    {
                        Preferences.Set("CountProc", 10);
                    }
                    if (list.First().Proc == 2)
                    {
                        Preferences.Set("CountProc", 10);
                    }
                    if (list.First().Proc == 3)
                    {
                        Preferences.Set("CountProc", 10);
                    }
                    if (list.First().Proc == 4)
                    {
                        Preferences.Set("CountProc", 7);
                    }

                    Preferences.Set("DoneProc", 0);

                    NeobezData();

                    Preferences.Set("FinalRes", false);

                    Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    DisplayAlert("", ex.ToString(), "ok");
                }
            }
        }

        private void NeobezData()
        {
            try
            {
                if (ShpL.Text != null)
                {
                    Preferences.Set("SShpL", ShpL.Text);
                }
                else
                {
                    Preferences.Set("SShpL", "00.00");
                }
                if (ShpR.Text != null)
                {
                    Preferences.Set("SShpR", ShpR.Text);
                }
                else
                {
                    Preferences.Set("SShpR", "00.00");
                }
                if (CylL.Text != null)
                {
                    Preferences.Set("SCylL", CylL.Text);
                }
                else
                {
                    Preferences.Set("SCylL", "00.00");
                }
                if (CylR.Text != null)
                {
                    Preferences.Set("SCylR", CylR.Text);
                }
                else
                {
                    Preferences.Set("SCylR", "00.00");
                }

                if (AxL.Text != null)
                {
                    Preferences.Set("SAxL", AxL.Text);
                }
                else
                {
                    Preferences.Set("SAxL", "180");
                }

                if (AxR.Text != null)
                {
                    Preferences.Set("SAxR", AxR.Text);
                }
                else
                {
                    Preferences.Set("SAxL", "180");
                }



                if (OsL.Text != null)
                {
                    Preferences.Set("SOsL", OsL.Text);
                }
                else
                {
                    Preferences.Set("SOsL", "1.0");
                }
                if (OsR.Text != null)
                {
                    Preferences.Set("SOsR", OsR.Text);
                }
                else
                {
                    Preferences.Set("SOsR", "1.0");
                }

                if (VGDL.Text != null)
                {
                    Preferences.Set("SVGDL", VGDL.Text);
                }
                else
                {
                    Preferences.Set("SVGDL", "18");
                }
                if (VGDR.Text != null)
                {
                    Preferences.Set("SVGDR", VGDR.Text);
                }
                else
                {
                    Preferences.Set("SVGDR", "18");
                }
            }
            catch(Exception ex)
            {
                DisplayAlert("", ex.ToString(), "ok");
            }
        }
        

        private void Question3Q1_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question3Q1.IsChecked)
            {
                if (Question3Q2.IsChecked == true)
                    Question3Q2.IsChecked = false;
                if (Question3Q3.IsChecked == true)
                    Question3Q3.IsChecked = false;
                if (Question3Q4.IsChecked == true)
                    Question3Q4.IsChecked = false;
                Question3Q1.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer3 = "3.1";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question3Q2_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question3Q2.IsChecked)
            {
                if (Question3Q1.IsChecked == true)
                    Question3Q1.IsChecked = false;
                if (Question3Q3.IsChecked == true)
                    Question3Q3.IsChecked = false;
                if (Question3Q4.IsChecked == true)
                    Question3Q4.IsChecked = false;
                Question3Q2.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer3 = "3.2";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question3Q3_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question3Q3.IsChecked)
            {
                if (Question3Q1.IsChecked == true)
                    Question3Q1.IsChecked = false;
                if (Question3Q2.IsChecked == true)
                    Question3Q2.IsChecked = false;
                if (Question3Q4.IsChecked == true)
                    Question3Q4.IsChecked = false;
                Question3Q3.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer3 = "3.3";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question3Q4_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question3Q4.IsChecked)
            {
                if (Question3Q1.IsChecked == true)
                    Question3Q1.IsChecked = false;
                if (Question3Q3.IsChecked == true)
                    Question3Q3.IsChecked = false;
                if (Question3Q2.IsChecked == true)
                    Question3Q2.IsChecked = false;
                Question3Q4.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer3 = "3.4";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }


        private void NameProf_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameProf.Text != "")
            {
                QuestionButton.IsEnabled = true;
            }
        }

        private void Question4Q1_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question4Q1.IsChecked)
            {
                if (Question4Q2.IsChecked == true)
                    Question4Q2.IsChecked = false;
                if (Question4Q3.IsChecked == true)
                    Question4Q3.IsChecked = false;
                if (Question4Q4.IsChecked == true)
                    Question4Q4.IsChecked = false;
                if (Question4Q5.IsChecked == true)
                    Question4Q5.IsChecked = false;
                Question4Q1.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer4 = "4.1";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question4Q2_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question4Q2.IsChecked)
            {
                if (Question4Q1.IsChecked == true)
                    Question4Q1.IsChecked = false;
                if (Question4Q3.IsChecked == true)
                    Question4Q3.IsChecked = false;
                if (Question4Q4.IsChecked == true)
                    Question4Q4.IsChecked = false;
                if (Question4Q5.IsChecked == true)
                    Question4Q5.IsChecked = false;
                Question4Q2.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer4 = "4.2";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question4Q3_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question4Q3.IsChecked)
            {
                if (Question4Q2.IsChecked == true)
                    Question4Q2.IsChecked = false;
                if (Question4Q1.IsChecked == true)
                    Question4Q1.IsChecked = false;
                if (Question4Q4.IsChecked == true)
                    Question4Q4.IsChecked = false;
                if (Question4Q5.IsChecked == true)
                    Question4Q5.IsChecked = false;
                Question4Q3.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer4 = "4.3";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question4Q4_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question4Q4.IsChecked)
            {
                if (Question4Q2.IsChecked == true)
                    Question4Q2.IsChecked = false;
                if (Question4Q3.IsChecked == true)
                    Question4Q3.IsChecked = false;
                if (Question4Q1.IsChecked == true)
                    Question4Q1.IsChecked = false;
                if (Question4Q5.IsChecked == true)
                    Question4Q5.IsChecked = false;
                Question4Q4.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer4 = "4.4";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question4Q5_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question4Q5.IsChecked)
            {
                if (Question4Q2.IsChecked == true)
                    Question4Q2.IsChecked = false;
                if (Question4Q3.IsChecked == true)
                    Question4Q3.IsChecked = false;
                if (Question4Q4.IsChecked == true)
                    Question4Q4.IsChecked = false;
                if (Question4Q1.IsChecked == true)
                    Question4Q1.IsChecked = false;
                Question4Q5.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer4 = "4.5";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question5Q1_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question5Q1.IsChecked)
            {
                if (Question5Q2.IsChecked == true)
                    Question5Q2.IsChecked = false;
                if (Question5Q3.IsChecked == true)
                    Question5Q3.IsChecked = false;
                Question5Q1.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer5 = "5.1";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question5Q2_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question5Q2.IsChecked)
            {
                if (Question5Q1.IsChecked == true)
                    Question5Q1.IsChecked = false;
                if (Question5Q3.IsChecked == true)
                    Question5Q3.IsChecked = false;
                Question5Q2.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer5 = "5.2";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question5Q3_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question5Q3.IsChecked)
            {
                if (Question5Q2.IsChecked == true)
                    Question5Q2.IsChecked = false;
                if (Question5Q1.IsChecked == true)
                    Question5Q1.IsChecked = false;
                Question5Q3.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer5 = "5.3";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void ContentPage_Disappearing(object sender, EventArgs e)
        {
            alive = false;
        }

        private void Question2Q1_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question2Q1.IsChecked)
            {
                if (Question2Q2.IsChecked == true)
                    Question2Q2.IsChecked = false;
                if (Question2Q3.IsChecked == true)
                    Question2Q3.IsChecked = false;
                if (Question2Q4.IsChecked == true)
                    Question2Q4.IsChecked = false;
                Question2Q1.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer2 = "2.1";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }
        private void Question2Q2_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question2Q2.IsChecked)
            {
                if (Question2Q1.IsChecked == true)
                    Question2Q1.IsChecked = false;
                if (Question2Q3.IsChecked == true)
                    Question2Q3.IsChecked = false;
                if (Question2Q4.IsChecked == true)
                    Question2Q4.IsChecked = false;
                Question2Q2.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer2 = "2.2";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question2Q3_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question2Q3.IsChecked)
            {
                if (Question2Q2.IsChecked == true)
                    Question2Q2.IsChecked = false;
                if (Question2Q1.IsChecked == true)
                    Question2Q1.IsChecked = false;
                if (Question2Q4.IsChecked == true)
                    Question2Q4.IsChecked = false;
                Question2Q3.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer2 = "2.3";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }

        private void Question2Q4_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Question2Q4.IsChecked)
            {
                if (Question2Q2.IsChecked == true)
                    Question2Q2.IsChecked = false;
                if (Question2Q3.IsChecked == true)
                    Question2Q3.IsChecked = false;
                if (Question2Q1.IsChecked == true)
                    Question2Q1.IsChecked = false;
                Question2Q4.IsChecked = true;
                QuestionButton.IsEnabled = true;
                Answer2 = "2.4";
            }
            else
            {
                QuestionButton.IsEnabled = false;
            }
        }
    }
}