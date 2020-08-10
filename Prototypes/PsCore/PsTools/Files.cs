using System.IO;
using System.Web;

namespace PsTools
{
    public class Files
    {
        public static string UploadPhoto(HttpPostedFileBase file, string folder, string name)
        {
            var pic = string.Empty;

            if (file == null) return pic;
            // pic = Path.GetFileName(file.FileName);
            pic = name == "" ? Path.GetFileName(file.FileName) : name;
            var path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
            // path = Path.Combine(HttpContent.Current.Server.MapPath(folder), pic);                
            file.SaveAs(path);
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    file.InputStream.CopyTo(ms);
            //    byte[] array = ms.GetBuffer();
            //}

            return pic;
        }

        public static bool UploadPhoto(MemoryStream stream, string folder, string name)
        {

            try
            {

                stream.Position = 0;
                var path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                File.WriteAllBytes(path, stream.ToArray());

            }
            catch (System.Exception)
            {

                return false;

            }

            return true;

        }

    }
}

//Hot Examples
//EN
//Example: DateTime.Now
//C# (CSharp) IFormFile.OpenReadStream Examples
//C# (CSharp) IFormFile.OpenReadStream - 30 examples found. These are the top rated real world C# (CSharp) examples of IFormFile.OpenReadStream extracted from open source projects. You can rate examples to help us improve the quality of examples.
//Programming Language: C# (CSharp)
//Class/Type: IFormFile
//Method/Function: OpenReadStream
//Examples at hotexamples.com: 30
//FREQUENTLY USED METHODS
//OpenReadStream (30)
//SaveAsAsync(22)
//SaveAs(12)
//IsAcceptableImageContentType(11)
//GetFileName(6)
//CopyToAsync(5)
//ReadAllBytesAsync(4)
//CopyTo(2)
//SaveImageAsAsync(2)
//ReadAllBytes(1)
//FREQUENTLY USED METHODS
//ToString(1)
//RELATED
//IdolOfTheChampion
//Pastebin
//LTChung
//IntValueRetriever
//ModifyReplicationInstanceRequestMarshaller
//EmployeeAddTypeHistoryGateway
//Joinable
//StoreSettings
//Properties
//GUIPlayListButtonControl
//Google BookmarkFacebookTwitterPrintEmailMore
//EXAMPLE #11  

//File: ImageController.cs Project: VysotskiVadim/bsuir-misoi-car-number
// public async Task<ImageUploadResult> SaveImage(IFormFile file)
//{
//    IImage image;
//    using (var fileStram = file.OpenReadStream())
//    {
//        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Replace("\"", string.Empty);
//        string imageName = Guid.NewGuid() + Path.GetExtension(fileName);
//        image = _imageFactory.CreateImage(imageName, fileStram);
//    }
//    await _imageRespository.SaveImageAsync(image);
//    string imageUrl = _imageUrlProvider.GetImageUrl(image.Name);
//    return new ImageUploadResult(imageUrl, image.Name);
//}
//EXAMPLE #20  

//File: AdminController.cs Project: Codeflyers/EC-GitHub
//        public ActionResult Edit(Product product, IFormFile image)
//{
//    if (ModelState.IsValid)
//    {
//        if (image != null)
//        {
//            FileDetails fileDetails;
//            using (var reader = new StreamReader(image.OpenReadStream()))
//            {
//                var fileContent = reader.ReadToEnd();
//                var parsedContentDisposition = ContentDispositionHeaderValue.Parse(image.ContentDisposition);
//                fileDetails = new FileDetails
//                {
//                    Filename = parsedContentDisposition.FileName,
//                    Content = fileContent,
//                    ContentType = image.ContentType
//                };
//            }
//            //product.ImageMimeType = image.ContentType;
//            product.ImageData = fileDetails.Content;
//        }
//        _productRepository.AddOrUpdate(product);
//        TempData["message"] = string.Format("{0} has been saved successfully", product.Title);

//        return RedirectToAction("Index");
//    }
//    return View(product);
//}
//EXAMPLE #30  

//File: UploadsController.cs Project: andrewmelnichuk/repo2
//    public async Task Upload(IFormFile file)
//{
//    if (file == null || file.Length == 0)
//        Exceptions.ServerError("Invalid file.");

//    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

//    using (var stream = file.OpenReadStream())
//        await DeployManager.Upload(stream, fileName);
//}
//EXAMPLE #40  

//File: AvatarService.cs Project: skimur/skimur
//        public string UploadAvatar(IFormFile file, string key)
//{
//    if (file.Length >= 300000)
//        throw new Exception("Uploaded image may not exceed 300 kb, please upload a smaller image.");

//    try
//    {
//        using (var readStream = file.OpenReadStream())
//        {
//            using (var img = Image.FromStream(readStream))
//            {
//                if (!img.RawFormat.Equals(ImageFormat.Jpeg) && !img.RawFormat.Equals(ImageFormat.Png))
//                    throw new Exception("Uploaded file is not recognized as an image.");

//                var tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

