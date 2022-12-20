using HighSchool.Data;
using HighSchool.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;

namespace HighSchool
{
    internal class Program                           // Erik Engvall NET22
    {
        static void Main(string[] args)
        {
            using var context = new HighSchoolContext();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Employees.");
                Console.WriteLine("2. Students, Class info and Grades etc.");
                Console.WriteLine("5. Add Student.");
                Console.WriteLine("6. Add Employee.");
                Console.WriteLine("0. Exit.");
                Console.Write("Enter a number to select an option: ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("1. All Employees.");
                        Console.WriteLine("2. Teachers.");
                        Console.WriteLine("0. Go back to the previous menu.");

                        Console.Write("Enter a number to select an option: ");
                        int keyInput = Convert.ToInt32(Console.ReadLine());
                        if (keyInput == 1)
                        {
                            Console.Clear();
                            SqlConnection sqlCon = new SqlConnection(@"Data Source = LAPTOP-0PAUKFKI; Initial Catalog = HighSchool; Integrated Security = true");
                            SqlDataAdapter sqlData = new SqlDataAdapter("select * from Employee", sqlCon);
                            DataTable dtTbl = new DataTable();
                            sqlData.Fill(dtTbl);

                            foreach (DataRow dr in dtTbl.Rows)
                            {
                                Console.WriteLine("Employee ID: " + dr["EmployeeId"]);
                                Console.WriteLine("Title: " + (dr["Title"]));
                                Console.WriteLine("Full Name: " + dr["FirstName"] + " " + dr["LastName"]);
                                Console.WriteLine("Social Security Number: " + dr["Ssn"]);
                                Console.WriteLine("Hire date: " + dr["HireDate"]);
                                Console.WriteLine("Salary: " + dr["Salary"]);
                                Console.WriteLine(new string('-', (30)));
                            }
                            Console.WriteLine("Press enter to continue.");
                            Console.ReadLine();
                        }

                        else if (keyInput == 2)
                        {
                            Console.Clear();
                            string teachers = "select * from Employee where Title = 'Teacher'";
                            SqlConnection sqlCon = new SqlConnection(@"Data Source = LAPTOP-0PAUKFKI; Initial Catalog = HighSchool; Integrated Security = true");
                            SqlDataAdapter dataSql = new SqlDataAdapter(teachers, sqlCon);
                            DataTable dtTbl = new DataTable();
                            dataSql.Fill(dtTbl);

                            foreach (DataRow dr in dtTbl.Rows)
                            {
                                Console.WriteLine("Employee ID: " + dr["EmployeeId"]);
                                Console.WriteLine("Title: " + (dr["Title"]));
                                Console.WriteLine("Full Name: " + dr["FirstName"] + " " + dr["LastName"]);
                                Console.WriteLine("Social Security Number: " + dr["Ssn"]);
                                Console.WriteLine("Hire date: " + dr["HireDate"]);
                                Console.WriteLine("Salary: " + dr["Salary"]);
                                Console.WriteLine(new string('-', (30)));
                            }
                            Console.WriteLine("Press enter to continue.");
                            Console.ReadLine();

                        }

                        else if (keyInput == 0)
                        {
                            break;
                        }
                    }
                }

