﻿using System.Collections.Generic;
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
        private IdleManager idle;

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
            
            List<Texture2D> skeletonTextures = new List<Texture2D>();

            skeletonTextures.Add(Content.Load<Texture2D>("skeleton1"));
            skeletonTextures.Add(Content.Load<Texture2D>("skeleton2"));
            skeletonTextures.Add(Content.Load<Texture2D>("skeleton3"));

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
            idle = new IdleManager(
                _spriteBatch,
                width,
                height,
                skeletonTextures,
                Content.Load<Texture2D>("bonesmith"),
                Content.Load<Texture2D>("floor"));
            
            idle.SkeletonClicked += StartForge;

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
                    idle.Update(gameTime, GraphicsDevice.Viewport.Height, GraphicsDevice.Viewport.Width, mState, prevMState);
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
                    idle.Draw(gameTime, GraphicsDevice.Viewport.Height);
                    break;
                case GameState.Forge:
                    forge.Draw();
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
            gameState = GameState.Idle;
        }
        
        private void StartForge()
        {
            gameState = GameState.Forge;
        }
    }
}