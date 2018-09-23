using System.Collections.Generic;
using System.Linq;

namespace ValidationServices.Results
{
    /// <summary>
    /// Class to report results of multiple validation acts.
    /// It accumulates elementary conclusions to give the final verdict.
    /// </summary>
    public class ServiceConclusion : ValidationConclusion
    {
        /// <summary>
        /// Gets a report on problems which led to invalid status of the validated object.
        /// </summary>
        public List<string> Details { get; protected set; }

        /// <summary>
        /// Constructor that takes object status and problem details.
        /// </summary>
        /// <param name="isValid">The status of validated object</param>
        /// <param name="details">The report on problems (to be specified only when the object is not valid)</param>
        public ServiceConclusion(bool isValid, List<string> details = null)
        {
            this.IsValid = isValid;
            this.Details = details;
        }

        /// <summary>
        /// Overloaded operator that helps to merge two instances of <see cref="ServiceConclusion"/>
        /// </summary>
        /// <param name="a">First conclusion</param>
        /// <param name="b">Second conclusion</param>
        /// <returns>
        /// <see cref="ServiceConclusion"/> which contains a logical sum of <c>IsValid</c> flags of operands
        /// and problem details of both conclusions.
        /// </returns>
        public static ServiceConclusion operator +(ServiceConclusion a, ServiceConclusion b)
        {
            bool isValid = a.IsValid && b.IsValid;

            if (a.Details == null && b.Details == null)
            {
                return new ServiceConclusion(isValid);
            }

            if (a.Details == null)
            {
                return new ServiceConclusion(isValid, new List<string>(b.Details));
            }
            else if (b.Details == null)
            {
                return new ServiceConclusion(isValid, new List<string>(a.Details));
            }
            else
            {
                return new ServiceConclusion(isValid, a.Details.Concat(b.Details).ToList<string>());
            }
        }

        /// <summary>
        /// Overloaded operator that helps to merge instance of <see cref="ElementaryConclusion"/>
        /// with instance of <see cref="ServiceConclusion"/>
        /// </summary>
        /// <param name="a">A general conclusion</param>
        /// <param name="b">An elementary conclusion</param>
        /// <returns>
        /// <see cref="ServiceConclusion"/> which contains a logical sum of <c>IsValid</c> flags of operands
        /// and problem details of both conclusions.
        /// </returns>
        public static ServiceConclusion operator +(ServiceConclusion a, ElementaryConclusion b)
        {
            bool isValid = a.IsValid && b.IsValid;
            List<string> details = null;

            if (a.Details != null)
            {
                details = new List<string>(a.Details);
            }

            if (b.Details != null)
            {
                details = details ?? new List<string>();
                details.Add(b.Details);
            }

            return new ServiceConclusion(isValid, details);
        }
    }
}
