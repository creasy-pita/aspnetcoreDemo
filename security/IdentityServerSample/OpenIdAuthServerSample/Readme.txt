2018-3-27
1 consent 页面实现
index页面跳转的请求中获取 returnUrl ,其中包含clientid
使用IClientStore, IResourceStore,使用 IIdentityServerInteractionService 来于client,resource信息交互
通过以上的类获取 consentviewModel需要的信息：client信息，同意访问的scope信息
在consent页面 提供可访问scope的勾选  

2018-3-26
作为第三方的认证服务器
1 nuget identityserver4 
2 
            .AddInMemoryApiResources(Config.GetResource())
            .AddInMemoryClients(Config.GetClient())
            .AddTestUsers(Config.GetTestUser());
            addidentityresource
3  app.UseIdentityServer();

4 认证 控制器 
注入  testuserStore



2018-3-26 之前
identity Mvc + cookie模式认证
1 cookie 验证
2 identity 登录，登出， 
3后台 数据库形式存储
 需要 数据库 迁移操作

1 引入cookie认证服务
2 认证方法加入中间件流程

3 在需要授权的页面加入授权属性  [Autho...]
4 增加 AccountController 加入 login ,logout

说明：
options.LoginPath="/Account/MakeLogin"//需要登录是跳转到的path ，默认为 Account/Login


加入注册 和 登录功能，授权和认证使用后台数据库的数据

1 加入注册和登录的ui,  accountcontroller 加入 注册 signin(), 登入 login 的action; viewmodel 和view

登录ui


2 加入注册和登录的后台逻辑  data中 引入Identity User,Role


3 加入returnurl 
httpget 方式进入register view 时 接收rurl,  同时保存到viewdata 做页面级别的状态保存 
register view httpost 时 前端 from中传递rurl
注册成功是转向rurl;失败是转向 home  index

4 加入 后端登录 验证
model 增加 required, datatype.emailaddress等属性  后端验证时按此规则来验证； view页面增加 asp-validation-for="Email" 后端范围验证结果时能够按验证规则提示用户不满足的内容

4 加入前端逻辑，及JavaScript的规则校验逻辑

