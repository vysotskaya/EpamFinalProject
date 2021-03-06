﻿using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.InterfaceServices
{
    public interface ILotService
    {
        IEnumerable<LotEntity> GetAllLotEntities();
        LotEntity GetLotEntity(int id);
        void CreateLot(LotEntity entity);
        void UpdateLot(LotEntity entity);
        void DeleteLot(LotEntity entity);
        IEnumerable<LotEntity> GetActiveLots();
    }
}
