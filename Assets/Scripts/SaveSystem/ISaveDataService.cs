namespace SaveSystem
{
    public interface ISaveDataService
    {
        public bool Save<T>(T data, string fileName);
        public bool Load<T>(out T data, string fileName);
    }
}