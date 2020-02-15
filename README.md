# Projet_Stage
Gestion de stock ADO.net 
# Proc to add :
create table accs(
username varchar(50),
passuser varchar(50),
permlvl int,
constraint PK_USER primary key(username),
);
go
create proc sp_addacc @username varchar(50), @password varchar(50), @permlvl int,
as
insert Accounts values (@username,Lower(CONVERT(VARCHAR(50),HashBytes('MD5', @password), 2)), @permlvl)
go
