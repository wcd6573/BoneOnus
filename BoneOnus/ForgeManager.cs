using BoneOnus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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
        /// Controls the state of forge the "minigame."
        /// </summary>
        private ForgeState forgeState;

        private List<BoneType> currentBones;
        private List<Rectangle> currentBonePos;

        private Texture2D anvil;
        private Texture2D frameImg;
        private List<Rectangle> framePos;
        private List<Texture2D> boneImgs;
        private List<Texture2D> weaponImgs;
        private List<SoundEffect> anvilSounds;
        private List<SoundEffect> boneSounds;
        private Random random;

        /// <summary>
        /// The weapon being produced in the forge.
        /// </summary>
        private Weapon weapon;

        /// <summary>
        /// Whacking hammer displayed during the Whack state.
        /// </summary>
        private Texture2D cursor;
        
        /// <summary>
        /// Counts how many times the player has whacked the bones together.
        /// Also used as a timer to end the minigame after the weapon is made.
        /// </summary>
        int whacks = 0;

        /// <summary>
        /// SpriteBatch used to draw.
        /// </summary>
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
        
        /// <summary>
        /// Read-only property for the List of Texture2Ds for the 
        /// weapons, in the order: Sword, Hammer, Scythe, Dagger.
        /// </summary>
        public List<Texture2D> WeaponTextures
        {
            get { return weaponImgs; }
        }

        // -------------------------- CONSTRUCTOR -----------------------------
        public ForgeManager(SpriteBatch sb, Texture2D anvil, Texture2D frameImg, 
            List<Texture2D> boneImgs, List<Texture2D> weaponImgs, List<SoundEffect> anvilSounds, List<SoundEffect> boneSounds, Texture2D cursor,
            int width, int height)
        {
            this.sb = sb;
            this.cursor = cursor;
            this.weaponImgs = weaponImgs;
            this.anvilSounds = anvilSounds;
            this.boneSounds = boneSounds;
            this.anvil = anvil;
            
            random = new Random();

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
                            
                            SoundEffectInstance boneSoundInstance = boneSounds[random.Next(0, 6)].CreateInstance();
                            boneSoundInstance.Play();
                            
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
                        // Play anvil hit sound
                        SoundEffectInstance anvilSoundInstance = anvilSounds[random.Next(0, 6)].CreateInstance();
                        anvilSoundInstance.Play();
                        
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
                            ForgeWeapon();

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
            // Draw background
            sb.Draw(anvil, Vector2.Zero, Color.White);

            // Draw current bones
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
        
        /// <summary>
        /// Sets the ForgeManager's weapon field to whatever weapon
        /// will be made depending on the player's selection.
        /// If no specific recipe is followed, a dagger is made.
        /// </summary>
        private void ForgeWeapon()
        {
            // This is repetitive and bad, but it is what it is
            if(currentBones.Contains(BoneType.Femur)
                && currentBones.Contains(BoneType.Pelvis)
                && currentBones.Contains(BoneType.Spine))
            {
                weapon = new Sword(currentBones[0], currentBones[1], currentBones[2]);
            }
            else if (currentBones.Contains(BoneType.Skull)
                && currentBones.Contains(BoneType.Femur)
                && currentBones.Contains(BoneType.Finger))
            {
                weapon = new Hammer(currentBones[0], currentBones[1], currentBones[2]);
            }
            else if (currentBones.Contains(BoneType.Rib)
                && currentBones.Contains(BoneType.Spine)
                && currentBones.Contains(BoneType.Femur))
            {
                weapon = new Scythe(currentBones[0], currentBones[1], currentBones[2]);
            }
            else
            {
                weapon = new Dagger(currentBones[0], currentBones[1], currentBones[2]);
            }
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
