# Enums.md

Dates for the new year are established after Sukkot. 
Once the dates are finalized you can run this code gen sql and add it to the Enum files.
Therefore a database connection is not necessary when viewing this information.

## `KeyDate.tvfDateCodeGen`
- Creates output like this...
```csharp
public override DateTime Date => Convert.ToDateTime("2023-10-14"); // EnumId: 1; Heshvan | Bul
```

```sql
CREATE  OR ALTER FUNCTION KeyDate.tvfDateCodeGen  (@YearId int, @DateTypeId int)
RETURNS TABLE AS RETURN 

/*

SELECT * FROM KeyDate.tvfDateCodeGen (2024, 1) -- Month
SELECT * FROM KeyDate.tvfDateCodeGen (2024, 2) -- Feast
SELECT * FROM KeyDate.tvfDateCodeGen (2024, 3) -- Season

GRANT SELECT ON KeyDate.tvfDateCodeGen TO [INSERT-USER-HERE]

*/

SELECT EnumId,
--CONVERT(varchar, Date, 23), 
'public override DateTime Date => Convert.ToDateTime("' + CONVERT(varchar, Date, 23) + '");' + ' // EnumId: ' + CAST(EnumId AS varchar(10)) + '; ' +  + Description AS CodeGen
FROM KeyDate.Calendar 
WHERE YearId = @YearId and DateTypeId=@DateTypeId
```