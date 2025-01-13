using DataModel.Common.Base;

namespace DataModel.Model.DTO
{
    public class CreatedTransactionDto
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public decimal TransactionAmount { get; set; }

        public DateTime? TransactionDate { get; set; }

        public int TransactionType { get; set; }

        public string Remarks { get; set; }

        public Guid TagId { get; set; }

        public Tag Tage { get; set; }
    }
}
