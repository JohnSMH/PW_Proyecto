﻿using System;
using System.Collections.Generic;

namespace PW_Proyecto.Models;

public partial class Partido
{
    public int Id { get; set; }

    public int TorneoId { get; set; }

    public int Jugador1Id { get; set; }

    public int Jugador2Id { get; set; }

    public DateTime Fecha { get; set; }
    public int? Resultado { get; set; }

    public virtual User? Jugador1 { get; set; } = null!;

    public virtual User? Jugador2 { get; set; } = null!;

    public virtual Torneo? Torneo { get; set; } = null!;
}
