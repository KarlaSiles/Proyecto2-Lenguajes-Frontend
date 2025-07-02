namespace MercatikaApp.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Estado { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? CreditCardNum { get; set; }
        public string DatePayment { get; set; }
        public string? PaymentMethodName { get; set; }
    }
}
