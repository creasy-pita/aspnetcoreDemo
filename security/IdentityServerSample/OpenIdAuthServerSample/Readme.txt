2018-3-31
 identity ef
 1 引入identityserver4.entityframework (identityuser ,role 是属于 identity 模块，而此处属于 identityserver4 认证和授权的信息 模块 如 claims,grantinfo)
 2 包括的dbcontext 包括   ConfigurationDbContext,PersistedGrantDbContext
 3 创建 ConfigurationDbContext,PersistedGrantDbContext 对应数据库的迁移代码 并执行更新到数据库
 netcore 开发主要命令工具包括 PM 和 CLI的dotnet 命令 两大类
 PM  
 命令
 EntityFramework\add-migration InitConfig  -Context ConfigurationDbContext -OutputDir  data/migrations/identityserver4/ConfigurationDbContext

 dotnet ef cli 即 dotnet 附加ef 的cli
dotnet ef migrations add InitDB -c ConfigurationDbContext -o  data/migrations/identityserver4/ConfigurationDbContext
dotnet ef database update -c ConfigurationDbContext
dotnet ef migrations add InitDB -c PersistedGrantDbContext -o  data/migrations/identityserver4/PersistedGrantDbContext
dotnet ef database update -c PersistedGrantDbContext

4 编写webhost 启动时创建库的方法

2018-3-31
1 通过实现IProfileservice 接口 读取更多用户的信息
包括 角色，头像 ， sub, subjectId
2 webhost启动时 执行检查是否有新的迁移并更新到数据库的检查迁移
（1） 增加 WebHostMigrationExtensions
（2）增加ApplicationDbContextSeed , 包括执行检查是否有新的迁移并更新到数据库的检查迁移，默认添加一个用户

2018-3-30
1 使用Identity
startup中的service改写，中间件加入  
登录中使用  usermanager,signmanager

错误：System.InvalidOperationException:“Service type: IUserClaimsPrincipalFactory`1 not registered.”
解决：将Asp.Net Identity添加到DI容器中时，一定要把注册IdentityServer放在Asp.Net Identity之后，因为注册IdentityServer会覆盖Asp.Net Identity的一些配置，这个非常重要。


错误： The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.
 bool isSucc = await userManager.CheckPasswordAsync(user, loginViewModel.Password);
 解决：
2018-3-29
1 consentcontroller 重构
创建Services 文件目录 ，添加ConsentService，把非action部分代码放到ConsentService中

没有选择任何记住的选项时 提示错误


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

