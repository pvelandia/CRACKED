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
    
    public partial class USUARIO_PRODUCTO
    {
        public int idUsuarioProducto { get; set; }
        public int idUsuario { get; set; }
        public int idProducto { get; set; }
    
        public virtual PRODUCTO PRODUCTO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
