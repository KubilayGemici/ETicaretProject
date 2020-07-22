using BilgeAdamBitirmeProjesi.Core.Service;
using EF = BilgeAdamBitirmeProjesi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Service.Service.Product
{
    public interface IProductService: ICoreService<EF.Product>
    {
    }
}
