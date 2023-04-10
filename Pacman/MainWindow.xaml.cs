using Pacman.CLASSES;
using Pacman.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public ENUM_TELAS telaAtual = ENUM_TELAS.USC_NENHUM;
        public enum ENUM_TELAS
        {
            USC_NENHUM,
            USC_INICIO,
            USC_GAME,
            USC_CONFIG_TILE_MAP,
            USC_GAME_OVER,
            USC_GAME_WIN
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                uscInicio.OnJogar += UscInicio_OnJogar;
                uscInicio.OnConfigurarMapa += UscInicio_OnConfigurarMapa;
                uscConfigTileMap.OnEnviaSalvarConfig += UscConfigTileMap_OnEnviaSalvarConfig;

                try
                {
                    DadosGerais.configGame = Negocios.CarregaConfigGame(DadosGerais.caminhoArquivoConfigGame);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Alguns arquivos estão faltando! Verifique se você seguiu corretamente o passo a passo do README.md", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
                uscConfigTileMap.OnVoltar += UscConfigTileMap_OnVoltar;
                uscGame.GameOver += UscGame_GameOver;
                uscGame.GameWin += UscGame_GameWin;
                uscGameOver.OnJogar += UscGameOver_OnJogar;
                OcultaTodasTelas();
                ExibeTela(ENUM_TELAS.USC_INICIO);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void UscGame_GameWin(string score, string tempo)
        {
            ExibeTela(ENUM_TELAS.USC_GAME_WIN, new object[] { tempo, score });


        }

        private void UscGameOver_OnJogar()
        {
             ExibeTela(ENUM_TELAS.USC_INICIO);

        }

        private void UscGame_GameOver(string pontos, string tempo)
        {
            ExibeTela(ENUM_TELAS.USC_GAME_OVER, new object[] { tempo, pontos });
        }

        private void UscConfigTileMap_OnVoltar()
        {
            try
            {
                ExibeTela(ENUM_TELAS.USC_INICIO);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UscConfigTileMap_OnEnviaSalvarConfig(UscTileMap utp)
        {
            try
            {
                ConfigGame cg = new ConfigGame();
                cg.Map = new BlocoCenario[utp.MapLinhas, utp.MapColunas];
                for (int i = 0; i < utp.MapLinhas; i++)
                {
                    for (int j = 0; j < utp.MapColunas; j++)
                    {
                        UscBlocoCenario ubcAtual = utp.Map[i, j];
                        cg.Map[i, j] = new BlocoCenario();
                        cg.Map[i, j].TipoBloco = ubcAtual.TipoBloco;
                        cg.Map[i, j].NomeArquivo = ubcAtual.NomeArquivo;
                        cg.Map[i, j].PosLeft = ubcAtual.PosLeft;
                        cg.Map[i, j].PosTop = ubcAtual.PosTop;
                        cg.Map[i, j].Linha = ubcAtual.Linha;
                        cg.Map[i, j].Coluna = ubcAtual.Coluna;

                    }
                }
                //cg.Map = utp.Map;
                cg.MapColunas = utp.MapColunas;
                cg.MapLinhas = utp.MapLinhas;
                cg.TileSize = utp.TileSize;
                Negocios.SalvaConfigGame(DadosGerais.caminhoArquivoConfigGame, cg);
                ExibeTela(ENUM_TELAS.USC_INICIO);
                DadosGerais.configGame = Negocios.CarregaConfigGame(DadosGerais.caminhoArquivoConfigGame);

            }
            catch (Exception ex)
            {

            }
        }

        private void UscInicio_OnConfigurarMapa()
        {
            try
            {
                ExibeTela(ENUM_TELAS.USC_CONFIG_TILE_MAP);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

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
                uscGameOver.Visibility = Visibility.Hidden;
                uscGameWin.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

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
                    case ENUM_TELAS.USC_GAME_OVER:
                        OcultaTodasTelas();
                        uscGameOver.Finaliza();
                        break;
                    case ENUM_TELAS.USC_GAME_WIN:
                        OcultaTodasTelas();
                        uscGameWin.Finaliza();
                        break;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

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
                    case ENUM_TELAS.USC_GAME_OVER:
                        OcultaTela(telaAtual);
                        uscGameOver.Inicializa(((object[])obj)[0].ToString(), ((object[])obj)[1].ToString());
                        uscGameOver.Visibility = Visibility.Visible;
                        uscGameOver.UpdateLayout();

                        break;
                    case ENUM_TELAS.USC_GAME_WIN:
                        OcultaTela(telaAtual);
                        uscGameWin.Inicializa(((object[])obj)[0].ToString(), ((object[])obj)[1].ToString());
                        uscGameWin.Visibility = Visibility.Visible;
                        uscGameWin.UpdateLayout();

                        break;
                }
                telaAtual = tela;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

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
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
