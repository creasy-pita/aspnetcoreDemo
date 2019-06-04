using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CustomModelBindingSample.Data;
using System;

namespace CustomModelBindingSample.Controllers
{

    [Produces("application/json")]
    [Route("api/JsonWithFilesFormData")]
    public class JsonWithFilesFormDataController : Controller
    {
        private readonly IHostingEnvironment _env;
        public JsonWithFilesFormDataController(IHostingEnvironment env)
        {
            _env = env;
        }

        #region post2
        [HttpPost]
        public string SaveProfile([ModelBinder( Name = "json")]CreatePostRequestModel model )
        {
            string a = "";

            foreach (var image in model.Image)
            {
                string filePath = Path.Combine(_env.ContentRootPath, "wwwroot\\images\\upload", image.FileName);
                if (System.IO.File.Exists(filePath)) return "a";
                byte[] buffer = new byte[image.Length];
                image.OpenReadStream().ReadAsync(buffer, 0, Convert.ToInt32(image.Length));
                System.IO.File.WriteAllBytes(filePath, buffer);
            }


            return model.Title;
        }
        #endregion
    }
}
