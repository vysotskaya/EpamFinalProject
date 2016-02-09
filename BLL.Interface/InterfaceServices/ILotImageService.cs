using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.InterfaceServices
{
    public interface ILotImageService
    {
        IEnumerable<LotImageEntity> GetAllLotImageEntities();
        LotImageEntity GetLotImageEntity(int id);
        void CreateLotImage(LotImageEntity entity);
        void UpdateLotImage(LotImageEntity entity);
        void DeleteLotImage(LotImageEntity entity);
    }
}
