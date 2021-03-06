﻿using NUnit.Framework;
using ServiceStack.Logging;
using ServiceStack.OrmLite.Tests.Shared;

namespace ServiceStack.OrmLite.Tests.Issues
{
    public class NoSqlLoggingIssue : OrmLiteTestBase
    {
        [Test]
        public void Does_log_SQL_Insert_for_Saves()
        {
            var sbLogFactory = new StringBuilderLogFactory();
            OrmLiteConfig.ResetLogFactory(sbLogFactory);

            using (var db = OpenDbConnection())
            {
                db.DropAndCreateTable<Person>();

                db.Save(new Person { Id = 1, FirstName = "first", LastName = "last", Age = 27 });
            }

            var sql = sbLogFactory.GetLogs();

            Assert.That(sql, Does.Contain("INSERT INTO"));
            OrmLiteConfig.ResetLogFactory(null);
        }

        [Test]
        public void Does_log_SQL_Insert_for_Saves_with_Auto_Ids()
        {
            var sbLogFactory = new StringBuilderLogFactory();
            OrmLiteConfig.ResetLogFactory(sbLogFactory);

            using (var db = OpenDbConnection())
            {
                db.DropAndCreateTable<PersonWithAutoId>();

                db.Save(new PersonWithAutoId { Id = 1, FirstName = "first", LastName = "last", Age = 27 });
            }

            var sql = sbLogFactory.GetLogs();

            Assert.That(sql, Does.Contain("INSERT INTO"));
            OrmLiteConfig.ResetLogFactory(null);
        }
    }
}