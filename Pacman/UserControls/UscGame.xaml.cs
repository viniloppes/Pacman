﻿using Pacman.CLASSES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using static Pacman.UserControls.UscBlocoCenario;
using static Pacman.UserControls.UscPacman;



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

        public delegate void EnviaEvento(string score, string tempo);
        public event EnviaEvento GameOver;
        public event EnviaEvento GameWin;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer dispatcherTimerSegundos = new DispatcherTimer();
        SoundEffectsControl audioWaka =
            new SoundEffectsControl(DadosGerais.arquivoAudioWaka);
        SoundEffectsControl audioEatGhost =
     new SoundEffectsControl(DadosGerais.arquivoAudioEatGhost);
        SoundEffectsControl audioGameOVer =
     new SoundEffectsControl(DadosGerais.arquivoAudioGameOver);

        SoundEffectsControl audioGameWin =
     new SoundEffectsControl(DadosGerais.arquivoAudioGameWin);

        SoundEffectsControl audioPowerDots =
     new SoundEffectsControl(DadosGerais.arquivoAudioPowerDot);

        int contadorAudioPowerDots = 0;
        const int CONTADOR_AUDIO_POWER_DOTS = 0;

        int toutDuracaGame = 0;
        int toutTempoJogado = 0;
        int pontos = 0;
        const int TOUT_DURACAO_GAME = 1200;//1min // 1 Segundo == 50
        int toutMudaDirecaoGhost = 0;
        const int TOUT_MUDA_DIRECAO_GHOST = 10;//100sec // 1 Segundo == 50

        int toutMudaDirecaoGhost_2 = 0;
        const int TOUT_MUDA_DIRECAO_GHOST_2 = 7;
        int toutPowerDotAtiva = 0;
        const int TOUT_POWER_DOT_ATIVA = 8;
        public void Inicializa(object obj)
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
                try
                {
                    dispatcherTimerSegundos.Tick += DispatcherTimerSegundos_Tick; ;
                    dispatcherTimerSegundos.Interval = new TimeSpan(0, 0, 0, 1);
                    dispatcherTimerSegundos.Start();
                }
                catch (Exception ex)
                {

                }

                uscTileMap.listaBlocoCenarioAux = new List<UscBlocoCenario>();

                ConfigGame cg = DadosGerais.configGame;
                uscTileMap.MapColunas = cg.MapColunas;
                uscTileMap.MapLinhas = cg.MapLinhas;
                uscTileMap.Map = new UscBlocoCenario[uscTileMap.MapColunas, uscTileMap.MapLinhas];
                uscTileMap.PowerDotAtiva = false;
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
                        uscTileMap.InsereBlocos(ubc);
                        uscTileMap.Map[i, j] = ubc;


                    }
                }
                uscTileMap.InsereSuperPastilha();
                uscTileMap.InserePacman();
                uscTileMap.InsereListaGhost();
                toutDuracaGame = TOUT_DURACAO_GAME;
                toutTempoJogado = 0;
                pontos = 0;
                txtPonto.Content = pontos + " pontos";
                rbVida.Value = 3;
                IniciaThread();
                toutPowerDotAtiva = 0;
                uscTileMap.DesativaPowerDot();
                audioGameWin.Play(false);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void Finaliza()
        {
            try
            {
                dispatcherTimer.Stop();
                dispatcherTimerSegundos.Stop();
            }
            catch (Exception ex)
            {

            }
            try
            {
                try
                {
                    dispatcherTimer.Tick -= DispatcherTimer_Tick;

                }
                catch (Exception ex)
                {

                }
                try
                {
                    dispatcherTimerSegundos.Tick -= DispatcherTimerSegundos_Tick;

                }
                catch (Exception ex)
                {

                }

                try
                {
                    uscTileMap.grdCenario.Children.Clear();
                }
                catch (Exception ex)
                {

                }
                toutDuracaGame = 0;
            }
            catch (Exception ex)
            {

            }
        }

        public void RetomaPosicao()
        {
            try
            {
                uscTileMap.RemoveTodosOsGhost();
                uscTileMap.RemovePacman();
                uscTileMap.InserePacman();
                uscTileMap.InsereListaGhost();
            }
            catch (Exception ex)
            {

            }
        }

        Thread threadToutGame;
        public void IniciaThread()
        {
            try
            {

                try
                {
                    threadToutGame = new Thread(dispatcherTimer_Tick);
                    threadToutGame.Priority = ThreadPriority.Highest;
                    threadToutGame.Start();

                }
                catch (Exception)
                {

                }

            }
            catch (Exception ex)
            { }
        }

        private void DispatcherTimerSegundos_Tick(object sender, EventArgs e)
        {
            try
            {
                toutTempoJogado++;
                txtTempo.Content = toutTempoJogado + " sec";
                if (toutPowerDotAtiva > 0)
                {

                    if (--toutPowerDotAtiva == 0)
                    {
                        toutPowerDotAtiva--;
                        uscTileMap.PowerDotAtiva = false;
                        uscTileMap.DesativaPowerDot();
                        audioPowerDots.StopPlaying();


                    }
                }
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

                        GameLoop();
                        //if (--toutDuracaGame == 0)
                        //{
                        //    toutDuracaGame = TOUT_DURACAO_GAME;
                        //}
                    }

                    if (toutMudaDirecaoGhost > 0)
                    {
                        toutMudaDirecaoGhost--;
                    }
                    if (toutMudaDirecaoGhost_2 > 0)
                    {
                        toutMudaDirecaoGhost_2--;
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

        public void VerificaColisaoPastilha()
        {
            try
            {
                if (uscTileMap.VerificaColisaoPastilha(uscTileMap.Pacman.PosLeft, uscTileMap.Pacman.PosTop, uscTileMap.Pacman.direcaoAtual, ENUM_TIPO_BLOCO.PASTILHA) == true)
                {
                    pontos += 10;
                    txtPonto.Content = pontos + " pontos";
                    audioWaka.Play(false);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void VerificaColisaoSuperPastilha()
        {
            try
            {
                if (uscTileMap.VerificaColisaoPastilha(uscTileMap.Pacman.PosLeft, uscTileMap.Pacman.PosTop, uscTileMap.Pacman.direcaoAtual, ENUM_TIPO_BLOCO.SUPER_PASTILHA) == true)
                {
                    pontos += 50;
                    txtPonto.Content = pontos + " pontos";
                    audioPowerDots.Play(true);
                    uscTileMap.PowerDotAtiva = true;
                    toutPowerDotAtiva = TOUT_POWER_DOT_ATIVA;
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

                VerificaColisaoBloco();
                MovePacman();
                VerificaColisaoPastilha();
                VerificaColisaoSuperPastilha();
                uscTileMap.AtualizaPosicaoGhost();
            }
            catch (Exception ex)
            {

            }
        }

        private void VerificaVitoria()
        {
            try
            {
                if (uscTileMap.VerificaVitoria() == true)
                {
                    GameWin?.Invoke(pontos.ToString(), toutTempoJogado.ToString());
                    audioGameWin.Play(false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void MovePacman()
        {
            if (uscTileMap.Pacman.direcaoAtual != uscTileMap.Pacman.requestedMovingDirection)
            {

                if (uscTileMap.VerificaCaminhoLivre(uscTileMap.Pacman.PosLeft, uscTileMap.Pacman.PosTop, uscTileMap.Pacman.requestedMovingDirection) == true)
                {
                    uscTileMap.MovePacman();

                }
            }
            uscTileMap.AtualizaPosicaoPacman();



        }
        public void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.Key)
                {
                    case Key.Down:
                        if (uscTileMap.Pacman.direcaoAtual == ENUM_DIRECAO.UP)
                            uscTileMap.Pacman.direcaoAtual = ENUM_DIRECAO.DOWN;
                        uscTileMap.Pacman.requestedMovingDirection = ENUM_DIRECAO.DOWN;
            
                        this.uscTileMap.Pacman.FezPrimeiroMovimento = true;

                        break;
                    case Key.Up:
                        if (uscTileMap.Pacman.direcaoAtual == ENUM_DIRECAO.DOWN)
                            uscTileMap.Pacman.direcaoAtual = ENUM_DIRECAO.UP;
                        uscTileMap.Pacman.requestedMovingDirection = ENUM_DIRECAO.UP;

                        this.uscTileMap.Pacman.FezPrimeiroMovimento = true;

                        break;
                    case Key.Left:
                        if (uscTileMap.Pacman.direcaoAtual == ENUM_DIRECAO.RIGHT)
                            uscTileMap.Pacman.direcaoAtual = ENUM_DIRECAO.LEFT;
                        uscTileMap.Pacman.requestedMovingDirection = ENUM_DIRECAO.LEFT;
                
                        this.uscTileMap.Pacman.FezPrimeiroMovimento = true;

                        break;
                    case Key.Right:
                        if (uscTileMap.Pacman.direcaoAtual == ENUM_DIRECAO.LEFT)
                            uscTileMap.Pacman.direcaoAtual = ENUM_DIRECAO.RIGHT;
                        uscTileMap.Pacman.requestedMovingDirection = ENUM_DIRECAO.RIGHT;
               
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



        public void dispatcherTimer_Tick()
        {
            while (true)
            {
                try
                {
                    if (toutDuracaGame > 0)
                    {
                        if (toutMudaDirecaoGhost == 0)
                        {
                            Dispatcher.Invoke(new Action(() =>
                                             {
                                                 uscTileMap.VerificaDirecaoDiferenteGhost();
                                             }));
                            toutMudaDirecaoGhost = TOUT_MUDA_DIRECAO_GHOST;

                        }
                        if (toutMudaDirecaoGhost_2 == 0)
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                uscTileMap.ColisaoGhostComGhost();
                                toutMudaDirecaoGhost_2 = TOUT_MUDA_DIRECAO_GHOST_2;

                            }));
                        }

                        Dispatcher.Invoke(new Action(() =>
                        {

                            if (toutPowerDotAtiva > 0)
                            {

                                if (toutPowerDotAtiva < 3)
                                {
                                    uscTileMap.AtivaPowerDot(true);

                                }
                                else
                                {
                                    uscTileMap.AtivaPowerDot(false);

                                }
                            }
                        }));
                        Dispatcher.Invoke(new Action(() =>
                        {

                            VerificaVitoria();
                        }));


                        Dispatcher.Invoke(new Action(() =>
                        {
                            if (uscTileMap.VerificaColisaoPacmanGhost() == true)
                            {
                                if (toutPowerDotAtiva > 0)
                                {
                                    audioEatGhost.Play(false);
                                }
                                else
                                {

                                    RetomaPosicao();
                                    audioGameOVer.Play(false);
                                    if (--rbVida.Value == 0)
                                    {
                                        GameOver?.Invoke(pontos.ToString(), toutTempoJogado.ToString());
                                    }
                                }
                            }
                        }));

                    }



                    //Temporizador do jogo
                    try
                    {
                        if (toutPowerDotAtiva > 0)
                        {

                        }

                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }
                catch (Exception ex)
                {

                    //throw ex;
                }
                Thread.Sleep(100);
            }

        }
    }
}
