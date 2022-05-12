using FluentMigrator;
using System;

namespace PaymentGateway.Infrastructure.Persistence.Migrations
{
    [Migration(00001, "Version 0.0.1 - users")]
    public class Migration00001 : Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            if (!Schema.Table("Users").Exists())
            {
                Create.Table("Users")
                     .WithColumn("Username").AsString().PrimaryKey()
                     .WithColumn("Password").AsString().NotNullable()
                     .WithColumn("CreationDate").AsDateTime().NotNullable()
                     .WithColumn("UpdationDate").AsDateTime().NotNullable()
                     .WithColumn("ExpirationDate").AsDateTime().NotNullable();

                Insert.IntoTable("Users").Row(new
                {
                    Username = "admin",
                    Password = "MU9NdE5mRTUzMThGMHd6STBOUzJPeFdBZmVMMWZrRXn2I6eyUh2dmpKG3MXo7dOG",    //Orpak1983                
                    CreationDate = DateTime.UtcNow,
                    UpdationDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddDays(360)
                });
            }
        }
    }
}
