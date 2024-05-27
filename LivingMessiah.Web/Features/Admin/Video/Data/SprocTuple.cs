using System;

namespace LivingMessiah.Web.Features.Admin.Video.Data;

public record SprocTuple(int AffectedRows, int ReturnValue, string ReturnMsg);

/*
var t = new SprocTupleVT(a: 1, b: 2);
ExampleMethod(t);

var sprocTupleVT =  new SprocTupleVT

ToDo: test this code and see if it's a better approach
 */
public record SprocTupleVT(int AffectedRows, int ReturnValue, string ReturnMsg)
{
	public static implicit operator ValueTuple<int, int, string>(SprocTupleVT record)
	{
		return (record.AffectedRows, record.ReturnValue, record.ReturnMsg);
	}
}

// Ignore Spelling:Sproc
