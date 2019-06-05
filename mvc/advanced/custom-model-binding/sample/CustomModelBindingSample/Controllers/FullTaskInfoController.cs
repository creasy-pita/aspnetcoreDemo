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
    [Route("api/FullTaskInfo")]
    public class FullTaskInfoController : Controller
    {
        private readonly IHostingEnvironment _env;
        public FullTaskInfoController(IHostingEnvironment env)
        {
            _env = env;
        }

        #region post2
        [HttpPost]
        public string SaveProfile([ModelBinder( Name = "json")]FullTaskInfo model )
        {
            string filePath = Path.Combine(_env.ContentRootPath, "wwwroot\\images\\upload", model.TaskDll.FileName);
            if (System.IO.File.Exists(filePath)) return "a";
            byte[] buffer = new byte[model.TaskDll.Length];
            model.TaskDll.OpenReadStream().ReadAsync(buffer, 0, Convert.ToInt32(model.TaskDll.Length));
            System.IO.File.WriteAllBytes(filePath, buffer);
            
            return model.model.taskname;
        }
        #endregion
    }
}
