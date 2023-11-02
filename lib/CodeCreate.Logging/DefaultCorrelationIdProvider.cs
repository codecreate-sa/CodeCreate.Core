namespace CodeCreate.Logging
{
    /// <summary>
    /// A default implementation for the ICorrelationIdProvider interface.
    /// </summary>
    public class DefaultCorrelationIdProvider : ICorrelationIdProvider
    {
        private string _correlationId { get; set; } = default!;

        /// <summary>
        /// The default constructor
        /// </summary>
        public DefaultCorrelationIdProvider()
        { 
        }

        /// <summary>
        /// The GetCorrelationId method
        /// </summary>
        /// <returns></returns>
        public string GetCorrelationId()
        {
            return _correlationId;
        }

        /// <summary>
        /// The SetCorrelationId method
        /// </summary>
        /// <param name="correlationId"></param>
        public void SetCorrelationId(string correlationId)
        {
            if (string.IsNullOrWhiteSpace(correlationId))
            {
                return;
            }

            _correlationId = correlationId;
        }
    }
}
