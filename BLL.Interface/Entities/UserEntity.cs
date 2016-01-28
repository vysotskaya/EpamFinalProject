using System.Collections.Generic;

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
        public ICollection<RoleEntity> Roles { get; set; } 
    }
}