//                try
//                {
//                    ImageBuilder.Current.Build(img, tempFile, new Instructions
//                    {
//                        Width = 150,
//                        Height = 150,
//                        Format = "jpg",
//                        Mode = FitMode.Max
//                    });

//                    var avatar = _avatarDirectoryInfo.GetFile(key + ".jpg");
//                    if (avatar.Exists)
//                        avatar.Delete();
//                    avatar.Open(FileMode.Create, stream =>
//                    {
//                        using (var imageStream = File.OpenRead(tempFile))
//                            imageStream.CopyTo(stream);
//                    });
//                }
//                catch (Exception ex)
//                {
//                    throw new Exception("Uploaded file is not recognized as a valid image.", ex);
//                }
//                finally
//                {
//                    if (File.Exists(tempFile))
//                        File.Delete(tempFile);
//                }

//                return key;
//            }
//        }
//    }
//    catch (Exception)
//    {
//        throw new Exception("Uploaded file is not recognized as an image.");
//    }
//}
//EXAMPLE #50  

//File: FileUploadHandler.cs Project: mathiasnohall/Servanda
//        public async Task<string> HandleUpload(IFormFile file)
//{
//    var data = await _streamHandler.CopyStreamToByteBuffer(file.OpenReadStream());

//    var encryptedData = await _streamHandler.EncryptData(data);

//    var uniqueFileName = Guid.NewGuid().ToString();

//    await _streamHandler.WriteBufferToFile(encryptedData, _hostingEnvironment.WebRootPath + "uploads/" + uniqueFileName);

//    return uniqueFileName;
//}
//EXAMPLE #60  

//File: ImageService.cs Project: rspaulino/allReady
//        private async Task<string> UploadImageAsync(string blobPath, IFormFile image)
//{

//    //Get filename
//    var fileName = (ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName).Trim('"').ToLower();
//    Debug.WriteLine(string.Format("BlobPath={0}, fileName={1}, image length={2}", blobPath, fileName, image.Length.ToString()));

//    if (fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg") || fileName.EndsWith(".png"))
//    {
//        string storageConnectionString = _config["Data:Storage:AzureStorage"];

//        CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
//        CloudBlobContainer container =
//            account.CreateCloudBlobClient().GetContainerReference(CONTAINER_NAME);

//        //Create container if it doesn't exist wiht public access
//        await container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Container, new BlobRequestOptions(), new OperationContext());

//        string blob = blobPath + "/" + fileName;
//        Debug.WriteLine("blob" + blob);

//        CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob);

//        blockBlob.Properties.ContentType = image.ContentType;

//        using (var imageStream = image.OpenReadStream())
//        {
//            //Option #1
//            byte[] contents = new byte[image.Length];

//            for (int i = 0; i < image.Length; i++)
//            {
//                contents[i] = (byte)imageStream.ReadByte();
//            }

//            await blockBlob.UploadFromByteArrayAsync(contents, 0, (int)image.Length);

//            //Option #2
//            //await blockBlob.UploadFromStreamAsync(imageStream);
//        }

//        Debug.WriteLine("Image uploaded to URI: " + blockBlob.Uri.ToString());
//        return blockBlob.Uri.ToString();
//    }
//    else
//    {
//        throw new Exception("Invalid file extension: " + fileName + "You can only upload images with the extension: jpg, jpeg, or png");
//    }
//}
//EXAMPLE #70  

//File: UploadHelperFunctions.cs Project: Microsoft/mattercenter
// public GenericResponseVM PerformContentCheck(string clientUrl, string folderUrl, IFormFile uploadedFile, string fileName)
//{
//    GenericResponseVM genericResponse = null;
//    ClientContext clientContext = spoAuthorization.GetClientContext(clientUrl);
//    using (MemoryStream targetStream = new MemoryStream())
//    {
//        Stream sourceStream = uploadedFile.OpenReadStream();
//        try
//        {
//            byte[] buffer = new byte[sourceStream.Length + 1];
//            int read = 0;
//            while ((read = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
//            {
//                targetStream.Write(buffer, 0, read);
//            }
//            string serverFileUrl = folderUrl + ServiceConstants.FORWARD_SLASH + fileName;
//            bool isMatched = documentRepository.PerformContentCheck(clientContext, targetStream, serverFileUrl);
//            if (isMatched)
//            {
//                //listResponse.Add(string.Format(CultureInfo.InvariantCulture, "{0}{1}{1}{1}{2}", ConstantStrings.FoundIdenticalContent, ConstantStrings.Pipe, ConstantStrings.TRUE));
//                genericResponse = new GenericResponseVM()
//                {
//                    IsError = true,
//                    Code = UploadEnums.IdenticalContent.ToString(),
//                    Value = string.Format(CultureInfo.InvariantCulture, errorSettings.FoundIdenticalContent)
//                };
//            }
//            else
//            {
//                genericResponse = new GenericResponseVM()
//                {
//                    IsError = true,
//                    Code = UploadEnums.NonIdenticalContent.ToString(),
//                    Value = string.Format(CultureInfo.InvariantCulture, errorSettings.FoundNonIdenticalContent)
//                };
//            }
//        }
//        catch (Exception exception)
//        {
//            //Logger.LogError(exception, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, UIConstantStrings.LogTableName);
//            //response = string.Format(CultureInfo.InvariantCulture, "{0}{1}{1}{1}{2}", ConstantStrings.ContentCheckFailed, ConstantStrings.Pipe, ConstantStrings.TRUE);
//        }
//        finally
//        {
//            sourceStream.Dispose();
//        }
//    }
//    return genericResponse;
//}
//EXAMPLE #80  

