using System;
using System.Collections.Generic;

namespace PW_API.Models;

public partial class Torneo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public int MaxParticipantes { get; set; }

    public int Organizador { get; set; }
}
