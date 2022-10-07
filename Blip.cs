
using System.ComponentModel;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace sweeper2
{
    public class Blip
    {
        private bool isBomb = false;

        private bool isMarked = false;

        private bool isClicked = false;

        private int surroundedBombs = 0;

        private int xPosition = 0;
        private int yPosition = 0;

        public event EventHandler RightClick;
        public event EventHandler LeftClick;

        private MouseState _currentMouse;
        private MouseState _previousMouse;

        private Texture2D unmarkedblip;
        private Texture2D markedblip;
        private Texture2D bombblip;
        private SpriteFont systemFont;

        private GridHandler gridHandler = GridHandler.Instance;

        public Blip(int x, int y, Texture2D unmarkedblip, Texture2D markedblip, Texture2D bombblip ,SpriteFont systemFont)
        {
            this.xPosition = (x +1)  * 20;
            this.yPosition = (y +1) * 20;

            this.unmarkedblip = unmarkedblip;
            this.markedblip = markedblip;
            this.bombblip = bombblip;
            this.oneblip = oneblip;
            this.twoblip = twoblip;
            this.systemFont = systemFont;

            this.RightClick += this.onRightClick;
            this.LeftClick += this.onLeftClick;

            Random rnd = new Random();
            int num = rnd.Next(10);
            if(num < 2){
                this.setBombState(true);
            }
        }

        public void setBombState(bool input)
        {
            this.isBomb = input;
        }
        public bool getBombstate()
        {
            return this.isBomb;
        }

        public void setMarkedState()
        {
            if (this.isMarked)
            {
                this.isMarked = false;
            }
            else
            {
                this.isMarked = true;
            }

        }
        public Texture2D getMarkedState()
        {
            if(!this.isMarked)
            {
                return this.unmarkedblip;
            }
            else
            {
                return this.markedblip;
            }
        }
        public void setBombAmounts(int input)
        {
            this.surroundedBombs = input;
        }
        public int getBombAmounts()
        {
            return this.surroundedBombs;
        }

        public int getxPosition()
        {
            return this.xPosition;
        }
        public int getyPosition()
        {
            return this.yPosition;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.isClicked){
                spriteBatch.DrawString(this.systemFont, surroundedBombs, new Vector2(xPosition,yPosition),Color.Black );
            }
            else{
                spriteBatch.Draw(this.getMarkedState(), new Rectangle(this.xPosition, this.yPosition, 20, 20), Color.White);
            }
            //spriteBatch.DrawString(this.systemFont, ((xPosition/20)-1) + "-"+ ((yPosition/20)-1), new Vector2(xPosition,yPosition),Color.Black );
        }

        public void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            if (mouseRectangle.Intersects(new Rectangle(xPosition, yPosition, 20, 20)))
            {
                if (_currentMouse.RightButton == ButtonState.Released && _previousMouse.RightButton == ButtonState.Pressed)
                {
                    RightClick?.Invoke(this, new EventArgs());
                }

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    LeftClick?.Invoke(this, new EventArgs());
                }
            }

        }

        public void boom(){

        }

        public void successfulClick(){
            this.surroundedBombs = gridHandler.getSurroundedBombs((xPosition/20)-1,(yPosition/20)-1);
            this.isClicked =true;
        }

        public void onRightClick(object sender, System.EventArgs e)
        {
            this.setMarkedState();
        }
        public void onLeftClick(object sender, System.EventArgs e){
            if(isBomb){
                System.Diagnostics.Debug.WriteLine("BOOOm!"); 
                this.boom();
            }
            else{
                System.Diagnostics.Debug.WriteLine("Successful Click!");
                this.successfulClick();
                System.Diagnostics.Debug.WriteLine(this.getBombAmounts());
            }
        }
    }
}