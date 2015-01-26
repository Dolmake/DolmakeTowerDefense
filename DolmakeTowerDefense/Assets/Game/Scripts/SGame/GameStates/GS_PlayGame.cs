using UnityEngine;
using System.Collections;
using Utils;

namespace SGame.GameStates
{
    public class GS_PlayGame : GameState
    {
        public int EnemiesToLevelUp = 10;
        int _currentLevel = 0;
        int _lastEnemiesCounter = 0;

        protected override void OnActive()
        {
            Debug.Log(this.Name);
            //Set the current Level
            _currentLevel = BattleServer.SINGLETON.CurrentLevel;
            _lastEnemiesCounter = BattleServer.SINGLETON.DeadFoes;
            BattleServer.SINGLETON.Allied.LifeComponent.OnNoLife += new System.Action<Common.ILife>(Life_OnNoLife);
        }

       

        protected override void OnDeactive()
        {
            BattleServer.SINGLETON.Allied.LifeComponent.OnNoLife -= new System.Action<Common.ILife>(Life_OnNoLife);
        }

        public override void OnUpdate()
        {
            //Increase level?
            if (EnemiesDeadInLevel > EnemiesToLevelUp)
                LevelUp();
        }

        #region MISC

        //Allied has lost!!!!
        void Life_OnNoLife(Common.ILife obj)
        {
            this.FiniteStateMachine.ChangeState(this.NextState);
        }
       

        private int EnemiesDeadInLevel
        {
            get
            {

                return BattleServer.SINGLETON.DeadFoes - _lastEnemiesCounter;
            }
        }

        void LevelUp()
        {
            _lastEnemiesCounter = BattleServer.SINGLETON.DeadFoes;
            EnemiesToLevelUp++;
            BattleServer.SINGLETON.IncreaseLevel();
        }
        #endregion
    }
}
