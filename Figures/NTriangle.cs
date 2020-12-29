﻿using System.Drawing;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NPaint.Figures
{
    class NTriangle : Figure
    {
        public NTriangle(Point point)
        {
            adaptedPath = new Path();
            adaptedGeometry = new PathGeometry();
            startPoint = point;
        }

        public override void MoveBy(Point point)
        {
            throw new System.NotImplementedException();
        }

        public override void Resize(Point point)
        {
            throw new System.NotImplementedException();
        }
    }
}
