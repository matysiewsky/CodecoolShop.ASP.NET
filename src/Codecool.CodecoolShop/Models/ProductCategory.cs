using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class ProductCategory: BaseModel
    {
        public List<Product> Products { get; set; }
        public string Department { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProductCategory);
        }

        public bool Equals(ProductCategory obj)
        {
            return obj != null && obj.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
