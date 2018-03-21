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
3 加入前端逻辑，及JavaScript的规则校验逻辑

