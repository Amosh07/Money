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

        #region Update Transactions
        private bool IsUpdateModalOpen { get; set; }

        private UpdateTransactionDto UpdateTransactionDto { get; set; } = new();

        private Transactions GetTransactionDto { get; set; } = new();

        private bool IsDebtButtonDisabled =>
            string.IsNullOrEmpty(UpdateTransactionDto.Title) ||
            string.IsNullOrEmpty(UpdateTransactionDto.TransactionDate.ToString()) ||
            string.IsNullOrEmpty(UpdateTransactionDto.TransactionAmount.ToString()) ||
            string.IsNullOrEmpty(UpdateTransactionDto.TransactionTypes.ToString()) ||
            string.IsNullOrEmpty(UpdateTransactionDto.Remarks.ToString()) ||
            string.IsNullOrEmpty(UpdateTransactionDto.Tag?.TagName);

        private async Task OpenUpdateModal(Guid transactionId)
        {
            var response = TransactionInterface.GetById(transactionId);

            if (response is null)
            {
                return;
            }

            GetTransactionDto = response;

            UpdateTransactionDto = new UpdateTransactionDto()
            {
                Id = GetTransactionDto.Id,
                Title = GetTransactionDto.Title,
                TransactionDate = GetTransactionDto.TransactionDate,
                TransactionAmount = GetTransactionDto.TransactionAmount,
                TransactionTypes = GetTransactionDto.TransactionTypes,
                Remarks = GetTransactionDto.Remarks,
                Tag = GetTransactionDto.Tag
            };

            OpenCloseEditModal();
            StateHasChanged();
        }

        private void OpenCloseEditModal()
        {
            IsUpdateModalOpen = !IsUpdateModalOpen;

            StateHasChanged();
        }

        private async Task UpdateTag(bool isClosed)
        {
            if (isClosed)
            {
                IsUpdateModalOpen = false;
                return;
            }

            try
            {
                var result = TransactionInterface.UpdateTransaction(UpdateTransactionDto);

                if (result is null)
                {
                    //SnackbarService.ShowSnackbar(result?.Message ?? Constants.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
                    return;
                }
                IsUpdateModalOpen = false;

                StateHasChanged();

            }
            catch (Exception ex)
            {
                // SnackbarService.ShowSnackbar(ex.Message, Severity.Error, Variant.Outlined);
            }
        }
        #endregion

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

        private CreatedTransactionDto? createTransaction { get; set; } = new();

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

                IsCreateModalOpen = false;

                StateHasChanged();

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
            var response = TagInterface.GetAllTagUseByOther();

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

        private async Task TransactionDelete(bool isActive)
        {
            try
            {
                TransactionInterface.ActiveDeactive(DeleteTransaction.Id, isActive);

                IsDeleteModalOpen = false;
            }
            catch (Exception ex)
            {
                {
                    //SnackbarService.ShowSnackbar(ex.Message, Severity.Error, Variant.Outlined);
                }
            }
            #endregion
        }
    }
}
