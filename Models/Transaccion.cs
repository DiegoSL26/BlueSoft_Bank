using System;
using System.Collections.Generic;

namespace Bluesoft_Bank.Models;

public partial class Transaccion
{
    public int Id { get; set; }

    public string? TipoTransaccion { get; set; }

    public string CiudadOrigen { get; set; } = null!;

    public double Monto { get; set; }

    public DateOnly Fecha { get; set; }

    public int? CuentaId { get; set; }

    public int? ClienteId { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Cuentum? Cuenta { get; set; }
}
