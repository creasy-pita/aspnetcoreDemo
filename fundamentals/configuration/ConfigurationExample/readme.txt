1 自定义 配置类 完成通过entityframework读取数据库配置信息 的实现

2 读取:从 不同 实现IConfigurationProvider 的 实例的Data 中的数据读取配置信息，Data 是一个dictionary<string,string> 类型
如果是多个层次的会组织如下
+		[0]	{[json_array:Key, value1]}	System.Collections.Generic.KeyValuePair<string, string>
+		[1]	{[json_array:Subsection:0, value1]}	System.Collections.Generic.KeyValuePair<string, string>
+		[2]	{[json_array:Subsection:1, valueB2]}	System.Collections.Generic.KeyValuePair<string, string>
+		[3]	{[json_array:Subsection:2, valueB3]}	System.Collections.Generic.KeyValuePair<string, string>

为了帮助理解上面的json_array:Subsection:【0】, value1  中的【0】此处 对json_arry.json
  "json_array": {
    "Key": "value1",
    "Subsection": [ "value1", "valueB2", "valueB3" ]
  }
可以解释为
  "json_array": {
    "Key": "value1",
    "Subsection": {"0":"value1","1": "valueB2", "2":"valueB3" ]
  }
最底层的叶子节点会变成值， 上层的在key这一层用多层的冒号组织【：：：：】
+		[0]	{[tvshow:metadata:series, Dr. Who]}	System.Collections.Generic.KeyValuePair<string, string>
+		[1]	{[tvshow:metadata:title, The Sun Makers]}	System.Collections.Generic.KeyValuePair<string, string>
+		[2]	{[tvshow:metadata:airdate, 11/26/1977]}	System.Collections.Generic.KeyValuePair<string, string>
+		[3]	{[tvshow:metadata:episodes, 4]}	System.Collections.Generic.KeyValuePair<string, string>
+		[4]	{[tvshow:actors:names, Tom Baker, Louise Jameson, John Leeson]}	System.Collections.Generic.KeyValuePair<string, string>
+		[5]	{[tvshow:legal, (c)1977 BBC https://www.bbc.co.uk/programmes/b006q2x0]}	System.Collections.Generic.KeyValuePair<string, string>


读取方法
config.GetValue  GetSection() 

3  DbContextOptions 的配置方式， 
	1 外部 需要 通过 DbContextOptionsBuilder 配置options
	1通过 委托 _optionsAction 传入 DbContextOptionsBuilder  来配置 DbContextOptions
            配置好了以后  通过DbContextOptionsBuilder 获取 DbContextOptions
			具体可以查看 EFConfigurationProvider的代码



	2 委托部分直接用 optionsBuilder.UseSqlite("Data Source=blog.db");
var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
optionsBuilder.UseSqlite("Data Source=blog.db");

	2 DbContext 内部 不需要传输 options,直接在 OnConfiguring配置
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=blog.db");
    }

4  CommandLine 
	ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddCommandLine(args);
            })
	参数传递方式
		key前置方式
			Key prefix	Example
			No prefix	CommandLineKey1=value1
			Two dashes (--)	--CommandLineKey2=value2, --CommandLineKey2 value2
			Forward slash (/)	/CommandLineKey3=value3, /CommandLineKey3 value3
		单个或多个参数
			CommandLineKey1=value1  /*单个*/
			CommandLineKey1=value1 CommandLineKey2=value2  /*多个*/
		获取参数
			config.getValue("CommandLineKey1")
		