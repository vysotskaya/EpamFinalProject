using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;

namespace DAL.Interface.Repositories
{
    public interface ILotRepository : IRepository<DalLot>
    {
        IEnumerable<DalLot> GetByPredicateMany(Expression<Func<DalLot, bool>> expression);
    }
}
