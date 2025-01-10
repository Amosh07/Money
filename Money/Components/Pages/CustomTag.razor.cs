using DataModel.Model;
using DataModel.Model.DTO;

namespace Money.Components.Pages
{
    public partial class CustomTag
    {
        private List<Tag>? Tags { get; set; }

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

        #region UpdateTag
        private async Task OpenUpdateTagModal(Guid TagId)
        {
            var response = TagInterface.TagGetById(TagId);

            if (response is null)
            {
                // SnackbarService.ShowSnackbar(response.Message?? Constant.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
            }
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

            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }
        #endregion
    }
}