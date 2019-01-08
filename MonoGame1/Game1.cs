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
        //Starting positions
        Vector2 PLAYER_STARTING_POSITION = new Vector2(0, 0);
        Vector2 GOBLIN_STARTING_POSITION = new Vector2(600, 200);

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

            // Player
            PlayerSpriteInfo.Texture = Content.Load<Texture2D>(PlayerSpriteInfo.SPRITESHEET_PATH);
            var playerInput = new Input(Keys.Up, Keys.Down, Keys.Left, Keys.Right);
            var playerSprite = new Sprite( PlayerSpriteInfo.GenerateAnimations(), PLAYER_STARTING_POSITION, playerInput, true);
            sprites.Add( playerSprite );

            //Goblin
            GoblinSpriteInfo.Texture = Content.Load<Texture2D>(GoblinSpriteInfo.SPRITESHEET_PATH);
            var goblinSprite = new Sprite( GoblinSpriteInfo.GenerateAnimations(), GOBLIN_STARTING_POSITION, null, false );
            sprites.Add( goblinSprite );
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
