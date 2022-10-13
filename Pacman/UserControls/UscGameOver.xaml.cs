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
    /// Interação lógica para UscGameOver.xam
    /// </summary>
    public partial class UscGameOver : UserControl
    {
        public UscGameOver()
        {
            InitializeComponent();
        }
        public delegate void EnviaEvento();
        public event EnviaEvento OnJogar;
        public void Inicializa(object obj)
        {

        }
        public void Finaliza()
        {

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
