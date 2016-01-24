using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public ICollection<RoleEntity> Roles { get; set; } 
    }
}
