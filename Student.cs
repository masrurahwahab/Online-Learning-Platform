using Microsoft.Azure.Amqp.Framing;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Online_Learning_Platform
{
    public class Student
    {

        private static string ConnectionStringWithoutDB = "Server = localhost; User = root; password =9594";
        private static string ConnectionString = "Server = localhost; User = root; database = OnlineLearningPlatform; password = 9594";

        public  bool Login()
        {
            Console.WriteLine("Enter your user name");
            string userid = Console.ReadLine(); 

            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                
                string query = "SELECT COUNT(*) FROM OnlineLearningPlatform.register WHERE username = @username AND Password = @password;";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", userid);
                command.Parameters.AddWithValue("@password", password);
               
                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    Console.WriteLine("Login successful!");
                    return true;
                }
                else
                {
                    Console.WriteLine("invalid credentials.");
                    return false;
                }
            }
            
        }
        public static void updateProfile()
        {
            Console.WriteLine("Enter your userid");
            if (!int.TryParse(Console.ReadLine(), out int userid))
            {
                Console.WriteLine("Invalid user ID format.");
                return;
            }

            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter your email address");
            string address = Console.ReadLine();

            Console.WriteLine("Enter your Password");
            string pass = Console.ReadLine();


            Console.WriteLine("Enter your role ");
            string role = Console.ReadLine();

            Console.WriteLine("Enter your username");
            string username = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "UPDATE OnlineLearningPlatform.register SET name = @name, email = @address, password = @password, role = @role, username = @username WHERE userid = @userid;";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@password",pass);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@username", username);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Update successful!");
                }
                else
                {
                    Console.WriteLine("No user found with the given user ID .");
                }
            }
        }
        public static void ViewAllCourse()
        {
            

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT*From Course";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                   

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Course: ");
                        if (!reader.Read())
                        {
                            Console.WriteLine("No course avaliable");
                        }
                        else
                        {

                            Console.WriteLine($"CourseId: {reader["CourseId"]}, InstructorID: {reader["InstructorID"]}, CourseTitle: {reader["CourseTitle"]},Descripton: {reader["Description"]}, Category: {reader["Category"]}, price: {reader["price"]}, IsPublished: {reader["Ispublished"]}");
                        }

                    }
                }
               
            }
            
        }
        public static void EnrollTable()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "create table if not exists  OnlineLearningPlatform.Enroll(EnrollmentID INT PRIMARY KEY AUTO_INCREMENT,  \r\n    CourseID INT,  \r\n    UserID INT,  \r\n    EnrollmentDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  \r\n    Progress DECIMAL(5, 2) DEFAULT 0,  \r\n    FOREIGN KEY (CourseID) REFERENCES Course(CourseID),  \r\n    FOREIGN KEY (UserID) REFERENCES Users(UserID) );";

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
        public static decimal GetCoursePrice(int courseId)
        {
            decimal price = -1; // Default to -1 to indicate not found

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Price FROM OnlineLearningPlatform.Course WHERE CourseID = @CourseID;";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseID", courseId);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            price = Convert.ToDecimal(result);
                        }
                        else
                        {
                            Console.WriteLine("Course not found.");
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error retrieving course price: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }
            }

            return price; // Return the price (or -1 if not found)
        }
        public static void EnrollUser()
        {
            Console.WriteLine("Enter User ID:");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Invalid User ID format.");
                return;
            }

            Console.WriteLine("Enter Course ID:");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid Course ID format.");
                return;
            }

            // Get course price
            decimal coursePrice = GetCoursePrice(courseId);
            if (coursePrice == null) return; // Exit if course was not found

            // Check wallet balance
            decimal walletBalance = CheckWalletBalance(userId);
            Console.WriteLine($"Your wallet balance is: {walletBalance:C}");

            if (walletBalance < coursePrice)
            {
                Console.WriteLine("Insufficient funds in wallet for enrollment.");
                return;
            }

            // Deduct course price from wallet
            AddFundsToWallet(userId, -coursePrice);

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO OnlineLearningPlatform.Enroll (CourseID, UserID) VALUES (@CourseID, @UserID);";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    command.Parameters.AddWithValue("@UserID", userId);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Enrollment successful!");
                        }
                        else
                        {
                            Console.WriteLine("Enrollment failed.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Error during enrollment: " + ex.Message);
                    }
                }
            }
        }
        public static void EnrollCourse()
        {
            Console.WriteLine("Enter User ID to view enrolled courses:");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                ViewEnrolledCourses(userId);
            }
            else
            {
                Console.WriteLine("Invalid User ID format.");
            }
        }

        public static void ViewEnrolledCourses(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                // SQL query to get the enrolled courses for a specific user
                   string query = @"
                    SELECT c.CourseID, c.CourseName, c.Price
                    FROM OnlineLearningPlatform.Enroll e
                    JOIN OnlineLearningPlatform.Course c ON e.CourseID = c.CourseID
                    WHERE e.UserID = @UserID;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No enrolled courses found for this user.");
                            return;
                        }

                        Console.WriteLine($"Enrolled Courses for User ID {userId}:");
                        while (reader.Read())
                        {
                            int courseId = reader.GetInt32("CourseID");
                            string courseName = reader.GetString("CourseName");
                            decimal price = reader.GetDecimal("Price");

                            Console.WriteLine($"Course ID: {courseId}, Course Name: {courseName}, Price: {price:C}");
                        }
                    }
                }
            }
        }
        public static void VIEWLESSON()
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

                string selectQuery = "SELECT lessonTitle,courseid,content From lesson where courseid = @courseid";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@courseid", courseid);

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
        public static void VIEWaLLLESSON()
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

                string selectQuery = "SELECT lessonID,LESSONTitle From lesson where courseid = @courseid";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@courseid", courseid);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Lesson: ");
                        if (!reader.Read())
                        {
                            Console.WriteLine("lesson Not Found");
                        }
                        else
                        {

                            Console.WriteLine($"LessonTitle: {reader["lessonTitle"]}, LESSONID: {reader["LESSONID"]}");
                        }

                    }
                }
            }
        }
        public static void ViewQuizz()
        {
            
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * From quizz";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        
                        if (!reader.Read())
                        {
                            Console.WriteLine("No quizz at the moment");
                        }
                        else
                        {

                            Console.WriteLine($"QuizzID: {reader["QuizzID"]}, CourseID: {reader["courseid"]}, CourseTitle: {reader["CourseTitle"]},DateCreated: {reader["DateCreated"]},QuizzTitle: {reader["QuizzTitle"]}");
                        }

                    }
                }
            }
        }
       public static void TakeQuizz()
       {
            Console.WriteLine("Enter the quizzid");
            if (!int.TryParse(Console.ReadLine(), out int quizzid))
            {
                Console.WriteLine("Invalid Quizz ID format.");
                return;
            }
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT  From question";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {


                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Lesson: ");
                        if (!reader.Read())
                        {
                            Console.WriteLine("lesson Not Found");
                        }
                        else
                        {

                            Console.WriteLine($"QuizzID: {reader["QuizzID"]}, CourseID: {reader["courseid"]}, CourseTitle: {reader["CourseTitle"]},DateCreated: {reader["DateCreated"]},QuizzTitle: {reader["QuizzTitle"]}");
                        }

                    }
                }
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
        
       
        public static void WalletTable()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "\r\nCREATE TABLE IF NOT EXISTS UserWallet (\r\n    WalletID INT PRIMARY KEY AUTO_INCREMENT,\r\n    UserID INT UNIQUE,\r\n    Balance DECIMAL(10, 2) DEFAULT 0.00,\r\n    FOREIGN KEY (UserID) REFERENCES Users(UserID)\r\n);;";

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
        public static void AddFundsToWallet(int userId, decimal amount)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO OnlineLearningPlatform.UserWallet (UserID, Balance) VALUES (@UserID, @Balance) " +
                               "ON DUPLICATE KEY UPDATE Balance = Balance + @Balance;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@Balance", amount);

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"Successfully added {amount:C} to wallet.");
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Error adding funds: " + ex.Message);
                    }
                }
            }
        }
      
        public static decimal CheckWalletBalance(int user)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Balance FROM OnlineLearningPlatform.UserWallet WHERE UserID = @UserID;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", user);
                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }
        }
        public static void DropCourse(int userId, int courseId)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                // SQL query to delete the enrollment record
                string query = "DELETE FROM OnlineLearningPlatform.Enroll WHERE UserID = @UserID AND CourseID = @CourseID;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@CourseID", courseId);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Successfully dropped the course.");
                        }
                        else
                        {
                            Console.WriteLine("No enrollment found for this course.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Error dropping course: " + ex.Message);
                    }
                }
            }
        }

        public static void DropCourse()
        {
            Console.WriteLine("Enter User ID to drop a course:");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Enter Course ID to drop:");
                if (int.TryParse(Console.ReadLine(), out int courseId))
                {
                    DropCourse(userId, courseId);
                }
                else
                {
                    Console.WriteLine("Invalid Course ID format.");
                }
            }
            else
            {
                Console.WriteLine("Invalid User ID format.");
            }
            Console.WriteLine("You have successfully drop the course");

        }

    }
}




