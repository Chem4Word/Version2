using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Wpg = DocumentFormat.OpenXml.Office2010.Word.DrawingGroup;
using Wps = DocumentFormat.OpenXml.Office2010.Word.DrawingShape;

namespace Chem4Word.UI.Molecules
{
    public static class WigglyLines
    {
        // Creates an Run instance and adds its children.
        public static Run GenerateRun()
        {
            Run run1 = new Run();

            var drawing1 = new DocumentFormat.OpenXml.Wordprocessing.Drawing();

            Wp.Inline inline1 = new Wp.Inline() { DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)0U, DistanceFromRight = (UInt32Value)0U };
            Wp.Extent extent1 = new Wp.Extent() { Cx = 1249388L, Cy = 1404544L };
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
            A.Extents extents1 = new A.Extents() { Cx = 1249388L, Cy = 1404544L };
            A.ChildOffset childOffset1 = new A.ChildOffset() { X = 0L, Y = 0L };
            A.ChildExtents childExtents1 = new A.ChildExtents() { Cx = 1249388L, Cy = 1404544L };

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
            A.Offset offset2 = new A.Offset() { X = 9525L, Y = 1023887L };
            A.Extents extents2 = new A.Extents() { Cx = 130061L, Cy = 139192L };

            transform2D1.Append(offset2);
            transform2D1.Append(extents2);

            A.CustomGeometry customGeometry1 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();
            A.Rectangle rectangle1 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList1 = new A.PathList();

            A.Path path1 = new A.Path() { Width = 130061L, Height = 139192L };

            A.MoveTo moveTo1 = new A.MoveTo();
            A.Point point1 = new A.Point() { X = "130061", Y = "0" };

            moveTo1.Append(point1);

            A.LineTo lineTo1 = new A.LineTo();
            A.Point point2 = new A.Point() { X = "0", Y = "139192" };

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
            A.Offset offset3 = new A.Offset() { X = 65049L, Y = 1345311L };
            A.Extents extents3 = new A.Extents() { Cx = 185572L, Cy = 43040L };

            transform2D2.Append(offset3);
            transform2D2.Append(extents3);

            A.CustomGeometry customGeometry2 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList2 = new A.AdjustValueList();
            A.Rectangle rectangle2 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList2 = new A.PathList();

            A.Path path2 = new A.Path() { Width = 185572L, Height = 43040L };

            A.MoveTo moveTo2 = new A.MoveTo();
            A.Point point3 = new A.Point() { X = "0", Y = "0" };

            moveTo2.Append(point3);

            A.LineTo lineTo2 = new A.LineTo();
            A.Point point4 = new A.Point() { X = "185572", Y = "43040" };

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
            A.Offset offset4 = new A.Offset() { X = 325158L, Y = 1066927L };
            A.Extents extents4 = new A.Extents() { Cx = 55524L, Cy = 182232L };

            transform2D3.Append(offset4);
            transform2D3.Append(extents4);

            A.CustomGeometry customGeometry3 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList3 = new A.AdjustValueList();
            A.Rectangle rectangle3 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList3 = new A.PathList();

            A.Path path3 = new A.Path() { Width = 55524L, Height = 182232L };

            A.MoveTo moveTo3 = new A.MoveTo();
            A.Point point5 = new A.Point() { X = "55524", Y = "182232" };

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
            A.Offset offset5 = new A.Offset() { X = 325158L, Y = 991095L };
            A.Extents extents5 = new A.Extents() { Cx = 107432L, Cy = 75832L };

            transform2D4.Append(offset5);
            transform2D4.Append(extents5);

            A.CustomGeometry customGeometry4 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList4 = new A.AdjustValueList();
            A.Rectangle rectangle4 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList4 = new A.PathList();

            A.Path path4 = new A.Path() { Width = 107432L, Height = 75832L };

            A.MoveTo moveTo4 = new A.MoveTo();
            A.Point point7 = new A.Point() { X = "0", Y = "75832" };

            moveTo4.Append(point7);

            A.LineTo lineTo4 = new A.LineTo();
            A.Point point8 = new A.Point() { X = "107432", Y = "0" };

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
            A.Offset offset6 = new A.Offset() { X = 527477L, Y = 992615L };
            A.Extents extents6 = new A.Extents() { Cx = 104907L, Cy = 78808L };

            transform2D5.Append(offset6);
            transform2D5.Append(extents6);

            A.CustomGeometry customGeometry5 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList5 = new A.AdjustValueList();
            A.Rectangle rectangle5 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList5 = new A.PathList();

            A.Path path5 = new A.Path() { Width = 104907L, Height = 78808L };

            A.MoveTo moveTo5 = new A.MoveTo();
            A.Point point9 = new A.Point() { X = "0", Y = "0" };

            moveTo5.Append(point9);

            A.LineTo lineTo5 = new A.LineTo();
            A.Point point10 = new A.Point() { X = "104907", Y = "78808" };

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
            A.Offset offset7 = new A.Offset() { X = 571157L, Y = 1071423L };
            A.Extents extents7 = new A.Extents() { Cx = 61227L, Cy = 180683L };

            transform2D6.Append(offset7);
            transform2D6.Append(extents7);

            A.CustomGeometry customGeometry6 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList6 = new A.AdjustValueList();
            A.Rectangle rectangle6 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList6 = new A.PathList();

            A.Path path6 = new A.Path() { Width = 61227L, Height = 180683L };

            A.MoveTo moveTo6 = new A.MoveTo();
            A.Point point11 = new A.Point() { X = "61227", Y = "0" };

            moveTo6.Append(point11);

            A.LineTo lineTo6 = new A.LineTo();
            A.Point point12 = new A.Point() { X = "0", Y = "180683" };

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
            A.Offset offset8 = new A.Offset() { X = 380683L, Y = 1249159L };
            A.Extents extents8 = new A.Extents() { Cx = 190475L, Cy = 2947L };

            transform2D7.Append(offset8);
            transform2D7.Append(extents8);

            A.CustomGeometry customGeometry7 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList7 = new A.AdjustValueList();
            A.Rectangle rectangle7 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList7 = new A.PathList();

            A.Path path7 = new A.Path() { Width = 190475L, Height = 2947L };

            A.MoveTo moveTo7 = new A.MoveTo();
            A.Point point13 = new A.Point() { X = "190475", Y = "2947" };

            moveTo7.Append(point13);

            A.LineTo lineTo7 = new A.LineTo();
            A.Point point14 = new A.Point() { X = "0", Y = "0" };

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
            A.Offset offset9 = new A.Offset() { X = 819201L, Y = 1034161L };
            A.Extents extents9 = new A.Extents() { Cx = 125679L, Cy = 143167L };

            transform2D8.Append(offset9);
            transform2D8.Append(extents9);

            A.CustomGeometry customGeometry8 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList8 = new A.AdjustValueList();
            A.Rectangle rectangle8 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList8 = new A.PathList();

            A.Path path8 = new A.Path() { Width = 125679L, Height = 143167L };

            A.MoveTo moveTo8 = new A.MoveTo();
            A.Point point15 = new A.Point() { X = "0", Y = "0" };

            moveTo8.Append(point15);

            A.LineTo lineTo8 = new A.LineTo();
            A.Point point16 = new A.Point() { X = "125679", Y = "143167" };

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
            A.Offset offset10 = new A.Offset() { X = 696925L, Y = 1357744L };
            A.Extents extents10 = new A.Extents() { Cx = 186830L, Cy = 37274L };

            transform2D9.Append(offset10);
            transform2D9.Append(extents10);

            A.CustomGeometry customGeometry9 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList9 = new A.AdjustValueList();
            A.Rectangle rectangle9 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList9 = new A.PathList();

            A.Path path9 = new A.Path() { Width = 186830L, Height = 37274L };

            A.MoveTo moveTo9 = new A.MoveTo();
            A.Point point17 = new A.Point() { X = "0", Y = "37274" };

            moveTo9.Append(point17);

            A.LineTo lineTo9 = new A.LineTo();
            A.Point point18 = new A.Point() { X = "186830", Y = "0" };

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
            A.Offset offset11 = new A.Offset() { X = 819201L, Y = 904956L };
            A.Extents extents11 = new A.Extents() { Cx = 43784L, Cy = 129205L };

            transform2D10.Append(offset11);
            transform2D10.Append(extents11);

            A.CustomGeometry customGeometry10 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList10 = new A.AdjustValueList();
            A.Rectangle rectangle10 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList10 = new A.PathList();

            A.Path path10 = new A.Path() { Width = 43784L, Height = 129205L };

            A.MoveTo moveTo10 = new A.MoveTo();
            A.Point point19 = new A.Point() { X = "0", Y = "129205" };

            moveTo10.Append(point19);

            A.LineTo lineTo10 = new A.LineTo();
            A.Point point20 = new A.Point() { X = "43784", Y = "0" };

            lineTo10.Append(point20);

            path10.Append(moveTo10);
            path10.Append(lineTo10);

            pathList10.Append(path10);

            customGeometry10.Append(adjustValueList10);
            customGeometry10.Append(rectangle10);
            customGeometry10.Append(pathList10);

            A.Outline outline10 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill10 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha10 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex10.Append(alpha10);

            solidFill10.Append(rgbColorModelHex10);

            outline10.Append(solidFill10);

            shapeProperties10.Append(transform2D10);
            shapeProperties10.Append(customGeometry10);
            shapeProperties10.Append(outline10);

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

            Wps.WordprocessingShape wordprocessingShape11 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties11 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties11 = new Wps.ShapeProperties();

            A.Transform2D transform2D11 = new A.Transform2D();
            A.Offset offset12 = new A.Offset() { X = 657060L, Y = 672302L };
            A.Extents extents12 = new A.Extents() { Cx = 19050L, Cy = 19050L };

            transform2D11.Append(offset12);
            transform2D11.Append(extents12);

            A.PresetGeometry presetGeometry1 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList11 = new A.AdjustValueList();

            presetGeometry1.Append(adjustValueList11);

            A.SolidFill solidFill11 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha11 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex11.Append(alpha11);

            solidFill11.Append(rgbColorModelHex11);

            shapeProperties11.Append(transform2D11);
            shapeProperties11.Append(presetGeometry1);
            shapeProperties11.Append(solidFill11);

            Wps.ShapeStyle shapeStyle11 = new Wps.ShapeStyle();
            A.LineReference lineReference11 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference11 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference11 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference11 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle11.Append(lineReference11);
            shapeStyle11.Append(fillReference11);
            shapeStyle11.Append(effectReference11);
            shapeStyle11.Append(fontReference11);
            Wps.TextBodyProperties textBodyProperties11 = new Wps.TextBodyProperties();

            wordprocessingShape11.Append(nonVisualDrawingShapeProperties11);
            wordprocessingShape11.Append(shapeProperties11);
            wordprocessingShape11.Append(shapeStyle11);
            wordprocessingShape11.Append(textBodyProperties11);

            Wps.WordprocessingShape wordprocessingShape12 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties12 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties12 = new Wps.ShapeProperties();

            A.Transform2D transform2D12 = new A.Transform2D();
            A.Offset offset13 = new A.Offset() { X = 630390L, Y = 681684L };
            A.Extents extents13 = new A.Extents() { Cx = 19050L, Cy = 19050L };

            transform2D12.Append(offset13);
            transform2D12.Append(extents13);

            A.PresetGeometry presetGeometry2 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList12 = new A.AdjustValueList();

            presetGeometry2.Append(adjustValueList12);

            A.SolidFill solidFill12 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex12 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha12 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex12.Append(alpha12);

            solidFill12.Append(rgbColorModelHex12);

            shapeProperties12.Append(transform2D12);
            shapeProperties12.Append(presetGeometry2);
            shapeProperties12.Append(solidFill12);

            Wps.ShapeStyle shapeStyle12 = new Wps.ShapeStyle();
            A.LineReference lineReference12 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference12 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference12 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference12 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle12.Append(lineReference12);
            shapeStyle12.Append(fillReference12);
            shapeStyle12.Append(effectReference12);
            shapeStyle12.Append(fontReference12);
            Wps.TextBodyProperties textBodyProperties12 = new Wps.TextBodyProperties();

            wordprocessingShape12.Append(nonVisualDrawingShapeProperties12);
            wordprocessingShape12.Append(shapeProperties12);
            wordprocessingShape12.Append(shapeStyle12);
            wordprocessingShape12.Append(textBodyProperties12);

            Wps.WordprocessingShape wordprocessingShape13 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties13 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties13 = new Wps.ShapeProperties();

            A.Transform2D transform2D13 = new A.Transform2D();
            A.Offset offset14 = new A.Offset() { X = 603719L, Y = 691067L };
            A.Extents extents14 = new A.Extents() { Cx = 19050L, Cy = 19050L };

            transform2D13.Append(offset14);
            transform2D13.Append(extents14);

            A.PresetGeometry presetGeometry3 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList13 = new A.AdjustValueList();

            presetGeometry3.Append(adjustValueList13);

            A.SolidFill solidFill13 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex13 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha13 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex13.Append(alpha13);

            solidFill13.Append(rgbColorModelHex13);

            shapeProperties13.Append(transform2D13);
            shapeProperties13.Append(presetGeometry3);
            shapeProperties13.Append(solidFill13);

            Wps.ShapeStyle shapeStyle13 = new Wps.ShapeStyle();
            A.LineReference lineReference13 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference13 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference13 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference13 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle13.Append(lineReference13);
            shapeStyle13.Append(fillReference13);
            shapeStyle13.Append(effectReference13);
            shapeStyle13.Append(fontReference13);
            Wps.TextBodyProperties textBodyProperties13 = new Wps.TextBodyProperties();

            wordprocessingShape13.Append(nonVisualDrawingShapeProperties13);
            wordprocessingShape13.Append(shapeProperties13);
            wordprocessingShape13.Append(shapeStyle13);
            wordprocessingShape13.Append(textBodyProperties13);

            Wps.WordprocessingShape wordprocessingShape14 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties14 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties14 = new Wps.ShapeProperties();

            A.Transform2D transform2D14 = new A.Transform2D();
            A.Offset offset15 = new A.Offset() { X = 577049L, Y = 700450L };
            A.Extents extents15 = new A.Extents() { Cx = 19050L, Cy = 19050L };

            transform2D14.Append(offset15);
            transform2D14.Append(extents15);

            A.PresetGeometry presetGeometry4 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList14 = new A.AdjustValueList();

            presetGeometry4.Append(adjustValueList14);

            A.SolidFill solidFill14 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex14 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha14 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex14.Append(alpha14);

            solidFill14.Append(rgbColorModelHex14);

            shapeProperties14.Append(transform2D14);
            shapeProperties14.Append(presetGeometry4);
            shapeProperties14.Append(solidFill14);

            Wps.ShapeStyle shapeStyle14 = new Wps.ShapeStyle();
            A.LineReference lineReference14 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference14 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference14 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference14 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle14.Append(lineReference14);
            shapeStyle14.Append(fillReference14);
            shapeStyle14.Append(effectReference14);
            shapeStyle14.Append(fontReference14);
            Wps.TextBodyProperties textBodyProperties14 = new Wps.TextBodyProperties();

            wordprocessingShape14.Append(nonVisualDrawingShapeProperties14);
            wordprocessingShape14.Append(shapeProperties14);
            wordprocessingShape14.Append(shapeStyle14);
            wordprocessingShape14.Append(textBodyProperties14);

            Wps.WordprocessingShape wordprocessingShape15 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties15 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties15 = new Wps.ShapeProperties();

            A.Transform2D transform2D15 = new A.Transform2D();
            A.Offset offset16 = new A.Offset() { X = 550379L, Y = 709832L };
            A.Extents extents16 = new A.Extents() { Cx = 19050L, Cy = 19050L };

            transform2D15.Append(offset16);
            transform2D15.Append(extents16);

            A.PresetGeometry presetGeometry5 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList15 = new A.AdjustValueList();

            presetGeometry5.Append(adjustValueList15);

            A.SolidFill solidFill15 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex15 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha15 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex15.Append(alpha15);

            solidFill15.Append(rgbColorModelHex15);

            shapeProperties15.Append(transform2D15);
            shapeProperties15.Append(presetGeometry5);
            shapeProperties15.Append(solidFill15);

            Wps.ShapeStyle shapeStyle15 = new Wps.ShapeStyle();
            A.LineReference lineReference15 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference15 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference15 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference15 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle15.Append(lineReference15);
            shapeStyle15.Append(fillReference15);
            shapeStyle15.Append(effectReference15);
            shapeStyle15.Append(fontReference15);
            Wps.TextBodyProperties textBodyProperties15 = new Wps.TextBodyProperties();

            wordprocessingShape15.Append(nonVisualDrawingShapeProperties15);
            wordprocessingShape15.Append(shapeProperties15);
            wordprocessingShape15.Append(shapeStyle15);
            wordprocessingShape15.Append(textBodyProperties15);

            Wps.WordprocessingShape wordprocessingShape16 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties16 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties16 = new Wps.ShapeProperties();

            A.Transform2D transform2D16 = new A.Transform2D();
            A.Offset offset17 = new A.Offset() { X = 470412L, Y = 875015L };
            A.Extents extents17 = new A.Extents() { Cx = 19050L, Cy = 19050L };

            transform2D16.Append(offset17);
            transform2D16.Append(extents17);

            A.PresetGeometry presetGeometry6 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList16 = new A.AdjustValueList();

            presetGeometry6.Append(adjustValueList16);

            A.SolidFill solidFill16 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex16 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha16 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex16.Append(alpha16);

            solidFill16.Append(rgbColorModelHex16);

            shapeProperties16.Append(transform2D16);
            shapeProperties16.Append(presetGeometry6);
            shapeProperties16.Append(solidFill16);

            Wps.ShapeStyle shapeStyle16 = new Wps.ShapeStyle();
            A.LineReference lineReference16 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference16 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference16 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference16 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle16.Append(lineReference16);
            shapeStyle16.Append(fillReference16);
            shapeStyle16.Append(effectReference16);
            shapeStyle16.Append(fontReference16);
            Wps.TextBodyProperties textBodyProperties16 = new Wps.TextBodyProperties();

            wordprocessingShape16.Append(nonVisualDrawingShapeProperties16);
            wordprocessingShape16.Append(shapeProperties16);
            wordprocessingShape16.Append(shapeStyle16);
            wordprocessingShape16.Append(textBodyProperties16);

            Wps.WordprocessingShape wordprocessingShape17 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties17 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties17 = new Wps.ShapeProperties();

            A.Transform2D transform2D17 = new A.Transform2D();
            A.Offset offset18 = new A.Offset() { X = 470273L, Y = 855917L };
            A.Extents extents18 = new A.Extents() { Cx = 19050L, Cy = 19050L };

            transform2D17.Append(offset18);
            transform2D17.Append(extents18);

            A.PresetGeometry presetGeometry7 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList17 = new A.AdjustValueList();

            presetGeometry7.Append(adjustValueList17);

            A.SolidFill solidFill17 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex17 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha17 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex17.Append(alpha17);

            solidFill17.Append(rgbColorModelHex17);

            shapeProperties17.Append(transform2D17);
            shapeProperties17.Append(presetGeometry7);
            shapeProperties17.Append(solidFill17);

            Wps.ShapeStyle shapeStyle17 = new Wps.ShapeStyle();
            A.LineReference lineReference17 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference17 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference17 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference17 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle17.Append(lineReference17);
            shapeStyle17.Append(fillReference17);
            shapeStyle17.Append(effectReference17);
            shapeStyle17.Append(fontReference17);
            Wps.TextBodyProperties textBodyProperties17 = new Wps.TextBodyProperties();

            wordprocessingShape17.Append(nonVisualDrawingShapeProperties17);
            wordprocessingShape17.Append(shapeProperties17);
            wordprocessingShape17.Append(shapeStyle17);
            wordprocessingShape17.Append(textBodyProperties17);

            Wps.WordprocessingShape wordprocessingShape18 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties18 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties18 = new Wps.ShapeProperties();

            A.Transform2D transform2D18 = new A.Transform2D();
            A.Offset offset19 = new A.Offset() { X = 470134L, Y = 836820L };
            A.Extents extents19 = new A.Extents() { Cx = 19050L, Cy = 19050L };

            transform2D18.Append(offset19);
            transform2D18.Append(extents19);

            A.PresetGeometry presetGeometry8 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList18 = new A.AdjustValueList();

            presetGeometry8.Append(adjustValueList18);

            A.SolidFill solidFill18 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex18 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha18 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex18.Append(alpha18);

            solidFill18.Append(rgbColorModelHex18);

            shapeProperties18.Append(transform2D18);
            shapeProperties18.Append(presetGeometry8);
            shapeProperties18.Append(solidFill18);

            Wps.ShapeStyle shapeStyle18 = new Wps.ShapeStyle();
            A.LineReference lineReference18 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference18 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference18 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference18 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle18.Append(lineReference18);
            shapeStyle18.Append(fillReference18);
            shapeStyle18.Append(effectReference18);
            shapeStyle18.Append(fontReference18);
            Wps.TextBodyProperties textBodyProperties18 = new Wps.TextBodyProperties();

            wordprocessingShape18.Append(nonVisualDrawingShapeProperties18);
            wordprocessingShape18.Append(shapeProperties18);
            wordprocessingShape18.Append(shapeStyle18);
            wordprocessingShape18.Append(textBodyProperties18);

            Wps.WordprocessingShape wordprocessingShape19 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties19 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties19 = new Wps.ShapeProperties();

