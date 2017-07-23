using Engineer.Engine;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.Project
{
    public class LoadingScene : Scene2D
    {
        public LoadingScene()
        {
            this.Name = "LoadingScene";
            TileCollection Backgrounds = new TileCollection();
            Backgrounds.TileImages.Add(ResourceManager.Images["back"]);
            TileCollection BarCollection = new TileCollection();
            BarCollection.TileImages.Add(ResourceManager.Images["progressbar"]);
            TileCollection ProgressCollection = new TileCollection();
            ProgressCollection.TileImages.Add(ResourceManager.Images["progress"]);
            Tile BackTile = new Tile();
            BackTile.Collection = Backgrounds;
            BackTile.SetIndex(0);
            BackTile.Scale = LocalSettings.Scale;
            BackTile.Translation = new Vertex();
            Tile BarTile = new Tile();
            BarTile.Collection = BarCollection;
            BarTile.SetIndex(0);
            BarTile.Scale = new Vertex(800, 60, 1);
            BarTile.Translation = new Vertex(112, 500, 0);
            DrawnSceneObject Back = new DrawnSceneObject("Back", BackTile);
            DrawnSceneObject Bar = new DrawnSceneObject("Bar", BarTile);
            this.AddSceneObject(Back);
            this.AddSceneObject(Bar);
        }
    }
}
