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
            try
            {
                return _lotRepository.GetAll().Select(l => l.ToLotEntity());
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return new List<LotEntity>();
            }
        }

        public LotEntity GetLotEntity(int id)
        {
            try
            {
                return _lotRepository.GetById(id).ToLotEntity();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return null;
            }
        }

        public void CreateLot(LotEntity entity)
        {
            try
            {
                var dalLot = entity.ToDalLot();
                _lotRepository.Create(dalLot);
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }

        public void UpdateLot(LotEntity entity)
        {
            try
            {
                _lotRepository.Update(entity.ToDalLot());
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }

        public void DeleteLot(LotEntity entity)
        {
            try
            {
                _lotRepository.Delete(entity.ToDalLot());
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }
    }
}
