using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestMultipart.ModelBinding;

namespace CustomModelBindingSample.Data
{
    [ModelBinder(BinderType = typeof(JsonWithFilesFormDataModelBinder))]
    public class CreatePostRequestModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string[] Tags { get; set; }
        [Required]
        public IFormFile[] Image { get; set; }
    }
}
