namespace LearningManagementSystem.Utils
{
    public enum ErrorCode
    {
        /// <summary>
        /// Không có lỗi, thực hiện thành công
        /// </summary>
        NoError = 100,
        /// <summary>
        /// Lỗi 404, không tìm thấy tài khoản với mật khẩu đã nhập
        /// </summary>
        AccountNotFound = 404,

    }

    public static class ErrorCodeExtension
    {
        public static (int code, string message) GetErrorInfo(this ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.NoError => (200, "Thực hiện thành công"),
                ErrorCode.AccountNotFound => (404, "Sai tên hoặc tài khoản"),
                _ => (0, "Lỗi không xác định")
            };
        }
    }
}
