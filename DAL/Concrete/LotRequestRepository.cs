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
    public class LotRequestRepository : ILotRequestRepository
    {
        private readonly DbContext _dbContext;

        public LotRequestRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DalLotRequest> GetAll()
        {
            return _dbContext.Set<LotRequest>().Select(lotRequest => new DalLotRequest()
            {
                Id = lotRequest.RequestId,
                CategoryName = lotRequest.Category.CategoryName,
                CategoryRefId = lotRequest.CategoryRefId,
                ToConfirm = lotRequest.ToConfirm,
                LotName = lotRequest.Lot.LotName,
                LotRefId = lotRequest.LotRefId
            });
        }

        public DalLotRequest GetById(int key)
        {
            var lotRequest = _dbContext.Set<LotRequest>().FirstOrDefault(r => r.RequestId == key);
            return lotRequest?.ToDalLotRequest();
        }

        public DalLotRequest GetByPredicate(Expression<Func<DalLotRequest, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(DalLotRequest entity)
        {
            var lotRquest = entity.ToLotRequest();
            _dbContext.Set<LotRequest>().Add(lotRquest);
        }

        public void Update(DalLotRequest entity)
        {
            var updatedLotRequest = entity.ToLotRequest();
            var existedLotRequest = _dbContext.Entry<LotRequest>(_dbContext.Set<LotRequest>().Find(updatedLotRequest.RequestId));
            if (existedLotRequest == null)
            {
                return;
            }
            existedLotRequest.State = EntityState.Modified;
        }

        public void Delete(DalLotRequest entity)
        {
            var lotRequest = entity.ToLotRequest();
            _dbContext.Set<LotRequest>().Remove(lotRequest);
        }
    }
}
