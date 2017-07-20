drop table if exists cities;
drop table if exists attributes;
drop table if exists personattributes;
drop table if exists categories;
drop table if exists persons;

create table categories (
	id int identity(1,1) primary key
	, name nvarchar(255)
);

create table cities (
	id int identity(1,1) primary key
	, name nvarchar(255)
);

create table attributes (
	id int primary key
	, name nvarchar(255)
);

create table persons (
    id int identity(1,1) primary key
    , lastname nvarchar(255) not null
    , firstname nvarchar(255) not null
	, middlename nvarchar(255)	
	, sex int 
	, idcity int
	, idcategory int
	, isvalid int not null
	, constraint FK_persons_categories foreign key (idcategory)     
		references categories (id)     
		on delete set null    
		on update cascade    
	, constraint FK_persons_cities foreign key (idcity)     
		references cities (id)     
		on delete set null    
		on update cascade    
);

create nonclustered index [nonclusteredindex-lastname-firstname-middlename] on [dbo].[persons]
(
	lastname asc,
	firstname asc,
	middlename asc
) with (pad_index = off, statistics_norecompute = off, sort_in_tempdb = off, drop_existing = off, online = off, allow_row_locks = on, allow_page_locks = on) on [primary]

create table personattributes (
	id int identity(1,1) primary key
	, idperson int not null
	, idattribute int not null
	, val nvarchar(255)	
	, constraint FK_personattributes_persons foreign key (idperson)     
		references persons (id)     
		on delete cascade
		on update cascade  
	, constraint FK_personattributes_attributes foreign key (idattribute)     
		references attributes (id)     
		on delete cascade
		on update cascade  
);

insert into attributes (id, name)
values (1, N'Телефон');