
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ProjectFinderApi.V1.Boundary.Request
{
    public class GetProjectRequest
    {
        [FromRoute]
        [Required]
        public int Id { get; set; }
    }
}
