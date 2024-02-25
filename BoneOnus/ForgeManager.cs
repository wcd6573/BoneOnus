using BoneOnus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
 * William Duprey
 * 2/24/24
 * Bone Onus ForgeManager Class
 */

namespace BoneOnus
{
    public enum ForgeState
    {
        Select,
        Whack,
        Done
    }

    internal class ForgeManager
    {
        // ---------------------------- FIELDS --------------------------------
        /// <summary>
        /// Control state of forge "minigame." True if the player is selecting
        /// bones, false if they are hammering the weapon together.
        /// </summary>
        private ForgeState forgeState;

        /// <summary>
        /// Bone inventory that the player can select from when forging.
        /// </summary>
        private Dictionary<BoneType, int> inventory;

        /// <summary>
        /// List of bones currently being crafted.
        /// </summary>
        private List<BoneType> currentBones;
        private List<Rectangle> currentBonePos;

        private Weapon weapon;

        private SpriteFont font;
        private Texture2D frameImg;
        
        private List<Rectangle> framePos;
        private List<Texture2D> boneImgs;

        private SpriteBatch sb;

        // -------------------------- PROPERTIES ------------------------------
        /// <summary>
        /// Readonly property for the state of the forge.
        /// </summary>
        public ForgeState ForgeState
        {
            get { return forgeState; }
        }

        // -------------------------- CONSTRUCTOR -----------------------------
        public ForgeManager(SpriteBatch sb, Texture2D frameImg, List<Texture2D> boneImgs,
            int width, int height)
        {
            this.sb = sb;

            // Set up inventory frames
            this.frameImg = frameImg;
            framePos = new List<Rectangle>(6);
            for(int i = 0; i < 6; i++)
            {
                framePos.Add(new Rectangle(((width / 2) - 300) + (i * 100),
                    0, 100, 100));
            }

            // Set up bone textures
            currentBones = new List<BoneType>(6);
            this.boneImgs = boneImgs;

            // Set up current bones display
            currentBones = new List<BoneType>(3);
            currentBonePos = new List<Rectangle>
            {
                new Rectangle(100, 180, 200, 200),
                new Rectangle(300, 180, 200, 200),
                new Rectangle(500, 180, 200, 200)
            };
        }

        // ---------------------------- METHODS -------------------------------
        public void Update(GameTime time, MouseState mState, MouseState prevMState)
        {
            switch (forgeState)
            {
                case ForgeState.Select:
                    for(int i = 0; i < framePos.Count; i++)
                    {
                        // If hovering over and pressed
                        if (framePos[i].Contains(mState.Position)
                            && mState.LeftButton == ButtonState.Pressed
                            && prevMState.LeftButton == ButtonState.Released)
                        {
                            // Add that bone to list of bones
                            currentBones.Add((BoneType)i);
                        }
                    }

                    // If three bones selected, transition to whack state
                    if(currentBones.Count == 3)
                    {
                        forgeState = ForgeState.Whack;
                    }
                    break;
                case ForgeState.Whack:
                    break;
                case ForgeState.Done:
                    break;
            }
        }

        public void Draw()
        {
            for(int i = 0; i < currentBones.Count; i++)
            {
                // Get texture by parsing bone enum to integer,
                // then indexing the boneImgs list
                sb.Draw(boneImgs[(int)currentBones[i]],
                    currentBonePos[i],
                    Color.White);
            }

            switch (forgeState)
            {
                case ForgeState.Select:
                    // Draw each bone and the frame behind it
                    for(int i = 0; i < framePos.Count; i++)
                    {
                        sb.Draw(frameImg, framePos[i], Color.White);
                        sb.Draw(boneImgs[i], framePos[i], Color.White);
                    }
                    break;
                case ForgeState.Whack:
                    break;
                case ForgeState.Done:
                    break;
            }
        }
        
        /// <summary>
        /// Increases the specified bone type by the given count.
        /// </summary>
        /// <param name="bone">Type of bone added.</param>
        /// <param name="count">Number of bones added.</param>
        public void AddBone(BoneType bone, int count)
        {
            inventory[bone] += count;
        }
    }
}
