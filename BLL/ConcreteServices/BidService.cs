using System.Collections.Generic;
using System.Linq;
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
            return _bidRepository.GetAll().Select(b => b.ToBidEntity());
        }

        public BidEntity GetBidEntity(int id)
        {
            return _bidRepository.GetById(id).ToBidEntity();
        }

        public void CreateBid(BidEntity entity)
        {
            _bidRepository.Create(entity.ToDalBid());
            _unitOfWork.Commit();
        }

        public void UpdateBid(BidEntity entity)
        {
            _bidRepository.Update(entity.ToDalBid());
            _unitOfWork.Commit();
        }

        public void DeleteBid(BidEntity entity)
        {
            _bidRepository.Delete(entity.ToDalBid());
            _unitOfWork.Commit();
        }
    }
}
