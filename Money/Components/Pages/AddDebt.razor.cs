using DataAccess.Interface;
using DataModel.Model;
using DataModel.Model.DTO;

namespace Money.Components.Pages
{
    public partial class AddDebt
    {
        private List<Debt>? debts { get; set; }

        #region Oninitialize
        protected override async Task OnInitializedAsync()
        {
            await GetAllDebt();
            await GetAllTags();
        }
        #endregion
        private async Task OpenUpdateDebtModal(Guid TagId)
        {
            var response = DebtInterface.GetById(TagId);

            if (response is null)
            {
                // SnackbarService.ShowSnackbar(response.Message?? Constant.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
            }
        }

        #region GetAllDebt
        private async Task GetAllDebt()
        {
            var response = DebtInterface.GetAllDebt();

            if (response is null)
            {
                return;
            }

            debts = response;
        }
        #endregion

        #region AddDebt
        private bool IsCreateButtonDisabled =>
        string.IsNullOrEmpty(CreateDebtDto.DebtSource) ||
        string.IsNullOrEmpty(CreateDebtDto.DueDate.ToString()) ||
        string.IsNullOrEmpty(CreateDebtDto.DebtAmount.ToString());


        private CreatedDebtDto CreateDebtDto { get; set; } = new();
        private bool IsCreateModalOpen { get; set; }
        private void OpenDebtRegister()
        {
            IsCreateModalOpen = true;
            CreateDebtDto = new CreatedDebtDto();
            StateHasChanged();
        }

        private async Task AddRegisterDebt(bool isclosed)
        {
            if (isclosed)
            {
                IsCreateModalOpen = false;
                return;
            }

            try
            {
                var result = DebtInterface.AddDebt(CreateDebtDto);

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

        #region GetAllTags
        private List<Tag>? Tags { get; set; }
        private async Task GetAllTags()
        {
            var response = TagInterface.GetAllTag();
            if (response == null)
            {
                Console.WriteLine("not found");
                return;
            }
            Tags = response;
        }
        #endregion

        #region Delete
        private bool IsDeleteModalOpen { get; set; }

        private Debt DeleteDebt { get; set; } = new();

        private async Task OpenDebtDeleteModal(Guid Id)
        {
            var response = DebtInterface.GetById(Id);

            if (response is null)
            {
                // SnackbarService.ShowSnackbar(response?.Message ?? Constants.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
                return;
            }

            DeleteDebt = response;

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
                DebtInterface.ActiveDeactive(DeleteDebt.Id);

                IsDeleteModalOpen = false;
            }
            catch (Exception ex)
            {
                //SnackbarService.ShowSnackbar(ex.Message, Severity.Error, Variant.Outlined);
            }
        }
        #endregion

        #region Update Debts
        private bool IsUpdateModalOpen { get; set; }

        private UpdateDebtDto UpdateDebtDto { get; set; } = new();

        private Debt GetDebtDto { get; set; } = new();

        private bool IsDebtButtonDisabled =>
            string.IsNullOrEmpty(UpdateDebtDto.DebtSource) ||
            string.IsNullOrEmpty(UpdateDebtDto.DueDate.ToString()) ||
            string.IsNullOrEmpty(UpdateDebtDto.DebtAmount.ToString()) ||
            string.IsNullOrEmpty(UpdateDebtDto.Tag?.TagName);

        private async Task OpenUpdateModal(Guid debtId)
        {
            var response = DebtInterface.GetById(debtId);

            if (response is null)
            {
                return;
            }

            GetDebtDto = response;

            UpdateDebtDto = new UpdateDebtDto()
            {
                Id = GetDebtDto.Id,
                DebtSource = GetDebtDto.DebtSource,
                DebtDate = GetDebtDto.DebtDate,
                DebtAmount = GetDebtDto.DebtAmount
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
                var result = DebtInterface.UpdateDebt(UpdateDebtDto);

                if (result is null)
                {
                    //SnackbarService.ShowSnackbar(result?.Message ?? Constants.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
                    return;
                }
            }
            catch (Exception ex)
            {
                // SnackbarService.ShowSnackbar(ex.Message, Severity.Error, Variant.Outlined);
            }
        }
        #endregion
    }
}
