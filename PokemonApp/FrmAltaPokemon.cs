using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;
using System.Configuration;

namespace PokemonApp
{
    public partial class FrmAltaPokemon : Form
    {
        private Pokemon pokemon = null;
        private OpenFileDialog archivo = null;
        public FrmAltaPokemon()
        {
            InitializeComponent();
        }
        public FrmAltaPokemon(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon; //No lo recibe nulo
            Text = "Modificar Pokemon"; //cambia el nombre del form
        }

        private void bntCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Pokemon poke = new Pokemon(); //necesitamos using dominio
            PokemonNegocio negocio = new PokemonNegocio(); //agregamos using negocio 
            try
            {  
                if (pokemon == null)
                {
                    pokemon = new Pokemon();
                }
                pokemon.Numero = int.Parse(txtNum.Text);
                pokemon.Nombre = txtName.Text;
                pokemon.Descripcion = txtDesc.Text;
                pokemon.UrlImagen = txtUrlImagen.Text;
                pokemon.Tipo = (Elemento)cboTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cboDeb.SelectedItem;

                if(pokemon.Id != 0)
                {
                    negocio.modificar(pokemon);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(pokemon);
                    MessageBox.Show("Agregado exitosamente");
                }
                //Guardo imagen si la levanto local
                
                if (archivo != null && !(txtUrlImagen.Text.Contains("http"))) 
                {
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images_folder"] + archivo.SafeFileName);
                }
                
                    
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
                cboTipo.ValueMember = "Id";
                cboTipo.DisplayMember = "Descripcion";
                cboDeb.DataSource = elementoNegocio.listar();
                cboDeb.ValueMember = "Id";
                cboDeb.DisplayMember = "Descripcion";
                  
                //validacion
                if (pokemon != null)
                {
                    //Precargo el pokemon
                    txtNum.Text = pokemon.Numero.ToString();
                    txtName.Text = pokemon.Nombre;
                    txtDesc.Text = pokemon.Descripcion;
                    txtUrlImagen.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen); //corrige el delate de carga
                    cboTipo.SelectedValue = pokemon.Tipo.Id;
                    cboDeb.SelectedValue = pokemon.Debilidad.Id;
                }
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

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            //creamos un objeto para abrir archivos
            //OpenFileDialog archivo = new OpenFileDialog();
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*jpg;|png|*.png";
            if(archivo.ShowDialog() == DialogResult.OK)
            {
                txtUrlImagen.Text = archivo.FileName; //devuelve una cadena con el archivo
                cargarImagen(archivo.FileName);

                //Guardo la imagen
                //File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images_folder"] + archivo.SafeFileName);
            }           

        }
    }
}
