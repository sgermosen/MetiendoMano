using LineDietXF.Enumerations;

namespace LineDietXF
{
    /// <summary>
    /// Menu item definitions as used on the main menu
    /// </summary>
    public class MenuItem
    {
        public MenuItemEnum MenuType { get; set; }
        public string Name { get; set; }
        public bool IsDivider { get; set; }

        public MenuItem(MenuItemEnum menuType, string name, bool isDivider)
        {
            MenuType = menuType;
            Name = name;
            IsDivider = isDivider;
        }
    }
}