                else if (input == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Showing all students.");
                    Console.WriteLine(new string('-', (50)));

                    var myStudents = context.Students.OrderByDescending(s => s.FirstName);
                    foreach (var item in myStudents)
                    {
                        Console.WriteLine(item.FirstName + " " + item.LastName + " " + item.Ssn + " " + item.Class);
                        Console.WriteLine(new string('-', (50)));
                    }
                    Console.WriteLine("Press enter to continue to see all the classes that are active.");
                    Console.ReadLine();
                    Console.Clear();

                    var myStudents2 = context.Students.Select(s => new { s.Class }).Distinct();
                    Console.WriteLine("The active classes");
                    Console.WriteLine(new string('-', (30)));
                    foreach (var item in myStudents2)
                    {
                        Console.WriteLine(item.Class);
                    }
                    Console.WriteLine("Press enter to continue to see the info about the Social class.");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Social class");
                    Console.WriteLine(new string('-', (30)));

                    var myStudents3 = from student in context.Students
                                      where student.Class == "Social"
                                      select student;

                    foreach (var item in myStudents3)
                    {
                        Console.WriteLine(item.FirstName + " " + item.LastName + " " + item.Class);
                        Console.WriteLine(new string('-', (30)));
                    }


                    Console.WriteLine("Press enter to continue to see all the recent grades within the last month.");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Recent Grades");
                    Console.WriteLine(new string('-', (50)));

                    SqlConnection sqlCon = new SqlConnection(@"Data Source = LAPTOP-0PAUKFKI; Initial Catalog = HighSchool; Integrated Security = true");
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From Grade where SetDate > '2022-11-30' ", sqlCon);
                    DataTable dtTbl = new DataTable();
                    sqlDataAdapter.Fill(dtTbl);

                    foreach (DataRow item in dtTbl.Rows)
                    {
                        Console.WriteLine("Grade ID: " + item["GradeId"]);
                        Console.WriteLine("Subject: " + item["Subject"]);
                        Console.WriteLine("Grade: " + item["Grade"]);
                        Console.WriteLine("Set date: " + item["SetDate"]);
                        Console.WriteLine("Student ID: " + item["Fk_StudentId"]);
                        Console.WriteLine("Student name: " + item["StudentName"]);
                        Console.WriteLine("Employee ID: " + item["Fk_EmployeeId"]);
                        Console.WriteLine("Employee name: " + item["EmployeeName"]);
                        Console.WriteLine(new string('-', (50)));
                    }

                    Console.WriteLine("Press enter to continue to see the summary of all grades.");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Showing Average, Highest and Lowest grade in each subject.");
                    Console.WriteLine(new string('-', (75)));

                    SqlConnection sqlCons = new SqlConnection(@"Data Source = LAPTOP-0PAUKFKI; Initial Catalog = HighSchool; Integrated Security = true");
                    SqlDataAdapter SqlGrade = new SqlDataAdapter("Select Subject, AVG(Grade) as 'Average grade', MAX(Grade) as 'Highest grade', MIN(Grade) as 'Lowest grade' From Grade Group By Subject", sqlCon);
                    DataTable gradeTbl = new DataTable();
                    SqlGrade.Fill(gradeTbl);

                    foreach (DataRow item in gradeTbl.Rows)
                    {
                        Console.WriteLine("Subject: " + item["Subject"]);
                        Console.WriteLine("Average grade: " + item["Average grade"]);
                        Console.WriteLine("Highest grade: " + item["Highest grade"]);
                        Console.WriteLine("Lowest grade: " + item["Lowest grade"]);
                        Console.WriteLine(new string('-', (50)));
                    }

                    Console.WriteLine("Press enter to continue to the menu.");
                    Console.ReadLine();
                }

                else if (input == "5")
                {
                    Console.Clear();
                    Console.Write("Enter FirstName: ");
                    string fName = Console.ReadLine();
                    Console.Write("Enter LastName: ");
                    string lName = Console.ReadLine();
                    Console.Write("Enter SSN: ");
                    string stuSsn = Console.ReadLine();
                    Console.Write("Enter Class: ");
                    string stuClass = Console.ReadLine();


                    SqlConnection sqlCon = new SqlConnection(@"Data Source= LAPTOP-0PAUKFKI; Initial Catalog= HighSchool; Integrated Security= true");
                    string addNew = "Insert Into Student (FirstName, LastName, SSN, Class) " +
                        "values (@FirstName, @LastName, @SSN, @Class) ";
                    using (SqlCommand cmd = new SqlCommand(addNew, sqlCon))
                    {
                        cmd.Parameters.Add(@"FirstName", SqlDbType.NVarChar, 50).Value = fName;
                        cmd.Parameters.Add(@"LastName", SqlDbType.NVarChar, 50).Value = lName;
                        cmd.Parameters.Add(@"SSN", SqlDbType.NVarChar, 12).Value = stuSsn;
                        cmd.Parameters.Add(@"Class", SqlDbType.NVarChar, 15).Value = stuClass;

                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                    Console.WriteLine("Student succesfully added.");
                    Console.WriteLine("Press enter to continue to the menu.");
                    Console.ReadLine();
                }

                else if (input == "6")
                {
                    Employee E1 = new Employee();
                    Console.Write("Enter Employee Id: ");
                    int emploid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter FirstName: ");
                    string fName = Console.ReadLine();
                    Console.Write("Enter LastName: ");
                    string lName = Console.ReadLine();
                    Console.Write("Enter SSN: ");
                    string ssn = Console.ReadLine();
                    Console.Write("Enter Hire date:  ");
                    DateTime hiDate = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Enter Salary: ");
                    decimal salary = Convert.ToDecimal(Console.ReadLine());

                    E1.EmployeeId = emploid;
                    E1.Title = title;
                    E1.FirstName = fName;
                    E1.LastName = lName;
                    E1.Ssn = ssn;
                    E1.HireDate = hiDate;
                    E1.Salary = salary;
                    context.Employees.Add(E1);
                    context.SaveChanges();
                    Console.WriteLine("Employee succesfully added.");
                    Console.WriteLine("Press enter to continue to the menu.");
                    Console.ReadLine();
                }

                else if (input == "0")
                {
                    Console.WriteLine("Closing menu");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }

            }

        }
    }
}