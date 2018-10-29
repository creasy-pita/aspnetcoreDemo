webapi App访问次数记录 功能
1 使用加入一个自定义middleware 来实现
要素
    RequestDelegate _next;
    Invoke(HttpContext context)

2 注册为 IApplicationBuilder 扩展方法
3 传入一个外部Application区域的生命周期的对象，是每个httpcontext 能够访问
4 startup config中注册中间件