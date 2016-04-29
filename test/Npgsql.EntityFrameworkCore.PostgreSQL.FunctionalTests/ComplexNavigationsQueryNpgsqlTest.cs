﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Npgsql.EntityFrameworkCore.PostgreSQL.FunctionalTests.Utilities;
using Xunit.Abstractions;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.FunctionalTests
{
    public class ComplexNavigationsQueryNpgsqlTest : ComplexNavigationsQueryTestBase<NpgsqlTestStore, ComplexNavigationsQueryNpgsqlFixture>
    {
        public ComplexNavigationsQueryNpgsqlTest(ComplexNavigationsQueryNpgsqlFixture fixture)
            : base(fixture)
        {
        }

        public override void Multi_level_include_one_to_many_optional_and_one_to_many_optional_produces_valid_sql()
        {
            base.Multi_level_include_one_to_many_optional_and_one_to_many_optional_produces_valid_sql();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
ORDER BY ""e"".""Id""

SELECT ""l"".""Id"", ""l"".""Level1_Optional_Id"", ""l"".""Level1_Required_Id"", ""l"".""Name"", ""l"".""OneToMany_Optional_InverseId"", ""l"".""OneToMany_Optional_Self_InverseId"", ""l"".""OneToMany_Required_InverseId"", ""l"".""OneToMany_Required_Self_InverseId"", ""l"".""OneToOne_Optional_PK_InverseId"", ""l"".""OneToOne_Optional_SelfId""
FROM ""Level2"" AS ""l""
INNER JOIN (
    SELECT DISTINCT ""e"".""Id""
    FROM ""Level1"" AS ""e""
) AS ""e0"" ON ""l"".""OneToMany_Optional_InverseId"" = ""e0"".""Id""
ORDER BY ""e0"".""Id"", ""l"".""Id""

SELECT ""l0"".""Id"", ""l0"".""Level2_Optional_Id"", ""l0"".""Level2_Required_Id"", ""l0"".""Name"", ""l0"".""OneToMany_Optional_InverseId"", ""l0"".""OneToMany_Optional_Self_InverseId"", ""l0"".""OneToMany_Required_InverseId"", ""l0"".""OneToMany_Required_Self_InverseId"", ""l0"".""OneToOne_Optional_PK_InverseId"", ""l0"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l0""
INNER JOIN (
    SELECT DISTINCT ""e0"".""Id"", ""l"".""Id"" AS ""Id0""
    FROM ""Level2"" AS ""l""
    INNER JOIN (
        SELECT DISTINCT ""e"".""Id""
        FROM ""Level1"" AS ""e""
    ) AS ""e0"" ON ""l"".""OneToMany_Optional_InverseId"" = ""e0"".""Id""
) AS ""l1"" ON ""l0"".""OneToMany_Optional_InverseId"" = ""l1"".""Id0""
ORDER BY ""l1"".""Id"", ""l1"".""Id0""",
                Sql);
        }

        public override void Multi_level_include_correct_PK_is_chosen_as_the_join_predicate_for_queries_that_join_same_table_multiple_times()
        {
            base.Multi_level_include_correct_PK_is_chosen_as_the_join_predicate_for_queries_that_join_same_table_multiple_times();

            Assert.Equal(
                @"SELECT ""e"".""Id"", ""e"".""Name"", ""e"".""OneToMany_Optional_Self_InverseId"", ""e"".""OneToMany_Required_Self_InverseId"", ""e"".""OneToOne_Optional_SelfId""
FROM ""Level1"" AS ""e""
ORDER BY ""e"".""Id""

SELECT ""l"".""Id"", ""l"".""Level1_Optional_Id"", ""l"".""Level1_Required_Id"", ""l"".""Name"", ""l"".""OneToMany_Optional_InverseId"", ""l"".""OneToMany_Optional_Self_InverseId"", ""l"".""OneToMany_Required_InverseId"", ""l"".""OneToMany_Required_Self_InverseId"", ""l"".""OneToOne_Optional_PK_InverseId"", ""l"".""OneToOne_Optional_SelfId""
FROM ""Level2"" AS ""l""
INNER JOIN (
    SELECT DISTINCT ""e"".""Id""
    FROM ""Level1"" AS ""e""
) AS ""e0"" ON ""l"".""OneToMany_Optional_InverseId"" = ""e0"".""Id""
ORDER BY ""e0"".""Id"", ""l"".""Id""

SELECT ""l0"".""Id"", ""l0"".""Level2_Optional_Id"", ""l0"".""Level2_Required_Id"", ""l0"".""Name"", ""l0"".""OneToMany_Optional_InverseId"", ""l0"".""OneToMany_Optional_Self_InverseId"", ""l0"".""OneToMany_Required_InverseId"", ""l0"".""OneToMany_Required_Self_InverseId"", ""l0"".""OneToOne_Optional_PK_InverseId"", ""l0"".""OneToOne_Optional_SelfId"", ""l2"".""Id"", ""l2"".""Level1_Optional_Id"", ""l2"".""Level1_Required_Id"", ""l2"".""Name"", ""l2"".""OneToMany_Optional_InverseId"", ""l2"".""OneToMany_Optional_Self_InverseId"", ""l2"".""OneToMany_Required_InverseId"", ""l2"".""OneToMany_Required_Self_InverseId"", ""l2"".""OneToOne_Optional_PK_InverseId"", ""l2"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l0""
INNER JOIN (
    SELECT DISTINCT ""e0"".""Id"", ""l"".""Id"" AS ""Id0""
    FROM ""Level2"" AS ""l""
    INNER JOIN (
        SELECT DISTINCT ""e"".""Id""
        FROM ""Level1"" AS ""e""
    ) AS ""e0"" ON ""l"".""OneToMany_Optional_InverseId"" = ""e0"".""Id""
) AS ""l1"" ON ""l0"".""OneToMany_Optional_InverseId"" = ""l1"".""Id0""
INNER JOIN ""Level2"" AS ""l2"" ON ""l0"".""OneToMany_Required_InverseId"" = ""l2"".""Id""
ORDER BY ""l1"".""Id"", ""l1"".""Id0"", ""l2"".""Id""

SELECT ""l3"".""Id"", ""l3"".""Level2_Optional_Id"", ""l3"".""Level2_Required_Id"", ""l3"".""Name"", ""l3"".""OneToMany_Optional_InverseId"", ""l3"".""OneToMany_Optional_Self_InverseId"", ""l3"".""OneToMany_Required_InverseId"", ""l3"".""OneToMany_Required_Self_InverseId"", ""l3"".""OneToOne_Optional_PK_InverseId"", ""l3"".""OneToOne_Optional_SelfId""
FROM ""Level3"" AS ""l3""
INNER JOIN (
    SELECT DISTINCT ""l1"".""Id"", ""l1"".""Id0"", ""l2"".""Id"" AS ""Id1""
    FROM ""Level3"" AS ""l0""
    INNER JOIN (
        SELECT DISTINCT ""e0"".""Id"", ""l"".""Id"" AS ""Id0""
        FROM ""Level2"" AS ""l""
        INNER JOIN (
            SELECT DISTINCT ""e"".""Id""
            FROM ""Level1"" AS ""e""
        ) AS ""e0"" ON ""l"".""OneToMany_Optional_InverseId"" = ""e0"".""Id""
    ) AS ""l1"" ON ""l0"".""OneToMany_Optional_InverseId"" = ""l1"".""Id0""
    INNER JOIN ""Level2"" AS ""l2"" ON ""l0"".""OneToMany_Required_InverseId"" = ""l2"".""Id""
) AS ""l20"" ON ""l3"".""OneToMany_Optional_InverseId"" = ""l20"".""Id1""
ORDER BY ""l20"".""Id"", ""l20"".""Id0"", ""l20"".""Id1""",
                Sql);
        }

        [Fact(Skip= "https://github.com/aspnet/EntityFramework/issues/5215")]
        public override void OrderBy_nav_prop_reference_optional() {}

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}
