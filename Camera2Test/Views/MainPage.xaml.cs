using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Camera2Test.DependceyService;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Camera2Test
{
    public partial class MainPage : ContentPage
    {
        string filePath;
        public event Action<string> PictureFinished;
        public MainPage()
        {
            InitializeComponent();
        }
        async protected override void OnAppearing()
        {
            base.OnAppearing();
            PictureFinished -= OnPictureFinished;
            PictureFinished += OnPictureFinished;
            await GetCameraPermission();
            await GetStorageReadPermission();
            await GetStorageWritePermission();

        }

        async Task<bool> GetCameraPermission()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                    //if (await Permissions.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    //{
                    //    var result = await DisplayAlert("Camera access needed", "App needs Camera access enabled to work.", "ENABLE", "CANCEL");

                    //    if (!result)
                    //        return false;
                    //}

                    status = await Permissions.RequestAsync<Permissions.Camera>();
                }

                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else
                {
                    await DisplayAlert("Could not access Camera", "App needs Camera access to work. Go to Settings >> App to enable Camera access ", "GOT IT");
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        async Task<bool> GetStorageReadPermission()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (status != PermissionStatus.Granted)
                {

                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                }

                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else
                {
                    await DisplayAlert("Could not access Camera", "App needs Camera access to work. Go to Settings >> App to enable Camera access ", "GOT IT");
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        async Task<bool> GetStorageWritePermission()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted)
                {

                    status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }

                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else
                {
                    await DisplayAlert("Could not access Camera", "App needs Camera access to work. Go to Settings >> App to enable Camera access ", "GOT IT");
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        async void TakeButton_Clicked(System.Object sender, System.EventArgs e)
        {

            var actionSheet = await DisplayActionSheet("Select Option", "Cancel", null, "Camera", "Gallery");

            switch (actionSheet)
            {
                case "Cancel":

                    // Do Something when 'Cancel' Button is pressed

                    break;

                case "Camera":
                    bool hasCameraPermission = await GetCameraPermission();

                    if (hasCameraPermission)
                    {
                        await Navigation.PushModalAsync(new CameraPage(PictureFinished));
                    }

                    break;

                case "Gallery":

                    var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                    {
                        Title = "Please pick a selfie"
                    });
                    filePath = result.FullPath;
                    image.Source = result.FullPath;
                    break;


            }
        }

      async  void SahreButton_Clicked(System.Object sender, System.EventArgs e)
        {

            string fileType = "image/jpeg";
            ShareFile fileToShare = new ShareFile(filePath, fileType);
            ShareFileRequest shareRequest = new ShareFileRequest(fileToShare);
            await Share.RequestAsync(shareRequest);
        }

        private void OnPictureFinished(string filePath)
        {
            image.Source = filePath;

        }
      public  async void Toast_Clicked(System.Object sender, System.EventArgs e)
        {
            DependencyService.Get<IToastMessage>().LongAlert("Hi Dibakar this is long toast message");
        }
        async void Short_Clicked(System.Object sender, System.EventArgs e)
        {

            DependencyService.Get<IToastMessage>().ShortAlert("Hi Dibakar this is short toast message");
        }
        public async void SahreText_Clicked(System.Object sender, System.EventArgs e)
        {
            await ShareText("Hi This is Camera Demo");
        }
        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Demo"
            });
        }
    }
}

