using EF = BilgeAdamBitirmeProjesi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using BilgeAdamBitirmeProjesi.Core.Service;
using BilgeAdamBitirmeProjesi.Service.Service.Base;
using BilgeAdamBitirmeProjesi.Model.Context;

namespace BilgeAdamBitirmeProjesi.Service.Service.Product
{
    public class ProductService : BaseService<EF.Product>,IProductService
    {
        public ProductService(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
