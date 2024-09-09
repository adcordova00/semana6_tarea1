using System;
using System.Windows.Forms;

namespace Login.config
{
    public static class ErrorHandler
    {
        public static void ManejarErrorSql(Exception ex, string mensajePersonalizado = "Ocurrió un error en la base de datos.")
        {
            MessageBox.Show($"{mensajePersonalizado}\n\nDetalles del error:\n{ex.Message}", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ManejarErrorGeneral(Exception ex=null, string mensajePersonalizado = "Ocurrió un error inesperado.")
        {
            if (ex != null)
            {
                MessageBox.Show($"{mensajePersonalizado}\n\nDetalles del error:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else { 
                MessageBox.Show(mensajePersonalizado);
            }

        }
        public static void ManejarInsertar() {
            MessageBox.Show("Se insertó con éxito");
        }
        public static void ManejarActualizar()
        {
            MessageBox.Show("Se actualizó con éxito");
        }
        public static void ManejarEliminar()
        {
            MessageBox.Show("Se eliminó con éxito");
        }
    }
}
