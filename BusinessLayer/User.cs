using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Name must be at most 20 characters long")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Surname must be at most 20 characters long")]
        public string Surname { get; set; }

        [Required]
        [Range(18, 81)]
        public int Age { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MaxLength(70)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Email { get; set; }

        public List<User> Friends { get; set; }
        public List<Interest> Interests { get; set; }

        public User() 
        {
            Interests = new List<Interest>();
            Friends = new List<User>();
        }

        public User(string firstName, string surname, int age, string username, string password, string email)
        {
            FirstName = firstName;
            Surname = surname;
            Age = age;
            Username = username;
            Password = password;
            Email = email;
            Interests = new List<Interest>();
            Friends = new List<User>();
        }
    }
}
