using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Wpg = DocumentFormat.OpenXml.Office2010.Word.DrawingGroup;
using Wps = DocumentFormat.OpenXml.Office2010.Word.DrawingShape;

namespace Chem4Word.UI.Molecules
{
    public static class FormicAcid
    {
        // Creates an Run instance and adds its children.
        public static Run GenerateRun()
        {
            Run run1 = new Run();

            var drawing1 = new DocumentFormat.OpenXml.Wordprocessing.Drawing();

            A.Graphic graphic1 = new A.Graphic();
            graphic1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            #region Group2 (GraphicData1)

            A.GraphicData graphicData1 = new A.GraphicData() { Uri = "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" };

            Wp.Inline inline1 = new Wp.Inline() { DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)0U, DistanceFromRight = (UInt32Value)0U };
            Wp.Extent extent1 = new Wp.Extent() { Cx = 518266L, Cy = 405116L };
            Wp.EffectExtent effectExtent1 = new Wp.EffectExtent() { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L };
            Wp.DocProperties docProperties1 = new Wp.DocProperties() { Id = (UInt32Value)1U, Name = "Group2" };

            Wpg.WordprocessingGroup wordprocessingGroup1 = new Wpg.WordprocessingGroup();
            Wpg.NonVisualGroupDrawingShapeProperties nonVisualGroupDrawingShapeProperties1 = new Wpg.NonVisualGroupDrawingShapeProperties();

            Wpg.GroupShapeProperties groupShapeProperties1 = new Wpg.GroupShapeProperties();

            A.TransformGroup transformGroup1 = new A.TransformGroup();
            A.Offset offset1 = new A.Offset() { X = 0L, Y = 0L };
            A.Extents extents1 = new A.Extents() { Cx = 518266L, Cy = 405116L };
            A.ChildOffset childOffset1 = new A.ChildOffset() { X = 0L, Y = 0L };
            A.ChildExtents childExtents1 = new A.ChildExtents() { Cx = 518266L, Cy = 405116L };

            transformGroup1.Append(offset1);
            transformGroup1.Append(extents1);
            transformGroup1.Append(childOffset1);
            transformGroup1.Append(childExtents1);

            groupShapeProperties1.Append(transformGroup1);
            wordprocessingGroup1.Append(nonVisualGroupDrawingShapeProperties1);
            wordprocessingGroup1.Append(groupShapeProperties1);

            #endregion

            #region Curve4 (Shape1) - Bond Line

            Wps.WordprocessingShape wordprocessingShape1 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)3U, Name = "Curve4" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties1 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties1 = new Wps.ShapeProperties();

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset2 = new A.Offset() { X = 91957L, Y = 244516L };
            A.Extents extents2 = new A.Extents() { Cx = 106854L, Cy = 61689L };

            transform2D1.Append(offset2);
            transform2D1.Append(extents2);

            A.CustomGeometry customGeometry1 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();
            A.Rectangle rectangle1 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList1 = new A.PathList();

            A.Path path1 = new A.Path() { Width = 106854L, Height = 61689L };

            A.MoveTo moveTo1 = new A.MoveTo();
            A.Point point1 = new A.Point() { X = "106854", Y = "0" };

            moveTo1.Append(point1);

            A.LineTo lineTo1 = new A.LineTo();
            A.Point point2 = new A.Point() { X = "0", Y = "61689" };

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

            #endregion

            #region Curve6 (Shape2) - Bond Line

