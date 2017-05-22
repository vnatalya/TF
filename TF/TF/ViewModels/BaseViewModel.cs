using System;
using TF.Helpers;
using TF.Models;
using TF.Services;

using Xamarin.Forms;

namespace TF.ViewModels
{
	public abstract class BaseViewModel : ObservableObject
	{

		bool isBusy = false;
		public bool IsBusy {
			get { return isBusy; }
			set { SetProperty (ref isBusy, value); }
		}

		public void Initialize ()
		{
			if (devicePlatform == null) {
#if __ANDROID__
                devicePlatform = DevicePlatform.Android;
#else
				devicePlatform = DevicePlatform.iOS;
#endif
			}

		}

		static DevicePlatform devicePlatform;

		private static DataBaseService dbService;
		public static DataBaseService DBService {
			get {
				if (dbService == null)
					throw new NullReferenceException (string.Format ("ViewModelBase.DBService has not been set. Derive a class from Shared.Services.DBServiceBase, override Init() method and init mentioned reference in {0}", devicePlatform.EntryPointName));

				return dbService;
			}
			set {
				dbService = value;
			}
		}
	}
}