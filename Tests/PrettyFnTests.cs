using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLitePCL.pretty;
using SQLitePCL.Functions.Pretty;

namespace Tests
{
    [TestClass]
    public class PrettyFnTests
    {
        static SQLiteDatabaseConnection db;
        string SelectSql = "SELECT {0}(vals), vals FROM test where type='{1}';";
        string GetValSql = "SELECT {0}() as vals;";
        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            SQLitePCL.Batteries.Init();
            SQLiteDatabaseConnectionBuilder dbbuilder = SQLiteDatabaseConnectionBuilder.InMemory;
            db = PrettyFn.Init(dbbuilder);
            db.Execute("CREATE TABLE test (vals text, type text);");
            db.Execute("INSERT INTO test VALUES ('" + int.MaxValue + "', 'int');");
            db.Execute("INSERT INTO test VALUES ('" + uint.MaxValue + "', 'uint');");
            db.Execute("INSERT INTO test VALUES ('" + long.MaxValue + "', 'long');");
            db.Execute("INSERT INTO test VALUES ('" + ulong.MaxValue + "', 'ulong');");
            db.Execute("INSERT INTO test VALUES ('" + byte.MaxValue + "', 'byte');");
            db.Execute("INSERT INTO test VALUES ('" + sbyte.MaxValue + "', 'sbyte');");
            db.Execute("INSERT INTO test VALUES ('" + short.MaxValue + "', 'short');");
            db.Execute("INSERT INTO test VALUES ('" + ushort.MaxValue + "', 'ushort');");
            db.Execute("INSERT INTO test VALUES ('" + float.MaxValue + "', 'float');");
            db.Execute("INSERT INTO test VALUES ('1.7976931348623157E+308', 'double');");
            db.Execute("INSERT INTO test VALUES ('" + decimal.MaxValue + "', 'decimal');");
            db.Execute("INSERT INTO test VALUES ('" + char.MaxValue + "', 'char');");
            db.Execute("INSERT INTO test VALUES ('P1Y2MT2H', 'isotimespan');");
            db.Execute("INSERT INTO test VALUES ('02:14:18', 'dotnettimespan');");
            db.Execute("INSERT INTO test VALUES (UUID(), 'guid');");
            db.Execute("INSERT INTO test VALUES ('0', 'nonpositiveint');");
            db.Execute("INSERT INTO test VALUES ('0', 'nonnegativeint');");
            db.Execute("INSERT INTO test VALUES ('1', 'positiveint');");
            db.Execute("INSERT INTO test VALUES ('-1', 'negativeint');");
            db.Execute("INSERT INTO test VALUES ('true', 'bool');");
            db.Execute("INSERT INTO test VALUES ('https', 'uri');");
            db.Execute("INSERT INTO test VALUES ('a', 'rownum');");
            db.Execute("INSERT INTO test VALUES ('b', 'rownum');");
            db.Execute("INSERT INTO test VALUES ('c', 'rownum');");
        }

        public string Query(string querytype, string datatype, string cmd)
        {
            var sql = (querytype == "GET") ? String.Format(GetValSql, cmd) : String.Format(SelectSql, cmd, datatype);
            var stmt = db.PrepareStatement(sql);
            stmt.MoveNext();
            var result = stmt.Current[0].ToString();
            //var val = stmt.Current[1].ToString();
            return result;
        }

        [TestMethod]
        public void IsIntTest()
        {
            Assert.IsTrue(Query("SELECT", "int", "ISINT") == "1");
            Assert.IsTrue(Query("SELECT", "int", "ISGUID") == "0");
        }

        [TestMethod]
        public void IsUIntTest()
        {
            Assert.IsTrue(Query("SELECT", "uint", "ISUINT") == "1");
        }

        [TestMethod]
        public void IsLongTest()
        {
            Assert.IsTrue(Query("SELECT", "long", "ISLONG") == "1");
        }

        [TestMethod]
        public void IsULongTest()
        {
            Assert.IsTrue(Query("SELECT", "ulong", "ISULONG") == "1");
        }

        [TestMethod]
        public void IsByteTest()
        {
            Assert.IsTrue(Query("SELECT", "byte", "ISBYTE") == "1");
        }

        [TestMethod]
        public void IsSbyteTest()
        {
            Assert.IsTrue(Query("SELECT", "sbyte", "ISSBYTE") == "1");
        }

        [TestMethod]
        public void IsShortTest()
        {
            Assert.IsTrue(Query("SELECT", "short", "ISSHORT") == "1");
        }

        [TestMethod]
        public void IsUShortTest()
        {
            Assert.IsTrue(Query("SELECT", "ushort", "ISUSHORT") == "1");
        }

