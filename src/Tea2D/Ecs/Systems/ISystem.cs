﻿namespace Tea2D.Ecs.Systems;

public interface ISystem
{
    void Initialize(GameContext context);
    void Update(GameContext context);
}