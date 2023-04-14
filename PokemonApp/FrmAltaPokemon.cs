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
                poke.UrlImagen = txtUrlImagen.Text;
                poke.Tipo = (Elemento)cboTipo.SelectedItem;
                poke.Debilidad = (Elemento)cboDeb.SelectedItem;

                negocio.agregar(poke);
                MessageBox.Show("Agregado exitosamente");
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());//mostramos mensaje de error
            }
        }

        private void FrmAltaPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementoNegocio = new ElementoNegocio();
            try
            {
                cboTipo.DataSource = elementoNegocio.listar();
                cboDeb.DataSource = elementoNegocio.listar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            //usamos propiedad leave en textboxUrl
            cargarImagen(txtUrlImagen.Text);

        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxPokemon.Load(imagen);

            }
            catch (Exception ex)
            {

                pbxPokemon.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQBqOKMMO9oLUSJnPu2CvEpWxYy8Q0oYDA8bA&usqp=CAU");
                //MessageBox.Show("Ash no pudo capturar ese pokemon aún!");
            }
        }
    }
}
