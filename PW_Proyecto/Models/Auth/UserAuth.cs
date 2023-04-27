using System.ComponentModel;

namespace PW_Proyecto.Models.Auth;

public class UserAuth
{
    [DisplayName("Usuario")]
    public string User { get; set; } = null!;

    [DisplayName("Contraseña")]
    public string Password { get; set; } = null!;
}