            A.Transform2D transform2D19 = new A.Transform2D();
            A.Offset offset20 = new A.Offset() { X = 469995L, Y = 817722L };
            A.Extents extents20 = new A.Extents() { Cx = 19050L, Cy = 19050L };

            transform2D19.Append(offset20);
            transform2D19.Append(extents20);

            A.PresetGeometry presetGeometry9 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Ellipse };
            A.AdjustValueList adjustValueList19 = new A.AdjustValueList();

            presetGeometry9.Append(adjustValueList19);

            A.SolidFill solidFill19 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex19 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha19 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex19.Append(alpha19);

            solidFill19.Append(rgbColorModelHex19);

            shapeProperties19.Append(transform2D19);
            shapeProperties19.Append(presetGeometry9);
            shapeProperties19.Append(solidFill19);

            Wps.ShapeStyle shapeStyle19 = new Wps.ShapeStyle();
            A.LineReference lineReference19 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference19 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference19 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference19 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle19.Append(lineReference19);
            shapeStyle19.Append(fillReference19);
            shapeStyle19.Append(effectReference19);
            shapeStyle19.Append(fontReference19);
            Wps.TextBodyProperties textBodyProperties19 = new Wps.TextBodyProperties();

            wordprocessingShape19.Append(nonVisualDrawingShapeProperties19);
            wordprocessingShape19.Append(shapeProperties19);
            wordprocessingShape19.Append(shapeStyle19);
            wordprocessingShape19.Append(textBodyProperties19);

