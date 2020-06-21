using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rad301ClubsV1.Models.ClubModel
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        [Display(Name = "ID")]
        public string ID { get; set; }
        [Display(Name = "Course ID")]
        public string CourseID { get; set; }
        [Display(Name = "Year")]
        public string Year { get; set; }
        [Display(Name = "CourseTitle")]
        public string CourseTitle { get; set; }

    }
}