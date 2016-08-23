// Created by Gergely István Oroszi - 2015.06.15.
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.  
//   This software is released under the Apache License, Version 2.0. 
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------

using System.Windows;

namespace Chem4Word.Drawing.TwoD.Common
{
    public interface IVisual
    {
        FrameworkElement AsVisual();
    }
}