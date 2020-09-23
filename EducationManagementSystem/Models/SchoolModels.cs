using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationManagementSystem.Models
{
    public class Course
    {
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Course Code")]
        public string Code { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Course Full Name")]
        public string FullName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Course Short Name")]
        public string ShortName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Course Description")]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Course Duration")]
        public int Duration { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Course Credit")]
        public int Credit { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }

    public class Category
    {
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Category Description")]
        public string Description { get; set; }
    }

    public class Topic
    {
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Topic Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Topic Description")]
        public string Description { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser Trainer { get; set; }

        public int? ClassId { get; set; }
        public virtual Class Class { get; set; }
    }

    public class Class
    {
        public int? Id { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Class Code")]
        public string Code { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Class Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Class End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public int? CourseId { get; set; }
        public virtual Course Course { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<TraineeClass> TraineeClasses { get; set; }

    }

    public class TraineeClass
    {
        public int? Id { get; set; }

        [DataType(DataType.Text)]
        public string Grade { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser Trainee { get; set; }

        public int? ClassId { get; set; }
        public virtual Class Class { get; set; }

    }
}