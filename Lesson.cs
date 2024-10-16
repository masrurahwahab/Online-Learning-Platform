using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_Platform
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public int CourseId { get; set; }
        public string LessonTitle { get; set; }
        public TextWriter Content { get; set; }
        public int Order { get; set; }

        //public Lesson(int lessonid ,int courseid,string lessontitle,TextWriter content ,int order)
        //{
        //    LessonId = lessonid;
        //    CourseId = courseid;
        //    LessonTitle = lessontitle;
        //    Content = content;
        //    Order =order;
        //}
        //public Lesson()
        //{

        //}
        //public override string ToString()
        //{

        //    return $"{LessonId}\t\t|{CourseId}\t\t|{LessonTitle}\t\t|{Content}\t\t|{Order}";
        //}
         public static void Students()
        {


            bool cool = true;
            while (cool)
            {
                Console.WriteLine("1. PROFILE");
                Console.WriteLine("2.QUIZZ");
                Console.WriteLine("3.ENROLL");
                Console.WriteLine("4. WALLET");
                Console.WriteLine("5.COURSE");
                Console.WriteLine("6.EXIT");

                Console.WriteLine("Enter anynumber from above to continue");
                string ENT = Console.ReadLine();

                switch (ENT)
                {
                    case "1":
                        Console.WriteLine("1. VIEW PROFILE");
                        Console.WriteLine("2. UPDATE PROFILE");

                        Console.WriteLine("Enter anynumber from above to continue");
                        string enter = Console.ReadLine();

                        switch (enter)
                        {
                            case "1":
                                Instructor.ViewProfile();
                                break;

                            case "2":
                                Student.updateProfile();
                                break;

                            default:
                                Console.WriteLine("Invalid number");
                                break;
                        }
                        break;

                    case "2":
                        Console.WriteLine("1. VIEW ALL QUIZZ");
                        Console.WriteLine("2.TAKE QUIZZ");

                        Console.WriteLine("Enter anynumber from above to continue");
                        string quizz = Console.ReadLine();

                        switch (quizz)
                        {
                            case "1":
                                Student.ViewQuizz();
                                break;

                            case "2":
                                Instructor.quizz();
                                break;

                            default:
                                Console.WriteLine("Invalid number");
                                break;

                        }
                        break;

                    case "3":
                        Console.WriteLine("1.ENROLL FOR A PARTICULAR COURSE");
                        Console.WriteLine("2. VIEW ENROLL COURSE");
                        Console.WriteLine("3.DROP COURSE");

                        Console.WriteLine("Enter anynumber from above to continue");
                        string course = Console.ReadLine();

                        switch (course)
                        {
                            case "1":
                                Student.EnrollUser();
                                break;

                            case "2":
                                Student.EnrollCourse();
                                break;

                            case "3":
                                Student.DropCourse();
                                break;

                            default:
                                Console.WriteLine("Invalid number");
                                break;

                        }
                        break;

                    case "4":
                        Console.WriteLine("1.ADD FUNDS TO WALLET");
                        Console.WriteLine("2. CHECK WALLET BALANCE");

                        Console.WriteLine("Enter anynumber from above to continue");
                        string wallet = Console.ReadLine();

                        switch (wallet)
                        {
                            case "1":
                                Console.WriteLine("Kindly enter your userid");
                                if (!int.TryParse(Console.ReadLine(), out int userid))
                                {
                                    Console.WriteLine("Invalid user ID format.");
                                    return;
                                }
                                Console.WriteLine("enter deposite amount");
                                decimal amount;
                                while (!decimal.TryParse(Console.ReadLine(), out amount))
                                {
                                    Console.WriteLine("Please enter a valid price.");
                                }

                                Student.AddFundsToWallet(userid, amount);
                                break;

                            case "2":
                                Console.WriteLine("Kindly enter your userid");
                                if (!int.TryParse(Console.ReadLine(), out int courserid))
                                {
                                    Console.WriteLine("Invalid user ID format.");
                                    return;
                                }

                                Student.CheckWalletBalance(courserid);
                                break;

                            default:
                                Console.WriteLine("Invalid number");
                                break;

                        }
                        break;
                    case "5":
                        Console.WriteLine("1.VIEW ALL COURSE");
                        Console.WriteLine("2.VIEW ALL LESSONS");
                        Console.WriteLine("3.TAKE LESSONS FOR A PARTICULAR COURSE");

                        Console.WriteLine("Choose any number from above");
                        string any = Console.ReadLine();

                        switch (any)
                        {
                            case "1":
                                Student.ViewAllCourse();
                                break;

                            case "2":
                                Student.VIEWaLLLESSON();
                                break;

                            case "3":
                                Student.VIEWLESSON();
                                break;

                            default:
                                Console.WriteLine("Invalid number");
                                break;
                        }
                        break;
                    case "6":
                        cool = false;
                        break;

                    default:
                        Console.WriteLine("Invalid number");
                        break;
                }
            }

        }

        public static void Instructors()
        {
            bool odd = true;

                while (odd)
            {
                Console.WriteLine("1.PROFILE");
                Console.WriteLine("2.COURSE");
                Console.WriteLine("3. LESSON");
                Console.WriteLine("4.Quizz");
                Console.WriteLine("5.EXIT");

                Console.WriteLine("CHOOSE ANY NUMBER FROM ABOVE");
                string ins = Console.ReadLine();

                switch (ins)
                {
                    case "1":
                        Console.WriteLine("1.VIEW PROFILE");
                        Console.WriteLine("2.UPDATE PROFILE");

                        Console.WriteLine("CHOOSE ANY NUMBER FROM ABOVE");
                        string insT = Console.ReadLine();

                        switch (insT)
                        {
                            case "1":
                                Instructor.ViewProfile();
                                break;

                            case "2":
                                Student.updateProfile();
                                break;

                            default:
                                Console.WriteLine("Invalid number");
                                break;

                        }
                        break;
                    case "2":
                        Console.WriteLine("1. CREATE COURSE");
                        Console.WriteLine("2. VIEW ALL COURSE");
                        Console.WriteLine("3. VIEW ALL PERSONAL CREATED COURSE");
                        Console.WriteLine("4. EDIT COURSE");
                        Console.WriteLine("5. DELETE COURSE");

                        Console.WriteLine("CHOOSE ANY NUMBER FROM ABOVE");
                        string insTR = Console.ReadLine();

                        switch (insTR)
                        {
                            case "1":
                                Instructor.CreateCOURSE();
                                break;

                            case "2":
                                Student.ViewAllCourse();
                                break;

                            case "3":
                                Console.WriteLine("ENTER YOUR USER ID ");
                                if (!int.TryParse(Console.ReadLine(), out int userid))
                                {
                                    Console.WriteLine("Invalid user ID format.");
                                    return;
                                }
                                Instructor.ViewAdminCourses(userid);
                                break;

                            case "4":
                                Instructor.EditCourse();
                                break;

                            case "5":
                                Instructor.DeleteCOURSE();
                                break;
                            default:
                                Console.WriteLine("Invalid number");
                                break;
                        }
                        break;

                    case "3":
                        Console.WriteLine("1.INSERT LESSON INTO A PARTICULAR COURSE");
                        Console.WriteLine("2.VIEW ALL LESSON");
                        //Console.WriteLine("3.EDIT LESSON");

                        Console.WriteLine("CHOOSE ANY NUMBER FROM ABOVE");
                        string lesson = Console.ReadLine();

                        switch (lesson)
                        {
                            case "1":
                                Instructor.InsertLESSONS();
                                break;

                            case "2":
                                Student.VIEWaLLLESSON();
                                break;

                            //case "3":
                            //    Instructor.
                            //      break;
                            default:
                                Console.WriteLine("Invalid number");
                                break;
                        }
                        break;
                    case "4":
                        Console.WriteLine("1.CREATE QUIZZ");
                        Console.WriteLine("2.INSERT QUESTION");
                        Console.WriteLine("3.VIEW ALL QUIZZ");


                        Console.WriteLine("CHOOSE ANY NUMBER FROM ABOVE");
                        string QUIZZ = Console.ReadLine();

                        switch (QUIZZ)
                        {
                            case "1":
                                Instructor.CreateQuiz();
                                break;

                            case "2":
                                Instructor.InsertQuestions();
                                break;

                            case "3":
                                Instructor.VIEWQuizz();
                                break;

                            default:
                                Console.WriteLine("Invalid number");
                                break;
                        }
                        break;

                    case "5":
                        odd = false;
                        break;
                }
            }
        }
           
     }
}
