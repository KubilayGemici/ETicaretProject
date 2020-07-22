using BilgeAdamBitirmeProjesi.Core.Service;
using EF = BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Model.Context;
using BilgeAdamBitirmeProjesi.Service.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Service.Service.User
{
    public class UserService : BaseService<EF.User> , IUserService
    {
        public UserService(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
