using BilgeAdamBitirmeProjesi.Service.Service.Base;
using EF = BilgeAdamBitirmeProjesi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using BilgeAdamBitirmeProjesi.Model.Context;

namespace BilgeAdamBitirmeProjesi.Service.Service.Comment
{
    public class CommentService : BaseService<EF.Comment>, ICommentService
    {
        public CommentService(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
