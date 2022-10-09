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
    /// Interação lógica para UscTileMap.xam
    /// </summary>
    public partial class UscTileMap : UserControl
    {
        public UscTileMap()
        {
            InitializeComponent();
            grdCenario.Children.Clear();
        }

        private UscBlocoCenario[,] _map;
        public UscPacman Pacman { get; set; }

        public int TileSize { get; set; }
        private int _mapLinhas;
        private int _mapColunas;

        public int MapColunas
        {
            get { return _mapColunas; }
            set { _mapColunas = value; }
        }

        public int MapLinhas
        {
            get { return _mapLinhas; }
            set { _mapLinhas = value; }
        }


        public UscBlocoCenario[,] Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;

            }
        }

        public bool VerificaColisaoParede(double x, double y, ENUM_DIRECAO direcao)
        {
            try
            {
                //if (direcao == null)
                //{
                //    return false;
                //}
                string verificaX = (x / this.TileSize).ToString();
                string verificaY = (y / this.TileSize).ToString();
                int intAux;
                if (Int32.TryParse(verificaX, out intAux) &&
                    Int32.TryParse(verificaY, out intAux))
                {
                    int linha = 0;
                    int coluna = 0;
                    int proximaColuna;
                    int proximaLinha;

                    switch (direcao)
                    {
                        case ENUM_DIRECAO.RIGHT:
                            proximaColuna = (int)x + this.TileSize;
                            coluna = proximaColuna / this.TileSize;
                            linha = (int)y / this.TileSize;
                            break;
                        case ENUM_DIRECAO.LEFT:
                            proximaColuna = (int)x - this.TileSize;
                            coluna = proximaColuna / this.TileSize;
                            linha = (int)y / this.TileSize;
                            break;
                        case ENUM_DIRECAO.UP:
                            proximaLinha = (int)y - this.TileSize;
                            linha = proximaLinha / this.TileSize;
                            coluna = (int)x / this.TileSize;
                            break;
                        case ENUM_DIRECAO.DOWN:
                            proximaLinha = (int)y + this.TileSize;
                            linha = proximaLinha / this.TileSize;
                            coluna = (int)x / this.TileSize;
                            break;

                    }
                    var m = this.Map[linha, coluna];
                    if (m.TipoBloco == UscBlocoCenario.ENUM_TIPO_BLOCO.PAREDE)
                    {
                        return true;
                    }
                }

                return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool VerificaCaminhoLivre(double x, double y, ENUM_DIRECAO direcao)
        {
            try
            {
                string verificaX = (x / this.TileSize).ToString();
                string verificaY = (y / this.TileSize).ToString();
                int intAux;
                if (Int32.TryParse(verificaX, out intAux) &&
                    Int32.TryParse(verificaY, out intAux))
                {
                    if (VerificaColisaoParede(x, y, direcao) == false)
                    {
                        return true;
                    }

                  
                }

                return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void imprimeBloco(UscBlocoCenario ubc)
        {

            ubc.Margin = new Thickness(ubc.PosLeft, ubc.PosTop, 0, 0);
            ubc.HorizontalAlignment = HorizontalAlignment.Left;
            ubc.VerticalAlignment = VerticalAlignment.Top;
            ubc.Height = this.TileSize;
            ubc.Width = this.TileSize;
            grdCenario.Children.Add(ubc);


        }
        public void InserePacman(UscPacman up)
        {
            up.Height = this.TileSize;
            up.Width = this.TileSize;
            up.Margin = new Thickness(up.PosLeft, up.PosTop, 0, 0);
            up.HorizontalAlignment = HorizontalAlignment.Left;
            up.VerticalAlignment = VerticalAlignment.Top;
            grdCenario.Children.Add(up);

            Pacman = up;
        }


    }
}
