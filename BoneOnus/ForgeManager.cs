using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * William Duprey
 * 2/24/24
 * Bone Onus ForgeManager Class
 */

namespace BoneOnus
{
    internal class ForgeManager
    {
        // ---------------------------- FIELDS --------------------------------
        /// <summary>
        /// Control state of forge "minigame." False if the player is selecting
        /// bones, true if they are hammering the weapon together.
        /// </summary>
        private bool forgeState;

        /// <summary>
        /// Bone inventory that the player can select from when forging.
        /// </summary>
        private Dictionary<BoneType, int> inventory;

        private SpriteFont font;
        private Texture2D frameImg;
        private List<Button> bones;

        // -------------------------- PROPERTIES ------------------------------


        // -------------------------- CONSTRUCTOR -----------------------------
        

        // ---------------------------- METHODS -------------------------------
        
    }
}
