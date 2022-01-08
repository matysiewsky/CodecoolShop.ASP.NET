using Codecool.CodecoolShop.Logic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IShoppingCartDao: IDao<ShoppingCart>
    {
        public ShoppingCart AddAndReturn(ShoppingCart item);
        public ShoppingCart GetByUserId(string userId);

    }
}