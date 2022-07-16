using System;
using Tea2D.Common;
using Tea2D.Core.Memory;

namespace Tea2D.Graphics;

public interface IWindow : IDisposable
{
    /// <summary>
    /// Position of the window
    /// </summary>
    Vector2I Position { get; set; }

    /// <summary>
    /// Size of the rendering region of the window
    /// </summary>
    Vector2U Size { get; set; }

    /// <summary>
    /// Call the event handlers for each pending event
    /// </summary>
    void DispatchEvents();

    /// <summary>
    /// Change the title of the window
    /// </summary>
    /// <param name="title">New title</param>
    void SetTitle(ref ValueString title);
}