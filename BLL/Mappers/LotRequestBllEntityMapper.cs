using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static partial class BllEntityMapper
    {
        public static DalLotRequest ToDalLotRequest(this LotRequestEntity lotRequestEntity)
        {
            return new DalLotRequest()
            {
                Id = lotRequestEntity.Id,
                CategoryName = lotRequestEntity.CategoryName,
                CategoryRefId = lotRequestEntity.CategoryRefId,
                ToConfirm = lotRequestEntity.ToConfirm,
                LotName = lotRequestEntity.LotName,
                LotRefId = lotRequestEntity.LotRefId
            };
        }

        public static LotRequestEntity ToLotRequestEntity(this DalLotRequest dalLotRequest)
        {
            return new LotRequestEntity()
            {
                Id = dalLotRequest.Id,
                CategoryName = dalLotRequest.CategoryName,
                CategoryRefId = dalLotRequest.CategoryRefId,
                ToConfirm = dalLotRequest.ToConfirm,
                LotName = dalLotRequest.LotName,
                LotRefId = dalLotRequest.LotRefId
            };
        }
    }
}
