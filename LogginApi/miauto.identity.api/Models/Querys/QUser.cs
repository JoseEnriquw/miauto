namespace miauto.identity.api.Models.Querys
{
    public static class QUser
    {
        public const string GETALL = " SELECT id, email,password  From public.user";
        public const string GET = " SELECT id, email,password  From public.user WHERE id=@Id";
        public const string GET_BY_EMAIL_AND_PASSWORD = " SELECT id, email,password  From public.user WHERE email=@Email AND password=@Password";
        public const string INSERT = " INSERT INTO public.user ( email,password) VALUES (  @Email, @Password)";
        public const string UPDATE = " UPDATE public.user SET email=@Email password=@Password WHERE id=@Id";
        public const string DELETE = " DELETE FROM public.user WHERE id=@Id";

    }
}
