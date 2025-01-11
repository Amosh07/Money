using DataAccess.Interface;
using DataModel.Model;
using DataModel.Model.DTO;

namespace Money.Components.Pages
{
    public partial class AddTransaction
    {
        private List<Transactions>? transactions { get; set; }

        #region OnInitialize
        protected override async Task OnInitializedAsync()
        {
            await GetAllTransaction();
            await GetAllTags();
        }
        #endregion

        private async Task OpenUpdateDebtModal(Guid TagId)
        {
            var response = TransactionInterface.GetById(TagId);

            if (response is null)
            {
                // SnackbarService.ShowSnackbar(response.Message?? Constant.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
            }
        }

        #region GetAllTransaction
        private async Task GetAllTransaction()
        {
            var response = TransactionInterface.GetAllTransaction();

            if (response == null)
            {
                return;
            }

            transactions = response;
        }
        #endregion

        #region Add Transaction
        private bool IsCreateButtonDisabled =>
        string.IsNullOrEmpty(createTransaction.Title) ||
        string.IsNullOrEmpty(createTransaction.TransactionDate.ToString()) ||
        string.IsNullOrEmpty(createTransaction.TransactionType.ToString()) ||
        string.IsNullOrEmpty(createTransaction.Remarks);

        private bool IsCreateModalOpen { get; set; }

        private CreatedTransactionDto createTransaction { get; set; } = new();

        private void OpenTransactionRegister()
        {
            IsCreateModalOpen = true;
            createTransaction = new CreatedTransactionDto();
            StateHasChanged();
        }

        private async Task AddRegisterTransaction(bool isclosed)
        {
            if (isclosed)
            {
                IsCreateModalOpen = false;
                return;
            }

            try
            {
                var result = TransactionInterface.AddTransaction(createTransaction);

                if (result is null)
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }
        #endregion

        #region GetAll Tags
        private List<Tag>? Tags { get; set; }
        private async Task GetAllTags()
        {
            var response = TagInterface.GetAllTag();

            if (response is null)
            {
                return;
            }

            Tags = response;

            StateHasChanged();
        }
        #endregion

        #region Delete
        private bool IsDeleteModalOpen { get; set; }

        private Transactions DeleteTransaction { get; set; } = new();

        private async Task OpenDebtDeleteModal(Guid Id)
        {
            var response = TransactionInterface.GetById(Id);

            if (response is null)
            {
                // SnackbarService.ShowSnackbar(response?.Message ?? Constants.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
                return;
            }

            DeleteTransaction = response;

            IsDeleteModalOpen = true;

            StateHasChanged();
        }

        private async Task DeleteTag(bool isClosed)
        {
            if (isClosed)
            {
                IsDeleteModalOpen = false;
                return;
            }

            try
            {
                TransactionInterface.ActiveDeactive(DeleteTransaction.Id);

                IsDeleteModalOpen = false;
            }
            catch (Exception ex)
            {
                //SnackbarService.ShowSnackbar(ex.Message, Severity.Error, Variant.Outlined);
            }
        }
        #endregion
    }
}
