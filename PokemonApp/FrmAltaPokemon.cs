using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace PokemonApp
{
    public partial class FrmAltaPokemon : Form
    {
        public FrmAltaPokemon()
        {
            InitializeComponent();
        }

        private void bntCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Pokemon poke = new Pokemon(); //necesitamos using dominio
            PokemonNegocio negocio = new PokemonNegocio(); //agregamos using negocio 
            try
            {
                poke.Numero = int.Parse(txtNum.Text);
                poke.Nombre = txtName.Text;
                poke.Descripcion = txtDesc.Text;

                negocio.agregar(poke);
                MessageBox.Show("Agregado exitosamente");
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());//mostramos mensaje de error
            }
        }
    }
}
