        private static string GetResourceId()
        {
//{[{
            string resourceName = null;
            switch(Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.Android:
                    resourceName = "Param_ProjectName.Droid.Strings.AppResources";
                    break;
                case Xamarin.Forms.Device.iOS:
                    resourceName = "Param_ProjectName.iOS.Strings.AppResources";
                    break;
                case Xamarin.Forms.Device.UWP:
                    resourceName = "Param_ProjectName.UWP.Strings.AppResources";
                    break;
            }
//}]}
            return resourceName;
        }
