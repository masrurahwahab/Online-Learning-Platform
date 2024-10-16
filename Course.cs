using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_Platform
{
    public class Course
    {
        public int CourseID { get; set; }
        public int InstructorID { get; set; }
        public string CourseTitle { get; set; }
        public TextWriter Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public Boolean IsPublished { get; set; }


        //public Course (int courseid ,int instructorid ,string courseTitle ,TextWriter description,string category,decimal price,DateTime datecreated ,Boolean ispublished)
        //{
        //    CourseID = courseid;
        //    InstructorID = instructorid;
        //    CourseTitle = courseTitle;
        //    Description = description;
        //    Category = category;
        //    Price = price;
        //    DateCreated = datecreated;
        //    IsPublished = ispublished;

        //}
        //public Course()
        //{

        //}

        //public override string ToString()
        //{
        //    return $"{CourseID}\t\t|{ InstructorID}\t\t|{CourseTitle}\t\t|{Description}\t\t|{Category}\t\t|{Price}\t\t|{DateCreated}\t\t|{IsPublished}";
        //}
    }
}
