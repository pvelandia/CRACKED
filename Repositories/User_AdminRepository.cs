using CRACKED.Dtos;
using CRACKED.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRACKED.Repositories
{

    public class User_AdminRepository
    {
        public List<USUARIO> ObtenerUsuariosFiltrados(string searchValue, string filterValue)
        {
            using (var db = new CRACKEDEntities36())
            {
                var query = db.USUARIOs.AsQueryable();

                // Aplicar filtro de búsqueda si existe
                if (!string.IsNullOrEmpty(searchValue))
                {
                    query = query.Where(u => u.nombre.Contains(searchValue) ||
                                             u.apellido.Contains(searchValue) ||
                                             u.correoElectronico.Contains(searchValue));
                }

                // Aplicar filtro de rol y estado
                if (!string.IsNullOrEmpty(filterValue))
                {
                    switch (filterValue)
                    {
                        case "administrador":
                            query = query.Where(u => u.idRol == 2);
                            break;
                        case "domiciliario":
                            query = query.Where(u => u.idRol == 3);
                            break;
                        case "cliente":
                            query = query.Where(u => u.idRol == 1);
                            break;
                        case "activo":
                            query = query.Where(u => u.idEstado == 1);
                            break;
                        case "inactivo":
                            query = query.Where(u => u.idEstado == 0);
                            break;
                    }
                }

                return query.ToList();
            }
        }
    }

}