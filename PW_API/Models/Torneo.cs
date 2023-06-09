﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PW_API.Models;

public partial class Torneo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public int MaxParticipantes { get; set; }

    public int Organizador { get; set; }

    public virtual User? OrganizadorNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Partido>? Partidos { get; } = new List<Partido>();

    public virtual ICollection<User>? Users { get; } = new List<User>();
}
