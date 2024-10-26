namespace LearningManagementSystem.Utils
{
    public enum ErrorCode
    {
        /// <summary>
        /// Không có lỗi, thực hiện thành công
        /// </summary>
        NoError = 200,
        /// <summary>
        /// Có lỗi, thực hiện không thành công
        /// </summary>
        Error = 400,
        /// <summary>
        /// Lỗi 404, không tìm thấy tài khoản với mật khẩu đã nhập
        /// </summary>
        AccountNotFound = 404,
        Unauthorized = 401
    }

    public static class ErrorCodeExtension
    {
        public static (int code, string message) GetErrorInfo(this ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.Error => (400, "Thực hiện thất bại"),
                ErrorCode.NoError => (200, "Thực hiện thành công"),
                ErrorCode.AccountNotFound => (404, "Sai tên hoặc tài khoản"),
                ErrorCode.Unauthorized => (401, "Không được xác thực"),
                _ => (0, "Lỗi không xác định")
            };
        }
    }
}
