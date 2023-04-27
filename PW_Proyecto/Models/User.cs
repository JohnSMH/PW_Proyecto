﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PW_Proyecto.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Partido>? PartidoJugador1s { get; } = new List<Partido>();

    public virtual ICollection<Partido>? PartidoJugador2s { get; } = new List<Partido>();

    public virtual ICollection<Torneo>? TorneosNavigation { get; } = new List<Torneo>();
    [JsonIgnore]
    public virtual ICollection<Torneo>? Torneos { get; } = new List<Torneo>();
}
