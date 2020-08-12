using BilgeAdamBitirmeProjesi.Service.Service.Base;
using EF = BilgeAdamBitirmeProjesi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using BilgeAdamBitirmeProjesi.Model.Context;

namespace BilgeAdamBitirmeProjesi.Service.Service.OrderDetail
{
    public class OrderDetailService : BaseService<EF.OrderDetail>, IOrderDetailService
    {
        //Var olan yapıları istediğim gibi burada da kullanabilirim.
        public OrderDetailService(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
