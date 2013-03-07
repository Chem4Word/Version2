using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chem4Word.UI.TwoD
{



   public  class DisplayBox : FrameworkElement {
        /// <summary>
        ///   QUESTION
        ///   should we pass this canvas around or can we always get to it as "Parent"
        /// </summary>
        private readonly ChemCanvas theCanvas;
        private readonly Pen visibleStrokePen = new Pen(Brushes.Gray, ViewBox.VisibleStrokeWidth) { DashStyle = DashStyles.Dash };

        /// <summary>
        ///   Required to provide the extention functionallity from FrameworkElement
        /// </summary>
        private VisualCollection children;


        public DisplayBox(ChemCanvas chemCanvas) {
            children = new VisualCollection(this);
            theCanvas = chemCanvas;
            Refresh();
        }

        /// <summary>
        ///   Provide a required override for the VisualChildrenCount property.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }

        private void Refresh()
        {
            var drawingVisual = new DrawingVisual();
            var drawingContext =
                drawingVisual.RenderOpen();

            var initialRect = theCanvas.ContextObject.ViewBoxDimensions;

            var rect = new Rect(theCanvas.ToScreenX(initialRect.X), theCanvas.ToScreenY(initialRect.Y),
                                theCanvas.ToScreenX(initialRect.Width),
                                Math.Abs(theCanvas.ToScreenY(initialRect.Height)));
            
            rect.X += ViewBox.VisibleStrokeWidth;
            rect.Y += ViewBox.VisibleStrokeWidth;
            rect.Width -= 3 * ViewBox.VisibleStrokeWidth;
            rect.Height -=  3 * ViewBox.VisibleStrokeWidth;

            var rectangleGeometry = new RectangleGeometry(rect);
            drawingContext.DrawGeometry(null, visibleStrokePen, rectangleGeometry);

            drawingContext.Close();
            children.Add(drawingVisual);
        }

        /// <summary>
        ///   Provide a required override for the GetVisualChild method.
        /// </summary>
        /// <param name = "index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= children.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return children[index];
        }
    }
}
