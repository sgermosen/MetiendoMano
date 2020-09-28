using LineDietXF.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(CustomViewCellRenderer))]
namespace LineDietXF.iOS.Renderers
{
    /// <summary>
    /// The intent of this custom renderer is to change the selected row color in a listview
    /// NOTE:: For some reason this only works on the main menu, row selection of gray is still seen on weight listing listview
    /// </summary>
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = UIColor.Clear
            };

            return cell;
        }
    }
}