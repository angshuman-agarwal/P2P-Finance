#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
namespace LoanApp.Angshuman
{
    /// <summary>
    /// This class represent the default values for loan calculation parameters as stated in the requirements
    /// </summary>
    public sealed class Defaults
    {
        public const int Tenure = 36;
        public const int RepaymentAmtPrecision = 2;
        public const int RatePrecision = 1;
        public const int LoanAmtMultiple = 100;
        public const double MinAmt = 1000.0;
        public const double MaxAmt = 15000.0;
    }
}
