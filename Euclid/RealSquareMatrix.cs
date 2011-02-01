// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Linq;

namespace Euclid
{
    public class RealSquareMatrix
    {
        private readonly double[][] matrix;
        private readonly int rows;

        /// <summary>
        ///  Constructor. This gives a default matrix with cols = rows = 0.
        /// </summary>
        public RealSquareMatrix()
        {}

        /// <summary>
        /// Constructor from array.
        /// </summary>
        /// <param name="matrix">matrix is not square (might even not be rectangular!</param>
        public RealSquareMatrix(double[][] matrix)
        {
            rows = matrix.Count();
            if (matrix[0].Count() != rows)
            {
                throw new ArgumentException("bad matrix");
            }
            this.matrix = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                this.matrix[i] = new double[rows];
                for (int j = 0; j < rows; j++)
                {
                    this.matrix[i][j] = matrix[i][j];
                }
            }
        }

        /**
     * Creates real square matrix from array 
     * THE COLUMN IS THE FASTEST MOVING
     * INDEX, that is the matrix is filled as mat(0,0), mat(0,1) ... C-LIKE
     * 
     * @param rows
     *            the final rows and cols of real square matrix
     * @param array
     *            of size (rows * rows)
     * @exception EuclidRuntimeException
     *                <TT>array</TT> size must be multiple of <TT>rows</TT>
     */

        public RealSquareMatrix(int rows, double[] array)
        {
            this.rows = rows;
            if (array.Count() != rows*rows)
            {
                throw new ArgumentException("bad matrix");
            }
            int k = 0;
            this.matrix = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                this.matrix[i] = new double[rows];
                for (int j = 0; j < rows; j++)
                {
                    matrix[i][j] = array[k++];
                }
            }
        }

        /**
     * determinant. hardcoded up to order 3 at present; rest is VERY slow :-(
     * calls determinant recursively for order > 3
     * 
     * @return the determinant
     */

        public double Determinant()
        {
            double det = 0.0;
            switch (rows)
            {
                case 1:
                    det = this.matrix[0][0];
                    break;
                case 2:
                    det = this.matrix[0][0]*this.matrix[1][1] - this.matrix[1][0]*this.matrix[0][1];
                    break;
                case 3:
                    det = this.matrix[0][0]
                          *(this.matrix[1][1]*this.matrix[2][2] - this.matrix[1][2]*this.matrix[2][1])
                          + this.matrix[0][1]
                            *(this.matrix[1][2]*this.matrix[2][0] - this.matrix[1][0]*this.matrix[2][2])
                          + this.matrix[0][2]
                            *(this.matrix[1][0]*this.matrix[2][1] - this.matrix[1][1]*this.matrix[2][0]);
                    break;
                default:
                {
                    int sign = 1;
                    for (int j = 0; j < rows; j++)
                    {
                        det += sign*this.matrix[0][j]*MinorDet(j);
                        sign = -sign;
                    }
                }
                    break;
            }
            return det;
        }

        private double MinorDet(int i)
        {
            int r = rows - 1;
            double[] array = new double[r*r];
            int countN = 0;
            for (int j = 1; j < rows; j++)
            {
                for (int k = 0; k < rows; k++)
                {
                    if (k != i)
                    {
                        array[countN++] = this.matrix[j][k];
                    }
                }
            }
            RealSquareMatrix mm = null;
            mm = new RealSquareMatrix(r, array);
            double d = mm.Determinant();
            return d;
        }
    }
}