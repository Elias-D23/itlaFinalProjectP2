namespace Crud.Domain.Entities
{
    public class Loan
    {
        public string LoanNumber { get; set; }

        public string Amount { get; set; }

        public string Rate { get; set; }

        public string Term { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Inactive { get; set; }
    }
}
