﻿using Raylib_cs;

namespace Groova;

public class BaseItemItemList : ItemList
{
    public BaseItemItemList()
    {
        ItemSize = new(100, 40);
    }

    public override void Update()
    {
        float x = Position.X;
        float y = 50;
        Position = new(x, y);

        float width = Raylib.GetScreenWidth();
        float height = Raylib.GetScreenHeight() - Position.Y - 100; // - 80
        Size = new(width, height);

        base.Update();
    }
}