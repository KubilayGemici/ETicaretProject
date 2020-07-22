using BilgeAdamBitirmeProjesi.Core.Entity.Enums;
using System;

namespace BilgeAdamBitirmeProjesi.Core.Entity
{
    public class CoreEntity : IEntity<Guid>
    {
        //Ortak alanlar olduğu için tüm Entitylere dağıtılmak üzere oluşturuldu.
        public Guid Id { get; set; }
        public Status Status { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }
        public Guid? CreatedUserID { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedIP { get; set; }
        public Guid? ModifiedUserID { get; set; }
    }
}
