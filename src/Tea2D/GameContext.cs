using SFML.Graphics;
using Tea2D.Core;
using Tea2D.Ecs;

namespace Tea2D;

public struct GameContext
{
    public GameTime GameTime;
    public IGameWorld GameWorld;
    public RenderWindow RenderWindow;
    //public IApplication Application;
}