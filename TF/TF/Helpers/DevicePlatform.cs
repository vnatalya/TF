using System;
using System.Collections.Generic;
using System.Text;

namespace TF
{
    public class DevicePlatform
    {
        public int UnitId { get; private set; }
        public string PlatformName { get; private set; }
        public string EntryPointName { get; private set; }

        public static DevicePlatform Windows
        {
            get
            {
                return new DevicePlatform
                {
                    UnitId = 5,
                    PlatformName = "Windows",
                    EntryPointName = "App.xaml.cs"
                };
            }
        }

        public static DevicePlatform Android
        {
            get
            {
                return new DevicePlatform
                {
                    UnitId = 2,
                    PlatformName = "Android",
                    EntryPointName = "Application.cs"
                };
            }
        }

        public static DevicePlatform iOS
        {
            get
            {
                return new DevicePlatform
                {
                    UnitId = 3,
                    PlatformName = "iOS",
                    EntryPointName = "AppDelegate.cs"
                };
            }
        }
    }
}
