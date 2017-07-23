using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactListMvc.Models.Repository.EF
{
    public class RepEf : Repository, IRepEf
    {
        public List<person> GetPersons(int? page, int? limit, string sortBy, string direction)
        {
            List<person> records;
            int total;
            using (BeeperDbContext context = new BeeperDbContext())
            {
                var query = context.persons.Select(c => c);
                

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
                            case "lastname":
                                query = query.OrderBy(q => q.lastname);
                                break;
                            case "firstname":
                                query = query.OrderBy(q => q.firstname);
                                break;
                            case "middlename":
                                query = query.OrderBy(q => q.middlename);
                                break;
                        }
                    }
                    else
                    {
                        switch (sortBy.Trim().ToLower())
                        {
                            case "lastname":
                                query = query.OrderByDescending(q => q.lastname);
                                break;
                            case "firstname":
                                query = query.OrderByDescending(q => q.firstname);
                                break;
                            case "middlename":
                                query = query.OrderByDescending(q => q.middlename);
                                break;
                        }
                    }
                }
                else
                {
                    query = query.OrderBy(q => q.lastname);
                }

                total = query.Count();
                if (page.HasValue && limit.HasValue)
                {
                    int start = (page.Value - 1) * limit.Value;
                    records = query.Skip(start).Take(limit.Value).ToList();
                }
                else
                {
                    records = query.ToList();
                }

                return records;
            }
        }
    }
}