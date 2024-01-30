using System;
using System.Collections.Generic;

namespace Bluesoft_Bank.Models;

public partial class Transaccion
{
    public int Id { get; set; }

    public string? TipoTransaccion { get; set; }

    public string CiudadOrigen { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
