namespace Rad301ClubsV1.Migrations.ClubModelMigrations
{
    using CsvHelper;
    using Models.ClubModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<Rad301ClubsV1.Models.ClubModel.ClubContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ClubModelMigrations";
        }

        protected override void Seed(Rad301ClubsV1.Models.ClubModel.ClubContext context)
        {
            seedClubs(context);
            //context.Clubs.AddOrUpdate(c => c.ClubName, seedClubs(context)
            //new Club { ClubName = "The Chess Club", CreationDate = DateTime.Now };
       
            

            //seedStudents(context);
            //try
            //{
            //    StudentData testStudents = new StudentData();
            //}
            //catch(Exception e) {
            //    throw new Exception { Source = e.Message };
            //}
        }

        private void seedClubs(ClubContext context)
        {
            //List<Member> selectedMembers = SeedMembers(context);
            #region Club1
            context.Clubs.AddOrUpdate(c => c.ClubName,
             new Club
             {
                 ClubName = "The Tiddly Winks Club",
                 CreationDate = DateTime.Now,
                 adminID = 1,
                 clubMembers = SeedMembers(context),
                 clubEvents = new List<ClubEvent>()
                 {	
                     // Create a new ClubEvent 
                        new ClubEvent
                        { StartDateTime = DateTime.Now.Subtract( new TimeSpan(5,0,0,0,0)),
                           EndDateTime = DateTime.Now.Subtract( new TimeSpan(5,0,0,0,0)),
                           Location="Sligo", Venue="Arena",
                           //attendees = selectedMembers // You’ll need to comment this out on subsequent seeds
                        },
                        new ClubEvent
                        { StartDateTime = DateTime.Now.Subtract( new TimeSpan(3,0,0,0,0)),
                           EndDateTime = DateTime.Now.Subtract( new TimeSpan(3,0,0,0,0)),
                           Location="Sligo", Venue="Main Canteen"
                        },
                }
             }
             );
            #endregion
            context.SaveChanges(); // NOTE EF will update the relevant foreign key fields in the clubs, club events and member tables based on the attributes
        }
        private static List<Member> SeedMembers(ClubContext context)
        {
         List<Member> selectedMembers = new List<Member>();

        var randomSetStudent = context.members.Select(s => new { s.StudentID, r = Guid.NewGuid() });
            List<string> subset = randomSetStudent.OrderBy(s => s.r).Select(s => s.StudentID.ToString()).Take(10).ToList();
            foreach (string s in subset)
            {
                selectedMembers.Add(
                   new Member { StudentID = s}
                    //context.members.First(st => st.memberID.ToString() == s)
                   );
            }
            return selectedMembers;
        }

        public void seedStudents(Rad301ClubsV1.Models.ClubModel.ClubContext context)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "Rad301ClubsV1.Migrations.ClubModelMigrations.TestStudents.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.HasHeaderRecord = false;
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    var testStudents = csvReader.GetRecords<Student>().ToArray();
                    context.Students.AddOrUpdate(s => s.StudentID, testStudents);
                }
            }
        }
    }
}
