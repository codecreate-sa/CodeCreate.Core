namespace CodeCreate.Messaging
{
    using System.Threading.Tasks;

    using CodeCreate.Events;

    /// <summary>
    /// 
    /// </summary>
    public interface IMessagePump
    {
        /// <summary>
        ///
        /// </summary>
        void Start();

        /// <summary>
        ///
        /// </summary>
        void Stop();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<bool> DispatchAsync(IEvent @event);
    }
}
