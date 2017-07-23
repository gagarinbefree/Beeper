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