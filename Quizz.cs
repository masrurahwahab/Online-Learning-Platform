using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_Platform
{
    public class Quizz
    {
        public int QuizzId { get; set; }
        public int CourseId { get; set; }
        public string QuizzTitle { get; set; }
        public DateTime DateCreated { get; set; }


        //public Quizz (int quizzid , int courseid, string quizztitle, DateTime datecreated)
        //{
        //    QuizzId = quizzid;
        //    CourseId = courseid;
        //    QuizzTitle = quizztitle;
        //    DateCreated = datecreated;
        //}
        //public Quizz()
        //{

        //}

        //public override string ToString()
        //{
        //    return $"{QuizzId}\t\t|{CourseId}\t\t|{QuizzTitle}\t\t|{DateCreated}";
        //}
    }
}
