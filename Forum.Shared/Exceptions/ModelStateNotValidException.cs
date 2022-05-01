namespace Forum.Shared.Exceptions
{
    public class ModelStateNotValidException : Exception
    {
        public ModelStateNotValidException()
        {
        }

        public ModelStateNotValidException(string message)
            : base(message)
        {
        }

        public ModelStateNotValidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
