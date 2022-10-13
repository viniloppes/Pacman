using Pacman.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pacman.UserControls.UscBlocoCenario;

namespace Pacman.CLASSES
{
    public class ConfigGame
    {

        private BlocoCenario[,] _map;
        //public UscPacman Pacman { get; set; }

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


        public BlocoCenario[,] Map
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
    }


    public class BlocoCenario
    {


        private string _nomeArquivo;

        public string NomeArquivo
        {
            get
            {
                return _nomeArquivo;
            }
            set
            {
                _nomeArquivo = value;
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
                return _tipoBloco;
            }
            set
            {
                _tipoBloco = value;
            }
        }

    }
}
