namespace Forum.Shared.Exceptions
{
    /// <summary>
    /// EmptyTopicListException custom exception, inherits from <see cref="Exception"/>
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class EmptyTopicListException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyTopicListException"/> class.
        /// </summary>
        public EmptyTopicListException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyTopicListException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EmptyTopicListException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyTopicListException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public EmptyTopicListException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
