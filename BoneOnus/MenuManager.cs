using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * William Duprey
 * 2/24/24
 * Bone Onus Title Screen Class
 */

namespace BoneOnus
{
    /// <summary>
    /// Class for handling the menu interactions.
    /// </summary>
    internal class MenuManager
    {
        /// <summary>
        /// Declaration for button click delegate.
        /// </summary>
        public delegate void ButtonClick();

        // ---------------------------- FIELDS --------------------------------
        /// <summary>
        /// Whether controls screen is displayed or not.
        /// </summary>
        private bool controls;

        /// <summary>
        /// SpriteBatch used to draw stuff.
        /// </summary>
        private SpriteBatch sb;

        private Texture2D title;
        private Rectangle titlePos;

        /// <summary>
        /// Contains the three buttons on the title screen:
        /// start, controls, and quit.
        /// </summary>
        private List<Button> titleButtons;

        /// <summary>
        /// Back button for the controls screen.
        /// </summary>
        private Button backButton;

        private Texture2D background;
        private Vector2 backgroundPos1;
        private Vector2 backgroundPos2;

        private const float scrollRate = 1;

        // -------------------------- PROPERTIES ------------------------------


        // -------------------------- CONSTRUCTOR -----------------------------
        /// <summary>
        /// Gigantic, disgusting constructor. But hey, it works.
        /// </summary>
        /// <param name="sb">SpriteBatch used to draw buttons / text.</param>
        /// <param name="title">Title text texture.</param>
        /// <param name="start">Start button texture.</param>
        /// <param name="startGame">Start button method delegate.</param>
        /// <param name="controls">Controls button texture.</param>
        /// <param name="back">Back button texture.</param>
        /// <param name="quit">Quit button texture.</param>
        /// <param name="quitGame">Quit button method delegate.</param>
        /// <param name="font">Spritefont used to draw </param>
        /// <param name="background">Scrolling background image.</param>
        /// <param name="width">Width of window.</param>
        /// <param name="height">Height of window.</param>
        public MenuManager(SpriteBatch sb, Texture2D title, Texture2D start,
            ButtonClick startGame, Texture2D controls, Texture2D back, 
            Texture2D quit, ButtonClick quitGame, /*SpriteFont font,*/ 
            Texture2D background, int width, int height)
        {
            this.sb = sb;
            this.title = title;
            this.background = background;
            backgroundPos1 = new Vector2(0, 0);
            backgroundPos2 = new Vector2(800, 0);

            titlePos = new Rectangle((width / 2) - (title.Width / 2), height / 10,
                title.Width, title.Height);

            titleButtons = new List<Button>();

            // Hard-coding heights is dumb and bad, but I don't have time to do
            // good math based on the dimensions of the window

            // Set up back button
            backButton = new Button(
                new Rectangle((width / 2) - 125, 0, 250, 80),
                ToggleControls,
                back,
                sb);

            // Set up start button
            titleButtons.Add(new Button(
                new Rectangle((width / 2) - 125, 180, 250, 80),
                startGame,
                start,
                sb));

            // Set up controls button
            titleButtons.Add(new Button(
                new Rectangle((width / 2) - 125, 280, 250, 80),
                ToggleControls,
                controls,
                sb));

            // Set up quit button
            titleButtons.Add(new Button(
                new Rectangle((width / 2) - 125, 380, 250, 80),
                quitGame,
                quit,
                sb));
        }

        // ---------------------------- METHODS -------------------------------
        public void Update(GameTime time, MouseState mState, MouseState prevMState)
        {
            if (controls)
            {
                backButton.Update(mState, prevMState);
            }
            else
            {
                backgroundPos1.X -= scrollRate;
                backgroundPos2.X -= scrollRate;

                if(backgroundPos1.X < -800)
                {
                    backgroundPos1.X = 0;
                    backgroundPos2.X = 800;
                }

                foreach(Button butt in titleButtons)
                {
                    butt.Update(mState, prevMState);
                }
            }
        }

        public void Draw(GameTime time)
        {
            if (controls)
            {
                backButton.Draw();
            }
            else
            {
                sb.Draw(background, backgroundPos1, Color.White);
                sb.Draw(background, backgroundPos2, Color.White);

                foreach (Button butt in titleButtons)
                {
                    butt.Draw();
                }
                sb.Draw(title, titlePos, Color.White);
            }
        }

        /// <summary>
        /// Method passed for the Controls and Back button delegates.
        /// </summary>
        private void ToggleControls()
        {
            controls = !controls;
        }
    }
}
