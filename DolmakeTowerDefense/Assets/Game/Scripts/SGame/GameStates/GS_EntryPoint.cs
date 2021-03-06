﻿using UnityEngine;
using System.Collections;
using Utils;
using Scripts.SMain;


namespace SGame.GameStates
{
    public class GS_EntryPoint : GameState
    {
        protected override void OnActive()
        {
			//We load the GUI
            Core.SINGLETON.AddSubScene("SGameGUI");            
        }

        protected override void OnDeactive()
        {

        }

        public override void OnUpdate()
        {
            Debug.Log(this.Name);
            
            BattleServer.SINGLETON.StartPlaying();
            this.FiniteStateMachine.ChangeState(this.NextState);
        }
    }
}