            Wps.WordprocessingShape wordprocessingShape2 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties2 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)5U, Name = "Curve6" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties2 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties2 = new Wps.ShapeProperties();

            A.Transform2D transform2D2 = new A.Transform2D();
            A.Offset offset3 = new A.Offset() { X = 224211L, Y = 244516L };
            A.Extents extents3 = new A.Extents() { Cx = 105377L, Cy = 60841L };

            transform2D2.Append(offset3);
            transform2D2.Append(extents3);

            A.CustomGeometry customGeometry2 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList2 = new A.AdjustValueList();
            A.Rectangle rectangle2 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList2 = new A.PathList();

            A.Path path2 = new A.Path() { Width = 105377L, Height = 60841L };

            A.MoveTo moveTo2 = new A.MoveTo();
            A.Point point3 = new A.Point() { X = "0", Y = "0" };

            moveTo2.Append(point3);

            A.LineTo lineTo2 = new A.LineTo();
            A.Point point4 = new A.Point() { X = "105377", Y = "60841" };

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

            #endregion

            #region Curve8 (Shape3) - Bond Line

            Wps.WordprocessingShape wordprocessingShape3 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties3 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)7U, Name = "Curve8" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties3 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties3 = new Wps.ShapeProperties();

            A.Transform2D transform2D3 = new A.Transform2D();
            A.Offset offset4 = new A.Offset() { X = 224211L, Y = 110504L };
            A.Extents extents4 = new A.Extents() { Cx = 0L, Cy = 134012L };

            transform2D3.Append(offset4);
            transform2D3.Append(extents4);

            A.CustomGeometry customGeometry3 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList3 = new A.AdjustValueList();
            A.Rectangle rectangle3 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList3 = new A.PathList();

            A.Path path3 = new A.Path() { Width = 0L, Height = 134012L };

            A.MoveTo moveTo3 = new A.MoveTo();
            A.Point point5 = new A.Point() { X = "0", Y = "134012" };

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

            #endregion

            #region Curve10 (Shape4) - Bond Line

            Wps.WordprocessingShape wordprocessingShape4 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties4 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)9U, Name = "Curve10" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties4 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties4 = new Wps.ShapeProperties();

            A.Transform2D transform2D4 = new A.Transform2D();
            A.Offset offset5 = new A.Offset() { X = 198811L, Y = 110504L };
            A.Extents extents5 = new A.Extents() { Cx = 0L, Cy = 134012L };

            transform2D4.Append(offset5);
            transform2D4.Append(extents5);

            A.CustomGeometry customGeometry4 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList4 = new A.AdjustValueList();
            A.Rectangle rectangle4 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList4 = new A.PathList();

            A.Path path4 = new A.Path() { Width = 0L, Height = 134012L };

            A.MoveTo moveTo4 = new A.MoveTo();
            A.Point point7 = new A.Point() { X = "0", Y = "134012" };

            moveTo4.Append(point7);

            A.LineTo lineTo4 = new A.LineTo();
            A.Point point8 = new A.Point() { X = "0", Y = "0" };

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

            #endregion

            #region Curve12 (Shape5) - Letter H (Grey)

            Wps.WordprocessingShape wordprocessingShape5 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties5 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)11U, Name = "Curve12" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties5 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties5 = new Wps.ShapeProperties();

            A.Transform2D transform2D5 = new A.Transform2D();
            A.Offset offset6 = new A.Offset() { X = 0L, Y = 289091L };
            A.Extents extents6 = new A.Extents() { Cx = 80958L, Cy = 90027L };

            transform2D5.Append(offset6);
            transform2D5.Append(extents6);

            A.CustomGeometry customGeometry5 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList5 = new A.AdjustValueList();
            A.Rectangle rectangle5 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList5 = new A.PathList();

            A.Path path5 = new A.Path() { Width = 80958L, Height = 90027L };

            A.MoveTo moveTo5 = new A.MoveTo();
            A.Point point9 = new A.Point() { X = "80958", Y = "90027" };

            moveTo5.Append(point9);

            A.LineTo lineTo5 = new A.LineTo();
            A.Point point10 = new A.Point() { X = "68986", Y = "90027" };

            lineTo5.Append(point10);

            A.LineTo lineTo6 = new A.LineTo();
            A.Point point11 = new A.Point() { X = "68986", Y = "45951" };

            lineTo6.Append(point11);

            A.LineTo lineTo7 = new A.LineTo();
            A.Point point12 = new A.Point() { X = "24064", Y = "45951" };

            lineTo7.Append(point12);

            A.LineTo lineTo8 = new A.LineTo();
            A.Point point13 = new A.Point() { X = "24064", Y = "90027" };

            lineTo8.Append(point13);

            A.LineTo lineTo9 = new A.LineTo();
            A.Point point14 = new A.Point() { X = "12092", Y = "90027" };

            lineTo9.Append(point14);

            A.LineTo lineTo10 = new A.LineTo();
            A.Point point15 = new A.Point() { X = "12092", Y = "0" };

            lineTo10.Append(point15);

            A.LineTo lineTo11 = new A.LineTo();
            A.Point point16 = new A.Point() { X = "24064", Y = "0" };

            lineTo11.Append(point16);

            A.LineTo lineTo12 = new A.LineTo();
            A.Point point17 = new A.Point() { X = "24064", Y = "35309" };

            lineTo12.Append(point17);

            A.LineTo lineTo13 = new A.LineTo();
            A.Point point18 = new A.Point() { X = "68986", Y = "35309" };

            lineTo13.Append(point18);

            A.LineTo lineTo14 = new A.LineTo();
            A.Point point19 = new A.Point() { X = "68986", Y = "0" };

            lineTo14.Append(point19);

            A.LineTo lineTo15 = new A.LineTo();
            A.Point point20 = new A.Point() { X = "80958", Y = "0" };

            lineTo15.Append(point20);

            A.LineTo lineTo16 = new A.LineTo();
            A.Point point21 = new A.Point() { X = "80958", Y = "90027" };

            lineTo16.Append(point21);
            A.CloseShapePath closeShapePath1 = new A.CloseShapePath();

            path5.Append(moveTo5);
            path5.Append(lineTo5);
            path5.Append(lineTo6);
            path5.Append(lineTo7);
            path5.Append(lineTo8);
            path5.Append(lineTo9);
            path5.Append(lineTo10);
            path5.Append(lineTo11);
            path5.Append(lineTo12);
            path5.Append(lineTo13);
            path5.Append(lineTo14);
            path5.Append(lineTo15);
            path5.Append(lineTo16);
            path5.Append(closeShapePath1);

            pathList5.Append(path5);

            customGeometry5.Append(adjustValueList5);
            customGeometry5.Append(rectangle5);
            customGeometry5.Append(pathList5);

            A.SolidFill solidFill5 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex() { Val = "808080" };
            A.Alpha alpha5 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex5.Append(alpha5);

            solidFill5.Append(rgbColorModelHex5);

            shapeProperties5.Append(transform2D5);
            shapeProperties5.Append(customGeometry5);
            shapeProperties5.Append(solidFill5);

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

            #endregion

            #region Curve14 (Shape6) - Letter O (Red)

            Wps.WordprocessingShape wordprocessingShape6 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties6 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)13U, Name = "Curve14" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties6 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties6 = new Wps.ShapeProperties();

            A.Transform2D transform2D6 = new A.Transform2D();
            A.Offset offset7 = new A.Offset() { X = 327752L, Y = 287216L };
            A.Extents extents7 = new A.Extents() { Cx = 90571L, Cy = 93776L };

            transform2D6.Append(offset7);
            transform2D6.Append(extents7);

            A.CustomGeometry customGeometry6 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList6 = new A.AdjustValueList();
            A.Rectangle rectangle6 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList6 = new A.PathList();

            A.Path path6 = new A.Path() { Width = 90571L, Height = 93776L };

            A.MoveTo moveTo6 = new A.MoveTo();
            A.Point point22 = new A.Point() { X = "79204", Y = "12213" };

            moveTo6.Append(point22);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo1 = new A.QuadraticBezierCurveTo();
            A.Point point23 = new A.Point() { X = "84706", Y = "18259" };
            A.Point point24 = new A.Point() { X = "87609", Y = "27026" };

            quadraticBezierCurveTo1.Append(point23);
            quadraticBezierCurveTo1.Append(point24);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo2 = new A.QuadraticBezierCurveTo();
            A.Point point25 = new A.Point() { X = "90511", Y = "35793" };
            A.Point point26 = new A.Point() { X = "90571", Y = "46918" };

            quadraticBezierCurveTo2.Append(point25);
            quadraticBezierCurveTo2.Append(point26);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo3 = new A.QuadraticBezierCurveTo();
            A.Point point27 = new A.Point() { X = "90571", Y = "58043" };
            A.Point point28 = new A.Point() { X = "87609", Y = "66810" };

            quadraticBezierCurveTo3.Append(point27);
            quadraticBezierCurveTo3.Append(point28);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo4 = new A.QuadraticBezierCurveTo();
            A.Point point29 = new A.Point() { X = "84646", Y = "75577" };
            A.Point point30 = new A.Point() { X = "79204", Y = "81502" };

            quadraticBezierCurveTo4.Append(point29);
            quadraticBezierCurveTo4.Append(point30);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo5 = new A.QuadraticBezierCurveTo();
            A.Point point31 = new A.Point() { X = "73642", Y = "87609" };
            A.Point point32 = new A.Point() { X = "66084", Y = "90692" };

            quadraticBezierCurveTo5.Append(point31);
            quadraticBezierCurveTo5.Append(point32);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo6 = new A.QuadraticBezierCurveTo();
            A.Point point33 = new A.Point() { X = "58527", Y = "93776" };
            A.Point point34 = new A.Point() { X = "48732", Y = "93776" };

            quadraticBezierCurveTo6.Append(point33);
            quadraticBezierCurveTo6.Append(point34);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo7 = new A.QuadraticBezierCurveTo();
            A.Point point35 = new A.Point() { X = "39239", Y = "93776" };
            A.Point point36 = new A.Point() { X = "31500", Y = "90632" };

            quadraticBezierCurveTo7.Append(point35);
            quadraticBezierCurveTo7.Append(point36);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo8 = new A.QuadraticBezierCurveTo();
            A.Point point37 = new A.Point() { X = "23761", Y = "87488" };
            A.Point point38 = new A.Point() { X = "18259", Y = "81502" };

            quadraticBezierCurveTo8.Append(point37);
            quadraticBezierCurveTo8.Append(point38);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo9 = new A.QuadraticBezierCurveTo();
            A.Point point39 = new A.Point() { X = "12818", Y = "75516" };
            A.Point point40 = new A.Point() { X = "9916", Y = "66810" };

            quadraticBezierCurveTo9.Append(point39);
            quadraticBezierCurveTo9.Append(point40);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo10 = new A.QuadraticBezierCurveTo();
            A.Point point41 = new A.Point() { X = "7014", Y = "58103" };
            A.Point point42 = new A.Point() { X = "6953", Y = "46918" };

            quadraticBezierCurveTo10.Append(point41);
            quadraticBezierCurveTo10.Append(point42);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo11 = new A.QuadraticBezierCurveTo();
            A.Point point43 = new A.Point() { X = "6953", Y = "35914" };
            A.Point point44 = new A.Point() { X = "9855", Y = "27208" };

            quadraticBezierCurveTo11.Append(point43);
            quadraticBezierCurveTo11.Append(point44);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo12 = new A.QuadraticBezierCurveTo();
            A.Point point45 = new A.Point() { X = "12757", Y = "18501" };
            A.Point point46 = new A.Point() { X = "18320", Y = "12213" };

            quadraticBezierCurveTo12.Append(point45);
            quadraticBezierCurveTo12.Append(point46);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo13 = new A.QuadraticBezierCurveTo();
            A.Point point47 = new A.Point() { X = "23640", Y = "6288" };
            A.Point point48 = new A.Point() { X = "31500", Y = "3144" };

            quadraticBezierCurveTo13.Append(point47);
            quadraticBezierCurveTo13.Append(point48);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo14 = new A.QuadraticBezierCurveTo();
            A.Point point49 = new A.Point() { X = "39360", Y = "0" };
            A.Point point50 = new A.Point() { X = "48732", Y = "0" };

            quadraticBezierCurveTo14.Append(point49);
            quadraticBezierCurveTo14.Append(point50);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo15 = new A.QuadraticBezierCurveTo();
            A.Point point51 = new A.Point() { X = "58406", Y = "0" };
            A.Point point52 = new A.Point() { X = "66084", Y = "3144" };

            quadraticBezierCurveTo15.Append(point51);
            quadraticBezierCurveTo15.Append(point52);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo16 = new A.QuadraticBezierCurveTo();
            A.Point point53 = new A.Point() { X = "73763", Y = "6288" };
            A.Point point54 = new A.Point() { X = "79204", Y = "12213" };

            quadraticBezierCurveTo16.Append(point53);
            quadraticBezierCurveTo16.Append(point54);
            A.CloseShapePath closeShapePath2 = new A.CloseShapePath();

            A.MoveTo moveTo7 = new A.MoveTo();
            A.Point point55 = new A.Point() { X = "78116", Y = "46918" };

            moveTo7.Append(point55);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo17 = new A.QuadraticBezierCurveTo();
            A.Point point56 = new A.Point() { X = "78116", Y = "29384" };
            A.Point point57 = new A.Point() { X = "70256", Y = "19892" };

            quadraticBezierCurveTo17.Append(point56);
            quadraticBezierCurveTo17.Append(point57);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo18 = new A.QuadraticBezierCurveTo();
            A.Point point58 = new A.Point() { X = "62396", Y = "10399" };
            A.Point point59 = new A.Point() { X = "48792", Y = "10339" };

            quadraticBezierCurveTo18.Append(point58);
            quadraticBezierCurveTo18.Append(point59);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo19 = new A.QuadraticBezierCurveTo();
            A.Point point60 = new A.Point() { X = "35068", Y = "10339" };
            A.Point point61 = new A.Point() { X = "27268", Y = "19831" };

            quadraticBezierCurveTo19.Append(point60);
            quadraticBezierCurveTo19.Append(point61);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo20 = new A.QuadraticBezierCurveTo();
            A.Point point62 = new A.Point() { X = "19469", Y = "29324" };
            A.Point point63 = new A.Point() { X = "19408", Y = "46918" };

            quadraticBezierCurveTo20.Append(point62);
            quadraticBezierCurveTo20.Append(point63);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo21 = new A.QuadraticBezierCurveTo();
            A.Point point64 = new A.Point() { X = "19408", Y = "64633" };
            A.Point point65 = new A.Point() { X = "27389", Y = "74005" };

            quadraticBezierCurveTo21.Append(point64);
            quadraticBezierCurveTo21.Append(point65);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo22 = new A.QuadraticBezierCurveTo();
            A.Point point66 = new A.Point() { X = "35370", Y = "83376" };
            A.Point point67 = new A.Point() { X = "48792", Y = "83437" };

            quadraticBezierCurveTo22.Append(point66);
            quadraticBezierCurveTo22.Append(point67);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo23 = new A.QuadraticBezierCurveTo();
            A.Point point68 = new A.Point() { X = "62215", Y = "83437" };
            A.Point point69 = new A.Point() { X = "70135", Y = "74065" };

            quadraticBezierCurveTo23.Append(point68);
            quadraticBezierCurveTo23.Append(point69);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo24 = new A.QuadraticBezierCurveTo();
            A.Point point70 = new A.Point() { X = "78056", Y = "64694" };
            A.Point point71 = new A.Point() { X = "78116", Y = "46918" };

            quadraticBezierCurveTo24.Append(point70);
            quadraticBezierCurveTo24.Append(point71);
            A.CloseShapePath closeShapePath3 = new A.CloseShapePath();

            path6.Append(moveTo6);
            path6.Append(quadraticBezierCurveTo1);
            path6.Append(quadraticBezierCurveTo2);
            path6.Append(quadraticBezierCurveTo3);
            path6.Append(quadraticBezierCurveTo4);
            path6.Append(quadraticBezierCurveTo5);
            path6.Append(quadraticBezierCurveTo6);
            path6.Append(quadraticBezierCurveTo7);
            path6.Append(quadraticBezierCurveTo8);
            path6.Append(quadraticBezierCurveTo9);
            path6.Append(quadraticBezierCurveTo10);
            path6.Append(quadraticBezierCurveTo11);
            path6.Append(quadraticBezierCurveTo12);
            path6.Append(quadraticBezierCurveTo13);
            path6.Append(quadraticBezierCurveTo14);
            path6.Append(quadraticBezierCurveTo15);
            path6.Append(quadraticBezierCurveTo16);
            path6.Append(closeShapePath2);
            path6.Append(moveTo7);
            path6.Append(quadraticBezierCurveTo17);
            path6.Append(quadraticBezierCurveTo18);
            path6.Append(quadraticBezierCurveTo19);
            path6.Append(quadraticBezierCurveTo20);
            path6.Append(quadraticBezierCurveTo21);
            path6.Append(quadraticBezierCurveTo22);
            path6.Append(quadraticBezierCurveTo23);
            path6.Append(quadraticBezierCurveTo24);
            path6.Append(closeShapePath3);

            pathList6.Append(path6);

            customGeometry6.Append(adjustValueList6);
            customGeometry6.Append(rectangle6);
            customGeometry6.Append(pathList6);

            A.SolidFill solidFill6 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex() { Val = "ff0000" };
            A.Alpha alpha6 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex6.Append(alpha6);

            solidFill6.Append(rgbColorModelHex6);

            shapeProperties6.Append(transform2D6);
            shapeProperties6.Append(customGeometry6);
            shapeProperties6.Append(solidFill6);

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

            #endregion

            #region Curve16 (Shape7) - Letter O (Red)

            Wps.WordprocessingShape wordprocessingShape7 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties7 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)15U, Name = "Curve16" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties7 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties7 = new Wps.ShapeProperties();

            A.Transform2D transform2D7 = new A.Transform2D();
            A.Offset offset8 = new A.Offset() { X = 162779L, Y = 1466L };
            A.Extents extents8 = new A.Extents() { Cx = 90571L, Cy = 93776L };

            transform2D7.Append(offset8);
            transform2D7.Append(extents8);

            A.CustomGeometry customGeometry7 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList7 = new A.AdjustValueList();
            A.Rectangle rectangle7 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList7 = new A.PathList();

            A.Path path7 = new A.Path() { Width = 90571L, Height = 93776L };

            A.MoveTo moveTo8 = new A.MoveTo();
            A.Point point72 = new A.Point() { X = "79204", Y = "12213" };

            moveTo8.Append(point72);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo25 = new A.QuadraticBezierCurveTo();
            A.Point point73 = new A.Point() { X = "84706", Y = "18259" };
            A.Point point74 = new A.Point() { X = "87609", Y = "27026" };

            quadraticBezierCurveTo25.Append(point73);
            quadraticBezierCurveTo25.Append(point74);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo26 = new A.QuadraticBezierCurveTo();
            A.Point point75 = new A.Point() { X = "90511", Y = "35793" };
            A.Point point76 = new A.Point() { X = "90571", Y = "46918" };

            quadraticBezierCurveTo26.Append(point75);
            quadraticBezierCurveTo26.Append(point76);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo27 = new A.QuadraticBezierCurveTo();
            A.Point point77 = new A.Point() { X = "90571", Y = "58043" };
            A.Point point78 = new A.Point() { X = "87609", Y = "66810" };

            quadraticBezierCurveTo27.Append(point77);
            quadraticBezierCurveTo27.Append(point78);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo28 = new A.QuadraticBezierCurveTo();
            A.Point point79 = new A.Point() { X = "84646", Y = "75577" };
            A.Point point80 = new A.Point() { X = "79204", Y = "81502" };

            quadraticBezierCurveTo28.Append(point79);
            quadraticBezierCurveTo28.Append(point80);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo29 = new A.QuadraticBezierCurveTo();
            A.Point point81 = new A.Point() { X = "73642", Y = "87609" };
            A.Point point82 = new A.Point() { X = "66084", Y = "90692" };

            quadraticBezierCurveTo29.Append(point81);
            quadraticBezierCurveTo29.Append(point82);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo30 = new A.QuadraticBezierCurveTo();
            A.Point point83 = new A.Point() { X = "58527", Y = "93776" };
            A.Point point84 = new A.Point() { X = "48732", Y = "93776" };

            quadraticBezierCurveTo30.Append(point83);
            quadraticBezierCurveTo30.Append(point84);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo31 = new A.QuadraticBezierCurveTo();
            A.Point point85 = new A.Point() { X = "39239", Y = "93776" };
            A.Point point86 = new A.Point() { X = "31500", Y = "90632" };

            quadraticBezierCurveTo31.Append(point85);
            quadraticBezierCurveTo31.Append(point86);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo32 = new A.QuadraticBezierCurveTo();
            A.Point point87 = new A.Point() { X = "23761", Y = "87488" };
            A.Point point88 = new A.Point() { X = "18259", Y = "81502" };

            quadraticBezierCurveTo32.Append(point87);
            quadraticBezierCurveTo32.Append(point88);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo33 = new A.QuadraticBezierCurveTo();
            A.Point point89 = new A.Point() { X = "12818", Y = "75516" };
            A.Point point90 = new A.Point() { X = "9916", Y = "66810" };

            quadraticBezierCurveTo33.Append(point89);
            quadraticBezierCurveTo33.Append(point90);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo34 = new A.QuadraticBezierCurveTo();
            A.Point point91 = new A.Point() { X = "7014", Y = "58103" };
            A.Point point92 = new A.Point() { X = "6953", Y = "46918" };

            quadraticBezierCurveTo34.Append(point91);
            quadraticBezierCurveTo34.Append(point92);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo35 = new A.QuadraticBezierCurveTo();
            A.Point point93 = new A.Point() { X = "6953", Y = "35914" };
            A.Point point94 = new A.Point() { X = "9855", Y = "27208" };

            quadraticBezierCurveTo35.Append(point93);
            quadraticBezierCurveTo35.Append(point94);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo36 = new A.QuadraticBezierCurveTo();
            A.Point point95 = new A.Point() { X = "12757", Y = "18501" };
            A.Point point96 = new A.Point() { X = "18320", Y = "12213" };

            quadraticBezierCurveTo36.Append(point95);
            quadraticBezierCurveTo36.Append(point96);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo37 = new A.QuadraticBezierCurveTo();
            A.Point point97 = new A.Point() { X = "23640", Y = "6288" };
            A.Point point98 = new A.Point() { X = "31500", Y = "3144" };

            quadraticBezierCurveTo37.Append(point97);
            quadraticBezierCurveTo37.Append(point98);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo38 = new A.QuadraticBezierCurveTo();
            A.Point point99 = new A.Point() { X = "39360", Y = "0" };
            A.Point point100 = new A.Point() { X = "48732", Y = "0" };

            quadraticBezierCurveTo38.Append(point99);
            quadraticBezierCurveTo38.Append(point100);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo39 = new A.QuadraticBezierCurveTo();
            A.Point point101 = new A.Point() { X = "58406", Y = "0" };
            A.Point point102 = new A.Point() { X = "66084", Y = "3144" };

            quadraticBezierCurveTo39.Append(point101);
            quadraticBezierCurveTo39.Append(point102);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo40 = new A.QuadraticBezierCurveTo();
            A.Point point103 = new A.Point() { X = "73763", Y = "6288" };
            A.Point point104 = new A.Point() { X = "79204", Y = "12213" };

            quadraticBezierCurveTo40.Append(point103);
            quadraticBezierCurveTo40.Append(point104);
            A.CloseShapePath closeShapePath4 = new A.CloseShapePath();

            A.MoveTo moveTo9 = new A.MoveTo();
            A.Point point105 = new A.Point() { X = "78116", Y = "46918" };

            moveTo9.Append(point105);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo41 = new A.QuadraticBezierCurveTo();
            A.Point point106 = new A.Point() { X = "78116", Y = "29384" };
            A.Point point107 = new A.Point() { X = "70256", Y = "19892" };

            quadraticBezierCurveTo41.Append(point106);
            quadraticBezierCurveTo41.Append(point107);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo42 = new A.QuadraticBezierCurveTo();
            A.Point point108 = new A.Point() { X = "62396", Y = "10399" };
            A.Point point109 = new A.Point() { X = "48792", Y = "10339" };

            quadraticBezierCurveTo42.Append(point108);
            quadraticBezierCurveTo42.Append(point109);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo43 = new A.QuadraticBezierCurveTo();
            A.Point point110 = new A.Point() { X = "35068", Y = "10339" };
            A.Point point111 = new A.Point() { X = "27268", Y = "19831" };

            quadraticBezierCurveTo43.Append(point110);
            quadraticBezierCurveTo43.Append(point111);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo44 = new A.QuadraticBezierCurveTo();
            A.Point point112 = new A.Point() { X = "19469", Y = "29324" };
            A.Point point113 = new A.Point() { X = "19408", Y = "46918" };

            quadraticBezierCurveTo44.Append(point112);
            quadraticBezierCurveTo44.Append(point113);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo45 = new A.QuadraticBezierCurveTo();
            A.Point point114 = new A.Point() { X = "19408", Y = "64633" };
            A.Point point115 = new A.Point() { X = "27389", Y = "74005" };

            quadraticBezierCurveTo45.Append(point114);
            quadraticBezierCurveTo45.Append(point115);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo46 = new A.QuadraticBezierCurveTo();
            A.Point point116 = new A.Point() { X = "35370", Y = "83376" };
            A.Point point117 = new A.Point() { X = "48792", Y = "83437" };

            quadraticBezierCurveTo46.Append(point116);
            quadraticBezierCurveTo46.Append(point117);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo47 = new A.QuadraticBezierCurveTo();
            A.Point point118 = new A.Point() { X = "62215", Y = "83437" };
            A.Point point119 = new A.Point() { X = "70135", Y = "74065" };

            quadraticBezierCurveTo47.Append(point118);
            quadraticBezierCurveTo47.Append(point119);

            A.QuadraticBezierCurveTo quadraticBezierCurveTo48 = new A.QuadraticBezierCurveTo();
            A.Point point120 = new A.Point() { X = "78056", Y = "64694" };
            A.Point point121 = new A.Point() { X = "78116", Y = "46918" };

            quadraticBezierCurveTo48.Append(point120);
            quadraticBezierCurveTo48.Append(point121);
            A.CloseShapePath closeShapePath5 = new A.CloseShapePath();

            path7.Append(moveTo8);
            path7.Append(quadraticBezierCurveTo25);
            path7.Append(quadraticBezierCurveTo26);
            path7.Append(quadraticBezierCurveTo27);
            path7.Append(quadraticBezierCurveTo28);
            path7.Append(quadraticBezierCurveTo29);
            path7.Append(quadraticBezierCurveTo30);
            path7.Append(quadraticBezierCurveTo31);
            path7.Append(quadraticBezierCurveTo32);
            path7.Append(quadraticBezierCurveTo33);
            path7.Append(quadraticBezierCurveTo34);
            path7.Append(quadraticBezierCurveTo35);
            path7.Append(quadraticBezierCurveTo36);
            path7.Append(quadraticBezierCurveTo37);
            path7.Append(quadraticBezierCurveTo38);
            path7.Append(quadraticBezierCurveTo39);
            path7.Append(quadraticBezierCurveTo40);
            path7.Append(closeShapePath4);
            path7.Append(moveTo9);
            path7.Append(quadraticBezierCurveTo41);
            path7.Append(quadraticBezierCurveTo42);
            path7.Append(quadraticBezierCurveTo43);
            path7.Append(quadraticBezierCurveTo44);
            path7.Append(quadraticBezierCurveTo45);
            path7.Append(quadraticBezierCurveTo46);
            path7.Append(quadraticBezierCurveTo47);
            path7.Append(quadraticBezierCurveTo48);
            path7.Append(closeShapePath5);

            pathList7.Append(path7);

            customGeometry7.Append(adjustValueList7);
            customGeometry7.Append(rectangle7);
            customGeometry7.Append(pathList7);

            A.SolidFill solidFill7 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex() { Val = "ff0000" };
            A.Alpha alpha7 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex7.Append(alpha7);

            solidFill7.Append(rgbColorModelHex7);

            shapeProperties7.Append(transform2D7);
            shapeProperties7.Append(customGeometry7);
            shapeProperties7.Append(solidFill7);

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

            #endregion

            #region Curve18 (Shape8) - Letter H (Red)

            Wps.WordprocessingShape wordprocessingShape8 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties8 = new Wps.NonVisualDrawingProperties() { Id = (UInt32Value)17U, Name = "Curve18" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties8 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties8 = new Wps.ShapeProperties();

            A.Transform2D transform2D8 = new A.Transform2D();
            A.Offset offset9 = new A.Offset() { X = 425216L, Y = 289091L };
            A.Extents extents9 = new A.Extents() { Cx = 80958L, Cy = 90027L };

            transform2D8.Append(offset9);
            transform2D8.Append(extents9);

            A.CustomGeometry customGeometry8 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList8 = new A.AdjustValueList();
            A.Rectangle rectangle8 = new A.Rectangle() { Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList8 = new A.PathList();

            A.Path path8 = new A.Path() { Width = 80958L, Height = 90027L };

            A.MoveTo moveTo10 = new A.MoveTo();
            A.Point point122 = new A.Point() { X = "80958", Y = "90027" };

            moveTo10.Append(point122);

            A.LineTo lineTo17 = new A.LineTo();
            A.Point point123 = new A.Point() { X = "68986", Y = "90027" };

            lineTo17.Append(point123);

            A.LineTo lineTo18 = new A.LineTo();
            A.Point point124 = new A.Point() { X = "68986", Y = "45951" };

            lineTo18.Append(point124);

            A.LineTo lineTo19 = new A.LineTo();
            A.Point point125 = new A.Point() { X = "24064", Y = "45951" };

            lineTo19.Append(point125);

            A.LineTo lineTo20 = new A.LineTo();
            A.Point point126 = new A.Point() { X = "24064", Y = "90027" };

            lineTo20.Append(point126);

            A.LineTo lineTo21 = new A.LineTo();
            A.Point point127 = new A.Point() { X = "12092", Y = "90027" };

            lineTo21.Append(point127);

            A.LineTo lineTo22 = new A.LineTo();
            A.Point point128 = new A.Point() { X = "12092", Y = "0" };

            lineTo22.Append(point128);

            A.LineTo lineTo23 = new A.LineTo();
            A.Point point129 = new A.Point() { X = "24064", Y = "0" };

            lineTo23.Append(point129);

            A.LineTo lineTo24 = new A.LineTo();
            A.Point point130 = new A.Point() { X = "24064", Y = "35309" };

            lineTo24.Append(point130);

            A.LineTo lineTo25 = new A.LineTo();
            A.Point point131 = new A.Point() { X = "68986", Y = "35309" };

            lineTo25.Append(point131);

            A.LineTo lineTo26 = new A.LineTo();
            A.Point point132 = new A.Point() { X = "68986", Y = "0" };

            lineTo26.Append(point132);

            A.LineTo lineTo27 = new A.LineTo();
            A.Point point133 = new A.Point() { X = "80958", Y = "0" };

            lineTo27.Append(point133);

            A.LineTo lineTo28 = new A.LineTo();
            A.Point point134 = new A.Point() { X = "80958", Y = "90027" };

            lineTo28.Append(point134);
            A.CloseShapePath closeShapePath6 = new A.CloseShapePath();

            path8.Append(moveTo10);
            path8.Append(lineTo17);
            path8.Append(lineTo18);
            path8.Append(lineTo19);
            path8.Append(lineTo20);
            path8.Append(lineTo21);
            path8.Append(lineTo22);
            path8.Append(lineTo23);
            path8.Append(lineTo24);
            path8.Append(lineTo25);
            path8.Append(lineTo26);
            path8.Append(lineTo27);
            path8.Append(lineTo28);
            path8.Append(closeShapePath6);

            pathList8.Append(path8);

            customGeometry8.Append(adjustValueList8);
            customGeometry8.Append(rectangle8);
            customGeometry8.Append(pathList8);

            A.SolidFill solidFill8 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "ff0000" };
            A.Alpha alpha8 = new A.Alpha() { Val = new Int32Value() { InnerText = "100%" } };

            rgbColorModelHex8.Append(alpha8);

            solidFill8.Append(rgbColorModelHex8);

            shapeProperties8.Append(transform2D8);
            shapeProperties8.Append(customGeometry8);
            shapeProperties8.Append(solidFill8);

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

            #endregion

            wordprocessingGroup1.Append(wordprocessingShape1);
            wordprocessingGroup1.Append(wordprocessingShape2);
            wordprocessingGroup1.Append(wordprocessingShape3);
            wordprocessingGroup1.Append(wordprocessingShape4);
            wordprocessingGroup1.Append(wordprocessingShape5);
            wordprocessingGroup1.Append(wordprocessingShape6);
            wordprocessingGroup1.Append(wordprocessingShape7);
            wordprocessingGroup1.Append(wordprocessingShape8);

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