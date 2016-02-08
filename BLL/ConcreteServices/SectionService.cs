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
    public class SectionService : ISectionService
    {
        private readonly IUnitOfWork _unitOfWorkuow;
        private readonly ISectionReposytory _sectionReposytory;

        public SectionService(IUnitOfWork unitOfWork, ISectionReposytory sectionReposytory)
        {
            _unitOfWorkuow = unitOfWork;
            _sectionReposytory = sectionReposytory;
        }
 
        public IEnumerable<SectionEntity> GetAllSectionEntities()
        {
            try
            {
                return _sectionReposytory.GetAll().Select(s => s?.ToBllSection());
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return new List<SectionEntity>();
            }
        }

        public SectionEntity GetSectionEntity(int id)
        {
            try
            {
                return _sectionReposytory.GetById(id)?.ToBllSection();
            }
            catch (Exception e)
            {
                Log.LogError(e);
                return null;
            }
        }

        public void CreateSection(SectionEntity entity)
        {
            try
            {
                _sectionReposytory.Create(entity.ToDalSection());
                _unitOfWorkuow.Commit();
            }
            catch (Exception e)
            {
                Log.LogError(e);
            }
        }

        public void UpdateSection(SectionEntity entity)
        {
            try
            {
                _sectionReposytory.Update(entity.ToDalSection());
                _unitOfWorkuow.Commit();
            }
            catch (Exception e)
            {
                Log.LogError(e);
            }
        }

        public void DeleteSection(SectionEntity entity)
        {
            try
            {
                _sectionReposytory.Delete(entity.ToDalSection());
                _unitOfWorkuow.Commit();
            }
            catch (Exception e)
            {
                Log.LogError(e);
            }
        }
    }
}
