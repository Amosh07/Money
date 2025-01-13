
using DataModel.Model;

namespace Money.Components.Pages
{
    public partial class Dashboard

    {
        #region OnInitilized 
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
        #endregion

        #region TopFiveHighestTransactions
        private List<Transactions> Transactions { get; set; } = [];

        private async Task HighestFive() 
        {
            var response = await TransactionInterface.HighestTransaction();

            if (response is null) 
            {
                return;
            }
            Transactions = response;
        }
        #endregion
    }
}