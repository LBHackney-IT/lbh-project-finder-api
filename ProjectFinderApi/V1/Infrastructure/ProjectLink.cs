using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFinderApi.V1.Infrastructure
{
    [Table("pf_project_links")]
    public class ProjectLink
    {
        [Column("id")] [MaxLength(16)] [Key] public int Id { get; set; }

        [Column("project_id")] [MaxLength(16)] [Required] public int ProjectId { get; set; }

        [Column("link_title")] [MaxLength(100)] [Required] public string LinkTitle { get; set; } = null!;

        [Column("link")] [MaxLength(1000)] [Required] public string Link { get; set; } = null!;

        //nav props
        public Project Project { get; set; } = null!;
    }
}
