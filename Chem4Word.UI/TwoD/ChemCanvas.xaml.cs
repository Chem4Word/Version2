// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using Chem4Word.UI.Commands;
using Euclid;
using log4net;
using Numbo.Cml;
using Numbo.Cml.Helpers;
using Numbo.Coa;

namespace Chem4Word.UI.TwoD {
    /// <summary>
    ///   Interaction logic for ChemCanvas.xaml
    /// </summary>
    public partial class ChemCanvas : INotifyPropertyChanged {
        private static readonly ILog Log = LogManager.GetLogger(typeof (ChemCanvas));
        private readonly BondContextMenu bondContextMenu = new BondContextMenu();
        private readonly RadialChemContextMenu chemContextMenu = new RadialChemContextMenu();
        private readonly RotateTransform rotate = new RotateTransform();
        private readonly SelectionRotationTool selectionRotationTool = new SelectionRotationTool();
        internal double StandardXOffset;
        internal double StandardYOffset;
        private bool deferredBondSelectionPending;
        private AbstractEdgeControl deferredSelectionBond;
        private AbstractNodeControl deferredSelectionNode;
        private bool deferredSelectionPending;
        private Point dragStart;
        private AbstractNodeControl draggableNode;
        private string draggableNodeId;
        private bool dragging;
        private bool hasMouseDowned;
        private Vector initialRotateVector;
        private bool isRotating;
        private double lastAngle;
        private SortedDictionary<string, AbstractNodeControl> nodes;
        private Point rotationCentreInCanvasSpace;
        private RectangleGeometry selectionGeometry;
        private TransformGroup standardTransformGroup;
        private ViewBoxDraggingPosition viewBoxDraggingPosition = ViewBoxDraggingPosition.None;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public event AtomTypeOptionsUpdatedHandler AtomTypeOptionsUpdated;
        public event IsotopeOptionsUpdatedHandler IsotopeOptionsUpdated;

        internal void RotateExecuted(object sender, ExecutedRoutedEventArgs e) {
            if (Children.Contains(chemContextMenu)) {
                Children.Remove(chemContextMenu);
                chemContextMenu.Opacity = 1.0;
            }

            XElement[] xelmns = new XElement[selectedAtoms.Count];
            for (int i = 0; i < selectedAtoms.Count; i++) {
                xelmns[i] = selectedAtoms.Values.ToArray()[i].DelegateElement;
            }

            rotationCentreInMolSpace = ChemicalIntelligence.GetRotationPoint(ContextObject, xelmns);
            rotationCentreInCanvasSpace = new Point(ToScreenX(rotationCentreInMolSpace.X) + StandardXOffset,
                                                    ToScreenY(rotationCentreInMolSpace.Y) + StandardYOffset);

            rotate.Angle = 0f;

            selectionRotationTool.Initialise(this, SelectedAtoms, StandardXOffset, StandardYOffset);

            rotate.CenterX = rotationCentreInCanvasSpace.X - GetLeft(selectionRotationTool);
            rotate.CenterY = rotationCentreInCanvasSpace.Y - GetTop(selectionRotationTool);

            Children.Add(selectionRotationTool);

            selectionRotationTool.InvalidateVisual();
        }

        internal void RotateSelectionCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = selectedAtoms.Count > 1;
        }

        private void SelectionRotationToolMouseDown(object sender, MouseButtonEventArgs e) {
            TakeSnapshotOfContextObject();
            Point p = e.GetPosition(this);
            initialRotateVector = p - rotationCentreInCanvasSpace;

            initialRotateVector.Normalize();
            lastAngle = 0.0;
            isRotating = true;
            e.Handled = true;
        }

        private void BondContextMenuBondGroupSelected(string group) {
            TakeSnapshotOfContextObject();

            foreach (XElement bond in GetSelectedBondsAsXElements()) {
                Cid.SubstituteBondByGroup(ContextObject, bond, group);
            }

            Refresh();
        }

        private void BondContextMenuBondStereoSelected(string type) {
            TakeSnapshotOfContextObject();

            foreach (XElement bond in GetSelectedBondsAsXElements()) {
                Cid.SetBondStereo(ContextObject, bond, type);
            }

            Refresh();
        }

        private void BondContextMenuCustomBondOrderSelected(string order) {
            TakeSnapshotOfContextObject();

            foreach (XElement bond in GetSelectedBondsAsXElements()) {
                Cid.SetBondOrder(ContextObject, bond, order);
            }

            Refresh();
        }

        private void BondContextMenuBondOrderSelected(string order) {
            TakeSnapshotOfContextObject();

            foreach (XElement bond in GetSelectedBondsAsXElements()) {
                Cid.SetBondOrderAndFixChemistry(ContextObject, bond, order);
            }

            Refresh();
        }

        internal void ChemAtomTypeSelected(string atomType) {
            TakeSnapshotOfContextObject();
            List<XElement> atoms = GetSelectedAtomsAsXElements();
            selectedAtoms.Clear();
            Cid.ChangeElementTypeAndFixChemistry(ContextObject, atoms,
                                                 atomType);
            if (Children.Contains(chemContextMenu)) {
                Children.Remove(chemContextMenu);
            }

            Refresh();
        }

        public void ChemContextMenuCustomAtomTypeSelected(string atomType) {
            TakeSnapshotOfContextObject();
            Cid.ChangeElementType(ContextObject, GetSelectedAtomsAsXElements(),
                                  atomType);
            if (Children.Contains(chemContextMenu)) {
                Children.Remove(chemContextMenu);
            }
            Refresh();
        }

        internal void ChemGroupSelected(string groupType) {
            TakeSnapshotOfContextObject();
            XElement atom = selectedAtoms.ToArray()[0].Value.DelegateElement;
            selectedAtoms.Clear();
            Cid.SubstituteAtomByGroup(ContextObject, atom, groupType);
            if (Children.Contains(chemContextMenu)) {
                Children.Remove(chemContextMenu);
            }

            Refresh();
        }

        private void ChemContextMenuCustomChargeSelected(int charge) {
            TakeSnapshotOfContextObject();
            Cid.EditOrSetCharge(ContextObject, SelectedAtoms.ToArray()[0].Value.DelegateElement, charge);
            Refresh();
        }

        private void ChemContextMenuChargeSelected(int charge) {
            TakeSnapshotOfContextObject();
            Cid.EditOrSetCharge(ContextObject, selectedAtoms.ToArray()[0].Value.DelegateElement, charge);
            Refresh();
        }

        internal void ChemContextMenuIsotopeSelected(int isotope) {
            if (CmlAtom.IsValidIsotopeNumber(isotope)) {
                TakeSnapshotOfContextObject();
                Cid.EditOrSetIsotopeNumber(ContextObject, selectedAtoms.ToArray()[0].Value.DelegateElement, isotope);
                Refresh();
            } else {
                MessageBox.Show(Window.GetWindow(this), isotope + " is not a valid isotope for this atom.",
                                "Invalid Chemistry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal void DeleteSelectionCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = selectedAtoms.Count > 0;
        }

        internal void DeleteSelectionExecuted(object sender, ExecutedRoutedEventArgs e) {
            List<XElement> atoms = GetSelectedAtomsAsXElements();
            // TODO decide on and implement deletion modes
            // if DELETION MODE is remove and fix valencies 
            // various possibilities for deletion mode would be:
            // 1 just remove atoms and attached bonds (the simplest case)
            // 2 do 1 then change charges
            // 3 do 1 then add hydrogen (the default)
            TakeSnapshotOfContextObject();
            Cid.DeleteAtomsAndFixChemistry(ContextObject, atoms);
            selectedAtoms.Clear();

            Refresh();
        }

        internal void FlipHorizontalExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            Vector2AndPoint2 vecAndPoint = ChemicalIntelligence.CanFlipHorizontally(ContextObject,
                                                                                    GetSelectedAtomsAsXElements());

            Cid.FlipAboutVectorThroughPoint(ContextObject, GetSelectedAtomsAsXElements(), vecAndPoint.Vector,
                                            vecAndPoint.Point);

            Refresh();
        }

        internal void FlipHorizontalCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ChemicalIntelligence.CanFlipHorizontally(ContextObject, GetSelectedAtomsAsXElements()) !=
                           null;
        }

        internal void FlipVerticalExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            Vector2AndPoint2 vecAndPoint = ChemicalIntelligence.CanFlipVertically(ContextObject,
                                                                                  GetSelectedAtomsAsXElements());
            Cid.FlipAboutVectorThroughPoint(ContextObject, GetSelectedAtomsAsXElements(), vecAndPoint.Vector,
                                            vecAndPoint.Point);

