using System.Collections.Generic;
using LivingMessiah.Web.Domain;

namespace LivingMessiah.Web.Data
{
	public interface ILinksFactory
	{
		List<Link> GetLinks();
		List<Link> GetFeastLinks();
		List<LinkBasic> GetDashboardLinks();
		List<LinkBasic> GetMarkdownLinks();
	}
}
