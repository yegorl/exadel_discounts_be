namespace Exadel.CrazyPrice.Common.Configurations
{
    public interface ILoggerConfiguration<out T> where T : class, new()
    {
        public T LoggerConfiguration { get; }
    }
}
