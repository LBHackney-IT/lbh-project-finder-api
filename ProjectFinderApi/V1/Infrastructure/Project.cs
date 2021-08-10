using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFinderApi.V1.Infrastructure
{
    [Table("pf_project")]
    public class Project
    {
        [Column("id")] [MaxLength(16)] [Key] public int Id { get; set; }

        [Column("project_name")] [MaxLength(500)] [Required] public string ProjectName { get; set; } = null!;

        [Column("description")] [MaxLength(1000)] [Required] public string Description { get; set; } = null!;

        [Column("project_contact")] [MaxLength(100)] public string? ProjectContact { get; set; }

        [Column("phase")] [MaxLength(70)] [Required] public string Phase { get; set; } = null!;

        [Column("size")] [MaxLength(70)] [Required] public string Size { get; set; } = null!;

        [Column("category")] [MaxLength(70)] public string? Category { get; set; }

        [Column("priority")] [MaxLength(50)] public string? Priority { get; set; }

        [Column("product_users")] [MaxLength(500)] public string? ProductUsers { get; set; }

        [Column("dependencies")] [MaxLength(300)] public string? Dependencies { get; set; }
    }
}
