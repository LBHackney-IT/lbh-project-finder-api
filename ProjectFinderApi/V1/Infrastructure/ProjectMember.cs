using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFinderApi.V1.Infrastructure
{
    [Table("pf_project_members")]
    public class ProjectMember
    {
        [Column("id")] [MaxLength(16)] [Key] public int Id { get; set; }

        [Column("project_id")] [MaxLength(16)] [Required] public int ProjectId { get; set; }

        [Column("user_id")] [MaxLength(16)] [Required] public int UserId { get; set; }

        [Column("project_role")] [MaxLength(100)] public string ProjectRole { get; set; } = null!;

        //nav props
        public Project Project { get; set; } = null!;

        public User User { get; set; } = null!;

    }
}
