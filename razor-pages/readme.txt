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
@page ��ʾ ��cshtml ����ֱ�Ӵ��� requests,����Ҫcontroller

2 asp-for ���Լ��� Model �е� ����ֵ
	Edit.cshtml��
	<input asp-for="Customer.Name" />
	Edit.cshtml.cs��
		public class EditModel : PageModel
		{
			[BindProperty]
			public Customer Customer { get; set; }
		}
3 model binding 
