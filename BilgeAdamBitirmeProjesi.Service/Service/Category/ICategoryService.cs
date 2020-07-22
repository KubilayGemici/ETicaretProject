using BilgeAdamBitirmeProjesi.Core.Service;
//Dönen entities EF eşitleyip kısa halini aşağıda kullandım.
using EF = BilgeAdamBitirmeProjesi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Service.Service.Category
{
    public interface ICategoryService : ICoreService<EF.Category>
    {

    }
}
