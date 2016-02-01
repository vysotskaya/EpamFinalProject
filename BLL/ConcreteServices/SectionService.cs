using System.Collections.Generic;
using System.Linq;
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
            return _sectionReposytory.GetAll().Select(s => s.ToBllSection());
        }

        public SectionEntity GetSectionEntity(int id)
        {
            return _sectionReposytory.GetById(id).ToBllSection();
        }

        public void CreateSection(SectionEntity entity)
        {
            _sectionReposytory.Create(entity.ToDalSection());
            _unitOfWorkuow.Commit();
        }

        public void UpdateSection(SectionEntity entity)
        {
            _sectionReposytory.Update(entity.ToDalSection());
            _unitOfWorkuow.Commit();
        }

        public void DeleteSection(SectionEntity entity)
        {
            _sectionReposytory.Delete(entity.ToDalSection());
            _unitOfWorkuow.Commit();
        }
    }
}
