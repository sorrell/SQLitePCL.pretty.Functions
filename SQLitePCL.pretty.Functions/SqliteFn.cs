using System;
using System.Collections.Generic;
using SQLitePCL.core.Functions;

namespace SQLitePCL.pretty.Functions
{
    public static class SqliteFn
    {
        public static SQLiteDatabaseConnection Init(SQLiteDatabaseConnectionBuilder dbbuilder)
        {
            return dbbuilder
                .WithScalarFunc("REGEXP",       RegexIsMatch_Func)
                .WithScalarFunc("ISDATETIME",   DateIsValid_Func)
                .WithScalarFunc("ISBOOL",       IsBool_Func)
                .WithScalarFunc("ISBYTE",       IsByte_Func)
                .WithScalarFunc("ISSBYTE",      IsSbyte_Func)
                .WithScalarFunc("ISSHORT",      IsShort_Func)
                .WithScalarFunc("ISUSHORT",     IsUshort_Func)
                .WithScalarFunc("ISINT",        IsInt_Func)
                .WithScalarFunc("ISUINT",       IsUint_Func)
                .WithScalarFunc("ISLONG",       IsLong_Func)
                .WithScalarFunc("ISULONG",      IsUlong_Func)
                .WithScalarFunc("ISFLOAT",      IsFloat_Func)
                .WithScalarFunc("ISDOUBLE",     IsDouble_Func)
                .WithScalarFunc("ISDECIMAL",    IsDecimal_Func)
                .WithScalarFunc("ISCHAR",       IsChar_Func)
                .WithScalarFunc("ISGUID",       IsGuid_Func)
                .WithScalarFunc("ISISOTIMESPAN",   IsISO8610Timespan_Func)
                .WithScalarFunc("ISDOTNETTIMESPAN", IsDotNetTimespan_Func)
                .WithScalarFunc("ISURI",        IsUri_Func)
                .WithScalarFunc("DATECHK",      DateCompare_Func)
                .WithScalarFunc("COMPARE",      CompareVals_Func)
                .WithScalarFunc("UUID",         GetUuid_Func)
                .WithScalarFunc("ROW_NUMBER",   GetRowNumber_Func)
                .Build();
        }

        private static Func<ISQLiteValue, ISQLiteValue, ISQLiteValue> RegexIsMatch_Func =
            (ISQLiteValue val, ISQLiteValue regexStr) =>
            {
                return CoreFn.RegexIsMatch(Convert.ToString(val), Convert.ToString(regexStr)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue, ISQLiteValue> DateIsValid_Func =
            (ISQLiteValue val, ISQLiteValue dateFmt) =>
            {
                return CoreFn.DateIsValid(Convert.ToString(val), Convert.ToString(dateFmt)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsBool_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsBool(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue, ISQLiteValue> IsUri_Func =
            (ISQLiteValue val, ISQLiteValue uriType) =>
            {
                return CoreFn.IsUri(Convert.ToString(val), Convert.ToString(uriType)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsInt_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsInt(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsUint_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsUint(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsLong_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsLong(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsUlong_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsUlong(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsByte_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsByte(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsSbyte_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsSbyte(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsShort_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsShort(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsUshort_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsUshort(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsFloat_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsFloat(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsDouble_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsDouble(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsDecimal_Func =
           (ISQLiteValue val) =>
           {
               return CoreFn.IsDecimal(Convert.ToString(val)).ToSQLiteValue();
           };

        private static Func<ISQLiteValue, ISQLiteValue> IsChar_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsChar(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsISO8610Timespan_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsISO8601Timespan(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsDotNetTimespan_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsDotNetTimespan(Convert.ToString(val)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue, ISQLiteValue> IsGuid_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsGuid(Convert.ToString(val)).ToSQLiteValue();
            };


        private static Func<ISQLiteValue, ISQLiteValue, ISQLiteValue, ISQLiteValue, ISQLiteValue> DateCompare_Func =
            (ISQLiteValue val1, ISQLiteValue dateFmt, ISQLiteValue operation, ISQLiteValue val2) =>
            {
                return CoreFn.DateCompare(Convert.ToString(val1), Convert.ToString(dateFmt), Convert.ToString(operation), Convert.ToString(val2)).ToSQLiteValue();
            };

        // Comparison Functions
        private static Func<ISQLiteValue, ISQLiteValue, ISQLiteValue, ISQLiteValue, ISQLiteValue> CompareVals_Func =
            (ISQLiteValue a, ISQLiteValue comparator, ISQLiteValue b, ISQLiteValue colType) =>
            {
                return CoreFn.CompareVals(Convert.ToString(a), Convert.ToString(comparator), Convert.ToString(b), Convert.ToString(colType)).ToSQLiteValue();
            };

        private static Func<ISQLiteValue> GetUuid_Func = () => { return CoreFn.GetGuid().ToSQLiteValue(); };

        private static Func<ISQLiteValue, ISQLiteValue> GetRowNumber_Func = (ISQLiteValue key) =>
        {
            return CoreFn.GetRowNumber(Convert.ToString(key)).ToSQLiteValue();
        };

        // XML specific functions
        private static Func<ISQLiteValue, ISQLiteValue> IsNonPositiveInt_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsNonPositiveInt(Convert.ToString(val)).ToSQLiteValue();
            };
        private static Func<ISQLiteValue, ISQLiteValue> IsNonNegativeInt_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsNonNegativeInt(Convert.ToString(val)).ToSQLiteValue();
            };
        private static Func<ISQLiteValue, ISQLiteValue> IsPositiveInt_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsPositiveInt(Convert.ToString(val)).ToSQLiteValue();
            };
        private static Func<ISQLiteValue, ISQLiteValue> IsNegativeInt_Func =
            (ISQLiteValue val) =>
            {
                return CoreFn.IsNegativeInt(Convert.ToString(val)).ToSQLiteValue();
            };

    }

}
