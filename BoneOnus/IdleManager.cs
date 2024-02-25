using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using BoneOnus.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
 * Caity Kurutz
 * 2/24/24
 * Bone Onus Idle Screen Class
 */

namespace BoneOnus
{
    /// <summary>
    /// Class for handling the menu interactions.
    /// </summary>
    internal class IdleManager
    {
        // ---------------------------- FIELDS --------------------------------
        /// <summary>
        /// SpriteBatch used to draw stuff.
        /// </summary>
        private SpriteBatch sb;
        
        private Texture2D bonesmithTexture;
        private Texture2D floor;
        private Texture2D hearth;

        private int skeletonWidth;
        private int skeletonHeight;

        private int width;
        private int height;

        public struct Skeleton
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public bool IsJumping;
            public Texture2D Texture;
            public Texture2D WeaponTexture;
        }

        private Skeleton[] skeletons;
        private float walkSpeed = 2f;
        private float jumpForce = -10f;
        private Random random;
        
        // when skeleton clicked, then do this. establish handler to do things after click
        public delegate void SkeletonClickedEventHandler();
        public event SkeletonClickedEventHandler SkeletonClicked;
        private int clickedSkeletonIndex;

        // -------------------------- PROPERTIES ------------------------------
        public Texture2D SetWeapon
        {
            set
            {
                if(value != null)
                {
                    skeletons[clickedSkeletonIndex].WeaponTexture = value;
                }
            }
        }


        // -------------------------- CONSTRUCTOR -----------------------------
        /// <summary>
        /// Creation of the Idle screen
        /// </summary>
        /// <param name="sb">SpriteBatch used to draw</param>
        /// <param name="width">Width of window.</param>
        /// <param name="height">Height of window.</param>
        /// 
        public IdleManager(SpriteBatch sb, int width, int height, List<Texture2D> skeletonTextures,
            Texture2D bonesmithTexture, Texture2D floor, Texture2D hearth)
        {
            this.sb = sb;
            random = new Random();
            
            // texture initialization     
            this.bonesmithTexture = bonesmithTexture;
            this.floor = floor;
            this.hearth = hearth;

            // skeleton party initialization
            skeletonHeight = skeletonTextures[0].Height;
            skeletonWidth = skeletonTextures[0].Width;

            // party number adjustable based on what we want
            skeletons = new Skeleton[3];

            // loops through and assigns values to each skeleton in the array
            for (int i = 0; i < skeletons.Length; i++)
            {
                int randomIndex = random.Next(skeletonTextures.Count);
                skeletons[i].Texture = skeletonTextures[randomIndex];
                skeletons[i].Position = new Vector2(random.Next(width - skeletonWidth), 
                    height - floor.Height - skeletonHeight * 0.5f);
                skeletons[i].Velocity = new Vector2((float)(random.Next(1, 4)), 0);
                skeletons[i].IsJumping = false;
            }
        }

        // ---------------------------- METHODS -------------------------------
        public void Update(GameTime time, int screenHeight, int screenWidth, MouseState currentMouseState, 
            MouseState previousMouseState)
        {

            if (currentMouseState.LeftButton == ButtonState.Pressed &&
                previousMouseState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < skeletons.Length; i++)
                {
                    Skeleton skeleton = skeletons[i];
                    Rectangle skeletonBounds = new Rectangle((int)skeleton.Position.X, (int)skeleton.Position.Y,
                        skeletonWidth, skeletonHeight);
                    if (skeletonBounds.Contains(currentMouseState.Position))
                    {
                        clickedSkeletonIndex = i;
                        SkeletonClicked?.Invoke();
                        break;
                    }
                }
            }

            for (int i = 0; i < skeletons.Length; i++){
                
                Skeleton skeleton = skeletons[i];

                skeleton.Position.X += skeleton.Velocity.X;

                if (skeleton.Position.X <= 0 || skeleton.Position.X >= screenWidth - skeletonWidth / 2)
                {
                    skeleton.Velocity.X *= -1;
                }

                skeleton.Velocity.Y += 0.5f;
                skeleton.Position.Y += skeleton.Velocity.Y;

                if (skeleton.Position.Y + skeletonHeight / 2 >= screenHeight - floor.Height)
                {
                    skeleton.Position.Y = screenHeight - floor.Height - skeletonHeight / 2;
                    skeleton.Velocity.Y = 0;
                    skeleton.IsJumping = false;
                }

                if (!skeleton.IsJumping && random.Next(100) < 25)
                {
                    skeleton.IsJumping = true;
                    skeleton.Velocity.Y = jumpForce;
                }

                skeletons[i] = skeleton;
            }
        }

        public void Draw(int screenHeight)
        {
            sb.Draw(hearth, Vector2.Zero, Color.White);

            //int textureHeight = floor.Height;
            //sb.Draw(floor, new Vector2(0, screenHeight - textureHeight), null, Color.White,
            //    0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            
            sb.Draw(bonesmithTexture, new Vector2(100,screenHeight - floor.Height - skeletonHeight * 0.5f),
                null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 
                1f);
            
            // draws each skeleton in the array
            foreach (Skeleton skeleton in skeletons)
            {
                sb.Draw(skeleton.Texture, skeleton.Position, null, Color.White, 0f, 
                    Vector2.Zero, 0.5f, SpriteEffects.None, 1f);
                if(skeleton.WeaponTexture != null)
                {
                    sb.Draw(skeleton.WeaponTexture, skeleton.Position, null, Color.White,
                        0, default, 0.5f, SpriteEffects.None, 0);
                }
            }
        }
    }
}
