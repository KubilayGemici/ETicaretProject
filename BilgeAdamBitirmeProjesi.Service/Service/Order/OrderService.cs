using BilgeAdamBitirmeProjesi.Service.Service.Base;
using EF = BilgeAdamBitirmeProjesi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using BilgeAdamBitirmeProjesi.Model.Context;

namespace BilgeAdamBitirmeProjesi.Service.Service.Order
{
    public class OrderService : BaseService<EF.Order>, IOrderService
    {
        public OrderService(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
