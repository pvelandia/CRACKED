//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRACKED.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PEDIDO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PEDIDO()
        {
            this.PEDIDO_PRODUCTO = new HashSet<PEDIDO_PRODUCTO>();
        }
    
        public int idPedido { get; set; }
        public int idCliente { get; set; }
        public int idDomiciliario { get; set; }
        public int idCiudad { get; set; }
        public int idMetodoPago { get; set; }
        public int idEstado { get; set; }
        public Nullable<System.DateTime> fechaEntrega { get; set; }
        public Nullable<System.DateTime> fechaVenta { get; set; }
        public string direccion { get; set; }
        public Nullable<double> subtotal { get; set; }
        public Nullable<double> valorDomicilio { get; set; }
        public Nullable<double> totalPedido { get; set; }
    
        public virtual CIUDAD CIUDAD { get; set; }
        public virtual ESTADO ESTADO { get; set; }
        public virtual METODO_PAGO METODO_PAGO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDO_PRODUCTO> PEDIDO_PRODUCTO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        public virtual USUARIO USUARIO1 { get; set; }
    }
}
