namespace QrGen.Domain.Helpers
{
    public class Result
    {
        public bool IsSuccess => Errors.Count == 0;
        public bool IsFailure => !IsSuccess;
        public List<string> Errors { get; private set; } = [];

        public static Result Success() => new();
        public static Result Failure(params string[] errors) =>
            new() { Errors = [.. errors] };
        public static Result Failure(IEnumerable<string> errors) =>
            new() { Errors = [.. errors] };

    }

    public class Result<T> : Result
    {
        public T? Value { get; private set; }

        private Result(T? value)
        {
            Value = value;
        }

        private Result(List<string> list)
        {
            Errors.AddRange(list);
        }

        public static Result<T> Success(T value) => new(value);
        public static new Result<T> Failure(params string[] errors) =>
            new([.. errors]);
        public static new Result<T> Failure(IEnumerable<string> errors) =>
            new(errors.ToList());
    }
}
