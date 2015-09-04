using System.Data.Entity;
using CarWorkshop.Models.Concept;

namespace CarWorkshop.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Concept> Concepts { get; set; }


#if DEBUG
        public DatabaseContext()
        {
            Database.SetInitializer(new DbInitializer());
            Database.Connection.ConnectionString = @"Server=(LocalDB)\v11.0;Database=CarWorkshop;Persist Security Info=True;Integrated Security=SSPI;Connect Timeout=180";
        }
#else
        public DatabaseContext(string versionNumber)
        {
            //Database.SetInitializer(new DbInitializer());
            Database.Connection.ConnectionString = @"Server=KHC_IB_01\DEVELOPMENT;Database=SudacadV" + versionNumber + ";Persist Security Info=True;Integrated Security=SSPI;Connect Timeout=180";
        }
#endif



    }

    public class DbInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
        }
    }

}