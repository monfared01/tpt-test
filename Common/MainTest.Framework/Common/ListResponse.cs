namespace MainTest.Framework.Common
{
    public class ListResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalRowCount { get; set; }
    }
}
