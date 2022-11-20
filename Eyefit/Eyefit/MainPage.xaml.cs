using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eyefit
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private void Question1Checkbox1_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(e.Value)
            {
                Question1Checkbox2.IsChecked = false;
                Question1Checkbox3.IsChecked = false;
                Question1Checkbox4.IsChecked = false;
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void Question1Checkbox2_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                Question1Checkbox1.IsChecked = false;
                Question1Checkbox3.IsChecked = false;
                Question1Checkbox4.IsChecked = false;
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void Question1Checkbox3_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                Question1Checkbox2.IsChecked = false;
                Question1Checkbox1.IsChecked = false;
                Question1Checkbox4.IsChecked = false;
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void Question1Checkbox4_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                Question1Checkbox2.IsChecked = false;
                Question1Checkbox3.IsChecked = false;
                Question1Checkbox1.IsChecked = false;
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(Question2.IsVisible==true)
            {
                Question3.IsVisible = true;
                Question2.IsVisible = false;
            }
            else if (Question3.IsVisible == true)
            {
                Question4.IsVisible = true;
                Question3.IsVisible = false;
            }
        }

        private void Question3Checkbox4_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
          
        }

        private void Question3Checkbox3_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
           
        }

        private void Question3Checkbox2_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            
        }

        private void Question3Checkbox1_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                Question3Checkbox2.IsChecked = false;
                Question3Checkbox3.IsChecked = false;
                Question3Checkbox4.IsChecked = false;
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void Question2Checkbox4_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                Question3Checkbox2.IsChecked = false;
                Question3Checkbox3.IsChecked = false;
                Question3Checkbox1.IsChecked = false;
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void Question2Checkbox3_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                Question3Checkbox2.IsChecked = false;
                Question3Checkbox1.IsChecked = false;
                Question3Checkbox4.IsChecked = false;
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void Question2Checkbox2_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                Question3Checkbox1.IsChecked = false;
                Question3Checkbox3.IsChecked = false;
                Question3Checkbox4.IsChecked = false;
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }
    }
}
