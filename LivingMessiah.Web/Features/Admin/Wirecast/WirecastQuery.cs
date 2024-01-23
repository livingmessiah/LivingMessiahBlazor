using System;

namespace LivingMessiah.Web.Features.Admin.Wirecast;

public record WirecastQuery(int Id, DateTime ShabbatDate, string? WirecastLink);