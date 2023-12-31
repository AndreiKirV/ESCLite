using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class EcsStartup : MonoBehaviour 
    {
        EcsWorld _world;        
        IEcsSystems _systems;

        void Start () 
        {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _systems
                // register your systems here, for example:
                .Add (new InitMovePlayer ())
                .Add(new InitEnemiesSystem ())
                .Add (new RunMovePlayer ())
                .Add(new TriggerEventSystem())
                
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif          
                .Inject()
                .Init ();
        }

        void Update () 
        {
            // process systems here.
            _systems?.Run ();
            
        }

        void OnDestroy () {
            if (_systems != null) 
            {//точко выхода
                _systems.Destroy ();
                _systems = null;
            }
            
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}