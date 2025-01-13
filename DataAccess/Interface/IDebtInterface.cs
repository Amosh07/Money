using DataModel.Model;
using DataModel.Model.DTO;

namespace DataAccess.Interface
{
    public interface IDebtInterface
    {
        Task AddDebt(CreatedDebtDto debt);

        List<Debt> GetAllDebt();

        Debt GetById(Guid Id);

        void ActiveDeactive(Guid Id);

        Task UpdateDebt(UpdateDebtDto debt);
    }
}
