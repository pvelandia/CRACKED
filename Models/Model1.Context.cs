﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CRACKEDEntities34 : DbContext
    {
        public CRACKEDEntities34()
            : base("name=CRACKEDEntities34")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AUDITORIA> AUDITORIAs { get; set; }
        public virtual DbSet<CIUDAD> CIUDADs { get; set; }
        public virtual DbSet<DEPARTAMENTO> DEPARTAMENTOes { get; set; }
        public virtual DbSet<ESTADO> ESTADOes { get; set; }
        public virtual DbSet<PEDIDO> PEDIDOes { get; set; }
        public virtual DbSet<PEDIDO_PRODUCTO> PEDIDO_PRODUCTO { get; set; }
        public virtual DbSet<PRODUCTO> PRODUCTOes { get; set; }
        public virtual DbSet<ROL> ROLs { get; set; }
        public virtual DbSet<SABOR> SABORs { get; set; }
        public virtual DbSet<TIPO_PRODUCTO> TIPO_PRODUCTO { get; set; }
        public virtual DbSet<USUARIO> USUARIOs { get; set; }
    }
}
