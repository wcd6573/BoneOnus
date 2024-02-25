using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

    public enum BoneType
    {
        Femur,
        Skull,
        Finger,
        Rib,
        Pelvis,
        Spine
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Width and height of the window
        private int width;
        private int height;

        private MouseState mState;
        private MouseState prevMState;
        private GameState gameState;

        private MenuManager menu;
        private ForgeManager forge;

        private SpriteFont arial;

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

            arial = Content.Load<SpriteFont>("arial20");

            menu = new MenuManager(
                _spriteBatch,
                Content.Load<Texture2D>("title"),
                Content.Load<Texture2D>("start"),
                StartGame,
                Content.Load<Texture2D>("controls"),   
                Content.Load<Texture2D>("back"),
                Content.Load<Texture2D>("quit"),
                Exit,
                width,
                height);

            forge = new ForgeManager(
                _spriteBatch,
                Content.Load<Texture2D>("forge_frame"),
                new List<Texture2D>
                {
                    Content.Load<Texture2D>("forge_femur"),
                    Content.Load<Texture2D>("forge_skull"),
                    Content.Load<Texture2D>("forge_rib"),
                    Content.Load<Texture2D>("forge_pelvis"),
                    Content.Load<Texture2D>("forge_finger"),
                    Content.Load<Texture2D>("forge_spine")
                },
                new List<Texture2D>
                {
                    Content.Load<Texture2D>("w_sword"),
                    Content.Load<Texture2D>("w_hammer"),
                    Content.Load<Texture2D>("w_scythe"),
                    Content.Load<Texture2D>("w_dagger")
                },
                Content.Load<Texture2D>("forge_cursor"),
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

            switch (gameState)
            {
                case GameState.Title:
                    menu.Update(gameTime, mState, prevMState);
                    break;
                case GameState.Idle:
                    break;
                case GameState.Forge:
                    forge.Update(gameTime, mState, prevMState);
                    break;
            }

            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Title:
                    menu.Draw(gameTime);
                    break;
                case GameState.Idle:
                    break;
                case GameState.Forge:
                    forge.Draw(mState);
                    break;
            }

            

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Method passed to the title screen start button.
        /// </summary>
        private void StartGame()
        {
            gameState = GameState.Forge;
        }
    }
}