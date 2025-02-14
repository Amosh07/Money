﻿using DataModel.BaseEntity;

namespace DataModel.Model
{
    public class Debt : BaseEntity<Guid>
    {
        public string DebtSource { get; set; }

        public decimal DebtAmount { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsCleard { get; set; }

        public bool IsActive { get; set; }

        public DateTime DebtDate { get; set; }

        public Guid TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
