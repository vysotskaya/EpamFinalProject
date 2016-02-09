using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static partial class BllEntityMapper
    {
        public static DalLotImage ToDalLotImage(this LotImageEntity lotImageEntity)
        {
            return new DalLotImage()
            {
                Id = lotImageEntity.Id,
                LotRefId = lotImageEntity.LotRefId,
                Content = lotImageEntity.Content.ImageToByteArray()
            };
        }

        public static LotImageEntity ToLotImageEntity(this DalLotImage dalLotImage)
        {
            return new LotImageEntity()
            {
                Id = dalLotImage.Id,
                LotRefId = dalLotImage.LotRefId,
                Content = dalLotImage.Content.ByteArrayToImage()
            };
        }
    }
}
