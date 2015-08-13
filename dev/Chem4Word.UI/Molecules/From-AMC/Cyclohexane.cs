using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Wpg = DocumentFormat.OpenXml.Office2010.Word.DrawingGroup;
using Wps = DocumentFormat.OpenXml.Office2010.Word.DrawingShape;

namespace Chem4Word.UI.Molecules
{
    public static class Cyclohexane
    {
        // Creates an Run instance and adds its children.
        public static Run GenerateRun()
        {
            Run run1 = new Run();

            var drawing1 = new DocumentFormat.OpenXml.Wordprocessing.Drawing();

            Wp.Inline inline1 = new Wp.Inline(){ DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)0U, DistanceFromRight = (UInt32Value)0U };
            Wp.Extent extent1 = new Wp.Extent(){ Cx = 349009L, Cy = 400050L };
            Wp.EffectExtent effectExtent1 = new Wp.EffectExtent(){ LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L };
            Wp.DocProperties docProperties1 = new Wp.DocProperties(){ Id = (UInt32Value)1U, Name = "Group2" };

            A.Graphic graphic1 = new A.Graphic();
            graphic1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.GraphicData graphicData1 = new A.GraphicData(){ Uri = "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" };

            Wpg.WordprocessingGroup wordprocessingGroup1 = new Wpg.WordprocessingGroup();
            Wpg.NonVisualGroupDrawingShapeProperties nonVisualGroupDrawingShapeProperties1 = new Wpg.NonVisualGroupDrawingShapeProperties();

            Wpg.GroupShapeProperties groupShapeProperties1 = new Wpg.GroupShapeProperties();

            A.TransformGroup transformGroup1 = new A.TransformGroup();
            A.Offset offset1 = new A.Offset(){ X = 0L, Y = 0L };
            A.Extents extents1 = new A.Extents(){ Cx = 349009L, Cy = 400050L };
            A.ChildOffset childOffset1 = new A.ChildOffset(){ X = 0L, Y = 0L };
            A.ChildExtents childExtents1 = new A.ChildExtents(){ Cx = 349009L, Cy = 400050L };

            transformGroup1.Append(offset1);
            transformGroup1.Append(extents1);
            transformGroup1.Append(childOffset1);
            transformGroup1.Append(childExtents1);

            groupShapeProperties1.Append(transformGroup1);

            Wps.WordprocessingShape wordprocessingShape1 = new Wps.WordprocessingShape();
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Wps.NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Curve4" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties1 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties1 = new Wps.ShapeProperties();

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset2 = new A.Offset(){ X = 9525L, Y = 104775L };
            A.Extents extents2 = new A.Extents(){ Cx = 0L, Cy = 190500L };

            transform2D1.Append(offset2);
            transform2D1.Append(extents2);

            A.CustomGeometry customGeometry1 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();
            A.Rectangle rectangle1 = new A.Rectangle(){ Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList1 = new A.PathList();

            A.Path path1 = new A.Path(){ Width = 0L, Height = 190500L };

            A.MoveTo moveTo1 = new A.MoveTo();
            A.Point point1 = new A.Point(){ X = "0", Y = "0" };

            moveTo1.Append(point1);

            A.LineTo lineTo1 = new A.LineTo();
            A.Point point2 = new A.Point(){ X = "0", Y = "190500" };

            lineTo1.Append(point2);

            path1.Append(moveTo1);
            path1.Append(lineTo1);

            pathList1.Append(path1);

            customGeometry1.Append(adjustValueList1);
            customGeometry1.Append(rectangle1);
            customGeometry1.Append(pathList1);

            A.Outline outline1 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill1 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha1 = new A.Alpha(){ Val = new Int32Value() { InnerText = "100%"} };

            rgbColorModelHex1.Append(alpha1);

            solidFill1.Append(rgbColorModelHex1);

            outline1.Append(solidFill1);

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(customGeometry1);
            shapeProperties1.Append(outline1);

            Wps.ShapeStyle shapeStyle1 = new Wps.ShapeStyle();
            A.LineReference lineReference1 = new A.LineReference(){ Index = (UInt32Value)0U };
            A.FillReference fillReference1 = new A.FillReference(){ Index = (UInt32Value)0U };
            A.EffectReference effectReference1 = new A.EffectReference(){ Index = (UInt32Value)0U };
            A.FontReference fontReference1 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };

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
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties2 = new Wps.NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Curve6" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties2 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties2 = new Wps.ShapeProperties();

