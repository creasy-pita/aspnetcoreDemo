/*创建库和表*/
create database ljqbetauser;
use ljqbetauser;
CREATE TABLE AppUser (  Id int(11) NOT NULL AUTO_INCREMENT,  Company text,  Name text,  Title text,  PRIMARY KEY (Id)) ENGINE=InnoDB DEFAULT CHARSET=utf8;
insert into ljqbetauser.AppUser (Company,Name,Title) values('initgoogle11','creasypita','comment');
/*创建用户 和 授权*/
CREATE USER 'creasypita'@'%' IDENTIFIED BY 'root';
GRANT ALL PRIVILEGES ON *.* TO 'creasypita'@'%' WITH GRANT OPTION;	