namespace ContaObj.Domain.Options;

public class RabbitMqOptions
{
    public const string RabbitMq = "RabbitMq";

    public string HostName { get; set; } = String.Empty;
}