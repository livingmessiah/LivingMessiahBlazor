namespace LivingMessiah.Web.Features.Shavuot.Domain;

public class HebrewLetter
{
	public int Id { get; set; }
	public string? UnicodeName { get; set; }
	public string? Hebrew { get; set; }
	public int Gematria { get; set; }
	public string? Sofit { get; set; }
	public string? WithoutDagesh { get; set; }
}
