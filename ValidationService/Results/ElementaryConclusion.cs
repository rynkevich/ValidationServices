namespace ValidationService.Results
{
    public class ElementaryConclusion : ValidationConclusion
    {
        public string Details { get; protected set; }

        public ElementaryConclusion(bool isValid, string details = null)
        {
            this.IsValid = isValid;
            this.Details = details;
        }
    }
}
