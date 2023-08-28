# Feature | Database 

Some noteworthy things about this bit of code found in Features/Admin/Database

1. Project LivingMessiah.Web connects to two database (see LivingMessiah.Web.Data.Enums.Database)
2. This requires two distinct repositories code that is are exactly the same except for the constructors which bases in different [DatabaseName] values
3. Managing State: I'm not using Fluxor but I'm using the `EventCallback`

## LM.Repository and Sukkot.Repository constructors
```csharp
	public Repository(IConfiguration config, ILogger<Repository> logger) 
		: base(config, logger, DataEnumsDatabase.[DatabaseName].ConnectionStringKey)  // [DatabaseName] = LivingMessiah or Sukkot
```

## Manage State via `EventCallback`
Instead of using Fluxor which I have done with a lot of Master List /Detail scenarios, I wanted to try and do this with `EventCallback`s.

#### Advantage
- No reliance on a 3rd part library (Fluxor)
- I wanted to better understand how to do this in native Blazor.
- It repopulates the data for `Table.razor` automatically which I have not figured out how to do this with Fluxor.
- Nowhere in the code do I need to call `StateHasChanged()`. a little strange, not sure I fully understand `StateHasChanged()` ???

### Disadvantage. 
- This may work well for simple  management scenarios only.  
- It turns out I have a lot of code in `Index.razor.cs` and that doesn't smell right.  
	- Maybe this is a case for needing a service to sits between Index.cs and the `Repository.cs`s. I'm trying to avoid `Service.cs` type classes
- I'm also passing the data required by `Table.razor` as a parameter instead of just passing a filter as the parameter and letting it figure out how to get the data.

## MasterDetail component
- My template for `Index.razor` (see below is to give it high level the responsibilities
- Instead of a `Service.cs` abstraction, maybe I MasterDetail sub folder should create a component 


## Core component using `EventCallback`

Below, `Index.razor` responsibilities, I describe what Index component should look like.
I'm thinking I need to refactor it and extract out of it a `Core.razor` component.
Anyway that solution uses Fluxor which has Effects (found in `Index/IndexStore.cs`) the `Core.razor` will be significantly different for the Database solution
What I mean is that content found in Effects needs to become a database service layer (`Service.cs` or `DataService.cs`).

 
# `Index.razor` responsibilities 
- example gotten from Pages\Admin\Video

## Core component using Fluxor
ToDo: should I create `Core.razor` component?

Where i say below **Call the guts of the Feature** maybe I need a `Core.razor` thats a wrapper around the two abstraction ... **Master** and **Detail**
the parameters are...

1.` bool ShowMaster`; 
- Maybe add this `bool` as an extra field to Enums.VisibleComponent, call it `bool MasterSection` // as opposed to `DetailSection`

2. IsXsOrSm

## 1. Enforce Authorization
```html
<AuthorizeView  Roles="@Roles.AdminOrAudiovisual"> 
	<Authorized>
		 <!-- Call the guts of the Feature e.g. --> 
			@if (State!.Value.VisibleComponent == Enums.VisibleComponent.MasterList) 	
			{	
				<PageHeader /> 
				<MasterDetail />   
			}
			else 
				{
					<ShowMasterIndexButton /> <DetailPageHeader /> 
					switch State!.Value.VisibleComponent
						Enums.VisibleComponent.AddEditForm => AddEdit.Form 
						Enums.VisibleComponent.WeeklyVideosTable => WeeklyVideos.Index
				}
		
	</Authorized>
	<NotAuthorized>
		<LoginRedirectButton ReturnUrl="@Page.Index" />`
	</NotAuthorized>
</AuthorizeView>
```

## 2. Determine Media Queries 
```html
	<div class=@MediaQuery.XsOrSm.DivClass` <Foo IsXsOrSm="true" />...`
```

## 3. UI Feedback
```html
<Toaster />
```


## ToDo: maybe...
1. maybe this region of code can be a service, it could be an abstract base class where you have to pass 
	`LM.IRepository` or `Sukkot.IRepository`
2. maybe change the back-end to procs and return `DatabaseTuple`
3. maybe split up DatabaseTuple into Queries and Commands

