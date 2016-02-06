using DAL.Interface.DTO;
using DAL.Interface.Repository;

namespace DAL.Interface.Repositories
{
    public interface ILotRepository : IRepository<DalLot>
    {
        int LoadEntityID { get; set; }
    }
}
