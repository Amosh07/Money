using DataAccess.Interface;
using DataModel.Abstraction;
using DataModel.Model;
using DataModel.Model.DTO;


namespace DataAccess.Services
{
    public class TagService : UserBase<Tag>, ITagInterface
    {
        private List<Tag> _tag;
        public TagService() : base("Tag.json") 
        {
            _tag = LoadData();
        }
        public void ActiveDeactive(Guid Id, bool status)
        {
            UpdateItem(t => t.Id == Id, t =>
            {
                t.IsActive = status;
            });
        }

        public async Task AddTag(CreatedTagDto tag)
        {
            var exists = _tag.FirstOrDefault<Tag>(t => t.TagName == tag.TagName);

            if (exists != null)
            {
                throw new Exception("the flowong tag is already exits");
            }

            var tagModel = new Tag()
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                TagName = tag.TagName,
            };

            _tag.Add(tagModel);

            SaveData(_tag);
        }

        public List<Tag> GetAllTag()
        {
            return _tag.ToList();
        }

        public Tag TagGetById(Guid Id)
        {
            return _tag.FirstOrDefault(t => t.Id == Id);
        }

        public async Task UpdateTag(UpdateTagDto updateTagDto)
        {
            UpdateItem(t => t.Id == updateTagDto.Id, t =>
            {
                t.TagName = updateTagDto.TagName;
            });
        }
        public List<Tag> GetAllTagUseByOther()
        {
            return _tag.Where(t => t.IsActive).OrderByDescending(t => t.Id).ToList();
        }

    }
}
