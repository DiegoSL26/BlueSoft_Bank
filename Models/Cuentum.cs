using System;
using System.Collections.Generic;

namespace Bluesoft_Bank.Models;

public partial class Cuentum
{
    public int Id { get; set; }

    public double Saldo { get; set; }

    public string? TipoCuenta { get; set; }

    public string Ciudad { get; set; } = null!;

    public int? ClientId { get; set; }

    public int? TransaccionId { get; set; }

    public virtual Cliente? Client { get; set; }

    public virtual Transaccion? Transaccion { get; set; }
}
