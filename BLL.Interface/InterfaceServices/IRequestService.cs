using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.InterfaceServices
{
    public interface IRequestService
    {
        IEnumerable<RequestEntity> GetAllRequestEntities();
        RequestEntity GetRequestEntity(int id);
        void CreateRequest(RequestEntity entity);
        void UpdateRequest(RequestEntity entity);
        void DeleteRequest(RequestEntity entity);
    }
}
