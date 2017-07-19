drop table if exists persons;
drop table if exists cities;
drop table if exists attributes;
drop table if exists personattributes;
drop table if exists categories;

create table persons (
    id int identity(1,1) primary key
    , lastname nvarchar(255) not null
    , firstname nvarchar(255) not null
	, middlename nvarchar(255)	
	, sex int 
	, idcity int not null
	, idcategory int
	, isvalid int not null
);

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

create table personattributes (
	id int identity(1,1) primary key
	, idperson int not null
	, idattribute int not null
	, val nvarchar(255)	
);

insert into attributes (id, name)
values (1, N'Телефон');

insert into cities (name)
select distinct city from templist;

insert into categories (name)
select distinct category from templist
where category is not null;

update templist set
idcategory = (select c.id from categories c where c.name = templist.category)
where idcategory is null;

update templist set
idcity = (select c.id from cities c where c.name = templist.city)
where idcity is null;

update qw set
qw.isvalid = 1
from templist qw
join 
(
	select z.phone, min(id) mid
	from templist z
	where z.phone is not null
	group by z.phone
) z on z.mid = qw.id;

update z set
z.isvalid = 0
from templist z
where z.isvalid is null
and not exists(
	select 1
	from templist zz
	where zz.isvalid = 1
	and zz.lastname = z.lastname
	and zz.name = z.name
	and zz.middlename = z.middlename
);

update q set
q.isvalid = 1
from templist q
where q.isvalid is null;

delete qw
from templist qw
join 
(
	select z.phone, z.lastname, z.name, z.middlename, min(z.id) mid
	from templist z
	where z.phone is not null
	group by z.phone, z.lastname, z.name, z.middlename
) zz on zz.phone = qw.phone
and zz.lastname = qw.lastname
and zz.name = qw.name
and zz.middlename = qw.middlename
and qw.id > zz.mid;

update t set
t.isvalid = 0
from templist t
join 
(
	select p.firstname, p.lastname, p.middlename, pa.val
	from persons p
	join personattributes pa on pa.idperson = p.id and pa.idattribute = 1
) qw on qw.val = t.phone
and qw.firstname = t.name
and qw.lastname = t.lastname
and qw.middlename = t.middlename;

update templist set
idsex = case
	when lower(sex) = N'м' then 1
	when lower(sex) = N'ж' then 0
	else null 
end;

insert into persons (lastname, firstname, middlename, sex, idcity, idcategory, isvalid)
select t.lastname, t.name, t.middlename, 
t.idsex, t.idcity, t.idcategory, t.isvalid
from templist t;



