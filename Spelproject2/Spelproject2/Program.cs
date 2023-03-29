using Raylib_cs;
Raylib.InitWindow(845, 600, "spel");
Raylib.SetTargetFPS(60);

float speed = 6f;
float enemieX = 0;
float enemieY = 0;

List<Rectangle> grid = start();


Rectangle playerRect = new Rectangle(300, 500, 175, 50);
while (!Raylib.WindowShouldClose())
{
    move();

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.DARKGRAY);

    foreach (Rectangle r in grid)
    {
        Raylib.DrawRectangleRec(r, color[y]);
    }


    Raylib.DrawRectangle((int)playerRect.x, 500, 175, 50, Color.DARKPURPLE);

    Raylib.EndDrawing();

}


void move()
{
    if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && playerRect.x > 3)
    {
        playerRect.x -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && playerRect.x < 625)
    {
        playerRect.x += speed;
    }
}


static List<Rectangle> start()
{
    List<Rectangle> grid = new();

    int width = 100;
    int height = 50;
    Color[] colors = {Color.BLUE, Color.BLACK, Color.GREEN, Color.RED,Color.GOLD, Color.PURPLE, Color.MAGENTA, Color.ORANGE};

    for (int y = 0; y < 6; y++)
    {
        for (int x = 0; x < 8; x++)
        {
            grid.Add(new Rectangle((y % 2 * 35) + 5 + (width + 5) * x, 5 + (height + 5) * y, width, height));
        }
    }
    return grid;
}
