namespace Email.Interface
{
    public interface IEmailService
    {
        Task<bool> Enviar(EmailModel mailModel);

    }
}
