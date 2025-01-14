
using DataModel.Model;

namespace Money.Components.Pages
{
    public partial class Dashboard

    {
        #region OnInitilized 
        protected override async Task OnInitializedAsync()
        {
            await HighestFive();
            await CurrentAmount();
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

        #region Current Balance 

        private decimal CurrentBalance { get; set; }    

         private async Task CurrentAmount()
         {
            var response = await TransactionInterface.CurrentBalance();

            if(response <= 0)
            {
                return;
            }

            CurrentBalance = response;  
         }
        #endregion
    }
}