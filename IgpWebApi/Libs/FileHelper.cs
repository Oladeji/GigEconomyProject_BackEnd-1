using Azure.Storage.Blobs;
    public static class FileHelper
    {
        public static  async Task<string> ConvertFileToBase64strString(IFormFile file)
        {
          if (file.Length > 0)
          {
            try{
               using (var ms = new MemoryStream())
             {
               await  file.CopyToAsync(ms);
               var fileBytes = ms.ToArray();
               string s = Convert.ToBase64String(fileBytes);
               return s;
            }}
            catch(Exception ex){
              throw new Exception("Problem saving image"+ ex.ToString());
            }
           
          }
          return null;
            
        }
         public static  async Task<string> UploadImage(IFormFile value)
        {
            string str = @"DefaultEndpointsProtocol=https;AccountName=serveradminrgdiag192;AccountKey=BnSBF2jCXIl3a2q1tsbooXEmqYgB1Y60CfsojPczkDbNO47jEI9oueg9+SU33DdU4JixHOcFzZsIPoqhncy8OQ==;EndpointSuffix=core.windows.net";
            //DefaultEndpointsProtocol=https;AccountName=mystorageaccountuwe;AccountKey=e9yzYvHsGUqUYwWjOFCvwia6kjGU5XF6ugCsBc+iqGL3Tao2U5xS4gJe9aogYqm9xhmk75NJTTpKsT1mH20URg==;EndpointSuffix=core.windows.net";
            string ctn ="uweigppics";// "mystoragecontainer";

            BlobContainerClient blbc = new BlobContainerClient(str, ctn);
            BlobClient bc = blbc.GetBlobClient(value.FileName);
            var memoryStream = new MemoryStream();
            await value.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await bc.UploadAsync(memoryStream,true);
            return  bc.Uri.AbsoluteUri;
            
        }
    }