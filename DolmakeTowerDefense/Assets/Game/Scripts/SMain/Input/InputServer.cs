using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SMain.Input
{
    public class InputServer : MonoBehaviour
    {
        /// <summary>
        /// Delegate that defines the events from rays   
        /// </summary>
        /// <param name="const_hit">Using REF for optimization</param>
        /// <param name="press"></param>
        /// <param name="pressId"></param>
        /// <param name="camera"></param>
        /// <param name="const_ray">Using REF for optimization</param>
        public delegate void InputRaycasthitHandler(ref RaycastHit const_hit,Press touch, Camera camera, ref Ray const_ray);
        
        /// <summary>
        /// Private delegate to manage subcriptions for OnRayWhilePress
        /// </summary>
        private InputRaycasthitHandler OnRayWhilePressInvoker;

        /// <summary>
        /// Collision event OnRayWhilePress
        /// </summary>
        public event InputRaycasthitHandler OnRayWhilePress
        {
            add
            {
                OnRayWhilePressInvoker += value;
            }

            remove
            {
                OnRayWhilePressInvoker -= value;
            }
        }

        /// <summary>
        /// Private delegate to manage subcriptions for OnRayAtPressInvoker
        /// </summary>
        private InputRaycasthitHandler OnRayAtPressInvoker;

        /// <summary>
        /// Collision event OnRayAtPress
        /// </summary>
        public event InputRaycasthitHandler OnRayAtPress
        {
            add
            {
                OnRayAtPressInvoker += value;
            }

            remove
            {
                OnRayAtPressInvoker -= value;
            }
        }

        /// <summary>
        /// Private delegate to manage subcriptions for OnRayAtLeaveInvoker
        /// </summary>
        private InputRaycasthitHandler OnRayAtLeaveInvoker;

        /// <summary>
        /// Collision event OnRayAtLeave
        /// </summary>
        public event InputRaycasthitHandler OnRayAtLeave
        {
            add
            {
                OnRayAtLeaveInvoker += value;
            }

            remove
            {
                OnRayAtLeaveInvoker -= value;
            }
        }

      
        /// <summary>
        /// Server private instance.
        /// </summary>
        private static InputServer _instance = null;

        /// <summary>
        /// Server SINGLETON.
        /// </summary>
        public static InputServer SINGLETON
        {
            get
            {
                return _instance;
            }
        }   
      
        /// <summary>
        /// Should we propagete inputs among cameras
        /// </summary>
        public bool PropagateInputs = true;

        /// <summary>
        /// Disable input ray layer
        /// </summary>
        public int DisableLayerRay;       

        /// <summary>
        /// Is touching the screen
        /// </summary>
        public bool Touching { get; private set; }

        /// <summary>
        /// Number of touches in a moment
        /// </summary>
        public int TouchesCount
        {
            get
            {
#if UNITY_EDITOR
                int counter = 0;
                for (int i = 0; i < Presses.Length; ++i)
                    counter += Presses[i].phase != TouchPhase.Canceled ? 1 : 0;
                return counter;
#elif UNITY_ANDROID || UNITY_IOS
                return UnityEngine.Input.touchCount;
#endif
            }
        }

        Press[] _presses;
        /// <summary>
        /// Presses available
        /// </summary>
        public Press[] Presses
        {
            get
            {
                
#if UNITY_EDITOR         
                if (_presses == null)
                    _presses = new Press[1];
                _presses[0].UpdateWithMouse();                             
#elif UNITY_ANDROID || UNITY_IOS      
                if (_presses == null)
                    _presses = new Press[10];
                for (int i = 0; i < UnityEngine.Input.touchCount; ++i)
                    _presses[i].UpdateWithTouch(UnityEngine.Input.touches[i]);                
#endif
                return _presses;
            }
        }


        /// <summary>
        /// Al cargar este elemento se inicializa el singleton y se evita que se destruya al cambiar de escena.
        /// </summary>
        void Awake()
        {
            _instance = this;
            GameObject.DontDestroyOnLoad(this);           
            DisableLayerRay = LayerMask.NameToLayer("Ignore Raycast");
            UnityEngine.Input.simulateMouseWithTouches = true;
            InitializeTouches();
        }

        private void InitializeTouches()
        {
            for (int i = 0; i < this.TouchesCount; ++i)
                Presses[i].Initialize();
        }


        /// <summary>
        /// En cada pasada del bucle se comprueba si hay un evento de entrada.
        /// En caso de que lo haya, identifica al objeto que lo ha recibido y le manda un mensaje.
        /// </summary>
        void Update()       
        {
            Touching = this.TouchesCount > 0;
            //Debug.Log("Touching:" + this.TouchesCount);
            for (int i = 0; i < this.TouchesCount; ++i)
                ProcessInput(Presses[i]);
            //Debug.Log("Raycast:" + UnityEngine.Input.touchCount);            
        }


        /// <summary>
        /// Lanza un rayo con todas las cámaras y envía el evento correspondiente al tipo de entrada.
        /// </summary>
        /// <param name="id">
        /// Identificador del input.<see cref="System.Int32"/>
        /// </param>
        /// <param name="type">
        /// Tipo de evento.<see cref="System.Int32"/>
        /// </param>
        void ProcessInput(Press press)
        {  
            bool rayCast = false;
            Camera[] cameras = Camera.allCameras;
            for (int i = 0; i < cameras.Length; ++i) //Por cada cámara de la escena se lanza un rayo            
            {
                Camera cam = cameras[i];
                if (!cam.enabled) continue;               

                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(press.Position2D);
                int layer = DisableLayerRay;
                int layerMask = 1 << layer;
                layerMask = ~layerMask;               
                
                if (Physics.Raycast(ray, out hit, cam.farClipPlane, layerMask))
                {
                    
                    rayCast = true;
                    switch (press.phase)
                    {
                        case TouchPhase.Began:
                            if (OnRayAtPressInvoker != null)
                                OnRayAtPressInvoker(ref hit, press, cam, ref ray);
                            break;
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            if (OnRayWhilePressInvoker != null)
                                OnRayWhilePressInvoker(ref hit, press, cam, ref ray);
                            break;
                        default:
                        case TouchPhase.Ended:
                            if (OnRayAtLeaveInvoker != null)
                                OnRayAtLeaveInvoker(ref hit, press,  cam, ref ray);
                            break;
                    }
                }             
                if (rayCast && !PropagateInputs)
                    return;
            }
        }




        public override string ToString()
        {
            return "InputServer";
        }
    }
}