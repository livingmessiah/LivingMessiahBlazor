# Meta Queries

## `SP_Help` table_name
-- Extensive Details: Table, lists of: Identities, Guilds, "Data_located_on_filegroup" , Index(s), Contstraints, 
```sql
	EXEC SP_HELP 'Sukkot.Constants' 
```

## zvwListColumns
- 
```sql
CREATE VIEW dbo.zvwListColumns
AS
/*
	SELECT * 
	-- schema_name, table_name, column_id, column_name, data_type, max_length ,precision
	FROM zvwListColumns
	ORDER BY schema_name,  table_name, column_id;
*/

select schema_name(tab.schema_id) as schema_name,
    tab.name as table_name, 
    col.column_id,
    col.name as column_name, 
    t.name as data_type,    
    col.max_length,
    col.precision
from sys.tables as tab
    inner join sys.columns as col
        on tab.object_id = col.object_id
    left join sys.types as t
    on col.user_type_id = t.user_type_id
```


# Other

## `GRANT`
-- `SELECT ON`, `EXECUTE ON`
```sql
	GRANT SELECT ON  dbo.zvwListColumns TO [InserUserName]
  GRANT EXECUTE ON Sukkot.stpRegistrationUpdate TO InserUserName
```

```
