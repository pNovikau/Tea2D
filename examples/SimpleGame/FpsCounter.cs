using SFML.System;
using Tea2D.Core;

namespace SimpleGame;

public struct FpsCounter
{
    private readonly GameTime _gameTime;

    private Clock _clock;
    private int _lastTotalFrames;
    private int _fps;

    public FpsCounter(GameTime gameTime) : this()
    {
        _gameTime = gameTime;
        _clock = new Clock();
    }

    public int Fps
    {
        get
        {
            if (_clock.ElapsedTime.AsSeconds() < 1.0f)
                return _fps;

            var totalFrames = _gameTime.TotalFrames;
            _fps = totalFrames - _lastTotalFrames;
            _lastTotalFrames = totalFrames;

            _clock.Restart();

            return _fps;
        }
    }
}