namespace CodeCreate.Logging
{
    /// <summary>
    /// The ICorrelationIdProvider interface
    /// </summary>
    public interface ICorrelationIdProvider
    {
        /// <summary>
        /// The GetCorrelationId method
        /// </summary>
        /// <returns></returns>
        public string? GetCorrelationId();

        /// <summary>
        /// The SetCorrelationId method
        /// </summary>
        /// <param name="correlationId"></param>
        public void SetCorrelationId(string? correlationId);
    }
}
