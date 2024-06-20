using Nop.Core;

namespace Nop.Plugin.Misc.ShopManagement.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PictureId { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount 
        { 
            get => (Discount)DiscountId; 
            set => DiscountId = (int)value; 
        }
    }
}
