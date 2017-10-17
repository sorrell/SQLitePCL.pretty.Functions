# SQLitePCL.pretty.Functions

These are extension functions for SQLite that can be added to a SQLite.pretty DB via the builder.

## Example

```
public SQLiteDatabaseConnection DbConnection
{
    get
    {
        if (_dbcon == null)
        {
            SQLiteDatabaseConnectionBuilder dbbuilder;
            if (_dbname == null)
                dbbuilder = SQLiteDatabaseConnectionBuilder
                        .InMemory;
            else
                dbbuilder = SQLiteDatabaseConnectionBuilder
                        .Create(_dbname);

            _dbcon = SQLitePCL.pretty.Functions.SqliteFn.Init(dbbuilder);
        }
        return _dbcon;
    }
}
```

We can now query SQLite using the functions in this library:

```
SELECT ISINT(5);
-- Returns 1

SELECT ISINT('X');
-- Returns 0
```