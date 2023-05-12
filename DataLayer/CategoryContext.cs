using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CategoryContext : IDb<Category, int>
    {
        private readonly MilenShopov11jDBContext dbContext;

        public CategoryContext(MilenShopov11jDBContext dBContext)
        {
            this.dbContext = dBContext;
        }

        public void Create(Category item)
        {
            try
            {
                dbContext.Categories.Add(item);
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
                Category categoryFromDb = Read(key);

                if (categoryFromDb != null)
                {
                    dbContext.Categories.Remove(categoryFromDb);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Category with this id does not exist!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Category Read(int key, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return dbContext.Categories.Include(x => x.Users).FirstOrDefault(x => x.Id == key);
                }
                else
                {
                    return dbContext.Categories.Find(key);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Category> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Category> query = dbContext.Categories;

                if (useNavigationalProperties)
                {
                    query = query.Include(x => x.Users);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Category item, bool useNavigationalProperties = false)
        {
            try
            {
                Category categoryFromDb = Read(item.Id, useNavigationalProperties);

                if (categoryFromDb == null)
                {
                    Create(item);
                    return;
                }

                categoryFromDb.Name = item.Name;

                if (useNavigationalProperties)
                {
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
                    categoryFromDb.Users = users;
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
