namespace GeekStore.Core.Results
{
    public abstract class Result<TResult>
    {
        public Result(bool succeeded, TResult data)
        {
            Succeeded = succeeded;
            Data = data;
        }

        public bool Succeeded { get; }
        public TResult Data { get; }
    }

    public class FailResult<TResult> : Result<TResult>
    {
        public FailResult(TResult data = default) : base(false, data)
        { }
    }

    public class SuccessResult<TResult> : Result<TResult>
    {
        public SuccessResult(TResult data) : base(true, data)
        { }
    }
}
