using SFML.System;

namespace Tea2D.Core;

public class GameTime
{
    private int _totalFrames;
    private Clock _clock = new();
    private Clock _deltaClock = new();
    private Time _deltaTime;

    public int TotalFrames => _totalFrames;
    public Time GetTime => _clock.ElapsedTime;
    public Time Delta => _deltaTime;
    
    public void Update()
    {
        ++_totalFrames;
        _deltaTime = _deltaClock.Restart();
    }
}