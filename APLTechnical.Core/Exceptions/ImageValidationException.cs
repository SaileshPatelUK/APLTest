namespace APLTechnical.Core.Exceptions;

/// <summary>
/// Represents errors that occur during image validation.
/// </summary>
public class ImageValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageValidationException"/> class.
    /// </summary>
    public ImageValidationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageValidationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ImageValidationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageValidationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public ImageValidationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
