# Dapper Base Repository

## `BaseRepositoryAsync`
This was changed to be able to pass in the database configuration key.

## Index.razor.cs  	
#region DatabaseAction

## ToDo: maybe...
1. maybe this region of code can be a service, it could be an abstract base class where you have to pass 
	`LM.IRepository` or `Sukkot.IRepository`
2. maybe change the back-end to procs and return `DatabaseTuple`
3. maybe split up DatabaseTuple into Queries and Commands

# Further studies
- [Matthew Jones](https://exceptionnotfound.net/using-a-dapper-base-repository-in-c-to-improve-readability/)
