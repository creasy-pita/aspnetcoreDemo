#aspnetcore conceptual code sample
主要在 https://github.com/aspnet/AspNetCore.Docs/aspnetcore 的基础上填写 


title 
	netcore 基础 概念 练习



#mvcroute
	关键词：
	
	术语：模板 
	
	router
		what
			
		why （作用）
			incoming request to a route handler
		how
			构造方式
				app.UseRoute(Router)
					可以
				app.UseRoute(Action<IRouteBuilder> action);
			构造router 的主要方式
				routeBuilder.MapRoute()
				 的主要包含参数 
					包括路由名称，模板，默认
					路由模板
						配合mvc的 模板引用
						模板的组成 ：主要有 slash,固定文本，保留路由参数名称，自定义路由参数
							自定义路由参数：主要用于参数数据的交互
					如何解析？？
					
				handler
					
					参数指定action
					
					动态指向controller/action
					
					指向urlending			
			resolve order 不同路由匹配的顺序
	
	

	属性 的应用
		Route HttpGet [FromBody] ...httpinput 
		HttpGet
			HttpGet["view/"]
		slash url  
	路径的匹配问题

问题
	1 如何解析{controller}/{action}22{id}/{name?} 
		1 ‘’
	2 解析时 route 的 url?a=1&b=2   ?a=1&b=2 部分如何获取 
		【?】 后的内容不作为url,而是分离为查询参数的方式去解析，  extract and resolve this part as the query parameters path by organizing a '?' character
		
		例如：        
			url example:http://localhost:5001/home/index1/1?name=creasy&phone=8750
			/home/index1/1 会作为route的内容 根据路由模板规则来匹配
			?name=creasy&phone=8750 部分会作为 querystring
		
	3如何理解/zjzs/api/{controller}/{action}/{name?}；
		1 /zjzs/api/ 表示签名两段用固定值 /zjzs/api/ 匹配
		2 带 controller，action，name  用 curly , 会放入 RouteValueDictionary 字典，可以使用 httpcontext.GetRouteValue("key").Values 获取字典列表供后续使用
			比如 mvc 框架用来确定 导向那个 controller 和action 时会使用 
		
		
		3 子问题 为什么 route 中的预留的关键字只需要 controller ，action，handler，page,area
			因为mvc 中 控制器需要 确定 controller 和 action 来指向确定的request handler 
	4 [Route("api/{controller}")]属性会不会增加一个路由模板及其匹配规则
		会
	5 Route  属性 方式 要加到action 方法上才能生效
		以上命题 错误，只要 Route 属性 有 action的参数
		
		比如：以下可以匹配：  http://localhost:5001/home/index/1
			[Route("[controller]")]
			public class HomeController : Controller
			{
				[Route("[action]/{id}")]
				public ActionResult<IEnumerable<string>> Index(string id)
				{
					//
				}
			}
	
		比如：以下可以匹配：  http://localhost:5001/api/home/index/1
			public class HomeController : Controller
			{
				[Route("api/home/index/{id}")]
				public ActionResult<IEnumerable<string>> Index(string id)
				{
					//
				}
			}
		比如：以下可匹配路径   http://localhost:5001/home/index
			[Route("[controller]/[action]")]
			public class HomeController : Controller
			{
				public ActionResult<IEnumerable<string>> Index(string id)
				{
					//
				}
			}
		比如：以下可匹配路径   http://localhost:5001/Oindex
			[Route("/O[action]")]
			public class HomeController : Controller
			{
				public ActionResult<IEnumerable<string>> Index(string id)
				{
					//
				}
			}
		比如：以下可匹配路径   http://localhost:5001/index  Controller上装饰的route 与 action name 相同 则 作为最终模板
			[Route("/Index")]
			public class HomeController : Controller
			{
				public ActionResult<IEnumerable<string>> Index(string id)
				{
					//
				}
			}			
	6 Route 属性 Controller类定义上也可以加， 会与action的 route 合并，在Controller类定义的 [Route("[controller]")] 《=》Route("home") 
			即[controller]会在加载是解析为 实际的 控制器名称 home	
	验证 
		比如以下 需要会匹配   http://localhost:5001/home/home/index/aa/ww
		[Route("[controller]")]
		public class HomeController : Controller
		{
			[Route("[controller]/[action]/aa/{id}")]
			public ActionResult<IEnumerable<string>> Index(string id)
			{	
				//
			}
		}
			参考资料：https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.1#route-name    Combining routes
			https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.1#token-replacement-in-route-templates-controller-action-area
	6 Route 属性 Controller 装饰和action上装饰的 route 属性会叠加 产生一个路由模板
		比如：以下 会产生的匹配为 : /Home/Home/Index/aa/{id}
			[Route("[controller]")]
			public class HomeController : Controller
			{
				[Route("[controller]/[action]/aa/{id}")]
				public ActionResult<IEnumerable<string>> Index(string id)
				{	
					//
				}
			}		
	6 route 属性  action上同时装饰的 route属性 和 HttpGet属性
		比如：以下会忽略 [Route("aaa")]
			[Route("aaa")]
			[HttpGet("[action]/{id}")]
			public ActionResult<IEnumerable<string>> Index1(string id, string name = "defaultname", string phone = "defaultphone")
				{
					
				}	
		比如：以下可以两种路径，但不会合并，类似在 action上装饰 多个 HttpGet 属性（会同时有多种路径）

			[HttpGet("[action]/{id}")]
			[Route("aaa")]			
			public ActionResult<IEnumerable<string>> Index1(string id, string name = "defaultname", string phone = "defaultphone")
				{
					
				}			
	5 app.usemvc() 注释后 能否使用自定义handler 的 router
		router模块脱离 MVC 单独使用  方法
			services.AddRouting();//startup 的ConfigureServices 方法中加入
			app.UseRouter(Action<IRouteBuilder> action);//startup Configure方法中加入
			
	6 FromBody FromForm 的区分
	
	7 例子
	api/{controller}/{action}-{id}/{name?} 
	api{controller}/{action}-{id}/{name?} 
	{id}-api/{controller}/{action}/{name?} 
	8 api{controller}/{action}-{id}/{name?} 与 API{controller}/{action}-{id}/{name?} 
	9 为什么要匹配
		匹配 到具体的handler
	10 同时出现 会怎么解析
		[Route("")]
        [HttpGet()]
说明：

	1 route template 加入 动态参数 可以处理一个 url 集； 不加入 则只可能处理单个url	
	2 controller类 单独只在类定义上 加入 route 属性 并不能map 到实际的action
	通过 unittest 批量测试多种路由 需要考虑实现思路
	重点 属性 的应用 和 				url
					如何解析？？
			
FromBody FromForm FromQuery FromRoute FromService 
参考资料：https://www.dotnetcurry.com/aspnet/1390/aspnet-core-web-api-attributes

FromQuery
	[FromQuery]string id   //match name  id
	[FromQuery(Name="keyId")="FromQuery"]string id // match name= keyId
FromService
	例子
	[FromServices] IOrderService orderService
	public Task<Order> Get([FromRoute] int id,[FromServices] IOrderService orderService)
	This attribute asks our dependency injection container for the corresponding implementation of the IOrderService