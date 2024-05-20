


namespace LivingMessiah.Web.Features.InDepthStudy;

public static class Defaults
{
	public static Leadership.Enums.Office Facilitator = Leadership.Enums.Office.Elder2;
	
	public static string FacilitatorName(Leadership.Enums.Office? office)
	{
		if (office is not null)
		{
			return LivingMessiah.Web.Blobs.Persons(LivingMessiah.Web.Blobs.Persons(office.Name));
		}
		else
		{
			return Facilitator.OfficeHolderName;
		}
	}
}

public static class Blobs
{
	private const string BasePath = "https://livingmessiahstorage.blob.core.windows.net/indepth-images/";  

	public static string DefaultImage()
	{
		return BasePath + "generic.jpg"; //indepth-images/generic.jpg
	}

	public static string ImageFullPath(string blob)
	{
		return BasePath + blob;
	}

	public static string FacilitatorImg(Leadership.Enums.Office? office)  
	{
		if (office is not null)
		{
			return LivingMessiah.Web.Blobs.Persons(LivingMessiah.Web.Blobs.Persons(office.IconFile2));
		}
		else
		{
			return LivingMessiah.Web.Blobs.Leadership(Defaults.Facilitator.IconFile2); 
		}
	}



}