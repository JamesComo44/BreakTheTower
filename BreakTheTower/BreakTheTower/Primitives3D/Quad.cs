using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakTheTower.Primitives3D
{
    public class Quad
    {
        public readonly VertexPositionNormalTexture[] Vertices;
        public readonly short[] Indexes;

        public BasicEffect Effect { get; set; }

        private Vector3 _origin;
        private Vector3 _upperLeft;
        private Vector3 _lowerLeft;
        private Vector3 _upperRight;
        private Vector3 _lowerRight;
        private Vector3 _normal;
        private Vector3 _up;
        private Vector3 _left;

        public Quad(Vector3 origin, Vector3 normal, Vector3 up, float width, float height)
        {
            Vertices = new VertexPositionNormalTexture[4];
            Indexes = new short[6];

            _origin = origin;
            _normal = normal;
            _up = up;

            CalculateCorners(width, height);
            FillVertices();
        }

        private void CalculateCorners(float width, float height)
        {
            _left = Vector3.Cross(_normal, _up);

            Vector3 upperCenter = (_up * height / 2) + _origin;
            _upperLeft = upperCenter + (_left * width / 2);
            _upperRight = upperCenter - (_left * width / 2);
            _lowerLeft = _upperLeft - (_up * height);
            _lowerRight = _upperRight - (_up * height);
        }

        private void FillVertices()
        {
            Vector2 textureUpperLeft = new Vector2(0.0f, 0.0f);
            Vector2 textureLowerLeft = new Vector2(0.0f, 1.0f);
            Vector2 textureUpperRight = new Vector2(1.0f, 0.0f);
            Vector2 textureLowerRight = new Vector2(1.0f, 1.0f);

            for (int i = 0; i < Vertices.Length; i++)
                Vertices[i].Normal = _normal;

            Vertices[0].Position = _lowerLeft;
            Vertices[1].Position = _upperLeft;
            Vertices[2].Position = _lowerRight;
            Vertices[3].Position = _upperRight;

            Vertices[0].TextureCoordinate = textureLowerLeft;
            Vertices[1].TextureCoordinate = textureUpperLeft;
            Vertices[2].TextureCoordinate = textureLowerRight;
            Vertices[3].TextureCoordinate = textureUpperRight;

            // Indexed using Clockwise Winding.
            Indexes[0] = 0;
            Indexes[1] = 1;
            Indexes[2] = 2;
            Indexes[3] = 2;
            Indexes[4] = 1;
            Indexes[5] = 3;
        }

        public void InitializeEffect(GraphicsDevice graphics, Camera3D camera, Texture2D texture)
        {
            Effect = new BasicEffect(graphics)
            {
                View = camera.View,
                World = camera.World,
                Projection = camera.Projection
            };

            Effect.Texture = texture;
            Effect.TextureEnabled = true;
        }

        public void Draw(GraphicsDevice graphics, Camera3D camera)
        {
            if (Effect is null)
                throw new InvalidOperationException("Attempted to draw a Quad without calling InitializeEffect first.");

            Effect.View = camera.View;

            BlendState oldBlendState = graphics.BlendState;
            graphics.BlendState = BlendState.AlphaBlend;

            foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList,
                    Vertices, 0, 4,
                    Indexes, 0, 2
                );
            }

            graphics.BlendState = oldBlendState;
        }
    }
}
