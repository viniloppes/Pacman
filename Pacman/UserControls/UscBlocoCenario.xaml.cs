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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pacman.UserControls
{
    /// <summary>
    /// Interação lógica para UscBlocoCenario.xam
    /// </summary>
    public partial class UscBlocoCenario : UserControl
    {
        public UscBlocoCenario()
        {
            InitializeComponent();
        }

        private string _nomeArquivo;

        public string NomeArquivo
        {
            get
            {
                imgBloco.Source = new BitmapImage(
                    new Uri("/Pacman;component/Imagens/blocos/" + _nomeArquivo,
                    UriKind.Relative));

                return _nomeArquivo;
            }
            set
            {
                _nomeArquivo = value;
                imgBloco.Source = new BitmapImage(
                 new Uri("/Pacman;component/Imagens/blocos/" + _nomeArquivo,
                 UriKind.Relative));
            }
        }


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
        public enum ENUM_TIPO_BLOCO
        {
            VAZIO,
            PAREDE,
            PASTILHA,
            SUPER_PASTILHA
        }
        private double _posLeft;

        public double PosLeft
        {
            get { return _posLeft; }
            set { _posLeft = value; }
        }

        private double _posTop;

        public double PosTop
        {
            get { return _posTop; }
            set { _posTop = value; }
        }


        private ENUM_TIPO_BLOCO _tipoBloco;

        public ENUM_TIPO_BLOCO TipoBloco
        {
            get
            {
                switch (_tipoBloco)
                {
                    case ENUM_TIPO_BLOCO.VAZIO:
                        imgBloco.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/blocos/vazio.png", UriKind.Relative));
                        grdSuperPastilhaAnimada.Visibility = Visibility.Hidden;

                        break;
                    case ENUM_TIPO_BLOCO.PAREDE:
                        imgBloco.Source =
                            new BitmapImage(
                            new Uri("/Pacman;component/Imagens/blocos/" + _nomeArquivo,
                            UriKind.Relative));
                        grdSuperPastilhaAnimada.Visibility = Visibility.Hidden;

                        break;
                    case ENUM_TIPO_BLOCO.PASTILHA:
                        imgBloco.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/blocos/pastilha.png", UriKind.Relative));
                        grdSuperPastilhaAnimada.Visibility = Visibility.Hidden;

                        break;
                    case ENUM_TIPO_BLOCO.SUPER_PASTILHA:
                        imgBloco.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/blocos/super-pastilha.png", UriKind.Relative));
                        grdSuperPastilhaAnimada.Visibility = Visibility.Visible;

                        break;
                }
                return _tipoBloco;
            }
            set
            {
                _tipoBloco = value;

                switch (_tipoBloco)
                {
                    case ENUM_TIPO_BLOCO.VAZIO:
                        imgBloco.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/blocos/vazio.png", UriKind.Relative));
                        grdSuperPastilhaAnimada.Visibility = Visibility.Hidden;

                        break;
                    case ENUM_TIPO_BLOCO.PAREDE:
                        imgBloco.Source =
                            new BitmapImage(
                            new Uri("/Pacman;component/Imagens/blocos/block.png",
                            UriKind.Relative));
                        grdSuperPastilhaAnimada.Visibility = Visibility.Hidden;


                        break;
                    case ENUM_TIPO_BLOCO.PASTILHA:
                        imgBloco.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/blocos/pastilha.png", UriKind.Relative));
                        grdSuperPastilhaAnimada.Visibility = Visibility.Hidden;

                        break;
                    case ENUM_TIPO_BLOCO.SUPER_PASTILHA:
                        imgBloco.Source = new BitmapImage(new Uri("/Pacman;component/Imagens/blocos/super-pastilha.png", UriKind.Relative));
                        grdSuperPastilhaAnimada.Visibility = Visibility.Visible;
                        break;


                }
            }
        }

    }
}
