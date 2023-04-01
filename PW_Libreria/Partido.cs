using System;
using System.Collections.Generic;

namespace PW_API.Models;

public partial class Partido
{
    public int Id { get; set; }

    public int TorneoId { get; set; }

    public int Jugador1Id { get; set; }

    public int Jugador2Id { get; set; }

    public DateTime Fecha { get; set; }
}
