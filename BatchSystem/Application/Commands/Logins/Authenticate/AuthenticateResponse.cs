namespace BatchSystem.Application.Commands.Logins.Authenticate
{
    public class AuthenticateResponse
    {
        public string AccessToken { get; set; }
        public InformationAccount Account { get; set; }
        public bool PassWordTrue { get; set; }
        public string Messsage {  get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AuthenticateResponse(bool passWordTrue, string messsage)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            PassWordTrue=passWordTrue;
            Messsage=messsage;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AuthenticateResponse(string accessToken, InformationAccount account)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            PassWordTrue = true;
            AccessToken=accessToken;
            Account=account;
        }
    }
}
