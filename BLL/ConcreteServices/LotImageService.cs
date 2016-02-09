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
    public class LotImageService : ILotImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILotImageRepository _lotImageRepository;

        public LotImageService(IUnitOfWork unitOfWork, ILotImageRepository lotImageRepository)
        {
            _unitOfWork = unitOfWork;
            _lotImageRepository = lotImageRepository;
        }

        public IEnumerable<LotImageEntity> GetAllLotImageEntities()
        {
            try
            {
                return _lotImageRepository.GetAll().Select(r => r.ToLotImageEntity());
            }
            catch (Exception e)
            {
                Log.LogError(e);
                return new List<LotImageEntity>();
            }
        }

        public LotImageEntity GetLotImageEntity(int id)
        {
            try
            {
                return _lotImageRepository.GetById(id).ToLotImageEntity();
            }
            catch (Exception e)
            {
                Log.LogError(e);
                return null;
            }
        }

        public void CreateLotImage(LotImageEntity entity)
        {
            try
            {
                _lotImageRepository.Create(entity.ToDalLotImage());
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.LogError(e);
            }
        }

        public void UpdateLotImage(LotImageEntity entity)
        {
            try
            {
                _lotImageRepository.Update(entity.ToDalLotImage());
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.LogError(e);
            }
        }

        public void DeleteLotImage(LotImageEntity entity)
        {
            try
            {
                _lotImageRepository.Delete(entity.ToDalLotImage());
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.LogError(e);
            }
        }
    }
}
