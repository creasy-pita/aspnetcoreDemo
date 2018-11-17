razor-pages and aspnetcore mvc
	learn step
		mvc overview
		M
			model binding 
		V 
			view
				razor-pages  (Razor Pages is a new aspect of ASP.NET Core MVC that makes coding page-focused scenarios easier and more productive. )
		C

1 @page makes the file into an MVC action - which means that it handles requests directly, without going through a controller
@page 表示 此cshtml 可以直接处理 requests,不需要controller

2 asp-for 属性加载 Model 中的 属性值
	Edit.cshtml中
	<input asp-for="Customer.Name" />
	Edit.cshtml.cs中
		public class EditModel : PageModel
		{
			[BindProperty]
			public Customer Customer { get; set; }
		}
3 model binding 
