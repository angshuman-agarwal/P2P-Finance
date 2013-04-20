#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuoteTest
{
    /// <summary>
    /// Helper class used for testing for specific exceptions in unit tests.
    /// </summary>
    public static class ExceptionAssert
    {
        /// <summary>
        /// Asserts that the given action throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of exception that is expected to be thrown.</typeparam>
        /// <param name="action">Action to execute.</param>
        /// <returns>The thrown exception.</returns>
        public static T Throws<T>(Action action) where T : Exception
        {
            try
            {
                action();
            }
            catch (T ex)
            {
                return ex;
            }

            Assert.Fail("Expected exception of type {0}.", typeof(T));
            return null;
        }
    }
}
