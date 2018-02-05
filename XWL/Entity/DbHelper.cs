using System.Data.Entity;
using Entity.Base;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Entity
{
    public class DbHelper : DbContext
    {
        public DbHelper() : base("EntitiesDbContext")
        {
            //自动创建表，如果Entity有改到就更新到表结构
            Database.SetInitializer<DbHelper>(new MigrateDatabaseToLatestVersion<DbHelper, DbMigrationsConfiguration>());

            // 在使用context前调用一次
            Database.SetInitializer<DbHelper>(new MyDatabaseInitializer());
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
        }

        public class MyDatabaseInitializer : CreateDatabaseIfNotExists<DbHelper>
        {
            protected override void Seed(DbHelper context)
            {
                context.Admin.Add(new Base.Admin { UserName = "jiazk", Password = "1", IsPrimary = true });
                // ... 初始化数据代码
                context.SaveChanges();
            }
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<User> User { get; set; }
    }
}
