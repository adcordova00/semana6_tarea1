using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Login.Controllers;
using Login.Views.Acesos;
using Login.Views.Bodega;
using Login.Views.Financiero;
using Login.Views.Usuarios;
using Login.Config;

namespace Login
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            txt_contrasenia.Text = "";
            txt_usuario.Text = "";
        }
        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            #region
            try
            {
                UsuariosController _usuariosController = new UsuariosController();
                var usuariomodel = _usuariosController.AutenticarUsuario(txt_usuario.Text.Trim(), txt_contrasenia.Text.Trim());
                if (usuariomodel != null)
                {
                    lbl_mensaje.Text = "Ingreso exitoso";
                    this.Hide();

                    ConfiguracionProyecto.IDusuario = (int)usuariomodel.ID;
                    ConfiguracionProyecto.usuario = usuariomodel.NombreUsuario;
                    ConfiguracionProyecto.rol = usuariomodel.Roles;


                    frm_Usuarios _frm_usuarios = new frm_Usuarios();
                    this.Hide();
                    _frm_usuarios.Show();
                }

            }
            catch (Exception)
            {

                throw;
            }
            #endregion

        }
    }
}
