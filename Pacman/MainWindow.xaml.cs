using Pacman.CLASSES;
using Pacman.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace Pacman
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public int[,] map;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        
        public ENUM_TELAS telaAtual = ENUM_TELAS.USC_NENHUM;
        public enum ENUM_TELAS
        {
            USC_NENHUM,
            USC_INICIO,
            USC_GAME,
            USC_CONFIG_TILE_MAP
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    dispatcherTimer.Tick += DispatcherTimer_Tick;
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
                    dispatcherTimer.Start();


                }
                catch (Exception ex)
                {
                }

                uscInicio.OnJogar += UscInicio_OnJogar;
    

                OcultaTodasTelas();
                ExibeTela(ENUM_TELAS.USC_INICIO);
            }
            catch (Exception ex)
            {

            }
        }

        private void UscInicio_OnJogar()
        {
            try
            {
                ExibeTela(ENUM_TELAS.USC_GAME);
            }
            catch (Exception ex)
            {

            }
        }

        private void OcultaTodasTelas()
        {
            try
            {
                uscConfigTileMap.Visibility = Visibility.Hidden;
                uscGame.Visibility = Visibility.Hidden;
                uscInicio.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {

            }
        }
       

        private void OcultaTela(ENUM_TELAS tela)
        {
            try
            {
                switch (tela)
                {
                    case ENUM_TELAS.USC_INICIO:
                        OcultaTodasTelas();
                        uscInicio.Finaliza();
                        break;
                    case ENUM_TELAS.USC_GAME:
                        OcultaTodasTelas();
                        uscGame.Finaliza();
                        break;
                    case ENUM_TELAS.USC_CONFIG_TILE_MAP:
                        OcultaTodasTelas();
                        uscConfigTileMap.Finaliza();
                        break;
                }
            }
            catch (Exception ex)
            {

                
            }
        }
        private void ExibeTela(ENUM_TELAS tela, object obj = null)
        {
            try
            {
                switch (tela)
                {
                    case ENUM_TELAS.USC_INICIO:
                        OcultaTela(telaAtual);
                        uscInicio.Inicializa(obj);
                        uscInicio.Visibility = Visibility.Visible;
                        uscInicio.UpdateLayout();
                        break;
                    case ENUM_TELAS.USC_CONFIG_TILE_MAP:
                        OcultaTela(telaAtual);
                        uscConfigTileMap.Inicializa(obj);
                        uscConfigTileMap.Visibility = Visibility.Visible;
                        uscConfigTileMap.UpdateLayout();

                        break;
                    case ENUM_TELAS.USC_GAME:
                        OcultaTela(telaAtual);
                        uscGame.Inicializa(obj);
                        uscGame.Visibility = Visibility.Visible;
                        uscGame.UpdateLayout();
                        uscGame.Focus();

                        break;
                }
                telaAtual = tela;
            }
            catch (Exception ex)
            {

                
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    //GameLoop();

                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        public delegate void EnviaEventoKeyUp(KeyEventArgs kea);
        public event EnviaEventoKeyUp OnKeyUp;
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                OnKeyUp?.Invoke(e);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
