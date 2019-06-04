# Custom Model Binding Demo

2019��6��4��
	webapi ��action handler ���Դ��� post ������ͬʱ �ύ�ļ���json�Ĺ��ܿ���
webapi ���� post ������ͬʱ �ύ�ļ���json ����webapi �� action ��һ����������	
	
�Զ��� ģ�Ͱ� custom model binding ����
	1 �Զ��� CreatePostRequestModel ,��ָ�� �󶨵�����Ϊ JsonWithFilesFormDataModelBinder
		    [ModelBinder(BinderType = typeof(JsonWithFilesFormDataModelBinder))]
			public class CreatePostRequestModel
			...
			
			˵�� ��action ����Ϊ CreatePostRequestModel ���Զ������а��� JsonWithFilesFormDataModelBinder �� BindModelAsync���������п��Լ����Լ��İ��߼� ����ɺ��ٽ��� action ������ ����������˶�  http request ӳ�䵽 Model ���Զ����
	2 ��д�Զ������  JsonWithFilesFormDataModelBinder ���� BindModelAsync�����м����Լ��İ��߼� �������յĽ����ֵ�� bindingContext.Result
		bindingContext.Result = ModelBindingResult.Success(model);
	
	�ο� 
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
