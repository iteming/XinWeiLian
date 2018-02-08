using System.Data.Entity;
using Entity.Base;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Entity
{
    public class DbHelper : DbContext
    {
        public DbHelper() : base("name=EntitiesDbContext")
        {
            //自动创建表，如果Entity有改到就更新到表结构
            Database.SetInitializer<DbHelper>(new MigrateDatabaseToLatestVersion<DbHelper, DbMigrationsConfiguration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //表名为类名，不是带s的表名  //移除复数表名的契约
        }

        internal sealed class DbMigrationsConfiguration : DbMigrationsConfiguration<DbHelper>
        {
            public DbMigrationsConfiguration()
            {
                AutomaticMigrationsEnabled = true;//任何Model Class的修改將會直接更新DB
                AutomaticMigrationDataLossAllowed = true;
            }

            protected override void Seed(DbHelper context)
            {
                //  This method will be called after migrating to the latest version.
                //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
                //  to avoid creating duplicate seed data.

                if (context.Admin.Where(f => f.UserName == "NEDT").FirstOrDefault() == null)
                {
                    context.Admin.Add(new Base.Admin { UserName = "NEDT", Password = "123456", IsPrimary = true });
                    // ... 初始化数据代码
                    context.SaveChanges();
                }
            }
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<User> User { get; set; }
    }
}
