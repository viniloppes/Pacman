using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pacman.UserControls
{
    /// <summary>
    /// Interação lógica para UscGameWin.xam
    /// </summary>
    public partial class UscGameWin : UserControl
    {
        public UscGameWin()
        {
            InitializeComponent();
        }
        public delegate void EnviaEvento();
        public event EnviaEvento OnJogar;
        public void Inicializa(string tempo, string pontos)
        {
            try
            {
                txtPonto.Content = pontos + " pontos";
                txtTempo.Content = tempo + " segundos";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Finaliza()
        {
            txtPonto.Content = "";
            txtTempo.Content = "";
        }

        private void btnJogar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OnJogar?.Invoke();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
