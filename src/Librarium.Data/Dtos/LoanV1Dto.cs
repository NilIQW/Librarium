namespace Librarium.Data.Dtos;

public class LoanV1Dto
{
    public int LoanId { get; set; }
    public string BookTitle { get; set; } = null!;
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}