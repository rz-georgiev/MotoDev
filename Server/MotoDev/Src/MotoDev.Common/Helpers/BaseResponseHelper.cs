using MotoDev.Common.Dtos;

public static class ResponseHelper
{
    public static BaseResponse<T> Success<T>(T result)
    {
        return new BaseResponse<T>
        {
            IsOk = true,
            Result = result
        };
    }

    public static BaseResponse<T> Success<T>(T result, string message)
    {
        return new BaseResponse<T>
        {
            IsOk = true,
            Result = result,
            Message = message
        };
    }

    public static BaseResponse Success(string message = "")
    {
        return new BaseResponse
        {
            IsOk = true,
            Message = message
        };
    }

    public static BaseResponse Failure(string message)
    {
        return new BaseResponse
        {
            IsOk = false,
            Message = message
        };
    }

    public static BaseResponse<T> Failure<T>(T result, string message)
    {
        return new BaseResponse<T>
        {
            IsOk = false,
            Message = message,
            Result = result
       
        
        };
    }

    public static BaseResponse<T> Failure<T>(T result)
    {
        return new BaseResponse<T>
        {
            IsOk = false,
            Result = result
        };
    }
}