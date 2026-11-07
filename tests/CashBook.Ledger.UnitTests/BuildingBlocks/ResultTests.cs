using CashBook.BuildingBlocks.Results;

namespace CashBook.Ledger.UnitTests.BuildingBlocks;

public sealed class ResultTests
{
    private static readonly Error SampleError = Error.Validation("Sample.Invalid", "Sample is invalid.");

    [Fact]
    public void Success_IsSuccessWithoutError()
    {
        var result = Result.Success();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(Error.None, result.Error);
    }

    [Fact]
    public void Failure_CarriesErrorCode()
    {
        var result = Result.Failure(SampleError);

        Assert.True(result.IsFailure);
        Assert.Equal("Sample.Invalid", result.Error.Code);
        Assert.Equal(ErrorType.Validation, result.Error.Type);
    }

    [Fact]
    public void Failure_WithErrorNone_Throws()
    {
        Assert.Throws<ArgumentException>(() => Result.Failure(Error.None));
    }

    [Fact]
    public void GenericSuccess_ExposesValue()
    {
        var result = Result.Success(42);

        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void GenericFailure_AccessingValue_Throws()
    {
        var result = Result.Failure<int>(SampleError);

        Assert.Equal("Sample.Invalid", result.Error.Code);
        Assert.Throws<InvalidOperationException>(() => result.Value);
    }

    [Fact]
    public void ImplicitConversion_FromValue_IsSuccess()
    {
        Result<string> result = "ok";

        Assert.True(result.IsSuccess);
        Assert.Equal("ok", result.Value);
    }

    [Fact]
    public void ImplicitConversion_FromError_IsFailure()
    {
        Result<string> result = SampleError;

        Assert.True(result.IsFailure);
        Assert.Equal("Sample.Invalid", result.Error.Code);
    }

    [Theory]
    [InlineData(ErrorType.Failure)]
    [InlineData(ErrorType.Validation)]
    [InlineData(ErrorType.NotFound)]
    [InlineData(ErrorType.Conflict)]
    public void ErrorFactories_SetType(ErrorType type)
    {
        var error = type switch
        {
            ErrorType.Validation => Error.Validation("X.Code", "m"),
            ErrorType.NotFound => Error.NotFound("X.Code", "m"),
            ErrorType.Conflict => Error.Conflict("X.Code", "m"),
            _ => Error.Failure("X.Code", "m"),
        };

        Assert.Equal(type, error.Type);
        Assert.Equal("X.Code", error.Code);
    }
}
