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
        public ContactListViewModel GetPersons(int? page, int? limit, string sortBy, string direction)
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

                //if (!string.IsNullOrWhiteSpace(name))
                //{
                //    query = query.Where(q => q.Name.Contains(name));
                //}

                //if (!string.IsNullOrWhiteSpace(placeOfBirth))
                //{
                //    query = query.Where(q => q.PlaceOfBirth.Contains(placeOfBirth));
                //}

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