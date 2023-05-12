using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public List<User> Users { get; set; }

        public Category() 
        {
            Users = new List<User>();
        }
        public Category(string name) 
        {
            Name = name;
            Users = new List<User>();
        }
    }
}
