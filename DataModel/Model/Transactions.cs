using DataModel.BaseEntity;
using DataModel.Common.Base;

namespace DataModel.Model
{
    public class Transactions: BaseEntity<Guid>
    {
        public string Title { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public TransactionTypes TransactionTypes { get; set; }

        public DefultTags Tags { get; set; }

        public string Notes { get; set; }
    }
}
