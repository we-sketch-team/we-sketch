﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeSketch.App.Data.Shapes
{
    class ShapeComposite : IShape
    {
        private List<IShape> elements;
        private Canvas wrapper;

        public ShapeComposite()
        {
            elements = new List<IShape>();
            wrapper = new Canvas();
            wrapper.Width = 2000;
            wrapper.Height = 2000;
        }

        public void AddShape(IShape shape)
        {
            elements.Add(shape);
        }

        public void RemoveShape(IShape shape)
        {
            elements.Remove(shape);
        }


        public void Delete(Canvas target)
        {
            target.Children.Remove(wrapper);
        }

        public void Draw(Canvas target)
        {
            elements.ForEach(el => el.Draw(wrapper));
            target.Children.Add(wrapper);
        }

        public void SetLeft(int left)
        {
            throw new NotImplementedException();
        }

        public void SetRight(int right)
        {
            throw new NotImplementedException();
        }

        public void SetTop(int top)
        {
            throw new NotImplementedException();
        }

        public void SetBottom(int bottom)
        {
            throw new NotImplementedException();
        }

        public void Move(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}