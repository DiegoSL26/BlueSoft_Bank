using System;
using System.Collections.Generic;

namespace Bluesoft_Bank.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? TipoCliente { get; set; }

    public virtual ICollection<Cuentum> Cuentas { get; set; } = new List<Cuentum>();

    public virtual ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}
