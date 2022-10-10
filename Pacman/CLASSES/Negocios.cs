using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static Pacman.UserControls.UscBlocoCenario;

namespace Pacman.CLASSES
{
    public static class Negocios
    {
        public static ConfigGame CarregaConfigGame(string arquivoConfig)
        {
            ConfigGame configGame = new ConfigGame();
            List<BlocoCenario> listaBc = new List<BlocoCenario>();
            try
            {
                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                dynamic resultado = serializer.DeserializeObject(File.ReadAllText(arquivoConfig));


                configGame.MapColunas = resultado["MapColunas"];
                configGame.MapLinhas = resultado["MapLinhas"];
                configGame.TileSize = resultado["TileSize"];
                var map = resultado["Map"];
                configGame.Map = new BlocoCenario[configGame.MapLinhas, configGame.MapColunas];
                
                foreach (dynamic o in map)
                {
                    BlocoCenario bc = new BlocoCenario();
                    bc.Coluna = o["Coluna"];
                    bc.Linha = o["Linha"];
                    bc.NomeArquivo = o["NomeArquivo"];
                    bc.PosLeft = o["PosLeft"];
                    bc.PosTop = o["PosTop"];
                    bc.TipoBloco = (ENUM_TIPO_BLOCO)o["TipoBloco"];
                    listaBc.Add(bc);
                }

                foreach (BlocoCenario blocoAux in listaBc)
                {
                    configGame.Map[blocoAux.Linha, blocoAux.Coluna] = blocoAux;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return configGame;
        }

        public static void SalvaConfigGame(string caminhoConfigGame, ConfigGame configGame)
        {
            try
            {

                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string strJson = serializer.Serialize(configGame);
                File.WriteAllText(caminhoConfigGame, strJson);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
