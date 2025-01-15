namespace Money.Components.Layout
{
    public partial class NavMenu
    {
        private bool IslogOut { get; set; } = false;

        private async void ShowLogoutConfirmation()
        {
            IslogOut = true;
        }

        private async void HideLogoutConfirmation()
        {
            IslogOut = false;
        }

        #region Logout
        private async void Logout()
        {
            Nav.NavigateTo("/login");
        }
        #endregion
    }
}