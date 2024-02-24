using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
 * William Duprey, Caity Kurutz, Sadie Newton
 * 2/24/24
 * Bone Onus Game1
 */

namespace BoneOnus
{
    public enum GameState
    {
        Title,      // Title screen
        Idle,       // Idle chilling
        Forge       // Crafting "minigame"
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private Texture2D titleImg;
        private Vector2 titlePos;

        // Width and height of the window
        private int width;
        private int height;

        private MouseState mState;
        private MouseState prevMState;
        private GameState gameState;

        private MenuManager menu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            width = _graphics.PreferredBackBufferWidth;
            height = _graphics.PreferredBackBufferHeight;
            gameState = GameState.Title;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
            // Setup title image and center its position
            titleImg = Content.Load<Texture2D>("title");
            titlePos = new Vector2((width / 2) - (titleImg.Width / 2), height / 10);


            menu = new MenuManager(
                _spriteBatch,
                Content.Load<Texture2D>("start"),
                StartGame,
                Content.Load<Texture2D>("controls"),   
                Content.Load<Texture2D>("back"),
                Content.Load<Texture2D>("quit"),
                Exit,
                width,
                height);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            prevMState = mState;
            mState = Mouse.GetState();
            
            menu.Update(gameTime, mState, prevMState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(titleImg, titlePos, Color.White);
            menu.Draw(gameTime);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Method passed to the title screen start button.
        /// </summary>
        private void StartGame()
        {
            gameState = GameState.Idle;
        }
    }
}