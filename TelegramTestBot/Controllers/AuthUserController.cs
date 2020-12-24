using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;
using TLSharp.Core.Exceptions;

namespace TelegramTestBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthUserController : ControllerBase
    {
        private readonly IConfiguration _config;

        private string botToken { get; set; }

        private string NumberToSendMessage { get; set; }

        private string NumberToAuthenticate { get; set; }

        private string CodeToAuthenticate { get; set; }

        //private string PasswordToAuthenticate { get; set; }

        private string NotRegisteredNumberToSignUp { get; set; }

        private string UserNameToSendMessage { get; set; }

        private string NumberToGetUserFull { get; set; }

        private string NumberToAddToChat { get; set; }

        private string ApiHash { get; set; }

        private int ApiId { get; set; }

        public object PasswordToAuthenticate { get; private set; }

        private class Assert
        {
            static internal void IsNotNull(object obj)
            {
                IsNotNullHanlder(obj);
            }

            static internal void IsTrue(bool cond)
            {
                IsTrueHandler(cond);
            }
        }

        internal static Action<object> IsNotNullHanlder;
        internal static Action<bool> IsTrueHandler;

        protected AuthUserController(IConfiguration config)
        {
            _config = config;

            ApiId = int.Parse(_config.GetSection("TelegramSettings:api_id").Value);

            ApiHash = _config.GetSection("TelegramSettings:api_hash").Value;

            botToken = _config.GetSection("TelegramSettings:bot_token").Value;
        }

        [HttpGet]
        [Route("{phonenumber}/{code}")]
        public virtual async Task AuthPhoneNumber(string phoneNumber, string code)
        {
            //var client = NewClient();
            var client = NewClient();

            await client.ConnectAsync();

            var hash = await client.SendCodeRequestAsync(phoneNumber);
            //var newCode = await client.get
            //var code = "94904"; // you can change code in debugger

            //var user = await client.MakeAuthAsync("+989372474147", hash, code);

            if (String.IsNullOrWhiteSpace(code))
            {
                throw new Exception("CodeToAuthenticate is empty in the app.config file, fill it with the code you just got now by SMS/Telegram");
            }

            TLUser user = null;
            try
            {
                user = await client.MakeAuthAsync(phoneNumber, hash, code);
            }
            catch (CloudPasswordNeededException ex)
            {
                var passwordSetting = await client.GetPasswordSetting();
                var password = "";

                user = await client.MakeAuthWithPasswordAsync(passwordSetting, password);
            }
            catch (InvalidPhoneCodeException ex)
            {
                throw new Exception("CodeToAuthenticate is wrong in the app.config file, fill it with the code you just got now by SMS/Telegram",
                                    ex);
            }
            Assert.IsNotNull(user);
            Assert.IsTrue(client.IsUserAuthorized());
        }

        private TelegramClient NewClient()
        {
            try
            {
                return new TelegramClient(ApiId, ApiHash);
            }
            catch (MissingApiConfigurationException ex)
            {
                throw new Exception($"Please add your API settings to the `app.config` file. (More info: {MissingApiConfigurationException.InfoUrl})",
                                    ex);
            }
        }
    }
}