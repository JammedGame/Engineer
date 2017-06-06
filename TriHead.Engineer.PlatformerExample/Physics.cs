using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletSharp;

namespace Engineer.PlatformerExample
{
    public class Physics
    {
        private BroadphaseInterface _Broadphase;
        private DefaultCollisionConfiguration _CollisionConfiguration;
        private CollisionDispatcher _Dispatcher;
        private SequentialImpulseConstraintSolver _Solver;
        DiscreteDynamicsWorld _DynamicsWorld;
        public Physics()
        {
            Init();
        }
        public void Init()
        {
            this._Broadphase = new DbvtBroadphase();
            this._CollisionConfiguration = new DefaultCollisionConfiguration();
            this._Dispatcher = new CollisionDispatcher(_CollisionConfiguration);
            this._Solver = new SequentialImpulseConstraintSolver();
            this._DynamicsWorld = new DiscreteDynamicsWorld(_Dispatcher, _Broadphase, _Solver, _CollisionConfiguration);
            this._DynamicsWorld.Gravity = new OpenTK.Vector3(0, -10, 0);
        }
    }
}
