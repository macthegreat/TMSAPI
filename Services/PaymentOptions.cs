using System.ComponentModel.DataAnnotations;

public class paymentOptions()
{
    [Required] public required string GatewayUrl {get;init;}
    
}