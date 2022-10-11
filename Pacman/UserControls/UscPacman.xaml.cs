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
    /// Interação lógica para UscPacman.xam
    /// </summary>
    public partial class UscPacman : UserControl
    {
        public UscPacman()
        {
            InitializeComponent();
        }

        public enum ENUM_DIRECAO
        {
            UP,
            LEFT,
            RIGHT,
            DOWN
        }
        public double velocidade { get; set; }
        //public bool Pause { get; set; }
        public bool FezPrimeiroMovimento { get;  set; }
        public bool ColisaoParede { get;  set; }
        public ENUM_DIRECAO direcaoAtual { get; set; }
        public ENUM_DIRECAO requestedMovingDirection { get; set; }
        private int _linha;

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
            return !this.FezPrimeiroMovimento  || this.ColisaoParede;
        }
      

        public void AtualizaPosicao()
        {
            try
            {
                if (Pause() == false)
                {
                    RotateTransform rotate;
                    switch (this.direcaoAtual)
                    {
                        case ENUM_DIRECAO.LEFT:
                            this.PosLeft -= this.velocidade;
                            this.Margin = new Thickness(this.PosLeft, this.PosTop, 0, 0);
                            rotate =
    new RotateTransform(180);
                            grdImagens.RenderTransform = rotate;
                            break;
                        case ENUM_DIRECAO.UP:
                            this.PosTop -= this.velocidade;
                            this.Margin = new Thickness(this.PosLeft, this.PosTop, 0, 0);

                            rotate =
  new RotateTransform(-90);
                            grdImagens.RenderTransform = rotate;
                            break;
                        case ENUM_DIRECAO.RIGHT:
                            this.PosLeft += this.velocidade;
                            this.Margin = new Thickness(this.PosLeft, this.PosTop, 0, 0);

                            rotate =
new RotateTransform(0);
                            grdImagens.RenderTransform = rotate;
                            break;
                        case ENUM_DIRECAO.DOWN:
                            this.PosTop += this.velocidade;
                            rotate =
new RotateTransform(90);
                            grdImagens.RenderTransform = rotate;
                            this.Margin = new Thickness(this.PosLeft, this.PosTop, 0, 0);


                            break;
                    }
                    //this.txtMarginLeft.Text = this.PosLeft.ToString();
                    //this.txtMarginTop.Text = this.PosTop.ToString();

                    //this.direcaoAtual = direcao;

                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
