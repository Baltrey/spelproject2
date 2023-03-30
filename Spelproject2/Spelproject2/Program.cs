using Raylib_cs;
using System.Numerics;
Random generator = new Random();
Raylib.InitWindow(845, 600, "spel");
Raylib.SetTargetFPS(60);

float speed = 6f;
string currentscene = "start";

List<Block> grid = start();
Ball ball = new();

Rectangle playerRect = new Rectangle(300, 500, 175, 50);
InitGame();
while (!Raylib.WindowShouldClose())
{
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
    {
        if (!ball.active) ball.active = true;
        else ball.active = false;
    }
    move();

    foreach (Block b in grid)
    {
        if (Raylib.CheckCollisionCircleRec(ball.position, ball.radius, b.rect) && (b.active = true))
        {
            b.active = false;
            ball.speed.Y *= -1.0f;

        }
    }


    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.DARKGRAY);

    foreach (var r in grid)
    {
        if (ball.active != false) Raylib.DrawRectangleRec(r.rect, r.color);
    }




    Raylib.DrawRectangle((int)playerRect.x, 500, 175, 50, Color.DARKPURPLE);
    if (ball.active)
    {
        // Bouncing ball logic
        ball.position.X += ball.speed.X;
        ball.position.Y += ball.speed.Y;

        if ((ball.position.Y >= (600 - ball.radius))) ball.speed.Y *= -1.0f;
        if ((ball.position.X >= (845 - ball.radius)) || (ball.position.X <= ball.radius)) ball.speed.X *= -1.0f;
        if (Raylib.CheckCollisionCircleRec(ball.position, ball.radius, playerRect)) ball.speed.Y *= -1.0f;

        //Score
        if (ball.position.Y <= ball.radius)
        {
            currentscene = "end";
            ball.active = false;
        }

    }
    Raylib.DrawCircle((int)ball.position.X, (int)ball.position.Y, ball.radius, Color.ORANGE);
    Raylib.EndDrawing();

}


void move()
{
    if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && playerRect.x > 3)
    {
        playerRect.x -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && playerRect.x < 675)
    {
        playerRect.x += speed;
    }
}


static List<Block> start()
{
    List<Block> grid = new();

    int width = 100;
    int height = 50;
    // int i = generator.Next(0, 9);


    Color[] colors = { Color.BLUE, Color.BLACK, Color.GREEN, Color.RED, Color.GOLD, Color.PURPLE, Color.MAGENTA, Color.ORANGE, Color.BEIGE };


    for (int y = 0; y < 6; y++)
    {
        for (int x = 0; x < 9; x++)
        {
            Block b = new();
            b.rect = new Rectangle((y % 2 * 50) - 100 + (width + 5) * x, 5 + (height + 5) * y, width, height);
            b.color = colors[x];
            grid.Add(b);
        }
    }
    return grid;
}
void InitGame()
{

    ball.position.Y = (playerRect.y - (ball.radius * 2));
    ball.position.X = (playerRect.x + ball.radius);
    ball.radius = 25;
    ball.speed = new Vector2(5f, 5f);
    ball.active = false;
}
public class Ball
{
    public Vector2 position;
    public Vector2 speed;
    public float radius;
    public bool active;
}

public class Block
{
    public Rectangle rect;
    public Color color;
    public bool active;
}