            A.Transform2D transform2D2 = new A.Transform2D();
            A.Offset offset3 = new A.Offset(){ X = 9525L, Y = 9525L };
            A.Extents extents3 = new A.Extents(){ Cx = 164973L, Cy = 95250L };

            transform2D2.Append(offset3);
            transform2D2.Append(extents3);

            A.CustomGeometry customGeometry2 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList2 = new A.AdjustValueList();
            A.Rectangle rectangle2 = new A.Rectangle(){ Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList2 = new A.PathList();

            A.Path path2 = new A.Path(){ Width = 164973L, Height = 95250L };

            A.MoveTo moveTo2 = new A.MoveTo();
            A.Point point3 = new A.Point(){ X = "0", Y = "95250" };

            moveTo2.Append(point3);

            A.LineTo lineTo2 = new A.LineTo();
            A.Point point4 = new A.Point(){ X = "164973", Y = "0" };

            lineTo2.Append(point4);

            path2.Append(moveTo2);
            path2.Append(lineTo2);

            pathList2.Append(path2);

            customGeometry2.Append(adjustValueList2);
            customGeometry2.Append(rectangle2);
            customGeometry2.Append(pathList2);

            A.Outline outline2 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill2 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha2 = new A.Alpha(){ Val = new Int32Value() { InnerText = "100%"} };

            rgbColorModelHex2.Append(alpha2);

            solidFill2.Append(rgbColorModelHex2);

            outline2.Append(solidFill2);

            shapeProperties2.Append(transform2D2);
            shapeProperties2.Append(customGeometry2);
            shapeProperties2.Append(outline2);

            Wps.ShapeStyle shapeStyle2 = new Wps.ShapeStyle();
            A.LineReference lineReference2 = new A.LineReference(){ Index = (UInt32Value)0U };
            A.FillReference fillReference2 = new A.FillReference(){ Index = (UInt32Value)0U };
            A.EffectReference effectReference2 = new A.EffectReference(){ Index = (UInt32Value)0U };
            A.FontReference fontReference2 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };

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
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties3 = new Wps.NonVisualDrawingProperties(){ Id = (UInt32Value)7U, Name = "Curve8" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties3 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties3 = new Wps.ShapeProperties();

            A.Transform2D transform2D3 = new A.Transform2D();
            A.Offset offset4 = new A.Offset(){ X = 9525L, Y = 295275L };
            A.Extents extents4 = new A.Extents(){ Cx = 164973L, Cy = 95250L };

            transform2D3.Append(offset4);
            transform2D3.Append(extents4);

            A.CustomGeometry customGeometry3 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList3 = new A.AdjustValueList();
            A.Rectangle rectangle3 = new A.Rectangle(){ Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList3 = new A.PathList();

            A.Path path3 = new A.Path(){ Width = 164973L, Height = 95250L };

            A.MoveTo moveTo3 = new A.MoveTo();
            A.Point point5 = new A.Point(){ X = "0", Y = "0" };

            moveTo3.Append(point5);

            A.LineTo lineTo3 = new A.LineTo();
            A.Point point6 = new A.Point(){ X = "164973", Y = "95250" };

            lineTo3.Append(point6);

            path3.Append(moveTo3);
            path3.Append(lineTo3);

            pathList3.Append(path3);

            customGeometry3.Append(adjustValueList3);
            customGeometry3.Append(rectangle3);
            customGeometry3.Append(pathList3);

            A.Outline outline3 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill3 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha3 = new A.Alpha(){ Val = new Int32Value() { InnerText = "100%"} };

            rgbColorModelHex3.Append(alpha3);

            solidFill3.Append(rgbColorModelHex3);

            outline3.Append(solidFill3);

            shapeProperties3.Append(transform2D3);
            shapeProperties3.Append(customGeometry3);
            shapeProperties3.Append(outline3);

            Wps.ShapeStyle shapeStyle3 = new Wps.ShapeStyle();
            A.LineReference lineReference3 = new A.LineReference(){ Index = (UInt32Value)0U };
            A.FillReference fillReference3 = new A.FillReference(){ Index = (UInt32Value)0U };
            A.EffectReference effectReference3 = new A.EffectReference(){ Index = (UInt32Value)0U };
            A.FontReference fontReference3 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };

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
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties4 = new Wps.NonVisualDrawingProperties(){ Id = (UInt32Value)9U, Name = "Curve10" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties4 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties4 = new Wps.ShapeProperties();

            A.Transform2D transform2D4 = new A.Transform2D();
            A.Offset offset5 = new A.Offset(){ X = 174498L, Y = 295275L };
            A.Extents extents5 = new A.Extents(){ Cx = 164986L, Cy = 95250L };

            transform2D4.Append(offset5);
            transform2D4.Append(extents5);

            A.CustomGeometry customGeometry4 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList4 = new A.AdjustValueList();
            A.Rectangle rectangle4 = new A.Rectangle(){ Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList4 = new A.PathList();

            A.Path path4 = new A.Path(){ Width = 164986L, Height = 95250L };

            A.MoveTo moveTo4 = new A.MoveTo();
            A.Point point7 = new A.Point(){ X = "0", Y = "95250" };

            moveTo4.Append(point7);

            A.LineTo lineTo4 = new A.LineTo();
            A.Point point8 = new A.Point(){ X = "164986", Y = "0" };

            lineTo4.Append(point8);

            path4.Append(moveTo4);
            path4.Append(lineTo4);

            pathList4.Append(path4);

            customGeometry4.Append(adjustValueList4);
            customGeometry4.Append(rectangle4);
            customGeometry4.Append(pathList4);

            A.Outline outline4 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill4 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha4 = new A.Alpha(){ Val = new Int32Value() { InnerText = "100%"} };

            rgbColorModelHex4.Append(alpha4);

            solidFill4.Append(rgbColorModelHex4);

            outline4.Append(solidFill4);

            shapeProperties4.Append(transform2D4);
            shapeProperties4.Append(customGeometry4);
            shapeProperties4.Append(outline4);

            Wps.ShapeStyle shapeStyle4 = new Wps.ShapeStyle();
            A.LineReference lineReference4 = new A.LineReference(){ Index = (UInt32Value)0U };
            A.FillReference fillReference4 = new A.FillReference(){ Index = (UInt32Value)0U };
            A.EffectReference effectReference4 = new A.EffectReference(){ Index = (UInt32Value)0U };
            A.FontReference fontReference4 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };

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
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties5 = new Wps.NonVisualDrawingProperties(){ Id = (UInt32Value)11U, Name = "Curve12" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties5 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties5 = new Wps.ShapeProperties();

            A.Transform2D transform2D5 = new A.Transform2D();
            A.Offset offset6 = new A.Offset(){ X = 339484L, Y = 104775L };
            A.Extents extents6 = new A.Extents(){ Cx = 0L, Cy = 190500L };

            transform2D5.Append(offset6);
            transform2D5.Append(extents6);

            A.CustomGeometry customGeometry5 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList5 = new A.AdjustValueList();
            A.Rectangle rectangle5 = new A.Rectangle(){ Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList5 = new A.PathList();

            A.Path path5 = new A.Path(){ Width = 0L, Height = 190500L };

            A.MoveTo moveTo5 = new A.MoveTo();
            A.Point point9 = new A.Point(){ X = "0", Y = "190500" };

            moveTo5.Append(point9);

            A.LineTo lineTo5 = new A.LineTo();
            A.Point point10 = new A.Point(){ X = "0", Y = "0" };

            lineTo5.Append(point10);

            path5.Append(moveTo5);
            path5.Append(lineTo5);

            pathList5.Append(path5);

            customGeometry5.Append(adjustValueList5);
            customGeometry5.Append(rectangle5);
            customGeometry5.Append(pathList5);

            A.Outline outline5 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill5 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha5 = new A.Alpha(){ Val = new Int32Value() { InnerText = "100%"} };

            rgbColorModelHex5.Append(alpha5);

            solidFill5.Append(rgbColorModelHex5);

            outline5.Append(solidFill5);

            shapeProperties5.Append(transform2D5);
            shapeProperties5.Append(customGeometry5);
            shapeProperties5.Append(outline5);

            Wps.ShapeStyle shapeStyle5 = new Wps.ShapeStyle();
            A.LineReference lineReference5 = new A.LineReference(){ Index = (UInt32Value)0U };
            A.FillReference fillReference5 = new A.FillReference(){ Index = (UInt32Value)0U };
            A.EffectReference effectReference5 = new A.EffectReference(){ Index = (UInt32Value)0U };
            A.FontReference fontReference5 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };

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
            Wps.NonVisualDrawingProperties nonVisualDrawingProperties6 = new Wps.NonVisualDrawingProperties(){ Id = (UInt32Value)13U, Name = "Curve14" };
            Wps.NonVisualDrawingShapeProperties nonVisualDrawingShapeProperties6 = new Wps.NonVisualDrawingShapeProperties();

            Wps.ShapeProperties shapeProperties6 = new Wps.ShapeProperties();

            A.Transform2D transform2D6 = new A.Transform2D();
            A.Offset offset7 = new A.Offset(){ X = 174498L, Y = 9525L };
            A.Extents extents7 = new A.Extents(){ Cx = 164986L, Cy = 95250L };

            transform2D6.Append(offset7);
            transform2D6.Append(extents7);

            A.CustomGeometry customGeometry6 = new A.CustomGeometry();
            A.AdjustValueList adjustValueList6 = new A.AdjustValueList();
            A.Rectangle rectangle6 = new A.Rectangle(){ Left = "l", Top = "t", Right = "r", Bottom = "b" };

            A.PathList pathList6 = new A.PathList();

            A.Path path6 = new A.Path(){ Width = 164986L, Height = 95250L };

            A.MoveTo moveTo6 = new A.MoveTo();
            A.Point point11 = new A.Point(){ X = "164986", Y = "95250" };

            moveTo6.Append(point11);

            A.LineTo lineTo6 = new A.LineTo();
            A.Point point12 = new A.Point(){ X = "0", Y = "0" };

            lineTo6.Append(point12);

            path6.Append(moveTo6);
            path6.Append(lineTo6);

            pathList6.Append(path6);

            customGeometry6.Append(adjustValueList6);
            customGeometry6.Append(rectangle6);
            customGeometry6.Append(pathList6);

            A.Outline outline6 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Round };

            A.SolidFill solidFill6 = new A.SolidFill();

            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha6 = new A.Alpha(){ Val = new Int32Value() { InnerText = "100%"} };

            rgbColorModelHex6.Append(alpha6);

            solidFill6.Append(rgbColorModelHex6);

            outline6.Append(solidFill6);

            shapeProperties6.Append(transform2D6);
            shapeProperties6.Append(customGeometry6);
            shapeProperties6.Append(outline6);

            Wps.ShapeStyle shapeStyle6 = new Wps.ShapeStyle();
            A.LineReference lineReference6 = new A.LineReference(){ Index = (UInt32Value)0U };
            A.FillReference fillReference6 = new A.FillReference(){ Index = (UInt32Value)0U };
            A.EffectReference effectReference6 = new A.EffectReference(){ Index = (UInt32Value)0U };
            A.FontReference fontReference6 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };

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

            wordprocessingGroup1.Append(nonVisualGroupDrawingShapeProperties1);
            wordprocessingGroup1.Append(groupShapeProperties1);
            wordprocessingGroup1.Append(wordprocessingShape1);
            wordprocessingGroup1.Append(wordprocessingShape2);
            wordprocessingGroup1.Append(wordprocessingShape3);
            wordprocessingGroup1.Append(wordprocessingShape4);
            wordprocessingGroup1.Append(wordprocessingShape5);
            wordprocessingGroup1.Append(wordprocessingShape6);

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