            Refresh();
        }

        internal void FlipVerticalCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ChemicalIntelligence.CanFlipVertically(ContextObject, GetSelectedAtomsAsXElements()) != null;
        }

        internal void AddRemoveElectronCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ChemicalIntelligence.CanEditOrSetSpinMultiplicity(ContextObject,
                                                                             GetSelectedAtomsAsXElements());
        }

        internal void AddElectronExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            foreach (XElement xElement in GetSelectedAtomsAsXElements()) {
                Cid.AddElectron(ContextObject, xElement);
            }
            Refresh();
        }

        internal void RemoveElectronExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            foreach (XElement xElement in GetSelectedAtomsAsXElements()) {
                Cid.RemoveElectron(ContextObject, xElement);
            }
            Refresh();
        }

        internal void UndoCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = undoStack.Count > 0;
        }

        internal void UndoExecuted(object sender, ExecutedRoutedEventArgs e) {
            IsDirty = true;
            redoStack.Push(ContextObject);
            ContextObject = undoStack.Pop();
            molecule = CmlUtils.GetFirstDescendentMolecule(ContextObject.Cml);
            InvalidateAll();
            Refresh();
        }

        internal void RedoCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = redoStack.Count > 0;
        }

        internal void RedoExecuted(object sender, ExecutedRoutedEventArgs e) {
            IsDirty = true;
            undoStack.Push(ContextObject);
            ContextObject = redoStack.Pop();
            molecule = CmlUtils.GetFirstDescendentMolecule(ContextObject.Cml);
            Refresh();
        }

        internal void AddLabelCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ChemicalIntelligence.CanAddLabel(ContextObject, GetSelectedAtomsAsXElements());
        }

        internal void AddLabelExecuted(object sender, ExecutedRoutedEventArgs e) {
            AddLabelWindow dlg = new AddLabelWindow {Owner = Window.GetWindow(this)};
            XElement labelPointer = null;
            if (selectedAtoms.Count == 1) {
                CmlLabel[] labels = selectedAtoms.ToArray()[0].Value.GetLabels().ToArray();
                if (labels.Length > 0) {
                    labelPointer = labels[0].DelegateElement;
                    dlg.LabelText = labels[0].Value;
                    dlg.SelectAll();
                }
            }
            dlg.ShowDialog();
            if (dlg.DialogResult == true) {
                TakeSnapshotOfContextObject();
                if (labelPointer != null) {
                    Cid.EditOrSetLabelValue(ContextObject, labelPointer, dlg.LabelText);
                } else {
                    Cid.AddLabel(ContextObject, GetSelectedAtomsAsXElements(), dlg.LabelText);
                }
            }
            Refresh();
        }

        internal void RemoveLabelCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ChemicalIntelligence.CanRemoveLabels(ContextObject, GetSelectedAtomsAsXElements());
        }

        internal void RemoveLabelExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            Cid.RemoveLabel(ContextObject, GetSelectedAtomsAsXElements());
            Refresh();
        }

        /// <summary>
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        internal void FlipSelectionCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = (ChemicalIntelligence.CanFlipAboutExternalAcyclicBond(ContextObject,
                                                                                 GetSelectedAtomsAsXElements()) != null);
        }

        internal void FlipSelectionExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            Vector2AndPoint2 vecAndPoint = ChemicalIntelligence.CanFlipAboutExternalAcyclicBond(ContextObject,
                                                                                                GetSelectedAtomsAsXElements
                                                                                                    ());
            Cid.FlipAboutVectorThroughPoint(ContextObject, GetSelectedAtomsAsXElements(), vecAndPoint.Vector,
                                            vecAndPoint.Point);
            Refresh();
        }

        internal void RemoveIsotopeCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ChemicalIntelligence.CanUnsetIsotopeNumber(ContextObject, GetSelectedAtomsAsXElements());
        }

        internal void RemoveIsotopeExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            Cid.UnsetIsotopeNumber(ContextObject, GetSelectedAtomsAsXElements());
            Refresh();
        }

        internal void SetChargeCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ChemicalIntelligence.CanEditOrSetCharge(ContextObject, GetSelectedAtomsAsXElements());
        }

        internal void SetChargeExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            AddLabelWindow dlg = new AddLabelWindow {Title = "Set Charge", Owner = Window.GetWindow(this)};
            dlg.ShowDialog();
            if (dlg.DialogResult == true) {
                int result;
                bool isValid = int.TryParse(dlg.LabelText, out result);
                if (isValid) {
                    foreach (XElement element in GetSelectedAtomsAsXElements()) {
                        Cid.EditOrSetCharge(ContextObject, element, result);
                    }
                }
            }
            Refresh();
        }

        internal void RemoveChargeCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ChemicalIntelligence.CanUnsetCharge(ContextObject, GetSelectedAtomsAsXElements());
        }

        internal void RemoveChargeExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            Cid.UnsetCharge(ContextObject, GetSelectedAtomsAsXElements());
            Refresh();
        }

        internal void AddProtonCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ChemicalIntelligence.CanAddProton(ContextObject, GetSelectedAtomsAsXElements());
        }

        internal void AddProtonExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            foreach (XElement element in GetSelectedAtomsAsXElements()) {
                Cid.AddProton(ContextObject, element);
            }
            Refresh();
        }

        internal void RemoveProtonCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            List<XElement> atoms = GetSelectedAtomsAsXElements();
            e.CanExecute = ChemicalIntelligence.CanRemoveProton(ContextObject, atoms);
        }

        internal void RemoveProtonExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            foreach (XElement element in GetSelectedAtomsAsXElements()) {
                Cid.RemoveProton(ContextObject, element);
            }
            Refresh();
        }

        internal void AddHydrogenDotCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ChemicalIntelligence.CanAddHydrogenDot(ContextObject, GetSelectedAtomsAsXElements());
        }

        internal void AddHydrogenDotExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            foreach (XElement element in GetSelectedAtomsAsXElements()) {
                Cid.AddHydrogenDot(ContextObject, element);
            }
            Refresh();
        }

        internal void RemoveHydrogenDotCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = ChemicalIntelligence.CanRemoveHydrogenDot(ContextObject, GetSelectedAtomsAsXElements());
        }

        internal void RemoveHydrogenDotExecuted(object sender, ExecutedRoutedEventArgs e) {
            TakeSnapshotOfContextObject();
            foreach (XElement element in GetSelectedAtomsAsXElements()) {
                Cid.RemoveHydrogenDot(ContextObject, element);
            }
            Refresh();
        }

        internal void SelectAllExecuted(object sender, ExecutedRoutedEventArgs e) {
            selectedAtoms.Clear();
            foreach (CmlAtom atom in pointer.GetAllAtoms()) {
                selectedAtoms.Add(atom.Id, atom);
            }
            if (selectedAtoms.Count > 0) {
                Rect r = CoordinateTool.GetBounds2D(ContextObject, SelectedAtoms.Values);
                SetLeft(chemContextMenu, ToScreenX(r.Left) + ToScreenX(r.Width/2) - (chemContextMenu.Width/2));
                SetTop(chemContextMenu, ToScreenY(r.Bottom) - chemContextMenu.Height - 25);

                Children.Add(chemContextMenu);
            }
            Refresh();
        }

        private void OnPropertyChanged(string property) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #region constructors

        public ChemCanvas() {
            InitializeComponent();

            //chemContextMenu.AtomTypeSelected += ChemContextMenuAtomTypeSelected;
            //chemContextMenu.CustomAtomTypeSelected += ChemContextMenuCustomAtomTypeSelected;
            //chemContextMenu.GroupSelected += ChemContextMenuGroupSelected;
            //chemContextMenu.ChargeSelected += ChemContextMenuChargeSelected;
            //chemContextMenu.CustomChargeSelected += ChemContextMenuCustomChargeSelected;
            //chemContextMenu.IsotopeSelected += ChemContextMenuIsotopeSelected;
            //chemContextMenu.CustomIsotopeSelected += ChemContextMenuIsotopeSelected;
            //chemContextMenu.ParentCanvas = this;
            bondContextMenu.BondOrderSelected += BondContextMenuBondOrderSelected;
            bondContextMenu.CustomBondOrderSelected += BondContextMenuCustomBondOrderSelected;
            bondContextMenu.BondStereoSelected += BondContextMenuBondStereoSelected;
            bondContextMenu.BondGroupSelected += BondContextMenuBondGroupSelected;
            selectionRotationTool.MouseDown += SelectionRotationToolMouseDown;
            selectionRotationTool.RenderTransform = rotate;
        }

        public ChemCanvas(ContextObject contextObject, CmlMolecule moleculePointer)
            : this() {
            SetData(contextObject, moleculePointer);
        }

        #endregion constructors

        #region ZoomFactor

        /// <summary>
        ///   ZoomFactor Dependency Property
        /// </summary>
        public static readonly DependencyProperty ZoomFactorProperty =
            DependencyProperty.Register("ZoomFactor", typeof (double), typeof (ChemCanvas),
                                        new FrameworkPropertyMetadata(1.0,
                                                                      new PropertyChangedCallback(OnZoomFactorChanged)));

        /// <summary>
        ///   Gets or sets the ZoomFactor property.  This dependency property 
        ///   indicates ....
        /// </summary>
        public double ZoomFactor {
            get { return (double) GetValue(ZoomFactorProperty); }
            set { SetValue(ZoomFactorProperty, value); }
        }

        /// <summary>
        ///   Handles changes to the ZoomFactor property.
        /// </summary>
        private static void OnZoomFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ChemCanvas) d).OnZoomFactorChanged(e);
        }

        /// <summary>
        ///   Provides derived classes an opportunity to handle changes to the ZoomFactor property.
        /// </summary>
        protected virtual void OnZoomFactorChanged(DependencyPropertyChangedEventArgs e) {
            if (ContextObject != null) {
                zoomFactor = (double) e.NewValue;
                Init();
                Refresh();
            }
        }

        #endregion

        #region public methods

        public void SetData(ContextObject contextObject, CmlMolecule moleculePointer) {
            ContextObject = contextObject;
            selectedAtoms.Clear();
            selectedBonds.Clear();
            pointer = moleculePointer;
            Init(moleculePointer);
        }

        /// <summary>
        ///   Convert the model x coordinate to screen x coodinate
        /// </summary>
        /// <param name = "x"></param>
        /// <returns></returns>
        public double ToScreenX(double x) {
            x = x*(scaleFactor*zoomFactor);
            return x;
        }

        /// <summary>
        ///   Convert the screen x coordinate to model x coodinate
        /// </summary>
        /// <param name = "x"></param>
        /// <returns></returns>
        public double FromScreenX(double x) {
            x = x/(scaleFactor*zoomFactor);
            return x;
        }

        /// <summary>
        ///   Convert the model y coordinate to screen y coodinate
        /// </summary>
        /// <param name = "y"></param>
        /// <returns></returns>
        public double ToScreenY(double y) {
            y = y*(scaleFactor*zoomFactor);
            if (invertYAxis) {
                y = -y;
            }
            return y;
        }

        /// <summary>
        ///   Convert the screen y coordinate to model y coodinate
        /// </summary>
        /// <param name = "y"></param>
        /// <returns></returns>
        public double FromScreenY(double y) {
            if (invertYAxis) {
                y = -y;
            }
            y = y/(scaleFactor*zoomFactor);
            return y;
        }

        /// <summary>
        ///   Method to maintain compatability with original design
        ///   TODO should refactor calls to Refresh to include the bool
        /// </summary>
        public void Refresh() {
            Refresh(false);
        }

        /// <summary>
        ///   Clear the canvas and redraw all the elements.
        /// </summary>
        public void Refresh(bool forPng) {
            var contextMenuShown = Children.Contains(chemContextMenu);

            Children.Clear();
            if (!forPng) {
                var viewBox = new ViewBox(this);
                viewBox.WestControlMouseDown += WestControlMouseDown;
                viewBox.EastControlMouseDown += EastControlMouseDown;
                viewBox.NorthControlMouseDown += NorthControlMouseDown;
                viewBox.SouthControlMouseDown += SouthControlMouseDown;
                viewBox.NorthWestControlMouseDown += NorthWestControlMouseDown;
                viewBox.NorthEastControlMouseDown += NorthEastControlMouseDown;
                viewBox.SouthWestControlMouseDown += SouthWestControlMouseDown;
                viewBox.SouthEastControlMouseDown += SouthEastControlMouseDown;
                Children.Add(viewBox);
            }
            if (forPng) {
                InitForPng();
                var r1 = CoordinateTool.GetBounds2D(ContextObject, molecule.GetAllAtoms());
                if (invertYAxis) {
                    var temp = r1;
                    r1 = new Rect(temp.X, temp.Bottom, temp.Width, temp.Height);
                }
                var viewboxActualLeft = ToScreenX(ContextObject.ViewBoxDimensions.Left);
                var viewboxActualRight = ToScreenX(ContextObject.ViewBoxDimensions.Right);
                var viewboxActualTop = ToScreenY(ContextObject.ViewBoxDimensions.Top);
                var viewboxActualBottom = ToScreenY(ContextObject.ViewBoxDimensions.Bottom);

                var molActualLeft = ToScreenX(r1.Left);
                var molActualRight = ToScreenX(r1.Right);
                var molActualTop = ToScreenY(r1.Top);
                var molActualBottom = ToScreenY(r1.Bottom);

                var viewBoxActual = new Rect(viewboxActualLeft, viewboxActualTop, viewboxActualRight - viewboxActualLeft,
                                             Math.Abs(viewboxActualTop - viewboxActualBottom));
                var molActual = new Rect(molActualLeft, molActualTop, molActualRight - molActualLeft,
                                         Math.Abs(molActualTop - molActualBottom));
                if (!viewBoxActual.Contains(molActual)) {
                    var displayBox = new DisplayBox(this);
                    Children.Add(displayBox);
                }
            }


            IsCmlValid = ChemicalIntelligence.AreThereWarningsOfCurrentChemicalRules(ContextObject,
                                                                                     molecule.DelegateElement);
            nodes = DrawAtoms(molecule);
            DrawBonds(molecule, nodes);

            if (forPng) {
                RenderTransform = standardTransformGroup;
            } else {
                foreach (UIElement child in Children) {
                    child.RenderTransform = standardTransformGroup;
                }
            }

            chemContextMenu.RenderTransform = standardTransformGroup;
            bondContextMenu.RenderTransform = standardTransformGroup;

            if (!forPng) {
                if (isRotating) {
                    Children.Add(selectionRotationTool);
                }
                if (contextMenuShown) {
                    Children.Add(chemContextMenu);
                }
            }

            if (Refreshed != null) {
                Refreshed(this, null);
            }
        }


        public event EventHandler<EventArgs> Refreshed;

        private Rectangle GetSelectionBoundsRectangle() {
            Rect rect = CoordinateTool.GetBounds2D(ContextObject, selectedAtoms.Values);

            double marginExtra = 20;

            var p1 = new Point(ToScreenX(rect.TopLeft.X) - marginExtra, ToScreenY(rect.TopLeft.Y) + marginExtra);
            var p2 = new Point(ToScreenX(rect.TopRight.X) + marginExtra, ToScreenY(rect.TopRight.Y) + marginExtra);
            var p3 = new Point(ToScreenX(rect.BottomRight.X) + marginExtra, ToScreenY(rect.BottomRight.Y) - marginExtra);
            var p4 = new Point(ToScreenX(rect.BottomLeft.X) - marginExtra, ToScreenY(rect.BottomLeft.Y) - marginExtra);

            Rectangle newRect = new Rectangle
                                    {
                                        Width = p2.X - p1.X,
                                        Height = p1.Y - p3.Y,
                                        RenderTransform = standardTransformGroup,
                                        Stroke = Brushes.Black,
                                        RadiusX = 15,
                                        RadiusY = 15
                                    };

            SetLeft(newRect, p1.X);
            SetTop(newRect, p4.Y);

            return newRect;
        }

        public void Flip() {
            if (SelectedAtoms.Count > 0) {
                HashSet<string> currentlySelectedAtoms = new HashSet<string>();

                HashSet<string> neighbours = new HashSet<string>();
                foreach (KeyValuePair<string, CmlAtom> kvp in SelectedAtoms) {
                    currentlySelectedAtoms.Add(kvp.Key);
                    foreach (string ligand in kvp.Value.GetLigandIds()) {
                        neighbours.Add(ligand);
                    }
                }
                foreach (string selected in currentlySelectedAtoms) {
                    neighbours.Remove(selected);
                }
                if (neighbours.Count == 1) {
                    CmlAtom atom0 = molecule.GetAtomById(neighbours.First());
                    ICollection<string> ligands = atom0.GetLigandIds();
                    string atom1ID = "";
                    foreach (string ligand in ligands) {
                        if (currentlySelectedAtoms.Contains(ligand)) {
                            atom1ID = ligand;
                        }
                    }
                    CmlAtom atom1 = molecule.GetAtomById(atom1ID);
                    Vector2 bondVector = atom1.Point2.Subtract(atom0.Point2);
                    Point2 otherPoint = atom0.Point2;

                    // TODO FIXME JAT
                    //CID.FlipAboutVectorThroughPoint(ContextObject, selectedAtoms, bondVector, otherPoint);

                    Refresh();
                }
            }
        }

        #endregion public methods

        #region public properties

        private readonly Stack<ContextObject> redoStack = new Stack<ContextObject>();
        private readonly Stack<ContextObject> undoStack = new Stack<ContextObject>();

        /// <summary>
        ///   The ContextObject which contains the chemistry to draw.
        /// </summary>
        private ContextObject _co;

        /// <summary>
        ///   The list of atoms currently selected.
        ///   This may have to move to an ordered set - so that we know directionality of the users
        ///   choice (ie which end of the bond was chosen first)
        /// </summary>
        private SortedDictionary<string, CmlAtom> selectedAtoms = new SortedDictionary<string, CmlAtom>();

        private SortedDictionary<string, CmlBond> selectedBonds = new SortedDictionary<string, CmlBond>();

        public SortedDictionary<string, CmlAtom> SelectedAtoms {
            get { return selectedAtoms; }
            set { selectedAtoms = value; }
        }

        public SortedDictionary<string, CmlBond> SelectedBonds {
            get { return selectedBonds; }
            set { selectedBonds = value; }
        }

        public ContextObject ContextObject {
            get { return _co; }
            internal set {
                Log.Info("setting CO");
                _co = value;
            }
        }

        /// <summary>
        ///   Get the canvas to which to add UIElements.
        /// </summary>
        public Canvas Canvas {
            get { return this; }
        }

        internal bool IsCmlValid { get; private set; }

        /// <summary>
        /// </summary>
        public bool ConnectionTableChanged { internal set; get; }

        public bool IsPerformingDirectionBondSelect { get; internal set; }

        public bool IsDirty { get; private set; }

        public bool IsSaved { get; private set; }

        internal void TakeSnapshotOfContextObject() {
            Log.Debug("Old context object cml:\n\n" + ContextObject.Cml);
            IsDirty = true;
            undoStack.Push(ContextObject.Clone());
            redoStack.Clear();
        }

        public ContextObject Save() {
            IsDirty = false;
            return ContextObject.Clone();
        }

        #endregion public properties

        #region private methods

        internal void Init() {
            InitialiseCoords();

            ICollection<CmlBond> bonds = molecule.GetAllBonds();
            avbondlen = bonds.Any() ? GeometryTool.GetMedianBondLength2D(bonds) : 1.54;
            scaleFactor = defaultBondLength/avbondlen;

            molWidthScreenCoords = (xmax - xmin)*(scaleFactor*zoomFactor);
            molHeightScreenCoords = (ymax - ymin)*(scaleFactor*zoomFactor);

            maxBoundScreenCoords = (molWidthScreenCoords > molHeightScreenCoords)
                                       ? molWidthScreenCoords
                                       : molHeightScreenCoords;

            StandardXOffset = border + ToScreenX(-xmin) + (maxBoundScreenCoords - molWidthScreenCoords)/2;
            StandardYOffset = border + ToScreenY(-ymax) + (maxBoundScreenCoords - molHeightScreenCoords)/2;

            Log.Debug(string.Format("xmax {0} xmin {1} ymax {2} ymin {3}", xmax, xmin, ymax, ymin));
            Log.Debug(string.Format("mol width (screen): {0} mol height (screen) {1}", molWidthScreenCoords,
                                    molHeightScreenCoords));
            Log.Debug(string.Format("x offset {0} y offset {1}", StandardXOffset, StandardYOffset));


            var currentViewBox = ContextObject.ViewBoxDimensions;

            if (currentViewBox.X.Equals(0d) && currentViewBox.Y.Equals(0d) && currentViewBox.Width.Equals(0d) &&
                currentViewBox.Height.Equals(0d)) {
//                viewBox = new Rect(xmin, ymax, molWidthScreenCoords, molHeightScreenCoords);

                ContextObject.ViewBoxDimensions = new Rect(xmin - 1, ymax + 1, Math.Abs(xmax - xmin) + 2,
                                                           Math.Abs(ymax - ymin) + 2);
            }

            Log.Debug(string.Format("xmax {0} xmin {1} ymax {2} ymin {3}", xmax, xmin, ymax, ymin));
            Log.Debug(string.Format("mol width (screen): {0} mol height (screen) {1}", molWidthScreenCoords,
                                    molHeightScreenCoords));
            Log.Debug(string.Format("x offset {0} y offset {1}", StandardXOffset, StandardYOffset));


            TransformGroup tg = new TransformGroup();
            // ensure that molecule has border if required
            tg.Children.Add(new TranslateTransform(StandardXOffset, StandardYOffset));
            // move the molecule to the top left of the area within the border
            //tg.Children.Add(new TranslateTransform(ToScreenX(-this.xmin), ToScreenY(-this.ymax)));

            // translate the molecule so the centre of the bounding box is at the centre of the canvas
            //tg.Children.Add(new TranslateTransform((this.maxBoundScreenCoords - this.molWidthScreenCoords) / 2, (this.maxBoundScreenCoords - this.molHeightScreenCoords) / 2));
            // this.RenderTransform = tg;
            standardTransformGroup = tg;

            Width = maxBoundScreenCoords + (2*border);
            Height = maxBoundScreenCoords + (2*border);
        }

        private void InitialiseCoords() {
            ICollection<CmlAtom> atoms = molecule.GetAllAtoms();
            if (!atoms.Any()) {
                xmin = -1;
                xmax = +1;
                ymin = -1;
                ymax = +1;
            } else {
                Rect r = CoordinateTool.GetBounds2D(ContextObject, atoms);
                xmin = r.Left;
                xmax = r.Right;
                if (invertYAxis) {
                    ymin = r.Top;
                    ymax = r.Bottom;
                } else {
                    ymin = r.Bottom;
                    ymax = r.Top;
                }
            }

            molWidthScreenCoords = (xmax - xmin)*(scaleFactor*zoomFactor);
            molHeightScreenCoords = (ymax - ymin)*(scaleFactor*zoomFactor);
            maxBoundScreenCoords = (molWidthScreenCoords > molHeightScreenCoords)
                                       ? molWidthScreenCoords
                                       : molHeightScreenCoords;
        }

        private void InitForPng() {
            selectedAtoms.Clear();
            selectedBonds.Clear();
            Effect = null;

            HideMenus();

            InitialiseCoords();

            TransformGroup tg = new TransformGroup();
            // move the molecule to the top left of the area within the border
            tg.Children.Add(new TranslateTransform(ToScreenX(-ContextObject.ViewBoxDimensions.X),
                                                   ToScreenY(-ContextObject.ViewBoxDimensions.Y)));

            standardTransformGroup = tg;

            Width = Math.Abs(ToScreenX(ContextObject.ViewBoxDimensions.Width));
            Height = Math.Abs(ToScreenY(ContextObject.ViewBoxDimensions.Height));
        }

        private void HideMenus() {
            if (Children.Contains(chemContextMenu)) {
                Children.Remove(chemContextMenu);
                //chemContextMenu.IsPeriodicTableShown = false;
            }

            if (Children.Contains(bondContextMenu)) {
                Children.Remove(bondContextMenu);
                //chemContextMenu.IsPeriodicTableShown = false;
            }
        }

        /// <summary>
        ///   Initialise all variable required 
        ///   TODO this should actually be passed an AbstractAtomContainer or the like
        /// </summary>
        /// <param name = "mol">the molecule to render</param>
        private void Init(CmlMolecule mol) {
            molecule = mol;
            Init();
        }

        /// <summary>
        ///   Creates NodeControls for the atoms. 
        ///   The types of NodeControl may have to be determined using ChemSS. 
        ///   GroupControl creation will probably also be done here.
        /// </summary>
        /// <param name = "molecule"></param>
        /// <returns>sorted dictionary of atom ids to node controls</returns>
        private SortedDictionary<string, AbstractNodeControl> DrawAtoms(CmlMolecule molecule) {
            SortedDictionary<string, AbstractNodeControl> sortedNodes =
                new SortedDictionary<string, AbstractNodeControl>();

            // QUESTION
            // Tola mentioned using a factory method here - this may well be worthwhile but 
            // is not highest priority.
            foreach (var atom in
                molecule.GetAllAtoms().Where(
                    atom => ChemicalIntelligence.ShouldAtomBeDrawnInTwoD(ContextObject, atom.DelegateElement))) {
                AbstractNodeControl node;
                switch (atom.ElementType) {
                    case "H":
                        node = new HAtomNodeControl(ContextObject, atom, this);
                        break;
                    case "C":
                        if (atom.IsotopeNumber != null ||
                            ChemicalIntelligence.IsAtomLinearCarbon(ContextObject, atom.DelegateElement)) {
                            node = new BasicNodeControl(ContextObject, atom, this);
                        } else {
                            node = new NoTextNodeControl(ContextObject, atom, this);
                        }
                        break;
                    case "F":
                        // F atoms are drawn in green - just for to show we can do a different colour
                        node = new FAtomNodeControl(ContextObject, atom, this);
                        break;
                    default:
                        node = new BasicNodeControl(ContextObject, atom, this);
                        break;
                }
                node.IsHitTestVisible = DrawingMode != CanvasContainer.DrawingMode.BondSelect;
                node.NodeMouseDown += NodeNodeMouseDown;
                Children.Add(node);
                node.CmlChangedEvent += NodeCmlChangedEvent;
                sortedNodes.Add(atom.Id, node);
            }
            return sortedNodes;
        }

        /// <summary>
        ///   Create EdgeControls for each bond.
        /// 
        ///   TODO FactoryMethod cf DrawAtoms
        /// </summary>
        /// <param name = "molecule"></param>
        /// <param name = "nodes"></param>
        private void DrawBonds(CmlMolecule molecule, SortedDictionary<string, AbstractNodeControl> nodes) {
            foreach (CmlBond bond in molecule.GetAllBonds()) {
                AbstractNodeControl startNode;
                nodes.TryGetValue(bond.GetAtoms().ElementAt(0).Id, out startNode);
                AbstractNodeControl endNode;
                nodes.TryGetValue(bond.GetAtoms().ElementAt(1).Id, out endNode);

                AbstractEdgeControl edge = null;
                switch (bond.Order) {
                    case CmlBond.Triple:
                        // always draw triple bonds
                        edge = new TripleEdgeControl(ContextObject, bond, startNode, endNode, this);
                        break;
                    case CmlBond.Double:
                        // always draw double bonds
                        edge = new DoubleEdgeControl(ContextObject, bond, startNode, endNode, this);
                        break;

                        #region case1

                    case CmlBond.Single:
                        // don't draw if C-H unless also wedge/hatch
                        // if it is a C-H non-W/H bond then the H will not have been drawn
                        // ie either the StartNode or the EndNode will be null
                        if (startNode != null && endNode != null) {
                            // a 'something' has been drawn at both ends of this bonds so 
                            // we must draw a bond
                            CmlBondStereo bondStereo = bond.GetBondStereo();
                            if (bondStereo == null) {
                                // not a stereo bond so just show a plain single bond
                                edge = new BasicEdgeControl(ContextObject, bond, startNode, endNode, this);
                            } else {
                                // this  should actually use the bondStereo.Concise value too to ensure 
                                // that the W/H is being propertly interpretted
                                switch (bondStereo.DelegateElement.Value) {
                                    case "W":
                                        edge = new WedgeEdgeControl(ContextObject, bond, startNode, endNode, this);
                                        break;
                                    case "H":
                                        edge = new HatchEdgeControl(ContextObject, bond, startNode, endNode, this);
                                        break;
                                    default:
                                        edge = new BasicEdgeControl(ContextObject, bond, startNode, endNode, this);
                                        break;
                                }
                            }
                        }
                        break;

                        // either the start or the end node has not been drawn onscreen so
                        // there can't be a bond

                        #endregion case1

                    default:
                        // unrecognised bond type so just draw a dotted line to indicate association between the atoms
                        edge = new DottedEdgeControl(ContextObject, bond, startNode, endNode, this);
                        break;
                }
                if (edge != null) {
                    edge.EdgeClicked += EdgeEdgeClicked;
                    Children.Add(edge);
                    // then probably add some listeners
                }
            }
        }

        private void EdgeEdgeClicked(object sender, EventArgs e) {
            var edge = sender as AbstractEdgeControl;

            // add or remove atoms to those selected
            if (Keyboard.IsKeyDown(Key.LeftCtrl)) {
                edge.IsSelected = !edge.IsSelected;
                Refresh();
            } else {
                deferredBondSelectionPending = true;
                deferredSelectionBond = edge;
            }

            selectionGeometry = null;
        }

        /// <summary>
        ///   Handler for CMLChangedEvents from nodes
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "cmlChangedEventArgs"></param>
        private void NodeCmlChangedEvent(object sender, CmlChangedEventArgs cmlChangedEventArgs) {
            if (!dragging) {
                ContextObject = cmlChangedEventArgs.ContextObject;
            }

            Refresh();
        }

        private void ViewBoxChangedEvent(object sender, CmlChangedEventArgs cmlChangedEventArgs) {
            ContextObject = cmlChangedEventArgs.ContextObject;
            Log.Debug(string.Format("new ContextObject viewbox x {0} y {1} w {2} h {3} ",
                                    ContextObject.ViewBoxDimensions.X, ContextObject.ViewBoxDimensions.Y,
                                    ContextObject.ViewBoxDimensions.Width, ContextObject.ViewBoxDimensions.Height));
            Refresh();
        }

        private List<XElement> GetSelectedAtomsAsXElements() {
            List<XElement> atoms = new List<XElement>(selectedAtoms.Count);
            foreach (CmlAtom atom in selectedAtoms.Values) {
                atoms.Add(atom.DelegateElement);
            }
            return atoms;
        }

        private List<XElement> GetSelectedBondsAsXElements() {
            List<XElement> bonds = new List<XElement>(selectedBonds.Count);
            foreach (CmlBond bond in selectedBonds.Values) {
                bonds.Add(bond.DelegateElement);
            }
            return bonds;
        }

        #endregion private methods

        #region private properties

        private const double DefaultBondSpacing = 4;

        /// <summary>
        ///   The values below should be derived from the ChemSS
        /// </summary>
        private const double DefaultBondWidth = 2;

        /// <summary>
        ///   The average bondlength of the bonds in the molecule
        ///   Used to calcualte appropriate scale factors.
        /// </summary>
        private double avbondlen = double.NaN;

        private double bondSpacing = DefaultBondSpacing;
        private double bondWidth = DefaultBondWidth;
        private int border = 200;
        private float defaultBondLength = 32;
        private CanvasContainer.DrawingMode drawingMode;

        /// <summary>
        ///   should y axis be inverted to draw molecule? 
        ///   Needs setting because not all drawing programs define the y-axis 
        ///   consistently.
        /// </summary>
        private bool invertYAxis = true;

        private double maxBoundScreenCoords;

        private double molHeightScreenCoords;
        private double molWidthScreenCoords;

        /// <summary>
        ///   The molecule to depict - this should become an AbstractAtomContainer
        /// </summary>
        internal CmlMolecule molecule;

