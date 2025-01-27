namespace Base.Application.Wallet.Commands.TransferBalance;

public class TransferBalanceResponse
{
    public string NameOut {  get; set; }
    public string NameIn { get; set; }
    public decimal BalanceOut {  get; set; }
    public decimal BalanceIn { get; set; }
}
