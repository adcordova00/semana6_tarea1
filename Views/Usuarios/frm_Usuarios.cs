using System;
using System.Windows.Forms;
using Login.Controllers;
using Login.Models;
using Login.config;

namespace Login.Views.Usuarios
{
    public partial class frm_Usuarios : Form
    {

        UsuariosController _usuariosController = new UsuariosController();
        UsuariosModel usuariosModel = new UsuariosModel();
        int id = 0;

        public frm_Usuarios()
        {
            InitializeComponent();
        }

        private void frm_Usuarios_Load(object sender, EventArgs e)
        {
            cargalista();
            cmb_roles.SelectedIndex = 0;

        }

        private void cargalista() {

            var listausuarios = _usuariosController.ObtenerTodosLosUsuarios();
            lst_usuarios.DataSource = null;
            lst_usuarios.DataSource = listausuarios;
            lst_usuarios.DisplayMember = "NombreUsuario";
            lst_usuarios.ValueMember = "ID";
        }

        private void frm_Usuarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_arduino.IsOpen) {
                _arduino.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _arduino.Write("F");
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            
            if (comprobar())
            {
                var resultado = new UsuariosModel();
                var res = "";
                var usuario = new UsuariosModel {
                ID = this.id,
                NombreUsuario = txt_nombre.Text.Trim().ToString(),
                Password = txt_contrasenia.Text.Trim().ToString(),
                Roles = cmb_roles.SelectedItem.ToString()
                };
                MessageBox.Show(cmb_roles.SelectedItem.ToString());
                if (this.id != 0)
                {
                   res = UsuariosModel.Actualizar(usuario);
                }
                else {
                    resultado = _usuariosController.InsertarUsuario(usuario);
                    _ = resultado.ID > 0 ? res = "ok" : "error";
                }
                if (res == "ok") {
                    MessageBox.Show("Se guardo con exito");
                    cargalista();
                    txt_contrasenia.Text = "";
                    txt_nombre.Text = "";
                    txt_repita.Text = "";
                    cmb_roles.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Ocurrio un error al guardar, intentelo mas tarde");
                }

            }
        }

        public bool comprobar() {

            if (txt_nombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese el nombre del usuario");
                return false;
            }
            else if (txt_contrasenia.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese la contrasenia");
                return false;
            }
            else if (txt_repita.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese la contrasenia");
                return false;
            }
            else if (cmb_roles.SelectedIndex == -1 || cmb_roles.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccion un item de la lista de roles");
                return false;
            }
            else {
                return true;
            }

        }

        public void LimpiarForm() { 
            txt_nombre.Text = string.Empty;
            txt_contrasenia.Text = string.Empty;
            txt_repita.Text= string.Empty;
            cmb_roles.SelectedIndex = 0;
        }

        private void lst_usuarios_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lst_usuarios.SelectedValue != null)
            {
                var usuario = _usuariosController.ObtenerUsuarioPorId((int)lst_usuarios.SelectedValue);
                this.id = (int)usuario.ID;
                txt_nombre.Text = usuario.NombreUsuario;
                txt_contrasenia.Text = usuario.Password;
                txt_repita.Text = usuario.Password;
                cmb_roles.SelectedItem = usuario.Roles;
            }
            else {
                ErrorHandler.ManejarErrorGeneral(null, "Seleccione un usuario de la lista");
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Desea Eliminar el usuario?", "Formulario de usuarios", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var usuario = _usuariosController.EliminarUsuario(Convert.ToInt32(lst_usuarios.SelectedValue));
                if (lst_usuarios.SelectedItem == null)
                {
                    ErrorHandler.ManejarEliminar();
                }
                else
                {
                    MessageBox.Show("El usuario se elimino con exito");
                    cargalista();
                }
            }
            else
            {
                MessageBox.Show("El usuario cancelo la operacion");
            }

        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            LimpiarForm();
        }
    }
}