//        private int pngBorder = 10;
        private CmlMolecule pointer;
        private Point2 rotationCentreInMolSpace;

        private double scaleFactor = double.NaN;
        private double xmax;

        /// <summary>
        ///   these values are derived ... they may not need to be stored
        /// </summary>
        private double xmin;

        /// <summary>
        ///   Offset to make sure that the left hand side of the molecule is translated 
        ///   to the edge of the border
        /// </summary>
        private double xoffset;

        private double xoffsetScreenCoords;
        private double ymax;
        private double ymin;

        /// <summary>
        ///   Offset to make sure that the top of the molecule is translated 
        ///   to the edge of the border
        /// </summary>
        private double yoffset;

        private double yoffsetScreenCoords;
        private double zoomFactor = 1.0d;

        public CanvasContainer.DrawingMode DrawingMode {
            get { return drawingMode; }
            set {
                selectedAtoms.Clear();
                selectedBonds.Clear();

                if (Children.Contains(chemContextMenu)) {
                    Children.Remove(chemContextMenu);
                }

                if (Children.Contains(bondContextMenu)) {
                    Children.Remove(bondContextMenu);
                }

                drawingMode = value;
                OnPropertyChanged("DrawingMode");
                Refresh();
            }
        }

        #endregion private properties

        #region Drag Code

        private void WestControlMouseDown(object sender, EventArgs e) {
            dragStart = Mouse.GetPosition(this);
            viewBoxDraggingPosition = ViewBoxDraggingPosition.West;
            Cursor = Cursors.SizeWE;
            Mouse.Capture(this);
            hasMouseDowned = true;
        }

        private void EastControlMouseDown(object sender, EventArgs e) {
            dragStart = Mouse.GetPosition(this);
            viewBoxDraggingPosition = ViewBoxDraggingPosition.East;
            Cursor = Cursors.SizeWE;
            Mouse.Capture(this);
            hasMouseDowned = true;
        }

        private void NorthControlMouseDown(object sender, EventArgs e) {
            dragStart = Mouse.GetPosition(this);
            viewBoxDraggingPosition = ViewBoxDraggingPosition.North;
            Cursor = Cursors.SizeNS;
            Mouse.Capture(this);
            hasMouseDowned = true;
        }

        private void SouthControlMouseDown(object sender, EventArgs e) {
            dragStart = Mouse.GetPosition(this);
            viewBoxDraggingPosition = ViewBoxDraggingPosition.South;
            Cursor = Cursors.SizeNS;
            Mouse.Capture(this);
            hasMouseDowned = true;
        }

        private void NorthWestControlMouseDown(object sender, EventArgs e) {
            dragStart = Mouse.GetPosition(this);
            viewBoxDraggingPosition = ViewBoxDraggingPosition.NorthWest;
            Cursor = Cursors.SizeNWSE;
            Mouse.Capture(this);
            hasMouseDowned = true;
        }

        private void NorthEastControlMouseDown(object sender, EventArgs e) {
            dragStart = Mouse.GetPosition(this);
            viewBoxDraggingPosition = ViewBoxDraggingPosition.NorthEast;
            Cursor = Cursors.SizeNESW;
            Mouse.Capture(this);
            hasMouseDowned = true;
        }

        private void SouthWestControlMouseDown(object sender, EventArgs e) {
            dragStart = Mouse.GetPosition(this);
            viewBoxDraggingPosition = ViewBoxDraggingPosition.SouthWest;
            Cursor = Cursors.SizeNESW;
            Mouse.Capture(this);
            hasMouseDowned = true;
        }

        private void SouthEastControlMouseDown(object sender, EventArgs e) {
            dragStart = Mouse.GetPosition(this);
            viewBoxDraggingPosition = ViewBoxDraggingPosition.SouthEast;
            Cursor = Cursors.SizeNWSE;
            Mouse.Capture(this);
            hasMouseDowned = true;
        }


        private void NodeNodeMouseDown(object sender, EventArgs e) {
            //ContextObject = ContextObject;
            draggableNode = sender as AbstractNodeControl;
            draggableNodeId = draggableNode.CmlAtom.Id;
            var node = sender as AbstractNodeControl;

            // add or remove atoms to those selected
            if (Keyboard.IsKeyDown(Key.LeftCtrl)) {
                node.IsSelected = !node.IsSelected;
                Refresh();
            } else {
                deferredSelectionPending = true;
                deferredSelectionNode = node;
            }

            dragStart = Mouse.GetPosition(this);
            hasMouseDowned = true;
            selectionGeometry = null;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);

            dragStart = e.MouseDevice.GetPosition(this);
            hasMouseDowned = true;
            selectionGeometry = null;

            deferredSelectionPending = true;
            deferredSelectionNode = null;
            deferredBondSelectionPending = true;
            deferredSelectionBond = null;

            if (Children.Contains(chemContextMenu)) {
                Children.Remove(chemContextMenu);
            }
        }

        protected override void OnContextMenuOpening(ContextMenuEventArgs e) {
            base.OnContextMenuOpening(e);

            HideMenus();
        }

        private void SetContextMenuOpacity(MouseEventArgs e, FrameworkElement element) {
            if (element == chemContextMenu) {
                element.Opacity = 1;
                return;
            }
            if (element != null) {
                if (element.IsMouseOver) {
                    element.Opacity = 1.0;
                    return;
                }

                Point pos = e.GetPosition(this);

                double left = GetLeft(element) + StandardXOffset;
                double right = left + element.Width;
                double top = GetTop(element) + StandardYOffset;
                double bottom = top + element.Height;
                double distance;

                if (pos.X > left && pos.X < right) {
                    distance = Math.Min(Math.Abs(pos.Y - top), Math.Abs(pos.Y - bottom));
                } else if (pos.Y > bottom && pos.Y < top) {
                    distance = Math.Min(Math.Abs(pos.X - left), Math.Abs(pos.X - right));
                } else {
                    double nearestX = Math.Abs(right - pos.X) < Math.Abs(left - pos.X) ? right : left;
                    double nearestY = Math.Abs(top - pos.Y) < Math.Abs(bottom - pos.Y) ? top : bottom;

                    distance = Math.Pow(Math.Pow(nearestX - pos.X, 2.0) + Math.Pow(nearestY - pos.Y, 2.0), 0.5);
                }

                element.Opacity = distance < 100 ? 1.0 - distance/100 : 0;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            if (isRotating) {
                DoRotate(e);
                return;
            }
            SetContextMenuOpacity(e, chemContextMenu);
            SetContextMenuOpacity(e, bondContextMenu);

            // We're just moving the mouse around with nothing pressed.
            if (!MouseButtonState.Pressed.Equals(e.LeftButton)) {
                draggableNodeId = null;
                draggableNode = null;
                hasMouseDowned = false;
                dragging = false;

                return;
            }

            // we're dragging the viewbox
            if (!ViewBoxDraggingPosition.None.Equals(viewBoxDraggingPosition)) {
                var mousePos = e.GetPosition(this);
                if (!dragging) {
                    if (Math.Abs(dragStart.X - mousePos.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(dragStart.Y - mousePos.Y) > SystemParameters.MinimumVerticalDragDistance) {
                        dragging = true;
                    } else {
                        return;
                    }
                }
                var x = FromScreenX(mousePos.X - StandardXOffset);
                var y = FromScreenY(mousePos.Y - StandardYOffset);
                var co = ContextObject;
                var newViewBox = ContextObject.ViewBoxDimensions;
                switch (viewBoxDraggingPosition) {
                    case ViewBoxDraggingPosition.West:
                        {
                            var dx = x - ContextObject.ViewBoxDimensions.Left;
                            var newWidth = ContextObject.ViewBoxDimensions.Width - dx;
                            if (newWidth > FromScreenX(ViewBox.MininumWidth)) {
                                newViewBox.X += dx;
                                newViewBox.Width = newWidth;
                                co.ViewBoxDimensions = newViewBox;
                                ViewBoxChangedEvent(null, new CmlChangedEventArgs(co));
                                return;
                            }
                            break;
                        }
                    case ViewBoxDraggingPosition.East:
                        {
                            var dx = x - ContextObject.ViewBoxDimensions.Right;
                            var newWidth = ContextObject.ViewBoxDimensions.Width + dx;
                            if (newWidth > FromScreenX(ViewBox.MininumWidth)) {
                                newViewBox.Width = newWidth;
                                co.ViewBoxDimensions = newViewBox;
                                ViewBoxChangedEvent(null, new CmlChangedEventArgs(co));
                                return;
                            }
                            break;
                        }
                    case ViewBoxDraggingPosition.North:
                        {
                            var dy = y - ContextObject.ViewBoxDimensions.Top;
                            var newHeight = ContextObject.ViewBoxDimensions.Height + dy;
                            if (newHeight > Math.Abs(FromScreenY(ViewBox.MininumHeight))) {
                                newViewBox.Y += dy;
                                newViewBox.Height = newHeight;
                                co.ViewBoxDimensions = newViewBox;
                                ViewBoxChangedEvent(null, new CmlChangedEventArgs(co));
                                return;
                            }
                            break;
                        }
                    case ViewBoxDraggingPosition.South:
                        {
                            var dy = y - (ContextObject.ViewBoxDimensions.Y - ContextObject.ViewBoxDimensions.Height);
                            var newHeight = ContextObject.ViewBoxDimensions.Height - dy;
                            if (newHeight > Math.Abs(FromScreenY(ViewBox.MininumHeight))) {
                                newViewBox.Height = newHeight;
                                co.ViewBoxDimensions = newViewBox;
                                ViewBoxChangedEvent(null, new CmlChangedEventArgs(co));
                                return;
                            }
                            break;
                        }
                    case ViewBoxDraggingPosition.NorthWest:
                        {
                            var dx = x - ContextObject.ViewBoxDimensions.Left;
                            var newWidth = ContextObject.ViewBoxDimensions.Width - dx;

                            var dy = y - ContextObject.ViewBoxDimensions.Top;
                            var newHeight = ContextObject.ViewBoxDimensions.Height + dy;

                            if (newWidth > FromScreenX(ViewBox.MininumWidth) &&
                                newHeight > Math.Abs(FromScreenY(ViewBox.MininumHeight))) {
                                newViewBox.X += dx;
                                newViewBox.Width = newWidth;
                                newViewBox.Y += dy;
                                newViewBox.Height = newHeight;
                                co.ViewBoxDimensions = newViewBox;
                                ViewBoxChangedEvent(null, new CmlChangedEventArgs(co));
                                return;
                            }
                            break;
                        }
                    case ViewBoxDraggingPosition.NorthEast:
                        {
                            var dx = x - ContextObject.ViewBoxDimensions.Right;
                            var newWidth = ContextObject.ViewBoxDimensions.Width + dx;

                            var dy = y - ContextObject.ViewBoxDimensions.Top;
                            var newHeight = ContextObject.ViewBoxDimensions.Height + dy;

                            if (newWidth > FromScreenX(ViewBox.MininumWidth) &&
                                newHeight > Math.Abs(FromScreenY(ViewBox.MininumHeight))) {
                                newViewBox.Width = newWidth;
                                newViewBox.Y += dy;
                                newViewBox.Height = newHeight;

                                co.ViewBoxDimensions = newViewBox;
                                ViewBoxChangedEvent(null, new CmlChangedEventArgs(co));
                                return;
                            }
                            break;
                        }
                    case ViewBoxDraggingPosition.SouthWest:
                        {
                            var dx = x - ContextObject.ViewBoxDimensions.Left;
                            var newWidth = ContextObject.ViewBoxDimensions.Width - dx;
                            var dy = y - (ContextObject.ViewBoxDimensions.Y - ContextObject.ViewBoxDimensions.Height);
                            var newHeight = ContextObject.ViewBoxDimensions.Height - dy;

                            if (newWidth > FromScreenX(ViewBox.MininumWidth) &&
                                newHeight > Math.Abs(FromScreenY(ViewBox.MininumHeight))) {
                                newViewBox.X += dx;
                                newViewBox.Width = newWidth;
                                newViewBox.Height = newHeight;
                                co.ViewBoxDimensions = newViewBox;
                                ViewBoxChangedEvent(null, new CmlChangedEventArgs(co));
                                return;
                            }
                            break;
                        }
                    case ViewBoxDraggingPosition.SouthEast:
                        {
                            var dx = x - ContextObject.ViewBoxDimensions.Right;
                            var newWidth = ContextObject.ViewBoxDimensions.Width + dx;

                            var dy = y - (ContextObject.ViewBoxDimensions.Y - ContextObject.ViewBoxDimensions.Height);
                            var newHeight = ContextObject.ViewBoxDimensions.Height - dy;

                            if (newWidth > FromScreenX(ViewBox.MininumWidth) &&
                                newHeight > Math.Abs(FromScreenY(ViewBox.MininumHeight))) {
                                newViewBox.Width = newWidth;
                                newViewBox.Height = newHeight;
                                co.ViewBoxDimensions = newViewBox;
                                ViewBoxChangedEvent(null, new CmlChangedEventArgs(co));
                                return;
                            }
                            break;
                        }
                }
            }

            // We're dragging a node.
            if (draggableNodeId != null && draggableNode != null) {
                if (deferredSelectionPending) {
                    if (!deferredSelectionNode.IsSelected) {
                        ProcessDeferedSelection();
                    }
                    deferredSelectionPending = false;
                }
                if (!dragging) {
                    Point p = e.GetPosition(this);
                    if (Math.Abs(dragStart.X - p.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(dragStart.Y - p.Y) > SystemParameters.MinimumVerticalDragDistance) {
                        dragging = true;
                        TakeSnapshotOfContextObject();
                    } else {
                        return;
                    }
                }

                #region FOR EDIT

                // the conversion of coordinates between model (CML) and screen
                // coordinates should be drastically improved.

                // get the position of the sender
                Point mousePos = e.GetPosition(this);
                double currX = FromScreenX(mousePos.X - StandardXOffset);
                double currY = FromScreenY(mousePos.Y - StandardYOffset);

                Point2 old = molecule.GetAtomById(draggableNodeId).Point2;
                Point2 current = new Point2(currX, currY);

                Vector2 vector = current.Subtract(old);

                #endregion FOR EDIT

                List<XElement> pointers = GetSelectedAtomsAsXElements();

                ContextObject co = Cid.TranslateAtomSet2D(ContextObject, pointers, vector);
                NodeCmlChangedEvent(null, new CmlChangedEventArgs(co));

                return;
            }

            draggableNode = null;
            draggableNodeId = null;

            if (!hasMouseDowned) {
                return;
            }

            if (IsPerformingDirectionBondSelect) {
                return;
            }

            if (DrawingMode == CanvasContainer.DrawingMode.Select || DrawingMode == CanvasContainer.DrawingMode.Delete) {
                // We're selecting.
                deferredSelectionPending = false;
                Point currentPoint = e.GetPosition(this);

                Rect selectionRect = new Rect(dragStart, currentPoint);
                selectionGeometry = new RectangleGeometry(selectionRect);

                selectedAtoms.Clear();
                selectedBonds.Clear();

                foreach (KeyValuePair<string, AbstractNodeControl> node in nodes) {
                    CmlAtom atom = node.Value.CmlAtom;

                    Point p = new Point(ToScreenX(atom.X2.Value) + StandardXOffset,
                                        ToScreenY(atom.Y2.Value) + StandardYOffset);

                    if (selectionGeometry.FillContains(p)) {
                        node.Value.IsSelected = true;
                        node.Value.IsActive = false;
                    } else {
                        node.Value.IsSelected = false;
                        node.Value.IsActive = false;
                    }

                    node.Value.Invalidate();
                }

                InvalidateVisual(); // Grrrr, must find better way to do this.
                //Refresh(false);
            }

            if (DrawingMode == CanvasContainer.DrawingMode.BondSelect) {
                deferredBondSelectionPending = false;
                Point currentPoint = e.GetPosition(this);

                Rect selectionRect = new Rect(dragStart, currentPoint);
                selectionGeometry = new RectangleGeometry(selectionRect);

                selectedAtoms.Clear();
                selectedBonds.Clear();

                foreach (object obj in Children) {
                    AbstractEdgeControl edge = obj as AbstractEdgeControl;

                    if (edge != null) {
                        CmlBond bond = edge.Bond;

                        Point p1 = new Point(ToScreenX(edge.StartNode.CmlAtom.X2.Value) + StandardXOffset,
                                             ToScreenY(edge.StartNode.CmlAtom.Y2.Value) + StandardYOffset);
                        Point p2 = new Point(ToScreenX(edge.EndNode.CmlAtom.X2.Value) + StandardXOffset,
                                             ToScreenY(edge.EndNode.CmlAtom.Y2.Value) + StandardYOffset);

                        if (selectionGeometry.FillContains(p1) && selectionGeometry.FillContains(p2)) {
                            edge.IsSelected = true;
                            edge.IsActive = false;
                        } else {
                            edge.IsSelected = false;
                            edge.IsActive = false;
                        }

                        edge.Invalidate();
                    }
                }

                InvalidateVisual(); // Grrrr, must find better way to do this.
                //Refresh(false);
            }
        }

        private void DoRotate(MouseEventArgs e) {
            Point p = e.GetPosition(this);
            Vector vec = p - rotationCentreInCanvasSpace;
            vec.Normalize();

            double newAngle = AngleBetweenNormalisedVectors(initialRotateVector, vec);

            double delta = newAngle - lastAngle;
            Log.Debug(delta);

            Cid.RotateAtomSet2D(ContextObject, GetSelectedAtomsAsXElements(), new Angle(-delta, Angle.Unit.Degrees),
                                rotationCentreInMolSpace);
            lastAngle = newAngle;

            rotate.Angle = newAngle;

            NodeCmlChangedEvent(null, new CmlChangedEventArgs(ContextObject));
        }

        private double AngleBetweenNormalisedVectors(Vector v1, Vector v2) {
            double newAngle = (Math.Atan2(v2.Y, v2.X) - Math.Atan2(v1.Y, v1.X))*180/Math.PI;
            return newAngle;
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);

            if (selectionGeometry != null) {
                Brush b = DrawingMode == CanvasContainer.DrawingMode.Select ||
                          DrawingMode == CanvasContainer.DrawingMode.BondSelect
                              ? Brushes.LightBlue
                              : Brushes.Pink;
                Pen p = DrawingMode == CanvasContainer.DrawingMode.Select ||
                        DrawingMode == CanvasContainer.DrawingMode.BondSelect
                            ? new Pen(Brushes.DarkBlue, 0.3)
                            : new Pen(Brushes.DarkRed, 0.3);

                dc.DrawGeometry(b, p, selectionGeometry);
            }
        }

        internal void DoDirectionalSelect(CmlBond bond, CmlAtom atomInSelectionDirection) {
            foreach (KeyValuePair<string, AbstractNodeControl> node in nodes) {
                node.Value.IsSelected = false;
                node.Value.Invalidate();
            }

            selectedAtoms.Clear();
            selectedBonds.Clear();

            if (bond != null && atomInSelectionDirection != null) {
                IEnumerable<string> downstreamAtomIDs =
                    ChemicalIntelligence.GetDownstreamAtomIds(ContextObject, bond.DelegateElement,
                                                              atomInSelectionDirection.DelegateElement);

                foreach (string iD in downstreamAtomIDs) {
                    if (nodes.ContainsKey(iD)) {
                        nodes[iD].IsSelected = true;
                        nodes[iD].Invalidate();
                    }
                }
            }

            deferredSelectionNode = null;
            deferredSelectionPending = false;
            InvalidateVisual();
            //Refresh(false);
        }

        private void SetBondActions() {
            IEnumerable<string> orders = ChemicalIntelligence.SuggestPossibleBondOrders(ContextObject,
                                                                                        GetSelectedBondsAsXElements());
            IEnumerable<string> stereos = ChemicalIntelligence.SuggestPossibleBondStereos(ContextObject,
                                                                                          GetSelectedBondsAsXElements());
            IEnumerable<string> groups = ChemicalIntelligence.SuggestPossibleBondGroups(ContextObject,
                                                                                        GetSelectedBondsAsXElements());

            bondContextMenu.SetBondOptions(orders, stereos, groups);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e) {
            base.OnMouseUp(e);

            if (Mouse.Captured != null) {
                Mouse.Captured.ReleaseMouseCapture();
            }

            Cursor = Cursors.Arrow;


            ProcessDeferedSelection();
            ProcessDeferedBondSelection();
            SetAtomTypeOptions();
            SetIsotopeOptions();
            SetBondActions();

            Log.Debug("Number of selected atoms : " + SelectedAtoms.Count);
            CommandManager.InvalidateRequerySuggested();
            draggableNodeId = null;
            draggableNode = null;
            viewBoxDraggingPosition = ViewBoxDraggingPosition.None;
            hasMouseDowned = false;
            dragging = false;

            if (chemContextMenu.IsMouseOver || bondContextMenu.IsMouseOver) {
                return;
            }

            if (isRotating) {
                isRotating = false;
                Children.Add(chemContextMenu);
                Children.Remove(selectionRotationTool);

                InvalidateAll();

                return;
            }

            selectionGeometry = null;

            if (DrawingMode == CanvasContainer.DrawingMode.Delete /*&& selectionGeometry != null*/) {
                Log.Debug("Doing MouseUp delete");
                ChemCommands.DeleteSelection.Execute(null, this);
            }

            if (Children.Contains(selectionRotationTool)) {
                Children.Remove(selectionRotationTool);
            }

            if (Children.Contains(chemContextMenu)) {
                Children.Remove(chemContextMenu);
                chemContextMenu.Opacity = 1.0;
            }

            if (Children.Contains(bondContextMenu)) {
                Children.Remove(bondContextMenu);
                bondContextMenu.Opacity = 1.0;
            }
            //claim focus
            Keyboard.Focus(this);
            if (selectedAtoms.Count > 0) {
                if (!Children.Contains(chemContextMenu)) {
                    Point point = e.GetPosition(this);
                    SetLeft(chemContextMenu, point.X - StandardXOffset - chemContextMenu.Width/2);
                    SetTop(chemContextMenu, point.Y - chemContextMenu.Height/2 - StandardYOffset);

                    Children.Add(chemContextMenu);
                }
            }
            if (selectedBonds.Count > 0) {
                if (!Children.Contains(bondContextMenu)) {
                    Rect r = CoordinateTool.GetBounds2D(ContextObject, SelectedBonds.Values);
                    SetLeft(bondContextMenu, e.GetPosition(this).X - StandardXOffset);
                    SetTop(bondContextMenu, ToScreenY(r.Bottom) - bondContextMenu.Height - 25);

                    Children.Add(bondContextMenu);
                }
            }

            InvalidateAll();
        }

        private void SetAtomTypeOptions() {
            if (selectedAtoms.Count == 1) {
                HashSet<PeriodicTable.Element> atoms = ChemicalIntelligence.SuggestPossibleElements(ContextObject,
                                                                                                    SelectedAtoms.
                                                                                                        ToArray()[0].
                                                                                                        Value.
                                                                                                        DelegateElement);
                IEnumerable<string> groups = ChemicalIntelligence.SuggestPossibleGroups(ContextObject,
                                                                                        SelectedAtoms.ToArray()[0].Value
                                                                                            .
                                                                                            DelegateElement);

                //chemContextMenu.SetAtomOptions(atoms, groups);
                OnAtomTypeOptionsUpdated(atoms, groups);
            } else {
                //chemContextMenu.SetAtomOptions(null, null);
                OnAtomTypeOptionsUpdated(null, null);
            }
        }

        private void OnAtomTypeOptionsUpdated(HashSet<PeriodicTable.Element> options, IEnumerable<string> groups) {
            if (AtomTypeOptionsUpdated != null) {
                AtomTypeOptionsUpdated(options, groups);
            }
        }

        private void OnIsotopesOpionsUpdated(IEnumerable<int> isotopes) {
            if (IsotopeOptionsUpdated != null) {
                IsotopeOptionsUpdated(isotopes);
            }
        }

        private void SetIsotopeOptions() {
            if (selectedAtoms.Count == 1) {
                IEnumerable<int> isotopes = ChemicalIntelligence.SuggestPossibleIsotopeNumbers(ContextObject,
                                                                                               SelectedAtoms.ToArray()[0
                                                                                                   ].Value.
                                                                                                   DelegateElement);

                OnIsotopesOpionsUpdated(isotopes);
                //chemContextMenu.SetIsotopeOptions(isotopes);
            } else {
                //chemContextMenu.SetIsotopeOptions(null);
            }
        }

        private void ProcessDeferedBondSelection() {
            if (deferredBondSelectionPending) {
                selectedBonds.Clear();

                if (deferredSelectionBond == null) {
                    Refresh();
                    InvalidateAll();
                } else {
                    deferredSelectionBond.IsSelected = true;
                    deferredSelectionBond.Invalidate();
                }

                deferredBondSelectionPending = false;
                deferredSelectionBond = null;
            }
        }

        private void ProcessDeferedSelection() {
            if (deferredSelectionPending) {
                selectedAtoms.Clear();

                if (deferredSelectionNode == null) {
                    Refresh();
                    InvalidateAll();
                } else {
                    deferredSelectionNode.IsSelected = true;
                    deferredSelectionNode.Invalidate();
                }

                deferredSelectionPending = false;
                deferredSelectionNode = null;
            }
        }

        internal void InvalidateAll() {
            foreach (object child in Children) {
                if (child is AbstractNodeControl) {
                    ((AbstractNodeControl) child).Invalidate();
                    ((AbstractNodeControl) child).InvalidateVisual();
                }
                if (child is AbstractEdgeControl) {
                    ((AbstractEdgeControl) child).Invalidate();
                    ((AbstractEdgeControl) child).InvalidateVisual();
                }
            }

            InvalidateVisual();
        }

        private enum ViewBoxDraggingPosition {
            None,
            West,
            East,
            North,
            South,
            NorthWest,
            NorthEast,
            SouthWest,
            SouthEast
        }

        #endregion
    }

    public delegate void AtomTypeOptionsUpdatedHandler(
        HashSet<PeriodicTable.Element> options, IEnumerable<string> groups);

    public delegate void IsotopeOptionsUpdatedHandler(IEnumerable<int> isotopes);
}