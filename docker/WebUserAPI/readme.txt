2018-5-9
1 开放外网访问
    localhost:5000-> 0.0.0.0:80
2 后台使用mysql EFCore ，webapi 启动时能够初始化mysql 中一条用户记录
    注意：Dockerfile 中不能使用  CMD["dotnet","run"] ,而是使用  ENTERPOINT ["dotnet","run"]
    区别 一个在构建时运行（此时去运行 网络没有构建 ，还不能连接到mysql 所在的容器），一个在开启容器时运行  
3 使用link 互联 docker 容器下的mysql


碰到错误
    1 dotnet ef database update InitDB 时 会先执行Startup 类Configure方法，所以Configure内部给库初始记录的方法InitUserDb会报错
    解决
        可以对报错进行内部catch的方式处理
    2 microsoft/dotnet 没有aspnetcore librarys 所以webapi应用的容器开启时也没有任何 kernel 的控制台日志
    解决：
        能让 microsoft/aspnetcore-build  microsoft/aspnetcore 合成
        或者 microsoft/dotnet 中能加入本地package  参见：https://stackoverflow.com/questions/43400069/dotnet-add-package-with-local-package-file

        自部署方式，这样只需要runtime 
            1 查看目标平台系统和硬件参数
                cat /etc/issue
                getconf LONG_BIT  --查看32/64
                   
            2 dotnet publish -r debian-x64 -o out  -- 比如： debian-x64
            3 Dockerfile 只需要 FROM microsoft/aspnetcore

2018-4-20


docker network webapi+mysql
1 继续EFcore mysql+webapi, 数据库codefirst 形式迁移 
（1） dotnet ef CLI 安装
（2）efcore mysql 下载
(3)mysql 创建数据库，连接测试
（4）webapi 增加 dbcontext,model, startup引入 dbcontext 并切换到mysql.data.EFcore
(5)创建首次迁移， 更新到数据库
（6）startup 类中增加启动时初始一条默认记录
（7） 本地启动测试
（8）部署到linux 环境（linux 事先安装好netcore环境）
（9）启动，并完成本地访问
（10） 完成远程访问
2 制作 webapi的镜像
（1） dockerfile文件
（2）docker build 命令 使用
存在问题：见错误

dockerfile 中可以有多个镜像，相互之间有文件的传递 一开始是dockfile文件所在的环境。
COPY --from=build /code/out  ./   
这句是不是切换到别名为build的镜像环境，然后传递code/out/ 的文件
上图命令中，
我的是centos7.x 环境，试了没有办法升级docker（还没有想到其他成功的办法） ，上图1中 FROM microsoft/aspnetcore-build as build 的 as 语法（我的docker 是ce 17.03 ）没办用，查了需要升级docker到 17.06 or later 才能用
有没有办法修改为图2的形式 （就是 COPY --from   不使用--from）

以下未做
（3）build 完成并暴露在80端口测试 docker run -p 80:80 -name  name1 containername
（4）完成network 方式与 mysql 互联
3 制作compose 镜像
（1） webapi 和 mysql 镜像 合并 开启


2 

错误

	1 dotnet ef migrations add
	只简单的提示错误 build failed

	此时会简单把 ef migrations 相关的代码作为关注点
	而其他代码跟他是平级的，需要多生成通过才可以。所以需要升维无看这个问题

	dotnet search 
		1 资料 https://github.com/billpratt/dotnet-search

		2 Installation ：
		（1）前置条件（Preconditions） 
		.NET Core 2.1 & higher
		（2）命令：dotnet install tool --global dotnet-search
	2 dotnet ef database update 提示：Table 'ljqbetauser.__efmigrationshistory' doesn't exist
	解决：https://stackoverflow.com/questions/46089982/ef-core-update-database-on-mysql-fails-with-efmigrationshistory-doesnt-ex
                    CREATE TABLE `__EFMigrationsHistory` 
                ( 
                    `MigrationId` nvarchar(150) NOT NULL, 
                    `ProductVersion` nvarchar(32) NOT NULL, 
                    PRIMARY KEY (`MigrationId`) 
                );
	3 connection refuse
	直接cmd 访问
		https://www.dwheeler.com/essays/open-files-urls.html
	centos 上 cmd 查看 curl http://localhost:50722/api/values
	进程端口查看
	防火墙服务 查看
	是否 
	一定需要反向代理
    
    解决： 
        1 防火墙服务未安装  centos 7及以上版本 防火墙服务采用 firewalldd
        2 开发ip访问到external network :http://localhost-> http://0.0.0.0 | +
        3 不一定需要反向代理 外网可直接访问webserver
    4 使用Dockerfile build  镜像是出错
		问题  
            Step 1/9 : FROM microsoft/aspnetcore-build as build-env
            Step 9/11 : COPY --from=microsoft/aspnetcore-build /code/out ./
            Unknown flag: from
                Error parsing reference: "microsoft/aspnetcore-build as build-env" is not a valid repository/tag: invalid reference format
		问题原因：
            不支持 ：This sample requires Docker 17.06 or later of the Docker client
		    需要升级docker到 17.06 or later ：
            而centos7 虚拟机还无法升级docker到 17.06 
		参考资料:
            https://github.com/dotnet/dotnet-docker-samples/issues/80
            https://stackoverflow.com/questions/26472586/upgrade-docker-on-centos-7
            http://blog.devzeng.com/blog/build-docker-image-with-dockerfile.html
        归档老版本
            https://docs.docker.com/v17.03
            比如 dockerfiler用法
            https://docs.docker.com/v17.03/engine/reference/builder/#shell-form-entrypoint-example