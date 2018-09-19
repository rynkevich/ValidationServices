namespace ValidationServices.Results
{
    /// <summary>
    /// Class to report status of validated object.
    /// <para>Should be extended to provide more information on validation act.</para>
    /// </summary>
    public abstract class ValidationConclusion
    {
        /// <summary>
        /// Gets a validated object status.
        /// </summary>
        public bool IsValid { get; protected set; }
    }
}