//File: FileUploadController.cs Project: AndersBillLinden/Mvc
//        public FileDetails UploadSingle(IFormFile file)
//{
//    FileDetails fileDetails;
//    using (var reader = new StreamReader(file.OpenReadStream()))
//    {
//        var fileContent = reader.ReadToEnd();
//        var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
//        fileDetails = new FileDetails
//        {
//            Filename = parsedContentDisposition.FileName,
//            Content = fileContent
//        };
//    }

//    return fileDetails;
//}
//EXAMPLE #90  

//File: ImageUtility.cs Project: oshalygin/SolutionPub
//        //TODO: Need to be able to test this...FrameworkIssues
//        public Image SaveImage(string fileName, string description, IFormFile file)
//{
//    Image image;
//    using (var reader = new StreamReader(file.OpenReadStream()))
//    {
//        var fileContent = reader.ReadToEnd();
//        var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
//        image = new Image
//        {
//            FileName = parsedContentDisposition.FileName,
//            Content = fileContent,
//            Description = description,
//            UploadDate = DateTime.UtcNow,
//            ImagePath = Path.Combine(_applicationEnvironment.ApplicationBasePath, BlogImageDatabasePath, parsedContentDisposition.FileName)
//        };
//    }

//    return image;
//}
//EXAMPLE #100  

//File: ImageUploader.cs Project: SurferJeffAtGoogle/getting-started-dotnet
//        public async Task<String> UploadImage(IFormFile image, long id)
//{
//    // Create client and use it to upload object to Cloud Storage
//    var client = await StorageClient
//        .FromApplicationCredentials(_applicationName);

//    var imageAcl = ObjectsResource
//        .InsertMediaUpload.PredefinedAclEnum.PublicRead;

//    var imageObject = await client.UploadObjectAsync(
//        bucket: _bucketName,
//        objectName: id.ToString(),
//        contentType: image.ContentType,
//        source: image.OpenReadStream(),
//        options: new UploadObjectOptions { PredefinedAcl = imageAcl }
//    );

//    return imageObject.MediaLink;
//}
//EXAMPLE #110  

//File: ApiProjectsController.cs Project: GeorgDangl/WebDocu
// public IActionResult Upload(string ApiKey, IFormFile ProjectPackage)
//{
//    if (ProjectPackage == null)
//    {
//        return HttpBadRequest();
//    }
//    if (string.IsNullOrWhiteSpace(ApiKey))
//    {
//        // Not accepting empty API key -> Disable API upload to projects by setting the API key empty
//        return HttpNotFound();
//    }
//    var projectEntry = Context.DocumentationProjects.FirstOrDefault(Project => Project.ApiKey == ApiKey);
//    if (projectEntry == null)
//    {
//        return HttpNotFound();
//    }
//    // Try to read as zip file
//    using (var inputStream = ProjectPackage.OpenReadStream())
//    {
//        try
//        {
//            using (var archive = new ZipArchive(inputStream))
//            {
//                var physicalRootDirectory = HostingEnvironment.MapPath("App_Data/");
//                var result = ProjectWriter.CreateProjectFilesFromZip(archive, physicalRootDirectory, projectEntry.Id, Context);
//                if (!result)
//                {
//                    return HttpBadRequest();
//                }
//            }
//            return Ok();
//        }
//        catch (InvalidDataException caughtException)
//        {
//            return HttpBadRequest(new { Error = "Could not read file as zip." });
//        }
//        catch
//        {
//            return HttpBadRequest();
//        }
//    }
//}
//EXAMPLE #120  

//File: PlayerController.cs Project: afuersch/Wuzlstats
//        public async Task<IActionResult> Avatar(int id, IFormFile avatar)
//{
//    var player = await LoadAndEnsurePlayerExists(id);
//    if (avatar.Length > 0)
//    {
//        var settings = new ResizeSettings
//        {
//            MaxWidth = 150,
//            MaxHeight = 150,
//            Format = "png"
//        };

//        var outputStream = new MemoryStream();
//        ImageBuilder.Current.Build(avatar.OpenReadStream(), outputStream, settings);

//        player.Image = outputStream.ToArray();
//        await _db.SaveChangesAsync();
//    }

//    return RedirectToAction("Index", new { id });
//}
//EXAMPLE #130  

