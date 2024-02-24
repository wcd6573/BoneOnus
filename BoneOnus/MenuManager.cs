using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Contains the three buttons on the title screen:
        /// start, controls, and quit.
        /// </summary>
        private List<Button> titleButtons;

        /// <summary>
        /// Back button for the controls screen.
        /// </summary>
        private Button back;

        // -------------------------- PROPERTIES ------------------------------


        // -------------------------- CONSTRUCTOR -----------------------------
        public MenuManager(SpriteBatch sb, Texture2D start, Texture2D controls,
            Texture2D back, Texture2D quit)
        {
            this.sb = sb;
            titleButtons = new List<Button>();

        }

        // ---------------------------- METHODS -------------------------------
        public void Update(GameTime time, MouseState mState, MouseState prevMState)
        {
            if (controls)
            {
                back.Update(mState, prevMState);
            }
            else
            {
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
                back.Draw();
            }
            else
            {
                foreach (Button butt in titleButtons)
                {
                    butt.Draw();
                }
            }
        }
    }
}
