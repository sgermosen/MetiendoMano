using Common;
using System.Collections.Generic;
using System.Linq;

namespace FrontEnd.ViewModels
{
    public class PanelMenuViewModel
    {
        public Enums.PanelMenu id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public string Icon { get; set; }
        public bool Selected { get; set; }
        public int Order { get; set; }

        public static IEnumerable<PanelMenuViewModel> GetAll(Enums.PanelMenu id)
        {
            var result = new List<PanelMenuViewModel> {
                new PanelMenuViewModel
                {
                    id = Enums.PanelMenu.Home,
                    Name = "Inicio",
                    Href = "/panel",
                    Icon = "fa-home",
                    Order = 1
                },
                new PanelMenuViewModel
                {
                    id = Enums.PanelMenu.Categories,
                    Name = "Categorías",
                    Href = "/panel/categories",
                    Icon = "fa fa-object-group",
                    Order = 2
                },
                new PanelMenuViewModel
                {
                    id = Enums.PanelMenu.Courses,
                    Name = "Cursos",
                    Href = "/panel/courses",
                    Icon = "fa-book",
                    Order = 3
                },
                new PanelMenuViewModel
                {
                    id = Enums.PanelMenu.Users,
                    Name = "Usuarios",
                    Href = "/panel/users",
                    Icon = "fa-user",
                    Order = 4
                },
            };

            return result.Select(x => new PanelMenuViewModel {
                id = x.id,
                Name = x.Name,
                Href = x.Href,
                Icon = x.Icon,
                Selected = x.id == id
            });
        }
    }
}