using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Wpg = DocumentFormat.OpenXml.Office2010.Word.DrawingGroup;
using Wps = DocumentFormat.OpenXml.Office2010.Word.DrawingShape;

namespace Chem4Word.UI.Molecules
{
    public static class Pyridine
    {
        // Creates an Run instance and adds its children.
        public static Run GenerateRun()
        {
            Run run1 = new Run();

            var drawing1 = new DocumentFormat.OpenXml.Wordprocessing.Drawing();

            Wp.Inline inline1 = new Wp.Inline() { DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)0U, DistanceFromRight = (UInt32Value)0U };
            Wp.Extent extent1 = new Wp.Extent() { Cx = 348996L, Cy = 437209L };
            Wp.EffectExtent effectExtent1 = new Wp.EffectExtent() { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L };
            Wp.DocProperties docProperties1 = new Wp.DocProperties() { Id = (UInt32Value)1U, Name = "Group2" };

            A.Graphic graphic1 = new A.Graphic();
            graphic1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.GraphicData graphicData1 = new A.GraphicData() { Uri = "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" };

            Wpg.WordprocessingGroup wordprocessingGroup1 = new Wpg.WordprocessingGroup();
            Wpg.NonVisualGroupDrawingShapeProperties nonVisualGroupDrawingShapeProperties1 = new Wpg.NonVisualGroupDrawingShapeProperties();

            Wpg.GroupShapeProperties groupShapeProperties1 = new Wpg.GroupShapeProperties();

            A.TransformGroup transformGroup1 = new A.TransformGroup();
            A.Offset offset1 = new A.Offset() { X = 0L, Y = 0L };
            A.Extents extents1 = new A.Extents() { Cx = 348996L, Cy = 437209L };
            A.ChildOffset childOffset1 = new A.ChildOffset() { X = 0L, Y = 0L };
            A.ChildExtents childExtents1 = new A.ChildExtents() { Cx = 348996L, Cy = 437209L };

            transformGroup1.Append(offset1);
            transformGroup1.Append(extents1);
            transformGroup1.Append(childOffset1);
            transformGroup1.Append(childExtents1);

            groupShapeProperties1.Append(transformGroup1);

            Wps.WordprocessingShape wordprocessingShape1 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)3U, Name = "Curve4" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties1 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties1 = new Wps.ShapeProperties();

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset2 = new A.Offset() { X = 9525L, Y = 72792L };
            A.Extents extents2 = new A.Extents() { Cx = 119753L, Cy = 69142L };

            transform2D1.Append(offset2);
            transform2D1.Append(extents2);

            A.CustomGeometry customGeometry1 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();
            A.Rectangle rectangle1 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList1 = new A.PathList();

            A.Path path1 = new A.Path() { Width = 119753L, Height = 69142L };

            A.MoveTo moveTo1 = new A.MoveTo();
            A.Point point1 = new A.Point() { X = "119753", Y = "0" };

            moveTo1.Append(point1);

            A.LineTo lineTo1 = new A.LineTo();
            A.Point point2 = new A.Point() { X = "0", Y = "69142" };

            lineTo1.Append(point2);

            path1.Append(moveTo1);
            path1.Append(lineTo1);

            pathList1.Append(path1);

            customGeometry1.Append(adjustValueList1);
            customGeometry1.Append(rectangle1);
            customGeometry1.Append(pathList1);

            A.Outline outline1 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill1 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha1 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex1.Append(alpha1);

            solidFill1.Append(rgbColorModelHex1);

            outline1.Append(solidFill1);

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(customGeometry1);
            shapeProperties1.Append(outline1);

            Wps.ShapeStyle shapeStyle1 = new Wps.ShapeStyle();
            A.LineReference lineReference1 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference1 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference1 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference1 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle1.Append(lineReference1);
            shapeStyle1.Append(fillReference1);
            shapeStyle1.Append(effectReference1);
            shapeStyle1.Append(fontReference1);
            Wps.TextBodyProperties textBodyProperties1 = new Wps.TextBodyProperties();

