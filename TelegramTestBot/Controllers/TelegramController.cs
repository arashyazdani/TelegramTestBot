using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Updates;
using TLSharp.Core;
using TLSharp.Core.Exceptions;

namespace TelegramTestBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelegramController : ControllerBase
    {
        private readonly IConfiguration _config;

        private static TelegramBotClient _myBot = new TelegramBotClient("1457576836:AAE8TbGEdNVfhyp7OdIpxtQXSHjia3XbiFM");

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

        public TelegramController(IConfiguration config)
        {
            _config = config;

            ApiId = int.Parse(_config.GetSection("TelegramSettings:api_id").Value);

            ApiHash = _config.GetSection("TelegramSettings:api_hash").Value;

            botToken = _config.GetSection("TelegramSettings:bot_token").Value;
        }

        [HttpGet]
        public async Task<ActionResult> TelegramSendTestMessageAsync()
        {
            try
            {
                //var aa = _config.GetSection("TelegramSettings");
                //ApiId = int.Parse(_config["TelegramSettings:api_hash"]);
                //ApiId = int.Parse(_config.GetSection("TelegramSettings:api_id").Value);

                //ApiHash = _config.GetSection("TelegramSettings:api_hash").Value;

                var client = NewClient();

                var newBot = NewBotClient();

                //var client = new TelegramClient(ApiId, ApiHash);

                //var client = NewClient();

                //var client = new TelegramClient(int.Parse(_config.GetSection("TelegramSettings:api_id").Value), _config.GetSection("TelegramSettings:api_hash").Value);
                await client.ConnectAsync();

                if (!client.IsUserAuthorized())
                {
                    //var hash = await client.SendCodeRequestAsync("+989372474147");
                    //var code = "12154"; // you can change code in debugger

                    //var user = await client.MakeAuthAsync("+989372474147", hash, code);
                    //var user = await client.MakeAuthAsync("<user_number>", hash, code);
                    await AuthUser();
                    //user = result.Users
                    //.OfType<TLUser>()
                    //.FirstOrDefault(x => x.Phone == "989107790912");

                    //if (user == null)
                    //{
                    //    throw new System.Exception("Number was not found in Contacts List of user: " + "+989107790912");
                    //}

                    //await client.SendTypingAsync(new TLInputPeerUser() { UserId = user.Id });
                    //Thread.Sleep(3000);
                    //await client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, "TEST");
                    //return Ok();
                }
                var result = await client.GetContactsAsync();

                var session = client.Session;
                //var user1 = await client.MakeAuthAsync("+989372474147", session.GetHashCode().ToString(), session.SessionUserId);

                var user1 = result.Users
                    .OfType<TLUser>()
                    .FirstOrDefault(x => x.Phone == "989107790912");

                if (user1 == null)
                {
                    throw new System.Exception("Number was not found in Contacts List of user: " + "+989107790912");
                }

                await client.SendTypingAsync(new TLInputPeerUser() { UserId = user1.Id });
                //Thread.Sleep(3000);
                await client.SendMessageAsync(new TLInputPeerUser() { UserId = user1.Id }, "TEST");

                var dialogs = (TLDialogs)await client.GetUserDialogsAsync();

                var a = newBot.GetUpdatesAsync();
                var chat = dialogs.Chats
                    .OfType<TLChannel>()
                    .FirstOrDefault(c => c.Title == "Test Group");
                //await newBot.SendTextMessageAsync(chat.Id, "Test Bot");
                //await newBot.SendTextMessageAsync("s1209103133_9102905055035361196", "Test Bot");
                await newBot.SendTextMessageAsync("-s1209103133_9102905055035361196", "Test Bot");
                await newBot.SendTextMessageAsync("-" + chat.Id.ToString(), "Test Bot");

                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [HttpGet]
        public async Task<ActionResult> TelegramSendTestMessageAsync(string id)
        {
            try
            {
                var client = NewClient();

                var newBot = NewBotClient();

                await client.ConnectAsync();

                if (!client.IsUserAuthorized())
                {
                    await AuthUser();
                }
                var result = await client.GetContactsAsync();

                var dialogs = (TLDialogs)await client.GetUserDialogsAsync();

                var a = newBot.GetUpdatesAsync();
                var chat = dialogs.Chats
                    .OfType<TLChannel>()
                    .FirstOrDefault(c => c.Title == "Test Group");
                await newBot.SendTextMessageAsync(id, "Test Bot");

                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("grouptest")]
        public async Task<ActionResult> TelegramGroupSendTestMessageAsync()
        {
            try
            {
                var client = NewClient();
                ////var client = new TelegramClient(int.Parse(_config.GetSection("TelegramSettings:api_id").Value), _config.GetSection("TelegramSettings:api_hash").Value);
                //await client.ConnectAsync();
                //var result = await client.GetContactsAsync();

                //if (!client.IsUserAuthorized())
                //{
                //    var hash = await client.SendCodeRequestAsync("+989372474147");
                //    var code = "12154"; // you can change code in debugger

                //    var user = await client.MakeAuthAsync("+989372474147", hash, code);
                //    //var user = await client.MakeAuthAsync("<user_number>", hash, code);
                //    user = result.Users
                //    .OfType<TLUser>()
                //    .FirstOrDefault(x => x.Phone == "989107790912");

                //    if (user == null)
                //    {
                //        throw new System.Exception("Number was not found in Contacts List of user: " + "+989107790912");
                //    }

                //    await client.SendTypingAsync(new TLInputPeerUser() { UserId = user.Id });
                //    Thread.Sleep(3000);
                //    await client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, "TEST");
                //    return Ok();
                //}
                //var session = client.Session;
                ////var user1 = await client.MakeAuthAsync("+989372474147", session.GetHashCode().ToString(), session.SessionUserId);

                //var user1 = result.Users
                //    .OfType<TLUser>()
                //    .FirstOrDefault(x => x.Phone == "989107790912");

                //if (user1 == null)
                //{
                //    throw new System.Exception("Number was not found in Contacts List of user: " + "+989107790912");
                //}

                //await client.SendTypingAsync(new TLInputPeerUser() { UserId = user1.Id });
                //Thread.Sleep(3000);
                //await client.SendMessageAsync(new TLInputPeerUser() { UserId = user1.Id }, "TEST");

                await client.ConnectAsync();

                var dialogs = (TLDialogs)await client.GetUserDialogsAsync();
                var chat = dialogs.Chats
                    .OfType<TLChannel>()
                    .FirstOrDefault(c => c.Title == "Test Group");

                await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value }, "TEST MSG");
                var peer = new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value };

                //var requestGetAllChats = await client.GetHistoryAsync(new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value }, 0, 0, 1, 1,1,1);
                var requestGetAllChats = await client.GetHistoryAsync(peer, 0, -1, 1, 100, 10, 1);
                string[] sss = new string[10];

                //for (int i = 7; i >= 7; i--)
                //{
                //    sss[i] = await client.GetHistoryAsync(peer,0,0,1).Result();
                //}

                return Ok(requestGetAllChats);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("groupgetmessages")]
        public async Task<ActionResult<string[]>> TelegramGroupGetMessagesTestAsync()
        {
            try
            {
                var client = NewClient();

                await client.ConnectAsync();

                var dialogs = (TLDialogs)await client.GetUserDialogsAsync();

                var chat = dialogs.Chats
                    .OfType<TLChannel>()
                    .FirstOrDefault(c => c.Title == "Test Group");
                var sss = new List<string>();
                //int i = 0;

                ////foreach (var dia in dialogs.Dialogs.Where(x => x.Peer is TLPeerChannel && x.UnreadCount > 0))
                ////{
                ////var peer = dia.Peer as TLPeerChannel;
                ////var target = new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = (long)chat.AccessHash };
                //var target = new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value };
                ////var dia = dialogs.Dialogs.FirstOrDefault(x => x.Peer = target);

                //var hist = await client.GetHistoryAsync(target, 0, -1, dia.UnreadCount);

                ////Console.WriteLine("=====================================================================");
                ////Console.WriteLine("THIS IS:" + chat.Title + " WITH " + dia.UnreadCount + " UNREAD MESSAGES");

                //foreach (var m in (hist as TLChannelMessages).Messages)
                //{
                //    if ((m as TLMessage) != null)
                //    {
                //        sss[i] = (m as TLMessage).Message.ToString();
                //    }
                //    i++;
                //}
                ////}
                ///

                foreach (var dia in dialogs.Dialogs.Where(x => x.Peer is TLPeerChannel))
                {
                    var peer = dia.Peer as TLPeerChannel;
                    var chat1 = dialogs.Chats.OfType<TLChannel>().First(x => x.Id == peer.ChannelId);
                    var target = new TLInputPeerChannel() { ChannelId = chat1.Id, AccessHash = (long)chat1.AccessHash };
                    if (target.ChannelId == chat.Id)
                    {
                        var hist = await client.GetHistoryAsync(target, 0, -1, 0, dia.UnreadCount, dia.TopMessage + 1, dia.ReadInboxMaxId);

                        //Console.WriteLine("=====================================================================");
                        //Console.WriteLine("THIS IS:" + chat.Title + " WITH " + dia.UnreadCount + " UNREAD MESSAGES");

                        foreach (var m in (hist as TLChannelMessages).Messages)
                        {
                            if (m.GetType().ToString().Equals("TeleSharp.TL.TLMessage"))
                            {
                                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                                dtDateTime = dtDateTime.AddSeconds((m as TLMessage).Date).ToLocalTime();
                                TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
                                double unixTime = span.TotalMilliseconds;
                                DateTime dtDateTime1 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                                dtDateTime1 = dtDateTime1.AddMilliseconds(unixTime).ToLocalTime();
                                long unixTimeStampInTicks = (dtDateTime.ToUniversalTime() - dtDateTime).Ticks;

                                sss.Add((m as TLMessage).Message.ToString() + ", Date:" + (unixTimeStampInTicks / TimeSpan.TicksPerSecond) + ", ReplyToMsgId:" + (m as TLMessage).ReplyToMsgId + ", newDate:" + dtDateTime + ", unixTime:" + dtDateTime1);
                                //sss[i] = (m as TLMessage).Message.ToString();
                            }
                            //i++;
                        }
                    }
                }
                //if (!client.IsUserAuthorized())
                //{
                //}

                //foreach (var dia in dialogs.Dialogs.Where(x => x.Peer is TLPeerChannel && x.UnreadCount > 0))
                //{
                //    var peer = dia.Peer as TLPeerChannel;
                //    var target = new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = (long)chat.AccessHash };
                //    var hist = await client.GetHistoryAsync(target, 0, -1, dia.UnreadCount);

                //    //Console.WriteLine("=====================================================================");
                //    //Console.WriteLine("THIS IS:" + chat.Title + " WITH " + dia.UnreadCount + " UNREAD MESSAGES");

                //    foreach (var m in (hist as TLChannelMessages).Messages)
                //    {
                //        if (m.GetType().ToString().Equals("TeleSharp.TL.TLMessage"))
                //        {
                //            sss[i] = (m as TLMessage).Message.ToString();
                //        }
                //        //if ((m as TLMessage) != null)
                //        //{
                //        //    sss[i] = (m as TLMessage).Message.ToString();
                //        //}
                //        i++;
                //    }
                //}

                return Ok(sss.ToList());
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("testreplymessages")]
        public async Task<ActionResult<string[]>> TelegramReplyMessagesTestAsync()
        {
            try
            {
                var client = NewClient();

                await client.ConnectAsync();

                var dialogs = (TLDialogs)await client.GetUserDialogsAsync();

                var chat = dialogs.Chats
                    .OfType<TLChannel>()
                    .FirstOrDefault(c => c.Title == "Test Group");
                var mesagesList = new List<string>();

                bool readingMessages = true;

                while (readingMessages)
                {
                    foreach (var dia in dialogs.Dialogs.Where(x => x.Peer is TLPeerChannel))
                    {
                        var peer = dia.Peer as TLPeerChannel;
                        var chat1 = dialogs.Chats.OfType<TLChannel>().First(x => x.Id == peer.ChannelId);
                        var target = new TLInputPeerChannel() { ChannelId = chat1.Id, AccessHash = (long)chat1.AccessHash };
                        if (target.ChannelId == chat.Id)
                        {
                            var hist = await client.GetHistoryAsync(target, 0, -1, 0);

                            foreach (var m in (hist as TLChannelMessages).Messages)
                            {
                                if (m.GetType().ToString().Equals("TeleSharp.TL.TLMessage"))
                                {
                                    mesagesList.Add((m as TLMessage).Message.ToString() + "," + (m as TLMessage).Date.ToString());
                                    if ((m as TLMessage).Message.ToString().StartsWith("02"))
                                    {
                                        //await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value }, "TEST MSG");
                                        var send = new TLRequestSendMessage //Make a request
                                        {
                                            Peer = new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value },
                                            Message = "خرید",
                                            //ReplyToMsgId = (m as TLMessage).Id
                                            ReplyToMsgId = (m as TLMessage).Id,
                                            RandomId = (m as TLMessage).Id
                                        };

                                        //await client.SendRequestAsync<TLRequestSendMessage>(send); //Send a request`
                                        await client.SendRequestAsync<TLUpdates>(send); //Send a request`
                                    }
                                    else if ((m as TLMessage).Message.ToString().StartsWith("03"))
                                    {
                                        var send = new TLRequestSendMessage //Make a request
                                        {
                                            Peer = new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value },
                                            Message = "فروش",
                                            ReplyToMsgId = (m as TLMessage).Id,
                                            RandomId = (m as TLMessage).Id
                                        };

                                        //await client.SendRequestAsync<TLRequestSendMessage>(send); //Send a request`
                                        await client.SendRequestAsync<TLUpdates>(send); //Send a request`
                                    }
                                    else if ((m as TLMessage).Message.ToString() == "01STOP")
                                    {
                                        return Ok(mesagesList.ToList());
                                    }
                                }
                            }
                        }
                    }
                    Thread.Sleep(300);
                }

                return Ok(mesagesList.ToList());
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("checknewmessages")]
        public async Task<ActionResult<string[]>> TelegramCheckNewMessagesAsync()
        {
            try
            {
                var client = NewClient();

                await client.ConnectAsync();

                var dialogs = (TLDialogs)await client.GetUserDialogsAsync();

                var chats = dialogs.Chats.OfType<TLChannel>();

                var chat1 = chats.FirstOrDefault(c => c.Title == "Test Group");
                var chat2 = chats.FirstOrDefault(c => c.Title == "TestRecieveMessages");

                //var chat = dialogs.Chats.OfType<TLChannel>().FirstOrDefault(c => c.Title == "Test Group");

                //var chat2 = dialogs.Chats.OfType<TLChannel>().FirstOrDefault(c => c.Title == "TestRecieveMessages");

                var chat3 = chats.FirstOrDefault(c => c.Title == "TestRecieveMessages");

                bool readingMessages = true;
                var _lastMessageStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                while (readingMessages)
                {
                    var state = await client.SendRequestAsync<TLState>(new TLRequestGetState());
                    var req = new TLRequestGetDifference() { Date = state.Date, Pts = state.Pts, Qts = state.Qts };
                    var adiff = await client.SendRequestAsync<TLAbsDifference>(req);
                    if (!(adiff is TLDifferenceEmpty))
                    {
                        if (adiff is TLDifference)
                        {
                            var diff = adiff as TLDifference;
                            string resultMessage = "chats:" + diff.Chats.Count + ", " + "encrypted:" + diff.NewEncryptedMessages.Count + ", " + "new:" + diff.NewMessages.Count + ", " + "user:" + diff.Users.Count + ", " + "other:" + diff.OtherUpdates.Count;

                            //Console.WriteLine("chats:" + diff.Chats.Count);
                            //Console.WriteLine("encrypted:" + diff.NewEncryptedMessages.Count);
                            //Console.WriteLine("new:" + diff.NewMessages.Count);
                            //Console.WriteLine("user:" + diff.Users.Count);
                            //Console.WriteLine("other:" + diff.OtherUpdates.Count);

                            //await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat2.Id, AccessHash = chat2.AccessHash.Value }, resultMessage);

                            foreach (var upd in diff.OtherUpdates.OfType<TLUpdateNewChannelMessage>())
                            {
                                resultMessage = (upd.Message as TLMessage).Message + ", ReplyToMsgId:" + (upd.Message as TLMessage).ReplyToMsgId + ", ReplyMarkup:" + (upd.Message as TLMessage).ReplyMarkup;
                                await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat2.Id, AccessHash = chat2.AccessHash.Value }, resultMessage);
                                if ((upd.Message as TLMessage).Message.ToString().StartsWith("02") || (upd.Message as TLMessage).Message.ToString().StartsWith("۰۲"))
                                {
                                    //await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value }, "TEST MSG");
                                    var send = new TLRequestSendMessage //Make a request
                                    {
                                        Peer = new TLInputPeerChannel() { ChannelId = chat1.Id, AccessHash = chat1.AccessHash.Value },
                                        Message = "خرید",
                                        ReplyToMsgId = (upd.Message as TLMessage).Id,
                                        RandomId = (upd.Message as TLMessage).Id
                                    };

                                    await client.SendRequestAsync<TLUpdates>(send); //Send a request`
                                }
                                else if ((upd.Message as TLMessage).Message.ToString().StartsWith("03") || (upd.Message as TLMessage).Message.ToString().StartsWith("۰۳"))
                                {
                                    var send = new TLRequestSendMessage //Make a request
                                    {
                                        Peer = new TLInputPeerChannel() { ChannelId = chat1.Id, AccessHash = chat1.AccessHash.Value },
                                        Message = "فروش",
                                        ReplyToMsgId = (upd.Message as TLMessage).Id,
                                        RandomId = (upd.Message as TLMessage).Id
                                    };

                                    await client.SendRequestAsync<TLUpdates>(send); //Send a request`
                                }

                                //var ich = new TLInputChannel() { ChannelId = chat1.Id, AccessHash = (long)chat1.AccessHash };
                                //var readed = new TeleSharp.TL.Channels.TLRequestReadHistory() { Channel = ich, MaxId = -1 };
                                //Console.WriteLine((upd.Message as TLMessage).Message);
                            }

                            //foreach (var ch in diff.Chats.OfType<TLChannel>().Where(x => !x.Left))
                            //{
                            //    var ich = new TLInputChannel() { ChannelId = ch.Id, AccessHash = (long)ch.AccessHash };
                            //    var readed = new TeleSharp.TL.Channels.TLRequestReadHistory() { Channel = ich, MaxId = -1 };
                            //    await client.SendRequestAsync<bool>(readed);
                            //}

                            //    var mss = diff.OtherUpdates.Where(x => x.GetType() == typeof(TLMessage))
                            //.Cast<TLMessage>().ToList().Where(x => x.Date > _lastMessageStamp && x.Out == false)
                            //.OrderBy(dt => dt.Date);

                            //    foreach (TLMessage upd in mss)
                            //    {
                            //        resultMessage = ("New message ({0}): {1}", upd.Date, upd.Message).ToString();
                            //        await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat2.Id, AccessHash = chat2.AccessHash.Value }, resultMessage);
                            //        //Console.WriteLine("New message ({0}): {1}", upd.Date, upd.Message);
                            //    }
                            //    _lastMessageStamp = mss.Any() ? mss.Max(x => x.Date) : _lastMessageStamp;
                        }
                        else if (adiff is TLDifferenceTooLong)
                            await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat2.Id, AccessHash = chat2.AccessHash.Value }, "too long");
                        //Console.WriteLine("too long");
                        else if (adiff is TLDifferenceSlice)
                            await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat2.Id, AccessHash = chat2.AccessHash.Value }, "slice");
                        //Console.WriteLine("slice");
                    }
                    await Task.Delay(100);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        public virtual async Task AuthUser()
        {
            //var client = NewClient();
            var client = NewClient();

            await client.ConnectAsync();

            var hash = await client.SendCodeRequestAsync("+989372474147");
            var code = "94904"; // you can change code in debugger

            //var user = await client.MakeAuthAsync("+989372474147", hash, code);

            if (String.IsNullOrWhiteSpace(code))
            {
                throw new Exception("CodeToAuthenticate is empty in the app.config file, fill it with the code you just got now by SMS/Telegram");
            }

            TLUser user = null;
            try
            {
                user = await client.MakeAuthAsync("+989372474147", hash, code);
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

        public virtual async Task AuthPhoneNumber(string phoneNumber)
        {
            //var client = NewClient();
            var client = NewClient();

            await client.ConnectAsync();

            var hash = await client.SendCodeRequestAsync("+989372474147");
            var code = "94904"; // you can change code in debugger

            //var user = await client.MakeAuthAsync("+989372474147", hash, code);

            if (String.IsNullOrWhiteSpace(code))
            {
                throw new Exception("CodeToAuthenticate is empty in the app.config file, fill it with the code you just got now by SMS/Telegram");
            }

            TLUser user = null;
            try
            {
                user = await client.MakeAuthAsync("+989372474147", hash, code);
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

        private TelegramBotClient NewBotClient()
        {
            try
            {
                return new TelegramBotClient(botToken);
            }
            catch (MissingApiConfigurationException ex)
            {
                throw new Exception($"Please add your API settings to the `app.config` file. (More info: {MissingApiConfigurationException.InfoUrl})",
                                    ex);
            }
        }
    }
}