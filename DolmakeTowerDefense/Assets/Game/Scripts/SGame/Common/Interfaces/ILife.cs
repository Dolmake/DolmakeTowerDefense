using UnityEngine;
using System.Collections;

namespace SGame.Common
{
    public interface ILife
    {
        event System.Action<ILife> OnLifeChanged, OnNoLife;
        void ResetLife();
        int StartingLife { get; set; }
        int Life { get; set; }
       
    }
}
