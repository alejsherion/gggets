using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETS.GGGETSApp.Domain.Core.Specification;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace Domain.GGGETS
{
    public class HAWBBarCodeSpecification : Specification<HAWB>
    {
        #region Members

        string _BarCode = default(String);

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="bankAccountNumber">Bank account number for this specification</param>
        public HAWBBarCodeSpecification(string barcode)
        {
            if (string.IsNullOrEmpty(barcode)
                ||
                string.IsNullOrWhiteSpace(barcode))
            {
                throw new ArgumentNullException("barcode");
            }

            _BarCode = barcode;
        }

        #endregion

        #region Specification

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{BankAccount}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Core.Specification.Specification{BankAccount}"/></returns>
        public override System.Linq.Expressions.Expression<Func<HAWB, bool>> SatisfiedBy()
        {
            return ba => ba.BarCode == _BarCode;
        }

        #endregion
    }
}
