using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WeiboImageBackend.Controllers
{
    public class ImageController : Controller
    {
        private string[] urlPrefix = { "http://wx1.sinaimg.cn/mw690/", "http://wx2.sinaimg.cn/mw690/", "http://wx3.sinaimg.cn/mw690/", "http://wx4.sinaimg.cn/mw690/" };
        private string[] urlLargePrefix = { "http://wx1.sinaimg.cn/large/", "http://wx2.sinaimg.cn/large/", "http://wx3.sinaimg.cn/large/", "http://wx4.sinaimg.cn/large/" };
        // GET: Image
        public async Task<ActionResult> Load(string img, int large = 0)
        {
            var url = large == 0 ? urlPrefix[new Random().Next(0, urlPrefix.Length)] : urlLargePrefix[new Random().Next(0, urlLargePrefix.Length)];
            try
            {
                HttpWebRequest client = (HttpWebRequest)WebRequest.CreateHttp(url + img);
                client.Timeout = 30 * 1000;
                {
                    var response = await client.GetResponseAsync();
                    using (var stream = response.GetResponseStream())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            return File(ms.ToArray(), "image/jpeg");
                        }
                    }
                }
            }
            catch (Exception exp)
            { }

            return new HttpStatusCodeResult(404);
        }
    }
}