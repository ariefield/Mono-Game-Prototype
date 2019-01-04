using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame1.Models;
using MonoGame1.Sprites;


namespace MonoGame1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // Player spritesheet specific variables
        const string PLAYER_SPRITESHEET_PATH = "Player/PlayerAll";
        const int PLAYER_ANIMATION_FRAMES = 3;
        const int PLAYER_SPRITESHEET_COLUMNS = 6;
        const int PLAYER_SPRITESHEET_ROWS = 10;
        const int PLAYER_UPWALK_ROW = 1;
        const int PLAYER_DOWNWALK_ROW = 0;
        const int PLAYER_LEFTWALK_ROW = 3;
        const int PLAYER_RIGHTWALK_ROW = 2;
        const int PLAYER_ALLWALK_COLUMN = 0;
        
        // Monogame variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Sprite> sprites;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            sprites = new List<Sprite>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var playerSprite = new Sprite( GeneratePlayerAnimations(), new Vector2(0, 0) );

            sprites.Add( playerSprite );
        }

        private Dictionary<AnimationName, Animation> GeneratePlayerAnimations()
        {
            // Animation texture, frameWidth, frameHeight, and numFrames are assumed to be the same
            Texture2D playerSpriteSheet = Content.Load<Texture2D>(PLAYER_SPRITESHEET_PATH);
            int playerFrameWidth = playerSpriteSheet.Width / PLAYER_SPRITESHEET_COLUMNS;
            int playerFrameHeight = playerSpriteSheet.Height / PLAYER_SPRITESHEET_ROWS;

            // Define player animation positions dict
            var PlayerAnimationStartPositions = new Dictionary<AnimationName, Vector2>
            {
                [AnimationName.UpWalk] = new Vector2(playerFrameWidth * PLAYER_ALLWALK_COLUMN,
                                                     playerFrameHeight * PLAYER_UPWALK_ROW),

                [AnimationName.DownWalk] = new Vector2(playerFrameWidth * PLAYER_ALLWALK_COLUMN,
                                                       playerFrameHeight * PLAYER_DOWNWALK_ROW),

                [AnimationName.LeftWalk] = new Vector2(playerFrameWidth * PLAYER_ALLWALK_COLUMN,
                                                       playerFrameHeight * PLAYER_LEFTWALK_ROW),

                [AnimationName.RightWalk] = new Vector2(playerFrameWidth * PLAYER_ALLWALK_COLUMN, 
                                                        playerFrameHeight * PLAYER_RIGHTWALK_ROW)
            };

            var playerAnimations = new Dictionary<AnimationName, Animation>();
            foreach(AnimationName animationName in Enum.GetValues(typeof(AnimationName)))
            {
                playerAnimations[animationName] = new Animation(playerSpriteSheet,
                                                                PlayerAnimationStartPositions[animationName],
                                                                PLAYER_ANIMATION_FRAMES,
                                                                playerFrameWidth,
                                                                playerFrameHeight);
            }

            return playerAnimations;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if ( GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) )
                Exit();

            foreach (Sprite sprite in sprites)
            {
                sprite.Update( gameTime );
            }

            //Console.WriteLine("Elapsed Time:" + gameTime.ElapsedGameTime.Milliseconds);
            //Console.WriteLine("Total Time:" + gameTime.TotalGameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Sprite sprite in sprites)
            {
                spriteBatch.Draw(sprite.CurrentAnimation.Texture, sprite.Position, sprite.CurrentAnimation.SourceRect, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
