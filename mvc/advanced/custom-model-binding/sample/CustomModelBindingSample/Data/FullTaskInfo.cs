using CustomModelBindingSample.Binders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomModelBindingSample.Data
{
    [ModelBinder(BinderType = typeof(FulltaskInfoBinder))]
    public class FullTaskInfo
    {
        //public ActionResult Add(IFormFile TaskDll, tb_task_model model, string tempdatajson)

        public IFormFile TaskDll { get; set; }

        public tb_task_model model { get; set; }

        public tb_task_config_model[] config_models { get; set; }
        public string tempdatajson { get; set; }
    }
}
