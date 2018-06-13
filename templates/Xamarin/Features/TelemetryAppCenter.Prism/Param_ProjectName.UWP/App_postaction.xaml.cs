using Windows.UI.Xaml;
//^^
//{[{
using Microsoft.AppCenter.Analytics;
//}]}

namespace Param_RootNamespace.UWP
{
    sealed partial class App : Application
    {
//{[{
        private readonly string AppCenterId = string.Empty; //TODO: Replace with AppCenter ID

//}]}
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
//{[{
            InitialIzeTelemetry();
//}]}
        }
 //{[{
        private void InitialIzeTelemetry()
        {
           if (!string.IsNullOrEmpty(AppCenterId))
            {
                Microsoft.AppCenter.AppCenter.Start(AppCenterId, typeof(Analytics));
            }
        } 
//}]}    
    }
}
