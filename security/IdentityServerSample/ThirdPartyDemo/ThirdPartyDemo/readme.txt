1 nuget IdentityModel.Client
2 使用 DiscoveryClient 获取 指定认证服务器地址  http://localhost:5000的 tokenendpoint地址
3 使用 TokenClient 传入 username，password，clientid,clientsecret等信息 返回 token
4 访问client 需授权的页面：http://localhost:5001/api/values
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var httpResponse = httpClient.GetAsync("http://localhost:5001/api/values").Result;


Q  访问client 需授权的页面 时 由另一个第三方（并非真正的认证服务器） 产生 使用任意的密码模式产生的 token 来访问时， 
似乎也能访问
