using UnityEngine;
using System.Collections;
using Utils;
using Scripts.SMain;
using SMain.Input;

namespace SGame.GameStates
{
    public class GS_EntryPoint : GameState
    {
        protected override void OnActive()
        {
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
