using DataAccess.Interface;
using DataModel.Abstraction;
using DataModel.Common.Base;
using DataModel.Model;
using DataModel.Model.DTO;

namespace DataAccess.Services
{
    public class DebtService : UserBase<Debt>, IDebtInterface
    {
        private List<Debt> _debtList;

        private readonly ITransactionsInterface _transactionsInterface;
        public DebtService(ITransactionsInterface transactionsInterface) : base("Debt.json")
        {
            _debtList = LoadData();
            _transactionsInterface = transactionsInterface;
        }

        //public void ActiveDeactive(Guid Id)
        //{
        //    UpdateItem(t => t.Id == Id, t =>
        //    {
        //        t.IsActive = false;
        //    });
        //}

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

                _debtList.Add(debtModel);

                SaveData(_debtList);

                var transaction = new CreatedTransactionDto
                {
                    Title = $"debt added : {debt.DebtSource}",
                    TransactionAmount = debt.DebtAmount,
                    TransactionDate = DateTime.Now,
                    TransactionType = (int)TransactionTypes.debit,
                    TagId = debt.TagId,
                    Remarks = "Add Debts successully"
                };

                await _transactionsInterface.AddTransaction(transaction);



            }
            catch (Exception ex)
            {
                throw new Exception("some this is wrong");
            }
        }

        public List<Debt> GetAllDebt()
        {
            return _debtList.OrderByDescending(t => t.Id).ToList();
        }

        public Debt GetById(Guid Id)
        {
            return _debtList.FirstOrDefault(d => d.Id == Id);
        }

        public async Task<List<Debt>> RemainingDebt()
        {
           var pending = GetAllDebt();

            var debtPending = pending.Where(d => !d.IsCleard);

            return debtPending.OrderBy(t=> t.DueDate).ToList();
        }

        public async Task UpdateDebt(UpdateDebtDto debt)
        {
            UpdateItem(t => t.Id == debt.Id, t =>
            {
                t.DebtSource = debt.DebtSource;
                t.DebtDate = debt.DebtDate;
                t.DebtAmount = debt.DebtAmount;
            });

            var transaction = new UpdateTransactionDto
            {
                Title = $"debt Add : {debt.DebtSource}",
                TransactionAmount = debt.DebtAmount,
                TransactionDate = DateTime.Now,
                TransactionTypes = (int)TransactionTypes.debt,
                Remarks = "Add Debts "
            };

            await _transactionsInterface.UpdateTransaction(transaction);
        }

        public void ActiveDeactive(Guid Id, bool status)
        {
            UpdateItem(t => t.Id == Id, t =>
            {
                t.IsActive = false;
            });
        }
    }
}