//File: HomeController.cs Project: digitalcivics/happynotez
//        public async Task<IActionResult> Index(IFormFile photo, string query, string latitude, string longitude, string elevation, string zoom, bool found)
//{
//    if (photo != null)
//    {
//        Notez note = new Notez
//        {
//            Found = found,
//            Timestamp = DateTimeOffset.Now,
//            UserAgent = Request.Headers["User-Agent"],
//            HostAddress = Context.GetFeature<IHttpConnectionFeature>().RemoteIpAddress.ToString(),
//            LocationRaw = query,
//            Latitude = double.Parse(latitude, CultureInfo.InvariantCulture),
//            Longitude = double.Parse(longitude, CultureInfo.InvariantCulture),
//            Zoom = float.Parse(zoom, CultureInfo.InvariantCulture),
//            Elevation = float.Parse(elevation, CultureInfo.InvariantCulture),
//        };

//        _context.Notez.Add(note);
//        await _context.SaveChangesAsync();

//        string root = Path.Combine(_environment.MapPath("n"));
//        await photo.SaveAsAsync(Path.Combine(root, note.ID + ".jpg"));

//        try
//        {
//            using (Stream s = photo.OpenReadStream())
//                Helper.GenerateThumbnail(s, Path.Combine(root, "t" + note.ID + ".jpg"));
//        }
//        catch
//        {
//            note.FlagStatus = FlagStatus.Invalid;
//            await _context.SaveChangesAsync();
//        }

//        return RedirectToAction(nameof(Index), new { noteID = note.ID });
//    }

//    return RedirectToAction(nameof(Index));
//}
//EXAMPLE #140  

//        public IActionResult Index(IFormFile file)
//{
//    using (var stream = file.OpenReadStream())
//    {
//        using (var reader = new StreamReader(stream))
//        {
//            var csvReader = new CsvReader(reader);
//            csvReader.Configuration.WillThrowOnMissingField = false;
//            csvReader.Configuration.RegisterClassMap<RedCrossRequestMap>();
//            var requests = csvReader.GetRecords<Request>().ToList();

//            var errors = _mediator.Send(new AddRequestsCommand { Requests = requests });

//        }
//    }

//    // todo: - add error handling logic/results view
//    //       - proper view model
//    //       - more complete result type/info

//    ViewBag.ImportSuccess = true;

//    return View();
//}
//EXAMPLE #150  

//File: MomentController.cs Project: yaoyel/Finework
//        public void UploadMomentBgImage(Guid orgId, IFormFile file)
//{
//    try
//    {
//        var org = OrgExistsResult.Check(this.m_OrgManager, orgId).ThrowIfFailed().Org;
//        var staff = StaffExistsResult.Check(this.m_StaffManager, org.Id, this.AccountId).ThrowIfFailed().Staff;
//        PermissionIsAdminResult.Check(this.m_OrgManager, org.Id, staff.Id).ThrowIfFailed();

//        using (var reader = new StreamReader(file.OpenReadStream()))
//        {
//            m_MomentFileManager.UploadMomentBgImage(org, file.ContentType, reader.BaseStream);
//        }
//    }
//    catch
//    {
//        throw new FineWorkException($"上传失败.");
//    }
//}
//EXAMPLE #160  

//File: FileHandler.cs Project: s165519/ASPNET5Examples
//        public async Task saveTransformImagesToBlobAsJpeg(IFormFile file, string fileName)
//{

//    foreach (var size in imageSizes)
//    {
//        CloudBlockBlob blockBlob = _container.GetBlockBlobReference(Enum.GetName(typeof(Sizes), size.Size) + "/" + fileName);
//        blockBlob.Properties.ContentType = "image/jpeg";

//        using (var inStream = file.OpenReadStream())
//        {
//            // Create or overwrite
//            using (var fileStream = await blockBlob.OpenWriteAsync())
//            {
//                var image = new Image(inStream);

//                image.Resize(size.width, 0) // Passing zero on height or width will perserve the aspect ratio of the image.
//                .Save(fileStream, new JpegFormat());
//            }
//        }
//    }
//}
//EXAMPLE #170  

//File: MomentController.cs Project: yaoyel/Finework
//        public void UploadMomentFile(Guid momentId, IFormFile file, int count = 1)
//{

//    var fileName = file.ContentDisposition.Split(';')[2].Split('=')[1].Replace("\"", "");
//    try
//    {
//        var moment = MomentExistsResult.Check(this.m_MomentManager, momentId).ThrowIfFailed().Moment;
//        using (var reader = new StreamReader(file.OpenReadStream()))
//        {
//            m_MomentFileManager.CreateMementFile(moment.Id, file.ContentType, reader.BaseStream, fileName);
//        }
//    }
//    catch
//    {
//        this.DeleteMoment(momentId);

//        throw new FineWorkException($"{fileName}上传失败.");
//    }
//}
//EXAMPLE #180  

//File: BlobStorage.cs Project: victorhugom/AzureApiHelpers
//        /// <summary>
//        /// Upload a photo to the Azure Blob Storage Service
//        /// </summary>
//        /// <param name="photoToUpload"></param>
//        /// <param name="fileName">Optional. Will use a random GUID if no filename defined</param>
//        /// <returns></returns>
//        public async Task<string> UploadPhotoAsync(IFormFile photoToUpload, string fileName = "")
//{
//    if (photoToUpload == null || photoToUpload.Length == 0)
//        return null;

