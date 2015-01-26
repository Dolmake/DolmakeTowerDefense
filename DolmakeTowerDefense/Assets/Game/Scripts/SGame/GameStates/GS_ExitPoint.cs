using UnityEngine;
using System.Collections;
using Utils;
using Scripts.SMain;

namespace SGame.GameStates
{
    public class GS_ExitPoint : GameState
    {

        protected override void OnActive()
        {
            Debug.Log(this.Name + "Thanks for playing");
        }

        protected override void OnDeactive()
        {

        }

        public override void OnUpdate()
        {
            Core.SINGLETON.LoadScene("SEndGame");
        }
    }
}
