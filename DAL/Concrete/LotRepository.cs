using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repositories;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class LotRepository : ILotRepository
    {
        private readonly DbContext _dbContext;

        public LotRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DalLot> GetAll()
        {
            return _dbContext.Set<Lot>().Select(lot => new DalLot()
            {
                Id = lot.LotId,
                IsBlocked = lot.IsBlocked,
                CategoryName = lot.Category.CategoryName,
                CategoryRefId = lot.CategoryRefId,
                IsConfirm = lot.IsConfirm,
                Discription = lot.Discription,
                BlockReason = lot.BlockReason,
                LotName = lot.LotName,
                SellerRefId = lot.SellerRefId,
                EndDate = lot.EndDate,
                IsSold = lot.IsSold,
                SellerEmail = lot.Seller.Email,
                SellerLogin = lot.Seller.Login,
                StartDate = lot.StartDate,
                StartingBid = lot.StartingBid
            });
        }

        public DalLot GetById(int key)
        {
            var lot = _dbContext.Set<Lot>().FirstOrDefault(l => l.LotId == key);
            return lot?.ToDalLot();
        }

        public DalLot GetByPredicate(Expression<Func<DalLot, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(DalLot entity)
        {
            var lot = entity.ToLot();
            _dbContext.Set<Lot>().Add(lot);
        }

        public void Update(DalLot entity)
        {
            var updatedLot = entity.ToLot();
            var existedLot = _dbContext.Entry<Lot>(_dbContext.Set<Lot>().Find(updatedLot.LotId));
            if (existedLot == null)
            {
                return;
            }
            existedLot.State = EntityState.Modified;
            existedLot.Entity.BlockReason = entity.BlockReason;
            existedLot.Entity.IsBlocked = entity.IsBlocked;
            existedLot.Entity.IsConfirm = entity.IsConfirm;
            existedLot.Entity.IsSold = entity.IsSold;
            existedLot.Entity.CategoryRefId = entity.CategoryRefId;
            existedLot.Entity.LotName = entity.LotName;
            existedLot.Entity.Discription = entity.Discription;
            existedLot.Entity.EndDate = entity.EndDate;
        }

        public void Delete(DalLot entity)
        {
            var lot = entity.ToLot();
            lot = _dbContext.Set<Lot>().Single(l => l.LotId == lot.LotId);
            _dbContext.Set<Lot>().Remove(lot);
        }
    }
}
