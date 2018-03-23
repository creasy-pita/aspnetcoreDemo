1 创建  webapi 
2 nuget 添加identityserver4 包
3 startup 配置
添加依赖注入services  
加入管道

4 添加  config类 ,加入 resource和初始的client
5 修改statrtup
在 依赖注入中 加入 resource和初始的client
                    .AddInMemoryApiResources(Config.GetResource())
                    .AddInMemoryClients(Config.GetClient());
6 webapi 没有页面
identityserver4 加入的页面  http://localhost:5000/.well-known/openid-configuration