            wordprocessingShape1.Append(nonVisualDrawingProperties1);
            wordprocessingShape1.Append(nonVisualDrawingShapeProperties1);
            wordprocessingShape1.Append(shapeProperties1);
            wordprocessingShape1.Append(shapeStyle1);
            wordprocessingShape1.Append(textBodyProperties1);

            Wps.WordprocessingShape wordprocessingShape2 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties2 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)5U, Name = "Curve6" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties2 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties2 = new Wps.ShapeProperties();

            A.Transform2D transform2D2 = new A.Transform2D();
            A.Offset offset3 = new A.Offset() { X = 9525L, Y = 332434L };
            A.Extents extents3 = new A.Extents() { Cx = 164973L, Cy = 95250L };

            transform2D2.Append(offset3);
            transform2D2.Append(extents3);

            A.CustomGeometry customGeometry2 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList2 = new A.AdjustValueList();
            A.Rectangle rectangle2 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList2 = new A.PathList();

            A.Path path2 = new A.Path() { Width = 164973L, Height = 95250L };

            A.MoveTo moveTo2 = new A.MoveTo();
            A.Point point3 = new A.Point() { X = "0", Y = "0" };

            moveTo2.Append(point3);

            A.LineTo lineTo2 = new A.LineTo();
            A.Point point4 = new A.Point() { X = "164973", Y = "95250" };

            lineTo2.Append(point4);

            path2.Append(moveTo2);
            path2.Append(lineTo2);

            pathList2.Append(path2);

            customGeometry2.Append(adjustValueList2);
            customGeometry2.Append(rectangle2);
            customGeometry2.Append(pathList2);

            A.Outline outline2 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill2 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha2 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex2.Append(alpha2);

            solidFill2.Append(rgbColorModelHex2);

            outline2.Append(solidFill2);

            shapeProperties2.Append(transform2D2);
            shapeProperties2.Append(customGeometry2);
            shapeProperties2.Append(outline2);

            Wps.ShapeStyle shapeStyle2 = new Wps.ShapeStyle();
            A.LineReference lineReference2 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference2 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference2 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference2 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle2.Append(lineReference2);
            shapeStyle2.Append(fillReference2);
            shapeStyle2.Append(effectReference2);
            shapeStyle2.Append(fontReference2);
            Wps.TextBodyProperties textBodyProperties2 = new Wps.TextBodyProperties();

            wordprocessingShape2.Append(nonVisualDrawingProperties2);
            wordprocessingShape2.Append(nonVisualDrawingShapeProperties2);
            wordprocessingShape2.Append(shapeProperties2);
            wordprocessingShape2.Append(shapeStyle2);
            wordprocessingShape2.Append(textBodyProperties2);

            Wps.WordprocessingShape wordprocessingShape3 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties3 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)7U, Name = "Curve8" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties3 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties3 = new Wps.ShapeProperties();

            A.Transform2D transform2D3 = new A.Transform2D();
            A.Offset offset4 = new A.Offset() { X = 339471L, Y = 141934L };
            A.Extents extents4 = new A.Extents() { Cx = 0L, Cy = 190500L };

            transform2D3.Append(offset4);
            transform2D3.Append(extents4);

            A.CustomGeometry customGeometry3 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList3 = new A.AdjustValueList();
            A.Rectangle rectangle3 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList3 = new A.PathList();

            A.Path path3 = new A.Path() { Width = 0L, Height = 190500L };

            A.MoveTo moveTo3 = new A.MoveTo();
            A.Point point5 = new A.Point() { X = "0", Y = "190500" };

            moveTo3.Append(point5);

            A.LineTo lineTo3 = new A.LineTo();
            A.Point point6 = new A.Point() { X = "0", Y = "0" };

            lineTo3.Append(point6);

            path3.Append(moveTo3);
            path3.Append(lineTo3);

            pathList3.Append(path3);

            customGeometry3.Append(adjustValueList3);
            customGeometry3.Append(rectangle3);
            customGeometry3.Append(pathList3);

            A.Outline outline3 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill3 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha3 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex3.Append(alpha3);

            solidFill3.Append(rgbColorModelHex3);

            outline3.Append(solidFill3);

            shapeProperties3.Append(transform2D3);
            shapeProperties3.Append(customGeometry3);
            shapeProperties3.Append(outline3);

            Wps.ShapeStyle shapeStyle3 = new Wps.ShapeStyle();
            A.LineReference lineReference3 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference3 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference3 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference3 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle3.Append(lineReference3);
            shapeStyle3.Append(fillReference3);
            shapeStyle3.Append(effectReference3);
            shapeStyle3.Append(fontReference3);
            Wps.TextBodyProperties textBodyProperties3 = new Wps.TextBodyProperties();

            wordprocessingShape3.Append(nonVisualDrawingProperties3);
            wordprocessingShape3.Append(nonVisualDrawingShapeProperties3);
            wordprocessingShape3.Append(shapeProperties3);
            wordprocessingShape3.Append(shapeStyle3);
            wordprocessingShape3.Append(textBodyProperties3);

            Wps.WordprocessingShape wordprocessingShape4 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties4 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)9U, Name = "Curve10" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties4 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties4 = new Wps.ShapeProperties();

            A.Transform2D transform2D4 = new A.Transform2D();
            A.Offset offset5 = new A.Offset() { X = 9525L, Y = 141934L };
            A.Extents extents5 = new A.Extents() { Cx = 0L, Cy = 190500L };

            transform2D4.Append(offset5);
            transform2D4.Append(extents5);

            A.CustomGeometry customGeometry4 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList4 = new A.AdjustValueList();
            A.Rectangle rectangle4 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList4 = new A.PathList();

            A.Path path4 = new A.Path() { Width = 0L, Height = 190500L };

            A.MoveTo moveTo4 = new A.MoveTo();
            A.Point point7 = new A.Point() { X = "0", Y = "0" };

            moveTo4.Append(point7);

            A.LineTo lineTo4 = new A.LineTo();
            A.Point point8 = new A.Point() { X = "0", Y = "190500" };

            lineTo4.Append(point8);

            path4.Append(moveTo4);
            path4.Append(lineTo4);

            pathList4.Append(path4);

            customGeometry4.Append(adjustValueList4);
            customGeometry4.Append(rectangle4);
            customGeometry4.Append(pathList4);

            A.Outline outline4 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill4 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha4 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex4.Append(alpha4);

            solidFill4.Append(rgbColorModelHex4);

            outline4.Append(solidFill4);

            shapeProperties4.Append(transform2D4);
            shapeProperties4.Append(customGeometry4);
            shapeProperties4.Append(outline4);

            Wps.ShapeStyle shapeStyle4 = new Wps.ShapeStyle();
            A.LineReference lineReference4 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference4 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference4 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference4 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle4.Append(lineReference4);
            shapeStyle4.Append(fillReference4);
            shapeStyle4.Append(effectReference4);
            shapeStyle4.Append(fontReference4);
            Wps.TextBodyProperties textBodyProperties4 = new Wps.TextBodyProperties();

            wordprocessingShape4.Append(nonVisualDrawingProperties4);
            wordprocessingShape4.Append(nonVisualDrawingShapeProperties4);
            wordprocessingShape4.Append(shapeProperties4);
            wordprocessingShape4.Append(shapeStyle4);
            wordprocessingShape4.Append(textBodyProperties4);

            Wps.WordprocessingShape wordprocessingShape5 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties5 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)11U, Name = "Curve12" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties5 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties5 = new Wps.ShapeProperties();

            A.Transform2D transform2D5 = new A.Transform2D();
            A.Offset offset6 = new A.Offset() { X = 34925L, Y = 167334L };
            A.Extents extents6 = new A.Extents() { Cx = 0L, Cy = 139700L };

            transform2D5.Append(offset6);
            transform2D5.Append(extents6);

            A.CustomGeometry customGeometry5 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList5 = new A.AdjustValueList();
            A.Rectangle rectangle5 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList5 = new A.PathList();

            A.Path path5 = new A.Path() { Width = 0L, Height = 139700L };

            A.MoveTo moveTo5 = new A.MoveTo();
            A.Point point9 = new A.Point() { X = "0", Y = "0" };

            moveTo5.Append(point9);

            A.LineTo lineTo5 = new A.LineTo();
            A.Point point10 = new A.Point() { X = "0", Y = "139700" };

            lineTo5.Append(point10);

            path5.Append(moveTo5);
            path5.Append(lineTo5);

            pathList5.Append(path5);

            customGeometry5.Append(adjustValueList5);
            customGeometry5.Append(rectangle5);
            customGeometry5.Append(pathList5);

            A.Outline outline5 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill5 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha5 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex5.Append(alpha5);

            solidFill5.Append(rgbColorModelHex5);

            outline5.Append(solidFill5);

            shapeProperties5.Append(transform2D5);
            shapeProperties5.Append(customGeometry5);
            shapeProperties5.Append(outline5);

            Wps.ShapeStyle shapeStyle5 = new Wps.ShapeStyle();
            A.LineReference lineReference5 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference5 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference5 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference5 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle5.Append(lineReference5);
            shapeStyle5.Append(fillReference5);
            shapeStyle5.Append(effectReference5);
            shapeStyle5.Append(fontReference5);
            Wps.TextBodyProperties textBodyProperties5 = new Wps.TextBodyProperties();

            wordprocessingShape5.Append(nonVisualDrawingProperties5);
            wordprocessingShape5.Append(nonVisualDrawingShapeProperties5);
            wordprocessingShape5.Append(shapeProperties5);
            wordprocessingShape5.Append(shapeStyle5);
            wordprocessingShape5.Append(textBodyProperties5);

            Wps.WordprocessingShape wordprocessingShape6 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties6 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)13U, Name = "Curve14" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties6 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties6 = new Wps.ShapeProperties();

            A.Transform2D transform2D6 = new A.Transform2D();
            A.Offset offset7 = new A.Offset() { X = 174498L, Y = 332434L };
            A.Extents extents7 = new A.Extents() { Cx = 164973L, Cy = 95250L };

            transform2D6.Append(offset7);
            transform2D6.Append(extents7);

            A.CustomGeometry customGeometry6 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList6 = new A.AdjustValueList();
            A.Rectangle rectangle6 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList6 = new A.PathList();

            A.Path path6 = new A.Path() { Width = 164973L, Height = 95250L };

            A.MoveTo moveTo6 = new A.MoveTo();
            A.Point point11 = new A.Point() { X = "0", Y = "95250" };

            moveTo6.Append(point11);

            A.LineTo lineTo6 = new A.LineTo();
            A.Point point12 = new A.Point() { X = "164973", Y = "0" };

            lineTo6.Append(point12);

            path6.Append(moveTo6);
            path6.Append(lineTo6);

            pathList6.Append(path6);

            customGeometry6.Append(adjustValueList6);
            customGeometry6.Append(rectangle6);
            customGeometry6.Append(pathList6);

            A.Outline outline6 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill6 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha6 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex6.Append(alpha6);

            solidFill6.Append(rgbColorModelHex6);

            outline6.Append(solidFill6);

            shapeProperties6.Append(transform2D6);
            shapeProperties6.Append(customGeometry6);
            shapeProperties6.Append(outline6);

            Wps.ShapeStyle shapeStyle6 = new Wps.ShapeStyle();
            A.LineReference lineReference6 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference6 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference6 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference6 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle6.Append(lineReference6);
            shapeStyle6.Append(fillReference6);
            shapeStyle6.Append(effectReference6);
            shapeStyle6.Append(fontReference6);
            Wps.TextBodyProperties textBodyProperties6 = new Wps.TextBodyProperties();

            wordprocessingShape6.Append(nonVisualDrawingProperties6);
            wordprocessingShape6.Append(nonVisualDrawingShapeProperties6);
            wordprocessingShape6.Append(shapeProperties6);
            wordprocessingShape6.Append(shapeStyle6);
            wordprocessingShape6.Append(textBodyProperties6);

            Wps.WordprocessingShape wordprocessingShape7 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties7 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)15U, Name = "Curve16" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties7 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties7 = new Wps.ShapeProperties();

            A.Transform2D transform2D7 = new A.Transform2D();
            A.Offset offset8 = new A.Offset() { X = 183795L, Y = 323137L };
            A.Extents extents8 = new A.Extents() { Cx = 120979L, Cy = 69849L };

            transform2D7.Append(offset8);
            transform2D7.Append(extents8);

            A.CustomGeometry customGeometry7 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList7 = new A.AdjustValueList();
            A.Rectangle rectangle7 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList7 = new A.PathList();

            A.Path path7 = new A.Path() { Width = 120979L, Height = 69849L };

            A.MoveTo moveTo7 = new A.MoveTo();
            A.Point point13 = new A.Point() { X = "0", Y = "69849" };

            moveTo7.Append(point13);

            A.LineTo lineTo7 = new A.LineTo();
            A.Point point14 = new A.Point() { X = "120979", Y = "0" };

            lineTo7.Append(point14);

            path7.Append(moveTo7);
            path7.Append(lineTo7);

            pathList7.Append(path7);

            customGeometry7.Append(adjustValueList7);
            customGeometry7.Append(rectangle7);
            customGeometry7.Append(pathList7);

            A.Outline outline7 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill7 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha7 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex7.Append(alpha7);

            solidFill7.Append(rgbColorModelHex7);

            outline7.Append(solidFill7);

            shapeProperties7.Append(transform2D7);
            shapeProperties7.Append(customGeometry7);
            shapeProperties7.Append(outline7);

            Wps.ShapeStyle shapeStyle7 = new Wps.ShapeStyle();
            A.LineReference lineReference7 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference7 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference7 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference7 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle7.Append(lineReference7);
            shapeStyle7.Append(fillReference7);
            shapeStyle7.Append(effectReference7);
            shapeStyle7.Append(fontReference7);
            Wps.TextBodyProperties textBodyProperties7 = new Wps.TextBodyProperties();

            wordprocessingShape7.Append(nonVisualDrawingProperties7);
            wordprocessingShape7.Append(nonVisualDrawingShapeProperties7);
            wordprocessingShape7.Append(shapeProperties7);
            wordprocessingShape7.Append(shapeStyle7);
            wordprocessingShape7.Append(textBodyProperties7);

            Wps.WordprocessingShape wordprocessingShape8 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties8 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)17U, Name = "Curve18" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties8 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties8 = new Wps.ShapeProperties();

            A.Transform2D transform2D8 = new A.Transform2D();
            A.Offset offset9 = new A.Offset() { X = 219959L, Y = 72932L };
            A.Extents extents9 = new A.Extents() { Cx = 119512L, Cy = 69002L };

            transform2D8.Append(offset9);
            transform2D8.Append(extents9);

            A.CustomGeometry customGeometry8 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList8 = new A.AdjustValueList();
            A.Rectangle rectangle8 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList8 = new A.PathList();

            A.Path path8 = new A.Path() { Width = 119512L, Height = 69002L };

            A.MoveTo moveTo8 = new A.MoveTo();
            A.Point point15 = new A.Point() { X = "0", Y = "0" };

            moveTo8.Append(point15);

            A.LineTo lineTo8 = new A.LineTo();
            A.Point point16 = new A.Point() { X = "119512", Y = "69002" };

            lineTo8.Append(point16);

            path8.Append(moveTo8);
            path8.Append(lineTo8);

            pathList8.Append(path8);

            customGeometry8.Append(adjustValueList8);
            customGeometry8.Append(rectangle8);
            customGeometry8.Append(pathList8);

            A.Outline outline8 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill8 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha8 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex8.Append(alpha8);

            solidFill8.Append(rgbColorModelHex8);

            outline8.Append(solidFill8);

            shapeProperties8.Append(transform2D8);
            shapeProperties8.Append(customGeometry8);
            shapeProperties8.Append(outline8);

            Wps.ShapeStyle shapeStyle8 = new Wps.ShapeStyle();
            A.LineReference lineReference8 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference8 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference8 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference8 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle8.Append(lineReference8);
            shapeStyle8.Append(fillReference8);
            shapeStyle8.Append(effectReference8);
            shapeStyle8.Append(fontReference8);
            Wps.TextBodyProperties textBodyProperties8 = new Wps.TextBodyProperties();

            wordprocessingShape8.Append(nonVisualDrawingProperties8);
            wordprocessingShape8.Append(nonVisualDrawingShapeProperties8);
            wordprocessingShape8.Append(shapeProperties8);
            wordprocessingShape8.Append(shapeStyle8);
            wordprocessingShape8.Append(textBodyProperties8);

            Wps.WordprocessingShape wordprocessingShape9 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties9 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)19U, Name = "Curve20" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties9 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties9 = new Wps.ShapeProperties();

            A.Transform2D transform2D9 = new A.Transform2D();
            A.Offset offset10 = new A.Offset() { X = 207259L, Y = 94929L };
            A.Extents extents10 = new A.Extents() { Cx = 97515L, Cy = 56302L };

            transform2D9.Append(offset10);
            transform2D9.Append(extents10);

            A.CustomGeometry customGeometry9 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList9 = new A.AdjustValueList();
            A.Rectangle rectangle9 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList9 = new A.PathList();

            A.Path path9 = new A.Path() { Width = 97515L, Height = 56302L };

            A.MoveTo moveTo9 = new A.MoveTo();
            A.Point point17 = new A.Point() { X = "0", Y = "0" };

            moveTo9.Append(point17);

            A.LineTo lineTo9 = new A.LineTo();
            A.Point point18 = new A.Point() { X = "97515", Y = "56302" };

            lineTo9.Append(point18);

            path9.Append(moveTo9);
            path9.Append(lineTo9);

            pathList9.Append(path9);

            customGeometry9.Append(adjustValueList9);
            customGeometry9.Append(rectangle9);
            customGeometry9.Append(pathList9);

            A.Outline outline9 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill9 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex9 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha9 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex9.Append(alpha9);

            solidFill9.Append(rgbColorModelHex9);

            outline9.Append(solidFill9);

            shapeProperties9.Append(transform2D9);
            shapeProperties9.Append(customGeometry9);
            shapeProperties9.Append(outline9);

            Wps.ShapeStyle shapeStyle9 = new Wps.ShapeStyle();
            A.LineReference lineReference9 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference9 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference9 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference9 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle9.Append(lineReference9);
            shapeStyle9.Append(fillReference9);
            shapeStyle9.Append(effectReference9);
            shapeStyle9.Append(fontReference9);
            Wps.TextBodyProperties textBodyProperties9 = new Wps.TextBodyProperties();

            wordprocessingShape9.Append(nonVisualDrawingProperties9);
            wordprocessingShape9.Append(nonVisualDrawingShapeProperties9);
            wordprocessingShape9.Append(shapeProperties9);
            wordprocessingShape9.Append(shapeStyle9);
            wordprocessingShape9.Append(textBodyProperties9);

            Wps.WordprocessingShape wordprocessingShape10 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties10 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)21U, Name = "Curve22" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties10 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties10 = new Wps.ShapeProperties();

            A.Transform2D transform2D10 = new A.Transform2D();
            A.Offset offset11 = new A.Offset() { X = 128185L, Y = 3340L };
            A.Extents extents11 = new A.Extents() { Cx = 80776L, Cy = 90027L };

            transform2D10.Append(offset11);
            transform2D10.Append(extents11);

            A.CustomGeometry customGeometry10 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList10 = new A.AdjustValueList();
            A.Rectangle rectangle10 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList10 = new A.PathList();

            A.Path path10 = new A.Path() { Width = 80776L, Height = 90027L };

            A.MoveTo moveTo10 = new A.MoveTo();
            A.Point point19 = new A.Point() { X = "80776", Y = "90027" };

            moveTo10.Append(point19);

            A.LineTo lineTo10 = new A.LineTo();
            A.Point point20 = new A.Point() { X = "65963", Y = "90027" };

            lineTo10.Append(point20);

            A.LineTo lineTo11 = new A.LineTo();
            A.Point point21 = new A.Point() { X = "23278", Y = "9492" };

            lineTo11.Append(point21);

            A.LineTo lineTo12 = new A.LineTo();
            A.Point point22 = new A.Point() { X = "23278", Y = "90027" };

            lineTo12.Append(point22);

            A.LineTo lineTo13 = new A.LineTo();
            A.Point point23 = new A.Point() { X = "12092", Y = "90027" };

            lineTo13.Append(point23);

            A.LineTo lineTo14 = new A.LineTo();
            A.Point point24 = new A.Point() { X = "12092", Y = "0" };

            lineTo14.Append(point24);

            A.LineTo lineTo15 = new A.LineTo();
            A.Point point25 = new A.Point() { X = "30654", Y = "0" };

            lineTo15.Append(point25);

            A.LineTo lineTo16 = new A.LineTo();
            A.Point point26 = new A.Point() { X = "69591", Y = "73521" };

            lineTo16.Append(point26);

            A.LineTo lineTo17 = new A.LineTo();
            A.Point point27 = new A.Point() { X = "69591", Y = "0" };

            lineTo17.Append(point27);

            A.LineTo lineTo18 = new A.LineTo();
            A.Point point28 = new A.Point() { X = "80776", Y = "0" };

            lineTo18.Append(point28);

            A.LineTo lineTo19 = new A.LineTo();
            A.Point point29 = new A.Point() { X = "80776", Y = "90027" };

            lineTo19.Append(point29);
            A.CloseShapePath closeShapePath1 = new A.CloseShapePath();

            path10.Append(moveTo10);
            path10.Append(lineTo10);
            path10.Append(lineTo11);
            path10.Append(lineTo12);
            path10.Append(lineTo13);
            path10.Append(lineTo14);
            path10.Append(lineTo15);
            path10.Append(lineTo16);
            path10.Append(lineTo17);
            path10.Append(lineTo18);
            path10.Append(lineTo19);
            path10.Append(closeShapePath1);

            pathList10.Append(path10);

            customGeometry10.Append(adjustValueList10);
            customGeometry10.Append(rectangle10);
            customGeometry10.Append(pathList10);

            A.SolidFill solidFill10 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = "0000ff" };
            A.Alpha alpha10 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex10.Append(alpha10);

            solidFill10.Append(rgbColorModelHex10);

            shapeProperties10.Append(transform2D10);
            shapeProperties10.Append(customGeometry10);
            shapeProperties10.Append(solidFill10);

            Wps.ShapeStyle shapeStyle10 = new Wps.ShapeStyle();
            A.LineReference lineReference10 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference10 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference10 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference10 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle10.Append(lineReference10);
            shapeStyle10.Append(fillReference10);
            shapeStyle10.Append(effectReference10);
            shapeStyle10.Append(fontReference10);
            Wps.TextBodyProperties textBodyProperties10 = new Wps.TextBodyProperties();

            wordprocessingShape10.Append(nonVisualDrawingProperties10);
            wordprocessingShape10.Append(nonVisualDrawingShapeProperties10);
            wordprocessingShape10.Append(shapeProperties10);
            wordprocessingShape10.Append(shapeStyle10);
            wordprocessingShape10.Append(textBodyProperties10);

            wordprocessingGroup1.Append(nonVisualGroupDrawingShapeProperties1);
            wordprocessingGroup1.Append(groupShapeProperties1);
            wordprocessingGroup1.Append(wordprocessingShape1);
            wordprocessingGroup1.Append(wordprocessingShape2);
            wordprocessingGroup1.Append(wordprocessingShape3);
            wordprocessingGroup1.Append(wordprocessingShape4);
            wordprocessingGroup1.Append(wordprocessingShape5);
            wordprocessingGroup1.Append(wordprocessingShape6);
            wordprocessingGroup1.Append(wordprocessingShape7);
            wordprocessingGroup1.Append(wordprocessingShape8);
            wordprocessingGroup1.Append(wordprocessingShape9);
            wordprocessingGroup1.Append(wordprocessingShape10);

            graphicData1.Append(wordprocessingGroup1);

            graphic1.Append(graphicData1);

            inline1.Append(extent1);
            inline1.Append(effectExtent1);
            inline1.Append(docProperties1);
            inline1.Append(graphic1);

            drawing1.Append(inline1);

            run1.Append(drawing1);
            return run1;
        }
    }
}