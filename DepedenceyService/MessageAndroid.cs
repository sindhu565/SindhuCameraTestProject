using System;
using Android.App;
using Android.Widget;
using Camera2Test.DependceyService;
using Camera2Test.Droid.DepedenceyService;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace Camera2Test.Droid.DepedenceyService
{
	public class MessageAndroid: IToastMessage
    {
		
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();

        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();

        }
    }
}

