using System;
using System.Linq;
using BLL.Interface.InterfaceServices;
using Quartz;

namespace MvcPL.Schedule
{
    public class UserStatusJob : IJob
    {
        private readonly ILotService _lotService;

        public UserStatusJob()
        {
            _lotService = (ILotService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(ILotService));
        }

        public void Execute(IJobExecutionContext context)
        {
            var lots = _lotService.GetAllLotEntities().Where(l => !l.IsSold && l.EndDate <= DateTime.Now).ToList();
            if (lots != null)
            {
                for (var i = 0; i < lots.Count(); i++ )
                {
                    lots[i].IsSold = true;
                    _lotService.UpdateLot(lots[i]);
                }
            }
        }
    }
}