using DataModel.Model;
using DataModel.Model.DTO;

namespace DataAccess.Interface
{
    public interface IDebtInterface
    {
        Task AddDebt(CreatedDebtDto debt);

        List<Debt> GetAllDebt();

        Debt GetById(Guid Id);

        void ActiveDeactive(Guid Id, bool status);

        Task UpdateDebt(UpdateDebtDto debt);

        Task<List<Debt>> RemainingDebt();

    }
}
