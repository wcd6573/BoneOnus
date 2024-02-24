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



        // -------------------------- PROPERTIES ------------------------------


        // -------------------------- CONSTRUCTOR -----------------------------
        public MenuManager(SpriteBatch sb)
        {
            this.sb = sb;
        }

        // ---------------------------- METHODS -------------------------------
        private void Update(GameTime time, MouseState mState, MouseState prevMState)
        {

        }

        private void Draw(GameTime time)
        {

        }
    }
}
