begin transaction;

delete from #templist
where len(phone) < 10

insert into cities (name)
select distinct t.city 
from ##templist t
where  t.city is not null
and not exists(select 1 
	from cities s 
	where s.name = t.city
);

insert into categories (name)
select distinct t.category 
from #templist t
where category is not null
and not exists(select 1 
	from categories c 
	where c.name = t.category
)

update #templist 
set idcategory = (select c.id 
	from categories c 
	where c.name = #templist.category
)
where idcategory is null;

update #templist set idcity = (select c.id 
	from cities c 
	where c.name = #templist.city
)
where idcity is null;

update qw set qw.isvalid = 1
from #templist qw
join (select z.phone, min(id) mid
	from #templist z
	where z.phone is not null
	group by z.phone
) z on z.mid = qw.id;

update z set z.isvalid = 0
from #templist z
where z.isvalid is null
and not exists(
	select 1
	from #templist zz
	where zz.isvalid = 1
	and zz.lastname = z.lastname
	and zz.name = z.name
	and zz.middlename = z.middlename
);

update q set q.isvalid = 1
from #templist q
where q.isvalid is null;

delete qw
from #templist qw
join (select z.lastname, z.firstname as pname, z.middlename
	from persons z		
) zz on zz.lastname = qw.lastname
and zz.pname = qw.name
and (qw.middlename is null 
or zz.middlename = qw.middlename);

delete qw
from #templist qw
join (select z.phone, z.lastname, z.name, z.middlename, min(z.id) mid
	from #templist z
	where z.phone is not null
	group by z.phone, z.lastname, z.name, z.middlename
) zz on zz.phone = qw.phone
and zz.lastname = qw.lastname
and zz.name = qw.name
and zz.middlename = qw.middlename
and qw.id > zz.mid;

delete qw
from #templist qw
join (select z.phone, z.lastname, z.name, z.middlename, min(z.id) mid
	from #templist z
	where z.phone is not null
	group by z.phone, z.lastname, z.name, z.middlename
) zz on zz.phone = qw.phone
and zz.lastname = qw.lastname
and zz.name = qw.name
and zz.middlename = qw.middlename
and qw.id > zz.mid;

update t set t.isvalid = 0
from #templist t
join (
	select p.firstname, p.lastname, p.middlename, pa.val
	from persons p
	join personattributes pa on pa.idperson = p.id and pa.idattribute = 1
) qw on qw.val = t.phone
and qw.firstname = t.name
and qw.lastname = t.lastname
and qw.middlename = t.middlename;

update #templist 
set idsex = case
	when lower(sex) = N'м' then 1
	when lower(sex) = N'ж' then 0
	else null 
end;

-- запрет эскалации блокировок
declare @lock int;
select @lock = t.lock_escalation from sys.tables t 
where t.name = 'persons';
alter table persons set (lock_escalation = disable);

insert into persons (lastname, firstname, middlename, sex, idcity, idcategory, isvalid, birthday)
select t.lastname, t.name, t.middlename, t.idsex, t.idcity, t.idcategory, t.isvalid, t.birthday
from #templist t;

-- восстановим настройку эскалации
if (@lock = 0)
	alter table persons set (lock_escalation = table);	
else if (@lock = 1)
	alter table persons set (lock_escalation = disable);
else
	alter table persons set (lock_escalation = auto);

update #templist
set idperson = (select p.id
	from persons p 
	join cities c on c.id = p.idcity
	join categories ct on ct.id = p.idcategory
	where p.lastname = #templist.lastname 
	and p.firstname = #templist.name 
	and p.middlename = #templist.middlename
	and c.id = #templist.idcity
	and ct.id = #templist.idcategory
	and p.birthday = #templist.birthday
);

insert into personattributes (idperson, idattribute, val)
select p.id, 1, t.phone from persons p
join #templist t on t.idperson = p.id;

insert into lists ([file], comment) values (@file, @comment);

commit;