//    var parsedContentDisposition = ContentDispositionHeaderValue.Parse(photoToUpload.ContentDisposition);

//    //check file size and upload
//    if (!FileSizeIsValid(photoToUpload))
//        return null;

//    using (var photoStream = photoToUpload.OpenReadStream())
//    {
//        return await UploadPhotoAsync(photoStream, fileName);
//    }
//}
//EXAMPLE #190  

//File: AppVeyorService.cs Project: michielpost/PublishSettings2AppVeyor
//        PublishData ParsePublishSettingsFile(IFormFile file)
//{
//    PublishData result = null;

//    XmlSerializer serializer = new XmlSerializer(typeof(PublishData));
//    using (StreamReader reader = new StreamReader(file.OpenReadStream()))
//    {
//        result = (PublishData)serializer.Deserialize(reader);
//    }

//    return result;

//}
//EXAMPLE #200  

//File: FileManagerController.cs Project: Quintinon/AspnetCoreFileManager
//        public ActionResult UploadFile(string dir, string name, int? chunks, int? chunk, IFormFile file)
//{
//    string appDirectory;
//    string currentDirectory;

//    if (!string.IsNullOrWhiteSpace(dir) && dir.StartsWith("/secure-files"))
//    {
//        appDirectory = _appEnvironment.ApplicationBasePath + "/Uploads";
//        currentDirectory = dir;
//    }
//    else
//    {
//        appDirectory = _appEnvironment.ApplicationBasePath + "/wwwroot";
//        currentDirectory = string.IsNullOrWhiteSpace(dir) ? "/media" : dir;
//        if (!currentDirectory.StartsWith("/media"))
//            //throw new System.Web.HttpException("Access is denied on the upload directory.");
//            throw new Exception("Access is denied on the upload directory.");
//    }

//    //bool hasPermission = Utilities.VerifyWritePermission(uploadDirectory);
//    //if (!hasPermission)
//    //{
//    //    context.Response.StatusCode = 500;
//    //    context.Response.Write("{\"jsonrpc\" : \"2.0\", \"error\" : {\"code\": 400, \"message\": \"Permissions have not been set correctly on the currently selected folder.\"}, \"id\" : \"id\"}");
//    //    return;
//    //}


//    //string fileName = string.IsNullOrWhiteSpace(name)?  string.Empty;
//    //fileName = Path.GetFileNameWithoutExtension(fileName).Slugify() + Path.GetExtension(fileName).ToLower();

//    chunks = chunks ?? 0;
//    chunk = chunk ?? 0;
//    bool isLastChunk = (chunk >= (chunks - 1)) ? true : false;

//    //var l = new Logging.LogWriter("Plupload.txt");
//    //l.WriteLine("Chunk " + (chunk + 1).ToString() + " of " + chunks.ToString() + ". IsLastChunk: " + isLastChunk.ToString());

//    var savePath = appDirectory + currentDirectory + "/" + name;
//    using (var fs = new FileStream(savePath, chunk == 0 ? FileMode.Create : FileMode.Append))
//    {
//        //var fileBytes = new byte[file.Length];
//        using (var reader = new BinaryReader(file.OpenReadStream()))
//        {
//            //var fileContent = reader.ReadToEnd();
//            //reader.read(fileBytes, 0, fileBytes.Length);
//            fs.Write(reader.ReadBytes(Convert.ToInt32(file.Length)), 0, Convert.ToInt32(file.Length));
//        }
//        //var buffer = new byte[file.Length];
//        //file.OpenReadStream();
//        //file.OpenReadStream.Read(buffer, 0, buffer.Length);
//        //fs.Write(fileBytes, 0, fileBytes.Length);
//    }

//    //if (isLastChunk && ImageResizer.Configuration.Config.Current.Pipeline.IsAcceptedImageType(fileName))
//    //{
//    //    // check the querystring for image properties and validate them if they exist
//    //    string widthQs = context.Request.QueryString["w"];
//    //    string heightQs = context.Request.QueryString["h"];
//    //    int width = 0;
//    //    if (widthQs.IsNumeric())
//    //        width = Convert.ToInt32(widthQs);
//    //    if (width > 4000)
//    //        width = 0;
//    //    int height = 0;
//    //    if (heightQs.IsNumeric())
//    //        height = Convert.ToInt32(heightQs);
//    //    if (height > 4000)
//    //        height = 0;

//    //    //l.WriteLine("Dimensions: " + width + "x" + height);

//    //    if ((width > 0 || height > 0))
//    //    {
//    //        var sourceFilePath = Path.Combine(savePath, fileName);
//    //        var newFilePath = Path.Combine(savePath, Guid.NewGuid().ToString());

