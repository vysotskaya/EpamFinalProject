using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ORM;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repositories;
using DAL.Mappers;

namespace DAL.Concrete
{
    public class BidRepository : IBidRepository
    {
        private readonly DbContext _dbContext;

        public BidRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DalBid> GetAll()
        {
            return _dbContext.Set<Bid>().Select(bid => new DalBid()
            {
                Id = bid.BidId,
                UserRefId = bid.UserRefId,
                LotRefId = bid.LotRefId,
                LotName = bid.Lot.LotName,
                UserLogin = bid.User.Login
            });
        }

        public DalBid GetById(int key)
        {
            var bid = _dbContext.Set<Bid>().FirstOrDefault(b => b.BidId == key);
            return bid.ToDalBid();
        }

        public DalBid GetByPredicate(Expression<Func<DalBid, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(DalBid entity)
        {
            var bid = entity.ToBid();
            _dbContext.Set<Bid>().Add(bid);
        }

        public void Update(DalBid entity)
        {
            var updatedBid = entity.ToBid();
            var existedBid = _dbContext.Entry<Bid>(_dbContext.Set<Bid>().Find(updatedBid.BidId));
            existedBid.State = EntityState.Modified;
            //field to update
        }

        public void Delete(DalBid entity)
        {
            var bid = entity.ToBid();
            _dbContext.Set<Bid>().Remove(bid);
        }
    }
}
