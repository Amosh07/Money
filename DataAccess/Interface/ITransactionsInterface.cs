using DataModel.Model;
using DataModel.Model.DTO;

namespace DataAccess.Interface
{
    public interface ITransactionsInterface
    {

        Task AddTransaction(CreatedTransactionDto transaction);

        List<Transactions> GetAllTransaction();

        Transactions GetById(Guid Id);

        void ActiveDeactive(Guid Id);

        Task<List<Transactions>> HighestTransaction();

        Task<Decimal> CurrentBalance();

    }
}