            Wps.WordprocessingShape wordprocessingShape20 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties11 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)23U, Name = "Curve24" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties20 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties20 = new Wps.ShapeProperties();

            A.Transform2D transform2D20 = new A.Transform2D();
            A.Offset offset21 = new A.Offset() { X = 749415L, Y = 481990L };
            A.Extents extents21 = new A.Extents() { Cx = 47916L, Cy = 125146L };

            transform2D20.Append(offset21);
            transform2D20.Append(extents21);

            A.CustomGeometry customGeometry11 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList20 = new A.AdjustValueList();
            A.Rectangle rectangle11 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList11 = new A.PathList();

            A.Path path11 = new A.Path() { Width = 47916L, Height = 125146L };

            A.MoveTo moveTo11 = new A.MoveTo();
            A.Point point21 = new A.Point() { X = "0", Y = "125146" };

            moveTo11.Append(point21);

            A.LineTo lineTo11 = new A.LineTo();
            A.Point point22 = new A.Point() { X = "47916", Y = "0" };

            lineTo11.Append(point22);

            path11.Append(moveTo11);
            path11.Append(lineTo11);

            pathList11.Append(path11);

            customGeometry11.Append(adjustValueList20);
            customGeometry11.Append(rectangle11);
            customGeometry11.Append(pathList11);

            A.Outline outline11 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill20 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex20 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha20 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex20.Append(alpha20);

            solidFill20.Append(rgbColorModelHex20);

            outline11.Append(solidFill20);

            shapeProperties20.Append(transform2D20);
            shapeProperties20.Append(customGeometry11);
            shapeProperties20.Append(outline11);

            Wps.ShapeStyle shapeStyle20 = new Wps.ShapeStyle();
            A.LineReference lineReference20 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference20 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference20 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference20 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle20.Append(lineReference20);
            shapeStyle20.Append(fillReference20);
            shapeStyle20.Append(effectReference20);
            shapeStyle20.Append(fontReference20);
            Wps.TextBodyProperties textBodyProperties20 = new Wps.TextBodyProperties();

            wordprocessingShape20.Append(nonVisualDrawingProperties11);
            wordprocessingShape20.Append(nonVisualDrawingShapeProperties20);
            wordprocessingShape20.Append(shapeProperties20);
            wordprocessingShape20.Append(shapeStyle20);
            wordprocessingShape20.Append(textBodyProperties20);

            Wps.WordprocessingShape wordprocessingShape21 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties12 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)25U, Name = "Curve26" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties21 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties21 = new Wps.ShapeProperties();

            A.Transform2D transform2D21 = new A.Transform2D();
            A.Offset offset22 = new A.Offset() { X = 797331L, Y = 450583L };
            A.Extents extents22 = new A.Extents() { Cx = 187884L, Cy = 31407L };

            transform2D21.Append(offset22);
            transform2D21.Append(extents22);

            A.CustomGeometry customGeometry12 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList21 = new A.AdjustValueList();
            A.Rectangle rectangle12 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList12 = new A.PathList();

            A.Path path12 = new A.Path() { Width = 187884L, Height = 31407L };

            A.MoveTo moveTo12 = new A.MoveTo();
            A.Point point23 = new A.Point() { X = "0", Y = "31407" };

            moveTo12.Append(point23);

            A.LineTo lineTo12 = new A.LineTo();
            A.Point point24 = new A.Point() { X = "187884", Y = "0" };

            lineTo12.Append(point24);

            path12.Append(moveTo12);
            path12.Append(lineTo12);

            pathList12.Append(path12);

            customGeometry12.Append(adjustValueList21);
            customGeometry12.Append(rectangle12);
            customGeometry12.Append(pathList12);

            A.Outline outline12 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill21 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex21 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha21 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex21.Append(alpha21);

            solidFill21.Append(rgbColorModelHex21);

            outline12.Append(solidFill21);

            shapeProperties21.Append(transform2D21);
            shapeProperties21.Append(customGeometry12);
            shapeProperties21.Append(outline12);

            Wps.ShapeStyle shapeStyle21 = new Wps.ShapeStyle();
            A.LineReference lineReference21 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference21 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference21 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference21 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle21.Append(lineReference21);
            shapeStyle21.Append(fillReference21);
            shapeStyle21.Append(effectReference21);
            shapeStyle21.Append(fontReference21);
            Wps.TextBodyProperties textBodyProperties21 = new Wps.TextBodyProperties();

            wordprocessingShape21.Append(nonVisualDrawingProperties12);
            wordprocessingShape21.Append(nonVisualDrawingShapeProperties21);
            wordprocessingShape21.Append(shapeProperties21);
            wordprocessingShape21.Append(shapeStyle21);
            wordprocessingShape21.Append(textBodyProperties21);

            Wps.WordprocessingShape wordprocessingShape22 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties13 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)27U, Name = "Curve28" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties22 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties22 = new Wps.ShapeProperties();

            A.Transform2D transform2D22 = new A.Transform2D();
            A.Offset offset23 = new A.Offset() { X = 930834L, Y = 125146L };
            A.Extents extents23 = new A.Extents() { Cx = 121133L, Cy = 147015L };

            transform2D22.Append(offset23);
            transform2D22.Append(extents23);

            A.CustomGeometry customGeometry13 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList22 = new A.AdjustValueList();
            A.Rectangle rectangle13 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList13 = new A.PathList();

            A.Path path13 = new A.Path() { Width = 121133L, Height = 147015L };

            A.MoveTo moveTo13 = new A.MoveTo();
            A.Point point25 = new A.Point() { X = "121133", Y = "147015" };

            moveTo13.Append(point25);

            A.LineTo lineTo13 = new A.LineTo();
            A.Point point26 = new A.Point() { X = "0", Y = "0" };

            lineTo13.Append(point26);

            path13.Append(moveTo13);
            path13.Append(lineTo13);

            pathList13.Append(path13);

            customGeometry13.Append(adjustValueList22);
            customGeometry13.Append(rectangle13);
            customGeometry13.Append(pathList13);

            A.Outline outline13 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill22 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex22 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha22 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex22.Append(alpha22);

            solidFill22.Append(rgbColorModelHex22);

            outline13.Append(solidFill22);

            shapeProperties22.Append(transform2D22);
            shapeProperties22.Append(customGeometry13);
            shapeProperties22.Append(outline13);

            Wps.ShapeStyle shapeStyle22 = new Wps.ShapeStyle();
            A.LineReference lineReference22 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference22 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference22 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference22 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle22.Append(lineReference22);
            shapeStyle22.Append(fillReference22);
            shapeStyle22.Append(effectReference22);
            shapeStyle22.Append(fontReference22);
            Wps.TextBodyProperties textBodyProperties22 = new Wps.TextBodyProperties();

            wordprocessingShape22.Append(nonVisualDrawingProperties13);
            wordprocessingShape22.Append(nonVisualDrawingShapeProperties22);
            wordprocessingShape22.Append(shapeProperties22);
            wordprocessingShape22.Append(shapeStyle22);
            wordprocessingShape22.Append(textBodyProperties22);

            Wps.WordprocessingShape wordprocessingShape23 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties14 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)29U, Name = "Curve30" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties23 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties23 = new Wps.ShapeProperties();

            A.Transform2D transform2D23 = new A.Transform2D();
            A.Offset offset24 = new A.Offset() { X = 676199L, Y = 156553L };
            A.Extents extents24 = new A.Extents() { Cx = 66751L, Cy = 178422L };

            transform2D23.Append(offset24);
            transform2D23.Append(extents24);

            A.CustomGeometry customGeometry14 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList23 = new A.AdjustValueList();
            A.Rectangle rectangle14 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList14 = new A.PathList();

            A.Path path14 = new A.Path() { Width = 66751L, Height = 178422L };

            A.MoveTo moveTo14 = new A.MoveTo();
            A.Point point27 = new A.Point() { X = "66751", Y = "0" };

            moveTo14.Append(point27);

            A.LineTo lineTo14 = new A.LineTo();
            A.Point point28 = new A.Point() { X = "0", Y = "178422" };

            lineTo14.Append(point28);

            path14.Append(moveTo14);
            path14.Append(lineTo14);

            pathList14.Append(path14);

            customGeometry14.Append(adjustValueList23);
            customGeometry14.Append(rectangle14);
            customGeometry14.Append(pathList14);

            A.Outline outline14 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill23 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex23 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha23 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex23.Append(alpha23);

            solidFill23.Append(rgbColorModelHex23);

            outline14.Append(solidFill23);

            shapeProperties23.Append(transform2D23);
            shapeProperties23.Append(customGeometry14);
            shapeProperties23.Append(outline14);

            Wps.ShapeStyle shapeStyle23 = new Wps.ShapeStyle();
            A.LineReference lineReference23 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference23 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference23 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference23 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle23.Append(lineReference23);
            shapeStyle23.Append(fillReference23);
            shapeStyle23.Append(effectReference23);
            shapeStyle23.Append(fontReference23);
            Wps.TextBodyProperties textBodyProperties23 = new Wps.TextBodyProperties();

            wordprocessingShape23.Append(nonVisualDrawingProperties14);
            wordprocessingShape23.Append(nonVisualDrawingShapeProperties23);
            wordprocessingShape23.Append(shapeProperties23);
            wordprocessingShape23.Append(shapeStyle23);
            wordprocessingShape23.Append(textBodyProperties23);

            Wps.WordprocessingShape wordprocessingShape24 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties15 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)31U, Name = "Curve32" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties24 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties24 = new Wps.ShapeProperties();

            A.Transform2D transform2D24 = new A.Transform2D();
            A.Offset offset25 = new A.Offset() { X = 621805L, Y = 9525L };
            A.Extents extents25 = new A.Extents() { Cx = 121145L, Cy = 147028L };

            transform2D24.Append(offset25);
            transform2D24.Append(extents25);

            A.CustomGeometry customGeometry15 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList24 = new A.AdjustValueList();
            A.Rectangle rectangle15 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList15 = new A.PathList();

            A.Path path15 = new A.Path() { Width = 121145L, Height = 147028L };

            A.MoveTo moveTo15 = new A.MoveTo();
            A.Point point29 = new A.Point() { X = "121145", Y = "147028" };

            moveTo15.Append(point29);

            A.LineTo lineTo15 = new A.LineTo();
            A.Point point30 = new A.Point() { X = "0", Y = "0" };

            lineTo15.Append(point30);

            path15.Append(moveTo15);
            path15.Append(lineTo15);

            pathList15.Append(path15);

            customGeometry15.Append(adjustValueList24);
            customGeometry15.Append(rectangle15);
            customGeometry15.Append(pathList15);

            A.Outline outline15 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill24 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex24 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha24 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex24.Append(alpha24);

            solidFill24.Append(rgbColorModelHex24);

            outline15.Append(solidFill24);

            shapeProperties24.Append(transform2D24);
            shapeProperties24.Append(customGeometry15);
            shapeProperties24.Append(outline15);

            Wps.ShapeStyle shapeStyle24 = new Wps.ShapeStyle();
            A.LineReference lineReference24 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference24 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference24 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference24 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle24.Append(lineReference24);
            shapeStyle24.Append(fillReference24);
            shapeStyle24.Append(effectReference24);
            shapeStyle24.Append(fontReference24);
            Wps.TextBodyProperties textBodyProperties24 = new Wps.TextBodyProperties();

            wordprocessingShape24.Append(nonVisualDrawingProperties15);
            wordprocessingShape24.Append(nonVisualDrawingShapeProperties24);
            wordprocessingShape24.Append(shapeProperties24);
            wordprocessingShape24.Append(shapeStyle24);
            wordprocessingShape24.Append(textBodyProperties24);

            Wps.WordprocessingShape wordprocessingShape25 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties16 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)33U, Name = "Curve34" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties25 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties25 = new Wps.ShapeProperties();

            A.Transform2D transform2D25 = new A.Transform2D();
            A.Offset offset26 = new A.Offset() { X = 1051966L, Y = 240767L };
            A.Extents extents26 = new A.Extents() { Cx = 187897L, Cy = 31394L };

            transform2D25.Append(offset26);
            transform2D25.Append(extents26);

            A.CustomGeometry customGeometry16 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList25 = new A.AdjustValueList();
            A.Rectangle rectangle16 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList16 = new A.PathList();

            A.Path path16 = new A.Path() { Width = 187897L, Height = 31394L };

            A.MoveTo moveTo16 = new A.MoveTo();
            A.Point point31 = new A.Point() { X = "0", Y = "31394" };

            moveTo16.Append(point31);

            A.LineTo lineTo16 = new A.LineTo();
            A.Point point32 = new A.Point() { X = "187897", Y = "0" };

            lineTo16.Append(point32);

            path16.Append(moveTo16);
            path16.Append(lineTo16);

            pathList16.Append(path16);

            customGeometry16.Append(adjustValueList25);
            customGeometry16.Append(rectangle16);
            customGeometry16.Append(pathList16);

            A.Outline outline16 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill25 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex25 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha25 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex25.Append(alpha25);

            solidFill25.Append(rgbColorModelHex25);

            outline16.Append(solidFill25);

            shapeProperties25.Append(transform2D25);
            shapeProperties25.Append(customGeometry16);
            shapeProperties25.Append(outline16);

            Wps.ShapeStyle shapeStyle25 = new Wps.ShapeStyle();
            A.LineReference lineReference25 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference25 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference25 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference25 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle25.Append(lineReference25);
            shapeStyle25.Append(fillReference25);
            shapeStyle25.Append(effectReference25);
            shapeStyle25.Append(fontReference25);
            Wps.TextBodyProperties textBodyProperties25 = new Wps.TextBodyProperties();

            wordprocessingShape25.Append(nonVisualDrawingProperties16);
            wordprocessingShape25.Append(nonVisualDrawingShapeProperties25);
            wordprocessingShape25.Append(shapeProperties25);
            wordprocessingShape25.Append(shapeStyle25);
            wordprocessingShape25.Append(textBodyProperties25);

            Wps.WordprocessingShape wordprocessingShape26 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties17 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)35U, Name = "Curve36" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties26 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties26 = new Wps.ShapeProperties();

            A.Transform2D transform2D26 = new A.Transform2D();
            A.Offset offset27 = new A.Offset() { X = 913946L, Y = 718018L };
            A.Extents extents27 = new A.Extents() { Cx = 109560L, Cy = 100562L };

            transform2D26.Append(offset27);
            transform2D26.Append(extents27);

            A.CustomGeometry customGeometry17 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList26 = new A.AdjustValueList();
            A.Rectangle rectangle17 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList17 = new A.PathList();

            A.Path path17 = new A.Path() { Width = 109560L, Height = 100562L };

            A.MoveTo moveTo17 = new A.MoveTo();
            A.Point point33 = new A.Point() { X = "6446", Y = "100562" };

            moveTo17.Append(point33);

            A.CubicBezierCurveTo cubicBezierCurveTo1 = new A.CubicBezierCurveTo();
            A.Point point34 = new A.Point() { X = "6446", Y = "100562" };
            A.Point point35 = new A.Point() { X = "0", Y = "72422" };
            A.Point point36 = new A.Point() { X = "27069", Y = "82457" };

            cubicBezierCurveTo1.Append(point34);
            cubicBezierCurveTo1.Append(point35);
            cubicBezierCurveTo1.Append(point36);

            A.CubicBezierCurveTo cubicBezierCurveTo2 = new A.CubicBezierCurveTo();
            A.Point point37 = new A.Point() { X = "27069", Y = "82457" };
            A.Point point38 = new A.Point() { X = "54138", Y = "92492" };
            A.Point point39 = new A.Point() { X = "47692", Y = "64351" };

            cubicBezierCurveTo2.Append(point37);
            cubicBezierCurveTo2.Append(point38);
            cubicBezierCurveTo2.Append(point39);

            A.CubicBezierCurveTo cubicBezierCurveTo3 = new A.CubicBezierCurveTo();
            A.Point point40 = new A.Point() { X = "47692", Y = "64351" };
            A.Point point41 = new A.Point() { X = "41245", Y = "36211" };
            A.Point point42 = new A.Point() { X = "68315", Y = "46246" };

            cubicBezierCurveTo3.Append(point40);
            cubicBezierCurveTo3.Append(point41);
            cubicBezierCurveTo3.Append(point42);

            A.CubicBezierCurveTo cubicBezierCurveTo4 = new A.CubicBezierCurveTo();
            A.Point point43 = new A.Point() { X = "68315", Y = "46246" };
            A.Point point44 = new A.Point() { X = "95384", Y = "56281" };
            A.Point point45 = new A.Point() { X = "88937", Y = "28140" };

            cubicBezierCurveTo4.Append(point43);
            cubicBezierCurveTo4.Append(point44);
            cubicBezierCurveTo4.Append(point45);

            A.CubicBezierCurveTo cubicBezierCurveTo5 = new A.CubicBezierCurveTo();
            A.Point point46 = new A.Point() { X = "88937", Y = "28140" };
            A.Point point47 = new A.Point() { X = "82491", Y = "0" };
            A.Point point48 = new A.Point() { X = "109560", Y = "10035" };

            cubicBezierCurveTo5.Append(point46);
            cubicBezierCurveTo5.Append(point47);
            cubicBezierCurveTo5.Append(point48);

            path17.Append(moveTo17);
            path17.Append(cubicBezierCurveTo1);
            path17.Append(cubicBezierCurveTo2);
            path17.Append(cubicBezierCurveTo3);
            path17.Append(cubicBezierCurveTo4);
            path17.Append(cubicBezierCurveTo5);

            pathList17.Append(path17);

            customGeometry17.Append(adjustValueList26);
            customGeometry17.Append(rectangle17);
            customGeometry17.Append(pathList17);

            A.Outline outline17 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill26 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex26 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha26 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex26.Append(alpha26);

            solidFill26.Append(rgbColorModelHex26);

            outline17.Append(solidFill26);

            shapeProperties26.Append(transform2D26);
            shapeProperties26.Append(customGeometry17);
            shapeProperties26.Append(outline17);

            Wps.ShapeStyle shapeStyle26 = new Wps.ShapeStyle();
            A.LineReference lineReference26 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference26 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference26 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference26 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle26.Append(lineReference26);
            shapeStyle26.Append(fillReference26);
            shapeStyle26.Append(effectReference26);
            shapeStyle26.Append(fontReference26);
            Wps.TextBodyProperties textBodyProperties26 = new Wps.TextBodyProperties();

            wordprocessingShape26.Append(nonVisualDrawingProperties17);
            wordprocessingShape26.Append(nonVisualDrawingShapeProperties26);
            wordprocessingShape26.Append(shapeProperties26);
            wordprocessingShape26.Append(shapeStyle26);
            wordprocessingShape26.Append(textBodyProperties26);

            Wps.WordprocessingShape wordprocessingShape27 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties18 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)37U, Name = "Curve38" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties27 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties27 = new Wps.ShapeProperties();

            A.Transform2D transform2D27 = new A.Transform2D();
            A.Offset offset28 = new A.Offset() { X = 916111L, Y = 845662L };
            A.Extents extents28 = new A.Extents() { Cx = 68206L, Cy = 55817L };

            transform2D27.Append(offset28);
            transform2D27.Append(extents28);

            A.CustomGeometry customGeometry18 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList27 = new A.AdjustValueList();
            A.Rectangle rectangle18 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList18 = new A.PathList();

            A.Path path18 = new A.Path() { Width = 68206L, Height = 55817L };

            A.MoveTo moveTo18 = new A.MoveTo();
            A.Point point49 = new A.Point() { X = "0", Y = "20204" };

            moveTo18.Append(point49);

            A.CubicBezierCurveTo cubicBezierCurveTo6 = new A.CubicBezierCurveTo();
            A.Point point50 = new A.Point() { X = "0", Y = "20204" };
            A.Point point51 = new A.Point() { X = "19519", Y = "0" };
            A.Point point52 = new A.Point() { X = "22735", Y = "27908" };

            cubicBezierCurveTo6.Append(point50);
            cubicBezierCurveTo6.Append(point51);
            cubicBezierCurveTo6.Append(point52);

            A.CubicBezierCurveTo cubicBezierCurveTo7 = new A.CubicBezierCurveTo();
            A.Point point53 = new A.Point() { X = "22735", Y = "27908" };
            A.Point point54 = new A.Point() { X = "25952", Y = "55817" };
            A.Point point55 = new A.Point() { X = "45471", Y = "35612" };

            cubicBezierCurveTo7.Append(point53);
            cubicBezierCurveTo7.Append(point54);
            cubicBezierCurveTo7.Append(point55);

            A.CubicBezierCurveTo cubicBezierCurveTo8 = new A.CubicBezierCurveTo();
            A.Point point56 = new A.Point() { X = "45471", Y = "35612" };
            A.Point point57 = new A.Point() { X = "64990", Y = "15408" };
            A.Point point58 = new A.Point() { X = "68206", Y = "43316" };

            cubicBezierCurveTo8.Append(point56);
            cubicBezierCurveTo8.Append(point57);
            cubicBezierCurveTo8.Append(point58);

            path18.Append(moveTo18);
            path18.Append(cubicBezierCurveTo6);
            path18.Append(cubicBezierCurveTo7);
            path18.Append(cubicBezierCurveTo8);

            pathList18.Append(path18);

            customGeometry18.Append(adjustValueList27);
            customGeometry18.Append(rectangle18);
            customGeometry18.Append(pathList18);

            A.Outline outline18 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill27 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex27 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha27 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex27.Append(alpha27);

            solidFill27.Append(rgbColorModelHex27);

            outline18.Append(solidFill27);

            shapeProperties27.Append(transform2D27);
            shapeProperties27.Append(customGeometry18);
            shapeProperties27.Append(outline18);

            Wps.ShapeStyle shapeStyle27 = new Wps.ShapeStyle();
            A.LineReference lineReference27 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference27 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference27 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference27 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle27.Append(lineReference27);
            shapeStyle27.Append(fillReference27);
            shapeStyle27.Append(effectReference27);
            shapeStyle27.Append(fontReference27);
            Wps.TextBodyProperties textBodyProperties27 = new Wps.TextBodyProperties();

            wordprocessingShape27.Append(nonVisualDrawingProperties18);
            wordprocessingShape27.Append(nonVisualDrawingShapeProperties27);
            wordprocessingShape27.Append(shapeProperties27);
            wordprocessingShape27.Append(shapeStyle27);
            wordprocessingShape27.Append(textBodyProperties27);

            Wps.WordprocessingShape wordprocessingShape28 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties19 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)39U, Name = "Curve40" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties28 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties28 = new Wps.ShapeProperties();

            A.Transform2D transform2D28 = new A.Transform2D();
            A.Offset offset29 = new A.Offset() { X = 353767L, Y = 639247L };
            A.Extents extents29 = new A.Extents() { Cx = 68573L, Cy = 59489L };

            transform2D28.Append(offset29);
            transform2D28.Append(extents29);

            A.CustomGeometry customGeometry19 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList28 = new A.AdjustValueList();
            A.Rectangle rectangle19 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList19 = new A.PathList();

            A.Path path19 = new A.Path() { Width = 68573L, Height = 59489L };

            A.MoveTo moveTo19 = new A.MoveTo();
            A.Point point59 = new A.Point() { X = "68573", Y = "59489" };

            moveTo19.Append(point59);

            A.LineTo lineTo17 = new A.LineTo();
            A.Point point60 = new A.Point() { X = "0", Y = "0" };

            lineTo17.Append(point60);

            path19.Append(moveTo19);
            path19.Append(lineTo17);

            pathList19.Append(path19);

            customGeometry19.Append(adjustValueList28);
            customGeometry19.Append(rectangle19);
            customGeometry19.Append(pathList19);

            A.Outline outline19 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill28 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex28 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha28 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex28.Append(alpha28);

            solidFill28.Append(rgbColorModelHex28);

            outline19.Append(solidFill28);

            shapeProperties28.Append(transform2D28);
            shapeProperties28.Append(customGeometry19);
            shapeProperties28.Append(outline19);

            Wps.ShapeStyle shapeStyle28 = new Wps.ShapeStyle();
            A.LineReference lineReference28 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference28 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference28 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference28 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle28.Append(lineReference28);
            shapeStyle28.Append(fillReference28);
            shapeStyle28.Append(effectReference28);
            shapeStyle28.Append(fontReference28);
            Wps.TextBodyProperties textBodyProperties28 = new Wps.TextBodyProperties();

            wordprocessingShape28.Append(nonVisualDrawingProperties19);
            wordprocessingShape28.Append(nonVisualDrawingShapeProperties28);
            wordprocessingShape28.Append(shapeProperties28);
            wordprocessingShape28.Append(shapeStyle28);
            wordprocessingShape28.Append(textBodyProperties28);

            Wps.WordprocessingShape wordprocessingShape29 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties20 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)41U, Name = "Curve42" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties29 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties29 = new Wps.ShapeProperties();

            A.Transform2D transform2D29 = new A.Transform2D();
            A.Offset offset30 = new A.Offset() { X = 985215L, Y = 272161L };
            A.Extents extents30 = new A.Extents() { Cx = 66751L, Cy = 178422L };

            transform2D29.Append(offset30);
            transform2D29.Append(extents30);

            A.CustomGeometry customGeometry20 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList29 = new A.AdjustValueList();
            A.Rectangle rectangle20 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList20 = new A.PathList();

            A.Path path20 = new A.Path() { Width = 66751L, Height = 178422L };

            A.MoveTo moveTo20 = new A.MoveTo();
            A.Point point61 = new A.Point() { X = "0", Y = "178422" };

            moveTo20.Append(point61);

            A.LineTo lineTo18 = new A.LineTo();
            A.Point point62 = new A.Point() { X = "66751", Y = "0" };

            lineTo18.Append(point62);

            path20.Append(moveTo20);
            path20.Append(lineTo18);

            pathList20.Append(path20);

            customGeometry20.Append(adjustValueList29);
            customGeometry20.Append(rectangle20);
            customGeometry20.Append(pathList20);

            A.Outline outline20 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill29 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex29 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha29 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex29.Append(alpha29);

            solidFill29.Append(rgbColorModelHex29);

            outline20.Append(solidFill29);

            shapeProperties29.Append(transform2D29);
            shapeProperties29.Append(customGeometry20);
            shapeProperties29.Append(outline20);

            Wps.ShapeStyle shapeStyle29 = new Wps.ShapeStyle();
            A.LineReference lineReference29 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference29 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference29 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference29 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle29.Append(lineReference29);
            shapeStyle29.Append(fillReference29);
            shapeStyle29.Append(effectReference29);
            shapeStyle29.Append(fontReference29);
            Wps.TextBodyProperties textBodyProperties29 = new Wps.TextBodyProperties();

            wordprocessingShape29.Append(nonVisualDrawingProperties20);
            wordprocessingShape29.Append(nonVisualDrawingShapeProperties29);
            wordprocessingShape29.Append(shapeProperties29);
            wordprocessingShape29.Append(shapeStyle29);
            wordprocessingShape29.Append(textBodyProperties29);

            Wps.WordprocessingShape wordprocessingShape30 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties21 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)43U, Name = "Curve44" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties30 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties30 = new Wps.ShapeProperties();

            A.Transform2D transform2D30 = new A.Transform2D();
            A.Offset offset31 = new A.Offset() { X = 970326L, Y = 287051L };
            A.Extents extents31 = new A.Extents() { Cx = 48951L, Cy = 130843L };

            transform2D30.Append(offset31);
            transform2D30.Append(extents31);

            A.CustomGeometry customGeometry21 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList30 = new A.AdjustValueList();
            A.Rectangle rectangle21 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList21 = new A.PathList();

            A.Path path21 = new A.Path() { Width = 48951L, Height = 130843L };

            A.MoveTo moveTo21 = new A.MoveTo();
            A.Point point63 = new A.Point() { X = "0", Y = "130843" };

            moveTo21.Append(point63);

            A.LineTo lineTo19 = new A.LineTo();
            A.Point point64 = new A.Point() { X = "48951", Y = "0" };

            lineTo19.Append(point64);

            path21.Append(moveTo21);
            path21.Append(lineTo19);

            pathList21.Append(path21);

            customGeometry21.Append(adjustValueList30);
            customGeometry21.Append(rectangle21);
            customGeometry21.Append(pathList21);

            A.Outline outline21 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill30 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex30 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha30 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex30.Append(alpha30);

            solidFill30.Append(rgbColorModelHex30);

            outline21.Append(solidFill30);

            shapeProperties30.Append(transform2D30);
            shapeProperties30.Append(customGeometry21);
            shapeProperties30.Append(outline21);

            Wps.ShapeStyle shapeStyle30 = new Wps.ShapeStyle();
            A.LineReference lineReference30 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference30 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference30 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference30 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle30.Append(lineReference30);
            shapeStyle30.Append(fillReference30);
            shapeStyle30.Append(effectReference30);
            shapeStyle30.Append(fontReference30);
            Wps.TextBodyProperties textBodyProperties30 = new Wps.TextBodyProperties();

            wordprocessingShape30.Append(nonVisualDrawingProperties21);
            wordprocessingShape30.Append(nonVisualDrawingShapeProperties30);
            wordprocessingShape30.Append(shapeProperties30);
            wordprocessingShape30.Append(shapeStyle30);
            wordprocessingShape30.Append(textBodyProperties30);

            Wps.WordprocessingShape wordprocessingShape31 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties22 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)45U, Name = "Curve46" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties31 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties31 = new Wps.ShapeProperties();

            A.Transform2D transform2D31 = new A.Transform2D();
            A.Offset offset32 = new A.Offset() { X = 742950L, Y = 125146L };
            A.Extents extents32 = new A.Extents() { Cx = 187884L, Cy = 31407L };

            transform2D31.Append(offset32);
            transform2D31.Append(extents32);

            A.CustomGeometry customGeometry22 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList31 = new A.AdjustValueList();
            A.Rectangle rectangle22 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList22 = new A.PathList();

            A.Path path22 = new A.Path() { Width = 187884L, Height = 31407L };

            A.MoveTo moveTo22 = new A.MoveTo();
            A.Point point65 = new A.Point() { X = "187884", Y = "0" };

            moveTo22.Append(point65);

            A.LineTo lineTo20 = new A.LineTo();
            A.Point point66 = new A.Point() { X = "0", Y = "31407" };

            lineTo20.Append(point66);

            path22.Append(moveTo22);
            path22.Append(lineTo20);

            pathList22.Append(path22);

            customGeometry22.Append(adjustValueList31);
            customGeometry22.Append(rectangle22);
            customGeometry22.Append(pathList22);

            A.Outline outline22 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill31 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex31 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha31 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex31.Append(alpha31);

            solidFill31.Append(rgbColorModelHex31);

            outline22.Append(solidFill31);

            shapeProperties31.Append(transform2D31);
            shapeProperties31.Append(customGeometry22);
            shapeProperties31.Append(outline22);

            Wps.ShapeStyle shapeStyle31 = new Wps.ShapeStyle();
            A.LineReference lineReference31 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference31 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference31 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference31 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle31.Append(lineReference31);
            shapeStyle31.Append(fillReference31);
            shapeStyle31.Append(effectReference31);
            shapeStyle31.Append(fontReference31);
            Wps.TextBodyProperties textBodyProperties31 = new Wps.TextBodyProperties();

            wordprocessingShape31.Append(nonVisualDrawingProperties22);
            wordprocessingShape31.Append(nonVisualDrawingShapeProperties31);
            wordprocessingShape31.Append(shapeProperties31);
            wordprocessingShape31.Append(shapeStyle31);
            wordprocessingShape31.Append(textBodyProperties31);

            Wps.WordprocessingShape wordprocessingShape32 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties23 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)47U, Name = "Curve48" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties32 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties32 = new Wps.ShapeProperties();

            A.Transform2D transform2D32 = new A.Transform2D();
            A.Offset offset33 = new A.Offset() { X = 772190L, Y = 154386L };
            A.Extents extents33 = new A.Extents() { Cx = 137779L, Cy = 23031L };

            transform2D32.Append(offset33);
            transform2D32.Append(extents33);

            A.CustomGeometry customGeometry23 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList32 = new A.AdjustValueList();
            A.Rectangle rectangle23 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList23 = new A.PathList();

            A.Path path23 = new A.Path() { Width = 137779L, Height = 23031L };

            A.MoveTo moveTo23 = new A.MoveTo();
            A.Point point67 = new A.Point() { X = "137779", Y = "0" };

            moveTo23.Append(point67);

            A.LineTo lineTo21 = new A.LineTo();
            A.Point point68 = new A.Point() { X = "0", Y = "23031" };

            lineTo21.Append(point68);

            path23.Append(moveTo23);
            path23.Append(lineTo21);

            pathList23.Append(path23);

            customGeometry23.Append(adjustValueList32);
            customGeometry23.Append(rectangle23);
            customGeometry23.Append(pathList23);

            A.Outline outline23 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill32 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex32 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha32 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex32.Append(alpha32);

            solidFill32.Append(rgbColorModelHex32);

            outline23.Append(solidFill32);

            shapeProperties32.Append(transform2D32);
            shapeProperties32.Append(customGeometry23);
            shapeProperties32.Append(outline23);

            Wps.ShapeStyle shapeStyle32 = new Wps.ShapeStyle();
            A.LineReference lineReference32 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference32 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference32 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference32 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle32.Append(lineReference32);
            shapeStyle32.Append(fillReference32);
            shapeStyle32.Append(effectReference32);
            shapeStyle32.Append(fontReference32);
            Wps.TextBodyProperties textBodyProperties32 = new Wps.TextBodyProperties();

            wordprocessingShape32.Append(nonVisualDrawingProperties23);
            wordprocessingShape32.Append(nonVisualDrawingShapeProperties32);
            wordprocessingShape32.Append(shapeProperties32);
            wordprocessingShape32.Append(shapeStyle32);
            wordprocessingShape32.Append(textBodyProperties32);

            Wps.WordprocessingShape wordprocessingShape33 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties24 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)49U, Name = "Curve50" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties33 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties33 = new Wps.ShapeProperties();

            A.Transform2D transform2D33 = new A.Transform2D();
            A.Offset offset34 = new A.Offset() { X = 676199L, Y = 334975L };
            A.Extents extents34 = new A.Extents() { Cx = 121133L, Cy = 147015L };

            transform2D33.Append(offset34);
            transform2D33.Append(extents34);

            A.CustomGeometry customGeometry24 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList33 = new A.AdjustValueList();
            A.Rectangle rectangle24 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList24 = new A.PathList();

            A.Path path24 = new A.Path() { Width = 121133L, Height = 147015L };

            A.MoveTo moveTo24 = new A.MoveTo();
            A.Point point69 = new A.Point() { X = "121133", Y = "147015" };

            moveTo24.Append(point69);

            A.LineTo lineTo22 = new A.LineTo();
            A.Point point70 = new A.Point() { X = "0", Y = "0" };

            lineTo22.Append(point70);

            path24.Append(moveTo24);
            path24.Append(lineTo22);

            pathList24.Append(path24);

            customGeometry24.Append(adjustValueList33);
            customGeometry24.Append(rectangle24);
            customGeometry24.Append(pathList24);

            A.Outline outline24 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill33 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex33 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha33 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex33.Append(alpha33);

            solidFill33.Append(rgbColorModelHex33);

            outline24.Append(solidFill33);

            shapeProperties33.Append(transform2D33);
            shapeProperties33.Append(customGeometry24);
            shapeProperties33.Append(outline24);

            Wps.ShapeStyle shapeStyle33 = new Wps.ShapeStyle();
            A.LineReference lineReference33 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference33 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference33 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference33 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle33.Append(lineReference33);
            shapeStyle33.Append(fillReference33);
            shapeStyle33.Append(effectReference33);
            shapeStyle33.Append(fontReference33);
            Wps.TextBodyProperties textBodyProperties33 = new Wps.TextBodyProperties();

            wordprocessingShape33.Append(nonVisualDrawingProperties24);
            wordprocessingShape33.Append(nonVisualDrawingShapeProperties33);
            wordprocessingShape33.Append(shapeProperties33);
            wordprocessingShape33.Append(shapeStyle33);
            wordprocessingShape33.Append(textBodyProperties33);

            Wps.WordprocessingShape wordprocessingShape34 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties25 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)51U, Name = "Curve52" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties34 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties34 = new Wps.ShapeProperties();

            A.Transform2D transform2D34 = new A.Transform2D();
            A.Offset offset35 = new A.Offset() { X = 711954L, Y = 338426L };
            A.Extents extents35 = new A.Extents() { Cx = 88829L, Cy = 107809L };

            transform2D34.Append(offset35);
            transform2D34.Append(extents35);

            A.CustomGeometry customGeometry25 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList34 = new A.AdjustValueList();
            A.Rectangle rectangle25 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList25 = new A.PathList();

            A.Path path25 = new A.Path() { Width = 88829L, Height = 107809L };

            A.MoveTo moveTo25 = new A.MoveTo();
            A.Point point71 = new A.Point() { X = "88829", Y = "107809" };

            moveTo25.Append(point71);

            A.LineTo lineTo23 = new A.LineTo();
            A.Point point72 = new A.Point() { X = "0", Y = "0" };

            lineTo23.Append(point72);

            path25.Append(moveTo25);
            path25.Append(lineTo23);

            pathList25.Append(path25);

            customGeometry25.Append(adjustValueList34);
            customGeometry25.Append(rectangle25);
            customGeometry25.Append(pathList25);

            A.Outline outline25 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill34 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex34 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha34 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex34.Append(alpha34);

            solidFill34.Append(rgbColorModelHex34);

            outline25.Append(solidFill34);

            shapeProperties34.Append(transform2D34);
            shapeProperties34.Append(customGeometry25);
            shapeProperties34.Append(outline25);

            Wps.ShapeStyle shapeStyle34 = new Wps.ShapeStyle();
            A.LineReference lineReference34 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference34 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference34 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference34 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle34.Append(lineReference34);
            shapeStyle34.Append(fillReference34);
            shapeStyle34.Append(effectReference34);
            shapeStyle34.Append(fontReference34);
            Wps.TextBodyProperties textBodyProperties34 = new Wps.TextBodyProperties();

            wordprocessingShape34.Append(nonVisualDrawingProperties25);
            wordprocessingShape34.Append(nonVisualDrawingShapeProperties34);
            wordprocessingShape34.Append(shapeProperties34);
            wordprocessingShape34.Append(shapeStyle34);
            wordprocessingShape34.Append(textBodyProperties34);

            Wps.WordprocessingShape wordprocessingShape35 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties26 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)53U, Name = "Curve54" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties35 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties35 = new Wps.ShapeProperties();

            A.Transform2D transform2D35 = new A.Transform2D();
            A.Offset offset36 = new A.Offset() { X = 9525L, Y = 1163079L };
            A.Extents extents36 = new A.Extents() { Cx = 55524L, Cy = 182232L };

            transform2D35.Append(offset36);
            transform2D35.Append(extents36);

            A.CustomGeometry customGeometry26 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList35 = new A.AdjustValueList();
            A.Rectangle rectangle26 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList26 = new A.PathList();

            A.Path path26 = new A.Path() { Width = 55524L, Height = 182232L };

            A.MoveTo moveTo26 = new A.MoveTo();
            A.Point point73 = new A.Point() { X = "0", Y = "0" };

            moveTo26.Append(point73);

            A.LineTo lineTo24 = new A.LineTo();
            A.Point point74 = new A.Point() { X = "55524", Y = "182232" };

            lineTo24.Append(point74);

            path26.Append(moveTo26);
            path26.Append(lineTo24);

            pathList26.Append(path26);

            customGeometry26.Append(adjustValueList35);
            customGeometry26.Append(rectangle26);
            customGeometry26.Append(pathList26);

            A.Outline outline26 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill35 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex35 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha35 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex35.Append(alpha35);

            solidFill35.Append(rgbColorModelHex35);

            outline26.Append(solidFill35);

            shapeProperties35.Append(transform2D35);
            shapeProperties35.Append(customGeometry26);
            shapeProperties35.Append(outline26);

            Wps.ShapeStyle shapeStyle35 = new Wps.ShapeStyle();
            A.LineReference lineReference35 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference35 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference35 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference35 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle35.Append(lineReference35);
            shapeStyle35.Append(fillReference35);
            shapeStyle35.Append(effectReference35);
            shapeStyle35.Append(fontReference35);
            Wps.TextBodyProperties textBodyProperties35 = new Wps.TextBodyProperties();

            wordprocessingShape35.Append(nonVisualDrawingProperties26);
            wordprocessingShape35.Append(nonVisualDrawingShapeProperties35);
            wordprocessingShape35.Append(shapeProperties35);
            wordprocessingShape35.Append(shapeStyle35);
            wordprocessingShape35.Append(textBodyProperties35);

            Wps.WordprocessingShape wordprocessingShape36 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties27 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)55U, Name = "Curve56" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties36 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties36 = new Wps.ShapeProperties();

            A.Transform2D transform2D36 = new A.Transform2D();
            A.Offset offset37 = new A.Offset() { X = 41225L, Y = 1179973L };
            A.Extents extents37 = new A.Extents() { Cx = 40718L, Cy = 133638L };

            transform2D36.Append(offset37);
            transform2D36.Append(extents37);

            A.CustomGeometry customGeometry27 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList36 = new A.AdjustValueList();
            A.Rectangle rectangle27 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList27 = new A.PathList();

            A.Path path27 = new A.Path() { Width = 40718L, Height = 133638L };

            A.MoveTo moveTo27 = new A.MoveTo();
            A.Point point75 = new A.Point() { X = "0", Y = "0" };

            moveTo27.Append(point75);

            A.LineTo lineTo25 = new A.LineTo();
            A.Point point76 = new A.Point() { X = "40718", Y = "133638" };

            lineTo25.Append(point76);

            path27.Append(moveTo27);
            path27.Append(lineTo25);

            pathList27.Append(path27);

            customGeometry27.Append(adjustValueList36);
            customGeometry27.Append(rectangle27);
            customGeometry27.Append(pathList27);

            A.Outline outline27 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill36 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex36 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha36 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex36.Append(alpha36);

            solidFill36.Append(rgbColorModelHex36);

            outline27.Append(solidFill36);

            shapeProperties36.Append(transform2D36);
            shapeProperties36.Append(customGeometry27);
            shapeProperties36.Append(outline27);

            Wps.ShapeStyle shapeStyle36 = new Wps.ShapeStyle();
            A.LineReference lineReference36 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference36 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference36 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference36 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle36.Append(lineReference36);
            shapeStyle36.Append(fillReference36);
            shapeStyle36.Append(effectReference36);
            shapeStyle36.Append(fontReference36);
            Wps.TextBodyProperties textBodyProperties36 = new Wps.TextBodyProperties();

            wordprocessingShape36.Append(nonVisualDrawingProperties27);
            wordprocessingShape36.Append(nonVisualDrawingShapeProperties36);
            wordprocessingShape36.Append(shapeProperties36);
            wordprocessingShape36.Append(shapeStyle36);
            wordprocessingShape36.Append(textBodyProperties36);

            Wps.WordprocessingShape wordprocessingShape37 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties28 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)57U, Name = "Curve58" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties37 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties37 = new Wps.ShapeProperties();

            A.Transform2D transform2D37 = new A.Transform2D();
            A.Offset offset38 = new A.Offset() { X = 250622L, Y = 1249159L };
            A.Extents extents38 = new A.Extents() { Cx = 130061L, Cy = 139192L };

            transform2D37.Append(offset38);
            transform2D37.Append(extents38);

            A.CustomGeometry customGeometry28 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList37 = new A.AdjustValueList();
            A.Rectangle rectangle28 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList28 = new A.PathList();

            A.Path path28 = new A.Path() { Width = 130061L, Height = 139192L };

            A.MoveTo moveTo28 = new A.MoveTo();
            A.Point point77 = new A.Point() { X = "0", Y = "139192" };

            moveTo28.Append(point77);

            A.LineTo lineTo26 = new A.LineTo();
            A.Point point78 = new A.Point() { X = "130061", Y = "0" };

            lineTo26.Append(point78);

            path28.Append(moveTo28);
            path28.Append(lineTo26);

            pathList28.Append(path28);

            customGeometry28.Append(adjustValueList37);
            customGeometry28.Append(rectangle28);
            customGeometry28.Append(pathList28);

            A.Outline outline28 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill37 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex37 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha37 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex37.Append(alpha37);

            solidFill37.Append(rgbColorModelHex37);

            outline28.Append(solidFill37);

            shapeProperties37.Append(transform2D37);
            shapeProperties37.Append(customGeometry28);
            shapeProperties37.Append(outline28);

            Wps.ShapeStyle shapeStyle37 = new Wps.ShapeStyle();
            A.LineReference lineReference37 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference37 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference37 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference37 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle37.Append(lineReference37);
            shapeStyle37.Append(fillReference37);
            shapeStyle37.Append(effectReference37);
            shapeStyle37.Append(fontReference37);
            Wps.TextBodyProperties textBodyProperties37 = new Wps.TextBodyProperties();

            wordprocessingShape37.Append(nonVisualDrawingProperties28);
            wordprocessingShape37.Append(nonVisualDrawingShapeProperties37);
            wordprocessingShape37.Append(shapeProperties37);
            wordprocessingShape37.Append(shapeStyle37);
            wordprocessingShape37.Append(textBodyProperties37);

            Wps.WordprocessingShape wordprocessingShape38 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties29 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)59U, Name = "Curve60" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties38 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties38 = new Wps.ShapeProperties();

            A.Transform2D transform2D38 = new A.Transform2D();
            A.Offset offset39 = new A.Offset() { X = 249404L, Y = 1250377L };
            A.Extents extents39 = new A.Extents() { Cx = 95378L, Cy = 102074L };

            transform2D38.Append(offset39);
            transform2D38.Append(extents39);

            A.CustomGeometry customGeometry29 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList38 = new A.AdjustValueList();
            A.Rectangle rectangle29 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList29 = new A.PathList();

            A.Path path29 = new A.Path() { Width = 95378L, Height = 102074L };

            A.MoveTo moveTo29 = new A.MoveTo();
            A.Point point79 = new A.Point() { X = "0", Y = "102074" };

            moveTo29.Append(point79);

            A.LineTo lineTo27 = new A.LineTo();
            A.Point point80 = new A.Point() { X = "95378", Y = "0" };

            lineTo27.Append(point80);

            path29.Append(moveTo29);
            path29.Append(lineTo27);

            pathList29.Append(path29);

            customGeometry29.Append(adjustValueList38);
            customGeometry29.Append(rectangle29);
            customGeometry29.Append(pathList29);

            A.Outline outline29 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill38 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex38 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha38 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex38.Append(alpha38);

            solidFill38.Append(rgbColorModelHex38);

            outline29.Append(solidFill38);

            shapeProperties38.Append(transform2D38);
            shapeProperties38.Append(customGeometry29);
            shapeProperties38.Append(outline29);

            Wps.ShapeStyle shapeStyle38 = new Wps.ShapeStyle();
            A.LineReference lineReference38 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference38 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference38 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference38 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle38.Append(lineReference38);
            shapeStyle38.Append(fillReference38);
            shapeStyle38.Append(effectReference38);
            shapeStyle38.Append(fontReference38);
            Wps.TextBodyProperties textBodyProperties38 = new Wps.TextBodyProperties();

            wordprocessingShape38.Append(nonVisualDrawingProperties29);
            wordprocessingShape38.Append(nonVisualDrawingShapeProperties38);
            wordprocessingShape38.Append(shapeProperties38);
            wordprocessingShape38.Append(shapeStyle38);
            wordprocessingShape38.Append(textBodyProperties38);

            Wps.WordprocessingShape wordprocessingShape39 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties30 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)61U, Name = "Curve62" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties39 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties39 = new Wps.ShapeProperties();

            A.Transform2D transform2D39 = new A.Transform2D();
            A.Offset offset40 = new A.Offset() { X = 139586L, Y = 1023887L };
            A.Extents extents40 = new A.Extents() { Cx = 185572L, Cy = 43040L };

            transform2D39.Append(offset40);
            transform2D39.Append(extents40);

            A.CustomGeometry customGeometry30 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList39 = new A.AdjustValueList();
            A.Rectangle rectangle30 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList30 = new A.PathList();

            A.Path path30 = new A.Path() { Width = 185572L, Height = 43040L };

            A.MoveTo moveTo30 = new A.MoveTo();
            A.Point point81 = new A.Point() { X = "0", Y = "0" };

            moveTo30.Append(point81);

            A.LineTo lineTo28 = new A.LineTo();
            A.Point point82 = new A.Point() { X = "185572", Y = "43040" };

            lineTo28.Append(point82);

            path30.Append(moveTo30);
            path30.Append(lineTo28);

            pathList30.Append(path30);

            customGeometry30.Append(adjustValueList39);
            customGeometry30.Append(rectangle30);
            customGeometry30.Append(pathList30);

            A.Outline outline30 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill39 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex39 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha39 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex39.Append(alpha39);

            solidFill39.Append(rgbColorModelHex39);

            outline30.Append(solidFill39);

            shapeProperties39.Append(transform2D39);
            shapeProperties39.Append(customGeometry30);
            shapeProperties39.Append(outline30);

            Wps.ShapeStyle shapeStyle39 = new Wps.ShapeStyle();
            A.LineReference lineReference39 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference39 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference39 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference39 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle39.Append(lineReference39);
            shapeStyle39.Append(fillReference39);
            shapeStyle39.Append(effectReference39);
            shapeStyle39.Append(fontReference39);
            Wps.TextBodyProperties textBodyProperties39 = new Wps.TextBodyProperties();

            wordprocessingShape39.Append(nonVisualDrawingProperties30);
            wordprocessingShape39.Append(nonVisualDrawingShapeProperties39);
            wordprocessingShape39.Append(shapeProperties39);
            wordprocessingShape39.Append(shapeStyle39);
            wordprocessingShape39.Append(textBodyProperties39);

            Wps.WordprocessingShape wordprocessingShape40 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties31 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)63U, Name = "Curve64" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties40 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties40 = new Wps.ShapeProperties();

            A.Transform2D transform2D40 = new A.Transform2D();
            A.Offset offset41 = new A.Offset() { X = 158590L, Y = 1054369L };
            A.Extents extents41 = new A.Extents() { Cx = 136086L, Cy = 31563L };

            transform2D40.Append(offset41);
            transform2D40.Append(extents41);

            A.CustomGeometry customGeometry31 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList40 = new A.AdjustValueList();
            A.Rectangle rectangle31 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList31 = new A.PathList();

            A.Path path31 = new A.Path() { Width = 136086L, Height = 31563L };

            A.MoveTo moveTo31 = new A.MoveTo();
            A.Point point83 = new A.Point() { X = "0", Y = "0" };

            moveTo31.Append(point83);

            A.LineTo lineTo29 = new A.LineTo();
            A.Point point84 = new A.Point() { X = "136086", Y = "31563" };

            lineTo29.Append(point84);

            path31.Append(moveTo31);
            path31.Append(lineTo29);

            pathList31.Append(path31);

            customGeometry31.Append(adjustValueList40);
            customGeometry31.Append(rectangle31);
            customGeometry31.Append(pathList31);

            A.Outline outline31 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill40 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex40 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha40 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex40.Append(alpha40);

            solidFill40.Append(rgbColorModelHex40);

            outline31.Append(solidFill40);

            shapeProperties40.Append(transform2D40);
            shapeProperties40.Append(customGeometry31);
            shapeProperties40.Append(outline31);

            Wps.ShapeStyle shapeStyle40 = new Wps.ShapeStyle();
            A.LineReference lineReference40 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference40 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference40 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference40 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle40.Append(lineReference40);
            shapeStyle40.Append(fillReference40);
            shapeStyle40.Append(effectReference40);
            shapeStyle40.Append(fontReference40);
            Wps.TextBodyProperties textBodyProperties40 = new Wps.TextBodyProperties();

            wordprocessingShape40.Append(nonVisualDrawingProperties31);
            wordprocessingShape40.Append(nonVisualDrawingShapeProperties40);
            wordprocessingShape40.Append(shapeProperties40);
            wordprocessingShape40.Append(shapeStyle40);
            wordprocessingShape40.Append(textBodyProperties40);

            Wps.WordprocessingShape wordprocessingShape41 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties32 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)65U, Name = "Curve66" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties41 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties41 = new Wps.ShapeProperties();

            A.Transform2D transform2D41 = new A.Transform2D();
            A.Offset offset42 = new A.Offset() { X = 571157L, Y = 1252106L };
            A.Extents extents42 = new A.Extents() { Cx = 125768L, Cy = 142913L };

            transform2D41.Append(offset42);
            transform2D41.Append(extents42);

            A.CustomGeometry customGeometry32 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList41 = new A.AdjustValueList();
            A.Rectangle rectangle32 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList32 = new A.PathList();

            A.Path path32 = new A.Path() { Width = 125768L, Height = 142913L };

            A.MoveTo moveTo32 = new A.MoveTo();
            A.Point point85 = new A.Point() { X = "0", Y = "0" };

            moveTo32.Append(point85);

            A.LineTo lineTo30 = new A.LineTo();
            A.Point point86 = new A.Point() { X = "125768", Y = "142913" };

            lineTo30.Append(point86);

            path32.Append(moveTo32);
            path32.Append(lineTo30);

            pathList32.Append(path32);

            customGeometry32.Append(adjustValueList41);
            customGeometry32.Append(rectangle32);
            customGeometry32.Append(pathList32);

            A.Outline outline32 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill41 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex41 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha41 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex41.Append(alpha41);

            solidFill41.Append(rgbColorModelHex41);

            outline32.Append(solidFill41);

            shapeProperties41.Append(transform2D41);
            shapeProperties41.Append(customGeometry32);
            shapeProperties41.Append(outline32);

            Wps.ShapeStyle shapeStyle41 = new Wps.ShapeStyle();
            A.LineReference lineReference41 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference41 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference41 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference41 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle41.Append(lineReference41);
            shapeStyle41.Append(fillReference41);
            shapeStyle41.Append(effectReference41);
            shapeStyle41.Append(fontReference41);
            Wps.TextBodyProperties textBodyProperties41 = new Wps.TextBodyProperties();

            wordprocessingShape41.Append(nonVisualDrawingProperties32);
            wordprocessingShape41.Append(nonVisualDrawingShapeProperties41);
            wordprocessingShape41.Append(shapeProperties41);
            wordprocessingShape41.Append(shapeStyle41);
            wordprocessingShape41.Append(textBodyProperties41);

            Wps.WordprocessingShape wordprocessingShape42 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties33 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)67U, Name = "Curve68" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties42 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties42 = new Wps.ShapeProperties();

            A.Transform2D transform2D42 = new A.Transform2D();
            A.Offset offset43 = new A.Offset() { X = 607005L, Y = 1254393L };
            A.Extents extents43 = new A.Extents() { Cx = 92208L, Cy = 104778L };

            transform2D42.Append(offset43);
            transform2D42.Append(extents43);

            A.CustomGeometry customGeometry33 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList42 = new A.AdjustValueList();
            A.Rectangle rectangle33 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList33 = new A.PathList();

            A.Path path33 = new A.Path() { Width = 92208L, Height = 104778L };

            A.MoveTo moveTo33 = new A.MoveTo();
            A.Point point87 = new A.Point() { X = "0", Y = "0" };

            moveTo33.Append(point87);

            A.LineTo lineTo31 = new A.LineTo();
            A.Point point88 = new A.Point() { X = "92208", Y = "104778" };

            lineTo31.Append(point88);

            path33.Append(moveTo33);
            path33.Append(lineTo31);

            pathList33.Append(path33);

            customGeometry33.Append(adjustValueList42);
            customGeometry33.Append(rectangle33);
            customGeometry33.Append(pathList33);

            A.Outline outline33 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill42 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex42 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha42 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex42.Append(alpha42);

            solidFill42.Append(rgbColorModelHex42);

            outline33.Append(solidFill42);

            shapeProperties42.Append(transform2D42);
            shapeProperties42.Append(customGeometry33);
            shapeProperties42.Append(outline33);

            Wps.ShapeStyle shapeStyle42 = new Wps.ShapeStyle();
            A.LineReference lineReference42 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference42 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference42 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference42 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle42.Append(lineReference42);
            shapeStyle42.Append(fillReference42);
            shapeStyle42.Append(effectReference42);
            shapeStyle42.Append(fontReference42);
            Wps.TextBodyProperties textBodyProperties42 = new Wps.TextBodyProperties();

            wordprocessingShape42.Append(nonVisualDrawingProperties33);
            wordprocessingShape42.Append(nonVisualDrawingShapeProperties42);
            wordprocessingShape42.Append(shapeProperties42);
            wordprocessingShape42.Append(shapeStyle42);
            wordprocessingShape42.Append(textBodyProperties42);

            Wps.WordprocessingShape wordprocessingShape43 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties34 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)69U, Name = "Curve70" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties43 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties43 = new Wps.ShapeProperties();

            A.Transform2D transform2D43 = new A.Transform2D();
            A.Offset offset44 = new A.Offset() { X = 883755L, Y = 1177328L };
            A.Extents extents44 = new A.Extents() { Cx = 61125L, Cy = 180416L };

            transform2D43.Append(offset44);
            transform2D43.Append(extents44);

            A.CustomGeometry customGeometry34 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList43 = new A.AdjustValueList();
            A.Rectangle rectangle34 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList34 = new A.PathList();

            A.Path path34 = new A.Path() { Width = 61125L, Height = 180416L };

            A.MoveTo moveTo34 = new A.MoveTo();
            A.Point point89 = new A.Point() { X = "0", Y = "180416" };

            moveTo34.Append(point89);

            A.LineTo lineTo32 = new A.LineTo();
            A.Point point90 = new A.Point() { X = "61125", Y = "0" };

            lineTo32.Append(point90);

            path34.Append(moveTo34);
            path34.Append(lineTo32);

            pathList34.Append(path34);

            customGeometry34.Append(adjustValueList43);
            customGeometry34.Append(rectangle34);
            customGeometry34.Append(pathList34);

            A.Outline outline34 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill43 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex43 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha43 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex43.Append(alpha43);

            solidFill43.Append(rgbColorModelHex43);

            outline34.Append(solidFill43);

            shapeProperties43.Append(transform2D43);
            shapeProperties43.Append(customGeometry34);
            shapeProperties43.Append(outline34);

            Wps.ShapeStyle shapeStyle43 = new Wps.ShapeStyle();
            A.LineReference lineReference43 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference43 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference43 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference43 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle43.Append(lineReference43);
            shapeStyle43.Append(fillReference43);
            shapeStyle43.Append(effectReference43);
            shapeStyle43.Append(fontReference43);
            Wps.TextBodyProperties textBodyProperties43 = new Wps.TextBodyProperties();

            wordprocessingShape43.Append(nonVisualDrawingProperties34);
            wordprocessingShape43.Append(nonVisualDrawingShapeProperties43);
            wordprocessingShape43.Append(shapeProperties43);
            wordprocessingShape43.Append(shapeStyle43);
            wordprocessingShape43.Append(textBodyProperties43);

            Wps.WordprocessingShape wordprocessingShape44 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties35 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)71U, Name = "Curve72" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties44 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties44 = new Wps.ShapeProperties();

            A.Transform2D transform2D44 = new A.Transform2D();
            A.Offset offset45 = new A.Offset() { X = 867849L, Y = 1193234L };
            A.Extents extents45 = new A.Extents() { Cx = 44824L, Cy = 132303L };

            transform2D44.Append(offset45);
            transform2D44.Append(extents45);

            A.CustomGeometry customGeometry35 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList44 = new A.AdjustValueList();
            A.Rectangle rectangle35 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList35 = new A.PathList();

            A.Path path35 = new A.Path() { Width = 44824L, Height = 132303L };

            A.MoveTo moveTo35 = new A.MoveTo();
            A.Point point91 = new A.Point() { X = "0", Y = "132303" };

            moveTo35.Append(point91);

            A.LineTo lineTo33 = new A.LineTo();
            A.Point point92 = new A.Point() { X = "44824", Y = "0" };

            lineTo33.Append(point92);

            path35.Append(moveTo35);
            path35.Append(lineTo33);

            pathList35.Append(path35);

            customGeometry35.Append(adjustValueList44);
            customGeometry35.Append(rectangle35);
            customGeometry35.Append(pathList35);

            A.Outline outline35 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill44 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex44 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha44 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex44.Append(alpha44);

            solidFill44.Append(rgbColorModelHex44);

            outline35.Append(solidFill44);

            shapeProperties44.Append(transform2D44);
            shapeProperties44.Append(customGeometry35);
            shapeProperties44.Append(outline35);

            Wps.ShapeStyle shapeStyle44 = new Wps.ShapeStyle();
            A.LineReference lineReference44 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference44 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference44 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference44 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle44.Append(lineReference44);
            shapeStyle44.Append(fillReference44);
            shapeStyle44.Append(effectReference44);
            shapeStyle44.Append(fontReference44);
            Wps.TextBodyProperties textBodyProperties44 = new Wps.TextBodyProperties();

            wordprocessingShape44.Append(nonVisualDrawingProperties35);
            wordprocessingShape44.Append(nonVisualDrawingShapeProperties44);
            wordprocessingShape44.Append(shapeProperties44);
            wordprocessingShape44.Append(shapeStyle44);
            wordprocessingShape44.Append(textBodyProperties44);

            Wps.WordprocessingShape wordprocessingShape45 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties36 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)73U, Name = "Curve74" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties45 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties45 = new Wps.ShapeProperties();

            A.Transform2D transform2D45 = new A.Transform2D();
            A.Offset offset46 = new A.Offset() { X = 632384L, Y = 1034161L };
            A.Extents extents46 = new A.Extents() { Cx = 186817L, Cy = 37262L };

            transform2D45.Append(offset46);
            transform2D45.Append(extents46);

            A.CustomGeometry customGeometry36 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList45 = new A.AdjustValueList();
            A.Rectangle rectangle36 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList36 = new A.PathList();

            A.Path path36 = new A.Path() { Width = 186817L, Height = 37262L };

            A.MoveTo moveTo36 = new A.MoveTo();
            A.Point point93 = new A.Point() { X = "186817", Y = "0" };

            moveTo36.Append(point93);

            A.LineTo lineTo34 = new A.LineTo();
            A.Point point94 = new A.Point() { X = "0", Y = "37262" };

            lineTo34.Append(point94);

            path36.Append(moveTo36);
            path36.Append(lineTo34);

            pathList36.Append(path36);

            customGeometry36.Append(adjustValueList45);
            customGeometry36.Append(rectangle36);
            customGeometry36.Append(pathList36);

            A.Outline outline36 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill45 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex45 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha45 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex45.Append(alpha45);

            solidFill45.Append(rgbColorModelHex45);

            outline36.Append(solidFill45);

            shapeProperties45.Append(transform2D45);
            shapeProperties45.Append(customGeometry36);
            shapeProperties45.Append(outline36);

            Wps.ShapeStyle shapeStyle45 = new Wps.ShapeStyle();
            A.LineReference lineReference45 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference45 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference45 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference45 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle45.Append(lineReference45);
            shapeStyle45.Append(fillReference45);
            shapeStyle45.Append(effectReference45);
            shapeStyle45.Append(fontReference45);
            Wps.TextBodyProperties textBodyProperties45 = new Wps.TextBodyProperties();

            wordprocessingShape45.Append(nonVisualDrawingProperties36);
            wordprocessingShape45.Append(nonVisualDrawingShapeProperties45);
            wordprocessingShape45.Append(shapeProperties45);
            wordprocessingShape45.Append(shapeStyle45);
            wordprocessingShape45.Append(textBodyProperties45);

            Wps.WordprocessingShape wordprocessingShape46 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties37 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)75U, Name = "Curve76" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties46 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties46 = new Wps.ShapeProperties();

            A.Transform2D transform2D46 = new A.Transform2D();
            A.Offset offset47 = new A.Offset() { X = 662261L, Y = 1064039L };
            A.Extents extents47 = new A.Extents() { Cx = 136998L, Cy = 27325L };

            transform2D46.Append(offset47);
            transform2D46.Append(extents47);

            A.CustomGeometry customGeometry37 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList46 = new A.AdjustValueList();
            A.Rectangle rectangle37 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList37 = new A.PathList();

            A.Path path37 = new A.Path() { Width = 136998L, Height = 27325L };

            A.MoveTo moveTo37 = new A.MoveTo();
            A.Point point95 = new A.Point() { X = "136998", Y = "0" };

            moveTo37.Append(point95);

            A.LineTo lineTo35 = new A.LineTo();
            A.Point point96 = new A.Point() { X = "0", Y = "27325" };

            lineTo35.Append(point96);

            path37.Append(moveTo37);
            path37.Append(lineTo35);

            pathList37.Append(path37);

            customGeometry37.Append(adjustValueList46);
            customGeometry37.Append(rectangle37);
            customGeometry37.Append(pathList37);

            A.Outline outline37 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill46 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex46 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha46 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex46.Append(alpha46);

            solidFill46.Append(rgbColorModelHex46);

            outline37.Append(solidFill46);

            shapeProperties46.Append(transform2D46);
            shapeProperties46.Append(customGeometry37);
            shapeProperties46.Append(outline37);

            Wps.ShapeStyle shapeStyle46 = new Wps.ShapeStyle();
            A.LineReference lineReference46 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference46 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference46 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference46 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle46.Append(lineReference46);
            shapeStyle46.Append(fillReference46);
            shapeStyle46.Append(effectReference46);
            shapeStyle46.Append(fontReference46);
            Wps.TextBodyProperties textBodyProperties46 = new Wps.TextBodyProperties();

            wordprocessingShape46.Append(nonVisualDrawingProperties37);
            wordprocessingShape46.Append(nonVisualDrawingShapeProperties46);
            wordprocessingShape46.Append(shapeProperties46);
            wordprocessingShape46.Append(shapeStyle46);
            wordprocessingShape46.Append(textBodyProperties46);

            Wps.WordprocessingShape wordprocessingShape47 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties38 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)77U, Name = "Curve78" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties47 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties47 = new Wps.ShapeProperties();

            A.Transform2D transform2D47 = new A.Transform2D();
            A.Offset offset48 = new A.Offset() { X = 771526L, Y = 714043L };
            A.Extents extents48 = new A.Extents() { Cx = 75766L, Cy = 97274L };

            transform2D47.Append(offset48);
            transform2D47.Append(extents48);

            A.CustomGeometry customGeometry38 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList47 = new A.AdjustValueList();
            A.Rectangle rectangle38 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList38 = new A.PathList();

            A.Path path38 = new A.Path() { Width = 75766L, Height = 97274L };

            A.MoveTo moveTo38 = new A.MoveTo();
            A.Point point97 = new A.Point() { X = "75766", Y = "97274" };

            moveTo38.Append(point97);

            A.LineTo lineTo36 = new A.LineTo();
            A.Point point98 = new A.Point() { X = "0", Y = "0" };

            lineTo36.Append(point98);

            path38.Append(moveTo38);
            path38.Append(lineTo36);

            pathList38.Append(path38);

            customGeometry38.Append(adjustValueList47);
            customGeometry38.Append(rectangle38);
            customGeometry38.Append(pathList38);

            A.Outline outline38 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill47 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex47 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha47 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex47.Append(alpha47);

            solidFill47.Append(rgbColorModelHex47);

            outline38.Append(solidFill47);

            shapeProperties47.Append(transform2D47);
            shapeProperties47.Append(customGeometry38);
            shapeProperties47.Append(outline38);

            Wps.ShapeStyle shapeStyle47 = new Wps.ShapeStyle();
            A.LineReference lineReference47 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference47 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference47 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference47 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle47.Append(lineReference47);
            shapeStyle47.Append(fillReference47);
            shapeStyle47.Append(effectReference47);
            shapeStyle47.Append(fontReference47);
            Wps.TextBodyProperties textBodyProperties47 = new Wps.TextBodyProperties();

            wordprocessingShape47.Append(nonVisualDrawingProperties38);
            wordprocessingShape47.Append(nonVisualDrawingShapeProperties47);
            wordprocessingShape47.Append(shapeProperties47);
            wordprocessingShape47.Append(shapeStyle47);
            wordprocessingShape47.Append(textBodyProperties47);

            Wps.WordprocessingShape wordprocessingShape48 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties39 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)79U, Name = "Curve80" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties48 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties48 = new Wps.ShapeProperties();

            A.Transform2D transform2D48 = new A.Transform2D();
            A.Offset offset49 = new A.Offset() { X = 751488L, Y = 729651L };
            A.Extents extents49 = new A.Extents() { Cx = 75766L, Cy = 97274L };

            transform2D48.Append(offset49);
            transform2D48.Append(extents49);

            A.CustomGeometry customGeometry39 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList48 = new A.AdjustValueList();
            A.Rectangle rectangle39 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList39 = new A.PathList();

            A.Path path39 = new A.Path() { Width = 75766L, Height = 97274L };

            A.MoveTo moveTo39 = new A.MoveTo();
            A.Point point99 = new A.Point() { X = "75766", Y = "97274" };

            moveTo39.Append(point99);

            A.LineTo lineTo37 = new A.LineTo();
            A.Point point100 = new A.Point() { X = "0", Y = "0" };

            lineTo37.Append(point100);

            path39.Append(moveTo39);
            path39.Append(lineTo37);

            pathList39.Append(path39);

            customGeometry39.Append(adjustValueList48);
            customGeometry39.Append(rectangle39);
            customGeometry39.Append(pathList39);

            A.Outline outline39 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill48 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex48 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha48 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex48.Append(alpha48);

            solidFill48.Append(rgbColorModelHex48);

            outline39.Append(solidFill48);

            shapeProperties48.Append(transform2D48);
            shapeProperties48.Append(customGeometry39);
            shapeProperties48.Append(outline39);

            Wps.ShapeStyle shapeStyle48 = new Wps.ShapeStyle();
            A.LineReference lineReference48 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference48 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference48 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference48 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle48.Append(lineReference48);
            shapeStyle48.Append(fillReference48);
            shapeStyle48.Append(effectReference48);
            shapeStyle48.Append(fontReference48);
            Wps.TextBodyProperties textBodyProperties48 = new Wps.TextBodyProperties();

            wordprocessingShape48.Append(nonVisualDrawingProperties39);
            wordprocessingShape48.Append(nonVisualDrawingShapeProperties48);
            wordprocessingShape48.Append(shapeProperties48);
            wordprocessingShape48.Append(shapeStyle48);
            wordprocessingShape48.Append(textBodyProperties48);
            ////OpenXmlMiscNode openXmlMiscNode1 = new OpenXmlMiscNode();

            Wps.WordprocessingShape wordprocessingShape49 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties40 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)81U, Name = "Curve82" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties49 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties49 = new Wps.ShapeProperties();

            A.Transform2D transform2D49 = new A.Transform2D();
            A.Offset offset50 = new A.Offset() { X = 431735L, Y = 912083L };
            A.Extents extents50 = new A.Extents() { Cx = 90571L, Cy = 93776L };

            transform2D49.Append(offset50);
            transform2D49.Append(extents50);

            A.CustomGeometry customGeometry40 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList49 = new A.AdjustValueList();
            A.Rectangle rectangle40 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList40 = new A.PathList();

            A.Path path40 = new A.Path() { Width = 90571L, Height = 93776L };

            A.MoveTo moveTo40 = new A.MoveTo();
            A.Point point101 = new A.Point() { X = "79204", Y = "12213" };

            moveTo40.Append(point101);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo1 = new A.QuadraticBezierCurveTo();
            A.Point point102 = new A.Point() { X = "84706", Y = "18259" };
            A.Point point103 = new A.Point() { X = "87609", Y = "27026" };

            quadraticBezierCurveTo1.Append(point102);
            quadraticBezierCurveTo1.Append(point103);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo2 = new A.QuadraticBezierCurveTo();
            A.Point point104 = new A.Point() { X = "90511", Y = "35793" };
            A.Point point105 = new A.Point() { X = "90571", Y = "46918" };

            quadraticBezierCurveTo2.Append(point104);
            quadraticBezierCurveTo2.Append(point105);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo3 = new A.QuadraticBezierCurveTo();
            A.Point point106 = new A.Point() { X = "90571", Y = "58043" };
            A.Point point107 = new A.Point() { X = "87609", Y = "66810" };

            quadraticBezierCurveTo3.Append(point106);
            quadraticBezierCurveTo3.Append(point107);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo4 = new A.QuadraticBezierCurveTo();
            A.Point point108 = new A.Point() { X = "84646", Y = "75577" };
            A.Point point109 = new A.Point() { X = "79204", Y = "81502" };

            quadraticBezierCurveTo4.Append(point108);
            quadraticBezierCurveTo4.Append(point109);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo5 = new A.QuadraticBezierCurveTo();
            A.Point point110 = new A.Point() { X = "73642", Y = "87609" };
            A.Point point111 = new A.Point() { X = "66084", Y = "90692" };

            quadraticBezierCurveTo5.Append(point110);
            quadraticBezierCurveTo5.Append(point111);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo6 = new A.QuadraticBezierCurveTo();
            A.Point point112 = new A.Point() { X = "58527", Y = "93776" };
            A.Point point113 = new A.Point() { X = "48732", Y = "93776" };

            quadraticBezierCurveTo6.Append(point112);
            quadraticBezierCurveTo6.Append(point113);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo7 = new A.QuadraticBezierCurveTo();
            A.Point point114 = new A.Point() { X = "39239", Y = "93776" };
            A.Point point115 = new A.Point() { X = "31500", Y = "90632" };

            quadraticBezierCurveTo7.Append(point114);
            quadraticBezierCurveTo7.Append(point115);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo8 = new A.QuadraticBezierCurveTo();
            A.Point point116 = new A.Point() { X = "23761", Y = "87488" };
            A.Point point117 = new A.Point() { X = "18259", Y = "81502" };

            quadraticBezierCurveTo8.Append(point116);
            quadraticBezierCurveTo8.Append(point117);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo9 = new A.QuadraticBezierCurveTo();
            A.Point point118 = new A.Point() { X = "12818", Y = "75516" };
            A.Point point119 = new A.Point() { X = "9916", Y = "66810" };

            quadraticBezierCurveTo9.Append(point118);
            quadraticBezierCurveTo9.Append(point119);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo10 = new A.QuadraticBezierCurveTo();
            A.Point point120 = new A.Point() { X = "7014", Y = "58103" };
            A.Point point121 = new A.Point() { X = "6953", Y = "46918" };

            quadraticBezierCurveTo10.Append(point120);
            quadraticBezierCurveTo10.Append(point121);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo11 = new A.QuadraticBezierCurveTo();
            A.Point point122 = new A.Point() { X = "6953", Y = "35914" };
            A.Point point123 = new A.Point() { X = "9855", Y = "27208" };

            quadraticBezierCurveTo11.Append(point122);
            quadraticBezierCurveTo11.Append(point123);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo12 = new A.QuadraticBezierCurveTo();
            A.Point point124 = new A.Point() { X = "12757", Y = "18501" };
            A.Point point125 = new A.Point() { X = "18320", Y = "12213" };

            quadraticBezierCurveTo12.Append(point124);
            quadraticBezierCurveTo12.Append(point125);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo13 = new A.QuadraticBezierCurveTo();
            A.Point point126 = new A.Point() { X = "23640", Y = "6288" };
            A.Point point127 = new A.Point() { X = "31500", Y = "3144" };

            quadraticBezierCurveTo13.Append(point126);
            quadraticBezierCurveTo13.Append(point127);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo14 = new A.QuadraticBezierCurveTo();
            A.Point point128 = new A.Point() { X = "39360", Y = "0" };
            A.Point point129 = new A.Point() { X = "48732", Y = "0" };

            quadraticBezierCurveTo14.Append(point128);
            quadraticBezierCurveTo14.Append(point129);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo15 = new A.QuadraticBezierCurveTo();
            A.Point point130 = new A.Point() { X = "58406", Y = "0" };
            A.Point point131 = new A.Point() { X = "66084", Y = "3144" };

            quadraticBezierCurveTo15.Append(point130);
            quadraticBezierCurveTo15.Append(point131);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo16 = new A.QuadraticBezierCurveTo();
            A.Point point132 = new A.Point() { X = "73763", Y = "6288" };
            A.Point point133 = new A.Point() { X = "79204", Y = "12213" };

            quadraticBezierCurveTo16.Append(point132);
            quadraticBezierCurveTo16.Append(point133);
            A.CloseShapePath closeShapePath1 = new A.CloseShapePath();

            A.MoveTo moveTo41 = new A.MoveTo();
            A.Point point134 = new A.Point() { X = "78116", Y = "46918" };

            moveTo41.Append(point134);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo17 = new A.QuadraticBezierCurveTo();
            A.Point point135 = new A.Point() { X = "78116", Y = "29384" };
            A.Point point136 = new A.Point() { X = "70256", Y = "19892" };

            quadraticBezierCurveTo17.Append(point135);
            quadraticBezierCurveTo17.Append(point136);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo18 = new A.QuadraticBezierCurveTo();
            A.Point point137 = new A.Point() { X = "62396", Y = "10399" };
            A.Point point138 = new A.Point() { X = "48792", Y = "10339" };

            quadraticBezierCurveTo18.Append(point137);
            quadraticBezierCurveTo18.Append(point138);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo19 = new A.QuadraticBezierCurveTo();
            A.Point point139 = new A.Point() { X = "35068", Y = "10339" };
            A.Point point140 = new A.Point() { X = "27268", Y = "19831" };

            quadraticBezierCurveTo19.Append(point139);
            quadraticBezierCurveTo19.Append(point140);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo20 = new A.QuadraticBezierCurveTo();
            A.Point point141 = new A.Point() { X = "19469", Y = "29324" };
            A.Point point142 = new A.Point() { X = "19408", Y = "46918" };

            quadraticBezierCurveTo20.Append(point141);
            quadraticBezierCurveTo20.Append(point142);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo21 = new A.QuadraticBezierCurveTo();
            A.Point point143 = new A.Point() { X = "19408", Y = "64633" };
            A.Point point144 = new A.Point() { X = "27389", Y = "74005" };

            quadraticBezierCurveTo21.Append(point143);
            quadraticBezierCurveTo21.Append(point144);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo22 = new A.QuadraticBezierCurveTo();
            A.Point point145 = new A.Point() { X = "35370", Y = "83376" };
            A.Point point146 = new A.Point() { X = "48792", Y = "83437" };

            quadraticBezierCurveTo22.Append(point145);
            quadraticBezierCurveTo22.Append(point146);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo23 = new A.QuadraticBezierCurveTo();
            A.Point point147 = new A.Point() { X = "62215", Y = "83437" };
            A.Point point148 = new A.Point() { X = "70135", Y = "74065" };

            quadraticBezierCurveTo23.Append(point147);
            quadraticBezierCurveTo23.Append(point148);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo24 = new A.QuadraticBezierCurveTo();
            A.Point point149 = new A.Point() { X = "78056", Y = "64694" };
            A.Point point150 = new A.Point() { X = "78116", Y = "46918" };

            quadraticBezierCurveTo24.Append(point149);
            quadraticBezierCurveTo24.Append(point150);
            A.CloseShapePath closeShapePath2 = new A.CloseShapePath();

            path40.Append(moveTo40);
            path40.Append(quadraticBezierCurveTo1);
            path40.Append(quadraticBezierCurveTo2);
            path40.Append(quadraticBezierCurveTo3);
            path40.Append(quadraticBezierCurveTo4);
            path40.Append(quadraticBezierCurveTo5);
            path40.Append(quadraticBezierCurveTo6);
            path40.Append(quadraticBezierCurveTo7);
            path40.Append(quadraticBezierCurveTo8);
            path40.Append(quadraticBezierCurveTo9);
            path40.Append(quadraticBezierCurveTo10);
            path40.Append(quadraticBezierCurveTo11);
            path40.Append(quadraticBezierCurveTo12);
            path40.Append(quadraticBezierCurveTo13);
            path40.Append(quadraticBezierCurveTo14);
            path40.Append(quadraticBezierCurveTo15);
            path40.Append(quadraticBezierCurveTo16);
            path40.Append(closeShapePath1);
            path40.Append(moveTo41);
            path40.Append(quadraticBezierCurveTo17);
            path40.Append(quadraticBezierCurveTo18);
            path40.Append(quadraticBezierCurveTo19);
            path40.Append(quadraticBezierCurveTo20);
            path40.Append(quadraticBezierCurveTo21);
            path40.Append(quadraticBezierCurveTo22);
            path40.Append(quadraticBezierCurveTo23);
            path40.Append(quadraticBezierCurveTo24);
            path40.Append(closeShapePath2);

            pathList40.Append(path40);

            customGeometry40.Append(adjustValueList49);
            customGeometry40.Append(rectangle40);
            customGeometry40.Append(pathList40);

            A.SolidFill solidFill49 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex49 = new A.RgbColorModelHex() { Val = "ff0000" };
            A.Alpha alpha49 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex49.Append(alpha49);

            solidFill49.Append(rgbColorModelHex49);

            shapeProperties49.Append(transform2D49);
            shapeProperties49.Append(customGeometry40);
            shapeProperties49.Append(solidFill49);

            Wps.ShapeStyle shapeStyle49 = new Wps.ShapeStyle();
            A.LineReference lineReference49 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference49 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference49 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference49 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle49.Append(lineReference49);
            shapeStyle49.Append(fillReference49);
            shapeStyle49.Append(effectReference49);
            shapeStyle49.Append(fontReference49);
            Wps.TextBodyProperties textBodyProperties49 = new Wps.TextBodyProperties();

            wordprocessingShape49.Append(nonVisualDrawingProperties40);
            wordprocessingShape49.Append(nonVisualDrawingShapeProperties49);
            wordprocessingShape49.Append(shapeProperties49);
            wordprocessingShape49.Append(shapeStyle49);
            wordprocessingShape49.Append(textBodyProperties49);
            //OpenXmlMiscNode openXmlMiscNode2 = new OpenXmlMiscNode();

            Wps.WordprocessingShape wordprocessingShape50 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties41 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)83U, Name = "Curve84" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties50 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties50 = new Wps.ShapeProperties();

            A.Transform2D transform2D50 = new A.Transform2D();
            A.Offset offset51 = new A.Offset() { X = 843004L, Y = 810402L };
            A.Extents extents51 = new A.Extents() { Cx = 70982L, Cy = 90027L };

            transform2D50.Append(offset51);
            transform2D50.Append(extents51);

            A.CustomGeometry customGeometry41 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList50 = new A.AdjustValueList();
            A.Rectangle rectangle41 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList41 = new A.PathList();

            A.Path path41 = new A.Path() { Width = 70982L, Height = 90027L };

            A.MoveTo moveTo42 = new A.MoveTo();
            A.Point point151 = new A.Point() { X = "70982", Y = "27208" };

            moveTo42.Append(point151);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo25 = new A.QuadraticBezierCurveTo();
            A.Point point152 = new A.Point() { X = "70982", Y = "33193" };
            A.Point point153 = new A.Point() { X = "68926", Y = "38272" };

            quadraticBezierCurveTo25.Append(point152);
            quadraticBezierCurveTo25.Append(point153);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo26 = new A.QuadraticBezierCurveTo();
            A.Point point154 = new A.Point() { X = "66870", Y = "43351" };
            A.Point point155 = new A.Point() { X = "63061", Y = "47160" };

            quadraticBezierCurveTo26.Append(point154);
            quadraticBezierCurveTo26.Append(point155);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo27 = new A.QuadraticBezierCurveTo();
            A.Point point156 = new A.Point() { X = "58406", Y = "51815" };
            A.Point point157 = new A.Point() { X = "52057", Y = "54113" };

            quadraticBezierCurveTo27.Append(point156);
            quadraticBezierCurveTo27.Append(point157);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo28 = new A.QuadraticBezierCurveTo();
            A.Point point158 = new A.Point() { X = "45709", Y = "56411" };
            A.Point point159 = new A.Point() { X = "36035", Y = "56471" };

            quadraticBezierCurveTo28.Append(point158);
            quadraticBezierCurveTo28.Append(point159);

            A.LineTo lineTo38 = new A.LineTo();
            A.Point point160 = new A.Point() { X = "24064", Y = "56471" };

            lineTo38.Append(point160);

            A.LineTo lineTo39 = new A.LineTo();
            A.Point point161 = new A.Point() { X = "24064", Y = "90027" };

            lineTo39.Append(point161);

            A.LineTo lineTo40 = new A.LineTo();
            A.Point point162 = new A.Point() { X = "12092", Y = "90027" };

            lineTo40.Append(point162);

            A.LineTo lineTo41 = new A.LineTo();
            A.Point point163 = new A.Point() { X = "12092", Y = "0" };

            lineTo41.Append(point163);

            A.LineTo lineTo42 = new A.LineTo();
            A.Point point164 = new A.Point() { X = "36519", Y = "0" };

            lineTo42.Append(point164);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo29 = new A.QuadraticBezierCurveTo();
            A.Point point165 = new A.Point() { X = "44621", Y = "0" };
            A.Point point166 = new A.Point() { X = "50243", Y = "1330" };

            quadraticBezierCurveTo29.Append(point165);
            quadraticBezierCurveTo29.Append(point166);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo30 = new A.QuadraticBezierCurveTo();
            A.Point point167 = new A.Point() { X = "55866", Y = "2660" };
            A.Point point168 = new A.Point() { X = "60220", Y = "5623" };

            quadraticBezierCurveTo30.Append(point167);
            quadraticBezierCurveTo30.Append(point168);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo31 = new A.QuadraticBezierCurveTo();
            A.Point point169 = new A.Point() { X = "65359", Y = "9069" };
            A.Point point170 = new A.Point() { X = "68140", Y = "14208" };

            quadraticBezierCurveTo31.Append(point169);
            quadraticBezierCurveTo31.Append(point170);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo32 = new A.QuadraticBezierCurveTo();
            A.Point point171 = new A.Point() { X = "70921", Y = "19348" };
            A.Point point172 = new A.Point() { X = "70982", Y = "27208" };

            quadraticBezierCurveTo32.Append(point171);
            quadraticBezierCurveTo32.Append(point172);
            A.CloseShapePath closeShapePath3 = new A.CloseShapePath();

            A.MoveTo moveTo43 = new A.MoveTo();
            A.Point point173 = new A.Point() { X = "58527", Y = "27510" };

            moveTo43.Append(point173);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo33 = new A.QuadraticBezierCurveTo();
            A.Point point174 = new A.Point() { X = "58527", Y = "22854" };
            A.Point point175 = new A.Point() { X = "56894", Y = "19408" };

            quadraticBezierCurveTo33.Append(point174);
            quadraticBezierCurveTo33.Append(point175);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo34 = new A.QuadraticBezierCurveTo();
            A.Point point176 = new A.Point() { X = "55262", Y = "15962" };
            A.Point point177 = new A.Point() { X = "51936", Y = "13785" };

            quadraticBezierCurveTo34.Append(point176);
            quadraticBezierCurveTo34.Append(point177);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo35 = new A.QuadraticBezierCurveTo();
            A.Point point178 = new A.Point() { X = "49034", Y = "11911" };
            A.Point point179 = new A.Point() { X = "45346", Y = "11125" };

            quadraticBezierCurveTo35.Append(point178);
            quadraticBezierCurveTo35.Append(point179);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo36 = new A.QuadraticBezierCurveTo();
            A.Point point180 = new A.Point() { X = "41658", Y = "10339" };
            A.Point point181 = new A.Point() { X = "35914", Y = "10278" };

            quadraticBezierCurveTo36.Append(point180);
            quadraticBezierCurveTo36.Append(point181);

            A.LineTo lineTo43 = new A.LineTo();
            A.Point point182 = new A.Point() { X = "24064", Y = "10278" };

            lineTo43.Append(point182);

            A.LineTo lineTo44 = new A.LineTo();
            A.Point point183 = new A.Point() { X = "24064", Y = "46253" };

            lineTo44.Append(point183);

            A.LineTo lineTo45 = new A.LineTo();
            A.Point point184 = new A.Point() { X = "34161", Y = "46253" };

            lineTo45.Append(point184);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo37 = new A.QuadraticBezierCurveTo();
            A.Point point185 = new A.Point() { X = "41416", Y = "46253" };
            A.Point point186 = new A.Point() { X = "45951", Y = "44983" };

            quadraticBezierCurveTo37.Append(point185);
            quadraticBezierCurveTo37.Append(point186);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo38 = new A.QuadraticBezierCurveTo();
            A.Point point187 = new A.Point() { X = "50485", Y = "43714" };
            A.Point point188 = new A.Point() { X = "53327", Y = "40811" };

            quadraticBezierCurveTo38.Append(point187);
            quadraticBezierCurveTo38.Append(point188);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo39 = new A.QuadraticBezierCurveTo();
            A.Point point189 = new A.Point() { X = "56169", Y = "37909" };
            A.Point point190 = new A.Point() { X = "57317", Y = "34705" };

            quadraticBezierCurveTo39.Append(point189);
            quadraticBezierCurveTo39.Append(point190);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo40 = new A.QuadraticBezierCurveTo();
            A.Point point191 = new A.Point() { X = "58466", Y = "31500" };
            A.Point point192 = new A.Point() { X = "58527", Y = "27510" };

            quadraticBezierCurveTo40.Append(point191);
            quadraticBezierCurveTo40.Append(point192);
            A.CloseShapePath closeShapePath4 = new A.CloseShapePath();

            path41.Append(moveTo42);
            path41.Append(quadraticBezierCurveTo25);
            path41.Append(quadraticBezierCurveTo26);
            path41.Append(quadraticBezierCurveTo27);
            path41.Append(quadraticBezierCurveTo28);
            path41.Append(lineTo38);
            path41.Append(lineTo39);
            path41.Append(lineTo40);
            path41.Append(lineTo41);
            path41.Append(lineTo42);
            path41.Append(quadraticBezierCurveTo29);
            path41.Append(quadraticBezierCurveTo30);
            path41.Append(quadraticBezierCurveTo31);
            path41.Append(quadraticBezierCurveTo32);
            path41.Append(closeShapePath3);
            path41.Append(moveTo43);
            path41.Append(quadraticBezierCurveTo33);
            path41.Append(quadraticBezierCurveTo34);
            path41.Append(quadraticBezierCurveTo35);
            path41.Append(quadraticBezierCurveTo36);
            path41.Append(lineTo43);
            path41.Append(lineTo44);
            path41.Append(lineTo45);
            path41.Append(quadraticBezierCurveTo37);
            path41.Append(quadraticBezierCurveTo38);
            path41.Append(quadraticBezierCurveTo39);
            path41.Append(quadraticBezierCurveTo40);
            path41.Append(closeShapePath4);

            pathList41.Append(path41);

            customGeometry41.Append(adjustValueList50);
            customGeometry41.Append(rectangle41);
            customGeometry41.Append(pathList41);

            A.SolidFill solidFill50 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex50 = new A.RgbColorModelHex() { Val = "ff8000" };
            A.Alpha alpha50 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex50.Append(alpha50);

            solidFill50.Append(rgbColorModelHex50);

            shapeProperties50.Append(transform2D50);
            shapeProperties50.Append(customGeometry41);
            shapeProperties50.Append(solidFill50);

            Wps.ShapeStyle shapeStyle50 = new Wps.ShapeStyle();
            A.LineReference lineReference50 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference50 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference50 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference50 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle50.Append(lineReference50);
            shapeStyle50.Append(fillReference50);
            shapeStyle50.Append(effectReference50);
            shapeStyle50.Append(fontReference50);
            Wps.TextBodyProperties textBodyProperties50 = new Wps.TextBodyProperties();

            wordprocessingShape50.Append(nonVisualDrawingProperties41);
            wordprocessingShape50.Append(nonVisualDrawingShapeProperties50);
            wordprocessingShape50.Append(shapeProperties50);
            wordprocessingShape50.Append(shapeStyle50);
            wordprocessingShape50.Append(textBodyProperties50);
            ////OpenXmlMiscNode openXmlMiscNode3 = new OpenXmlMiscNode();

            Wps.WordprocessingShape wordprocessingShape51 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties42 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)85U, Name = "Curve86" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties51 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties51 = new Wps.ShapeProperties();

            A.Transform2D transform2D51 = new A.Transform2D();
            A.Offset offset52 = new A.Offset() { X = 682946L, Y = 616434L };
            A.Extents extents52 = new A.Extents() { Cx = 80776L, Cy = 90027L };

            transform2D51.Append(offset52);
            transform2D51.Append(extents52);

            A.CustomGeometry customGeometry42 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList51 = new A.AdjustValueList();
            A.Rectangle rectangle42 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList42 = new A.PathList();

            A.Path path42 = new A.Path() { Width = 80776L, Height = 90027L };

            A.MoveTo moveTo44 = new A.MoveTo();
            A.Point point193 = new A.Point() { X = "80776", Y = "90027" };

            moveTo44.Append(point193);

            A.LineTo lineTo46 = new A.LineTo();
            A.Point point194 = new A.Point() { X = "65963", Y = "90027" };

            lineTo46.Append(point194);

            A.LineTo lineTo47 = new A.LineTo();
            A.Point point195 = new A.Point() { X = "23278", Y = "9492" };

            lineTo47.Append(point195);

            A.LineTo lineTo48 = new A.LineTo();
            A.Point point196 = new A.Point() { X = "23278", Y = "90027" };

            lineTo48.Append(point196);

            A.LineTo lineTo49 = new A.LineTo();
            A.Point point197 = new A.Point() { X = "12092", Y = "90027" };

            lineTo49.Append(point197);

            A.LineTo lineTo50 = new A.LineTo();
            A.Point point198 = new A.Point() { X = "12092", Y = "0" };

            lineTo50.Append(point198);

            A.LineTo lineTo51 = new A.LineTo();
            A.Point point199 = new A.Point() { X = "30654", Y = "0" };

            lineTo51.Append(point199);

            A.LineTo lineTo52 = new A.LineTo();
            A.Point point200 = new A.Point() { X = "69591", Y = "73521" };

            lineTo52.Append(point200);

            A.LineTo lineTo53 = new A.LineTo();
            A.Point point201 = new A.Point() { X = "69591", Y = "0" };

            lineTo53.Append(point201);

            A.LineTo lineTo54 = new A.LineTo();
            A.Point point202 = new A.Point() { X = "80776", Y = "0" };

            lineTo54.Append(point202);

            A.LineTo lineTo55 = new A.LineTo();
            A.Point point203 = new A.Point() { X = "80776", Y = "90027" };

            lineTo55.Append(point203);
            A.CloseShapePath closeShapePath5 = new A.CloseShapePath();

            path42.Append(moveTo44);
            path42.Append(lineTo46);
            path42.Append(lineTo47);
            path42.Append(lineTo48);
            path42.Append(lineTo49);
            path42.Append(lineTo50);
            path42.Append(lineTo51);
            path42.Append(lineTo52);
            path42.Append(lineTo53);
            path42.Append(lineTo54);
            path42.Append(lineTo55);
            path42.Append(closeShapePath5);

            pathList42.Append(path42);

            customGeometry42.Append(adjustValueList51);
            customGeometry42.Append(rectangle42);
            customGeometry42.Append(pathList42);

            A.SolidFill solidFill51 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex51 = new A.RgbColorModelHex() { Val = "0000ff" };
            A.Alpha alpha51 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex51.Append(alpha51);

            solidFill51.Append(rgbColorModelHex51);

            shapeProperties51.Append(transform2D51);
            shapeProperties51.Append(customGeometry42);
            shapeProperties51.Append(solidFill51);

            Wps.ShapeStyle shapeStyle51 = new Wps.ShapeStyle();
            A.LineReference lineReference51 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference51 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference51 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference51 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle51.Append(lineReference51);
            shapeStyle51.Append(fillReference51);
            shapeStyle51.Append(effectReference51);
            shapeStyle51.Append(fontReference51);
            Wps.TextBodyProperties textBodyProperties51 = new Wps.TextBodyProperties();

            wordprocessingShape51.Append(nonVisualDrawingProperties42);
            wordprocessingShape51.Append(nonVisualDrawingShapeProperties51);
            wordprocessingShape51.Append(shapeProperties51);
            wordprocessingShape51.Append(shapeStyle51);
            wordprocessingShape51.Append(textBodyProperties51);
            ////OpenXmlMiscNode openXmlMiscNode4 = new OpenXmlMiscNode();

            Wps.WordprocessingShape wordprocessingShape52 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties43 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)87U, Name = "Curve88" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties52 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties52 = new Wps.ShapeProperties();

            A.Transform2D transform2D52 = new A.Transform2D();
            A.Offset offset53 = new A.Offset() { X = 397350L, Y = 704496L };
            A.Extents extents53 = new A.Extents() { Cx = 77874L, Cy = 90027L };

            transform2D52.Append(offset53);
            transform2D52.Append(extents53);

            A.CustomGeometry customGeometry43 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList52 = new A.AdjustValueList();
            A.Rectangle rectangle43 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList43 = new A.PathList();

            A.Path path43 = new A.Path() { Width = 77874L, Height = 90027L };

            A.MoveTo moveTo45 = new A.MoveTo();
            A.Point point204 = new A.Point() { X = "77874", Y = "90027" };

            moveTo45.Append(point204);

            A.LineTo lineTo56 = new A.LineTo();
            A.Point point205 = new A.Point() { X = "7618", Y = "90027" };

            lineTo56.Append(point205);

            A.LineTo lineTo57 = new A.LineTo();
            A.Point point206 = new A.Point() { X = "7618", Y = "78902" };

            lineTo57.Append(point206);

            A.LineTo lineTo58 = new A.LineTo();
            A.Point point207 = new A.Point() { X = "62819", Y = "10641" };

            lineTo58.Append(point207);

            A.LineTo lineTo59 = new A.LineTo();
            A.Point point208 = new A.Point() { X = "9674", Y = "10641" };

            lineTo59.Append(point208);

            A.LineTo lineTo60 = new A.LineTo();
            A.Point point209 = new A.Point() { X = "9674", Y = "0" };

            lineTo60.Append(point209);

            A.LineTo lineTo61 = new A.LineTo();
            A.Point point210 = new A.Point() { X = "76544", Y = "0" };

            lineTo61.Append(point210);

            A.LineTo lineTo62 = new A.LineTo();
            A.Point point211 = new A.Point() { X = "76544", Y = "10823" };

            lineTo62.Append(point211);

            A.LineTo lineTo63 = new A.LineTo();
            A.Point point212 = new A.Point() { X = "20799", Y = "79386" };

            lineTo63.Append(point212);

            A.LineTo lineTo64 = new A.LineTo();
            A.Point point213 = new A.Point() { X = "77874", Y = "79386" };

            lineTo64.Append(point213);

            A.LineTo lineTo65 = new A.LineTo();
            A.Point point214 = new A.Point() { X = "77874", Y = "90027" };

            lineTo65.Append(point214);
            A.CloseShapePath closeShapePath6 = new A.CloseShapePath();

            path43.Append(moveTo45);
            path43.Append(lineTo56);
            path43.Append(lineTo57);
            path43.Append(lineTo58);
            path43.Append(lineTo59);
            path43.Append(lineTo60);
            path43.Append(lineTo61);
            path43.Append(lineTo62);
            path43.Append(lineTo63);
            path43.Append(lineTo64);
            path43.Append(lineTo65);
            path43.Append(closeShapePath6);

            pathList43.Append(path43);

            customGeometry43.Append(adjustValueList52);
            customGeometry43.Append(rectangle43);
            customGeometry43.Append(pathList43);

            A.SolidFill solidFill52 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex52 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha52 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex52.Append(alpha52);

            solidFill52.Append(rgbColorModelHex52);

            shapeProperties52.Append(transform2D52);
            shapeProperties52.Append(customGeometry43);
            shapeProperties52.Append(solidFill52);

            Wps.ShapeStyle shapeStyle52 = new Wps.ShapeStyle();
            A.LineReference lineReference52 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference52 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference52 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference52 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle52.Append(lineReference52);
            shapeStyle52.Append(fillReference52);
            shapeStyle52.Append(effectReference52);
            shapeStyle52.Append(fontReference52);
            Wps.TextBodyProperties textBodyProperties52 = new Wps.TextBodyProperties();

            wordprocessingShape52.Append(nonVisualDrawingProperties43);
            wordprocessingShape52.Append(nonVisualDrawingShapeProperties52);
            wordprocessingShape52.Append(shapeProperties52);
            wordprocessingShape52.Append(shapeStyle52);
            wordprocessingShape52.Append(textBodyProperties52);
            ////OpenXmlMiscNode openXmlMiscNode5 = new OpenXmlMiscNode();

            Wps.WordprocessingShape wordprocessingShape53 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties44 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)89U, Name = "Curve90" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties53 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties53 = new Wps.ShapeProperties();

            A.Transform2D transform2D53 = new A.Transform2D();
            A.Offset offset54 = new A.Offset() { X = 482177L, Y = 725114L };
            A.Extents extents54 = new A.Extents() { Cx = 67656L, Cy = 69410L };

            transform2D53.Append(offset54);
            transform2D53.Append(extents54);

            A.CustomGeometry customGeometry44 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList53 = new A.AdjustValueList();
            A.Rectangle rectangle44 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList44 = new A.PathList();

            A.Path path44 = new A.Path() { Width = 67656L, Height = 69410L };

            A.MoveTo moveTo46 = new A.MoveTo();
            A.Point point215 = new A.Point() { X = "67656", Y = "69410" };

            moveTo46.Append(point215);

            A.LineTo lineTo66 = new A.LineTo();
            A.Point point216 = new A.Point() { X = "56290", Y = "69410" };

            lineTo66.Append(point216);

            A.LineTo lineTo67 = new A.LineTo();
            A.Point point217 = new A.Point() { X = "56290", Y = "30956" };

            lineTo67.Append(point217);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo41 = new A.QuadraticBezierCurveTo();
            A.Point point218 = new A.Point() { X = "56290", Y = "26301" };
            A.Point point219 = new A.Point() { X = "55745", Y = "22250" };

            quadraticBezierCurveTo41.Append(point218);
            quadraticBezierCurveTo41.Append(point219);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo42 = new A.QuadraticBezierCurveTo();
            A.Point point220 = new A.Point() { X = "55201", Y = "18199" };
            A.Point point221 = new A.Point() { X = "53750", Y = "15841" };

            quadraticBezierCurveTo42.Append(point220);
            quadraticBezierCurveTo42.Append(point221);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo43 = new A.QuadraticBezierCurveTo();
            A.Point point222 = new A.Point() { X = "52239", Y = "13302" };
            A.Point point223 = new A.Point() { X = "49397", Y = "12092" };

            quadraticBezierCurveTo43.Append(point222);
            quadraticBezierCurveTo43.Append(point223);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo44 = new A.QuadraticBezierCurveTo();
            A.Point point224 = new A.Point() { X = "46555", Y = "10883" };
            A.Point point225 = new A.Point() { X = "42021", Y = "10823" };

            quadraticBezierCurveTo44.Append(point224);
            quadraticBezierCurveTo44.Append(point225);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo45 = new A.QuadraticBezierCurveTo();
            A.Point point226 = new A.Point() { X = "37365", Y = "10823" };
            A.Point point227 = new A.Point() { X = "32286", Y = "13120" };

            quadraticBezierCurveTo45.Append(point226);
            quadraticBezierCurveTo45.Append(point227);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo46 = new A.QuadraticBezierCurveTo();
            A.Point point228 = new A.Point() { X = "27208", Y = "15418" };
            A.Point point229 = new A.Point() { X = "22552", Y = "18985" };

            quadraticBezierCurveTo46.Append(point228);
            quadraticBezierCurveTo46.Append(point229);

            A.LineTo lineTo68 = new A.LineTo();
            A.Point point230 = new A.Point() { X = "22552", Y = "69410" };

            lineTo68.Append(point230);

            A.LineTo lineTo69 = new A.LineTo();
            A.Point point231 = new A.Point() { X = "11185", Y = "69410" };

            lineTo69.Append(point231);

            A.LineTo lineTo70 = new A.LineTo();
            A.Point point232 = new A.Point() { X = "11185", Y = "1874" };

            lineTo70.Append(point232);

            A.LineTo lineTo71 = new A.LineTo();
            A.Point point233 = new A.Point() { X = "22552", Y = "1874" };

            lineTo71.Append(point233);

            A.LineTo lineTo72 = new A.LineTo();
            A.Point point234 = new A.Point() { X = "22552", Y = "9372" };

            lineTo72.Append(point234);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo47 = new A.QuadraticBezierCurveTo();
            A.Point point235 = new A.Point() { X = "27873", Y = "4958" };
            A.Point point236 = new A.Point() { X = "33556", Y = "2479" };

            quadraticBezierCurveTo47.Append(point235);
            quadraticBezierCurveTo47.Append(point236);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo48 = new A.QuadraticBezierCurveTo();
            A.Point point237 = new A.Point() { X = "39239", Y = "0" };
            A.Point point238 = new A.Point() { X = "45225", Y = "0" };

            quadraticBezierCurveTo48.Append(point237);
            quadraticBezierCurveTo48.Append(point238);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo49 = new A.QuadraticBezierCurveTo();
            A.Point point239 = new A.Point() { X = "56169", Y = "0" };
            A.Point point240 = new A.Point() { X = "61913", Y = "6590" };

            quadraticBezierCurveTo49.Append(point239);
            quadraticBezierCurveTo49.Append(point240);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo50 = new A.QuadraticBezierCurveTo();
            A.Point point241 = new A.Point() { X = "67656", Y = "13181" };
            A.Point point242 = new A.Point() { X = "67656", Y = "25575" };

            quadraticBezierCurveTo50.Append(point241);
            quadraticBezierCurveTo50.Append(point242);

            A.LineTo lineTo73 = new A.LineTo();
            A.Point point243 = new A.Point() { X = "67656", Y = "69410" };

            lineTo73.Append(point243);
            A.CloseShapePath closeShapePath7 = new A.CloseShapePath();

            path44.Append(moveTo46);
            path44.Append(lineTo66);
            path44.Append(lineTo67);
            path44.Append(quadraticBezierCurveTo41);
            path44.Append(quadraticBezierCurveTo42);
            path44.Append(quadraticBezierCurveTo43);
            path44.Append(quadraticBezierCurveTo44);
            path44.Append(quadraticBezierCurveTo45);
            path44.Append(quadraticBezierCurveTo46);
            path44.Append(lineTo68);
            path44.Append(lineTo69);
            path44.Append(lineTo70);
            path44.Append(lineTo71);
            path44.Append(lineTo72);
            path44.Append(quadraticBezierCurveTo47);
            path44.Append(quadraticBezierCurveTo48);
            path44.Append(quadraticBezierCurveTo49);
            path44.Append(quadraticBezierCurveTo50);
            path44.Append(lineTo73);
            path44.Append(closeShapePath7);

            pathList44.Append(path44);

            customGeometry44.Append(adjustValueList53);
            customGeometry44.Append(rectangle44);
            customGeometry44.Append(pathList44);

            A.SolidFill solidFill53 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex53 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha53 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex53.Append(alpha53);

            solidFill53.Append(rgbColorModelHex53);

            shapeProperties53.Append(transform2D53);
            shapeProperties53.Append(customGeometry44);
            shapeProperties53.Append(solidFill53);

            Wps.ShapeStyle shapeStyle53 = new Wps.ShapeStyle();
            A.LineReference lineReference53 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference53 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference53 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference53 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle53.Append(lineReference53);
            shapeStyle53.Append(fillReference53);
            shapeStyle53.Append(effectReference53);
            shapeStyle53.Append(fontReference53);
            Wps.TextBodyProperties textBodyProperties53 = new Wps.TextBodyProperties();

            wordprocessingShape53.Append(nonVisualDrawingProperties44);
            wordprocessingShape53.Append(nonVisualDrawingShapeProperties53);
            wordprocessingShape53.Append(shapeProperties53);
            wordprocessingShape53.Append(shapeStyle53);
            wordprocessingShape53.Append(textBodyProperties53);
            ////OpenXmlMiscNode openXmlMiscNode6 = new OpenXmlMiscNode();

            Wps.WordprocessingShape wordprocessingShape54 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties45 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)91U, Name = "Curve92" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties54 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties54 = new Wps.ShapeProperties();

            A.Transform2D transform2D54 = new A.Transform2D();
            A.Offset offset55 = new A.Offset() { X = 984254L, Y = 871539L };
            A.Extents extents55 = new A.Extents() { Cx = 70982L, Cy = 90027L };

            transform2D54.Append(offset55);
            transform2D54.Append(extents55);

            A.CustomGeometry customGeometry45 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList54 = new A.AdjustValueList();
            A.Rectangle rectangle45 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList45 = new A.PathList();

            A.Path path45 = new A.Path() { Width = 70982L, Height = 90027L };

            A.MoveTo moveTo47 = new A.MoveTo();
            A.Point point244 = new A.Point() { X = "70982", Y = "27208" };

            moveTo47.Append(point244);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo51 = new A.QuadraticBezierCurveTo();
            A.Point point245 = new A.Point() { X = "70982", Y = "33193" };
            A.Point point246 = new A.Point() { X = "68926", Y = "38272" };

            quadraticBezierCurveTo51.Append(point245);
            quadraticBezierCurveTo51.Append(point246);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo52 = new A.QuadraticBezierCurveTo();
            A.Point point247 = new A.Point() { X = "66870", Y = "43351" };
            A.Point point248 = new A.Point() { X = "63061", Y = "47160" };

            quadraticBezierCurveTo52.Append(point247);
            quadraticBezierCurveTo52.Append(point248);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo53 = new A.QuadraticBezierCurveTo();
            A.Point point249 = new A.Point() { X = "58406", Y = "51815" };
            A.Point point250 = new A.Point() { X = "52057", Y = "54113" };

            quadraticBezierCurveTo53.Append(point249);
            quadraticBezierCurveTo53.Append(point250);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo54 = new A.QuadraticBezierCurveTo();
            A.Point point251 = new A.Point() { X = "45709", Y = "56411" };
            A.Point point252 = new A.Point() { X = "36035", Y = "56471" };

            quadraticBezierCurveTo54.Append(point251);
            quadraticBezierCurveTo54.Append(point252);

            A.LineTo lineTo74 = new A.LineTo();
            A.Point point253 = new A.Point() { X = "24064", Y = "56471" };

            lineTo74.Append(point253);

            A.LineTo lineTo75 = new A.LineTo();
            A.Point point254 = new A.Point() { X = "24064", Y = "90027" };

            lineTo75.Append(point254);

            A.LineTo lineTo76 = new A.LineTo();
            A.Point point255 = new A.Point() { X = "12092", Y = "90027" };

            lineTo76.Append(point255);

            A.LineTo lineTo77 = new A.LineTo();
            A.Point point256 = new A.Point() { X = "12092", Y = "0" };

            lineTo77.Append(point256);

            A.LineTo lineTo78 = new A.LineTo();
            A.Point point257 = new A.Point() { X = "36519", Y = "0" };

            lineTo78.Append(point257);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo55 = new A.QuadraticBezierCurveTo();
            A.Point point258 = new A.Point() { X = "44621", Y = "0" };
            A.Point point259 = new A.Point() { X = "50243", Y = "1330" };

            quadraticBezierCurveTo55.Append(point258);
            quadraticBezierCurveTo55.Append(point259);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo56 = new A.QuadraticBezierCurveTo();
            A.Point point260 = new A.Point() { X = "55866", Y = "2660" };
            A.Point point261 = new A.Point() { X = "60220", Y = "5623" };

            quadraticBezierCurveTo56.Append(point260);
            quadraticBezierCurveTo56.Append(point261);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo57 = new A.QuadraticBezierCurveTo();
            A.Point point262 = new A.Point() { X = "65359", Y = "9069" };
            A.Point point263 = new A.Point() { X = "68140", Y = "14208" };

            quadraticBezierCurveTo57.Append(point262);
            quadraticBezierCurveTo57.Append(point263);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo58 = new A.QuadraticBezierCurveTo();
            A.Point point264 = new A.Point() { X = "70921", Y = "19348" };
            A.Point point265 = new A.Point() { X = "70982", Y = "27208" };

            quadraticBezierCurveTo58.Append(point264);
            quadraticBezierCurveTo58.Append(point265);
            A.CloseShapePath closeShapePath8 = new A.CloseShapePath();

            A.MoveTo moveTo48 = new A.MoveTo();
            A.Point point266 = new A.Point() { X = "58527", Y = "27510" };

            moveTo48.Append(point266);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo59 = new A.QuadraticBezierCurveTo();
            A.Point point267 = new A.Point() { X = "58527", Y = "22854" };
            A.Point point268 = new A.Point() { X = "56894", Y = "19408" };

            quadraticBezierCurveTo59.Append(point267);
            quadraticBezierCurveTo59.Append(point268);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo60 = new A.QuadraticBezierCurveTo();
            A.Point point269 = new A.Point() { X = "55262", Y = "15962" };
            A.Point point270 = new A.Point() { X = "51936", Y = "13785" };

            quadraticBezierCurveTo60.Append(point269);
            quadraticBezierCurveTo60.Append(point270);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo61 = new A.QuadraticBezierCurveTo();
            A.Point point271 = new A.Point() { X = "49034", Y = "11911" };
            A.Point point272 = new A.Point() { X = "45346", Y = "11125" };

            quadraticBezierCurveTo61.Append(point271);
            quadraticBezierCurveTo61.Append(point272);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo62 = new A.QuadraticBezierCurveTo();
            A.Point point273 = new A.Point() { X = "41658", Y = "10339" };
            A.Point point274 = new A.Point() { X = "35914", Y = "10278" };

            quadraticBezierCurveTo62.Append(point273);
            quadraticBezierCurveTo62.Append(point274);

            A.LineTo lineTo79 = new A.LineTo();
            A.Point point275 = new A.Point() { X = "24064", Y = "10278" };

            lineTo79.Append(point275);

            A.LineTo lineTo80 = new A.LineTo();
            A.Point point276 = new A.Point() { X = "24064", Y = "46253" };

            lineTo80.Append(point276);

            A.LineTo lineTo81 = new A.LineTo();
            A.Point point277 = new A.Point() { X = "34161", Y = "46253" };

            lineTo81.Append(point277);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo63 = new A.QuadraticBezierCurveTo();
            A.Point point278 = new A.Point() { X = "41416", Y = "46253" };
            A.Point point279 = new A.Point() { X = "45951", Y = "44983" };

            quadraticBezierCurveTo63.Append(point278);
            quadraticBezierCurveTo63.Append(point279);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo64 = new A.QuadraticBezierCurveTo();
            A.Point point280 = new A.Point() { X = "50485", Y = "43714" };
            A.Point point281 = new A.Point() { X = "53327", Y = "40811" };

            quadraticBezierCurveTo64.Append(point280);
            quadraticBezierCurveTo64.Append(point281);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo65 = new A.QuadraticBezierCurveTo();
            A.Point point282 = new A.Point() { X = "56169", Y = "37909" };
            A.Point point283 = new A.Point() { X = "57317", Y = "34705" };

            quadraticBezierCurveTo65.Append(point282);
            quadraticBezierCurveTo65.Append(point283);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo66 = new A.QuadraticBezierCurveTo();
            A.Point point284 = new A.Point() { X = "58466", Y = "31500" };
            A.Point point285 = new A.Point() { X = "58527", Y = "27510" };

            quadraticBezierCurveTo66.Append(point284);
            quadraticBezierCurveTo66.Append(point285);
            A.CloseShapePath closeShapePath9 = new A.CloseShapePath();

            path45.Append(moveTo47);
            path45.Append(quadraticBezierCurveTo51);
            path45.Append(quadraticBezierCurveTo52);
            path45.Append(quadraticBezierCurveTo53);
            path45.Append(quadraticBezierCurveTo54);
            path45.Append(lineTo74);
            path45.Append(lineTo75);
            path45.Append(lineTo76);
            path45.Append(lineTo77);
            path45.Append(lineTo78);
            path45.Append(quadraticBezierCurveTo55);
            path45.Append(quadraticBezierCurveTo56);
            path45.Append(quadraticBezierCurveTo57);
            path45.Append(quadraticBezierCurveTo58);
            path45.Append(closeShapePath8);
            path45.Append(moveTo48);
            path45.Append(quadraticBezierCurveTo59);
            path45.Append(quadraticBezierCurveTo60);
            path45.Append(quadraticBezierCurveTo61);
            path45.Append(quadraticBezierCurveTo62);
            path45.Append(lineTo79);
            path45.Append(lineTo80);
            path45.Append(lineTo81);
            path45.Append(quadraticBezierCurveTo63);
            path45.Append(quadraticBezierCurveTo64);
            path45.Append(quadraticBezierCurveTo65);
            path45.Append(quadraticBezierCurveTo66);
            path45.Append(closeShapePath9);

            pathList45.Append(path45);

            customGeometry45.Append(adjustValueList54);
            customGeometry45.Append(rectangle45);
            customGeometry45.Append(pathList45);

            A.SolidFill solidFill54 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex54 = new A.RgbColorModelHex() { Val = "404040" };
            A.Alpha alpha54 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex54.Append(alpha54);

            solidFill54.Append(rgbColorModelHex54);

            shapeProperties54.Append(transform2D54);
            shapeProperties54.Append(customGeometry45);
            shapeProperties54.Append(solidFill54);

            Wps.ShapeStyle shapeStyle54 = new Wps.ShapeStyle();
            A.LineReference lineReference54 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference54 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference54 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference54 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle54.Append(lineReference54);
            shapeStyle54.Append(fillReference54);
            shapeStyle54.Append(effectReference54);
            shapeStyle54.Append(fontReference54);
            Wps.TextBodyProperties textBodyProperties54 = new Wps.TextBodyProperties();

            wordprocessingShape54.Append(nonVisualDrawingProperties45);
            wordprocessingShape54.Append(nonVisualDrawingShapeProperties54);
            wordprocessingShape54.Append(shapeProperties54);
            wordprocessingShape54.Append(shapeStyle54);
            wordprocessingShape54.Append(textBodyProperties54);
            ////OpenXmlMiscNode openXmlMiscNode7 = new OpenXmlMiscNode();

            Wps.WordprocessingShape wordprocessingShape55 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties46 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)93U, Name = "Curve94" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties55 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties55 = new Wps.ShapeProperties();

            A.Transform2D transform2D55 = new A.Transform2D();
            A.Offset offset56 = new A.Offset() { X = 1058924L, Y = 867488L };
            A.Extents extents56 = new A.Extents() { Cx = 67656L, Cy = 94078L };

            transform2D55.Append(offset56);
            transform2D55.Append(extents56);

            A.CustomGeometry customGeometry46 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList55 = new A.AdjustValueList();
            A.Rectangle rectangle46 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList46 = new A.PathList();

            A.Path path46 = new A.Path() { Width = 67656L, Height = 94078L };

            A.MoveTo moveTo49 = new A.MoveTo();
            A.Point point286 = new A.Point() { X = "67656", Y = "94078" };

            moveTo49.Append(point286);

            A.LineTo lineTo82 = new A.LineTo();
            A.Point point287 = new A.Point() { X = "56290", Y = "94078" };

            lineTo82.Append(point287);

            A.LineTo lineTo83 = new A.LineTo();
            A.Point point288 = new A.Point() { X = "56290", Y = "55625" };

            lineTo83.Append(point288);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo67 = new A.QuadraticBezierCurveTo();
            A.Point point289 = new A.Point() { X = "56290", Y = "50969" };
            A.Point point290 = new A.Point() { X = "55745", Y = "46918" };

            quadraticBezierCurveTo67.Append(point289);
            quadraticBezierCurveTo67.Append(point290);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo68 = new A.QuadraticBezierCurveTo();
            A.Point point291 = new A.Point() { X = "55201", Y = "42867" };
            A.Point point292 = new A.Point() { X = "53750", Y = "40509" };

            quadraticBezierCurveTo68.Append(point291);
            quadraticBezierCurveTo68.Append(point292);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo69 = new A.QuadraticBezierCurveTo();
            A.Point point293 = new A.Point() { X = "52239", Y = "37970" };
            A.Point point294 = new A.Point() { X = "49397", Y = "36761" };

            quadraticBezierCurveTo69.Append(point293);
            quadraticBezierCurveTo69.Append(point294);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo70 = new A.QuadraticBezierCurveTo();
            A.Point point295 = new A.Point() { X = "46555", Y = "35551" };
            A.Point point296 = new A.Point() { X = "42021", Y = "35491" };

            quadraticBezierCurveTo70.Append(point295);
            quadraticBezierCurveTo70.Append(point296);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo71 = new A.QuadraticBezierCurveTo();
            A.Point point297 = new A.Point() { X = "37365", Y = "35491" };
            A.Point point298 = new A.Point() { X = "32286", Y = "37788" };

            quadraticBezierCurveTo71.Append(point297);
            quadraticBezierCurveTo71.Append(point298);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo72 = new A.QuadraticBezierCurveTo();
            A.Point point299 = new A.Point() { X = "27208", Y = "40086" };
            A.Point point300 = new A.Point() { X = "22552", Y = "43653" };

            quadraticBezierCurveTo72.Append(point299);
            quadraticBezierCurveTo72.Append(point300);

            A.LineTo lineTo84 = new A.LineTo();
            A.Point point301 = new A.Point() { X = "22552", Y = "94078" };

            lineTo84.Append(point301);

            A.LineTo lineTo85 = new A.LineTo();
            A.Point point302 = new A.Point() { X = "11185", Y = "94078" };

            lineTo85.Append(point302);

            A.LineTo lineTo86 = new A.LineTo();
            A.Point point303 = new A.Point() { X = "11185", Y = "0" };

            lineTo86.Append(point303);

            A.LineTo lineTo87 = new A.LineTo();
            A.Point point304 = new A.Point() { X = "22552", Y = "0" };

            lineTo87.Append(point304);

            A.LineTo lineTo88 = new A.LineTo();
            A.Point point305 = new A.Point() { X = "22552", Y = "34040" };

            lineTo88.Append(point305);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo73 = new A.QuadraticBezierCurveTo();
            A.Point point306 = new A.Point() { X = "27873", Y = "29626" };
            A.Point point307 = new A.Point() { X = "33556", Y = "27147" };

            quadraticBezierCurveTo73.Append(point306);
            quadraticBezierCurveTo73.Append(point307);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo74 = new A.QuadraticBezierCurveTo();
            A.Point point308 = new A.Point() { X = "39239", Y = "24668" };
            A.Point point309 = new A.Point() { X = "45225", Y = "24668" };

            quadraticBezierCurveTo74.Append(point308);
            quadraticBezierCurveTo74.Append(point309);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo75 = new A.QuadraticBezierCurveTo();
            A.Point point310 = new A.Point() { X = "56169", Y = "24668" };
            A.Point point311 = new A.Point() { X = "61913", Y = "31259" };

            quadraticBezierCurveTo75.Append(point310);
            quadraticBezierCurveTo75.Append(point311);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo76 = new A.QuadraticBezierCurveTo();
            A.Point point312 = new A.Point() { X = "67656", Y = "37849" };
            A.Point point313 = new A.Point() { X = "67656", Y = "50243" };

            quadraticBezierCurveTo76.Append(point312);
            quadraticBezierCurveTo76.Append(point313);

            A.LineTo lineTo89 = new A.LineTo();
            A.Point point314 = new A.Point() { X = "67656", Y = "94078" };

            lineTo89.Append(point314);
            A.CloseShapePath closeShapePath10 = new A.CloseShapePath();

            path46.Append(moveTo49);
            path46.Append(lineTo82);
            path46.Append(lineTo83);
            path46.Append(quadraticBezierCurveTo67);
            path46.Append(quadraticBezierCurveTo68);
            path46.Append(quadraticBezierCurveTo69);
            path46.Append(quadraticBezierCurveTo70);
            path46.Append(quadraticBezierCurveTo71);
            path46.Append(quadraticBezierCurveTo72);
            path46.Append(lineTo84);
            path46.Append(lineTo85);
            path46.Append(lineTo86);
            path46.Append(lineTo87);
            path46.Append(lineTo88);
            path46.Append(quadraticBezierCurveTo73);
            path46.Append(quadraticBezierCurveTo74);
            path46.Append(quadraticBezierCurveTo75);
            path46.Append(quadraticBezierCurveTo76);
            path46.Append(lineTo89);
            path46.Append(closeShapePath10);

            pathList46.Append(path46);

            customGeometry46.Append(adjustValueList55);
            customGeometry46.Append(rectangle46);
            customGeometry46.Append(pathList46);

            A.SolidFill solidFill55 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex55 = new A.RgbColorModelHex() { Val = "404040" };
            A.Alpha alpha55 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex55.Append(alpha55);

            solidFill55.Append(rgbColorModelHex55);

            shapeProperties55.Append(transform2D55);
            shapeProperties55.Append(customGeometry46);
            shapeProperties55.Append(solidFill55);

            Wps.ShapeStyle shapeStyle55 = new Wps.ShapeStyle();
            A.LineReference lineReference55 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference55 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference55 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference55 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle55.Append(lineReference55);
            shapeStyle55.Append(fillReference55);
            shapeStyle55.Append(effectReference55);
            shapeStyle55.Append(fontReference55);
            Wps.TextBodyProperties textBodyProperties55 = new Wps.TextBodyProperties();

            wordprocessingShape55.Append(nonVisualDrawingProperties46);
            wordprocessingShape55.Append(nonVisualDrawingShapeProperties55);
            wordprocessingShape55.Append(shapeProperties55);
            wordprocessingShape55.Append(shapeStyle55);
            wordprocessingShape55.Append(textBodyProperties55);
            //OpenXmlMiscNode openXmlMiscNode8 = new OpenXmlMiscNode();

            Wps.WordprocessingShape wordprocessingShape56 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties47 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)95U, Name = "Curve96" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties56 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties56 = new Wps.ShapeProperties();

            A.Transform2D transform2D56 = new A.Transform2D();
            A.Offset offset57 = new A.Offset() { X = 223869L, Y = 538342L };
            A.Extents extents57 = new A.Extents() { Cx = 71405L, Cy = 90027L };

            transform2D56.Append(offset57);
            transform2D56.Append(extents57);

            A.CustomGeometry customGeometry47 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList56 = new A.AdjustValueList();
            A.Rectangle rectangle47 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList47 = new A.PathList();

            A.Path path47 = new A.Path() { Width = 71405L, Height = 90027L };

            A.MoveTo moveTo50 = new A.MoveTo();
            A.Point point315 = new A.Point() { X = "71405", Y = "90027" };

            moveTo50.Append(point315);

            A.LineTo lineTo90 = new A.LineTo();
            A.Point point316 = new A.Point() { X = "12092", Y = "90027" };

            lineTo90.Append(point316);

            A.LineTo lineTo91 = new A.LineTo();
            A.Point point317 = new A.Point() { X = "12092", Y = "0" };

            lineTo91.Append(point317);

            A.LineTo lineTo92 = new A.LineTo();
            A.Point point318 = new A.Point() { X = "71405", Y = "0" };

            lineTo92.Append(point318);

            A.LineTo lineTo93 = new A.LineTo();
            A.Point point319 = new A.Point() { X = "71405", Y = "10641" };

            lineTo93.Append(point319);

            A.LineTo lineTo94 = new A.LineTo();
            A.Point point320 = new A.Point() { X = "24064", Y = "10641" };

            lineTo94.Append(point320);

            A.LineTo lineTo95 = new A.LineTo();
            A.Point point321 = new A.Point() { X = "24064", Y = "35309" };

            lineTo95.Append(point321);

            A.LineTo lineTo96 = new A.LineTo();
            A.Point point322 = new A.Point() { X = "71405", Y = "35309" };

            lineTo96.Append(point322);

            A.LineTo lineTo97 = new A.LineTo();
            A.Point point323 = new A.Point() { X = "71405", Y = "45951" };

            lineTo97.Append(point323);

            A.LineTo lineTo98 = new A.LineTo();
            A.Point point324 = new A.Point() { X = "24064", Y = "45951" };

            lineTo98.Append(point324);

            A.LineTo lineTo99 = new A.LineTo();
            A.Point point325 = new A.Point() { X = "24064", Y = "79386" };

            lineTo99.Append(point325);

            A.LineTo lineTo100 = new A.LineTo();
            A.Point point326 = new A.Point() { X = "71405", Y = "79386" };

            lineTo100.Append(point326);

            A.LineTo lineTo101 = new A.LineTo();
            A.Point point327 = new A.Point() { X = "71405", Y = "90027" };

            lineTo101.Append(point327);
            A.CloseShapePath closeShapePath11 = new A.CloseShapePath();

            path47.Append(moveTo50);
            path47.Append(lineTo90);
            path47.Append(lineTo91);
            path47.Append(lineTo92);
            path47.Append(lineTo93);
            path47.Append(lineTo94);
            path47.Append(lineTo95);
            path47.Append(lineTo96);
            path47.Append(lineTo97);
            path47.Append(lineTo98);
            path47.Append(lineTo99);
            path47.Append(lineTo100);
            path47.Append(lineTo101);
            path47.Append(closeShapePath11);

            pathList47.Append(path47);

            customGeometry47.Append(adjustValueList56);
            customGeometry47.Append(rectangle47);
            customGeometry47.Append(pathList47);

            A.SolidFill solidFill56 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex56 = new A.RgbColorModelHex() { Val = "404040" };
            A.Alpha alpha56 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex56.Append(alpha56);

            solidFill56.Append(rgbColorModelHex56);

            shapeProperties56.Append(transform2D56);
            shapeProperties56.Append(customGeometry47);
            shapeProperties56.Append(solidFill56);

            Wps.ShapeStyle shapeStyle56 = new Wps.ShapeStyle();
            A.LineReference lineReference56 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference56 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference56 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference56 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle56.Append(lineReference56);
            shapeStyle56.Append(fillReference56);
            shapeStyle56.Append(effectReference56);
            shapeStyle56.Append(fontReference56);
            Wps.TextBodyProperties textBodyProperties56 = new Wps.TextBodyProperties();

            wordprocessingShape56.Append(nonVisualDrawingProperties47);
            wordprocessingShape56.Append(nonVisualDrawingShapeProperties56);
            wordprocessingShape56.Append(shapeProperties56);
            wordprocessingShape56.Append(shapeStyle56);
            wordprocessingShape56.Append(textBodyProperties56);
            //OpenXmlMiscNode openXmlMiscNode9 = new OpenXmlMiscNode();

            Wps.WordprocessingShape wordprocessingShape57 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties48 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)97U, Name = "Curve98" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties57 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties57 = new Wps.ShapeProperties();

            A.Transform2D transform2D57 = new A.Transform2D();
            A.Offset offset58 = new A.Offset() { X = 302166L, Y = 541426L };
            A.Extents extents58 = new A.Extents() { Cx = 46253L, Cy = 88274L };

            transform2D57.Append(offset58);
            transform2D57.Append(extents58);

            A.CustomGeometry customGeometry48 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList57 = new A.AdjustValueList();
            A.Rectangle rectangle48 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList48 = new A.PathList();

            A.Path path48 = new A.Path() { Width = 46253L, Height = 88274L };

            A.MoveTo moveTo51 = new A.MoveTo();
            A.Point point328 = new A.Point() { X = "46253", Y = "86339" };

            moveTo51.Append(point328);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo77 = new A.QuadraticBezierCurveTo();
            A.Point point329 = new A.Point() { X = "43049", Y = "87185" };
            A.Point point330 = new A.Point() { X = "39300", Y = "87730" };

            quadraticBezierCurveTo77.Append(point329);
            quadraticBezierCurveTo77.Append(point330);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo78 = new A.QuadraticBezierCurveTo();
            A.Point point331 = new A.Point() { X = "35551", Y = "88274" };
            A.Point point332 = new A.Point() { X = "32528", Y = "88274" };

            quadraticBezierCurveTo78.Append(point331);
            quadraticBezierCurveTo78.Append(point332);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo79 = new A.QuadraticBezierCurveTo();
            A.Point point333 = new A.Point() { X = "22189", Y = "88274" };
            A.Point point334 = new A.Point() { X = "16808", Y = "82711" };

            quadraticBezierCurveTo79.Append(point333);
            quadraticBezierCurveTo79.Append(point334);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo80 = new A.QuadraticBezierCurveTo();
            A.Point point335 = new A.Point() { X = "11427", Y = "77149" };
            A.Point point336 = new A.Point() { X = "11427", Y = "64875" };

            quadraticBezierCurveTo80.Append(point335);
            quadraticBezierCurveTo80.Append(point336);

            A.LineTo lineTo102 = new A.LineTo();
            A.Point point337 = new A.Point() { X = "11427", Y = "28961" };

            lineTo102.Append(point337);

            A.LineTo lineTo103 = new A.LineTo();
            A.Point point338 = new A.Point() { X = "3749", Y = "28961" };

            lineTo103.Append(point338);

            A.LineTo lineTo104 = new A.LineTo();
            A.Point point339 = new A.Point() { X = "3749", Y = "19408" };

            lineTo104.Append(point339);

            A.LineTo lineTo105 = new A.LineTo();
            A.Point point340 = new A.Point() { X = "11427", Y = "19408" };

            lineTo105.Append(point340);

            A.LineTo lineTo106 = new A.LineTo();
            A.Point point341 = new A.Point() { X = "11427", Y = "0" };

            lineTo106.Append(point341);

            A.LineTo lineTo107 = new A.LineTo();
            A.Point point342 = new A.Point() { X = "22794", Y = "0" };

            lineTo107.Append(point342);

            A.LineTo lineTo108 = new A.LineTo();
            A.Point point343 = new A.Point() { X = "22794", Y = "19408" };

            lineTo108.Append(point343);

            A.LineTo lineTo109 = new A.LineTo();
            A.Point point344 = new A.Point() { X = "46253", Y = "19408" };

            lineTo109.Append(point344);

            A.LineTo lineTo110 = new A.LineTo();
            A.Point point345 = new A.Point() { X = "46253", Y = "28961" };

            lineTo110.Append(point345);

            A.LineTo lineTo111 = new A.LineTo();
            A.Point point346 = new A.Point() { X = "22794", Y = "28961" };

            lineTo111.Append(point346);

            A.LineTo lineTo112 = new A.LineTo();
            A.Point point347 = new A.Point() { X = "22794", Y = "59736" };

            lineTo112.Append(point347);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo81 = new A.QuadraticBezierCurveTo();
            A.Point point348 = new A.Point() { X = "22794", Y = "65056" };
            A.Point point349 = new A.Point() { X = "23036", Y = "68019" };

            quadraticBezierCurveTo81.Append(point348);
            quadraticBezierCurveTo81.Append(point349);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo82 = new A.QuadraticBezierCurveTo();
            A.Point point350 = new A.Point() { X = "23278", Y = "70982" };
            A.Point point351 = new A.Point() { X = "24729", Y = "73642" };

            quadraticBezierCurveTo82.Append(point350);
            quadraticBezierCurveTo82.Append(point351);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo83 = new A.QuadraticBezierCurveTo();
            A.Point point352 = new A.Point() { X = "26059", Y = "76060" };
            A.Point point353 = new A.Point() { X = "28356", Y = "77149" };

            quadraticBezierCurveTo83.Append(point352);
            quadraticBezierCurveTo83.Append(point353);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo84 = new A.QuadraticBezierCurveTo();
            A.Point point354 = new A.Point() { X = "30654", Y = "78237" };
            A.Point point355 = new A.Point() { X = "35491", Y = "78298" };

            quadraticBezierCurveTo84.Append(point354);
            quadraticBezierCurveTo84.Append(point355);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo85 = new A.QuadraticBezierCurveTo();
            A.Point point356 = new A.Point() { X = "38272", Y = "78298" };
            A.Point point357 = new A.Point() { X = "41295", Y = "77512" };

            quadraticBezierCurveTo85.Append(point356);
            quadraticBezierCurveTo85.Append(point357);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo86 = new A.QuadraticBezierCurveTo();
            A.Point point358 = new A.Point() { X = "44318", Y = "76726" };
            A.Point point359 = new A.Point() { X = "45648", Y = "76121" };

            quadraticBezierCurveTo86.Append(point358);
            quadraticBezierCurveTo86.Append(point359);

            A.LineTo lineTo113 = new A.LineTo();
            A.Point point360 = new A.Point() { X = "46253", Y = "76121" };

            lineTo113.Append(point360);

            A.LineTo lineTo114 = new A.LineTo();
            A.Point point361 = new A.Point() { X = "46253", Y = "86339" };

            lineTo114.Append(point361);
            A.CloseShapePath closeShapePath12 = new A.CloseShapePath();

            path48.Append(moveTo51);
            path48.Append(quadraticBezierCurveTo77);
            path48.Append(quadraticBezierCurveTo78);
            path48.Append(quadraticBezierCurveTo79);
            path48.Append(quadraticBezierCurveTo80);
            path48.Append(lineTo102);
            path48.Append(lineTo103);
            path48.Append(lineTo104);
            path48.Append(lineTo105);
            path48.Append(lineTo106);
            path48.Append(lineTo107);
            path48.Append(lineTo108);
            path48.Append(lineTo109);
            path48.Append(lineTo110);
            path48.Append(lineTo111);
            path48.Append(lineTo112);
            path48.Append(quadraticBezierCurveTo81);
            path48.Append(quadraticBezierCurveTo82);
            path48.Append(quadraticBezierCurveTo83);
            path48.Append(quadraticBezierCurveTo84);
            path48.Append(quadraticBezierCurveTo85);
            path48.Append(quadraticBezierCurveTo86);
            path48.Append(lineTo113);
            path48.Append(lineTo114);
            path48.Append(closeShapePath12);

            pathList48.Append(path48);

            customGeometry48.Append(adjustValueList57);
            customGeometry48.Append(rectangle48);
            customGeometry48.Append(pathList48);

            A.SolidFill solidFill57 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex57 = new A.RgbColorModelHex() { Val = "404040" };
            A.Alpha alpha57 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex57.Append(alpha57);

            solidFill57.Append(rgbColorModelHex57);

            shapeProperties57.Append(transform2D57);
            shapeProperties57.Append(customGeometry48);
            shapeProperties57.Append(solidFill57);

            Wps.ShapeStyle shapeStyle57 = new Wps.ShapeStyle();
            A.LineReference lineReference57 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference57 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference57 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference57 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle57.Append(lineReference57);
            shapeStyle57.Append(fillReference57);
            shapeStyle57.Append(effectReference57);
            shapeStyle57.Append(fontReference57);
            Wps.TextBodyProperties textBodyProperties57 = new Wps.TextBodyProperties();

            wordprocessingShape57.Append(nonVisualDrawingProperties48);
            wordprocessingShape57.Append(nonVisualDrawingShapeProperties57);
            wordprocessingShape57.Append(shapeProperties57);
            wordprocessingShape57.Append(shapeStyle57);
            wordprocessingShape57.Append(textBodyProperties57);

            Wps.WordprocessingShape wordprocessingShape58 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties49 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)99U, Name = "Curve100" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties58 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties58 = new Wps.ShapeProperties();

            A.Transform2D transform2D58 = new A.Transform2D();
            A.Offset offset59 = new A.Offset() { X = 530952L, Y = 664607L };
            A.Extents extents59 = new A.Extents() { Cx = 35662L, Cy = 0L };

            transform2D58.Append(offset59);
            transform2D58.Append(extents59);

            A.CustomGeometry customGeometry49 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList58 = new A.AdjustValueList();
            A.Rectangle rectangle49 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList49 = new A.PathList();

            A.Path path49 = new A.Path() { Width = 35662L, Height = 0L };

            A.MoveTo moveTo52 = new A.MoveTo();
            A.Point point362 = new A.Point() { X = "0", Y = "0" };

            moveTo52.Append(point362);

            A.LineTo lineTo115 = new A.LineTo();
            A.Point point363 = new A.Point() { X = "35662", Y = "0" };

            lineTo115.Append(point363);

            path49.Append(moveTo52);
            path49.Append(lineTo115);

            pathList49.Append(path49);

            customGeometry49.Append(adjustValueList58);
            customGeometry49.Append(rectangle49);
            customGeometry49.Append(pathList49);

            A.Outline outline40 = new A.Outline() { Width = 9906, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill58 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex58 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha58 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex58.Append(alpha58);

            solidFill58.Append(rgbColorModelHex58);

            outline40.Append(solidFill58);

            shapeProperties58.Append(transform2D58);
            shapeProperties58.Append(customGeometry49);
            shapeProperties58.Append(outline40);

            Wps.ShapeStyle shapeStyle58 = new Wps.ShapeStyle();
            A.LineReference lineReference58 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference58 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference58 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference58 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle58.Append(lineReference58);
            shapeStyle58.Append(fillReference58);
            shapeStyle58.Append(effectReference58);
            shapeStyle58.Append(fontReference58);
            Wps.TextBodyProperties textBodyProperties58 = new Wps.TextBodyProperties();

            wordprocessingShape58.Append(nonVisualDrawingProperties49);
            wordprocessingShape58.Append(nonVisualDrawingShapeProperties58);
            wordprocessingShape58.Append(shapeProperties58);
            wordprocessingShape58.Append(shapeStyle58);
            wordprocessingShape58.Append(textBodyProperties58);

            Wps.WordprocessingShape wordprocessingShape59 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties50 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)101U, Name = "Curve102" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties59 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties59 = new Wps.ShapeProperties();

            A.Transform2D transform2D59 = new A.Transform2D();
            A.Offset offset60 = new A.Offset() { X = 548783L, Y = 646776L };
            A.Extents extents60 = new A.Extents() { Cx = 0L, Cy = 35662L };

            transform2D59.Append(offset60);
            transform2D59.Append(extents60);

            A.CustomGeometry customGeometry50 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList59 = new A.AdjustValueList();
            A.Rectangle rectangle50 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList50 = new A.PathList();

            A.Path path50 = new A.Path() { Width = 0L, Height = 35662L };

            A.MoveTo moveTo53 = new A.MoveTo();
            A.Point point364 = new A.Point() { X = "0", Y = "0" };

            moveTo53.Append(point364);

            A.LineTo lineTo116 = new A.LineTo();
            A.Point point365 = new A.Point() { X = "0", Y = "35662" };

            lineTo116.Append(point365);

            path50.Append(moveTo53);
            path50.Append(lineTo116);

            pathList50.Append(path50);

            customGeometry50.Append(adjustValueList59);
            customGeometry50.Append(rectangle50);
            customGeometry50.Append(pathList50);

            A.Outline outline41 = new A.Outline() { Width = 9906, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill59 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex59 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha59 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex59.Append(alpha59);

            solidFill59.Append(rgbColorModelHex59);

            outline41.Append(solidFill59);

            shapeProperties59.Append(transform2D59);
            shapeProperties59.Append(customGeometry50);
            shapeProperties59.Append(outline41);

            Wps.ShapeStyle shapeStyle59 = new Wps.ShapeStyle();
            A.LineReference lineReference59 = new A.LineReference() { Index = (UInt32Value)0U };
            A.FillReference fillReference59 = new A.FillReference() { Index = (UInt32Value)0U };
            A.EffectReference effectReference59 = new A.EffectReference() { Index = (UInt32Value)0U };
            A.FontReference fontReference59 = new A.FontReference() { Index = A.FontCollectionIndexValues.Minor };

            shapeStyle59.Append(lineReference59);
            shapeStyle59.Append(fillReference59);
            shapeStyle59.Append(effectReference59);
            shapeStyle59.Append(fontReference59);
            Wps.TextBodyProperties textBodyProperties59 = new Wps.TextBodyProperties();

            wordprocessingShape59.Append(nonVisualDrawingProperties50);
            wordprocessingShape59.Append(nonVisualDrawingShapeProperties59);
            wordprocessingShape59.Append(shapeProperties59);
            wordprocessingShape59.Append(shapeStyle59);
            wordprocessingShape59.Append(textBodyProperties59);

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
            wordprocessingGroup1.Append(wordprocessingShape11);
            wordprocessingGroup1.Append(wordprocessingShape12);
            wordprocessingGroup1.Append(wordprocessingShape13);
            wordprocessingGroup1.Append(wordprocessingShape14);
            wordprocessingGroup1.Append(wordprocessingShape15);
            wordprocessingGroup1.Append(wordprocessingShape16);
            wordprocessingGroup1.Append(wordprocessingShape17);
            wordprocessingGroup1.Append(wordprocessingShape18);
            wordprocessingGroup1.Append(wordprocessingShape19);
            wordprocessingGroup1.Append(wordprocessingShape20);
            wordprocessingGroup1.Append(wordprocessingShape21);
            wordprocessingGroup1.Append(wordprocessingShape22);
            wordprocessingGroup1.Append(wordprocessingShape23);
            wordprocessingGroup1.Append(wordprocessingShape24);
            wordprocessingGroup1.Append(wordprocessingShape25);
            wordprocessingGroup1.Append(wordprocessingShape26);
            wordprocessingGroup1.Append(wordprocessingShape27);
            wordprocessingGroup1.Append(wordprocessingShape28);
            wordprocessingGroup1.Append(wordprocessingShape29);
            wordprocessingGroup1.Append(wordprocessingShape30);
            wordprocessingGroup1.Append(wordprocessingShape31);
            wordprocessingGroup1.Append(wordprocessingShape32);
            wordprocessingGroup1.Append(wordprocessingShape33);
            wordprocessingGroup1.Append(wordprocessingShape34);
            wordprocessingGroup1.Append(wordprocessingShape35);
            wordprocessingGroup1.Append(wordprocessingShape36);
            wordprocessingGroup1.Append(wordprocessingShape37);
            wordprocessingGroup1.Append(wordprocessingShape38);
            wordprocessingGroup1.Append(wordprocessingShape39);
            wordprocessingGroup1.Append(wordprocessingShape40);
            wordprocessingGroup1.Append(wordprocessingShape41);
            wordprocessingGroup1.Append(wordprocessingShape42);
            wordprocessingGroup1.Append(wordprocessingShape43);
            wordprocessingGroup1.Append(wordprocessingShape44);
            wordprocessingGroup1.Append(wordprocessingShape45);
            wordprocessingGroup1.Append(wordprocessingShape46);
            wordprocessingGroup1.Append(wordprocessingShape47);
            wordprocessingGroup1.Append(wordprocessingShape48);
            //wordprocessingGroup1.Append(openXmlMiscNode1);
            wordprocessingGroup1.Append(wordprocessingShape49);
            //wordprocessingGroup1.Append(openXmlMiscNode2);
            wordprocessingGroup1.Append(wordprocessingShape50);
            //wordprocessingGroup1.Append(openXmlMiscNode3);
            wordprocessingGroup1.Append(wordprocessingShape51);
            //wordprocessingGroup1.Append(openXmlMiscNode4);
            wordprocessingGroup1.Append(wordprocessingShape52);
            //wordprocessingGroup1.Append(openXmlMiscNode5);
            wordprocessingGroup1.Append(wordprocessingShape53);
            //wordprocessingGroup1.Append(openXmlMiscNode6);
            wordprocessingGroup1.Append(wordprocessingShape54);
            //wordprocessingGroup1.Append(openXmlMiscNode7);
            wordprocessingGroup1.Append(wordprocessingShape55);
            //wordprocessingGroup1.Append(openXmlMiscNode8);
            wordprocessingGroup1.Append(wordprocessingShape56);
            //wordprocessingGroup1.Append(openXmlMiscNode9);
            wordprocessingGroup1.Append(wordprocessingShape57);
            wordprocessingGroup1.Append(wordprocessingShape58);
            wordprocessingGroup1.Append(wordprocessingShape59);

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
