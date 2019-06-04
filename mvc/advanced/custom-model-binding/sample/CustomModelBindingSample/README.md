# Custom Model Binding Demo

2019年6月4日
	webapi 的action handler 可以处理 post 请求中同时 提交文件和json的功能开发
webapi 可以 post 请求中同时 提交文件和json 并由webapi 的 action 的一个参数接收	
	
自定义 模型绑定 custom model binding 步骤
	1 自定义 CreatePostRequestModel ,并指明 绑定的类型为 JsonWithFilesFormDataModelBinder
		    [ModelBinder(BinderType = typeof(JsonWithFilesFormDataModelBinder))]
			public class CreatePostRequestModel
			...
			
			说明 在action 参数为 CreatePostRequestModel 会自动先运行绑定类 JsonWithFilesFormDataModelBinder 的 BindModelAsync方法，当中可以加入自己的绑定逻辑 ，完成后再进入 action 方法。 这样就完成了对  http request 映射到 Model 的自定义绑定
	2 编写自定义绑定类  JsonWithFilesFormDataModelBinder ，在 BindModelAsync方法中加入自己的绑定逻辑 ，把最终的结果赋值给 bindingContext.Result
		bindingContext.Result = ModelBindingResult.Success(model);
	
	参考 
		https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/custom-model-binding?view=aspnetcore-2.2
		https://thomaslevesque.com/2018/09/04/handling-multipart-requests-with-json-and-file-uploads-in-asp-net-core/


You can test the `ByteArrayModelBinder` by running the application and POSTing a base64-encoded string to the ImageController endpoint (/api/image/). You should specify the file and filename proparties in the request Body as form-data (using Postman or a similar tool). You can use [this sample string](Base64String.txt). The result will be saved in the wwwroot/images/upload folder with the filename you specified.

To test the custom binding example, try the following endpoints:
/api/authors/1
/api/authors/2 (NOT FOUND)
/api/boundauthors/1
/api/boundauthors/2 (NOT FOUND)
/api/boundauthors/get/1
/api/boundauthors/get/2 (NO CONTENT) - this action doesn't check for null and return a Not Found
