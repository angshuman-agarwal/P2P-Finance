#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
namespace LoanApp.Angshuman
{
    /// <summary>
    /// Interface for doing the loan calculation
    /// </summary>
    public interface ILoanProcessor
    {
        /// <summary>
        /// Method for calculating the monthly and total repayments based on the best rate
        /// </summary>
        void ProcessLoan();
    }
}