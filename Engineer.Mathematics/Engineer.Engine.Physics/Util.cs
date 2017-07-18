using BulletSharp.Math;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.Engine.Physics
{
    public class Util
    {
        public static Vector3 BulletVectorFromEngineerVertex(Vertex Position)
        {
            Vector3 V = new Vector3(Position.X, Position.Y, Position.Z);
            return V;
        }
        public static Vertex EngineerVertexFromBulletVector(Vector3 Position)
        {
            Vertex V = new Vertex(Position.X, Position.Y, Position.Z);
            return V;
        }
        public static Matrix BulletMatrixFromEngineerVertex(Vertex Position)
        {
            Matrix M = new Matrix(Position.X, 0, 0, 0, 0, Position.Y, 0, 0, 0, 0, Position.Z, 0, 0, 0, 0, 1);
            return M;
        }
    }
}
