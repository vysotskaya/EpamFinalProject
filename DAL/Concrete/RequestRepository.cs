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
    public class RequestRepository : IRequestRepository
    {
        private readonly DbContext _dbContext;

        public RequestRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DalRequest> GetAll()
        {
            return _dbContext.Set<Request>().Select(request => new DalRequest()
            {
                IsConfirm = request.ToConfirm,
                SectionRefId = request.SectionRefId,
                Id = request.RequestId,
                SectionName = request.Section.SectionName,
                CategoryName = request.Category.CategoryName,
                CategoryRefId = request.CategoryRefId
            });
        }

        public DalRequest GetById(int key)
        {
            var request = _dbContext.Set<Request>().FirstOrDefault(r => r.RequestId == key);
            return request.ToDalRequest();
        }

        public DalRequest GetByPredicate(Expression<Func<DalRequest, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(DalRequest entity)
        {
            var request = entity.ToRequest();
            _dbContext.Set<Request>().Add(request);
        }

        public void Update(DalRequest entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalRequest entity)
        {
            var request = entity.ToRequest();
            request = _dbContext.Set<Request>().Single(r => r.RequestId == request.RequestId);
            _dbContext.Set<Request>().Remove(request);
        }
    }
}
