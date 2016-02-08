using System;
using System.Collections.Generic;
using System.Linq;
using AuctionLog;
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
            try
            {
                return _requestRepository.GetAll().Select(r => r.ToRequestEntity());
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return new List<RequestEntity>();
            }
        }

        public RequestEntity GetRequestEntity(int id)
        {
            try
            {
                return _requestRepository.GetById(id).ToRequestEntity();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return null;
            }
        }

        public void CreateRequest(RequestEntity entity)
        {
            try
            {
                _requestRepository.Create(entity.ToDalRequest());
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }

        public void UpdateRequest(RequestEntity entity)
        {
            try
            {
                _requestRepository.Update(entity.ToDalRequest());
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }

        public void DeleteRequest(RequestEntity entity)
        {
            try
            {
                _requestRepository.Delete(entity.ToDalRequest());
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }
    }
}
