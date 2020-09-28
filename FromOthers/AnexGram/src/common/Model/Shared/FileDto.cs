using System;
using System.IO;
using System.Linq;

namespace Model.Shared
{
    public class FileDto
    {
        public string Content { get; set; }
        public string ContentType { get; set; }
        public int Length { get; set; }
        public string Name { get; set; }

        public MemoryStream ReadAsStream() => new MemoryStream(
             Convert.FromBase64String(
                 Content
             )
         );

        /// <summary>
        /// Get extension name
        /// </summary>
        public string Extension
        {
            get
            {
                return Name.Split('.').ToList().Last();
            }
        }

        /// <summary>
        /// Get unique name
        /// </summary>
        public string UniqueName
        {
            get
            {
                return $"{Guid.NewGuid().ToString().ToLower()}.{Extension}";
            }
        }
    }
}
