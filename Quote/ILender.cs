#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
namespace LoanApp.Angshuman
{
    /// <summary>
    /// Interface to represent the Lender specific data
    /// </summary>
    public interface ILender
    {
        /// <summary>
        /// Name of the Lender
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Rate specified by the Lender
        /// </summary>
        double Rate { get; set; }

        /// <summary>
        /// Amount of the Lender for the borrower
        /// </summary>
        double ActualAmount { get; set; }

        /// <summary>
        /// Tracking the remaining amount (if any) after giving away the loan 
        /// </summary>
        double RemainingAmount { get; set; }
    }
}