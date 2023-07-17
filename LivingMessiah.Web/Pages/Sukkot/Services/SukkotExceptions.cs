using System;

namespace LivingMessiah.Web.Pages.Sukkot.Services;

// public class SukkotExceptions{}



public class RegistrationNotFoundException : Exception
{
	public RegistrationNotFoundException()
	{
	}
	public RegistrationNotFoundException(string message)
			: base(message)
	{
	}

	public RegistrationNotFoundException(string message, Exception inner)
			: base(message, inner)
	{
	}
}


public class UserNotAuthoirizedException : Exception
{
	public UserNotAuthoirizedException()
	{
	}
	public UserNotAuthoirizedException(string message)
			: base(message)
	{
	}

	public UserNotAuthoirizedException(string message, Exception inner)
			: base(message, inner)
	{
	}
}

public class PaymentSummaryException : Exception
{
	public PaymentSummaryException()
	{
	}
	public PaymentSummaryException(string message)
			: base(message)
	{
	}
	public PaymentSummaryException(string message, Exception inner)
			: base(message, inner)
	{
	}
}

public class RegistratationException : Exception
{
	public RegistratationException()
	{
	}
	public RegistratationException(string message)
			: base(message)
	{
	}
	public RegistratationException(string message, Exception inner)
			: base(message, inner)
	{
	}
}


public class StatusException : Exception
{
	public StatusException()
	{
	}
	public StatusException(string message)
			: base(message)
	{
	}
	public StatusException(string message, Exception inner)
			: base(message, inner)
	{
	}
}


/*
 # Notes on Exceptions
 http://blog.abodit.com/2010/03/using-exception-data-to-add-additional-information-to-an-exception/
 catch (RegistratationException e) when (e.Data != null)
foreach (DictionaryEntry de in e.Data)
	Console.WriteLine("    Key: {0,-20}      Value: {1}", 
											 "'" + de.Key.ToString() + "'", de.Value);
*/