namespace Peppa.ViewModels.Interface
{
    public interface IToastViewModel
    {
        void ShowToast();

        void SaveLastTimeShow();

        bool CanShowToast { get; }
    }
}
