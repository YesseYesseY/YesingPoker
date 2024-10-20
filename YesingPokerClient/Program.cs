using SDL2;
using YesingPokerClient.Windows;

if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
{
    Console.WriteLine("Error initializing SDL");
    return;
}

//new TestWindow(1280, 720).Start();
new NetworkTestWindow(1280, 720).Start();

SDL.SDL_Quit();

