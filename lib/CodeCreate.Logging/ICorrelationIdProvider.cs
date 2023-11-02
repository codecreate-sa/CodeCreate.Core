namespace CodeCreate.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICorrelationIdProvider
    {
        /// <summary>
        /// The GetCorrelationId method
        /// </summary>
        /// <returns></returns>
        public string? GetCorrelationId();

        /// <summary>
        ///
        /// </summary>
        /// <param name="correlationId"></param>
        public void SetCorrelationId(string correlationId);
    }
}
