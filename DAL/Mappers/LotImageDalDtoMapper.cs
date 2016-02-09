using DAL.Interface.DTO;
using ORM;

namespace DAL.Mappers
{
    public static partial class DalDtoMapper
    {
        public static DalLotImage ToDalLotImage(this LotImage lotImage)
        {
            return new DalLotImage()
            {
                Id = lotImage.ImageId,
                Content = lotImage.Content,
                LotRefId = lotImage.LotRefId
            };
        }

        public static LotImage ToLotImage(this DalLotImage dalLotImage)
        {
            return new LotImage()
            {
                LotRefId = dalLotImage.LotRefId,
                Content = dalLotImage.Content,
                ImageId = dalLotImage.Id
            };
        }
    }
}
