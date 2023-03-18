using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Camera2Test.CustomViews;

namespace Camera2Test
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraPage : ContentPage
	{
        private Action<string> PictureFinished;

        public CameraPage (Action<string> pictureFinished)
		{
			InitializeComponent();
            PictureFinished = pictureFinished;
            CameraPreview.PictureFinished += OnPictureFinished;
        }

        void OnCameraClicked(object sender, EventArgs e)
        {
            CameraPreview.CameraClick.Execute(null);
        }

        private async void OnPictureFinished(string filePath)
        {
            PictureFinished?.Invoke(filePath);
            await Navigation.PopAsync();
        }
    }
}