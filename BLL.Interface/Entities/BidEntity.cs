﻿using System;

namespace BLL.Interface.Entities
{
    public class BidEntity
    {
        public int Id { get; set; }
        public int UserRefId { get; set; }
        public string UserLogin { get; set; }
        public int LotRefId { get; set; }
        public string LotName { get; set; }
        public double BidAmount { get; set; }
        public DateTime BidTime { get; set; }
    }
}
