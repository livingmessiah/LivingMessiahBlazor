# Meta Queries

## `SP_Help` table_name
-- Extensive Details: Table, lists of: Identities, Guilds, "Data_located_on_filegroup" , Index(s), Contstraints, 
```sql
	EXEC SP_HELP 'MySchema.Constants' 
```

## zvwForeignKeyReport
```sql
CREATE OR ALTER VIEW dbo.zvwForeignKeyReport
AS

/*
SELECT * FROM zvwForeignKeyReport
*/

SELECT 
fk.name 'FK Name',
tpar.name 'Parent Table',
colpar.name 'Parent Column',
tref.name 'Referenced Table',
colref.name 'Referenced Column',
fk.delete_referential_action_desc 'Delete Action',
fk.update_referential_action_desc 'Update Action'
FROM	sys.foreign_keys fk
INNER JOIN  sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
INNER JOIN  sys.tables tpar ON fk.parent_object_id = tpar.object_id
INNER JOIN  sys.columns colpar ON fkc.parent_object_id = colpar.object_id AND fkc.parent_column_id = colpar.column_id
INNER JOIN  sys.tables tref ON fk.referenced_object_id = tref.object_id
INNER JOIN  sys.columns colref ON fkc.referenced_object_id = colref.object_id AND fkc.referenced_column_id = colref.column_id
```

## zvwListColumns
- 
```sql
CREATE OR ALTER VIEW dbo.zvwListColumns
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


## zvwObjectSimple 
```sql
CREATE OR ALTER VIEW dbo.zvwObjectSimple
AS
/*
SELECT * FROM zvwObjectSimple ORDER BY name
SELECT * FROM zvwObjectSimple WHERE schema_name = 'MySchema' ORDER BY name
*/

SELECT TOP 2000 o.name, type_desc, s.name AS schema_name 
--, CONVERT(nvarchar(30), create_date, 101) AS CreateMDY
--, CONVERT(nvarchar(30), modify_date, 101) AS ModifyMDY
FROM sys.objects o
	INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
WHERE o.name NOT LIKE 'sys%' 
	AND o.name NOT LIKE 'sqlagent_%'
	AND o.name <> 'database_firewall_rules'
	AND [type_desc] <> 'INTERNAL_TABLE'
	AND [type_desc] <> 'SERVICE_QUEUE'
```

## zvwObjectSimpleByCreation
```sql
CREATE OR ALTER VIEW dbo.zvwObjectSimpleByCreation
AS
/*
SELECT * FROM zvwObjectSimpleByCreation 
SELECT * FROM zvwObjectSimpleByCreation WHERE schema_name = 'MySchema' ORDER BY name
*/

SELECT TOP 100 o.name, type_desc, s.name AS schema_name 
, CONVERT(nvarchar(30), create_date, 101) AS CreateMDY
, CONVERT(nvarchar(30), modify_date, 101) AS ModifyMDY
, CASE
   WHEN create_date <> modify_date    
   THEN
     CONVERT(VARCHAR, DATEDIFF(dd, create_date, modify_date)) + ' Days '
      + CONVERT(VARCHAR, DATEDIFF(hh, create_date, modify_date) % 24) + ' Hours '
      + CONVERT(VARCHAR, DATEDIFF(mi, create_date, modify_date) % 60) + ' Minutes '
   ELSE ''
 END AS CreateModDif
, CASE
  WHEN create_date = modify_date    
   THEN
     CASE 
      WHEN [type_desc] = 'VIEW' THEN 'GRANT SELECT ON ' + s.name + '.' + o.name + ' TO [USER NAME]' 
      WHEN [type_desc] = 'SQL_STORED_PROCEDURE' OR [type_desc] = 'SQL_SCALAR_FUNCTION' THEN 'GRANT EXECUTE ON ' + s.name + '.' + o.name + ' TO [USER NAME]' 
      --WHEN [type_desc] = 'SQL_INLINE_TABLE_VALUED_FUNCTION' THEN 'GRANT SELECT ON ' + s.name + '.' + o.name + ' TO [USER NAME]' 
      --WHEN [type_desc] = 'USER_TABLE' THEN 'GRANT SELECT ON ' + s.name + '.' + o.name + ' TO [USER NAME]' 
      ELSE ''
     END
   ELSE 'ALTER NEEDED'
 END AS CodeGen

