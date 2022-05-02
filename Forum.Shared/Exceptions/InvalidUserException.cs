namespace Forum.Shared.Exceptions
{
    /// <summary>
    /// InvalidUserException custom exception, inherits from <see cref="BaseException"/>
    /// </summary>
    /// <seealso cref="BaseException" />
    public class InvalidUserException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserException"/> class.
        /// </summary>
        public InvalidUserException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidUserException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public InvalidUserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
