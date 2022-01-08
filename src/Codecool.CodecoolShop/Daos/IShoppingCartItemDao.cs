using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos
{
    public interface IShoppingCartItemDao: IDao<CartItem>
    {
        public IEnumerable<CartItem> GetAllForUser(int shoppingCartId);

        public void Update(int itemId);

    }
}