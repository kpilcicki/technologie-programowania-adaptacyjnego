using System;
using BusinessLogic.Services;

namespace BusinessLogic.Base
{
    public class DisplayErrorHandler : IErrorHandler
    {
        private IUserInfo _userInfo;

        public DisplayErrorHandler(IUserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        public void HandleError(Exception ex)
        {
            _userInfo.PromptUser(ex.Message, "Error occurred");
        }
    }
}
