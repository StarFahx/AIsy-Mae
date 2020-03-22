namespace Turnip
{
    public interface IRepository<T>
    {
        IDataSet<T> GetData();
    }
}