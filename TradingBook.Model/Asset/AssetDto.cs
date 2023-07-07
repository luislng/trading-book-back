namespace TradingBook.Model.Asset
{
    public record AssetDto
    {        
        public uint Id { get; internal set; }

        public string Name { get; set; } = String.Empty;

        public string Code { get; set; } = String.Empty;

        public void SetId(in uint Id) => this.Id = Id;
    }
}
