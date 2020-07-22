using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Core.Entity
{
    public interface IEntity<T>
    {
        //Ortak kullanılacak T tipinde ID tanımladık.
         T Id { get; set; }
    }
}
