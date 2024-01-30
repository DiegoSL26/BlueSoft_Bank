using System;
using System.Collections.Generic;

namespace Bluesoft_Bank.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? TipoCliente { get; set; }

    public int? TransaccionId { get; set; }

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();

    public virtual Transaccion? Transaccion { get; set; }
}
