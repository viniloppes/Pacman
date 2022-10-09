using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Pacman.CLASSES
{
    public static class Negocios
    {
        public static ConfigGame CarregaConfigGame(string arquivoConfig)
        {
            ConfigGame configGame = new ConfigGame();
            try
            {
                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                dynamic resultado = serializer.DeserializeObject(File.ReadAllText(arquivoConfig));

                //Produto p = new Produto();
                //p.NomeArquivo = Path.GetFileName(strProduto);
                //p.Id = resultado["Id"];
                //p.Nome = resultado["Nome"];
                //p.Descricao = resultado["Descricao"];
                //p.Preco = (float)resultado["Preco"];
                //p.Caminho_img = resultado["Caminho_img"];
                //p.Molas = new List<Mola>();
                //var teste = resultado["Molas"];
                //foreach (dynamic o in teste)
                //{
                //    try
                //    {
                //        Mola m = new Mola();
                //        m.Id = o["Id"];
                //        m.Qtd = o["Qtd"];
                //        try
                //        {
                //            m.QtdMax = o["QtdMax"];
                //        }
                //        catch (Exception ex)
                //        {
                //            m.QtdMax = m.Qtd;
                //        }
                //        try
                //        {
                //            dynamic statusObjeto = o["Status"];
                //            m.Status = new StatusMola();
                //            m.Status.Id = statusObjeto["Id"];
                //            m.Status.Valor = statusObjeto["Valor"];
                //        }
                //        catch (Exception ex)
                //        {

                //        }

                //        p.Molas.Add(m);
                //    }
                //    catch
                //    {
                //    }
                //}

                //p.Mola = resultado["mola"];
                //

                //listaProd.Add(p);


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
