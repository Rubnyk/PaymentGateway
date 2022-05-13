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
            if (!Schema.Table("Merchants").Exists())
            {
                Create.Table("Merchants")
                     .WithColumn("MerchantId").AsString().PrimaryKey();
                     

                Insert.IntoTable("Merchants").Row(new
                {
                    MerchantId = "revolve"
                });
            }

            if (!Schema.Table("Transactions").Exists())
            {
                Create.Table("Transactions")
                     .WithColumn("id").AsInt64().Identity().PrimaryKey()
                     .WithColumn("merchantId").AsString().PrimaryKey()
                     .WithColumn("eventTime").AsDateTime2().NotNullable()
                     .WithColumn("transactionDate").AsString().NotNullable()
                     .WithColumn("amount").AsDecimal().NotNullable()
                     .WithColumn("errorMessage").AsString().NotNullable();
            }
        }
    }
}
