using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFinderApi.V1.Infrastructure
{
    [Table("pf_user")]
    public class User
    {
        [Column("id")] [MaxLength(16)] [Key] public int Id { get; set; }

        [Column("email")] [MaxLength(80)] [Required] public string Email { get; set; } = null!;

        [Column("first_name")] [MaxLength(100)] [Required] public string FirstName { get; set; } = null!;

        [Column("last_name")] [MaxLength(100)] [Required] public string LastName { get; set; } = null!;

        [Column("role")] [MaxLength(70)] [Required] public string Role { get; set; } = null!;
    }
}
