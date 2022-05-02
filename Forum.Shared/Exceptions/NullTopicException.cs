namespace Forum.Shared.Exceptions
{
    /// <summary>
    /// NullTopicException custom exception, inherits from <see cref="BaseException"/>
    /// </summary>
    /// <seealso cref="BaseException" />
    public class NullTopicException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullTopicException"/> class.
        /// </summary>
        public NullTopicException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullTopicException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NullTopicException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullTopicException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public NullTopicException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
