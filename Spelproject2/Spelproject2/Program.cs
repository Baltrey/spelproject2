using Raylib_cs;
using System.Numerics;
using System.Linq;

float screenheight = 800;
float screenwidth = 845;

Raylib.InitWindow((int)screenwidth, (int)screenheight, "spel");
Raylib.SetTargetFPS(60);

float speed;
string currentscene = "game";

List<Block> grid = start();
Ball ball = new();

Rectangle playerRect = new Rectangle(((int)screenwidth / 2), (((int)screenheight / 10) * 8), 175, 1);
InitGame();

while (!Raylib.WindowShouldClose())
{
    if (currentscene == "game")
    {

        if (checkPlayerWin())
        {
            // Vunnit ... 
            currentscene = "win";
            InitGame();
        }

        //startar bollen när man trycker space
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            if (!ball.active)
            {
                ball.active = true;
            }
        }
        //gör så att bollen följer efter blocket innan man startar den
        if (!ball.active)
        {
            ball.position.Y = (playerRect.y - ball.radius - 5);
            ball.position.X = (playerRect.x + 88);
        }
        move();
        //kollar om du har träffat en kub
        foreach (Block b in grid)
        {
            if (Raylib.CheckCollisionCircleRec(ball.position, ball.radius, b.rect) && (b.active != false))
            {
                b.active = false;
                ball.speed.Y *= -1.0f;

            }
        }


    }
    if (currentscene == "win" || currentscene == "end")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentscene = "game";
        }
    }
    Raylib.BeginDrawing();
    //ritar ut all grafik för spel skärmen
    Raylib.ClearBackground(Color.DARKGRAY);
    if (currentscene == "game")
    {
        foreach (var b in grid)
        {
            if (b.active != false)
            {
                Raylib.DrawRectangleRec(b.rect, b.color);
            }
        }




        Raylib.DrawRectangle((int)playerRect.x, (((int)screenheight / 10) * 8), 175, 50, Color.DARKPURPLE);
        if (ball.active)
        {
            // Bouncing ball logic
            ball.position.X += ball.speed.X;
            ball.position.Y += ball.speed.Y;

            if ((ball.position.Y <= (0 + ball.radius))) ball.speed.Y *= -1.0f;
            if ((ball.position.X >= (screenwidth - ball.radius)) || (ball.position.X <= ball.radius)) ball.speed.X *= -1.0f;
            // if (Raylib.CheckCollisionCircleRec(ball.position, ball.radius, playerRect)) ball.speed.Y *= -1.0f;
            if (Raylib.CheckCollisionCircleRec(ball.position, ball.radius, playerRect))
            {


                if (ball.position.Y > (playerRect.y))
                {
                    ball.speed.X *= -1.0f;
                }
                else
                {
                    ball.speed.Y *= -1.0f;
                }
            }
            //Score
            if (ball.position.Y >= (screenheight - ball.radius))
            {
                currentscene = "end";
                ball.active = false;
                InitGame();
            }

        }
        Raylib.DrawCircle((int)ball.position.X, (int)ball.position.Y, ball.radius, Color.ORANGE);
    }
    if (currentscene == "win")
    {
        Raylib.DrawText("Du vann grattis!!", (((int)screenwidth / 4) + 50), ((int)screenheight / 2), 25, Color.BLACK);
        Raylib.DrawText("tryck enter för att göra igen", ((int)screenwidth / 4), (((int)screenheight / 2) + 25), 25, Color.BLACK);
    }
    if (currentscene == "end")
    {
        Raylib.DrawText("Du förlorade!!", (((int)screenwidth / 4) + 50), ((int)screenheight / 2), 25, Color.BLACK);
        Raylib.DrawText("tryck enter för att göra igen", ((int)screenwidth / 4), (((int)screenheight / 2) + 25), 25, Color.BLACK);
    }
    Raylib.EndDrawing();

}
bool checkPlayerWin()
{
    foreach (Block b in grid)
    {
        if (b.active)
        {
            return false;
        }
    }

    return true;
}

void move()
{
    if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && playerRect.x > 3)
    {
        playerRect.x -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && playerRect.x < (screenwidth - 175))
    {
        playerRect.x += speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
    {
        speed = 13;
    }
    else
    {
        speed = 8f;
    }
}

static List<Block> start()
{
    List<Block> grid = new();

    int width = 100;
    int height = 50;



    Color[] colors = { Color.BLUE, Color.GREEN, Color.RED, Color.PURPLE, Color.ORANGE, Color.YELLOW, Color.SKYBLUE };

    for (int y = 0; y < 7; y++)
    {
        for (int x = 0; x < 8 + (y % 2 * 1); x++)
        {
            Block b = new();
            b.rect = new Rectangle((y % 2 * -50) + 5 + (width + 5) * x, 5 + (height + 5) * y, width, height);
            b.color = colors[y];
            b.active = true;
            grid.Add(b);
        }
    }
    return grid;
}
void InitGame()
{

    ball.position.Y = (playerRect.y - ball.radius);
    ball.position.X = (playerRect.x + 88);
    ball.radius = 25;
    ball.speed = new Vector2(9f, -7.5f);
    ball.active = false;
    speed = 8f;
    playerRect.x = 300;
    foreach (Block b in grid)
    {
        b.active = true;
    }
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

// Nico was here