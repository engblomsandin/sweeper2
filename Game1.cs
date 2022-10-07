using System.Net.Mime;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

namespace sweeper2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D background;
    private Texture2D unmarkedblip;
    private Texture2D markedblip;
    private Texture2D bombblip;
    private SpriteFont systemFont;

    private int rowCount = 20;
    private int columnCount = 20;

    public GridHandler gridHandler;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        background = Content.Load<Texture2D>("background");
        unmarkedblip = Content.Load<Texture2D>("unmarkedblip");
        markedblip = Content.Load<Texture2D>("markedblip");
        bombblip = Content.Load<Texture2D>("bombblip");
        systemFont = Content.Load<SpriteFont>("SystemFont");

        gridHandler = GridHandler.Instance;
        gridHandler.initializeGrid(rowCount,columnCount,unmarkedblip,markedblip,bombblip,systemFont);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                gridHandler.getGrid()[i][j].Update(gameTime);
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(background, new Rectangle(0, 0, 500, 500), Color.White);


        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                gridHandler.getGrid()[i][j].Draw(gameTime, _spriteBatch);
            }
        }


        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
