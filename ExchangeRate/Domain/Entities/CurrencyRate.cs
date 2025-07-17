namespace Domain.Entities
{
    public class CurrencyRate
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Nominal { get; set; }
    }
}
