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

            __islandTexture = Content.Load<Texture2D>("Test_Island");
            __texturedQuad.InitializeEffect(GraphicsDevice, __camera, __islandTexture);
        }

        KeyboardState prevKeyboardState;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.O) && !prevKeyboardState.IsKeyDown(Keys.O))
                __camera.Orbit = !__camera.Orbit;

            __camera.Update();

            prevKeyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            __texturedQuad.Draw(GraphicsDevice, __camera);

            base.Draw(gameTime);
        }
    }
}
