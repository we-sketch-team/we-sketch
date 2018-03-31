using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WeSketch.App.Data.Shapes
{
    public class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            //DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as MoveThumb;
            Control designerItem = thumb.DataContext as Control;

            if (designerItem != null)
            {
                double left = Canvas.GetLeft(designerItem);
                double top = Canvas.GetTop(designerItem);

                Canvas.SetLeft(designerItem, left + e.HorizontalChange);
                Canvas.SetTop(designerItem, top + e.VerticalChange);
            }
        }
    }
}
