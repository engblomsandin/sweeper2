using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sweeper2
{
    public class GridHandler
    {

        private bool gridState = true;
        private int rowCount = 0;
        private int columnCount = 0;
        private static GridHandler instance = null;

        public static GridHandler Instance{
            get{
                if(instance == null){
                    instance = new GridHandler();
                }
                return instance;
            }
        }

        private GridHandler(){

        }

        private List<List<Blip>> blipGrid;

        public List<List<Blip>> getGrid()
        {
            return this.blipGrid;
        }

        public void initializeGrid(int rowCount, int columnCount, Texture2D unmarkedblip, Texture2D markedblip, Texture2D bombblip,SpriteFont systemfont)
        {
            this.rowCount = rowCount;
            this.columnCount = columnCount;

            this.blipGrid = new List<List<Blip>>();
            for (int i = 0; i < rowCount; i++)
            {
                this.blipGrid.Add(new List<Blip>());
                for (int j = 0; j < columnCount; j++)
                {
                    this.blipGrid[i].Add(new Blip(j, i, unmarkedblip, markedblip, bombblip,systemfont));
                }
            }
        }

        public int getSurroundedBombs(int x, int y)
        {
            int surroundedBombs = 0;
            if(x != 0 && y != 0 && y < columnCount-1 && x < rowCount - 1){
                if(this.blipGrid[y-1][x-1].getBombstate()){
                    surroundedBombs++;
                }
                if(this.blipGrid[y][x-1].getBombstate()){
                    surroundedBombs++;
                }
                if(this.blipGrid[y+1][x-1].getBombstate()){   
                    surroundedBombs++;
                }
                if(this.blipGrid[y-1][x].getBombstate()){
                    surroundedBombs++;
                }
                if(this.blipGrid[y+1][x].getBombstate()){                   
                    surroundedBombs++;
                }
                if(this.blipGrid[y-1][x+1].getBombstate()){
                    surroundedBombs++;
                }
                if(this.blipGrid[y][x+1].getBombstate()){
                    surroundedBombs++;
                }
                if(this.blipGrid[y+1][x+1].getBombstate()){
                    surroundedBombs++;
                }
            }
            return surroundedBombs;
        }
        public void defeat(){
            this.gridState = false;
        }
        public bool getGridState(){
            return this.gridState;
        }

        public void caveExplore(Blip blip){
            int y = blip.getyPosition();
            int x = blip.getxPosition();
            if(x != 0 && y != 0 && y < columnCount-1 && x < rowCount - 1){
                if(this.blipGrid[y-1][x-1].isClicked == false && this.blipGrid[blip.getyPosition()-1][blip.getxPosition()-1].getBombAmounts() == 0){
                    this.blipGrid[blip.getyPosition()-1][blip.getxPosition()-1].successfulClick();
                }
                if(this.blipGrid[y][x-1].isClicked == false && this.blipGrid[y][x-1].getBombAmounts() == 0){
                    this.blipGrid[y][x-1].successfulClick();
                }
                if(this.blipGrid[y+1][x-1].isClicked == false && this.blipGrid[y+1][x-1].getBombAmounts() == 0){   
                    this.blipGrid[y+1][x-1].successfulClick();
                }
                if(this.blipGrid[y-1][x].isClicked == false && this.blipGrid[y-1][x].getBombAmounts() == 0){
                    this.blipGrid[y-1][x].successfulClick();
                }
                if(this.blipGrid[y+1][x].isClicked == false && this.blipGrid[y+1][x].getBombAmounts() == 0){                   
                    this.blipGrid[y+1][x].successfulClick();
                }
                if(this.blipGrid[y-1][x+1].isClicked == false && this.blipGrid[y-1][x+1].getBombAmounts() == 0){
                    this.blipGrid[y-1][x+1].successfulClick();
                }
                if(this.blipGrid[y][x+1].isClicked == false && this.blipGrid[y][x+1].getBombAmounts() == 0){
                   this.blipGrid[y][x+1].successfulClick();
                }
                if(this.blipGrid[y+1][x+1].isClicked == false && this.blipGrid[y+1][x+1].getBombAmounts() == 0){
                    this.blipGrid[y+1][x+1].successfulClick();
                }
            }
        }
    }
}