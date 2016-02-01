using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.InterfaceServices
{
    public interface ISectionService
    {
        IEnumerable<SectionEntity> GetAllSectionEntities();
        SectionEntity GetSectionEntity(int id);
        void CreateSection(SectionEntity entity);
        void UpdateSection(SectionEntity entity);
        void DeleteSection(SectionEntity entity);
    }
}
