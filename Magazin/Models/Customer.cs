using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazin.Models
{
    public class Customer
    {
        [Key]
        [Column ("CustomerId")]
        public int CustomerId { get; set; }

        [Column ("FirstName")]
        public string FirstName { get; set; } 

        [Column ("LastName")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
     
    }
}
