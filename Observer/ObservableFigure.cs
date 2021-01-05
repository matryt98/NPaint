﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using NPaint.Figures;

namespace NPaint.Observer
{
    class ObservableFigure : NRectangle, Observable
    {
        private List<Figure> Observers;
        public ObservableFigure()
        {
            adaptedPath = new Path();
            adaptedPath.Fill = Brushes.Transparent;
            adaptedPath.StrokeThickness = 1;
            adaptedPath.Stroke = Brushes.Gray;
            adaptedPath.StrokeDashArray = new DoubleCollection() { 3 };
            adaptedGeometry = new RectangleGeometry();
            rect = new Rect();
            Observers = new List<Figure>();
        }
        public void Attach(Figure figure)
        {
            Observers.Add(figure);
        }

        public void Detach(Figure figure)
        {
            Observers.Remove(figure);
        }

        public void Notify(Point point)
        {
            foreach(Figure figure in Observers)
            {
                // poki co to nie przejdzie, przez te shifty
                //figure.MoveBy(point);

                // a to do sprawdzania jakie figury sie zaznaczyly
                figure.ChangeFillColor(Brushes.Azure);
            }
        }
        public override void MoveBy(Point point)
        {
            // poki co to nie przejdzie, przez te shifty
            //base.MoveBy(point);
            Notify(point);
        }
        public bool Contains(Figure figure)
        {
            // poki co zaznaczam wszystkie figury ktore sa prostokatami
            if (figure.GetType() == typeof(NRectangle))
                return true;
            else
                return false;
        }
    }
}
