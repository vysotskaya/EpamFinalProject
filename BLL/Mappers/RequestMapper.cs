using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static partial class BllEntityMapper
    {
        public static DalRequest ToDalRequest(this RequestEntity entity)
        {
            return new DalRequest()
            {
                Id = entity.Id,
                SectionName = entity.SectionName,
                SectionRefId = entity.SectionRefId,
                CategoryName = entity.CategoryName,
                CategoryRefId = entity.CategoryRefId,
                IsConfirm = entity.ToConfirm
            };
        }

        public static RequestEntity ToRequestEntity(this DalRequest dalRequest)
        {
            return new RequestEntity()
            {
                Id = dalRequest.Id,
                SectionName = dalRequest.SectionName,
                SectionRefId = dalRequest.SectionRefId,
                CategoryName = dalRequest.CategoryName,
                CategoryRefId = dalRequest.CategoryRefId,
                ToConfirm = dalRequest.IsConfirm
            };
        }
    }
}
