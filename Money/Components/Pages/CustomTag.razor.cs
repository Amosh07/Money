using DataModel.Model;
using DataModel.Model.DTO;

namespace Money.Components.Pages
{
    public partial class CustomTag
    {
        private List<Tag>? Tags { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await GetAllTags();
        }

        #region GetAllTags
        private async Task GetAllTags() 
        {
            var response = TagInterface.GetAllTag();
            if (response is null) 
            {
                Console.WriteLine("not found");
                return;
            }
            Tags = response;
            StateHasChanged();
        }
        #endregion

        #region add CustomTag

        private bool IsCreateButtonDisabled =>
        string.IsNullOrEmpty(CreatedTagDto.TagName);

        private bool IsCreatedModal { get; set; }

        private CreatedTagDto CreatedTagDto { get; set; } = new CreatedTagDto();

        private void OpenTagRegister()
        {
            IsCreatedModal = true;
            CreatedTagDto = new CreatedTagDto();
            StateHasChanged();
        }
        private async Task AddRegisterTag(bool isclosed)
        {
            if (isclosed)
            {
                IsCreatedModal = false;
                return;
            }

            try
            {
                var result = TagInterface.AddTag(CreatedTagDto);

                if (result is null)
                {
                    return;
                }

                IsCreatedModal = false;
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }
        #endregion

        #region Update CustomTag
        private bool IsUpdateModalOpen { get; set; }

        private UpdateTagDto UpdateTagDto { get; set; } = new();

        private Tag GetTagDto { get; set; } = new();

        private bool IsTagButtonDisabled =>
            string.IsNullOrEmpty(UpdateTagDto.TagName);

        private async Task OpenUpdateModal(Guid tagId)
        {
            var response = TagInterface.TagGetById(tagId);

            if (response is null)
            {
                return;
            }

            GetTagDto = response;

            UpdateTagDto = new UpdateTagDto()
            {
                Id = GetTagDto.Id,
                TagName = GetTagDto.TagName,
                IsActive = GetTagDto.IsActive
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
                var result = TagInterface.UpdateTag(UpdateTagDto);

                if (result is null)
                {
                    //SnackbarService.ShowSnackbar(result?.Message ?? Constants.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
                    return;
                }
                IsUpdateModalOpen = false;
            }
            catch (Exception ex)
            {
                // SnackbarService.ShowSnackbar(ex.Message, Severity.Error, Variant.Outlined);
            }
        }
        #endregion

        #region Delete
        private bool IsDeleteModalOpen { get; set; }

        private Tag DeleteTags { get; set; } = new();

        private async Task OpenTagDeleteModal(Guid Id)
        {
            var response = TagInterface.TagGetById(Id);

            if (response is null)
            {
                // SnackbarService.ShowSnackbar(response?.Message ?? Constants.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
                return;
            }

            DeleteTags = response;

            IsDeleteModalOpen = true;

            StateHasChanged();
        }

        private async Task DeleteTag(bool isActive)
        {
            try
            {
                TagInterface.ActiveDeactive(DeleteTags.Id, isActive);

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