using Pacman.CLASSES;
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
    /// Interação lógica para UscConfigTileMap.xam
    /// </summary>
    public partial class UscConfigTileMap : UserControl
    {
        public UscConfigTileMap()
        {
            InitializeComponent();
        }



        public void Inicializa(object obj)
        {
            try
            {
                uscBlocoCenarioSombra.Opacity = 0;
                expdAtual = new Expander();
                ConfigGame cg = DadosGerais.configGame;
                uscTileMap.MapColunas = cg.MapColunas;
                uscTileMap.MapLinhas = cg.MapLinhas;
                uscTileMap.Map = new UscBlocoCenario[uscTileMap.MapColunas, uscTileMap.MapLinhas];
                uscTileMap.TileSize = cg.TileSize;
                for (int i = 0; i < uscTileMap.MapLinhas; i++)
                {

                    for (int j = 0; j < uscTileMap.MapColunas; j++)
                    {
                        BlocoCenario cgBlocoAtual = cg.Map[i, j];
                        UscBlocoCenario ubc = new UscBlocoCenario();
                        ubc.PosLeft = cgBlocoAtual.PosLeft;
                        ubc.PosTop = cgBlocoAtual.PosTop;
                        ubc.Linha = i;
                        ubc.Coluna = j;
                        ubc.HorizontalAlignment = HorizontalAlignment.Left;
                        ubc.VerticalAlignment = VerticalAlignment.Top;
                        ubc.TipoBloco = cgBlocoAtual.TipoBloco;
                        ubc.NomeArquivo = cgBlocoAtual.NomeArquivo == null? "pastilha.png" : cgBlocoAtual.NomeArquivo;
                        uscTileMap.imprimeBloco(ubc);
                        uscTileMap.Map[i, j] = ubc;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        public void Finaliza()
        {

        }

        private void scvConteudo_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        Expander expdAtual;

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (expdAtual != (Expander)sender)
                {
                    expdAtual.IsExpanded = false;
                    expdAtual = (Expander)sender;
                    scvConteudo.ScrollToEnd();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void imgBlocoCenario_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                uscBlocoCenarioAtual.NomeArquivo = ((UscBlocoCenario)sender).NomeArquivo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void uscBlocoCenarioAtual_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UscBlocoCenario ubcEnviadoAux = ((UscBlocoCenario)sender);
                UscBlocoCenario ubc = new UscBlocoCenario();
                ubc.TipoBloco = UscBlocoCenario.ENUM_TIPO_BLOCO.PAREDE;
                ubc.NomeArquivo = ubcEnviadoAux.NomeArquivo;
                ubc.Height = ubcEnviadoAux.Height;
                ubc.Width = ubcEnviadoAux.Width;
                ubc.HorizontalAlignment = HorizontalAlignment.Left;
                ubc.VerticalAlignment = VerticalAlignment.Top;
                ubc.MouseMove += uscBlocoCenarioAtual_MouseMove;
                ubc.MouseUp += uscBlocoCenarioAtual_MouseUp;
                Point p = e.GetPosition(grdMontaGame);
                ubc.Margin = new Thickness(p.X - (ubcEnviadoAux.Width / 2), p.Y - (ubcEnviadoAux.Height / 2), 0, 0);
                grdMontaGame.Children.Add(ubc);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void uscBlocoCenarioAtual_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Point pEpsCursor = e.GetPosition(grdMontaGame);
                UscBlocoCenario auxSender = ((UscBlocoCenario)sender);
                UscBlocoCenario ubcColisao = PosicaoColisaoBloco(pEpsCursor);

                if (ubcColisao != null)
                {
                    UscBlocoCenario novoUbc = new UscBlocoCenario();
                    novoUbc.PosLeft = ubcColisao.PosLeft;
                    novoUbc.PosTop = ubcColisao.PosTop;
                    novoUbc.TipoBloco = UscBlocoCenario.ENUM_TIPO_BLOCO.PAREDE;
                    novoUbc.NomeArquivo = auxSender.NomeArquivo;
                    novoUbc.Linha = ubcColisao.Linha;
                    novoUbc.Coluna = ubcColisao.Coluna;
                    uscTileMap.Map[novoUbc.Linha, novoUbc.Coluna] = novoUbc;
                    uscTileMap.imprimeBloco(novoUbc);

                }
                else
                {

                }
                grdMontaGame.Children.Remove(auxSender);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }


        private void uscBlocoCenarioAtual_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                UscBlocoCenario aux = ((UscBlocoCenario)sender);

                Point p = e.GetPosition(aux);
                Point pEpsCursor = e.GetPosition(grdMontaGame);


                if (e.LeftButton == MouseButtonState.Pressed)
                {

                    UscBlocoCenario ubcColisao = PosicaoColisaoBloco(pEpsCursor);

                    if (ubcColisao != null)
                    {
                        aux.Opacity = 0.1;
                        uscBlocoCenarioSombra.Opacity = 1;
                        uscBlocoCenarioSombra.TipoBloco = UscBlocoCenario.ENUM_TIPO_BLOCO.PAREDE;
                        uscBlocoCenarioSombra.NomeArquivo = aux.NomeArquivo;
                        uscBlocoCenarioSombra.HorizontalAlignment = HorizontalAlignment.Left;
                        uscBlocoCenarioSombra.VerticalAlignment = VerticalAlignment.Top;
                        uscBlocoCenarioSombra.Margin = new Thickness(ubcColisao.PosLeft, ubcColisao.PosTop, 0, 0);

                    }
                    else
                    {
                        uscBlocoCenarioSombra.Opacity = 0;
                        aux.Opacity = 1;

                    }

                    aux.Margin = new Thickness(aux.Margin.Left - ((aux.Width / 2) - p.X), aux.Margin.Top - ((aux.Height / 2) - p.Y), 0, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public UscBlocoCenario PosicaoColisaoBloco(Point p)
        {
            UscBlocoCenario ubcColisao;


            for (int i = 0; i < uscTileMap.MapLinhas; i++)
            {

                for (int j = 0; j < uscTileMap.MapColunas; j++)
                {

                    ubcColisao = uscTileMap.Map[i, j];
                    if (p.X < ubcColisao.PosLeft + ubcColisao.Width &&
                       p.X + ubcColisao.Width > ubcColisao.PosLeft &&
                       p.Y < ubcColisao.PosTop + ubcColisao.Height &&
                       p.Y + ubcColisao.Height > ubcColisao.PosTop
                        )
                    {
                        return ubcColisao;
                    }

                }
            }
            return null;
        }

        public delegate void EnviaSalvarConfig(UscTileMap utp);
        public event EnviaSalvarConfig OnEnviaSalvarConfig;

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OnEnviaSalvarConfig?.Invoke(uscTileMap);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
