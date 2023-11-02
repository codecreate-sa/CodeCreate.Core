namespace CodeCreate.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultCorrelationIdProvider : ICorrelationIdProvider
    {
        private string? _correlationId;

        /// <summary>
        /// 
        /// </summary>
        public DefaultCorrelationIdProvider()
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string? GetCorrelationId()
        {
            return _correlationId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="correlationId"></param>
        public void SetCorrelationId(string? correlationId)
        {
            if (string.IsNullOrWhiteSpace(correlationId))
            {
                return;
            }

            _correlationId = correlationId;
        }
    }
}
