using Rad301ClubsV1.Models.ClubModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Rad301ClubsV1.Migrations.ClubModelMigrations
{
    public class CoursesData
    {

        public List<Course> courseSeeds = new List<Course>();

        public CoursesData()
        {
            if (File.Exists(@".\Courses.csv"))
                courseSeeds = File.ReadAllLines(@".\Courses.csv")
                                               .Select(v => FromCsv(v)).ToList();
            else throw new 
                    Exception {
                Source = "Courses Data class" + @".\Courses.csv" + " does not exist",
                 
                 
                 };
        }

        public static Course FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Course CourseTestData = new Course();
            CourseTestData.ID = values[0];
            CourseTestData.CourseID = values[1];
            CourseTestData.Year = values[2];
            CourseTestData.CourseTitle = values[3];

            return CourseTestData;
        }
    }
}