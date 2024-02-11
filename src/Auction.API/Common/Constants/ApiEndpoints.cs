namespace Auction.API.Common.Constants;

public static partial class Constants
{
    public static class ApiEndpoints
    {
        private const string IdPlaceholder = "{id}";
        
        public static class Lot
        {
            private const string Base = "/api/lots";

            public const string GetAll = Base;
            public const string GetById = $"{Base}/{IdPlaceholder}";
            public const string Create = Base;
            public const string Update = $"{Base}/{IdPlaceholder}";
            public const string Upsert = $"{Base}/{IdPlaceholder}";
            public const string Delete = $"{Base}/{IdPlaceholder}";
        }
        
        public static class Bid
        {
            private const string Base = "/api/bids";

            public const string GetAll = Base;  
            public const string GetByLotId = "/api/lots/{lotId}/bids";
            public const string GetById = $"{Base}/{IdPlaceholder}";
            public const string CreateBid = "api/lots/{id}/bids";
            public const string Update = $"{Base}/{IdPlaceholder}";
            public const string Delete = $"{Base}/{IdPlaceholder}";
        }
        
        public static class Auth
        {
            private const string Base = "/api/auth";

            public const string Login = $"{Base}/login";
            public const string Signup = $"{Base}/signup";
        }
        
        public static class Chat
        {
            private const string Base = "/api/chats";

            public const string GetAll = Base;
            public const string GetById = $"{Base}/{IdPlaceholder}";
            public const string Create = Base;
            public const string Update = $"{Base}/{IdPlaceholder}";
            public const string Upsert = $"{Base}/{IdPlaceholder}";
            public const string Delete = $"{Base}/{IdPlaceholder}";
        }
        
        public static class Message
        {
            private const string Base = "/api/messages";

            public const string GetAll = Base;  
            public const string GetByChatId = "/api/chats/{chatId}/messages";
            public const string GetById = $"{Base}/{IdPlaceholder}";
            public const string CreateMessage = "api/chats/{chatId}/messages";
            public const string Update = $"{Base}/{IdPlaceholder}";
            public const string Delete = $"{Base}/{IdPlaceholder}";
        }
    }
}
