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

