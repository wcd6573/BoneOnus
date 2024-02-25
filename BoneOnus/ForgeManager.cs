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
        Forged,
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
        /// List of bones currently being crafted.
        /// </summary>
        private List<BoneType> currentBones;
        private List<Rectangle> currentBonePos;

        private Weapon weapon;

        private SpriteFont font;
        private Texture2D frameImg;
        
        private List<Rectangle> framePos;
        private List<Texture2D> boneImgs;
        private List<Texture2D> weaponImgs;

        private Texture2D cursor;
        int whacks = 0;

        private SpriteBatch sb;

        // -------------------------- PROPERTIES ------------------------------
        /// <summary>
        /// Readonly property for the state of the forge.
        /// </summary>
        public ForgeState ForgeState
        {
            get { return forgeState; }
        }

        /// <summary>
        /// Read-only property for the forge's weapon. Only
        /// returns a weapon if the forge is in the "Done" state.
        /// </summary>
        public Weapon ForgedWeapon
        {
            get
            {
                if(forgeState == ForgeState.Done && weapon != null)
                {
                    return weapon;
                }
                return null;
            }
        }

        // -------------------------- CONSTRUCTOR -----------------------------
        public ForgeManager(SpriteBatch sb, Texture2D frameImg, List<Texture2D> boneImgs,
            List<Texture2D> weaponImgs, Texture2D cursor, int width, int height)
        {
            this.sb = sb;
            this.cursor = cursor;
            this.weaponImgs = weaponImgs;

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
                    if(mState.LeftButton == ButtonState.Pressed
                        && prevMState.LeftButton == ButtonState.Released)
                    {
                        // Slide outer bones closer to center
                        Rectangle rect = currentBonePos[0];
                        rect.X += 50;
                        currentBonePos[0] = rect;

                        rect = currentBonePos[2];
                        rect.X -= 50;
                        currentBonePos[2] = rect;

                        whacks++;
                        
                        // If whacked enough, transition to Forged state
                        if(whacks >= 4)
                        {
                            forgeState = ForgeState.Forged;

                            weapon = ForgeWeapon();

                            // Clear bones list after weapon made
                            currentBones.Clear();
                        }
                    }
                    break;
                case ForgeState.Forged:
                    // Reusing whacks variable as a timer
                    if(whacks >= 180)
                    {
                        forgeState = ForgeState.Done;
                    }
                    else
                    {
                        whacks++;
                    }
                    break;
            }
        }

        public void Draw(MouseState mState)
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
                    sb.Draw(cursor, mState.Position.ToVector2(), Color.White);
                    break;
                case ForgeState.Forged:
                    // Definitely questioning whether inheritance was even a good
                    // idea, but it's too late to change things now
                    if(weapon is Sword) 
                    {
                        sb.Draw(weaponImgs[0], currentBonePos[1], Color.White);
                    }
                    else if (weapon is Hammer)
                    {
                        sb.Draw(weaponImgs[1], currentBonePos[1], Color.White);
                    }
                    else if (weapon is Scythe)
                    {
                        sb.Draw(weaponImgs[2], currentBonePos[1], Color.White);
                    }
                    else
                    {
                        sb.Draw(weaponImgs[3], currentBonePos[1], Color.White);
                    }
                    break;
            }
        }
        
        private Weapon ForgeWeapon()
        {
            return null;
        }

        /// <summary>
        /// Resets the forge for the next time a weapon needs to be made.
        /// </summary>
        public void Reset()
        {
            forgeState = ForgeState.Select;
            whacks = 0;
            currentBones.Clear();
            currentBonePos[0] = new Rectangle(100, 180, 200, 200);
            currentBonePos[2] = new Rectangle(500, 180, 200, 200);
        }
    }
}
