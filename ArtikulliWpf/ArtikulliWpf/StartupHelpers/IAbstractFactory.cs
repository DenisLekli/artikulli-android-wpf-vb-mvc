namespace ArtikulliWpf.StartupHelpers
{
    public interface IAbstractFactory<T>
    {
        T Create();
    }
}