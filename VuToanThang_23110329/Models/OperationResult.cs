namespace VuToanThang_23110329.Models
{
    /// <summary>
    /// Represents the result of an operation with success status and message
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Indicates whether the operation was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Message describing the result of the operation
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Optional data returned by the operation
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Creates a new OperationResult instance
        /// </summary>
        public OperationResult()
        {
            Success = false;
            Message = string.Empty;
        }

        /// <summary>
        /// Creates a new OperationResult with specified success status and message
        /// </summary>
        /// <param name="success">Success status</param>
        /// <param name="message">Result message</param>
        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// Creates a new OperationResult with specified success status, message and data
        /// </summary>
        /// <param name="success">Success status</param>
        /// <param name="message">Result message</param>
        /// <param name="data">Result data</param>
        public OperationResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Creates a successful operation result
        /// </summary>
        /// <param name="message">Success message</param>
        /// <returns>OperationResult with Success = true</returns>
        public static OperationResult CreateSuccess(string message = "Operation completed successfully")
        {
            return new OperationResult(true, message);
        }

        /// <summary>
        /// Creates a successful operation result with data
        /// </summary>
        /// <param name="data">Result data</param>
        /// <param name="message">Success message</param>
        /// <returns>OperationResult with Success = true and data</returns>
        public static OperationResult CreateSuccess(object data, string message = "Operation completed successfully")
        {
            return new OperationResult(true, message, data);
        }

        /// <summary>
        /// Creates a failed operation result
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns>OperationResult with Success = false</returns>
        public static OperationResult CreateFailure(string message)
        {
            return new OperationResult(false, message);
        }

        /// <summary>
        /// Creates a failed operation result with data
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="data">Error data</param>
        /// <returns>OperationResult with Success = false and data</returns>
        public static OperationResult CreateFailure(string message, object data)
        {
            return new OperationResult(false, message, data);
        }
    }

    /// <summary>
    /// Generic version of OperationResult with strongly typed data
    /// </summary>
    /// <typeparam name="T">Type of the result data</typeparam>
    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// Strongly typed data returned by the operation
        /// </summary>
        public new T Data { get; set; }

        /// <summary>
        /// Creates a new generic OperationResult instance
        /// </summary>
        public OperationResult() : base()
        {
        }

        /// <summary>
        /// Creates a new generic OperationResult with specified success status and message
        /// </summary>
        /// <param name="success">Success status</param>
        /// <param name="message">Result message</param>
        public OperationResult(bool success, string message) : base(success, message)
        {
        }

        /// <summary>
        /// Creates a new generic OperationResult with specified success status, message and data
        /// </summary>
        /// <param name="success">Success status</param>
        /// <param name="message">Result message</param>
        /// <param name="data">Strongly typed result data</param>
        public OperationResult(bool success, string message, T data) : base(success, message)
        {
            Data = data;
        }

        /// <summary>
        /// Creates a successful generic operation result
        /// </summary>
        /// <param name="data">Result data</param>
        /// <param name="message">Success message</param>
        /// <returns>OperationResult with Success = true and typed data</returns>
        public static OperationResult<T> CreateSuccess(T data, string message = "Operation completed successfully")
        {
            return new OperationResult<T>(true, message, data);
        }

        /// <summary>
        /// Creates a failed generic operation result
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns>OperationResult with Success = false</returns>
        public static new OperationResult<T> CreateFailure(string message)
        {
            return new OperationResult<T>(false, message);
        }
    }
}