FROM sys.objects o
  INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
WHERE o.name NOT LIKE 'sys%' 
 AND o.name NOT LIKE 'sqlagent_%'
 AND o.name <> 'database_firewall_rules'
 AND [type_desc] <> 'INTERNAL_TABLE'
 AND [type_desc] <> 'SERVICE_QUEUE'
ORDER BY modify_date desc```
```


## zvwObjectSimpleSchema
```sql
CREATE OR ALTER VIEW dbo.zvwObjectSimpleSchema
AS
/*
SELECT * FROM zvwObjectSimpleSchema ORDER BY name
SELECT * FROM zvwObjectSimpleSchema WHERE schema_name = 'MySchema' ORDER BY name
*/
SELECT o.name, type_desc, s.name AS schema_name 
--, CONVERT(nvarchar(30), create_date, 101) AS CreateMDY
--, CONVERT(nvarchar(30), modify_date, 101) AS ModifyMDY
FROM sys.objects o
  INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
WHERE o.name NOT LIKE 'sys%' 
  AND o.name NOT LIKE 'sqlagent_%'
  AND o.name <> 'database_firewall_rules'
  AND [type_desc] <> 'INTERNAL_TABLE'
  AND [type_desc] <> 'SERVICE_QUEUE'
```

## zvwObjectsWithNoPermissions
```sql
CREATE OR ALTER VIEW dbo.zvwObjectsWithNoPermissions
AS
/*
SELECT * FROM  dbo.zvwObjectsWithNoPermissions ORDER BY Name
*/

SELECT o.*, p.username,
CASE 
  WHEN o.type_desc = 'SQL_STORED_PROCEDURE' OR  o.type_desc = 'SQL_SCALAR_FUNCTION'
    THEN 'GRANT EXECUTE ON ' + o.schema_name + '.' + o.name + ' TO USER_NAME'
    ELSE 'GRANT SELECT ON ' + o.schema_name + '.' + o.name + ' TO USER_NAME'
END AS [Grant]
FROM zvwObjectSimpleByCreation o
LEFT OUTER JOIN zvwPermisions p ON 
  o.schema_name = [p].[Schema] AND o.name = p.Object
WHERE 
   (
    o.type_desc = 'SQL_STORED_PROCEDURE' OR  o.type_desc = 'SQL_SCALAR_FUNCTION' OR
    o.type_desc = 'VIEW' OR o.type_desc = 'SQL_INLINE_TABLE_VALUED_FUNCTION' OR 
    o.type_desc = 'USER_TABLE'
   )
AND p.username IS NULL
```

## zvwPermisions
```sql
CREATE OR ALTER VIEW dbo.zvwPermisions
AS
/*
SELECT * FROM zvwPermisions ORDER BY 1, 2, 3, 5
*/

SELECT sys.schemas.name 'Schema'
, sys.objects.name Object
, sys.database_principals.name username
, sys.database_permissions.type permissions_type
, sys.database_permissions.permission_name
, sys.database_permissions.state permission_state
, sys.database_permissions.state_desc
, state_desc + ' ' + permission_name + ' on ['+ sys.schemas.name + '].[' + sys.objects.name + '] to [' + sys.database_principals.name + ']' COLLATE LATIN1_General_CI_AS AS SQL
FROM sys.database_permissions 
  JOIN sys.objects on sys.database_permissions.major_id = sys.objects.object_id 
  JOIN sys.schemas on sys.objects.schema_id = sys.schemas.schema_id 
  JOIN sys.database_principals on sys.database_permissions.grantee_principal_id = sys.database_principals.principal_id 

```


## zvwPermisions
```sql
CREATE OR ALTER VIEW dbo.zvwPermissionsAndPrincipals
AS 
/*
SELECT principal_id, name, type_desc, authentication_type_desc, state_desc, permission_name 
FROM dbo.zvwPermissionsAndPrincipals
*/

SELECT DISTINCT pr.principal_id, pr.name, pr.type_desc, 
    pr.authentication_type_desc, pe.state_desc, pe.permission_name
FROM sys.database_principals AS pr
JOIN sys.database_permissions AS pe
    ON pe.grantee_principal_id = pr.principal_id;
```


