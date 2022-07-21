﻿using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Leadership.Enums;

public abstract class Office : SmartEnum<Office>
{

	#region Id's
	private static class Id
	{
		internal const int CongregationLeader = 1;
		internal const int BeitDin1 = 2;
		internal const int BeitDin2 = 3;
		internal const int BeitDin3 = 4;
		internal const int Elder1 = 5;
		internal const int Elder2 = 6;
		internal const int Elder3 = 7;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Office CongregationLeader = new CongregationLeaderSE();
	public static readonly Office BeitDin1 = new BeitDin1SE();
	public static readonly Office BeitDin2 = new BeitDin2SE();
	public static readonly Office BeitDin3 = new BeitDin3SE();
	public static readonly Office Elder1 = new Elder1SE();
	public static readonly Office Elder2 = new Elder2SE();
	public static readonly Office Elder3 = new Elder3SE();
	// SE=SmartEnum
	#endregion

	private Office(string name, int value) : base(name, value) { } // ConstructorSmartEnumTemplate

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string ImageFile { get; }
	public abstract string IconFile { get; }
	public abstract bool IsFilled { get; }
	public abstract string OfficeHolderName { get; }
	public abstract string Email { get; }
	//public abstract string BioFile { get; } // Elder3.md
	#endregion

	#region Private Instantiation

	private sealed class CongregationLeaderSE : Office
	{
		public CongregationLeaderSE() : base($"{nameof(Id.CongregationLeader)}", Id.CongregationLeader) { }
		public override string Title => "Congregation Leader";
		public override string ImageFile => "leader.jpg";
		public override string IconFile => "leader-icon.jpg";
		public override bool IsFilled => true;
		public override string OfficeHolderName => "Mark Webb";
		public override string Email => "Mark@livingmessiah.com";
	}

	private sealed class BeitDin1SE : Office
	{
		public BeitDin1SE() : base($"{nameof(Id.BeitDin1)}", Id.BeitDin1) { }
		public override string Title => "Bet Din";
		public override string ImageFile => "beit-din-1.jpg";
		public override string IconFile => "beit-din-1-icon.jpg";
		public override bool IsFilled => true;
		public override string OfficeHolderName => "Ralphie Cratty";
		public override string Email => "Ralphie@livingmessiah.com";
	}

	private sealed class BeitDin2SE : Office
	{
		public BeitDin2SE() : base($"{nameof(Id.BeitDin2)}", Id.BeitDin2) { }
		public override string Title => "Bet Din";
		public override string ImageFile => "beit-din-2.jpg";
		public override string IconFile => "beit-din-2-icon.jpg";
		public override bool IsFilled => false;
		public override string OfficeHolderName => "Vacant";
		public override string Email => "Info@livingmessiah.com";
	}

	private sealed class BeitDin3SE : Office
	{
		public BeitDin3SE() : base($"{nameof(Id.BeitDin3)}", Id.BeitDin3) { }
		public override string Title => "Bet Din";
		public override string ImageFile => "beit-din-3.jpg";
		public override string IconFile => "beit-din-3-icon.jpg";
		public override bool IsFilled => true;
		public override string OfficeHolderName => "John Marsing";
		public override string Email => "John@livingmessiah.com";
	}

	private sealed class Elder1SE : Office
	{
		public Elder1SE() : base($"{nameof(Id.Elder1)}", Id.Elder1) { }
		public override string Title => "Elder";
		public override string ImageFile => "elder-1.jpg";
		public override string IconFile => "elder-1-icon.jpg";
		public override bool IsFilled => true;
		public override string OfficeHolderName => "Pat Shackleford";
		public override string Email => "Pat@livingmessiah.com";
	}

	private sealed class Elder2SE : Office
	{
		public Elder2SE() : base($"{nameof(Id.Elder2)}", Id.Elder2) { }
		public override string Title => "Elder";
		public override string ImageFile => "elder-2.jpg";
		public override string IconFile => "elder-2-icon.jpg";
		public override bool IsFilled => false;
		public override string OfficeHolderName => "Vacant";
		public override string Email => "Info@livingmessiah.com";
	}

	private sealed class Elder3SE : Office
	{
		public Elder3SE() : base($"{nameof(Id.Elder3)}", Id.Elder3) { }
		public override string Title => "Elder";
		public override string ImageFile => "elder-3.jpg";
		public override string IconFile => "elder-3-icon.jpg";
		public override bool IsFilled => true;
		public override string OfficeHolderName => "Paul Hebron";
		public override string Email => "Paul@livingmessiah.com";
	}

	#endregion

	/*
	public string Dump
	{
		get
		{
			string s = "";
			s += $" {(this.CanTransitionTo(NotAuthenticated) ? NotAuthenticated.Name : "__")  }";
			return s;

		}
	}
	*/
}

