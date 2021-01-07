﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NPaint.Figures
{
    [Serializable]///
    class NTriangle : Figure
    {
        private PathFigure PathFigure;
        private Point point1; //można by było chyba bezpośrednio ustawiać ten StartPoint i Line'y
        private LineSegment line1;
        private Point point2;
        private LineSegment line2;
        private Point point3;

        public NTriangle() : base()
        {
            // inicjalizacja zmiennych
            adaptedPath = new Path();
            adaptedGeometry = new PathGeometry();
            adaptedPath.Data = adaptedGeometry;
            PathFigure = new PathFigure();
            line1 = new LineSegment();
            line2 = new LineSegment();

            PathFigure.IsClosed = true; // domkniecie trojkata 
        }

        public override void MoveBy(Point point)
        {
            
            double y = point.Y;
            double x = point.X;

            //////Canvas.SetTop(this.adaptedPath, y);
            //////Canvas.SetLeft(this.adaptedPath, x);

            double lengthShift = point1.Y - y;
            double widthShift = point1.X - x;
             
            point1.Y = y;
            point1.X = x;
            point2.Y -= lengthShift;
            point2.X -= widthShift;
            point3.Y -= lengthShift;
            point3.X -= widthShift;
            

            //PathFigure.StartPoint = new Point(x, y);
            //Nie wiem czy nie trza ustawić pól figury czy już są ustawione
            // trzeba, bo geometry zostaje w tym samym miejscu, choc path sie przemieszcza

            Repaint();
        }

        public override void MoveByInsideGroup(Point point)
        {
            //////////Vector vector = VisualTreeHelper.GetOffset(this.adaptedPath);
            ///////////Point positionOfTriangle = new Point(vector.X, vector.Y);
            ///////Canvas.SetTop(this.adaptedPath, positionOfTriangle.Y - point.Y);
            //////Canvas.SetLeft(this.adaptedPath, positionOfTriangle.X - point.X);
            point1.Y -= point.Y;
            point1.X -= point.X;
            point2.Y -= point.Y;
            point2.X -= point.X;
            point3.Y -= point.Y;
            point3.X -= point.X;


            Repaint();
        }
        public override void Resize(Point point)
        {
            // obliczenie polozenia lewego dolnego wierzcholka
            point1.X = Math.Min(point.X, startPoint.X);
            point1.Y = Math.Max(point.Y, startPoint.Y);

            // obliczenie polozenia prawego dolnego wierzcholka
            point2.X = Math.Max(point.X, startPoint.X);
            point2.Y = Math.Max(point.Y, startPoint.Y);

            // obliczenie polozenia gornego wierzcholka
            point3.X = MidPointX(point.X,startPoint.X);
            point3.Y = Math.Min(point.Y, startPoint.Y);

            Repaint();
        }

        public override void DecreaseSize()
        {
            // lewy dolny 
            point1.X++;
            point1.Y--;

            // prawy dolny
            point2.X--;
            point2.Y--;

            //srodkowy
            point3.Y++;

            Repaint();
        }
        public override void IncreaseSize()
        {
            // lewy dolny 
            point1.X--;
            point1.Y++;

            // prawy dolny
            point2.X++;
            point2.Y++;
            
            //srodkowy
            point3.Y--;

            Repaint();
        }

        protected override void SetPointCollection()
        {
            // do zaznaczenia trojkata potrzebne sa wszystkie 3 wierzcholki
            PointsList.Clear();
            PointsList.Add(PathFigure.StartPoint);    // lewy dolny
            PointsList.Add(line1.Point);              // prawy dolny
            PointsList.Add(line2.Point);              // gorny
        }

        private double MidPointX(double a, double b)
        {
            return (a+b)/2;
        }

        // musimy klonowac tez prywatne obiekty NTriangle, ale tylko te ktore sa inicjowane przez new
        public override object Clone()
        {
            NTriangle clonedFigure = base.Clone() as NTriangle;
            clonedFigure.PathFigure = PathFigure.Clone();
            clonedFigure.PathFigure.IsClosed = true;
            clonedFigure.line1 = line1.Clone();
            clonedFigure.line2 = line2.Clone();
            return clonedFigure;
        }

        public Point GetTopLeft()
        {
            return this.adaptedGeometry.Bounds.TopLeft;
        }

        public Point GetLeftDownCorner()
        {
            return PathFigure.StartPoint;
        }

        protected override void Repaint()
        {
            PathFigure.StartPoint = point1;
            line1.Point = point2;
            line2.Point = point3;

            PathFigure.Segments.Clear(); //czyszczenie niezbedne nwm dlaczego insert powinien zalatwic sprawe
            PathFigure.Segments.Insert(0, line1);
            PathFigure.Segments.Insert(1, line2);

            ((PathGeometry)adaptedGeometry).Figures.Clear(); //czyszczenie ten sam problem
            ((PathGeometry)adaptedGeometry).Figures.Insert(0, PathFigure);    // przypisanie figury trojkata do geometrii

            adaptedPath.Data = adaptedGeometry;

            SetPointCollection();
        }
    }
}
