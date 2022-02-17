namespace LivingMessiah.Web.SmartEnums;
using Ardalis.SmartEnum;


// public enum MediaQueryEnum { Xs = 1,	Sm = 2,	Md = 3,	Lg = 5,	Xl = 6}

/*

2xExtra lg	xxl					≥1400px  v5

<div class="d-sm-none"></div>
<div class="d-none d-sm-block d-md-none d-lg-none d-xl-none"></div>
<div class="d-none d-md-block d-lg-none d-xl-none"></div>
<div class="d-none d-lg-block d-xl-none"></div>
<div class="d-none d-xl-block"></div> 

Infix: An operator that comes in between its operands, such as multiplication in 24 * 7 .

 */


public abstract class MediaQuery : SmartEnum<MediaQuery>
{
	#region Id's
	private static class Id
	{
		internal const int Xs = 1;
		internal const int Sm = 2;
		internal const int Md = 3;
		internal const int Lg = 5;
		internal const int Xl = 6;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly MediaQuery Xs = new XsSE();
	public static readonly MediaQuery Sm = new SmSE();
	public static readonly MediaQuery Md = new MdSE();
	public static readonly MediaQuery Lg = new LgSE();
	public static readonly MediaQuery Xl = new XlSE();
	// SE=SmartEnum
	#endregion

	private MediaQuery(string name, int value) : base(name, value) { }  // Constructor

	#region Extra Fields
	public abstract string DivClass { get; }
	/*
	public abstract string Breakpoint { get; }
	public abstract string ClassInfix { get; }
	public abstract int Dimensions { get; }
	
	Breakpoint	ClassInfix	Dimensions
		X-Small			None				<576px
		Small				sm					≥576px
		Medium			md					≥768px
		Large				lg					≥992px
		Extra	large	xl					≥1200px
		2xExtra lg	xxl					≥1400px  v5
	*/
	#endregion


	#region Private Instantiation

	private sealed class XsSE : MediaQuery
	{
		public XsSE() : base($"{nameof(Id.Xs)}", Id.Xs) { }
		public override string DivClass => "d-sm-none";
	}

	private sealed class SmSE : MediaQuery
	{
		public SmSE() : base($"{nameof(Id.Sm)}", Id.Sm) { }
		public override string DivClass => "d-none d-sm-block d-md-none d-lg-none d-xl-none";
	}

	private sealed class MdSE : MediaQuery
	{
		public MdSE() : base($"{nameof(Id.Md)}", Id.Md) { }
		public override string DivClass => "d-none d-md-block d-lg-none d-xl-none";
	}

	private sealed class LgSE : MediaQuery
	{
		public LgSE() : base($"{nameof(Id.Lg)}", Id.Lg) { }
		public override string DivClass => "d-none d-lg-block d-xl-none";
	}

	private sealed class XlSE : MediaQuery
	{
		public XlSE() : base($"{nameof(Id.Xl)}", Id.Xl) { }
		public override string DivClass => "d-none d-xl-block";
	}

	#endregion

}
