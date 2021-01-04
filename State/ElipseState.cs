﻿using System;
using System.Windows;
using NPaint.Figures;

namespace NPaint.State
{
    class EllipseState : MenuState
    {
        public override void MouseLeftButtonDown(Point point)
        {
            ShapeFactory shapeFactory = ShapeFactory.getShapeFactory();
            Figure = (NEllipse)shapeFactory.getFigure("Ellipse");
            Figure.SetStartPoint(point);
            ((MainWindow)Application.Current.MainWindow).AddFigure(Figure);
            MouseMoveToResize(point);
        }

        public override void MouseLeftButtonUp(Point point)
        {
            ((MainWindow)Application.Current.MainWindow).SetSelectedFigure(Figure);
        }

        public override void MouseMoveToMove(Point point)
        {
            Figure.MoveBy(point);
        }

        public override void MouseMoveToResize(Point point)
        {
            Figure.Resize(point);
        }
    }
}
