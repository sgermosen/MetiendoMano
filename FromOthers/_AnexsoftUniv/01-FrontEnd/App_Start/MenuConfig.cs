using Common;
using Service;

namespace FrontEnd.App_Start
{
    public class MenuConfig
    {
        public static void Initialize()
        {
            var _categoryService = DependecyFactory.GetInstance<ICategoryService>();
            Parameters.CategoryList = _categoryService.GetForMenu();
        }
    }
}