using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;



namespace Scripts.SMain
{
    public class Core : MonoBehaviour
    {
        Stack<string> StackLevels = new Stack<string>();
        Stack<string> StackSubLevelsForScene = new Stack<string>();
        Stack<GameObject> StackSubLevelsGO_off = new Stack<GameObject>();

        public string INI_SCENE;       

        private static Core _instance = null;
        public static Core SINGLETON
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Current Subscene loaded
        /// </summary>
        public string CurrentSubScene
        {
            get
            {
                if (StackSubLevelsForScene.Count == 0) return null;

                return StackSubLevelsForScene.Peek();
            }

        }

        string _currentScene = "";
        /// <summary>
        /// Current Scene playing
        /// </summary>
        public string CurrentScene
        {
            get
            {
                return _currentScene;
            }
            private set
            {
                _currentScene = value;
            }

        }

        /// <summary>
        /// Number of Subscenes
        /// </summary>
        public int CountSubScenes
        {
            get
            {
                return StackSubLevelsForScene.Count;
            }
        }

        /// <summary>
        /// Number of Scenes on the stack
        /// </summary>
        public int CountScenes
        {
            get
            {
                return StackLevels.Count;
            }
        }

        /// <summary>
        /// A reference to the last subscen
        /// </summary>
        public GameObject LastSubSceneGO
        {
            get
            {
                if (StackSubLevelsGO_off.Count == 0) return null;

                return StackSubLevelsGO_off.Peek();
            }

        }

        public int TargetFrame = 1000;       
        
        void Awake()
        {
            _instance = this;
            Application.targetFrameRate = TargetFrame;
            GameObject.DontDestroyOnLoad(this);
            Debug.Log(this.ToString() + " Awaked");

            StackLevels.Push(Application.loadedLevelName);
        }
        // Use this for initialization
        void Start()
        {
            LoadScene(INI_SCENE);           
        }
       
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GoBack();
            }
        }

        /// <summary>
        /// Go to a previous scene
        /// </summary>
        public void GoBack()
        {
            PopScene();
            if (StackLevels.Count <= 1)
                Application.Quit();
            else
                LoadTop();
        }

        /// <summary>
        /// Go back to previous scene on the stack
        /// </summary>
        /// <param name="LevelToLoad"></param>
        public void GoBack(string LevelToLoad)
        {
            while (StackLevels.Peek() != LevelToLoad)
                PopScene();

            if (StackLevels.Count <= 1)
                Application.Quit();
            else
                LoadTop();
        }


        /// <summary>
        /// Add a subscene
        /// </summary>
        /// <param name="LevelToAdd"></param>
        public void AddSubScene(string LevelToAdd)
        {
            if (!string.IsNullOrEmpty(LevelToAdd))
            {
                GameObject goOff = GameObject.Find(CurrentSubScene);
                if (goOff)
                {
                    StackSubLevelsGO_off.Push(goOff);
                    goOff.SetActive(false);
                    Debug.Log("Deactivating, " + CurrentSubScene);
                }

                Application.LoadLevelAdditive(LevelToAdd);
                StackSubLevelsForScene.Push(LevelToAdd);
            }
        }

        /// <summary>
        /// Remove the las subscene
        /// </summary>
        /// <returns></returns>
        public bool RemoveSubScene()
        {
            if (StackSubLevelsForScene.Count == 0) return false;

            bool success = false;
            string LevelToRemove = StackSubLevelsForScene.Pop();

            if (!string.IsNullOrEmpty(LevelToRemove))
            {
                GameObject goToRemove = GameObject.Find(LevelToRemove);
                if (goToRemove)
                {
                    DestroyImmediate(goToRemove);
                    success = true;
                }
            }
            if (success)
            {
                Debug.Log(LastSubSceneGO + ",Active");
                if (LastSubSceneGO)
                {
                    LastSubSceneGO.SetActive(true);
                    StackSubLevelsGO_off.Pop();
                }
            }
            return success;

        }

        /// <summary>
        /// Loads a Scene
        /// </summary>
        /// <param name="LevelToLoad"></param>
        public void LoadScene(string LevelToLoad)
        {
            if (!string.IsNullOrEmpty(LevelToLoad))
            {
                StackLevels.Push(LevelToLoad);
                LoadTop();
            }
        }

        /// <summary>
        /// Remove from the stack the current Scene
        /// </summary>
        public void PopScene()
        {
            StackLevels.Pop();           
        }  
      

        void LoadTop()
        { 
            while (RemoveSubScene()) ;

            string scene = StackLevels.Peek();           

            
            Application.LoadLevel(scene);
            CurrentScene = scene;

            
        }

        public override string ToString()
        {
            return "Core";
        }
    }
}
