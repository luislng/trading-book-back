namespace TradingBook.ExternalServices.Http.Abstract
{
    internal interface IHttpService
    {
        public Task<T> Get<T>(Uri uri, IReadOnlyDictionary<string, string> parameters, IReadOnlyDictionary<string, string> headers);
    }
}
