using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonApp
{
    public partial class Form1 : Form
    {
        private List<Pokemon> listaPokemon;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            listaPokemon = negocio.listar();            
            dgvPokemon.DataSource = listaPokemon;
            dgvPokemon.Columns["UrlImagen"].Visible = false;
            cargarImagen(listaPokemon[0].UrlImagen);
        }

        private void dgvPokemon_SelectionChanged(object sender, EventArgs e)
        {
            
            Pokemon seleccionado = (Pokemon)dgvPokemon.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
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
                MessageBox.Show("Ash no pudo capturar ese pokemon aún!");
            }
        }
    }
}
