using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesingPokerCommon;

namespace YesingPokerClient.Windows
{
    public class PokerWindow : BaseWindow
    {
        Texture backgroundTexture;
        public PokerWindow(int _width, int _height) : base("Poker", _width, _height)
        {
            NewBackground();
        }

        private void NewBackground()
        {
            backgroundTexture = Texture.CreateNoise(Renderer, WindowSize.X, WindowSize.Y, 0x00ff00ff, 100, 150);
        }

        public override void Render()
        {
            Renderer.DrawTexture(backgroundTexture, 0, 0);
        }
    }
}