        [TestMethod]
        public void IsFloatTest()
        {
            Assert.IsTrue(Query("SELECT", "float", "ISFLOAT") == "1");
        }

        [TestMethod]
        public void IsDecimalTest()
        {
            Assert.IsTrue(Query("SELECT", "decimal", "ISDECIMAL") == "1");
        }

        [TestMethod]
        public void IsDoubleTest()
        {
            Assert.IsTrue(Query("SELECT", "double", "ISDOUBLE") == "1");
        }

        [TestMethod]
        public void IsCharTest()
        {
            Assert.IsTrue(Query("SELECT", "char", "ISCHAR") == "1");
        }

        [TestMethod]
        public void IsIsoTimespanTest()
        {
            Assert.IsTrue(Query("SELECT", "isotimespan", "ISISOTIMESPAN") == "1");
        }

        [TestMethod]
        public void IsDotNetTimespanTest()
        {
            Assert.IsTrue(Query("SELECT", "dotnettimespan", "ISDOTNETTIMESPAN") == "1");
        }

        [TestMethod]
        public void IsNonPosIntTest()
        {
            Assert.IsTrue(Query("SELECT", "nonpositiveint", "ISNONPOSINT") == "1");
        }

        [TestMethod]
        public void IsNonNegIntTest()
        {
            Assert.IsTrue(Query("SELECT", "nonnegativeint", "ISNONNEGINT") == "1");
        }

        [TestMethod]
        public void IsPosIntTest()
        {
            Assert.IsTrue(Query("SELECT", "positiveint", "ISPOSINT") == "1");
        }

        [TestMethod]
        public void IsNegIntTest()
        {
            Assert.IsTrue(Query("SELECT", "negativeint", "ISNEGINT") == "1");
        }

        [TestMethod]
        public void IsGuidTest()
        {
            Assert.IsTrue(Query("SELECT", "guid", "ISGUID") == "1");
        }

        [TestMethod]
        public void RegexText()
        {
            var sql = "SELECT REGEXP('bearcat', 'cat')";
            var stmt = db.PrepareStatement(sql);
            stmt.MoveNext();
            var result = stmt.Current[0].ToString();
            Assert.IsTrue(result == "1");
        }

        [TestMethod]
        public void IsUriTest()
        {
            var sql = "SELECT ISURI('http://www.cint.io', 'http')";
            var stmt = db.PrepareStatement(sql);
            stmt.MoveNext();
            var result = stmt.Current[0].ToString();
            Assert.IsTrue(result == "1");
        }

        [TestMethod]
        public void DateCompareTest()
        {
            var sql = "SELECT DATECHK('20170801', 'yyyymmdd', ' > ', '20110101')";
            var stmt = db.PrepareStatement(sql);
            stmt.MoveNext();
            var result = stmt.Current[0].ToString();
            Assert.IsTrue(result == "1");
        }

        [TestMethod]
        public void CompareValsTest()
        {
            var sql = @"SELECT COMPARE('5', '>', '4', 'int'), 
                        COMPARE('5', '>', '4', 'uint'), 
                        COMPARE('5', '>', '4', 'float'), 
                        COMPARE('5', '>', '4', 'double'), 
                        COMPARE('5', '>', '4', 'decimal'), 
                        COMPARE('5', '>', '4', 'short'), 
                        COMPARE('5', '>', '4', 'ushort'), 
                        COMPARE('5', '>', '4', 'long'), 
                        COMPARE('5', '>', '4', 'ulong'), 
                        COMPARE('5', '>', '4', 'byte'), 
                        COMPARE('5', '>', '4', 'sbyte');";
            var stmt = db.PrepareStatement(sql);
            stmt.MoveNext();
            for (var i = 0; i < sql.Split(new[] { "COMPARE" }, StringSplitOptions.None).Length - 1; i++)
            {
                var result = stmt.Current[i].ToString();
                Assert.IsTrue(result == "1");
            }

        }

        [TestMethod]
        public void GetRowNumberTest()
        {
            var sql = "SELECT ROW_NUMBER(type) as rn, type FROM test where type='rownum'";
            var stmt = db.PrepareStatement(sql);
            stmt.MoveNext();
            stmt.MoveNext();
            stmt.MoveNext();
            var result = stmt.Current[0].ToString();
            Assert.IsTrue(result == "3");
        }

        [TestMethod]
        public void GetGuidTest()
        {
            var guid = Query("GET", null, "UUID");
            Guid g;
            Assert.IsTrue(Guid.TryParse(guid, out g));
        }
    }
}
