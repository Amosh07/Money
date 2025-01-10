using DataAccess.Interface;
using DataModel.Abstraction;
using DataModel.Model;
using DataModel.Model.DTO;

namespace DataAccess.Services
{
    public class DebtService : UserBase<Debt>, IDebtInterface
    {
        private List<Debt> _debtList;
        public DebtService() : base("Debt.json")
        {
            _debtList = LoadData();
        }

        public void ActiveDeactive(Guid Id)
        {
            var tag = _debtList.FirstOrDefault(t => t.Id == Id);

            if (tag != null)
            {
                tag.IsActive = false;
            }
        }

        public async Task AddDebt(CreatedDebtDto debt)
        {
            try
            {
                var debtModel = new Debt()
                {
                    Id = Guid.NewGuid(),
                    DebtSource = debt.DebtSource,
                    DebtAmount = debt.DebtAmount,
                    DebtDate = DateTime.Now,
                    IsActive = true,
                    IsCleard = false,
                    DueDate = debt.DueDate,
                };

            }
            catch (Exception ex)
            {
                throw new Exception("some this is wrong");
            }
        }

        public List<Debt> GetAllDebt()
        {
            return _debtList.ToList();
        }

        public Debt GetById(Guid id)
        {
            return _debtList.FirstOrDefault(d => d.Id == id);
        }
    }
}
