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
    public partial class Resultsresults : ContentPage
    {
        public Resultsresults()
        {
            InitializeComponent();


            if (Preferences.Get("SShpL", null) != null)
            {
                SShpL.Text = Preferences.Get("SShpL", " ");
            }
            if (Preferences.Get("SShpR", null) != null)
            {
                SShpR.Text = Preferences.Get("SShpR", " ");
            }
            if (Preferences.Get("SCylL", null) != null)
            {
                SCylL.Text = Preferences.Get("SCylL", " ");
            }
            if (Preferences.Get("SCylR", null) != null)
            {
                SCylR.Text = Preferences.Get("SCylR", " ");
            }
            if (Preferences.Get("SAxL", null) != null)
            {
                SAxL.Text = Preferences.Get("SAxL", " ");
            }
            if (Preferences.Get("SAxR", null) != null)
            {
                SAxR.Text = Preferences.Get("SAxR", " ");
            }


            if (Preferences.Get("SOsL", null) != null)
            {
                SOsL.Text = Preferences.Get("SOsL", " ");
            }
            if (Preferences.Get("SOsR", null) != null)
            {
                SOsR.Text = Preferences.Get("SOsR", " ");
            }


            if (Preferences.Get("SVGDL", null) != null)
            {
                SVGDL.Text = Preferences.Get("SVGDL", " ") + " мм. рт. ст.";
            }
            if (Preferences.Get("SVGDR", null) != null)
            {
                SVGDR.Text = Preferences.Get("SVGDR", " ") + " мм. рт. ст.";
            }




            if (Preferences.Get("ShpL", null) != null)
            {
                ShpL.Text = Preferences.Get("ShpL", " ");
            }
            if (Preferences.Get("ShpR", null) != null)
            {
                ShpR.Text = Preferences.Get("ShpR", " ");
            }
            if (Preferences.Get("CylL", null) != null)
            {
                CylL.Text = Preferences.Get("CylL", " ");
            }
            if (Preferences.Get("CylR", null) != null)
            {
                CylR.Text = Preferences.Get("CylR", " ");
            }
            if (Preferences.Get("AxL", null) != null)
            {
                AxL.Text = Preferences.Get("AxL", " ");
            }
            if (Preferences.Get("ShpR", null) != null)
            {
                AxR.Text = Preferences.Get("AxR", " ");
            }


            if (Preferences.Get("OsL", null) != null)
            {
                OsL.Text = Preferences.Get("OsL", " ");
            }
            if (Preferences.Get("OsR", null) != null)
            {
                OsR.Text = Preferences.Get("OsR", " ");
            }


            if (Preferences.Get("VGDL", null) != null)
            {
                VGDL.Text = Preferences.Get("VGDL", " ") + " мм. рт. ст.";
            }
            if (Preferences.Get("VGDR", null) != null)
            {
                VGDR.Text = Preferences.Get("VGDR", " ")+ " мм. рт. ст.";
            }



        }
    }
}