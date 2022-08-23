using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakTheTower
{
    public class Camera3D
    {
        public Vector3 Target { get; set; }
        public Vector3 Position { get; set; }

        public Matrix Projection { get; private set; }
        public Matrix View { get; private set; }
        public Matrix World { get; private set; }

        public bool Orbit { get; set; }

        public Camera3D(float aspectRatio)
        {
            Target = Vector3.Zero;
            Position = new Vector3(0f, 0f, 100f);

            float fov = MathHelper.PiOver4;
            Projection = Matrix.CreatePerspectiveFieldOfView(fov, aspectRatio, 1.0f, 1000.0f);
            View = Matrix.CreateLookAt(Position, Target, Vector3.Up);
            World = Matrix.CreateWorld(Target, Vector3.Forward, Vector3.Up);

            Orbit = false;
        }

        public Camera3D(float aspectRatio, float fieldOfViewDegrees, float nearPlane, float farPlane)
        {
            Target = Vector3.Zero;
            Position = new Vector3(0f, 0f, -100f);

            float fov = MathHelper.ToRadians(fieldOfViewDegrees);
            Projection = Matrix.CreatePerspectiveFieldOfView(fov, aspectRatio, nearPlane, farPlane);
            View = Matrix.CreateLookAt(Position, Target, Vector3.Up);
            World = Matrix.CreateWorld(Position, Vector3.Forward, Vector3.Up);

            Orbit = false;
        }

        public void Update()
        {
            if (Orbit)
            {
                Matrix rotation = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
                Position = Vector3.Transform(Position, rotation);
            }

            View = Matrix.CreateLookAt(Position, Target, Vector3.Up);
        }
    }
}
