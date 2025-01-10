﻿namespace DataModel.Model.DTO
{
    public class CreatedDebtDto
    {
        public Guid Id { get; set; }

        public string DebtSource { get; set; }

        public decimal DebtAmount { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsCleard { get; set; }

        public bool IsActive { get; set; }

        public Guid TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
