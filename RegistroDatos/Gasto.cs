using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroDatos
{
    public class Gasto
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public decimal monto { get; set; }
        public string categoria { get; set; }
        public override string ToString()
        {
            return $"{id}; Descripcion: {descripcion}|| Monto: {monto}|| Categoria: {categoria}";
        }
    }
}
