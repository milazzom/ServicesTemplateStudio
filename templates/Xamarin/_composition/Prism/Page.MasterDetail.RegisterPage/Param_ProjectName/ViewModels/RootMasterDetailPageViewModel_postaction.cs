namespace Param_RootNamespace.ViewModels
{
    public class RootMasterDetailPageViewModel : ViewModelBase
    {
        public ObservableCollection<MasterDetailPageMenuItem> PrimaryMenuItems { get; private set; } = new ObservableCollection<MasterDetailPageMenuItem>
        {
            //^^
            //{[{
            new MasterDetailPageMenuItem { Title = "wts.ItemName", TargetPage = PageTokens.wts.ItemName, IconSource = "blank.png"},
            //}]}
        };
    }
}
