title 
	netcore 基础 概念 练习



#mvcroute
	关键词：
		
	router
		what
		why （作用）
		how
			构造方式
				app.UseRoute(Router)
					可以
				app.UseRoute(Action<IRouteBuilder> action);
			构造router 的主要方式
				routeBuilder.MapRoute()
				 的主要包含参数 
					包括路由名称，模板，默认
					模板
						配合mvc的 模板引用
					如何解析？？
					
				handler
					
					参数指定action
					
					动态指向controller/action
					
					指向urlending			
			resolve order 不同路由匹配的顺序
	
	

	属性 的应用
		Route HttpGet [FromBody] ...httpinput 
		slash url  
	路径的匹配问题

问题
	1 如何解析{controller}/{action}22{id}/{name?} 
	
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
	5 Route  属性 方式 要加到action 方法上才能生效, 
		比如：  http://localhost:5001/home/index/1
			[Route("[controller]")]
			public class HomeController : Controller
			{
				[Route("[action]/{id}")]
				public ActionResult<IEnumerable<string>> Index(string id)
				{
					//
				}
			}
	
		比如：  http://localhost:5001/api/home/index/1
			public class HomeController : Controller
			{
				[Route("api/home/index/{id}")]
				public ActionResult<IEnumerable<string>> Index(string id)
				{
					//
				}
			}
		注：Controller类定义上也可以加， 会与action的 route 合并
		在Controller类定义的 [Route("[controller]")] 《=》Route("home") 
			即[controller]会在加载是解析为 实际的 控制器名称 home	
	5 app.usemvc() 注释后 能否使用自定义handler 的 router
		router模块脱离 MVC 单独使用  方法
			services.AddRouting();//startup 的ConfigureServices 方法中加入
			app.UseRouter(Action<IRouteBuilder> action);//startup Configure方法中加入
			
	6 
说明：

	1 route template 加入 动态参数 可以处理一个 url 集； 不加入 则只可能处理单个url	
	2 controller类 单独只在类定义上 加入 route 属性 并不能map 到实际的action
	通过 unittest 批量测试多种路由 需要考虑实现思路
	重点 属性 的应用 和 				url
					如何解析？？
			
	