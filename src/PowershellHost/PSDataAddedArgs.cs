namespace PowershellHost
{
    public class PSDataAddedArgs<T>
    {
        public T Data { get; set; }

        public PSDataAddedArgs(T data)
        {
            Data = data;
        }
    }
}