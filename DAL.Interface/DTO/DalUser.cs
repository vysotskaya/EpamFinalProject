using System.Collections.Generic;

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
        public ICollection<DalRole> Roles { get; set; } 
    }
}
