create table dbo.templist (
	phone nvarchar(max) NULL
	, lastname nvarchar(max) NULL
	, name nvarchar(max) NULL
	, middlename nvarchar(max) NULL
	, category nvarchar(max) NULL
	, sex nvarchar(max) NULL
	, city nvarchar(max) NULL
	, birthday nvarchar(max) NULL
	, isvalid int NULL
	, idcategory int NULL
	, idcity int NULL
	, idsex int null
	, id int identity(1,1) primary key
) on [primary]
