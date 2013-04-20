#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
namespace LoanApp.Angshuman
{
    /// <summary>
    /// Validates the borrower's loan amount based on different criteria
    /// </summary>
    public interface ILoanValidator
    {
        /// <summary>
        /// Validates the borrower's loan amount based on :-
        /// 1. Minimum loan amount
        /// 2. Maximum loan amount
        /// 3. Loan amount should be in £100 increment (configurable) between min. and max. inclusive
        /// 4. Market has sufficient offers from lenders to satisfy the loan 
        /// </summary>
        /// <param name="message">Error message in case of a validation failure</param>
        /// <returns>true ot false based on the validation state</returns>
        bool ValidateLoan(out string message);
    }
}