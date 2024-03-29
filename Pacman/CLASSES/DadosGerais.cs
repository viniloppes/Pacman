﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.CLASSES
{
    public static class DadosGerais
    {
        public static int toutMovePacman = 0;
        public const int TOUT_MOVE_PACMAN = 100;
        public static string caminhoApp = Directory.GetCurrentDirectory();
        public static string caminhoArquivoConfigGame = caminhoApp + @"\config.json";

        public static string caminhoAudio = caminhoApp + @"\sounds";
        public static ConfigGame configGame;


        public static string arquivoAudioWaka = caminhoAudio + @"\waka.wav";
        public static string arquivoAudioGameOver = caminhoAudio + @"\gameOver.wav";
        public static string arquivoAudioGameWin = caminhoAudio + @"\gameWin.wav";
        public static string arquivoAudioPowerDot = caminhoAudio + @"\power_dot.wav";
        public static string arquivoAudioEatGhost = caminhoAudio + @"\eat_ghost.wav";


    }
}
