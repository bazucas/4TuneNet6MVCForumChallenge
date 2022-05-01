namespace Forum.Shared.Exceptions
{
    /// <summary>
    /// InvalidUserException custom exception, inherits from <see cref="Exception"/>
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidUserException : Exception
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
