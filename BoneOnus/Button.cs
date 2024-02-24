using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

/*
 * William Duprey
 * 2/24/24
 * Bone Onus Menu Button Class
 */

namespace BoneOnus
{
    /// <summary>
    /// Menu button class used for the title screen buttons.
    /// </summary>
    internal class Button
    {
        // ---------------------------- FIELDS --------------------------------
        /// <summary>
        /// Rectangle used to position and draw the button.
        /// </summary>
        private Rectangle rect;

        /// <summary>
        /// Whether mouse is currently over the button.
        /// </summary>
        private bool hover;

        /// <summary>
        /// The method called when the button is clicked.
        /// </summary>
        private MenuManager.ButtonClick OnClick;

        /// <summary>
        /// Texture2D image for the button.
        /// </summary>
        private Texture2D img;

        /// <summary>
        /// SpriteBatch passed in from Game1, used to draw.
        /// </summary>
        private SpriteBatch sb;

        // -------------------------- PROPERTIES ------------------------------


        // -------------------------- CONSTRUCTOR -----------------------------
        public Button(Rectangle rect, MenuManager.ButtonClick OnClick,
            Texture2D img, SpriteBatch sb)
        {
            this.rect = rect;
            this.OnClick = OnClick;
            this.img = img;
            this.sb = sb;
            hover = false;
        }

        // ---------------------------- METHODS -------------------------------
        /// <summary>
        /// Update is called every frame, checks whether the player is hovering
        /// over the button, and whether they clicked it.
        /// </summary>
        /// <param name="mState">Mouse state on current frame.</param>
        /// <param name="prevMState">Mouse state from prior frame.</param>
        public void Update(MouseState mState, MouseState prevMState)
        {
            // Check if mouse is hovering over button
            if(rect.Contains(mState.Position))   
            {
                hover = true;

                // If player clicked
                if(mState.LeftButton == ButtonState.Pressed         
                    && prevMState.LeftButton == ButtonState.Released)
                {
                    OnClick();  // Call button's click method
                }
            }
            // Not hovering
            else
            {
                hover = false;
            }
        }

        /// <summary>
        /// Draws the button to the screen using its Rectangle and Texture2D.
        /// If the player was hovering over the button, it is tinted darker.
        /// </summary>
        public void Draw()
        {
            // If hovering over the button
            if (hover)
            {
                // Tint it slightly darker
                sb.Draw(img, rect, Color.Gray);
            }
            else
            {   
                // Otherwise draw as usual
                sb.Draw(img, rect, Color.White);
            }
        }
    }
}
