namespace Forum.Shared.Exceptions
{
    public class NullTopicException : Exception
    {
        public NullTopicException()
        {
        }

        public NullTopicException(string message)
            : base(message)
        {
        }

        public NullTopicException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
