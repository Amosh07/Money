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
            try 
            {
                var tag = _tag.FirstOrDefault(t => t.Id == Id);

                if (tag != null)
                {
                    tag.IsActive = status;
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("not found", ex);
            }
           
        }

        public async Task AddTag(CreatedTagDto tag)
        {
            try
            {
                var tagmodel = new Tag()
                {
                    Id = new Guid(),
                    IsActive = true,
                    TagName = tag.TagName
                };
                _tag.Add(tagmodel);
                SaveData(_tag);
            }catch (Exception ex) 
            {
                throw new Exception("something is wrong", ex);
            }
        }

        public List<Tag> GetAllTag()
        {
            return _tag.ToList();
        }

        public Tag TagGetById(Guid Id)
        {
            return _tag.FirstOrDefault(t => t.Id == Id);
        }
    }
}
