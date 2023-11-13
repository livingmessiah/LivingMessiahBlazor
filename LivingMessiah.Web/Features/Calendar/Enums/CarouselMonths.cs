using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Features.Calendar.Enums;

public abstract class CarouselMonths : SmartEnum<CarouselMonths>
{

	#region Id's
	private static class Id
	{
		internal const int January = 1;
		internal const int February = 2;
		internal const int March = 3;
		internal const int April = 4;
		internal const int May= 5;
		internal const int June = 6;
		internal const int July = 7;
		internal const int August = 8;
		internal const int September = 9;
		internal const int October = 10;
		internal const int November = 11;
		internal const int December = 12;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly CarouselMonths January = new JanuarySE();
	public static readonly CarouselMonths February = new FebruarySE();
	public static readonly CarouselMonths March = new MarchSE();
	public static readonly CarouselMonths April = new AprilSE();
	public static readonly CarouselMonths May = new MaySE();
	public static readonly CarouselMonths June = new JuneSE();
	public static readonly CarouselMonths July = new JulySE();
	public static readonly CarouselMonths August = new AugustSE();
	public static readonly CarouselMonths September = new SeptemberSE();
	public static readonly CarouselMonths October = new OctoberSE();
	public static readonly CarouselMonths November = new NovemberSE();
	public static readonly CarouselMonths December = new DecemberSE();
	// SE=SmartEnum
	#endregion

	private CarouselMonths(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields

	public abstract string Image { get; }
	public abstract string ImageFullSize { get; }
	public abstract string Caption { get; }
	#endregion

	#region Private Instantiation

	private sealed class JanuarySE : CarouselMonths
	{
		public JanuarySE() : base($"{nameof(Id.January)}", Id.January) { }
		public override string Image => "calendar-2023-01.jpg";
		public override string ImageFullSize => "calendar-2023-01.jpg";
		public override string Caption => "January";
	}

	private sealed class FebruarySE : CarouselMonths
	{
		public FebruarySE() : base($"{nameof(Id.February)}", Id.February) { }
		public override string Image => "calendar-2023-02.jpg";
		public override string ImageFullSize => "calendar-2023-02.jpg";
		public override string Caption => "February";
	}

	private sealed class MarchSE : CarouselMonths
	{
		public MarchSE() : base($"{nameof(Id.March)}", Id.March) { }
		public override string Image => "calendar-2023-03.jpg";
		public override string ImageFullSize => "calendar-2023-03.jpg";
		public override string Caption => "March";
	}

	private sealed class AprilSE : CarouselMonths
	{
		public AprilSE() : base($"{nameof(Id.April)}", Id.April) { }
		public override string Image => "calendar-2023-04.jpg";
		public override string ImageFullSize => "calendar-2023-04.jpg";
		public override string Caption => "April";
	}

	private sealed class MaySE : CarouselMonths
	{
		public MaySE() : base($"{nameof(Id.May)}", Id.May) { }
		public override string Image => "calendar-2023-05.jpg";
		public override string ImageFullSize => "calendar-2023-05.jpg";
		public override string Caption => "May";
	}

	private sealed class JuneSE : CarouselMonths
	{
		public JuneSE() : base($"{nameof(Id.June)}", Id.June) { }
		public override string Image => "calendar-2023-06.jpg";
		public override string ImageFullSize => "calendar-2023-06.jpg";
		public override string Caption => "June";
	}

	private sealed class JulySE : CarouselMonths
	{
		public JulySE() : base($"{nameof(Id.July)}", Id.July) { }
		public override string Image => "calendar-2023-07.jpg";
		public override string ImageFullSize => "calendar-2023-07.jpg";
		public override string Caption => "July";
	}

	private sealed class AugustSE : CarouselMonths
	{
		public AugustSE() : base($"{nameof(Id.August)}", Id.August) { }
		public override string Image => "calendar-2023-08.jpg";
		public override string ImageFullSize => "calendar-2023-07.jpg";
		public override string Caption => "August";
	}

	private sealed class SeptemberSE : CarouselMonths
	{
		public SeptemberSE() : base($"{nameof(Id.September)}", Id.September) { }
		public override string Image => "calendar-2023-09.jpg";
		public override string ImageFullSize => "calendar-2023-09.jpg";
		public override string Caption => "September";
	}

	private sealed class OctoberSE : CarouselMonths
	{
		public OctoberSE() : base($"{nameof(Id.October)}", Id.October) { }
		public override string Image => "calendar-2023-10.jpg";
		public override string ImageFullSize => "calendar-2023-10.jpg";
		public override string Caption => "October";
	}

	private sealed class NovemberSE : CarouselMonths
	{
		public NovemberSE() : base($"{nameof(Id.November)}", Id.November) { }
		public override string Image => "calendar-2023-11.jpg";
		public override string ImageFullSize => "calendar-2023-11.jpg";
		public override string Caption => "November";
	}

	private sealed class DecemberSE : CarouselMonths
	{
		public DecemberSE() : base($"{nameof(Id.December)}", Id.December) { }
		public override string Image => "calendar-2023-12.jpg";
		public override string ImageFullSize => "calendar-2023-12.jpg";
		public override string Caption => "December";
	}
	#endregion

}

