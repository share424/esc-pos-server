namespace Dtos
{
    public record PrintDto
    {
        public int cash { get; init; }
        public PrintMetaDto meta { get; init; }
        public List<ProductDto> products { get; init; }
    }

    public record PrintMetaDto
    {
        public string store_name { get; init; }
        public string store_address { get; init; }
        public string phone { get; init; }
        public string trx_id { get; init; }
    }

    public record ProductDto
    {
      public string name { get; init; }
      public int qty { get; init; }
      public int price { get; init; }
      public int discount { get; init; }
      public string unit { get; init; }
    }
}