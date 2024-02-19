namespace Email.Models
{
    //singleton
    public class SMTPModel
    {
        private static SMTPModel _instance;
        private static readonly object _lockObject = new object();

        public string UserName { get; set; } = "agendemeteste@gmail.com";
        public string Password { get; set; } = "uyqs zwdc baft jgaz";
        public int Port { get; set; } = 587;

        private SMTPModel() { }

        public static SMTPModel Instance
        {
            get
            {
                // Verificar se já existe uma instância
                if (_instance == null)
                {
                    // Garantir que apenas uma thread pode criar a instância
                    lock (_lockObject)
                    {
                        // Verificar novamente após a obtenção do bloqueio
                        if (_instance == null)
                        {
                            _instance = new SMTPModel();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
