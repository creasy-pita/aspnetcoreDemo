Project MyPipeline 
	创建 请求处理管道（requestdelegate)

Project MiddlewareExtensibilitySample1
	1 自定义middleware
	2 自定义middleware,并在usemiddleware<T> 方法是传入参数
	其他：扩展方法的使用
		静态类名，静态方法，方法参数中传入 this class

Project MiddlewareExtensibilitySample2
	Pre-request dependencies: 
		IMyScopedService is injected into invoke

		IMyScopedService is injected into Constructor
			报错：Cannot resolve scoped service 'MiddlewareExtensibilitySample2.IMyScopedService' from root provider.
			Scoped service 在 pre-request 中创建， 而Constructor 在webhostbuilder 初始化过程中
			转用 AddSingleton 单例注册服务就不会报错
	