//    //        ImageResizer.ImageJob i = new ImageResizer.ImageJob();
//    //        i.Source = sourceFilePath;
//    //        i.Dest = newFilePath;
//    //        i.Instructions = new ImageResizer.Instructions();
//    //        i.Instructions.Mode = ImageResizer.FitMode.Max;
//    //        if (width > 0)
//    //            i.Instructions.Width = width;
//    //        if (height > 0)
//    //            i.Instructions.Height = height;
//    //        i.Build();
//    //        File.Delete(sourceFilePath);
//    //        File.Move(newFilePath, sourceFilePath);
//    //    }
//    //}

//    return Content("chunk uploaded", "text/plain");
//}
//EXAMPLE #210  

//File: HomeController.cs Project: martinkearn/FlipScript
//        //[HttpPost]
//        public async Task<IActionResult> Viewer(IFormFile file = null, string gitHubUrl = "")
//{
//    var fileContent = string.Empty;

//    //get file content as string
//    if (file != null)
//    {
//        //read incoming file to a string
//        using (var reader = new StreamReader(file.OpenReadStream()))
//        {
//            fileContent = reader.ReadToEnd();
//        }
//    }
//    else if (!string.IsNullOrEmpty(gitHubUrl))
//    {
//        //check if gitHubUrl is base 64, decoded if it is
//        gitHubUrl = DecodeBase64(gitHubUrl);

//        //get file content from GitHUb
//        fileContent = await GetGitHubFile(gitHubUrl);
//    }
//    else
//    {
//        //return back to index
//        return RedirectToAction("Index");
//    }

//    //convert markdown file to array of html section strings
//    var sections = ConvertFile(fileContent);

//    //find and extract h1 for page title from array of html strings
//    var title = GetTitle(sections);

//    //enumerate HTML sections and construct array of slides for Owl Carousel
//    List<string> slides = new List<string>();
//    foreach (var section in sections)
//    {
//        var outputSb = new StringBuilder();

//        //eliminate empty sections
//        if (section.ToLower() != "<p>﻿</p>\r\n")
//        {
//            //populate owl format carosel slides
//            outputSb.AppendLine("<div class=\"item\">");
//            outputSb.AppendLine(section);
//            outputSb.AppendLine("</div>");
//        }

//        //add slide to array
//        slides.Add(outputSb.ToString());
//    }

//    //View model
//    var viewModel = new Viewer()
//    {
//        Title = title,
//        SlidesHtml = slides
//    };

//    return View(viewModel);
//}
//EXAMPLE #220  

//File: AdminController.cs Project: GeorgDangl/WebDocu
// public IActionResult UploadProject(Guid ProjectId, IFormFile projectPackage)
//{
//    if (projectPackage == null)
//    {
//        ModelState.AddModelError("", "Please select a file to upload.");
//        return View();
//    }
//    var projectEntry = Context.DocumentationProjects.FirstOrDefault(Project => Project.Id == ProjectId);
//    if (projectEntry == null)
//    {
//        return HttpNotFound();
//    }
//    // Try to read as zip file
//    using (var inputStream = projectPackage.OpenReadStream())
//    {
//        try
//        {
//            using (var archive = new ZipArchive(inputStream))
//            {
//                var physicalRootDirectory = HostingEnvironment.MapPath("App_Data/");
//                var result = ProjectWriter.CreateProjectFilesFromZip(archive, physicalRootDirectory, projectEntry.Id, Context);
//                if (!result)
//                {
//                    ModelState.AddModelError(string.Empty, "Failed to update the project files");
//                    return View();
//                }
//            }
//            ViewBag.SuccessMessage = "Uploaded package.";
//            return View();
//        }
//        catch (InvalidDataException caughtException)
//        {
//            ModelState.AddModelError("", "Cannot read the file as zip archive.");
//            return View();
//        }
//        catch
//        {
//            ModelState.AddModelError("", "Error in request.");
//            return View();
//        }
//    }
//}
//EXAMPLE #230  

//File: FileNode.cs Project: XuPeiYao/SharpDisk
//        /// <summary>
//        /// 建立檔案節點
//        /// </summary>
//        /// <param name="File">檔案</param>
//        /// <param name="Database">資料庫</param>
//        /// <returns></returns>
//        public async Task<FileNode> CreateChildrenAsync(IFormFile File, SharpDiskDbContext Database)
//{
//    FileNode newFile = new FileNode();

//    #region 空間檢查
//    if (//使用者空間不足
//        Owner.SpaceSize.HasValue &&//有空間限制
//        Owner.GetTotalUsedSpace(Database) + File.Length > Owner.SpaceSize)
//    {
//        throw new LackOfSpaceException($"目前使用者空間不足");
//    }

//    //檢查目錄鍊容量是否足夠
//    var ParentChain = this.GetParentChain(Database);
//    ParentChain.Insert(0, this);
//    var TooSmail = ParentChain.Where(x =>
//        x.DirMaxSize.HasValue && x.Size + File.Length > x.DirMaxSize
//    );
//    if (TooSmail.Count() > 0)
//    {
//        throw new LackOfSpaceException($"達到父系目錄限制大小:{string.Join(",", TooSmail.Select(x => x.Name))}");
//    }
//    DateTime now = DateTime.Now;
//    foreach (var node in ParentChain)
//    {
//        node.Size += File.Length;
//        node.Datetime = now;
//    }
//    #endregion

