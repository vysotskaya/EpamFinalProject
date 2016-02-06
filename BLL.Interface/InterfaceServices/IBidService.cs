using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.InterfaceServices
{
    public interface IBidService
    {
        IEnumerable<BidEntity> GetAllBidEntities();
        BidEntity GetBidEntity(int id);
        //IEnumerable<BidEntity> GetAllBidsByUserId(int id);
        void CreateBid(BidEntity entity);
        void UpdateBid(BidEntity entity);
        void DeleteBid(BidEntity entity);
    }
}
