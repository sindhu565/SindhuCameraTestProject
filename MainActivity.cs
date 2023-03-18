using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Camera2Test.Droid.DepedenceyService;
using System.Runtime.Remoting.Messaging;
using Xamarin.Forms;
using Camera2Test.DependceyService;
using System.Collections.Generic;
using Plugin.FirebasePushNotification;

namespace Camera2Test.Droid
{
    [Activity(Label = "Camera2Test", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            LoadApplication(new App());
           
            DependencyService.Register<IToastMessage, MessageAndroid>();
            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
