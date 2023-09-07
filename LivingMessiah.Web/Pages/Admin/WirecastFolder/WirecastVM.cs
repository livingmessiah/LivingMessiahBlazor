﻿using System;
using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.Admin.WirecastFolder;

public class WirecastVM
{
	[Required]
	public Int32 Id { get; set; }

	[Required]
	public DateTime ShabbatDate { get; set; }

	[Required]
	[MaxLength(100)]
	[Url]
	public string? WirecastLink { get; set; }

}