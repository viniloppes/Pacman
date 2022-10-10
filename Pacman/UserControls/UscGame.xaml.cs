using Pacman.CLASSES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Pacman.UserControls
{
    /// <summary>
    /// Interação lógica para UscGame.xam
    /// </summary>
    public partial class UscGame : UserControl
    {
        public UscGame()
        {
            InitializeComponent();
        }
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer dispatcherTimerSegundos = new DispatcherTimer();

        int toutDuracaGame = 0;
        int toutTempoJogado = 0;
        const int TOUT_DURACAO_GAME = 1200;//1min // 1 Segundo == 20
        public void Inicializa(object obj)
        {
            try
            {

                try
                {
                    dispatcherTimer.Tick += DispatcherTimer_Tick;
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                    dispatcherTimer.Start();
                }
                catch (Exception ex)
                {

                }
                try
                {
                    dispatcherTimerSegundos.Tick += DispatcherTimerSegundos_Tick; ;
                    dispatcherTimerSegundos.Interval = new TimeSpan(0, 0, 0, 1);
                    dispatcherTimerSegundos.Start();
                }
                catch (Exception ex)
                {

                }
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
                        ubc.NomeArquivo = cgBlocoAtual.NomeArquivo == null ? "pastilha.png" : cgBlocoAtual.NomeArquivo;
                        uscTileMap.imprimeBloco(ubc);
                        uscTileMap.Map[i, j] = ubc;


                    }
                }
                UscPacman up = new UscPacman();
                up.PosLeft = up.Width * 2;
                up.PosTop = up.Height * 2;
                up.velocidade = 10;
                up.FezPrimeiroMovimento = false;
                uscTileMap.InserePacman(up);
                toutDuracaGame = TOUT_DURACAO_GAME;
                toutTempoJogado = 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void Finaliza()
        {

        }

        private void DispatcherTimerSegundos_Tick(object sender, EventArgs e)
        {
            try
            {
                toutTempoJogado++;
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
                    if (toutDuracaGame > 0)
                    {
                        //toutTempoJogado += 1;
                        //string intPase = (toutTempoJogado / 20).ToString();
                        //if( Int32.TryParse(intPase, out int i))
                        //{
                        //txtTempo.Content = toutTempoJogado++;
                        //}
                        GameLoop();
                        if (--toutDuracaGame == 0)
                        {
                            toutDuracaGame = TOUT_DURACAO_GAME;
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void VerificaColisaoBloco()
        {
            try
            {
                if (uscTileMap.VerificaColisaoParede(uscTileMap.Pacman.PosLeft, uscTileMap.Pacman.PosTop, uscTileMap.Pacman.direcaoAtual) == true)
                {
                    uscTileMap.Pacman.ColisaoParede = true;
                    //uscTileMap.Pacman.FezPrimeiroMovimento = false;
                }
                else
                {
                    uscTileMap.Pacman.ColisaoParede = false;

                }
            }
            catch (Exception ex)
            {

            }
        }
        public void GameLoop()
        {
            try
            {

                uscTileMap.Pacman.AtualizaPosicao();
                VerificaColisaoBloco();
                MovePacman();
                //if (DadosGerais.toutMovePacman > 0)
                //{
                //    if (--DadosGerais.toutMovePacman == 0)
                //    {

                //    }
                //}
                //uscTileMap.Pacman.AtualizaPosicao()

            }
            catch (Exception ex)
            {

            }
        }
        public void MovePacman()
        {
            if (uscTileMap.Pacman.direcaoAtual != uscTileMap.Pacman.requestedMovingDirection)
            {

                if (uscTileMap.VerificaCaminhoLivre(uscTileMap.Pacman.PosLeft, uscTileMap.Pacman.PosTop, uscTileMap.Pacman.requestedMovingDirection) == true)
                {
                    //uscTileMap.Pacman.Pause = false;
                    uscTileMap.Pacman.direcaoAtual = uscTileMap.Pacman.requestedMovingDirection;

                }
            }



        }
        public void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.Key)
                {
                    case Key.Down:
                        uscTileMap.Pacman.requestedMovingDirection = UscPacman.ENUM_DIRECAO.DOWN;
                        if (this.uscTileMap.Pacman.FezPrimeiroMovimento == false)
                        {
                            uscTileMap.Pacman.direcaoAtual = uscTileMap.Pacman.requestedMovingDirection;

                        }
                        this.uscTileMap.Pacman.FezPrimeiroMovimento = true;

                        break;
                    case Key.Up:
                        uscTileMap.Pacman.requestedMovingDirection = UscPacman.ENUM_DIRECAO.UP;
                        if (this.uscTileMap.Pacman.FezPrimeiroMovimento == false)
                        {
                            uscTileMap.Pacman.direcaoAtual = uscTileMap.Pacman.requestedMovingDirection;

                        }
                        this.uscTileMap.Pacman.FezPrimeiroMovimento = true;

                        break;
                    case Key.Left:
                        uscTileMap.Pacman.requestedMovingDirection = UscPacman.ENUM_DIRECAO.LEFT;
                        if (this.uscTileMap.Pacman.FezPrimeiroMovimento == false)
                        {
                            uscTileMap.Pacman.direcaoAtual = uscTileMap.Pacman.requestedMovingDirection;

                        }
                        this.uscTileMap.Pacman.FezPrimeiroMovimento = true;

                        break;
                    case Key.Right:
                        uscTileMap.Pacman.requestedMovingDirection = UscPacman.ENUM_DIRECAO.RIGHT;
                        if (this.uscTileMap.Pacman.FezPrimeiroMovimento == false)
                        {
                            uscTileMap.Pacman.direcaoAtual = uscTileMap.Pacman.requestedMovingDirection;

                        }
                        this.uscTileMap.Pacman.FezPrimeiroMovimento = true;

                        break;
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {

            Keyboard.Focus(sender as UserControl);

        }
    }
}
