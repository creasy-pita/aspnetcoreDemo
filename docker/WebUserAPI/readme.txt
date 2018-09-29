2018-5-14
使用 docker-compose 组合构建和运行 mysql 和 aspnetcore webapi 镜像
    1 创建docker-compose.yml 在项目根目录
        文件解读
            version: '2'--表示compose 文件的版本 不同版本特性有所不同
            services:--包括的镜像服务，这里有db 和 webapi, 容器名称与此有关： 比如containorname:out_db_1
            db:
                image: mysql:5.7--可以官方镜像
                restart: always
                ports: 
                - 3307:3306
                environment:
                MYSQL_ROOT_PASSWORD: root
                volumes:
                - /docker/mysql/config/my.cnf:/etc/my.cnf
                - /docker/mysql/data:/var/lib/mysql
                - /docker/mysql/beta/initdb:/docker-entrypoint-initdb.d
            webapi:
                image: conferenceapp-webapi--如果通过 Dockerfile构建，则会命名一个镜像名称
                ports:
                - 8001:80
                build:
                context: .
                dockerfile: Dockerfile
                volumes:
                - /docker/aspnetcore/config/appsettings.json:/appsettings.json  --有问题，不起作用，还需修改   
                depends_on:
                - db
    2 docker-compose up 构建镜像及创建容器运行


错误汇总
    1 外网没办法访问
        原因：
            Program.cs 中.UseUrls("http://0.0.0.0:80") webserver 会固定开放80
            docker-compose 中 虽然ports: - 8001:81, 也还是会固定开发80
            所以 本级 和局域网 可以使用 http://localhost:80 http://innernetip:80访问
            但外网使用 http://externalip:8001  不能访问
        解决
            docker-compose 中 虽然ports: - 8001:81 修改为 ports: - 8001:80
            或者 ---
    2 docker-compose down 后，修改docker-compose.yml 内容更新没有生效
        原因 
            Docker-compose down doesn’t remove images
                By default, the only things removed are:
                • Containers for services defined in the Compose file
                • Networks defined in the networks section of the Compose file
                • The default network, if one is used
        解决
            docker rmi [associate_imagename]
        资料
            https://forums.docker.com/t/docker-compose-down-doesnt-remove-images/22778
    3 使用启动容器时初始化mysql脚本
        host 新建目录 /docker/mysql/beta/initdb
        目录中增加 初始化脚本   xx.sql
        在 docker-compose 中增加  - /docker/mysql/beta/initdb:/docker-entrypoint-initdb.d
        
        说明：
            容器中默认有 目录docker-entrypoint-initdb.d 和 文件 entrypoint.sh  ，启动时会执行 sh shell文件
    3 docker-compose yaml 语法
        参考资料：
            https://yeasy.gitbooks.io/docker_practice/content/compose/compose_file.html
            https://yeasy.gitbooks.io/docker_practice/content/compose/commands.html
            https://docs.docker.com/compose/gettingstarted
            https://docs.docker.com/compose/compose-file/

2018-5-15
1 mysql镜像容器初始化脚本挂载和执行 
	步骤
		1 先创建/docker/mysql/beta/config/my.cnf文件和/docker/mysql/beta/data文件夹
			my.cnf内容如下
			[mysqld]
			user=mysql
			character-set-server=utf8
			[client]
			default-character-set=utf8
			[mysql]
			default-character-set=utf8

		2 创建初始化脚本initdb.sql：
			/*创建库和表*/
			create database ljqbetauser;
			use ljqbetauser;
			CREATE TABLE AppUser (  Id int(11) NOT NULL AUTO_INCREMENT,  Company text,  Name text,  Title text,  PRIMARY KEY (Id)) ENGINE=InnoDB DEFAULT CHARSET=utf8;
			insert into ljqbetauser.AppUser (Company,Name,Title) values('initgoogle11','creasypita','comment');
			/*创建用户 和 授权*/
			CREATE USER 'creasypita'@'%' IDENTIFIED BY 'root';
			GRANT ALL PRIVILEGES ON *.* TO 'creasypita'@'%' WITH GRANT OPTION;		
		3 创建目录/docker/mysql/beta/initdb,挂载时映射到容器的 docker-entrypoint-initdb.d文件夹
		
		4 启动 			
			docker container run  -p 3307:3306 -d --rm --name db --network my-net --env MYSQL_ROOT_PASSWORD=root -v=/docker/mysql/beta/initdb:/docker-entrypoint-initdb.d -v=/docker/mysql/beta/config/my.cnf:/etc/my.cnf -v=/docker/mysql/beta/data:/var/lib/mysql mysql/mysql-server:5.7
	错误汇总
		使用 mysql:5.7没有执行 initdb.sql 
			原因：
				容器启动时默认执行入口初始化文件 docker-entrypoint.sh 不存在
				mysql:5.7 镜像 使用的 entrypoint shell 是 docker-entrypoint.sh（可以 使用 docker inspect db查看）
				而容器的根目录只有 	entrypoint.sh 文件 没有docker-entrypoint.sh文件
		使用mysql/mysql-server:5.7   没有执行 initdb.sql
			原因：
				容器启动时默认执行入口初始化文件 entrypoint.sh 是存在
				但是 挂载data目录要清空 或者使用新的目录，mysql/mysql-server:5.7 不能使用 mysql:5.7 ，对执行 initdb.sql 有影响（未查明原因）
		mysql/mysql-server:5.7 容器启动时提示： Please read "Security" section of the manual to find out how to run mysqld as root!
			原因：
				使用root 启动有安全问题，可以配置另外的启动用户
			解决：
				my.cnf内容增加如下
					[mysqld]
					user=mysql
		Host 'webapi.out_default' is not allowed to connect to this MySQL server
			原因：	
				当前连接用户使用mysql creasypita 用户，creasypita用户只有 本地ip（localhost）的访问权限
			解决：
				授权增加外网ip的访问
				CREATE USER 'creasypita'@'%' IDENTIFIED BY 'root';
				grant all privileges on *.* to 'creasypita'@'%' with grant option;
			注释：
				此问题属于 aspnetcore webapi 与 此mysql容器互联时出现的问题 ， ConnectionStrings连接字符串使用：Server=db;Database=ljqbetauser;Uid=creasypita;Pwd=root;Encrypt=true

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

step B
	a 
		1  header 信息
		2 payload 用户信息+timstamp+expiretime
		3 对 header + payload with base64 code
	b 
		使用head中的指定的加密算法HS256 和 AUS持有的秘钥 
		对header + payload 信息 hash 得到签名
	c 
		返回

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
	centos 上 cmd 查看 curl http://localhost:50722/api/values 提示：connection refuse
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
        归档的docker老版本
            https://docs.docker.com/v17.03
            比如 dockerfiler用法
            https://docs.docker.com/v17.03/engine/reference/builder/#shell-form-entrypoint-example