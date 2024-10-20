using YesingPokerCommon;

namespace YesingPokerClient.Windows
{
    public class TestWindow : BaseWindow
    {
        Texture testTexture;
        Texture plrTexture;
        public TestWindow(int width, int height) : base("Test Window", width, height)
        {
            testTexture = Texture.CreateSolidColorTexture(Renderer, WindowSize.X, WindowSize.Y, 0xff00ffff);
            plrTexture = Texture.CreateSolidColorTexture(Renderer, 50, 50, 0xffff00ff);
        }

        public override void Render()
        {
            Renderer.DrawTexture(testTexture, 0, 0);
            Renderer.DrawTexture(plrTexture, MousePos.X, MousePos.Y);
        }

        public override void OnWindowResize()
        {
            testTexture.Destroy();
            testTexture = Texture.CreateSolidColorTexture(Renderer, WindowSize.X, WindowSize.Y, 0xff00ffff);
        }
    }
}
