#在aspnetcoreDI中怎样注册一个实现了多个接口的服务
##见Startup ConfigureServices方法的 片段：one class implements multi interface
##1 以单例的方式 给接口IFoo，IBar注册Foo
	//registered Foo as a singleton for both IFoo and IBar  snippet
    public void ConfigureServices(IServiceCollection services)
    {
		//other to configure
        services.AddSingleton<IFoo, Foo>();
        services.AddSingleton<IBar,Foo >(); 
	}
<code style="color:green;">services.AddSingleton<IFoo, Foo>()；
</code><br>
<span style="color:green;">`services.AddSingleton<IBar, Foo>`</span>;<br>

    //Index.cshtml.cs  snippet
	public class IndexModel : PageModel
    {
    private readonly IMyDependency _myDependency;
    public IndexModel(IFoo foo,IBar bar)
    {
	    Foo = foo;
	    Bar = bar;
    }
    public IFoo Foo { get; }
    public IBar Bar { get; }

	//Index.cshtml  snippet
    <div class="row">
    <h3>implements multi interface</h3>
    <h3>Ifoo</h3>
    <div>@Model.Foo.GetHashCode()</div>
    <h3>Ibar</h3>
    <div>@Model.Bar.GetHashCode()</div>
    </div>
***
	//output 输出结果
	implements multi interface
	Ifoo
	42918335
	Ibar
	42918335
![Philadelphia's Magic Gardens. This place was so cool! This comment for placeholder when the image cannot load](/markdownassets/images/output.jpg "Philadelphia's Magic Gardens")


**会获取的服务不同（属于两个实例）**
>note
* 同一类型不能重复注入，如果有一个类型需要以多种方式注册和使用的场景，采用如下方式
		services.AddTransient<IOperationTransient, Operation>();
		services.AddScoped<IOperationScoped, Operation>();
		services.AddSingleton<IOperationSingleton, Operation>();
* 

##2 
