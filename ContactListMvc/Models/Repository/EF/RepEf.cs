using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactListMvc.Models.Repository;
using ContactListMvc.Models.Repository.EF.DTO;
using ContactListMvc.Models.ViewModels;
using System.Data.Entity.SqlServer;

namespace ContactListMvc.Models.Repository.EF
{
    public class RepEf : Repository, IRepEf
    {
        public List<CategoryVeiwModel> GetCategories()
        {
            using (BeeperDbContext context = new BeeperDbContext())
            {
                var query = context.categories.Select(c => new CategoryVeiwModel
                {
                    id = c.id,
                    name = c.name
                });

                return query.ToList();
            }
        }

        public List<CitiyViewModel> GetCities()
        {
            using (BeeperDbContext context = new BeeperDbContext())
            {
                var query = context.cities.Select(c => new CitiyViewModel
                {
                    id = c.id,
                    name = c.name
                });

                return query.ToList();
            }
        }

        public ContactListViewModel GetPersons(int? page
            , int? limit
            , string sortBy
            , string direction
            , string lastname
            , string phone
            , string city
            , string category
            , string isvalid)
        {
            List<PersonViewModel> records;
            int total;
            using (BeeperDbContext context = new BeeperDbContext())
            {
                var atrribute = context.attributes.FirstOrDefault(f => f.id == 1);

                var query = context.persons.Select(c => new PersonViewModel
                {
                    id = c.id,
                    firstname = c.firstname,
                    lastname = c.lastname,
                    middlename = c.middlename,
                    birthday = c.birthday != null ? c.birthday.ToString() : "",
                    city = c.cities.name,
                    sex = c.sex != null ? (c.sex == 1 ? "М" : "Ж") : "",
                    category = c.categories.name,
                    phone = context.personattributes.Where(f => f.idattribute == atrribute.id && f.idperson == c.id).Select(x => x.val).FirstOrDefault(),
                    isvalid = c.isvalid == 1 ? "Да" : "Нет"
                });

                if (!String.IsNullOrWhiteSpace(lastname))
                    query = query.Where(f => f.lastname.Contains(lastname));

                if (!String.IsNullOrWhiteSpace(phone))
                    query = query.Where(f => f.phone.Contains(phone));

                if (!String.IsNullOrWhiteSpace(city))
                {
                    string[] cities = city.Split(',');
                    query = query.Where(x => city.Contains(x.city));                    
                }

                if (!String.IsNullOrWhiteSpace(category))
                {
                    string[] categories = category.Split(',');
                    query = query.Where(x => categories.Contains(x.category));
                }

                if (!String.IsNullOrWhiteSpace(isvalid))
                {
                    string[] cities = city.Split(',');
                    query = query.Where(x => isvalid.Contains(x.isvalid));
                }

                if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
                {
                    if (direction.Trim().ToLower() == "asc")
                    {
                        switch (sortBy.Trim().ToLower())
                        {
                            case "id":
                                query = query.OrderBy(q => q.id);
                                break;
                            case "lastname":
                                query = query.OrderBy(q => q.lastname);
                                break;
                            case "firstname":
                                query = query.OrderBy(q => q.firstname);
                                break;
                            case "middlename":
                                query = query.OrderBy(q => q.middlename);
                                break;
                            case "birthday":
                                query = query.OrderBy(q => q.birthday);
                                break;
                            case "city":
                                query = query.OrderBy(q => q.city);
                                break;
                            case "sex":
                                query = query.OrderBy(q => q.sex);
                                break;
                            case "category":
                                query = query.OrderBy(q => q.category);
                                break;
                            case "phone":
                                query = query.OrderBy(q => q.phone);
                                break;
                            case "isvalid":
                                query = query.OrderBy(q => q.isvalid);
                                break;
                        }
                    }
                    else
                    {
                        switch (sortBy.Trim().ToLower())
                        {
                            case "id":
                                query = query.OrderByDescending(q => q.id);
                                break;
                            case "lastname":
                                query = query.OrderByDescending(q => q.lastname);
                                break;
                            case "firstname":
                                query = query.OrderByDescending(q => q.firstname);
                                break;
                            case "middlename":
                                query = query.OrderByDescending(q => q.middlename);
                                break;
                            case "birthday":
                                query = query.OrderByDescending(q => q.birthday);
                                break;
                            case "city":
                                query = query.OrderByDescending(q => q.city);
                                break;
                            case "sex":
                                query = query.OrderByDescending(q => q.sex);
                                break;
                            case "category":
                                query = query.OrderByDescending(q => q.category);
                                break;
                            case "phone":
                                query = query.OrderByDescending(q => q.phone);
                                break;
                            case "isvalid":
                                query = query.OrderByDescending(q => q.isvalid);
                                break;
                        }
                    }
                }
                else
                    query = query.OrderBy(q => q.id);

                total = query.Count();
                if (page.HasValue && limit.HasValue)
                {
                    int start = (page.Value - 1) * limit.Value;
                    records = query.Skip(start).Take(limit.Value).ToList();
                }
                else
                    records = query.ToList();

                return new ContactListViewModel
                {
                    records = records,
                    total = total
                };
            }
        }
    }        
}