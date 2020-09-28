using System;

namespace Model.Shared
{
    public class PhotoCreateDto
    {
        public string Url { get; set; }
        public string UserId { get; set; }
    }

    public class PhotoListFilter
    {
        public string UserId { get; set; }
        public string SeoUrl { get; set; }
    }

    public class PhotoListDto
    {
        // Id de la foto
        public int PhotoId { get; set; }

        // La imagen
        public string Image { get; set; }

        // La imagen + las ruta
        public string ImagePath
        {
            get
            {
                return $"/Uploads/{Image}";
            }
        }

        // El usuario que creeo la foto
        public string UserId { get; set; }
        // El nombre del usuario creador
        public string UserName { get; set; }
        public string UserPic { get; set; }
        public string UserSeoUrl { get; set; }

        // Likes que ha obtenido
        public int Likes { get; set; }
        // Cantidad de comentarios
        public int Comments { get; set; }

        // Si es que yo le di Like a esta foto
        public bool ILikedIt { get; set; }

        // La fecha que fue creada
        public DateTime? CreatedAt { get; set; }
    }
}
