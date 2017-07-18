--[id] [int] IDENTITY(1,1) NOT NULL,

delete from categories
delete from sexes

insert into categories (name)
select distinct category from templist tt
where category is not null and
not exists (select c.id
from categories c
where upper(c.name) = upper(tt.category)
)

insert into sexes (name)
select distinct sex  from templist tt
where sex is not null and
not exists (select c.id
from categories c
where upper(c.name) = upper(tt.sex)
)

insert into cities (name)
select distinct city from templist tt
where sex is not null and
not exists (select c.id
from cities c
where upper(c.name) = upper(tt.city)
)



