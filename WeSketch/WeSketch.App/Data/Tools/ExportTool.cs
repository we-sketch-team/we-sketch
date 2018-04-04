using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WeSketch.App.Controller;
using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using WeSketch.App.Forms;

namespace WeSketch.App.Data.Tools
{
	public class ExportTool : ITool
	{
		IDrawable drawable;

		public ExportTool(IDrawable draw)
		{
			drawable = draw;
		}

		public void Activate()
		{	
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.ShowDialog();
			string path = saveFileDialog.FileName;

			FileStream fs = new FileStream(path, FileMode.Create);

			var canvas = drawable.GetCanvas();

			RenderTargetBitmap bmp = new RenderTargetBitmap((int)canvas.ActualWidth,
				(int)canvas.ActualHeight, 1 / 96, 1 / 96, PixelFormats.Pbgra32);

			bmp.Render(canvas);
			BitmapEncoder encoder = new TiffBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(bmp));
			encoder.Save(fs);
			fs.Close();
		}

		public void Deactivate()
		{
			
		}

		public void MouseDown(int x, int y)
		{
			
		}

		public void MouseUp(int x, int y)
		{
			
		}

		public void SetController(IWorkspaceController controller)
		{
			
		}
	}
}
