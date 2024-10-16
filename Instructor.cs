using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Mysqlx.Crud;

namespace Online_Learning_Platform
{
    public class Instructor
    {

        private static string ConnectionStringWithoutDB = "Server = localhost; User = root; password =9594";
        private static string ConnectionString = "Server = localhost; User = root; database = OnlineLearningPlatform; password = 9594";
        Users User = new Users();

        public static void CreateDB()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionStringWithoutDB))
            {
                connection.Open();
                string query = "Create Database if not exists  OnlineLearningPlatform";

                MySqlCommand command = new MySqlCommand(query, connection);
                var execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    Console.WriteLine("Database Created Successfully.");
                }
                else
                {
                    Console.WriteLine("Unable To Create Database.");
                }
            }
        }
        public static void CreateInstructorTable()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "create table if not exists  OnlineLearningPlatform.Instructor(id int primary key not null auto_increment ,UserId int not null unique,  Name varchar(255) Not Null,  Email varbinary(200) not null unique,password varchar(200)unique,dateregistered DateTime not null ,role varchar (200)not null,Username varchar(200)unique);";

                MySqlCommand command = new MySqlCommand(query, connection);
                var execute = command.ExecuteNonQuery();

                if (execute == 0)
                {
                    Console.WriteLine("Table Created Successfully.");
                }
                else
                {
                    Console.WriteLine("Unable To Create Table.");
                }
            }
        }
        public static void CreateStudentTable()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "create table if not exists  OnlineLearningPlatform.student(id int primary key not null auto_increment,UserId int  not null unique,  Name varchar(255) Not Null,  Email varbinary(200) not null unique,password varchar(200)unique,dateregistered DateTime not null ,role varchar (200)not null,Username varchar(200)unique);";

                MySqlCommand command = new MySqlCommand(query, connection);
                var execute = command.ExecuteNonQuery();

                if (execute == 0)
                {
                    Console.WriteLine("Table Created Successfully.");
                }
                else
                {
                    Console.WriteLine("Unable To Create Table.");
                }
            }
        }
        public static void CreateuserTable()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "CREATE TABLE IF NOT EXISTS OnlineLearningPlatform.Users (\r\n    UserID INT PRIMARY KEY AUTO_INCREMENT,\r\n    Name VARCHAR(255) NOT NULL,\r\n    Email VARCHAR(255) NOT NULL UNIQUE,\r\n    Password VARCHAR(255) NOT NULL,\r\n    UserRole ENUM('Instructor', 'Student') NOT NULL,\r\n    DateCreated DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP\r\n);;";

                MySqlCommand command = new MySqlCommand(query, connection);
                var execute = command.ExecuteNonQuery();

                if (execute == 0)
                {
                    Console.WriteLine("Table Created Successfully.");
                }
                else
                {
                    Console.WriteLine("Unable To Create Table.");
                }
            }
        }
        public static void CreateRegisterTable()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "create table if not exists  OnlineLearningPlatform.register( id int primary key not null auto_increment,UserId int  not null unique,  Name varchar(255) Not Null,  Email varbinary(200) not null unique,password varchar(200)unique,dateregistered DateTime not null ,role varchar (200)not null,Username varchar(200)unique);";
                
                MySqlCommand command = new MySqlCommand(query, connection);
                var execute = command.ExecuteNonQuery();
                               
                                
                if (execute == 0)
                {
                    Console.WriteLine("Table Created Successfully.");
                }
                else
                {
                    Console.WriteLine("Unable To Create Table.");
                }
            }
        }


        public bool Register()
        {
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter your email address");
            string address = Console.ReadLine();

            Console.WriteLine("Enter your Password");
            string pass = Console.ReadLine();

            DateTime registered = DateTime.Now;

            Console.WriteLine("Enter your role ");
            string role = Console.ReadLine();

            Console.WriteLine("Enter your username");
            string username = Console.ReadLine();

            int userId;
            Random random = new Random();

            
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                do
                {
                    userId = random.Next(1, 10000);

                    
                    string checkQuery = "SELECT COUNT(*) FROM OnlineLearningPlatform.register WHERE userid = @userId";
                    MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@userId", userId);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count == 0)
                    {
                        break; 
                    }
                } while (true);
            }

            
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO OnlineLearningPlatform.register (userid, Name, Email, Password, DateRegistered, Role, Username) VALUES (@userId, @name, @address, @pass, @registered, @role, @username);";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@pass", pass);
                command.Parameters.AddWithValue("@registered", registered);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@username", username);

                var execute = command.ExecuteNonQuery();

                if (execute > 0)
                {
                    Console.WriteLine("User Registered Successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Unable to Register User.");
                    return false;
                }
            }


        }
        public static void CreateCourseTable()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
                    CREATE TABLE IF NOT EXISTS OnlineLearningPlatform.Course (
                        id INT PRIMARY KEY AUTO_INCREMENT,
                        CourseId INT NOT NULL UNIQUE,
                        InstructorID INT NOT NULL,
                        CourseTitle VARCHAR(200) NOT NULL UNIQUE,
                        Description TEXT,
                        Category VARCHAR(255),
                        Price DECIMAL(10, 2),
                        DateCreated DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        IsPublished BOOLEAN DEFAULT FALSE,
                        FOREIGN KEY (InstructorID) REFERENCES OnlineLearningPlatform.Register(UserID)
                    );";

                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table created successfully.");
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error creating table: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }
            }
        }
        public static void EditCourse()
        {

            Console.WriteLine("Enter Courseid");
            if (!int.TryParse(Console.ReadLine(), out int Courseid))
            {
                Console.WriteLine("Invalid user ID format.");
                return;
            }

            Console.WriteLine("Enter Instructor ID (must exist in Users):");
            int instructorId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Course Title:");
            string courseTitle = Console.ReadLine();

            Console.WriteLine("Enter Description:");
            string description = Console.ReadLine();

            Console.WriteLine("Enter Category:");
            string category = Console.ReadLine();

            Console.WriteLine("Enter Price:");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Please enter a valid price.");
            }

            Console.WriteLine("Is the course published? (true/false):");
            bool isPublished = bool.Parse(Console.ReadLine());


            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "UPDATE OnlineLearningPlatform.Course SET instructorid = @instructorid, coursetitle = @coursetitle, description = @description, category = @category, price = @price WHERE courseid = @courseid;";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userid",Courseid);
                command.Parameters.AddWithValue("@coursetitle",courseTitle);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@category", category);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@instructorid", instructorId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Update successful!");
                }
                else
                {
                    Console.WriteLine("No course found with the given course ID .");
                }
            }
        }
        public static void CreateQuizzTable()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = @"
                    CREATE TABLE IF NOT EXISTS OnlineLearningPlatform.Quizz (
                        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
                        QuizzID INT NOT NULL UNIQUE,
                        CourseID INT NOT NULL,
                        QuizTitle VARCHAR(255) NOT NULL,
                        DateCreated DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        CourseTitle VARCHAR(255) NOT NULL UNIQUE,
                        FOREIGN KEY (CourseID) REFERENCES OnlineLearningPlatform.Course(CourseID)
                    );";

                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table created successfully.");
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error creating table: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }
            }
        }
        public static void CreatequestionTable()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = @"
                    CREATE TABLE IF NOT EXISTS OnlineLearningPlatform.Question (
                        id INT PRIMARY KEY AUTO_INCREMENT,
                        QuizID INT,
                        QuestionText TEXT NOT NULL,
                        CorrectAnswer VARCHAR(255) NOT NULL,
                        Options TEXT,
                        FOREIGN KEY (ID) REFERENCES OnlineLearningPlatform.Quizz(ID)
                    );";

                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Question table created successfully.");
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error creating table: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }
            }
        }

        public static void DeleteCOURSE()
        {
            Console.WriteLine("Enter course id");
            if (!int.TryParse(Console.ReadLine(), out int Courseid))
            {
                Console.WriteLine("Invalid course ID format.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                
                string query = "DELETE FROM OnlineLearningPlatform.Course WHERE CourseId = @CourseId;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseId", Courseid);
                    int count = command.ExecuteNonQuery();

                    if (count > 0)
                    {
                        Console.WriteLine("Deleted Successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Unable To Delete. Course ID may not exist.");
                    }
                }
            }
        }
        public static void ViewAdminCourses(int adminId)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                // Query to select courses created by the admin
                string query = "SELECT CourseId, CourseTitle, Description, Price, DateCreated, IsPublished " +
                               "FROM OnlineLearningPlatform.Course WHERE InstructorID = @InstructorID;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InstructorID", adminId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Courses created by Admin:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"Course ID: {reader["CourseId"]}, Title: {reader["CourseTitle"]}, " +
                                                  $"Description: {reader["Description"]}, Price: {reader["Price"]}, " +
                                                  $"Date Created: {reader["DateCreated"]}, Is Published: {reader["IsPublished"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No courses found for this admin.");
                        }
                    }
                }
            }
        }

        public static int CreateQuiz()
        {
            Random random = new Random();
            int quizId = random.Next(1, 10000);

            Console.WriteLine("Enter Quiz Title:");
            string quizTitle = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(quizTitle))
            {
                Console.WriteLine("Quiz title cannot be empty.");
                return -2;
            }

            Console.WriteLine("Enter Course ID:");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid course ID format.");
                return -2;
            }

            DateTime registered = DateTime.Now;

            Console.WriteLine("Course Title:");
            string courseTitle = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(courseTitle))
            {
                Console.WriteLine("Course title cannot be empty.");
                return -2;
            }

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                        string query = @"
                INSERT INTO OnlineLearningPlatform.Quizz (QuizID, QuizTitle, CourseID, DateCreated, CourseTitle)
                VALUES (@QuizID, @QuizTitle, @CourseID, @DateCreated, @CourseTitle);";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizId);
                    command.Parameters.AddWithValue("@QuizTitle", quizTitle);
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    command.Parameters.AddWithValue("@DateCreated", registered);
                    command.Parameters.AddWithValue("@CourseTitle", courseTitle);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Quiz created successfully with QuizID: " + quizId);
                            return quizId; // Return the generated QuizID
                        }
                        else
                        {
                            Console.WriteLine("Failed to create quiz.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Error creating quiz: " + ex.Message);
                    }
                }
            }
            return -1; // Return -1 to indicate failure
        }

        public static void VIEWQuizz()
        {
            Console.WriteLine("Enter courseid");
            if (!int.TryParse(Console.ReadLine(), out int courseid))
            {
                Console.WriteLine("Invalid course ID format.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT QuizzTitle,quizzid,content From quizz where courseid = @courseid";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@courseid", courseid);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("quizz: ");
                        if (!reader.Read())
                        {
                            Console.WriteLine("quizz Not Found");
                        }
                        else
                        {

                            Console.WriteLine($"QuizzTitle: {reader["quizzTitle"]},QuizzzID: {reader["Quizzid"]}");
                        }

                    }
                }
            }
        }
        public static void quizz()
        {
            
            // After creating the quiz, allow users to answer the questions
            Console.WriteLine("Enter the Quiz ID to answer questions:");
            if (int.TryParse(Console.ReadLine(), out int quizId))
            {
                AnswerQuestions(quizId);
            }
            else
            {
                Console.WriteLine("Invalid Quiz ID.");
            }
        }

        public static List<Question> RetrieveQuestions(int quizId)
        {
            List<Question> questions = new List<Question>();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT QuestionText, CorrectAnswer, Options FROM OnlineLearningPlatform.Question WHERE QuizID = @QuizID;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@QuizID", quizId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions.Add(new Question
                            {
                                QuestionText = reader["QuestionText"].ToString(),
                                CorrectAnswer = reader["CorrectAnswer"].ToString(),
                                Options = reader["Options"].ToString()
                            });
                        }
                    }
                }
            }

            return questions;
        }
        public static void AnswerQuestions(int quizId)
        {
            List<Question> questions = RetrieveQuestions(quizId);
            if (questions.Count == 0)
            {
                Console.WriteLine("No questions found for this quiz.");
                return;
            }

            int score = 0;

            foreach (var question in questions)
            {
                Console.WriteLine(question.QuestionText);
                Console.WriteLine("Options: " + question.Options);
                Console.Write("Your answer (A/B/C/D): ");
                string userAnswer = Console.ReadLine().Trim().ToUpper();

                if (userAnswer == question.CorrectAnswer)
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine("Incorrect! The correct answer was: " + question.CorrectAnswer);
                }

                Console.WriteLine($"Current Score: {score}");
                Console.WriteLine();
            }

            Console.WriteLine($"Final Score: {score} out of {questions.Count}");
        }


        public static void InsertQuestions()
        {
            int quizId = CreateQuiz(); // Call the method to create a quiz
            if (quizId == -1) return; // Exit if quiz creation failed

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                bool addingQuestions = true;

                while (addingQuestions)
                {
                    Console.WriteLine("Enter Question Text:");
                    string questionText = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(questionText))
                    {
                        Console.WriteLine("Question text cannot be empty.");
                        continue; // Ask again
                    }

                    Console.WriteLine("Enter Correct Answer (A/B/C/D):");
                    string correctAnswer = Console.ReadLine().Trim().ToUpper();
                    if (correctAnswer.Length != 1 || !"ABCD".Contains(correctAnswer))
                    {
                        Console.WriteLine("Invalid answer format. Please enter A, B, C, or D.");
                        continue; // Ask again
                    }

                    Console.WriteLine("Enter Options (comma-separated):");
                    string options = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(options))
                    {
                        Console.WriteLine("Options cannot be empty.");
                        continue; // Ask again
                    }

                            string query = @"
                    INSERT INTO OnlineLearningPlatform.Question (QuizID, QuestionText, CorrectAnswer, Options)
                    VALUES (@QuizID, @QuestionText, @CorrectAnswer, @Options);";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@QuizID", quizId);
                        command.Parameters.AddWithValue("@QuestionText", questionText);
                        command.Parameters.AddWithValue("@CorrectAnswer", correctAnswer);
                        command.Parameters.AddWithValue("@Options", options);

                        try
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Question inserted successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Failed to insert question.");
                            }
                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine("Error inserting question: " + ex.Message);
                        }
                    }

                    Console.WriteLine("Do you want to add another question? (yes/no):");
                    string response = Console.ReadLine().Trim().ToLower();
                    addingQuestions = response == "yes";
                }
            }
        }
        public static void CreateLessonTable()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
                    CREATE TABLE IF NOT EXISTS OnlineLearningPlatform.Lesson (
                        id INT PRIMARY KEY AUTO_INCREMENT,
                        LessonId INT NOT NULL UNIQUE,
                        CourseID INT,
                        LessonTitle VARCHAR(255) NOT NULL,
                        Content TEXT,
                        FOREIGN KEY (CourseID) REFERENCES OnlineLearningPlatform.Course(CourseID)
                    );";

                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table created successfully.");
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error creating table: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }
            }
        }
        public static int CreateCOURSE()
        {
            Random random = new Random();
            int courseId = random.Next(1, 10000);

            Console.WriteLine("Enter Instructor ID:");
            int instructorId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Course Title:");
            string courseTitle = Console.ReadLine();

            Console.WriteLine("Enter Description:");
            string description = Console.ReadLine();

            Console.WriteLine("Enter Category:");
            string category = Console.ReadLine();

            decimal price;
            Console.WriteLine("Enter Price:");
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Please enter a valid price.");
            }

            Console.WriteLine("Is the course published? (true/false):");
            bool isPublished = bool.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();


                // Check if the instructor exists
                string checkInstructorQuery = "SELECT COUNT(*) FROM OnlineLearningPlatform.register WHERE UserID = @InstructorID;";
                using (MySqlCommand checkCommand = new MySqlCommand(checkInstructorQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@InstructorID", instructorId);

                    int instructorExists = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (instructorExists == 0)
                    {
                        Console.WriteLine("Instructor ID does not exist in Users table.");
                        return -1;
                    }
                }


                string query = @"
                INSERT INTO OnlineLearningPlatform.Course (CourseId, InstructorID, CourseTitle, Description, Category, Price, IsPublished)
                VALUES (@CourseId, @InstructorID, @CourseTitle, @Description, @Category, @Price, @IsPublished);";


                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseId", courseId); 
                    command.Parameters.AddWithValue("@InstructorID", instructorId); 
                    command.Parameters.AddWithValue("@CourseTitle", courseTitle);
                    command.Parameters.AddWithValue("@Description", description); 
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@IsPublished", isPublished);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Course created successfully with CourseID: " + courseId);
                            return courseId;
                        }
                        else
                        {
                            Console.WriteLine("Failed to create course.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Error creating course: " + ex.Message);
                    }
                }
            }
            return -1; // Return -1 to indicate failure
        }
        public static void InsertLESSONS()
        {
            int courseId = CreateCOURSE();
            if (courseId == -1) return; 

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                bool addingLessons = true;

                while (addingLessons)
                {
                    Console.WriteLine("Enter Course id:");
                    if (!int.TryParse(Console.ReadLine(), out int courseid))
                    {
                        Console.WriteLine("Invalid Course ID format.");
                        return;
                    }

                    Random random = new Random();
                    int LessonId = random.Next(1, 10000);

                    Console.WriteLine("Enter Lesson Content:");
                    string Content = Console.ReadLine();

                    Console.WriteLine("Enter the lesson title");
                    string lessontitle = Console.ReadLine();

                    string query = @"
                    INSERT INTO OnlineLearningPlatform.question (Lessonid,   
                                        CourseID,  
                                        LessonTitle,  
                                        Content  )
                    VALUES (@LessonId, @CourseId, @LessonTitle, @Content);";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LessonId",LessonId );
                        command.Parameters.AddWithValue("@CourseId", courseId);
                        command.Parameters.AddWithValue("@LessonTitle", lessontitle);
                        command.Parameters.AddWithValue("@Content", Content);

                        try
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Lesson inserted successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Failed to insert Lesson.");
                            }
                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine("Error inserting Lesson: " + ex.Message);
                        }
                    }

                    Console.WriteLine("Do you want to add another lesson? (yes/no):");
                    string response = Console.ReadLine().Trim().ToLower();
                    addingLessons = response == "yes";
                }
            }
        }
        public static void VIEWLESSON()
        {
            Console.WriteLine("Enter Lessonid");
            if (!int.TryParse(Console.ReadLine(), out int Lessonid))
            {
                Console.WriteLine("Invalid lesson ID format.");
                return ;
            }

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT lessonTitle,courseid,content From lesson where lessonid = @lessonid";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@lessonid", Lessonid);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Lesson: ");
                        if (!reader.Read())
                        {
                            Console.WriteLine("lesson Not Found");
                        }
                        else
                        {

                            Console.WriteLine($"LessonTitle: {reader["lessonTitle"]}, CourseID: {reader["courseid"]}, Content: {reader["content"]}");
                        }
                       
                    }
                }
            }
        }
        public static void ViewProfile()
        {
            Console.WriteLine("Enter your password");
            if (!int.TryParse(Console.ReadLine(), out int password))
            {
                Console.WriteLine("Invalid password.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT UserId ,  Name ,Email ,password ,dateregistered ,role,Username From register Where Password = @password ";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@password" , password);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Course: ");
                        if (!reader.Read())
                        {
                            Console.WriteLine("No course avaliable");
                        }
                        else
                        {

                            Console.WriteLine($"UserId: {reader["UserId"]}, Name: {reader["Name"]},Password: {reader["password"]},Dateregistered: {reader["dateregistered"]},Role: {reader["role"]}, Username: {reader["username"]}");
                        }

                    }
                }
            }
        }
        public static void ViewTheirOwnCourse()
        {
            Console.WriteLine("Enter your userid");
            if (!int.TryParse(Console.ReadLine(), out int userid))
            {
                Console.WriteLine("Invalid user id.");
                return;
            }
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = " SELECT *\r\nFROM COURSE \r\nGROUP BY USERID\r\nHAVING USERID = @USERID;\r\n ";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@USERID", userid);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Course: ");
                        if (!reader.Read())
                        {
                            Console.WriteLine("No course avaliable");
                        }
                        else
                        {

                            Console.WriteLine($"CourseID: {reader["CourseID"]}, InstructorID: {reader["InstructorID"]}, CourseTitle: {reader["CourseTitle"]},Description: {reader["Description"]},Category: {reader["Category"]},Price: {reader["Price"]},IsPublished: {reader["IsPublished"]}");
                        }

                    }
                    
                }
            }

        }
      
    }
}
