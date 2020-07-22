using BilgeAdamBitirmeProjesi.Common.DTOs.Base;

namespace BilgeAdamBitirmeProjesi.Common.DTOs.Category
{
    //Web Api ile bağlantılı çalışacak.
    public class CategoryRequest : BaseDto
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
