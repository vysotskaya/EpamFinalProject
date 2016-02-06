using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.InterfaceServices
{
    public interface ILotRequestService
    {
        IEnumerable<LotRequestEntity> GetAllLotRequestEntities();
        LotRequestEntity GetLotRequestEntity(int id);
        void CreateLotRequest(LotRequestEntity entity);
        void UpdateLotRequest(LotRequestEntity entity);
        void DeleteLotRequest(LotRequestEntity entity);
    }
}
