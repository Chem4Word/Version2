// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System.Windows;
using System.Xml.Linq;
using Chem4Word.UI.CmlViewer;
using Chem4Word.UI.TwoD;
using log4net;
using Microsoft.Win32;
using Numbo.Cml;
using Numbo.Coa;

namespace TwoDUITest {
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private static readonly ILog Log = LogManager.GetLogger(typeof (MainWindow));

        private ContextObject contextObject = null;
        private ChemCanvas cv = null;

        private void Button_Click(object sender, RoutedEventArgs e) {
            var dlg = new OpenFileDialog();
            var result = dlg.ShowDialog();

            if (result.Value) {
                var xDoc = XDocument.Load(dlg.FileName);

                var molecule = CmlUtils.GetFirstDescendentMolecule(xDoc);
                contextObject = new ContextObject(xDoc);

                cv = new ChemCanvas(contextObject, molecule) {Focusable = true};
                chemCanvasHost.Children.Clear();
                chemCanvasHost.Children.Add(cv);
                cv.Refresh(false);
                Log.Info(cv);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            var ptec = new PeriodicTableElementChooser();
            var window = new Window {Content = ptec, Width = 582, Height = 348};
            window.ShowDialog();
        }

        private void ViewCMLButton_Click(object sender, RoutedEventArgs e) {
            var xDocument = XDocument.Parse(
                @"<cml xmlns='http://www.xml-cml.org/schema'>  <molecule id='m1'>    <atomArray>      <atom id='a1' elementType='C' x2='1.8666666746139526' y2='-0.5483672065041283' />      <atom id='a2' elementType='C' x2='3.200336015662211' y2='1.761683615172993' />      <atom id='a3' elementType='C' x2='0.5329973335656941' y2='1.761683615172993' />      <atom id='a4' elementType='C' x2='0.5329973335656941' y2='0.22164973405491217' />      <atom id='a5' elementType='C' x2='-1.8367802452266828' y2='0.9916927930417153' />      <atom id='a6' elementType='C' x2='-1.4075727119978318' y2='-1.71873462289945' />      <atom id='a7' elementType='C' x2='4.534020027799442' y2='-0.5483417956189791' />      <atom id='a8' elementType='C' x2='1.8666666746139526' y2='4.071700555732034' />      <atom id='a9' elementType='O' x2='1.8666666746139524' y2='-2.0883672065041283' />      <atom id='a10' elementType='O' x2='4.534020027799442' y2='2.5316751448468846' />      <atom id='a11' elementType='N' x2='-0.931636522301039' y2='-0.25412384069931254' />      <atom id='a12' elementType='N' x2='-0.9316198892351695' y2='2.23759998458582' />      <atom id='a13' elementType='N' x2='3.200336015662211' y2='0.22164973405491217' />      <atom id='a14' elementType='N' x2='1.8666666746139526' y2='2.5317005557320336' />      <atom id='a15' elementType='H' x2='-3.37678024329987' y2='0.9916157568033357' />      <atom id='a16' elementType='H' x2='0.05703807020230567' y2='-2.1946708125962426' />      <atom id='a17' elementType='H' x2='-2.8721834941979694' y2='-1.2427984332026574' />      <atom id='a18' elementType='H' x2='-1.8835089016946245' y2='-3.1833454050995877' />      <atom id='a19' elementType='H' x2='5.304011557473333' y2='0.7853422165182515' />      <atom id='a20' elementType='H' x2='3.7640284981255507' y2='-1.88202580775621' />      <atom id='a21' elementType='H' x2='5.8677040399366724' y2='-1.3183333252928704' />      <atom id='a22' elementType='H' x2='0.3266666746139526' y2='4.071700555732034' />      <atom id='a23' elementType='H' x2='3.4066666746139527' y2='4.071700555732034' />      <atom id='a24' elementType='H' x2='1.8666666746139524' y2='5.611700555732034' />    </atomArray>    <bondArray>      <bond id='b1' atomRefs2='a14 a3' order='S' />      <bond id='b2' atomRefs2='a14 a2' order='S' />      <bond id='b3' atomRefs2='a4 a1' order='S' />      <bond id='b4' atomRefs2='a1 a13' order='S' />      <bond id='b5' atomRefs2='a13 a2' order='S' />      <bond id='b6' atomRefs2='a12 a3' order='S' />      <bond id='b7' atomRefs2='a4 a3' order='D' />      <bond id='b8' atomRefs2='a11 a4' order='S' />      <bond id='b9' atomRefs2='a12 a5' order='D' />      <bond id='b10' atomRefs2='a5 a11' order='S' />      <bond id='b11' atomRefs2='a11 a6' order='S' />      <bond id='b12' atomRefs2='a1 a9' order='D' />      <bond id='b13' atomRefs2='a13 a7' order='S' />      <bond id='b14' atomRefs2='a2 a10' order='D' />      <bond id='b15' atomRefs2='a14 a8' order='S' />      <bond id='b16' atomRefs2='a5 a15' order='S' />      <bond id='b17' atomRefs2='a6 a16' order='S' />      <bond id='b18' atomRefs2='a6 a17' order='S' />      <bond id='b19' atomRefs2='a6 a18' order='S' />      <bond id='b20' atomRefs2='a7 a19' order='S' />      <bond id='b21' atomRefs2='a7 a20' order='S' />      <bond id='b22' atomRefs2='a7 a21' order='S' />      <bond id='b23' atomRefs2='a8 a22' order='S' />      <bond id='b24' atomRefs2='a8 a23' order='S' />      <bond id='b25' atomRefs2='a8 a24' order='S' />    </bondArray>  </molecule></cml>");

            var viewer = new Viewer(xDocument);
            viewer.ShowDialog();
        }
    }
}
