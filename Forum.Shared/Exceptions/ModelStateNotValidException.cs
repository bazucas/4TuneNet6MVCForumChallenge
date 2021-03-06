namespace Forum.Shared.Exceptions
{
    /// <summary>
    /// ModelStateNotValidException custom exception, inherits from <see cref="BaseException"/>
    /// </summary>
    /// <seealso cref="BaseException" />
    public class ModelStateNotValidException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateNotValidException"/> class.
        /// </summary>
        public ModelStateNotValidException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateNotValidException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ModelStateNotValidException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateNotValidException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ModelStateNotValidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
