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
    public class LotImageRepository : ILotImageRepository
    {
        private readonly DbContext _dbContext;

        public LotImageRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DalLotImage> GetAll()
        {
            return _dbContext.Set<LotImage>().Select(lotImage => new DalLotImage()
            {
                Id = lotImage.ImageId,
                LotRefId = lotImage.LotRefId,
                Content = lotImage.Content
            });
        }

        public DalLotImage GetById(int key)
        {
            var lotImage = _dbContext.Set<LotImage>().FirstOrDefault(i => i.ImageId == key);
            return lotImage?.ToDalLotImage();
        }

        public DalLotImage GetByPredicate(Expression<Func<DalLotImage, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(DalLotImage entity)
        {
            var lotImage = entity.ToLotImage();
            _dbContext.Set<LotImage>().Add(lotImage);
        }

        public void Update(DalLotImage entity)
        {
            var lotImage = entity.ToLotImage();
            var existedLotImage = _dbContext.Entry<LotImage>(_dbContext.Set<LotImage>().Find(lotImage.ImageId));
            if (existedLotImage == null)
            {
                return;
            }
            existedLotImage.State = EntityState.Modified;
        }

        public void Delete(DalLotImage entity)
        {
            var img = _dbContext.Set<LotImage>().FirstOrDefault(t => t.ImageId == entity.Id);
            if (img == null)
            {
                return;
            }
            //var lotImage = entity.ToLotImage();
            _dbContext.Set<LotImage>().Remove(img);
        }
    }
}