//    #region 儲存檔案
//    try
//    {
//        using (FileStream fileStream = System.IO.File.Create(Startup.FilesDirPath + newFile.Id))
//        {
//            await File.OpenReadStream().CopyToAsync(fileStream);
//            await fileStream.FlushAsync();
//        }
//    }
//    catch (Exception e)
//    {
//        throw new UnknowException(e.Message);
//    }
//    #endregion

//    #region FileNode建構
//    newFile.IsFile = true;
//    newFile.Owner = Owner;

//    newFile.GroupId = this.GroupId;//上傳後的檔案權限繼承
//    newFile.GroupAuthority = this.GroupAuthority;
//    newFile.OtherAuthority = this.OtherAuthority;

//    newFile.Parent = this;
//    newFile.Size = File.Length;
//    newFile.ContentType = File.ContentType;
//    newFile.Ext = File.FileName.Split('.').LastOrDefault();
//    newFile.Datetime = DateTime.Now;
//    newFile.Name = File.FileName.Substring(0, File.FileName.LastIndexOf("."));
//    #endregion

//    Database.FileNode.Add(newFile);
//    Database.SaveChanges();
//    return newFile;
//}
//EXAMPLE #240  

//File: OrgController.cs Project: yaoyel/Finework
//        public void UploadOrgAvatar(IFormFile file, Guid orgId)
//{
//    if (file == null) throw new ArgumentException(nameof(file));

//    if (!file.ContentType.StartsWith("image"))
//        throw new ArgumentException("Unsupported file type!");

//    OrgExistsResult.Check(this.m_OrgManager, orgId).ThrowIfFailed();

//    using (var reader = new StreamReader(file.OpenReadStream()))
//    {
//        m_OrgManager.UploadOrgAvatar(reader.BaseStream, orgId, file.ContentType);
//    }
//}
//EXAMPLE #250  

//File: DocumentProvision.cs Project: Microsoft/mattercenter
// private Dictionary<string, string> ContinueUpload(IFormFile uploadedFile, string fileExtension)
//{
//    Dictionary<string, string> mailProperties = new Dictionary<string, string>
//                     {
//                         { ServiceConstants.MAIL_SENDER_KEY, string.Empty },
//                         { ServiceConstants.MAIL_RECEIVER_KEY, string.Empty },
//                         { ServiceConstants.MAIL_RECEIVED_DATEKEY, string.Empty },
//                         { ServiceConstants.MAIL_CC_ADDRESS_KEY, string.Empty },
//                         { ServiceConstants.MAIL_ATTACHMENT_KEY, string.Empty },
//                         { ServiceConstants.MAIL_SEARCH_EMAIL_SUBJECT, string.Empty },
//                         { ServiceConstants.MAIL_SEARCH_EMAIL_FROM_MAILBOX_KEY, string.Empty },
//                         { ServiceConstants.MAIL_FILE_EXTENSION_KEY, fileExtension },
//                         { ServiceConstants.MAIL_IMPORTANCE_KEY, string.Empty},
//                         { ServiceConstants.MAIL_CONVERSATIONID_KEY, string.Empty},
//                         { ServiceConstants.MAIL_CONVERSATION_TOPIC_KEY, string.Empty},
//                         { ServiceConstants.MAIL_SENT_DATE_KEY, string.Empty},
//                         { ServiceConstants.MAIL_HAS_ATTACHMENTS_KEY, string.Empty},
//                         { ServiceConstants.MAIL_SENSITIVITY_KEY, string.Empty },
//                         { ServiceConstants.MAIL_CATEGORIES_KEY, string.Empty },
//                         { ServiceConstants.MailOriginalName, string.Empty}
//                     };
//    if (string.Equals(fileExtension, ServiceConstants.EMAIL_FILE_EXTENSION, StringComparison.OrdinalIgnoreCase))
//    {
//        var client = new Client()
//        {
//            Url = generalSettings.CentralRepositoryUrl
//        };

//        Users currentUserDetail = userRepository.GetLoggedInUserDetails(client);
//        mailProperties[ServiceConstants.MAIL_SEARCH_EMAIL_FROM_MAILBOX_KEY] = currentUserDetail.Name;
//        Stream fileStream = uploadedFile.OpenReadStream();
//        mailProperties = MailMessageParser.GetMailFileProperties(fileStream, mailProperties);       // Reading properties only for .eml file 

//    }
//    return mailProperties;
//}
//EXAMPLE #260  

//File: ProductController.cs Project: ciker/SimplCommerce
// private string SaveFile(IFormFile file)
//{
//    var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
//    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
//    mediaService.SaveMedia(file.OpenReadStream(), fileName, file.ContentType);
//    return fileName;
//}
//EXAMPLE #270  

