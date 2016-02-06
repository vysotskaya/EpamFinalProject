using DAL.Interface.DTO;
using ORM;

namespace DAL.Mappers
{
    public static partial class DalDtoMapper
    {
        public static DalLotRequest ToDalLotRequest(this LotRequest lotRequest)
        {
            return new DalLotRequest()
            {
                Id = lotRequest.RequestId,
                CategoryName = lotRequest.Category.CategoryName,
                CategoryRefId = lotRequest.CategoryRefId,
                ToConfirm = lotRequest.ToConfirm,
                LotName = lotRequest.Lot.LotName,
                LotRefId = lotRequest.LotRefId
            };
        }

        public static LotRequest ToLotRequest(this DalLotRequest dalLotRequest)
        {
            return new LotRequest()
            {
                LotRefId = dalLotRequest.LotRefId,
                CategoryRefId = dalLotRequest.CategoryRefId,
                RequestId = dalLotRequest.Id,
                ToConfirm = dalLotRequest.ToConfirm
            };
        }
    }
}
