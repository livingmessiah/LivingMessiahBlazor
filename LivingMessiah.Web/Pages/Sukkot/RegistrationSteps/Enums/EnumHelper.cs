using System;
using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

public static class EnumHelper
{
	public static T[] GetEnumValues<T>() where T : struct
	{
		if (!typeof(T).IsEnum)
		{
			throw new ArgumentException("GetValues<T> can only be called for types derived from System.Enum", "T");
		}
		return (T[])Enum.GetValues(typeof(T));
	}

}