//File: MobileController.cs Project: digitalcivics/happynotez
//        public async Task<IActionResult> Index(IFormFile photo, string query, string lat, string lng)
//{
//    if (photo != null)
//    {
//        Notez note = new Notez
//        {
//            Found = true,
//            Timestamp = DateTimeOffset.Now,
//            UserAgent = Request.Headers["User-Agent"],
//            HostAddress = Context.GetFeature<IHttpConnectionFeature>().RemoteIpAddress.ToString(),
//            LocationRaw = query,
//        };

//        if (!string.IsNullOrWhiteSpace(query) && (string.IsNullOrWhiteSpace(lat) || string.IsNullOrWhiteSpace(lng)))
//        {
//            using (HttpClient http = new HttpClient())
//            {
//                Stream response = await http.GetStreamAsync("http://maps.googleapis.com/maps/api/geocode/xml?address=" + Uri.EscapeDataString(query));
//                XDocument xml = XDocument.Load(response);

//                if (xml.Root.Element("status")?.Value == "OK")
//                {
//                    XElement location = xml.Root.Element("result")?.Element("geometry")?.Element("location");

//                    lat = location?.Element("lat")?.Value;
//                    lng = location?.Element("lng")?.Value;
//                }
//            }
//        }

//        double value;
//        if (double.TryParse(lat, NumberStyles.Float, CultureInfo.InvariantCulture, out value)) note.Latitude = value;
//        if (double.TryParse(lng, NumberStyles.Float, CultureInfo.InvariantCulture, out value)) note.Longitude = value;

//        _context.Notez.Add(note);
//        await _context.SaveChangesAsync();

//        string root = Path.Combine(_environment.MapPath("n"));
//        await photo.SaveAsAsync(Path.Combine(root, note.ID + ".jpg"));

//        try
//        {
//            using (Stream s = photo.OpenReadStream())
//                Helper.GenerateThumbnail(s, Path.Combine(root, "t" + note.ID + ".jpg"));
//        }
//        catch
//        {
//            note.FlagStatus = FlagStatus.Invalid;
//            await _context.SaveChangesAsync();
//        }

//        return RedirectToAction(nameof(Thanks), new { noteID = note.ID });
//    }

//    return RedirectToAction(nameof(Index));
//}
//EXAMPLE #28-1  

//File: ImportController.cs Project: nicolastarzia/allReady
//        public IActionResult Index(IFormFile file)
//{
//    // todo: - proper view model
//    //- more complete result type/info

//    if (file == null)
//    {
//        _logger.LogInformation($"User {User.Identity.Name} attempted a file upload without specifying a file.");
//        return RedirectToAction(nameof(Index));
//    }

//    using (var stream = file.OpenReadStream())
//    {
//        using (var reader = new StreamReader(stream))
//        {
//            var csvReader = new CsvReader(reader);
//            csvReader.Configuration.WillThrowOnMissingField = false;
//            csvReader.Configuration.RegisterClassMap<RedCrossRequestMap>();
//            var requests = csvReader.GetRecords<Request>().ToList();

//            var errors = _mediator.Send(new ImportRequestsCommand { Requests = requests });
//        }
//    }

//    _logger.LogDebug($"{User.Identity.Name} imported file {file.Name}");
//    ViewBag.ImportSuccess = true;

//    return View();
//}
//EXAMPLE #29-1  

//File: CarNumberController.cs Project: VysotskiVadim/bsuir-misoi-car-number
// public CarNumberResult ProcessImage(IFormFile file)
//{
//    var result = new CarNumberResult();
//    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Replace("\"", string.Empty);
//    using (var fileStram = file.OpenReadStream())
//    {
//        var image = _imageFactory.CreateImage(fileName, fileStram);
//        var processResult = _identifyService.IdentifyAsync(image).Result;
//        processResult.ProcessedImage.Name = Guid.NewGuid() + Path.GetExtension(processResult.ProcessedImage.Name);
//        _imageRepository.SaveImageAsync(processResult.ProcessedImage).Wait();
//        result.ImageUrl = _imageUrlProvider.GetImageUrl(processResult.ProcessedImage.Name);
//    }
//    return result;
//}
//EXAMPLE #30-1  

//File: UploadController.cs Project: Rafael-Pessoni/AzureStorageExample
//        public IActionResult Upload(IFormFile arquivo)
//{
//    //Cria um blob client
//    CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();

//    //Recupera a referencia do container documentos
//    CloudBlobContainer container = blobClient.GetContainerReference("documentos");

//    //Caso não exista, ele cria
//    container.CreateIfNotExists();

//    //Setar permissão de acesso para 'público'
//    container.SetPermissions(
//        new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
//    );

//    //Recupera a referência de um blob chamado 'cliente01'
//    CloudBlockBlob blockBlob = container.GetBlockBlobReference("cliente01.pdf");

//    Stream streamFile = arquivo.OpenReadStream();

//    //Cria ou substitui o blob com o conteúdo do upload
//    blockBlob.UploadFromStream(streamFile);

//    return RedirectToAction("VerArquivos");
//}