## zvwRoleNamesWithMembers
- For the `MY_DB` db, it compares roles with members
```sql
CREATE OR ALTER VIEW dbo.zvwRoleNamesWithMembers
AS
/*
SELECT * FROM zvwRoleNamesWithMembers 
*/

SELECT
 'MY_DB' AS DB -- Replace with your DB Name
, r.name AS role_name
, m.name AS member_name 
FROM sys.database_role_members rm 
  INNER JOIN sys.database_principals r ON rm.role_principal_id = r.principal_id
  INNER JOIN sys.database_principals m ON rm.member_principal_id = m.principal_id
--where r.name = 'db_owner' and m.name != 'dbo' -- you may want to uncomment this line
```


## zvwTableRowCount
```sql
CREATE OR ALTER VIEW dbo.zvwTableRowCount
AS
/*
  SELECT TableName, RowCnt, SchemaName FROM zvwTableRowCount
*/

SELECT TOP 500
T.name AS TableName
, I.rows AS RowCnt
, SCHEMA_NAME(T.schema_id) AS SchemaName
FROM sys.tables AS T 
  INNER JOIN sys.sysindexes AS I 
    ON T.object_id = I.id AND I.indid < 2 
--ORDER BY I.rows DESC
ORDER BY SchemaName, I.name
```

## zvwUniqueKeysAndIndexes
```sql
CREATE OR ALTER VIEW dbo.zvwUniqueKeysAndIndexes
AS
/*
SELECT * FROM zvwUniqueKeysAndIndexes 
*/

select TOP 999 schema_name(t.schema_id) + '.' + t.[name] as table_view, 
  case when t.[type] = 'U' then 'Table'
    when t.[type] = 'V' then 'View'
    end as [object_type],
  case when c.[type] = 'PK' then 'Primary key'
    when c.[type] = 'UQ' then 'Unique constraint'
    when i.[type] = 1 then 'Unique clustered index'
    when i.type = 2 then 'Unique index'
    end as constraint_type, 
  c.[name] as constraint_name,
  substring(column_names, 1, len(column_names)-1) as [columns],
  i.[name] as index_name,
  case when i.[type] = 1 then 'Clustered index'
    when i.type = 2 then 'Index'
    end as index_type
from sys.objects t
  left outer join sys.indexes i
    on t.object_id = i.object_id
  left outer join sys.key_constraints c
    on i.object_id = c.parent_object_id 
    and i.index_id = c.unique_index_id
   cross apply (select col.[name] + ', '
      from sys.index_columns ic
        inner join sys.columns col
          on ic.object_id = col.object_id
          and ic.column_id = col.column_id
      where ic.object_id = t.object_id
        and ic.index_id = i.index_id
          order by col.column_id
          for xml path ('') ) D (column_names)
where is_unique = 1
and t.is_ms_shipped <> 1
order by schema_name(t.schema_id) + '.' + t.[name]
```

# Other

## `GRANT`
-- `SELECT ON`, `EXECUTE ON`
```sql
  GRANT SELECT ON  dbo.zvwListColumns TO [InserUserName]
  GRANT EXECUTE ON MySchema.stpMySproc TO InserUserName
```

# CodeGen

## `zvwRenameDefaultConstraintsCodeGen`
- [Source](https://www.techbrothersit.com/2016/05/how-to-rename-all-default-constraints.html)
- Creates code to run `EXEC sp_rename`
```sql

CREATE OR ALTER VIEW dbo.zvwRenameDefaultConstraintsCodeGen
AS

/*
SELECT CodeGen FROM dbo.zvwRenameDefaultConstraintsCodeGen
*/

 SELECT 'exec sp_rename '''
    +Schema_name(d.Schema_id)+'.' 
    + '' + d.Name + ''''
    + ',''DF_' +Schema_Name(d.schema_id)
    +'_'+t.name
    +'_'+c.name+'''' as CodeGen
FROM sys.default_constraints d
INNER JOIN sys.columns c ON
    d.parent_object_id = c.object_id
    AND d.parent_column_id = c.column_id
INNER JOIN sys.tables t ON
    t.object_id = c.object_id
```


