using BilgeAdamBitirmeProjesi.Model.Context;
using BilgeAdamBitirmeProjesi.Service.Service.Base;
using EF = BilgeAdamBitirmeProjesi.Model.Entities;

namespace BilgeAdamBitirmeProjesi.Service.Service.Cart
{
    public class CartService : BaseService<EF.Cart>, ICartService
    {
        public CartService(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
