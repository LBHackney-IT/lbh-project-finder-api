using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFinderApi.V1.Infrastructure
{
    [Table("pf_user")]
    public class User
    {
        [Column("id")] [Key] public int Id { get; set; }

        [Column("email")] [Required] public string Email { get; set; }

        [Column("first_name")] [Required] public string FirstName { get; set; }

        [Column("last_name")] [Required] public string LastName { get; set; }

        [Column("role")] [Required] public string Role { get; set; }
    }
}
