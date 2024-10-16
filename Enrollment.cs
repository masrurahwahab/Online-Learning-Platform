using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_Platform
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int CourseID { get; set; }
        public int UserID { get; set; }
       public DateTime EnrollmentDate { get; set; }
        public decimal Progress { get; set; }


        //public Enrollment (int enrollmentId ,int courseId , int userId , DateTime enrollmentdate, decimal progress)
        //{
        //    EnrollmentId = enrollmentId;
        //    CourseID = courseId;
        //    UserID = userId;
        //    EnrollmentDate = enrollmentdate;
        //    Progress = progress;
        //}
        //public Enrollment()
        //{

        //}

        //public override string ToString()
        //{
        //    return $"{EnrollmentId}\t\t|{CourseID}\t\t|{UserID}\t\t|{EnrollmentDate}\t\t|{Progress}";
        //}
    }
}
