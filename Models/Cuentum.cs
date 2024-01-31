using System;
using System.Collections.Generic;

namespace Bluesoft_Bank.Models;

public partial class Cuentum
{
    public int Id { get; set; }

    public double Saldo { get; set; }

    public string? TipoCuenta { get; set; }

    public string Ciudad { get; set; } = null!;

    public int? ClienteId { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
