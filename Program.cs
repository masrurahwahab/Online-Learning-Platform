using Online_Learning_Platform;

Student student = new Student();
Instructor instructor = new Instructor();
//Instructor.CreateDB();
//Instructor.CreateInstructorTable();
//Instructor.CreateStudentTable();
//Instructor.CreateuserTable();
//Instructor.CreateRegisterTable();
//Instructor.CreateCourseTable();
//Instructor.CreateLessonTable();
//Instructor.CreatequestionTable();
//Instructor.CreateQuizzTable();
//Student.EnrollTable();
//Student.WalletTable();

bool hmm = true;
while (hmm)
{
    Console.WriteLine("WELCOME TO ONLINE LEARNING PLATFORM");
    Console.WriteLine("1.LOGG IN");
    Console.WriteLine("2.SIGN IN");
    Console.WriteLine("3.LOG OUT");

    Console.WriteLine("Enter anynumber from above to continue");
    string nnn = Console.ReadLine();

    switch(nnn)
    {
        case "1":

            Console.WriteLine("1.STUDENT");
            Console.WriteLine("2.INSTRUCTOR");

            Console.WriteLine("Choose any number from above");
            string vvv = Console.ReadLine();
            switch (vvv)
            {
                case "1":
                    var mmm = student.Login();
                    if (mmm != null)
                    {
                        Lesson.Students();
                    }
                    break;

                case "2":
                    var fff = student.Login();
                    if (fff != null)
                    {
                        Lesson.Instructors();
                    }
                    break;
            }
            
            break;
    }
}



   