using Microsoft.AspNetCore.Http;

namespace Survey.Function
{
	public  class Fileupload
	{
		public static string Fileuploader(IFormFile file,string uploadPath)
		{
			if (file != null && file.Length > 0)
			{
				// Dosya uzantısını kontrol et
				var fileExtension = Path.GetExtension(file.FileName).ToLower();

				if (fileExtension == ".pdf" || fileExtension == ".jpeg" || fileExtension == ".jpg" || fileExtension == ".png")
				{
					// Rasgele dosya adı oluştur
					var randomFileName = Path.GetRandomFileName();
					var fileName = Path.ChangeExtension(randomFileName, fileExtension);

					// Dosyayı belirtilen yola kaydet
					var filePath = Path.Combine(uploadPath, fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						file.CopyTo(stream);
					}

					
					return fileName;
				}
				else
				{
					
					return null;
				}
			}
			else
			{
				
				return null;
			}
		}
	}
	}

