create table #templist (
	phone nvarchar(max) NULL
	, lastname nvarchar(max) NULL
	, [name] nvarchar(max) NULL
	, middlename nvarchar(max) NULL
	, category nvarchar(max) NULL
	, sex nvarchar(max) NULL
	, city nvarchar(max) NULL
	, birthday datetime NULL
	, isvalid int NULL
	, idcategory int NULL
	, idcity int NULL
	, idsex int NULL		
	, idperson int
	, id int identity(1,1) primary key
) on [primary]
