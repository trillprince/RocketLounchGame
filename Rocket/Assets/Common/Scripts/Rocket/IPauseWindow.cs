﻿using System;

namespace Common.Scripts.Rocket
{
    public interface IPauseWindow
    {
        public void PauseTheGame(Action onPause = null);
        public void UnpauseTheGame(Action onUnpause = null);
    }
}