namespace Forum.Shared.Exceptions
{
    /// <summary>
    /// BaseException extends Exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class BaseException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="messageForUser">The message for user.</param>
        public BaseException(string messageForUser = "")
        {
            MessageForUser = messageForUser;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageForUser">The message for user.</param>
        public BaseException(string message, string messageForUser = "")
            : base(message)
        {
            MessageForUser = messageForUser;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        /// <param name="messageForUser">The message for user.</param>
        public BaseException(string message, Exception inner, string messageForUser = "")
            : base(message, inner)
        {
            MessageForUser = messageForUser;
        }

        /// <summary>
        /// Gets or sets the message for user.
        /// </summary>
        /// <value>
        /// The message for user.
        /// </value>
        public string MessageForUser { get; set; }

    }
}