using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlobStorageDemo.Controllers
{
    public class ImageController : Controller
    {
        ImageService imageService = new ImageService();

        // GET: Image
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase photo)
        {
            var imageUrl = await imageService.UploadImageAsync(photo);
            TempData["LatestImage"] = imageUrl.ToString();
            return RedirectToAction("LatestImage");
        }

        public ActionResult LatestImage()
        {
            var latestImage = string.Empty;
            if (TempData["LatestImage"] != null)
            {
                ViewBag.LatestImage = Convert.ToString(TempData["LatestImage"]);
            }

            return View();
        }
    }
}
