#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
namespace LoanApp.Angshuman
{
    /// <summary>
    /// Interface for validating a market data. The market data source can be anything from file to webservice or a database
    /// </summary>
    public interface IMarketDataSourceValidator
    {
        bool MarketDataSourceExists();
        bool MarketDataSourceIsValid();
        bool MarketDataSourceIsEmpty();
    }
}