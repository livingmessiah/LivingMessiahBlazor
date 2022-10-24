namespace LivingMessiah.Web.Store.Counter;

[FeatureState]
public class CounterState
{
	public int ClickCount { get; }  // Needs to be read only
	private CounterState() { }      // A parameterless constructor is required for creating initial state (can be private or public.)

	public CounterState(int clickCount)  // Only one custom constructor should be created
	{
		ClickCount = clickCount;
	}
}
/*



6:55 It's very important that *State classes 
1. use the `[FeatureState]` Attribute
2. The values of this start are going to only be read only (i.e. just has a `get`) 
3. State should be immutable (that's why you only have a getter)

7:10 The creator of the *State classes are the reducers and that's where the values get passed in

7:20 Constructors
1. You need a default constructor if you're going to use custom constructors
2. There should really only be **one custom constructor**.
3. In that custom constructor is where you specify each value for the state of the specific use case

 */