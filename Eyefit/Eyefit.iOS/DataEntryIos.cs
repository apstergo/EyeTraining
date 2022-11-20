using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Eyefit;
using Eyefit.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DataEntry), typeof(DataEntryIos))]
namespace Eyefit.iOS
{
    public class DataEntryIos: EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
			if (e.OldElement != null || Element == null)
			{
				return;
			}

			
			Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;

			if ((this.Element.Keyboard == Keyboard.Numeric) || (this.Element.Keyboard == Keyboard.Url))
			{
				this.AddDoneButton();
			}

		}
		protected void AddDoneButton()
		{
			var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

			var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
			{
				this.Control.ResignFirstResponder();
				var baseEntry = this.Element.GetType();
				((IEntryController)Element).SendCompleted();
			});

			toolbar.Items = new UIBarButtonItem[] {
				new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
				doneButton
			};
			this.Control.InputAccessoryView = toolbar;
		}
	}
}