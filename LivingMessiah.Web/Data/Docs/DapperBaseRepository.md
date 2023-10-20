# Dapper Base Repository

# ToDo create `BaseRepositoryMultiAsync` class

`RepositoryNoBaseHierarchicalQuery.cs` needs to embrace the features in `BaseRepositoryAsync` but make it handle **hierarchical queries** 
- Located at LivingMessiah.Web\Pages\Sukkot\ManageRegistration\Data\
- I think I have examples of handling multiple queries
- see `DetailAndDonationsHierarchicalQuery.cs` (LivingMessiah.Web\Pages\Sukkot\ManageRegistration\Detail\)

#### `DetailAndDonationsHierarchicalQuery.cs`
- used to be called `ReportVM.cs`

```csharp
// ...
// at the bottom of this class is how you hold multiple donations
	public IEnumerable<DonationDetailQuery>? Donations { get; set; } // = new();
```


#### `RepositoryNoBaseHierarchicalQuery.cs`
- Leverages **Dapper's** `QueryMultipleAsync` method

```csharp
MyParms = new DynamicParameters(new { Id = id });
MySql = $@" "
SELECT * FROM Sukkot.vwRegistration WHERE Id = @id;

SELECT * FROM Sukkot.vwDonationDetail WHERE RegistrationId=@Id ORDER BY Detail
";

  Detail.DetailAndDonationsHierarchicalQuery qry = new();

  using (var connection = new SqlConnection(connectionString))
  {
  	using (var multi = await connection.QueryMultipleAsync(MySql, MyParms))
  	{
    qry = await multi.ReadSingleOrDefaultAsync<Detail.DetailAndDonationsHierarchicalQuery>();
    if (qry is not null)
    {
    	var childItems = await multi.ReadAsync<DonationDetailQuery>();
    	qry.Donations = childItems.ToList();
    }
    return qry!;
  	}
  }
}
```

---

## `BaseRepositoryAsync`
This was changed to be able to pass in the database configuration key.

## Index.razor.cs  	
#region DatabaseAction

## ToDo: maybe...
1. maybe this region of code be a service, it could be an abstract base class where you have to pass 
	`LM.IRepository` or `Sukkot.IRepository`
2. maybe change the back-end to procs and return `DatabaseTuple`
3. maybe split up DatabaseTuple into Queries and Commands

# Further studies
- [Matthew Jones](https://exceptionnotfound.net/using-a-dapper-base-repository-in-c-to-improve-readability/)

# Multiple connections

## Example 1
- https://github.com/cq-panda/Vue.NetCore SEARCH SqlDapper.cs
```csharp
public async Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMultipleAsync<T1, T2>(string cmd, object param, CommandType? commandType = null, bool beginTransaction = false)
{
  return await ExecuteAsync(async (conn, dbTransaction) =>
  {
    using (SqlMapper.GridReader reader = await conn.QueryMultipleAsync(cmd, param, dbTransaction, commandType: commandType ?? CommandType.Text, commandTimeout: commandTimeout))
    {
      return (await reader.ReadAsync<T1>(), await reader.ReadAsync<T2>());
    }
  }, beginTransaction);
}
```
- https://github.dev/cq-panda/View.NetCore.SqlDapper.cs
- https://github.dev/cq-panda/Vue.NetCore/blob/8d56fb3a9303ac805bd5b5bbc1eb4628d65a94ee/.Net6%E7%89%88%E6%9C%AC/VOL.Core/Dapper/SqlDapper.cs#L300#L309


## Example 2
```csharp
public virtual async Task<(IEnumerable<T> Data
                    , TRecordCount RecordCount)> DbQueryMultipleAsync<T, TRecordCount>
                        (string sql, object parameters = null)
{
  IEnumerable<T> data = null;
  TRecordCount totalRecords = default;

  using (IDbConnection dbCon = DbConnection)
  {
    using GridReader results = await dbCon.QueryMultipleAsync(sql, parameters);
    data = await results.ReadAsync<T>();
    totalRecords = await results.ReadSingleAsync<TRecordCount>();
  }

  return (data, totalRecords);
}
```

