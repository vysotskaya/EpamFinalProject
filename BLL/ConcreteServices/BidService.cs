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
    public class BidService : IBidService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBidRepository _bidRepository;

        public BidService(IUnitOfWork unitOfWork, IBidRepository bidRepository)
        {
            _unitOfWork = unitOfWork;
            _bidRepository = bidRepository;
        }

        public IEnumerable<BidEntity> GetAllBidEntities()
        {
            try
            {
                return _bidRepository.GetAll().Select(b => b.ToBidEntity());
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return new List<BidEntity>();
            }
        }

        public BidEntity GetBidEntity(int id)
        {
            try
            {
                return _bidRepository.GetById(id).ToBidEntity();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return null;
            }
        }

        public void CreateBid(BidEntity entity)
        {
            try
            {
                _bidRepository.Create(entity.ToDalBid());
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }

        public void UpdateBid(BidEntity entity)
        {
            try
            {
                _bidRepository.Update(entity.ToDalBid());
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }

        public void DeleteBid(BidEntity entity)
        {
            try
            {
                _bidRepository.Delete(entity.ToDalBid());
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }
    }
}
