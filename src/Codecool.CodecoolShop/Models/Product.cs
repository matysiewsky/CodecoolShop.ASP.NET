namespace Codecool.CodecoolShop.Models
{
    public class Product : BaseModel
    {
        public string Currency { get; set; }
        public double DefaultPrice { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public int ProductCategoryId {get; set; }
        public Supplier Supplier { get; set; }
        public int SupplierId {get; set; }

        public void SetProductCategory(ProductCategory productCategory)
        {
            ProductCategory = productCategory;
            ProductCategoryId = productCategory.Id;

        }
    }
}
