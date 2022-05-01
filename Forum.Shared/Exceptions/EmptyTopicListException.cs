namespace Forum.Shared.Exceptions
{
    public class EmptyTopicListException : Exception
    {
        public EmptyTopicListException()
        {
        }

        public EmptyTopicListException(string message)
            : base(message)
        {
        }

        public EmptyTopicListException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
