using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using BLL.Mappers;
using DAL.Interface.Repositories;
using DAL.Interface.Repository;

namespace BLL.ConcreteServices
{
    public class LotService :ILotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILotRepository _lotRepository;

        public LotService(IUnitOfWork unitOfWork, ILotRepository lotRepository)
        {
            _unitOfWork = unitOfWork;
            _lotRepository = lotRepository;
        }

        public IEnumerable<LotEntity> GetAllLotEntities()
        {
            return _lotRepository.GetAll().Select(l => l.ToLotEntity());
        }

        public LotEntity GetLotEntity(int id)
        {
            return _lotRepository.GetById(id).ToLotEntity();
        }

        public int CreateLot(LotEntity entity)
        {
            var dalLot = entity.ToDalLot();
            _lotRepository.Create(dalLot);
            _unitOfWork.Commit();
            return _lotRepository.LoadEntityID;
        }

        public void UpdateLot(LotEntity entity)
        {
            _lotRepository.Update(entity.ToDalLot());
            _unitOfWork.Commit();
        }

        public void DeleteLot(LotEntity entity)
        {
            _lotRepository.Delete(entity.ToDalLot());
            _unitOfWork.Commit();
        }
    }
}
