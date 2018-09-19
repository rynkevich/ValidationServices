namespace ValidationServices.Results
{
    /// <summary>
    /// Class to report results of a single validation act.
    /// </summary>
    public class ElementaryConclusion : ValidationConclusion
    {
        /// <summary>
        /// Gets a problem report.
        /// </summary>
        public string Details { get; protected set; }

        /// <summary>
        /// Constructor that takes object status and problem details.
        /// </summary>
        /// <param name="isValid">The status of validated object</param>
        /// <param name="details">The report on problem (to be specified only when the object is not valid)</param>
        public ElementaryConclusion(bool isValid, string details = null)
        {
            this.IsValid = isValid;
            this.Details = details;
        }
    }
}
