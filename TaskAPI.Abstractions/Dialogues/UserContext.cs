using TaskAPI.Common.Enums;

namespace TaskAPI.Abstractions.Dialogues
{
    public class UserContext
    {
        public long ChatId { get; set; }

        public UserState UserState { get; set; }


        public AddContext AddContext { get; set; }

        public DeleteContext DeleteContext { get; set; }

        public GetContext GetContext { get; set; }

        public UserContext(long chatId)
        {
            ChatId = chatId;
            UserState = UserState.InitialState;
        }

        
    }
}
