using System;
namespace Camera2Test.DependceyService
{
	public interface IToastMessage
	{
        void LongAlert(string message);
        void ShortAlert(string message);

    }
}

