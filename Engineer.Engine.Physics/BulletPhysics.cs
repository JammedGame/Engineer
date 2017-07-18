using System;
using System.Collections.Generic;
using System.Text;
using BulletSharp;
using BulletSharp.Math;
using Engineer.Engine;
using Engineer.Mathematics;

namespace Engineer.Engine.Physics
{
    public class BulletPhysics : Physics
    {
        private int DownScale = 100;
        private Scene _CurrentScene;
        private List<SceneObject> _Objects;
        private List<CollisionShape> _CollisionShapes;
        private List<RigidBody> _RigidBodies;
        private BroadphaseInterface _Broadphase;
        private DefaultCollisionConfiguration _CollisionConfiguration;
        private CollisionDispatcher _Dispatcher;
        private SequentialImpulseConstraintSolver _Solver;
        private DiscreteDynamicsWorld _DynamicsWorld;
        public BulletPhysics()
        {
            Init();
        }
        public void Init()
        {
            this._Broadphase = new DbvtBroadphase();
            this._CollisionConfiguration = new DefaultCollisionConfiguration();
            this._Dispatcher = new CollisionDispatcher(_CollisionConfiguration);
            this._Solver = new SequentialImpulseConstraintSolver();
        }
        public void UpdateScene(Scene CurrentScene)
        {
            this._DynamicsWorld = new DiscreteDynamicsWorld(_Dispatcher, _Broadphase, _Solver, _CollisionConfiguration);
            this._DynamicsWorld.Gravity = new Vector3(0, -10, 0);
            this._Objects = new List<SceneObject>();
            this._CollisionShapes = new List<CollisionShape>();
            this._RigidBodies = new List<RigidBody>();
            this._CurrentScene = CurrentScene;
            for(int i = 0; i < _CurrentScene.Objects.Count; i++)
            {
                if(_CurrentScene.Objects[i].Data.ContainsKey("Collision") && (bool)_CurrentScene.Objects[i].Data["Collision"])
                {
                    AddShape(_CurrentScene.Objects[i]);
                }
            }
        }
        private void AddShape(SceneObject Current)
        {
            if (_CurrentScene.Type == SceneType.Scene2D)
            {
                _Objects.Add(Current);
                DrawnSceneObject DSO = Current as DrawnSceneObject;
                float MaxDimension = (DSO.Visual.Scale.X > DSO.Visual.Scale.Y) ? DSO.Visual.Scale.X : DSO.Visual.Scale.Y;
                MaxDimension /= DownScale * 1.0f;
                CollisionShape Shape;
                if (Current.Data.ContainsKey("CollisionShape") && (string)Current.Data["CollisionShape"] == "Sphere")
                {
                    Shape = new SphereShape(DSO.Visual.Scale.X / (DownScale * 2.0f));
                }
                else
                {
                    Shape = new Box2DShape(DSO.Visual.Scale.X / (DownScale * 2.0f), DSO.Visual.Scale.Y / (DownScale * 2.0f), 0);
                }
                _CollisionShapes.Add(Shape);
                Vertex InvertedY = new Vertex(DSO.Visual.Translation.X/ (DownScale * 1.0f), -DSO.Visual.Translation.Y/ (DownScale * 1.0f), DSO.Visual.Translation.Z/ (DownScale * 1.0f));
                DefaultMotionState FallMotionState = new DefaultMotionState(Matrix.Translation(Util.BulletVectorFromEngineerVertex(InvertedY)));
                Vector3 Inertia = Shape.CalculateLocalInertia((int)Current.Data["Weight"]);
                RigidBodyConstructionInfo Info = new RigidBodyConstructionInfo((int)Current.Data["Weight"], FallMotionState, Shape, Inertia);
                RigidBody Body = new RigidBody(Info);
                Body.LinearFactor = new Vector3(1, 1, 0);
                Body.AngularFactor = new Vector3(0, 0, 1);
                _RigidBodies.Add(Body);
                _DynamicsWorld.AddRigidBody(Body);
                Current.Data["PhysicsIndex"] = _Objects.Count - 1;
            }
        }
        public void RunSimulation()
        {
            if (_CurrentScene.Type == SceneType.Scene2D)
            {
                _DynamicsWorld.StepSimulation(1 / 30.0f, 10);
                for (int i = 0; i < _RigidBodies.Count; i++)
                {
                    DrawnSceneObject DSO = _Objects[i] as DrawnSceneObject;
                    Matrix Transform;
                    
                    _RigidBodies[i].GetWorldTransform(out Transform);
                    float X = Transform.Origin.X * DownScale;
                    float Y = -Transform.Origin.Y * DownScale;
                    float Z = Transform.Origin.Z * DownScale;
                    Vertex NewPosition = new Mathematics.Vertex(X, Y, 0);
                    DSO.Visual.Translation = NewPosition;
                }
            }
        }
        public void SetVelocities(int Index, Vertex Velocities)
        {
            Vector3 NewVelocity = Util.BulletVectorFromEngineerVertex(Velocities);
            if (!_RigidBodies[Index].IsActive) _RigidBodies[Index].Activate();
            _RigidBodies[Index].LinearVelocity = NewVelocity;
        }
        public Vertex GetVelocities(int Index)
        {
            return Util.EngineerVertexFromBulletVector(_RigidBodies[Index].LinearVelocity);
        }
    }
}
