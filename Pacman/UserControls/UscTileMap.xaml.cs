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
using static Pacman.UserControls.UscBlocoCenario;
using static Pacman.UserControls.UscEnemy;
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

        public List<UscBlocoCenario> listaBlocoCenarioAux { get; set; }
        public List<UscEnemy> listaGhost { get; set; }
        public List<ENUM_DIRECAO> _listaDirecoesGhostDisponiveis { get; private set; }

        private UscBlocoCenario[,] _map;
        public UscPacman Pacman { get; set; }
        //public UscEnemy Ghost { get; set; }
        public bool PowerDotActive { get; set; }
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

                var m = RenornaColisaoUscBlocoCenario(x, y, direcao);
                if (m != null)
                {

                    if (m.TipoBloco == UscBlocoCenario.ENUM_TIPO_BLOCO.PAREDE)
                    {
                        return true;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return false;
        }

        public UscBlocoCenario RenornaColisaoUscBlocoCenario(double x, double y, ENUM_DIRECAO direcao)
        {
            try
            {
                UscBlocoCenario ubc = null;
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
                    ubc = this.Map[linha, coluna];
                }
                return ubc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return null;

        }

        public bool VerificaColisaoPastilha(double x, double y, ENUM_DIRECAO direcao, ENUM_TIPO_BLOCO tipoPastilha)
        {
            try
            {
                //if (m != null)
                //{
                for (int i = 0; i < this.MapLinhas; i++)
                {
                    for (int j = 0; j < this.MapColunas; j++)
                    {
                        var m = Map[i, j];
                        //      if (m.TipoBloco == ENUM_TIPO_BLOCO.PASTILHA ||
                        //m.TipoBloco == ENUM_TIPO_BLOCO.SUPER_PASTILHA)              
                        if (m.TipoBloco == tipoPastilha)
                        {

                            if (x < m.PosLeft + (m.Width / 3) &&
                               x + (m.Width / 3) > m.PosLeft &&
                               y < m.PosTop + (m.Height / 3) &&
                               y + (m.Height / 3) > m.PosTop
                                )
                            {
                                //return ubcColisao;
                                ((UscBlocoCenario)grdCenario.Children[grdCenario.Children.IndexOf(m)]).TipoBloco = ENUM_TIPO_BLOCO.VAZIO;
                                m.TipoBloco = ENUM_TIPO_BLOCO.VAZIO;
                                return true;
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return false;
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


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return false;
        }
        public void RemoveBloco(UscBlocoCenario ubc)
        {

            try
            {
                //Map[ubc.Linha, ubc.Coluna] = null;
                UscBlocoCenario ubcRepositor = new UscBlocoCenario();
                ubcRepositor.PosLeft = ubc.Width * ubc.Coluna;
                ubcRepositor.PosTop = ubc.Height * ubc.Linha;
                ubcRepositor.Linha = ubc.Linha;
                ubcRepositor.Coluna = ubc.Coluna;
                ubcRepositor.HorizontalAlignment = HorizontalAlignment.Left;
                ubcRepositor.VerticalAlignment = VerticalAlignment.Top;
                ubcRepositor.TipoBloco = UscBlocoCenario.ENUM_TIPO_BLOCO.PASTILHA;
                InsereBlocos(ubcRepositor);
                grdCenario.Children.Remove(ubc);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        public void InsereBlocos(UscBlocoCenario ubc)
        {
            try
            {
                ubc.Margin = new Thickness(ubc.PosLeft, ubc.PosTop, 0, 0);
                ubc.HorizontalAlignment = HorizontalAlignment.Left;
                ubc.VerticalAlignment = VerticalAlignment.Top;

                ubc.Height = this.TileSize;
                ubc.Width = this.TileSize;
                grdCenario.Children.Add(ubc);

                this.listaBlocoCenarioAux.Add(ubc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }



        }


        public void InsereListaGhost()
        {
            try
            {
                listaGhost = new List<UscEnemy>();
                var listaBlocoVazio = this.listaBlocoCenarioAux.FindAll(x => x.TipoBloco != ENUM_TIPO_BLOCO.PAREDE);
                int qtd = listaBlocoVazio.Count;
                int aux = 0;
                for (int i = 0; i < qtd; i += 10)
                {
                    if (i > qtd / 1.5)
                    {

                        UscEnemy enemy = new UscEnemy();
                        try
                        {
                            var ue = grdCenario.Children.OfType<UscEnemy>().OrderBy(x => x.Id).ToList().FindLast(x => x.Id > 0);

                            if (ue != null)
                            {
                                enemy.Id = ue.Id + 1;

                            }
                            else
                            {
                                enemy.Id = 1;

                            }

                        }
                        catch (Exception ex)
                        {
                            enemy.Id = 1;
                        }
                        enemy.EstadoGhostAtual = ENUM_ESTADO_GHOST.NORMAL;
                        enemy.PosLeft = listaBlocoVazio[i].PosLeft;
                        enemy.PosTop = listaBlocoVazio[i].PosTop;
                        enemy.Coluna = listaBlocoVazio[i].Coluna;
                        enemy.Linha = listaBlocoVazio[i].Linha;
                        enemy.velocidade = 10;
                        enemy.HorizontalAlignment = HorizontalAlignment.Left;
                        enemy.VerticalAlignment = VerticalAlignment.Top;
                        enemy.Margin = listaBlocoVazio[i].Margin;
                        grdCenario.Children.Add(enemy);
                        listaGhost.Add(enemy);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public void RemoveTodosOsGhost()
        {
            try
            {
                listaGhost = null;
                foreach (UscEnemy ghost in grdCenario.Children.OfType<UscEnemy>().ToList())
                {
                    grdCenario.Children.Remove(ghost);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        public void InserePacman()
        {
            var blocoVazio = this.listaBlocoCenarioAux.Find(x => x.TipoBloco != ENUM_TIPO_BLOCO.PAREDE);

            UscPacman up = new UscPacman();
            up.Width = 50;
            up.Height = 50;
            up.PosLeft = blocoVazio.PosLeft;
            up.PosTop = blocoVazio.PosTop;
            up.velocidade = 10;
            up.FezPrimeiroMovimento = false;
            up.Height = this.TileSize;
            up.Width = this.TileSize;
            up.Margin = new Thickness(up.PosLeft, up.PosTop, 0, 0);
            up.HorizontalAlignment = HorizontalAlignment.Left;
            up.VerticalAlignment = VerticalAlignment.Top;
            grdCenario.Children.Add(up);

            Pacman = up;
        }

        public void RemovePacman()
        {
            try
            {
                grdCenario.Children.Remove(Pacman);
                Pacman = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }


        public void VerificaDirecaoDiferenteGhost()
        {
            try
            {

                foreach (UscEnemy ghost in grdCenario.Children.OfType<UscEnemy>())
                {
                    if (Int32.TryParse((ghost.PosLeft / this.TileSize).ToString(), out var r1) &&
                        Int32.TryParse((ghost.PosTop / this.TileSize).ToString(), out var r2))
                    {
                        _listaDirecoesGhostDisponiveis = new List<ENUM_DIRECAO>();
                        switch (ghost.direcaoAtual)
                        {

                            case ENUM_DIRECAO.LEFT:
                                var retornoUp = RenornaColisaoUscBlocoCenario(ghost.PosLeft, ghost.PosTop, ENUM_DIRECAO.UP);
                                if (retornoUp != null)
                                {
                                    if (retornoUp.TipoBloco != ENUM_TIPO_BLOCO.PAREDE)
                                    {
                                        _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.UP);
                                        ghost.mudandoDirecao = true;
                                    }
                                }
                                var retornoDown = RenornaColisaoUscBlocoCenario(ghost.PosLeft, ghost.PosTop, ENUM_DIRECAO.DOWN);
                                if (retornoDown != null)
                                {
                                    if (retornoDown.TipoBloco != ENUM_TIPO_BLOCO.PAREDE)
                                    {
                                        _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.DOWN);
                                        ghost.mudandoDirecao = true;

                                    }

                                }
                                _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.RIGHT);
                                if (!ghost.ColisaoParede) _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.LEFT);
                                break;
                            case ENUM_DIRECAO.UP:
                                var retornoLeft = RenornaColisaoUscBlocoCenario(ghost.PosLeft, ghost.PosTop, ENUM_DIRECAO.LEFT);
                                if (retornoLeft != null)
                                {
                                    if (retornoLeft.TipoBloco != ENUM_TIPO_BLOCO.PAREDE)
                                    {
                                        _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.LEFT);
                                        ghost.mudandoDirecao = true;
                                    }
                                }
                                var retornoRight = RenornaColisaoUscBlocoCenario(ghost.PosLeft, ghost.PosTop, ENUM_DIRECAO.RIGHT);
                                if (retornoRight != null)
                                {
                                    if (retornoRight.TipoBloco != ENUM_TIPO_BLOCO.PAREDE)
                                    {
                                        _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.RIGHT);
                                        ghost.mudandoDirecao = true;
                                    }
                                }
                                _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.DOWN);
                                if (!ghost.ColisaoParede) _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.UP);
                                break;
                            case ENUM_DIRECAO.RIGHT:
                                var retornoUp2 = RenornaColisaoUscBlocoCenario(ghost.PosLeft, ghost.PosTop, ENUM_DIRECAO.UP);
                                if (retornoUp2 != null)
                                {
                                    if (retornoUp2.TipoBloco != ENUM_TIPO_BLOCO.PAREDE)
                                    {
                                        _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.UP);
                                        ghost.mudandoDirecao = true;
                                    }
                                }
                                var retornoDown2 = RenornaColisaoUscBlocoCenario(ghost.PosLeft, ghost.PosTop, ENUM_DIRECAO.DOWN);
                                if (retornoDown2 != null)
                                {
                                    if (retornoDown2.TipoBloco != ENUM_TIPO_BLOCO.PAREDE)
                                    {
                                        _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.DOWN);
                                        ghost.mudandoDirecao = true;
                                    }
                                }
                                _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.LEFT);
                                if (!ghost.ColisaoParede) _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.RIGHT);
                                break;
                            case ENUM_DIRECAO.DOWN:
                                var retornoLeft2 = RenornaColisaoUscBlocoCenario(ghost.PosLeft, ghost.PosTop, ENUM_DIRECAO.LEFT);
                                if (retornoLeft2 != null)
                                {
                                    if (retornoLeft2.TipoBloco != ENUM_TIPO_BLOCO.PAREDE)
                                    {
                                        _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.LEFT);
                                        ghost.mudandoDirecao = true;

                                    }


                                }
                                var retornoRight2 = RenornaColisaoUscBlocoCenario(ghost.PosLeft, ghost.PosTop, ENUM_DIRECAO.RIGHT);
                                if (retornoRight2 != null)
                                {
                                    if (retornoRight2.TipoBloco != ENUM_TIPO_BLOCO.PAREDE)
                                    {
                                        _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.RIGHT);
                                        ghost.mudandoDirecao = true;

                                    }

                                }
                                _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.UP);
                                if (!ghost.ColisaoParede) _listaDirecoesGhostDisponiveis.Add(ENUM_DIRECAO.DOWN);
                                break;
                        }
                        Random rnd = new Random();
                        if (_listaDirecoesGhostDisponiveis.Count >= 3)
                        {

                            int randomDirecao = rnd.Next(0, (int)this._listaDirecoesGhostDisponiveis.Count);
                            ghost.direcaoAtual = _listaDirecoesGhostDisponiveis[randomDirecao];
                        }
                        else
                        {
                            if (VerificaColisaoParede(ghost.PosLeft, ghost.PosTop, ghost.direcaoAtual))
                            {
                                int randomDirecao = rnd.Next(0, (int)this._listaDirecoesGhostDisponiveis.Count);
                                ghost.direcaoAtual = _listaDirecoesGhostDisponiveis[randomDirecao];
                            }

                        }
                        ghost.ColisaoParede = false;
                        ghost.mudandoDirecao = false;

                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void MovePacman()
        {
            try
            {
                Pacman.direcaoAtual = Pacman.requestedMovingDirection;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }



        public void AtualizaPosicaoPacman()
        {
            try
            {
                Pacman.AtualizaPosicao();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }






        public void AtualizaPosicaoGhost()
        {
            try
            {
                if (Pacman.FezPrimeiroMovimento == true)
                {
                    grdCenario.Children.OfType<UscEnemy>().ToList().ForEach(x =>
                                  {
                                      if (x.mudandoDirecao == false)
                                      {
                                          if (VerificaColisaoParede(x.PosLeft, x.PosTop, x.direcaoAtual) == true)
                                          {
                                              x.ColisaoParede = true;
                                          }
                                          else
                                          {
                                              x.ColisaoParede = false;

                                              x.AtualizaPosicao();
                                          }
                                      }

                                  });
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public bool VerificaColisaoPacmanGhost()
        {
            try
            {
                var listGhost = grdCenario.Children.OfType<UscEnemy>().ToList();
                var pacman = grdCenario.Children.OfType<UscPacman>().ToList().First<UscPacman>();
                if (listGhost != null)
                {

                    foreach (UscEnemy ghost in listGhost)
                    {
                        if (pacman.PosLeft < ghost.PosLeft + (ghost.Width) &&
                                  pacman.PosLeft + (ghost.Width) > ghost.PosLeft &&
                                  pacman.PosTop < ghost.PosTop + (ghost.Height) &&
                                  pacman.PosTop + (ghost.Height) > ghost.PosTop
                                   )
                        {

                            return true;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return false;

        }

        public bool ColisaoGhostComGhost()
        {
            try
            {
                var listGhost = grdCenario.Children.OfType<UscEnemy>().ToList();
                for (int i = 0; i < listGhost.Count; i++)
                {
                    for (int j = 0; j < listGhost.Count; j++)
                    {
                        if (i != j)
                        {
                            if (listGhost[i].PosLeft < listGhost[j].PosLeft + (listGhost[j].Width * 1.5) &&
                             listGhost[i].PosLeft + (listGhost[j].Width * 1.5) > listGhost[j].PosLeft &&
                             listGhost[i].PosTop < listGhost[j].PosTop + (listGhost[j].Height * 1.5) &&
                             listGhost[i].PosTop + (listGhost[j].Height * 1.5) > listGhost[j].PosTop
                              )
                            {
                                switch (listGhost[i].direcaoAtual)
                                {
                                    case (ENUM_DIRECAO.DOWN):
                                        listGhost[i].direcaoAtual = ENUM_DIRECAO.UP;
                                        break;
                                    case (ENUM_DIRECAO.UP):
                                        listGhost[i].direcaoAtual = ENUM_DIRECAO.DOWN;
                                        break;
                                    case (ENUM_DIRECAO.LEFT):
                                        listGhost[i].direcaoAtual = ENUM_DIRECAO.RIGHT;
                                        break;
                                    case (ENUM_DIRECAO.RIGHT):
                                        listGhost[i].direcaoAtual = ENUM_DIRECAO.LEFT;
                                        break;
                                }

                                switch (listGhost[j].direcaoAtual)
                                {
                                    case (ENUM_DIRECAO.DOWN):
                                        listGhost[j].direcaoAtual = ENUM_DIRECAO.UP;
                                        break;
                                    case (ENUM_DIRECAO.UP):
                                        listGhost[j].direcaoAtual = ENUM_DIRECAO.DOWN;
                                        break;
                                    case (ENUM_DIRECAO.LEFT):
                                        listGhost[j].direcaoAtual = ENUM_DIRECAO.RIGHT;
                                        break;
                                    case (ENUM_DIRECAO.RIGHT):
                                        listGhost[j].direcaoAtual = ENUM_DIRECAO.LEFT;
                                        break;
                                }
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro: ", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return false;
        }


        public void InsereSuperPastilha()
        {
            try
            {
                var listaAux = grdCenario.Children.OfType<UscBlocoCenario>().ToList().FindAll(x => x.TipoBloco != ENUM_TIPO_BLOCO.PAREDE);
                int count = listaAux.Count / 3;
                for (int i = count; i < listaAux.Count; i += count)
                {
                    ((UscBlocoCenario)grdCenario.Children[grdCenario.Children.IndexOf(listaAux[i])]).TipoBloco = ENUM_TIPO_BLOCO.SUPER_PASTILHA;

                }


            }
            catch (Exception ex)
            {

            }
        }

    }
}
