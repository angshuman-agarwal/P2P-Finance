#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace LoanApp.Angshuman
{
    /// <summary>
    /// Responsible for reading and storing the application configuration values
    /// </summary>
    public class MarketDataConfiguration : IMarketDataConfiguration
    {
        /// <summary>
        /// Reads the configurations fromt he app.config file and stores the values. If the config file is missing, then use default values.
        /// </summary>
        public MarketDataConfiguration()
        {
            // check if config file exists
            if (ConfigurationManagerHelper.Exists())
            {
                LoanTenure = GetDoubleOrIntValue<int>(ConfigurationManager.AppSettings["LoanTenure"]);
                RepaymentAmountPrecision =
                    GetDoubleOrIntValue<int>(ConfigurationManager.AppSettings["RepaymentAmountPrecision"]);
                LoanRatePrecision = GetDoubleOrIntValue<int>(ConfigurationManager.AppSettings["LoanRatePrecision"]);
                LoanAmountIncrementor =
                    GetDoubleOrIntValue<int>(ConfigurationManager.AppSettings["LoanAmountIncrementor"]);
                MinimumLoanAmount = GetDoubleOrIntValue<double>(ConfigurationManager.AppSettings["MinimumLoanAmount"]);
                MaximumLoanAmount = GetDoubleOrIntValue<double>(ConfigurationManager.AppSettings["MaximumLoanAmount"]);
                var culture = ConfigurationManager.AppSettings["Culture"];
                Culture = CultureInfo.ReadOnly(new CultureInfo(culture));
            }
            else // read the default values as config file is missing
            {
                LoanTenure = Defaults.Tenure;
                RepaymentAmountPrecision = Defaults.RepaymentAmtPrecision;
                LoanRatePrecision = Defaults.RatePrecision;
                LoanAmountIncrementor = Defaults.LoanAmtMultiple;
                MinimumLoanAmount = Defaults.MinAmt;
                MaximumLoanAmount = Defaults.MaxAmt;
                Culture = CultureInfo.ReadOnly(new CultureInfo(Thread.CurrentThread.CurrentCulture.Name));
            }
        }

        /// <summary>
        /// Helper method to read int / double values from the string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        private static T GetDoubleOrIntValue<T>(string str) where T : IConvertible
        {
            var thisType = default(T);
            var typeCode = thisType.GetTypeCode();
            if (typeCode == TypeCode.Double)
            {
                double d;
                double.TryParse(str, out d);
                return (T)Convert.ChangeType(d, typeCode);
            }
            
            if (typeCode == TypeCode.Int32)
            {
                int i;
                int.TryParse(str, out i);
                return (T)Convert.ChangeType(i, typeCode);
            }

            return thisType;
        }

        public int LoanTenure
        {
            get; private set;
        }

        public int RepaymentAmountPrecision
        {
            get; private set;
        }

        public int LoanRatePrecision
        {
            get; private set;
        }

        public double MinimumLoanAmount
        {
            get; private set;
        }

        public double MaximumLoanAmount
        {
            get; private set;
        }

        public int LoanAmountIncrementor
        {
            get; private set;
        }

        public double BorrowerLoanAmount
        {
            get; set;
        }

        public CultureInfo Culture
        {
            get; private set;
        }
    }

    public static class ConfigurationManagerHelper
    {
        public static bool Exists()
        {
            return Exists(Assembly.GetEntryAssembly());
        }

        public static bool Exists(Assembly assembly)
        {
            return System.IO.File.Exists(assembly.Location + ".config");
        }
    }
}
