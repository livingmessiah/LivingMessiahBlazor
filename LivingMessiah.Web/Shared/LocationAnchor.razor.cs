using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Shared;

public partial class LocationAnchor
{
		[Parameter]
		public string AddressName { get; set; } = "Living Messiah";

		[Parameter]
		public string AddressStreet { get; set; } = ""; // the default is blank because the default route is a redirect to the location page

		[Parameter]
		public string Href { get; set; } = "/Location";

		[Parameter]
		public bool IsPrinterFriendly { get; set; } = false;

		[Parameter]
		public bool IsRemote { get; set; } = false;

}
/*
<li> Default: <LocationAnchor></LocationAnchor></li>

<li>Is Remote: <LocationAnchor IsRemote="true"></LocationAnchor></li>

<li>Is Remote and Printer Friendly:
	<LocationAnchor IsRemote="true"
					  IsPrinterFriendly="true">
	</LocationAnchor>
</li>

<li>Is printer friendly:
	<LocationAnchor IsPrinterFriendly="true">
	</LocationAnchor>
</li>

<li>Specific Address: 
	<LocationAnchor AddressName="Cine Mark Mesa 16"
										 AddressStreet="1051 N Dobson Rd, Mesa, AZ 85201"
										 Href="https://goo.gl/maps/MxYb8ue3Ksi6uMjD7">
	</LocationAnchor>
</li>

*/