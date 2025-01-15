using DataModel.Model;
using DataModel.Model.DTO;

namespace DataAccess.Interface
{
    public  interface ITagInterface
    {
        Task AddTag(CreatedTagDto tag);

        void ActiveDeactive(Guid Id,bool status);

        List<Tag> GetAllTag();

        Tag TagGetById(Guid Id);

        Task UpdateTag(UpdateTagDto tag);

        List<Tag> GetAllTagUseByOther();

    }
}
 