using LineDietXF.Enumerations;

namespace LineDietXF
{
    public interface IWindowColorService
    {
        void ChangeAppBaseColor(BaseColorEnum colorEnum);
        void ResetAppBaseColor();
    }
}