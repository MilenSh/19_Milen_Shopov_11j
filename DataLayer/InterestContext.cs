using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class InterestContext : IDb<Interest, int>
    {
        private readonly MilenShopov11jDBContext dbContext;

        public InterestContext(MilenShopov11jDBContext dBContext) 
        {
            this.dbContext = dBContext;
        }

        public void Create(Interest item)
        {
            try
            {
                dbContext.Interests.Add(item);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int key)
        {
            try
            {
                Interest interestFromDb = Read(key);

                if (interestFromDb != null)
                {
                    dbContext.Interests.Remove(interestFromDb);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Interest with this id does not exist!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Interest Read(int key, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return dbContext.Interests.Include(x => x.Users)
                        .Include(x=>x.Category).FirstOrDefault(x=>x.Id==key);
                }
                else
                {
                    return dbContext.Interests.Find(key);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Interest> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Interest> query = dbContext.Interests;

                if (useNavigationalProperties)
                {
                    query = query.Include(b => b.Users);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Interest item, bool useNavigationalProperties = false)
        {
            try
            {
                Interest interestFromDb = Read(item.Id, useNavigationalProperties);

                if (interestFromDb == null)
                {
                    Create(item);
                    return;
                }

                interestFromDb.Name = item.Name;
                interestFromDb.Category = item.Category;

                if (useNavigationalProperties)
                {
                    Category categoryFromDb = dbContext.Categories.Find(item.Category.Id);
                    if (categoryFromDb != null)
                    {
                        interestFromDb.Category = categoryFromDb;
                    }
                    else
                    {
                        interestFromDb.Category = item.Category;
                    }

                    List<User> users = new List<User>();

                    foreach (User u in item.Users)
                    {
                        User userFromDb = dbContext.Users.Find(u.Id);

                        if (userFromDb != null)
                        {
                            users.Add(userFromDb);
                        }
                        else
                        {
                            users.Add(u);
                        }

                    }

                    interestFromDb.Users = users;
                }
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }
}
