using System.Collections.Generic;
using System.Web.Http;
using WeiboImageBackend.Models;

namespace WeiboImageBackend.Controllers
{
    public class FetchController : ApiController
    {
        [HttpPost]
        public List<string> Moments(int size)
        {
            return WeiboImageRepo.Instance.ExtractImages(size);
        }
    }
}
