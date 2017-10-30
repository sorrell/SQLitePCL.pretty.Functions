# SQLitePCL.pretty.Functions

These are extension functions for SQLite that can be added to a SQLite.pretty DB via the builder.

See [the functions area of the ETLyte documentation](https://sorrell.github.io/etlyte) for further usage.

## Example

```
SQLiteDatabaseConnection DbConnection;
SQLiteDatabaseConnectionBuilder dbbuilder = SQLiteDatabaseConnectionBuilder.InMemory;
DbConnection = PrettyFn.Init(dbbuilder);
```

We can now query SQLite using the functions in this library:

```
SELECT ISINT(5);
-- Returns 1

SELECT ISINT('X');
-- Returns 0
```