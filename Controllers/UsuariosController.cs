using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Login.Models;

namespace Login.Controllers
{
    class UsuariosController
    {
        public UsuariosModel InsertarUsuario(UsuariosModel usuario)
        {
            return UsuariosModel.Insertar(usuario);
        }

        public string ActualizarUsuario(UsuariosModel usuario)
        {
            return UsuariosModel.Actualizar(usuario);
        }

        public string EliminarUsuario(int idUsuario)
        {
            return UsuariosModel.Eliminar(idUsuario);
        }

        public UsuariosModel ObtenerUsuarioPorId(int idUsuario)
        {
            return UsuariosModel.ObtenerPorId(idUsuario);
        }

        public List<UsuariosModel> ObtenerTodosLosUsuarios()
        {
            return UsuariosModel.ObtenerTodos();
        }

        public UsuariosModel AutenticarUsuario(string nombreUsuario, string password)
        {
            return UsuariosModel.Autenticar(nombreUsuario, password);
        }
    }
}
