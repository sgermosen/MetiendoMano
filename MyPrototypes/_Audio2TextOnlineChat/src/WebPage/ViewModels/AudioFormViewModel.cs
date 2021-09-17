using Microsoft.AspNetCore.Http;
using Web.Core.Models;

namespace WebPage.ViewModels
{
    public class AudioFormCreateViewModel : EntityBase
    {
        public string Title { get; set; }
        public IFormFile File { get; set; }
    }
}
