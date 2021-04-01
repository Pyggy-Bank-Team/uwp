namespace Peppa.Interface.WindowsService
{
    public interface IToastService
    {
        void ShowNotification(string header, string description);
    }
}