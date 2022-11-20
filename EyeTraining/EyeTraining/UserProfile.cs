using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Eyefit
{
    public class UserProfile
    {
        public string UserName { get; set; }
        public string NextDate { get; set; }
        public string NextTime { get; set; }
        public int DoneProc { get; set; }
        public int CountNumberProc { get; set; }
        public int CurrentNumberProc { get; set; }
        public double Progress { get; set; }
        public Color ProgressColor { get; set; }
    }
}
