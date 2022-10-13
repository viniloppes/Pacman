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
using static Pacman.UserControls.UscPacman;

namespace Pacman.UserControls
{
    /// <summary>
    /// Interação lógica para UscEnemy.xam
    /// </summary>
    public partial class UscEnemy : UserControl
    {
        public UscEnemy()
        {
            InitializeComponent();
        }
        public int Id { get; set; }
        private bool _estadoAssustadoAcabando;

        public bool EstadoAssustadoAcabando
        {
            get
            {
                if(_estadoAssustadoAcabando == true)
                {
                    grdAnimaGhost.Visibility = Visibility.Visible;
                }else
                {
                    grdAnimaGhost.Visibility = Visibility.Hidden;

                }
                return _estadoAssustadoAcabando;
            }
            set
            {
                _estadoAssustadoAcabando = value;
                if (_estadoAssustadoAcabando == true)
                {
                    grdAnimaGhost.Visibility = Visibility.Visible;
                }
                else
                {
                    grdAnimaGhost.Visibility = Visibility.Hidden;

                }
            }
        }

        private ENUM_ESTADO_GHOST _estadoGhostAtual;

        public ENUM_ESTADO_GHOST EstadoGhostAtual
        {
            get
            {
                switch (_estadoGhostAtual)
                {
                    case ENUM_ESTADO_GHOST.NORMAL:
                        //imgEnemy.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/ghost.png", UriKind.Relative));

                        break;
                    case ENUM_ESTADO_GHOST.ASSUSTADO:

                        imgEnemy.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/ghosts/scaredGhost.png", UriKind.Relative));


                        break;
                }
                return _estadoGhostAtual;
            }
            set
            {
                _estadoGhostAtual = value;
                switch (_estadoGhostAtual)
                {
                    case ENUM_ESTADO_GHOST.NORMAL:
                        //imgEnemy.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/ghost.png", UriKind.Relative));

                        break;
                    case ENUM_ESTADO_GHOST.ASSUSTADO:
                        imgEnemy.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/ghosts/scaredGhost.png", UriKind.Relative));

                        break;
                }
            }
        }


        public enum ENUM_ESTADO_GHOST
        {
            NORMAL,
            ASSUSTADO
        }
        public bool ColisaoParede { get; set; }
        public ENUM_DIRECAO direcaoAtual { get; set; }
        //public ENUM_DIRECAO requestedMovingDirection { get; set; }
        private int _linha;
        public double velocidade { get; set; }
        //private bool _scared;

        //public bool Scared
        //{
        //    get
        //    {
        //        imgEnemy.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/scaredGhost.png", UriKind.Relative));

        //        return _scared;

        //    }
        //    set
        //    {
        //        _scared = value;
        //    }
        //}

        public bool mudandoDirecao { get; set; }
        public int Linha
        {
            get { return _linha; }
            set { _linha = value; }
        }
        private int _coluna;

        public int Coluna
        {
            get { return _coluna; }
            set { _coluna = value; }
        }
        private double _posLeft;

        public double PosLeft
        {
            get
            {
                return _posLeft;
            }
            set
            {
                _posLeft = value;
            }
        }

        private double _posTop;

        public double PosTop
        {
            get
            {

                return _posTop;
            }
            set
            {
                _posTop = value;
            }
        }

        public bool Pause()
        {
            return this.ColisaoParede || this.mudandoDirecao;
        }



        public void AtualizaPosicao()
        {
            try
            {
                if (Pause() == false)
                {

                    switch (this.direcaoAtual)
                    {
                        case ENUM_DIRECAO.LEFT:
                            if (this.EstadoGhostAtual == ENUM_ESTADO_GHOST.NORMAL)
                            {
                                imgEnemy.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/ghosts/left.png", UriKind.Relative));

                            }

                            this.PosLeft -= this.velocidade;
                            this.Margin = new Thickness(this.PosLeft, this.PosTop, 0, 0);
                            break;
                        case ENUM_DIRECAO.UP:
                            if (this.EstadoGhostAtual == ENUM_ESTADO_GHOST.NORMAL)
                            {
                                imgEnemy.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/ghosts/up.png", UriKind.Relative));

                            }

                            this.PosTop -= this.velocidade;
                            this.Margin = new Thickness(this.PosLeft, this.PosTop, 0, 0);
                            break;
                        case ENUM_DIRECAO.RIGHT:
                            if (this.EstadoGhostAtual == ENUM_ESTADO_GHOST.NORMAL)
                            {
                                imgEnemy.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/ghosts/right.png", UriKind.Relative));

                            }

                            this.PosLeft += this.velocidade;
                            this.Margin = new Thickness(this.PosLeft, this.PosTop, 0, 0);
                            break;
                        case ENUM_DIRECAO.DOWN:
                            if (this.EstadoGhostAtual == ENUM_ESTADO_GHOST.NORMAL)
                            {
                                imgEnemy.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/ghosts/down.png", UriKind.Relative));

                            }

                            this.PosTop += this.velocidade;
                            this.Margin = new Thickness(this.PosLeft, this.PosTop, 0, 0);


                            break;
                    }


                    Int32.TryParse((this.PosTop / this.Height).ToString(), out int l);
                    this.Linha = l;
                    Int32.TryParse((this.PosLeft / this.Width).ToString(), out int c);
                    this.Coluna = c;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
