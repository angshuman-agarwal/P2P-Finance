#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
namespace LoanApp.Angshuman
{
    /// <summary>
    /// 
    /// </summary>
    public class Lender : ILender
    {
        public string Name { get;  set; }
        public double Rate { get;  set; }
        public double ActualAmount { get;  set; }
        public double RemainingAmount { get;  set; }
    }
}
