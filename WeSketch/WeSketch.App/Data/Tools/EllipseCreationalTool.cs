﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Data.Tools
{
    public class EllipseCreationalTool : TwoPointCreationalTool
    {
        public override IShape GetShapeInstance()
        {
            int width = Math.Abs(startPoint.X - endPoint.X);
            int height = Math.Abs(startPoint.Y - endPoint.Y);

            int setPosX = Math.Min(startPoint.X, endPoint.X);
            int setPosY = Math.Min(startPoint.Y, endPoint.Y);

            var ellipse = ShapesFactory.GetEllipseInstance();
            ellipse.SetWidth(width);
            ellipse.SetHeight(height);
            ellipse.Move(setPosX, setPosY);
            ellipse.Move(setPosX, setPosY);
            return ellipse;
        }
    }
}
