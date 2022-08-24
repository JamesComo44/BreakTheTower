using BreakTheTower.Primitives3D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BreakTheTower
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _debugFont;
        private KeyboardInputController _inputController;

        private Camera3D __camera;
        private Texture2D __islandTexture;
        private Quad __texturedQuad;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _inputController = new KeyboardInputController();

            __camera = new Camera3D(GraphicsDevice.Viewport.AspectRatio)
            {
                Position = new Vector3(0, -2f, 2f)
            };

            __texturedQuad = new Quad(Vector3.Zero, Vector3.Backward, Vector3.Up, 1, 1);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _debugFont = Content.Load<SpriteFont>("Fonts/debug");

            __islandTexture = Content.Load<Texture2D>("Test_Island");
            __texturedQuad.InitializeEffect(GraphicsDevice, __camera, __islandTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            try
            {
                _inputController.Update();
            }
            catch (AbortGameException exception)
            {
                System.Console.WriteLine(exception.Message);
                Exit();
            }

            if (_inputController.WasKeyPressed(Keys.O))
                __camera.Orbit = !__camera.Orbit;

            __camera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            __texturedQuad.Draw(GraphicsDevice, __camera);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_debugFont, __camera.Position.ToString(), new Vector2(0, 0), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
