using Android.Media;
using System;

namespace Camera2Test.Camera2
{
	public class ImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
	{
		public event EventHandler<byte[]> Photo;

		public void OnImageAvailable(ImageReader reader)
		{
			Image image = null;

			try
			{
				image = reader.AcquireLatestImage();
				var buffer = image.GetPlanes()[0].Buffer;
				var imageData = new byte[buffer.Capacity()];
				buffer.Get(imageData);

				Photo?.Invoke(this, imageData);
			}
			catch (Exception ex)
			{
				// ignored
			}
			finally
			{
				image?.Close();
			}
		}
	}
}
