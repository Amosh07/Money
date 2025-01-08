using DataModel.BaseEntity;

namespace DataModel.Model
{
    public class Tag: BaseEntity<Guid>
    {
        public string TagName { get; set; }
    }
}
