﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace BLL.Interface.Entities
{
    public class UserEntity
    {
        public UserEntity()
        {
            Roles = new HashSet<RoleEntity>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime BlockTime { get; set; }
        public string BlockReason { get; set; }
        public Image Photo { get; set; }

        public ICollection<RoleEntity> Roles { get; set; } 
    }
}
