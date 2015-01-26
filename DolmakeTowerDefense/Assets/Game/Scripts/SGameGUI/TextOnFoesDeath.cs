using UnityEngine;
using System.Collections;
using SGame;

namespace SGameGUI
{
    public class TextOnFoesDeath : MonoBehaviour
    {
        #region Components
        TextMesh _textMesh;
        TextMesh TextMesh
        {
            get
            {
                if (_textMesh == null)
                    _textMesh = GetComponent<TextMesh>();
                return _textMesh;
            }
        }
        #endregion



        // Update is called once per frame
        void Update()
        {
            TextMesh.text = BattleServer.SINGLETON.DeadFoes.ToString();
        }
    }
}
