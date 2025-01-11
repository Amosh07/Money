using DataModel.BaseEntity;
using DataModel.Common.Base;

namespace DataModel.Model
{
    public class Transactions: BaseEntity<Guid>
    {
       public Guid Id { get; set; }

        public string? Title { get; set; }

        public decimal TransactionAmount {  get; set; }

        public DateTime? TransactionDate { get; set; }

        public TransactionTypes TransactionTypes { get; set; }

        public bool IsActive { get; set; }

        public string Remarks { get; set; }

        public Guid TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
