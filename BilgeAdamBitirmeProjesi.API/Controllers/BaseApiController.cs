using BilgeAdamBitirmeProjesi.Common.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BilgeAdamBitirmeProjesi.API.Controllers
{
    [Authorize]
    [ApiController]
    //Kendisinden miras alınabilir.Intance alınamaz.Authorize kullanıldığı her yerde bu kullanılsın.Kullanıcının hangi kullanıcı olduğunu bilelim.
    public abstract class BaseApiController<T> : ControllerBase where T : BaseApiController<T>
    {

        private IWorkContext _workContext;

        public IWorkContext WorkContext
        {
            get 
            {
                if (_workContext == null)
                {
                    _workContext = HttpContext.RequestServices.GetService<IWorkContext>();
                };
                return _workContext;
            }
            set { _workContext = value; }
        }

        public BaseApiController()
        {

        }
    }
}
