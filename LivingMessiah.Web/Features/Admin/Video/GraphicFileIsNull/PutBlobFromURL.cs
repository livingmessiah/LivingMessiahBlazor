using Azure.Storage.Blobs.Specialized;
using System.Threading.Tasks;
using System;

namespace LivingMessiah.Web.Features.Admin.Video.GraphicFileIsNull;

public class PutBlobFromURL
{
	public static async Task CopyFromExternalSourceAsync(string sourceLocation,	BlockBlobClient destinationBlob)
	{
		Uri sourceUri = new(sourceLocation);
		await destinationBlob.SyncUploadFromUriAsync(sourceUri);
	}
}
