using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using BLL.Mappers;
using DAL.Interface.Repositories;
using DAL.Interface.Repository;

namespace BLL.ConcreteServices
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestRepository _requestRepository;

        public RequestService(IUnitOfWork unitOfWork, IRequestRepository requestRepository)
        {
            _unitOfWork = unitOfWork;
            _requestRepository = requestRepository;
        }

        public IEnumerable<RequestEntity> GetAllRequestEntities()
        {
            return _requestRepository.GetAll().Select(r => r.ToRequestEntity());
        }

        public RequestEntity GetRequestEntity(int id)
        {
            return _requestRepository.GetById(id).ToRequestEntity();
        }

        public void CreateRequest(RequestEntity entity)
        {
            _requestRepository.Create(entity.ToDalRequest());
            _unitOfWork.Commit();
        }

        public void UpdateRequest(RequestEntity entity)
        {
           _requestRepository.Update(entity.ToDalRequest());
            _unitOfWork.Commit();
        }

        public void DeleteRequest(RequestEntity entity)
        {
            _requestRepository.Delete(entity.ToDalRequest());
            _unitOfWork.Commit();
        }
    }
}
