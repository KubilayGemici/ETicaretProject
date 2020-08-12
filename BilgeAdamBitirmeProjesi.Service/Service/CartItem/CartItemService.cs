using BilgeAdamBitirmeProjesi.Model.Context;
using BilgeAdamBitirmeProjesi.Service.Service.Base;
using EF = BilgeAdamBitirmeProjesi.Model.Entities;

namespace BilgeAdamBitirmeProjesi.Service.Service.CartItem
{
    public class CartItemService : BaseService<EF.CartItem>, ICartItemService
    {
        public CartItemService(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
