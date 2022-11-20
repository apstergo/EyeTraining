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
    public partial class ResultTreatment : ContentPage
    {
        public ResultTreatment()
        {
            InitializeComponent();
        }

        private void radioXAMLType_OnChangeSelected(object sender, EventArgs e)
        {
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(ShpL.Text!=null)
            {
                Preferences.Set("ShpL", ShpL.Text);
            }
            else
            {
                Preferences.Set("ShpL", "00.00");
            }
            if (ShpR.Text != null)
            {
                Preferences.Set("ShpR", ShpR.Text);
            }
            else
            {
                Preferences.Set("ShpR", "00.00");
            }
            if (CylL.Text != null)
            {
                Preferences.Set("CylL", CylL.Text);
            }
            else
            {
                Preferences.Set("CylL", "00.00");
            }
            if (CylR.Text != null)
            {
                Preferences.Set("CylR", CylR.Text);
            }
            else
            {
                Preferences.Set("CylR", "00.00");
            }

            if (AxL.Text != null)
            {
                Preferences.Set("AxL", AxL.Text);
            }
            else
            {
                Preferences.Set("AxL", "180");
            }

            if (AxR.Text != null)
            {
                Preferences.Set("AxR", AxR.Text);
            }
            else
            {
                Preferences.Set("AxL", "180");
            }



            if (OsL.Text != null)
            {
                Preferences.Set("OsL", OsL.Text);
            }
            else
            {
                Preferences.Set("OsL", "1.0");
            }
            if (OsR.Text != null)
            {
                Preferences.Set("OsR", OsR.Text);
            }
            else
            {
                Preferences.Set("OsR", "1.0");
            }

            if (VGDL.Text != null)
            {
                Preferences.Set("VGDL", VGDL.Text);
            }
            else
            {
                Preferences.Set("VGDL", "18");
            }
            if (VGDR.Text != null)
            {
                Preferences.Set("VGDR", VGDR.Text);
            }
            else
            {
                Preferences.Set("VGDR", "18");
            }

            Preferences.Set("FinalRes", true);

            Navigation.PushAsync(new Resultsresults());
        }

    }
}