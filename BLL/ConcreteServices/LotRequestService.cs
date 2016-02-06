using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using BLL.Mappers;
using DAL.Interface.Repositories;
using DAL.Interface.Repository;

namespace BLL.ConcreteServices
{
    public class LotRequestService : ILotRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILotRequestRepository _lotRequestRepository;

        public LotRequestService(IUnitOfWork unitOfWork, ILotRequestRepository lotRequestRepository)
        {
            _unitOfWork = unitOfWork;
            _lotRequestRepository = lotRequestRepository;
        }

        public IEnumerable<LotRequestEntity> GetAllLotRequestEntities()
        {
            return _lotRequestRepository.GetAll().Select(r => r.ToLotRequestEntity());
        }

        public LotRequestEntity GetLotRequestEntity(int id)
        {
            return _lotRequestRepository.GetById(id).ToLotRequestEntity();
        }

        public void CreateLotRequest(LotRequestEntity entity)
        {
            _lotRequestRepository.Create(entity.ToDalLotRequest());
            _unitOfWork.Commit();
        }

        public void UpdateLotRequest(LotRequestEntity entity)
        {
            _lotRequestRepository.Update(entity.ToDalLotRequest());
            _unitOfWork.Commit();
        }

        public void DeleteLotRequest(LotRequestEntity entity)
        {
            _lotRequestRepository.Delete(entity.ToDalLotRequest());
            _unitOfWork.Commit();
        }
    }
}
