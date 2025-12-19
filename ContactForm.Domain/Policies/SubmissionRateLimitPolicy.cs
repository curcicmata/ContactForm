namespace ContactForm.Domain.Policies
{
    public static class SubmissionRateLimitPolicy
    {
        public static bool IsAllowed(DateTime lastSubmission, DateTime now)
            => (now - lastSubmission).TotalMinutes >= 1;
    }
}
