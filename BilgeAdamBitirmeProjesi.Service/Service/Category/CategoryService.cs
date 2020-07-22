using BilgeAdamBitirmeProjesi.Service.Service.Base;
using EF = BilgeAdamBitirmeProjesi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using BilgeAdamBitirmeProjesi.Model.Context;

namespace BilgeAdamBitirmeProjesi.Service.Service.Category
{
    public class CategoryService : BaseService<EF.Category>, ICategoryService
    {
        //Var olan yapıları istediğim gibi burada da kullanabilirim.
        public CategoryService(DataContext dataContext) : base(dataContext)
        {     
            
        }

    }
}
