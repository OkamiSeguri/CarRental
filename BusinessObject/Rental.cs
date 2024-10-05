namespace BusinessObject
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public string Image { get; set; }

        public virtual Car? Car { get; set; }
        public virtual Customer? Customer { get; set; }



        public TimeSpan GetRentalDuration()
        {
            return EndDate - StartDate;
        }
    }
}
