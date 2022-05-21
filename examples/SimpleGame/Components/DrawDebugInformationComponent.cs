#if DEBUG
using SFML.Graphics;
using Tea2D.Ecs.Components;

namespace SimpleGame.Components;

public struct DrawDebugInformationComponent : IComponent<DrawDebugInformationComponent>
{
    public int Id { get; init; }

    public Text Text;
}

#endif