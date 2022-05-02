namespace Forum.Shared.Exceptions
{
    /// <summary>
    /// InvalidIdException custom exception, inherits from <see cref="BaseException"/>
    /// </summary>
    /// <seealso cref="BaseException" />
    public class InvalidIdException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidIdException"/> class.
        /// </summary>
        public InvalidIdException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidIdException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidIdException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidIdException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public InvalidIdException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
