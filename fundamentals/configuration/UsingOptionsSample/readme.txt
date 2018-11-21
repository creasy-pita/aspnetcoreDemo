1 options 使用
  Named options support with IConfigureNamedOptions
用途：allows the app to distinguish between named options configurations. 
比如 MyOptions 类  在配置是可以配置多个实例， 通过名称来区分，使用时通过名称区分获取不同实例
例如 项目中的 example 6
	配置时：
            services.Configure<MyOptions>("named_options_1", Configuration);
            services.Configure<MyOptions>("named_options_2", myoptions => myoptions.Option1 = "value_from_named_options_2"); 
	获取
		IOptionsSnapshot<MyOptions> optionsSnapshot1 
		namedOptions1 = optionsSnapshot1.Get("named_options_1");
        namedOptions2 = optionsSnapshot1.Get("named_options_2");

默认方法
	
2018-11-15
options 源码解读问题
	_options.OnChange 事件 如何被触发
		1 可以参考 public static Configuration.IConfigurationBuilder AddJsonFile(this Configuration.IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange);
	需要了解 整个工作的过程
	option的数据，怎么读数据库获取呢

