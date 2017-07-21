using Engineer.Engine;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.Project
{
    public class Menu : Scene2D
    {
        public Menu()
        {
            TileCollection Backgrounds = new TileCollection();
            Backgrounds.TileImages.Add(ResourceManager.Images["back"]);
            TileCollection Buttons = new TileCollection();
            Buttons.TileImages.Add(ResourceManager.Images["play"]);
            Buttons.TileImages.Add(ResourceManager.Images["quit"]);
            Buttons.TileImages.Add(ResourceManager.Images["settings"]);
            Tile BackTile = new Tile();
            BackTile.Collection = Backgrounds;
            BackTile.SetIndex(0);
            BackTile.Scale = LocalSettings.Scale;
            BackTile.Translation = new Vertex();
            Tile PlayTile = new Tile();
            PlayTile.Collection = Buttons;
            PlayTile.SetIndex(0);
            PlayTile.Scale = new Vertex(300, 150, 1);
            PlayTile.Translation = new Vertex(100, 200, 0);
            Tile QuitTile = new Tile();
            QuitTile.Collection = Buttons;
            QuitTile.SetIndex(1);
            QuitTile.Scale = new Vertex(300, 150, 1);
            QuitTile.Translation = new Vertex(100, 550, 0);
            DrawnSceneObject Back = new DrawnSceneObject("Back", BackTile);
            DrawnSceneObject Play = new DrawnSceneObject("Play", PlayTile);
            DrawnSceneObject Quit = new DrawnSceneObject("Quit", QuitTile);
            this.AddSceneObject(Back);
            this.AddSceneObject(Play);
            this.AddSceneObject(Quit);
        }
    }
}
