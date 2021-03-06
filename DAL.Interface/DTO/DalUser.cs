﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace DAL.Interface.DTO
{
    public class DalUser : IEntity
    {
        public DalUser()
        {
            Roles = new HashSet<DalRole>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime BlockTime { get; set; }
        public string BlockReason { get; set; }
        public Byte[] Photo { get; set; }

        public ICollection<DalRole> Roles { get; set; } 
    }
}
