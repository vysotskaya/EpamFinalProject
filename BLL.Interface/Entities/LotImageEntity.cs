using System.Drawing;

namespace BLL.Interface.Entities
{
    public class LotImageEntity
    {
        public int Id { get; set; }
        public Image Content { get; set; }
        public int LotRefId { get; set; }
    }
}
