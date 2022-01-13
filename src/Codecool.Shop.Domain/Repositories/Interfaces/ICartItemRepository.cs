using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.Domain.Repositories.Interfaces;

public interface ICartItemRepository: IRepository<CartItem>
{
    public IEnumerable<CartItem> GetAll(int shoppingCartId);
    public void RemoveCartItems(int cartId);

    public void Update(int itemId);
    public void Modify(CartItem itemToUpdate);


}