namespace CodeCreate.Core
{
    /// <summary>
    /// Contains the various result codes that can be returned.
    /// </summary>
    public static class ResultCode
    {
        /// <summary>
        /// Undefined - 0
        /// </summary>
        public const int Undefined = 0;

        /// <summary>
        /// Success - 200
        /// </summary>
        public const int Success = 200;

        /// <summary>
        /// BadRequest - 400
        /// </summary>
        public const int BadRequest = 400;

        /// <summary>
        /// Unauthorized - 401
        /// </summary>
        public const int Unauthorized = 401;

        /// <summary>
        /// Forbidden - 403
        /// </summary>
        public const int Forbidden = 403;

        /// <summary>
        /// NotFound - 404
        /// </summary>
        public const int NotFound = 404;

        /// <summary>
        /// MethodNotAllowed - 405
        /// </summary>
        public const int MethodNotAllowed = 405;

        /// <summary>
        /// Conflict - 409
        /// </summary>
        public const int Conflict = 409;

        /// <summary>
        /// PreconditionFailed - 412
        /// </summary>
        public const int PreconditionFailed = 412;

        /// <summary>
        /// UnsupportedMediaType - 415
        /// </summary>
        public const int UnsupportedMediaType = 415;

        /// <summary>
        /// InternalServerError - 500
        /// </summary>
        public const int InternalServerError = 500;

        /// <summary>
        /// BadGateway - 502
        /// </summary>
        public const int BadGateway = 502;
    }
}
