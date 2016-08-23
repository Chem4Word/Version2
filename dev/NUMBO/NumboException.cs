// -----------------------------------------------------------------------
//  Copyright (c) 2011, The Outercurve Foundation.  
//  This software is released under the Apache License, Version 2.0. 
//  The license and further copyright text can be found in the file LICENSE.TXT at
//  the root directory of the distribution.
// -----------------------------------------------------------------------
using System;
using System.Runtime.Serialization;

namespace Numbo
{
    /// <summary>
    /// Base class for Chemistry Exceptions.
    /// </summary>
    [Serializable]
    public class NumboException : Exception
    {
        /// <summary>
        /// Error Message
        /// </summary>
        private string errorMessage = string.Empty;

        /// <summary>
        /// Initializes a new instance of the ChemistryException class.
        /// </summary>
        public NumboException()
        {}

        /// <summary>
        /// Initializes a new instance of the ChemistryException class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NumboException(string message)
            : base(message)
        {
            errorMessage = message;
        }

        /// <summary>
        /// Initializes a new instance of the ChemistryException class with a specified
        /// error message and a reference to the inner exception that is the cause of
        /// this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference
        /// if no inner exception is specified.
        ///</param>
        public NumboException(string message, Exception innerException)
            : base(message, innerException)
        {
            errorMessage = message;
        }

        /// <summary>
        /// Initializes a new instance of the System.Exception class with serialized
        /// data.
        /// </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The System.Runtime.Serialization.StreamingContext that contains contextual
        /// information about the source or destination.
        /// </param>
        protected NumboException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}

        /// <summary>
        /// Property that indicates if the message is a custom message that needs to be shown on the UI
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
        }
    }
}