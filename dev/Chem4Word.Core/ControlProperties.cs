// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Tools.Applications.Runtime;

namespace Chem4Word.Core
{
    /// <summary>
    /// Class that stores a list of property values for a saved control.
    /// </summary>
    [Serializable]
    public class ControlProperties
    {
        #region ChemistryControlType enum

        ///<summary>
        ///</summary>
        [Serializable]
        public enum ChemistryControlType
        {
            ///<summary>
            ///</summary>
            DocumentHostControl
        } ;

        #endregion

        private string customXmLid;
        private float height;

        private bool inline;
        private List<KeyValuePair<string, object>> list;
        private string name;
        private ChemistryControlType type;
        private float width;
        private float x;
        private float y;

        /// <summary>
        /// Default constructor - used for de-serialization.
        /// </summary>
        public ControlProperties()
        {}

        /// <summary>
        /// Constructor for floating controls.
        /// </summary>
        /// <param name="name">Control's name</param>
        /// <param name="type">Control's type</param>
        /// <param name="x">Control's X coordinate in points</param>
        /// <param name="y">Control's Y coordinate in points</param>
        /// <param name="width">Control's width in points</param>
        /// <param name="height">Control's height in points</param>
        /// <param name="customXmlid"></param>
        /// <param name="properties">List of control's properties to persist.</param>
        public ControlProperties(string name, ChemistryControlType type, float x, float y, float width, float height,
                                 string customXmlid, params KeyValuePair<string, object>[] properties)
        {
            this.name = name;
            this.type = type;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.CustomXmlId = customXmlid;
            this.list = new List<KeyValuePair<string, object>>();
            this.list.AddRange(properties);
        }

        /// <summary>
        /// Constructor for inline controls.
        /// </summary>
        /// <param name="name">Control's name</param>
        /// <param name="type">Control's type</param>
        /// <param name="start">Start position of control's range (in characters)</param>
        /// <param name="end">End position of control's range (in characters)</param>
        /// <param name="width">Control's width in points</param>
        /// <param name="height">Control's height in points</param>
        /// <param name="customXmlid"></param>
        /// <param name="properties">List of control's properties to persist.</param>
        public ControlProperties(string name, ChemistryControlType type, int start, int end, float width, float height,
                                 string customXmlid, params KeyValuePair<string, object>[] properties)
        {
            this.name = name;
            this.type = type;
            this.x = start;
            this.y = end;
            this.width = width;
            this.height = height;
            this.inline = true;
            this.CustomXmlId = customXmlid;
            this.list = new List<KeyValuePair<string, object>>();

            this.list.AddRange(properties);
        }

        /// <summary>
        /// Constructor for host controls.
        /// </summary>
        /// <param name="name">Control's name</param>
        /// <param name="type">Control's type</param>
        /// <param name="start">Start position of control's range (in characters)</param>
        /// <param name="end">End position of control's range (in characters)</param>
        /// <param name="properties">List of control's properties to persist.</param>
        public ControlProperties(string name, ChemistryControlType type, int start, int end,
                                 params KeyValuePair<string, object>[] properties)
            : this(name, type, start, end, 0, 0, null, properties)
        {}

        /// <summary>
        /// The control's name.
        /// </summary>
        public string ControlName
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The control's type.
        /// </summary>
        public ChemistryControlType ControlType
        {
            get { return type; }
            set { type = value; }
        }

        ///<summary>
        ///</summary>
        public string CustomXmlId
        {
            get { return this.customXmLid; }
            set { this.customXmLid = value; }
        }

        /// <summary>
        /// The control's property list.
        /// </summary>
        public List<KeyValuePair<string, object>> PropertyList
        {
            get { return list; }
            set { list = value; }
        }

        /// <summary>
        /// The control's width in points.
        /// </summary>
        public float ControlWidth
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// The control's height in points.
        /// </summary>
        public float ControlHeight
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// The control's X coordinate in points.
        /// </summary>
        public float ControlX
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// The control's Y coordinate in points.
        /// </summary>
        public float ControlY
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Flag indicating if a control is in-line. If true, the control is in-line;
        /// if foalse, the control is floating.
        /// </summary>
        public bool IsInline
        {
            get { return inline; }
            set { inline = value; }
        }
    }

    /// <summary>
    /// List of control's properties. This class provides an implementation of ICachedType
    /// for complete control of properties' load and save.
    /// </summary>
    [Serializable]
    public class ControlCollection : List<ControlProperties>, ICachedType
    {
        private bool dirty = true;

        /// <summary>
        /// Called before the object is loaded from the cache.
        /// </summary>
        /// <returns>true to continue loading.</returns>
        public bool BeforeLoad()
        {
            return true;
        }

        /// <summary>
        /// Called before the object is saved to the cache.
        /// </summary>
        /// <returns>true to continue saving.</returns>
        public bool BeforeSave()
        {
            return true;
        }

        /// <summary>
        /// Called after the object is loaded from the cache.
        /// </summary>
        public void AfterLoad()
        {
            // Immediately after loading we ae not dirty.
            dirty = false;
        }

        /// <summary>
        /// Called after the object is saved to the cache.
        /// </summary>
        public void AfterSave()
        {
            // Immediately after saving we ae not dirty.
            dirty = false;
        }

        /// <summary>
        /// Return true to indicate that the object is dirty, i.e.
        /// it needs to be saved to the cache.
        /// </summary>
        public bool IsDirty
        {
            get { return dirty; }
            // Give clients the possibility to force saving by setting dirty = value;
            set { dirty = value; }
        }
    }

    /// <summary>
    /// Comparer that will compare ControlProperties objects based on
    /// their position in the document.
    /// </summary>
    public class ControlCollectionComparer : IComparer<ControlProperties>
    {
        /// <summary>
        /// Compares two ControlProperties objects based on their
        /// position in the document.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// Returns -1 if x is earlier in the document than y,
        ///          1 if x is later in the document than y,
        ///       or 0 if they appear in the same position.
        /// </returns>
        public int Compare(ControlProperties x, ControlProperties y)
        {
            if (x.ControlX < y.ControlX)
            {
                return -1;
            }
            return x.ControlX > y.ControlX ? 1 : 0;
        }
    }
}