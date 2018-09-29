一 采用客户端模式

1 创建  webapi 
2 nuget 添加identityserver4 包
3 startup 配置
	添加依赖注入services  
	
		services.AddIdentityServer()
				.AddDeveloperSigningCredential()
				.AddInMemoryApiResources(Config.GetResource())
				.AddInMemoryClients(Config.GetClient())
				.AddTestUsers(Config.GetTestUser());
	加入管道
		app.UseIdentityServer();
4 添加  config类 ,加入 resource和初始的client
5 修改statrtup
在 依赖注入中 加入 resource和初始的client
                    .AddInMemoryApiResources(Config.GetResource())
                    .AddInMemoryClients(Config.GetClient());
6 webapi 没有页面
identityserver4 加入的页面  http://localhost:5000/.well-known/openid-configuration


二 采用密码模式
1 只需在之前的客户端模式 进行修改
2 添加 内存式用户 List<IdentityModel.Test.Testuser>
3 修改startup.cs config 
添加  services.AddTestUsers