namespace Email
{
    public class EmailModel
    {
        public string From { get; set; } = "sistemabatala@gmail.com";
        public string Assunto { get; set; } = string.Empty;
        public List<string> To { get; set; } = new();
        public string Subject { get; set; } = "Gestão Grupo Musical";
        public string AddresseeName { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Host { get; set; } = "smtp.gmail.com";
    }
}